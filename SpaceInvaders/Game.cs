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

        public static Player player;
        public static ShipGang shipGang;
        public static bool debug = false;
        public static bool hyperDrive = false;
        public static bool pause = false;
        public int nbLife = 3;
        private MediaPlayer hyperDriveSound = new MediaPlayer();
        private MediaPlayer theme = new MediaPlayer();
        private MediaPlayer begining = new MediaPlayer();
        public int score = 0;
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
            if (game == null) game = new Game(gameSize);
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
            if (!pause)
            {
                int start = DateTime.Now.Millisecond;
                DrawGame(g);
                DebugManager.HandleFrametime(start);
            }
            else if (nbLife == 0 && shipGang.IsAlive()) Utils.DrawWin(g, this);
            else if (nbLife == 0 &&!shipGang.IsAlive()) Utils.DrawRestart(g,this);
            else Utils.DrawPause(g, this);
        }

        /// <summary>
        /// Draw the game
        /// </summary>
        /// <param name="g"></param>
        private void DrawGame(Graphics g)
        {
            if (player.IsAlive()) player.Draw(this, g);
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(this, g);
                if (debug) DebugManager.DrawGameObjectDebug(g, gameObject);
            }
            foreach (Particle particle in particles) particle.Draw(g);
            if (debug) DebugManager.DrawDebug(g);
            Utils.DrawATH(g, this);

        }




        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            if (!pause)
            {
                shipGang.Update(this, deltaT);
                if (shipGang.IsAlive())
                {
                    pause = true;
                    nbLife = 0;
                }
                particles.UnionWith(ParticleGenerator.GenerateStars());
                gameObjects.UnionWith(pendingNewGameObjects);
                pendingNewGameObjects.Clear();

                UpdateEachObjects(deltaT);
                RemoveDeadObject();
            }
            HandleKeys(deltaT);
        }

        /// <summary>
        /// Handle the key press
        /// </summary>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        private void HandleKeys(double deltaT)
        {
            if (!pause)
            {
                if (keyPressed.Contains(Keys.Space)) player.Shoot(game, deltaT);
                if (keyPressed.Contains(Keys.Left)) player.MoveLeft(game, deltaT);
                if (keyPressed.Contains(Keys.Right)) player.MoveRight(game, deltaT);
                if (keyPressed.Contains(Keys.F3)) F3Action();
                if (keyPressed.Contains(Keys.H)) HandleHyperDrive();
            }
            if (keyPressed.Contains(Keys.P)) PAction();
            if (nbLife == 0 && keyPressed.Contains(Keys.Space)) SpaceRestartAction();
        }

        /// <summary>
        /// Perform F3 key actions
        /// </summary>
        private void F3Action()
        {
            debug = !debug;
            ReleaseKey(Keys.F3);
        }

        /// <summary>
        /// Perform p key actions
        /// </summary>
        private void PAction()
        {
            pause = !pause;
            ReleaseKey(Keys.P);
        }

        /// <summary>
        /// Perform Space key actions to restart
        /// </summary>
        private void SpaceRestartAction()
        {
            nbLife = 3;
            StartNewGame(false);
            ReleaseKey(Keys.Space);
            pause = false;
        }


        /// <summary>
        /// Enable or disable the hyper drive
        /// </summary>
        private void HandleHyperDrive()
        {
            if (hyperDrive) hyperDriveSound.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @".\sound\HyperdriveTrouble.wav")));
            else hyperDriveSound.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @".\sound\JumpToLightspeed.wav")));
            hyperDriveSound.Play();
            hyperDrive = !hyperDrive;
            ReleaseKey(Keys.H);
        }


        /// <summary>
        /// Update all the objects
        /// </summary>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        private void UpdateEachObjects(double deltaT)
        {
            player.Update(this, deltaT);
            foreach (GameObject gameObject in gameObjects) gameObject.Update(this, deltaT);
            foreach (Particle particle in particles) particle.Update(this, deltaT);
        }



        /// <summary>
        /// Remove all the dead objects
        /// </summary>
        private void RemoveDeadObject()
        {
            gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
            particles.RemoveWhere(generator => !generator.IsAlive());
        }



        /// <summary>
        /// Start a new game
        /// </summary>
        /// <param name="loadSound">Should he reload the data</param>
        public void StartNewGame(bool loadSound)
        {
            gameObjects = new HashSet<GameObject>();
            particles = new HashSet<Particle>();
            if(loadSound) Utils.InitSound();
            player = new Player(650, 600, 10);
            game.AddNewGameObject(Game.player);
            shipGang = new ShipGang(4, game);
            GenerateBunker();
            playSound();
        }

        /// <summary>
        /// Generate all the bunkers
        /// </summary>
        private void GenerateBunker()
        {
            game.AddNewGameObject(new Bunker(100, 500, 10000));
            game.AddNewGameObject(new Bunker(300, 500, 10000));
            game.AddNewGameObject(new Bunker(500, 500, 10000));
            game.AddNewGameObject(new Bunker(700, 500, 10000));
            game.AddNewGameObject(new Bunker(900, 500, 10000));
        }

        /// <summary>
        /// Play sounds at the beginning
        /// </summary>
        private void playSound()
        {
            theme.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @".\sound\theme.wav")));
            theme.Volume = 0.2;
            theme.MediaEnded += new EventHandler(Media_Ended);
            theme.Play();

            begining.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @".\sound\BattleAlarm.wav")));
            begining.Volume = 0.5;
            begining.Play();
        }

        /// <summary>
        /// When the theme as ended it start again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Media_Ended(object sender, EventArgs e)
        {
            theme.Position = TimeSpan.Zero;
            theme.Play();
        }
        #endregion
    }
}
