using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using SpaceInvaders.Manager;
using SpaceInvaders.Particule;
using System.Windows.Media;
using System.IO;

namespace SpaceInvaders
{
    class Game
    {

        #region GameObjects management
        /// <summary>
        /// Set of all particles currently in the game
        /// </summary>
        public HashSet<Particle> particles = new HashSet<Particle>();

        /// <summary>
        /// Set of all game objects currently in the game
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);
        }
        #endregion

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        /// <summary>
        /// A shared black brush
        /// </summary>
        private static System.Drawing.Brush blackBrush = new SolidBrush(System.Drawing.Color.Black);

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);

        public static Player player;
        public static ShipGang shipGang;
        public static bool debug = false;
        public static bool hyperDrive = false;
        private MediaPlayer hyperDriveSound = new MediaPlayer();
        public static double deltaT;
        #endregion


        #region constructors
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// 
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null)
                game = new Game(gameSize);
            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
            this.gameSize = gameSize;
        }

        #endregion

        #region methods

        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }


        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            int start = DateTime.Now.Millisecond;
            if (player.IsAlive())
                player.Draw(this, g);
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(this, g);

                if (debug)
                    Utils.drawGameObjectDebug(g, gameObject);
            }

            foreach (Particle particle in particles)
            {
                particle.Draw(g);
            }
            if (debug)
                Utils.drawDebug(g);

            Utils.HandleFrametime(start);
            //Console.WriteLine("frameTime: " + (DateTime.Now.Millisecond - start));
        }

        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {

            Game.deltaT = deltaT;
            shipGang.Update(this, deltaT);
            particles.UnionWith(ParticleGenerator.GenerateStars());
            // add new game objects
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();


            // if space is pressed
            if (keyPressed.Contains(Keys.Space))
                player.Shoot(game, deltaT);
            // if left is pressed
            if (keyPressed.Contains(Keys.Left))
                player.MoveLeft(game, deltaT);
            // if right is pressed
            if (keyPressed.Contains(Keys.Right))
                player.MoveRight(game, deltaT);
            // if d is pressed
            if (keyPressed.Contains(Keys.D))
            {
                debug = !debug;
                ReleaseKey(Keys.D);
            }
            // if h is pressed
            if (keyPressed.Contains(Keys.H))
            {
                if(hyperDrive)
                    hyperDriveSound.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\sound\HyperdriveTrouble.wav")));
                else
                    hyperDriveSound.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\sound\JumpToLightspeed.wav")));
                hyperDriveSound.Play();
                hyperDrive = !hyperDrive;
                ReleaseKey(Keys.H);
            }

            // update each game object
            player.Update(this, deltaT);
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(this, deltaT);
            }
            foreach (Particle particle in particles)
            {
                particle.Update(this, deltaT);
            }

            // remove dead objects
            gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
            particles.RemoveWhere(generator => !generator.IsAlive());

        }
        #endregion
    }
}
