using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Media;
using System.IO;
using SpaceInvaders.Particule;
using SpaceInvaders.Manager;

namespace SpaceInvaders
{
    public partial class GameForm : Form
    {

        #region fields
        /// <summary>
        /// Instance of the game
        /// </summary>
        private Game game;

        #region time management
        /// <summary>
        /// Game watch
        /// </summary>
        Stopwatch watch = new Stopwatch();

        /// <summary>
        /// Last update time
        /// </summary>
        long lastTime = 0;


        MediaPlayer theme = new MediaPlayer();
        MediaPlayer begining = new MediaPlayer();
        #endregion

        #endregion

        #region constructor
        /// <summary>
        /// Create form, create game
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
            game = Game.CreateGame(this.ClientSize);
            watch.Start();
            WorldClock.Start();

        }
        #endregion

        #region events
        /// <summary>
        /// Paint event of the form, see msdn for help => paint game with double buffering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics bg = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle);
            Graphics g = bg.Graphics;
            g.Clear(System.Drawing.Color.Black);

            game.Draw(g);

            bg.Render();
            bg.Dispose();

        }

        /// <summary>
        /// Tick event => update game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorldClock_Tick(object sender, EventArgs e)
        {
            // lets do 5 ms update to avoid quantum effects
            int maxDelta = 5;

            // get time with millisecond precision
            long nt = watch.ElapsedMilliseconds;
            // compute ellapsed time since last call to update
            double deltaT = (nt - lastTime);

            for (; deltaT >= maxDelta; deltaT -= maxDelta)
                game.Update(maxDelta / 1000.0);

            game.Update(deltaT / 1000.0);

            // remember the time of this update
            lastTime = nt;

            Invalidate();

        }

        /// <summary>
        /// Key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            game.keyPressed.Add(e.KeyCode);
        }

        /// <summary>
        /// Key up event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            game.keyPressed.Remove(e.KeyCode);
        }

        #endregion

        private void GameForm_Load(object sender, EventArgs e)
        {
            Utils.rand.Next();
            Utils.initSound();
            Game.player = new Player(650, 600, 10);
            game.AddNewGameObject(Game.player);

            Game.shipGang = new ShipGang(4, game);

            game.AddNewGameObject(new Bunker(100, 500, 10000));
            game.AddNewGameObject(new Bunker(300, 500, 10000));
            game.AddNewGameObject(new Bunker(500, 500, 10000));
            game.AddNewGameObject(new Bunker(700, 500, 10000));
            game.AddNewGameObject(new Bunker(900, 500, 10000));

            theme.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @".\sound\theme.wav")));
            theme.Volume = 0.2;
            theme.MediaEnded += new EventHandler(Media_Ended);
            //theme.Play();

            begining.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @".\sound\BattleAlarm.wav")));
            begining.Volume = 0.5;
            begining.Play();

        }
        private void Media_Ended(object sender, EventArgs e)
        {
            theme.Position = TimeSpan.Zero;
            theme.Play();
        }
    }

}
