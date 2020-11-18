using SpaceInvaders.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Manager
{

    /// <summary>
    /// A Utils class
    /// </summary>
    static class Utils
    {
        public static Random rand = new Random(Guid.NewGuid().GetHashCode());

        /// <summary>
        /// Return the minimal value between two int
        /// </summary>
        /// <param name="a">int</param>
        /// <param name="b">int</param>
        /// <returns>minimal value</returns>
        public static int Min(int a, int b)
        {
            if (a < b) return a;
            return b;
        }


        /// <summary>
        /// Draw the ath on the screen
        /// </summary>
        /// <param name="g">The graphic instance of the game</param>
        /// <param name="gameInstance">The instance of the game</param>
        public static void DrawATH(Graphics g, Game gameInstance)
        {
            DrawScore(g, gameInstance);
            DrawLives(g, gameInstance);
        }


        /// <summary>
        /// Draw the score on the screen
        /// </summary>
        /// <param name="g">The graphic instance of the game</param>
        /// <param name="gameInstance">The instance of the game</param>
        public static void DrawScore(Graphics g, Game gameInstance)
        {
            g.DrawString("SCORE: " + gameInstance.score, new Font(System.Drawing.FontFamily.GenericSansSerif, 18f, FontStyle.Regular), new SolidBrush(System.Drawing.Color.Blue), 1100, 0);
        }

        /// <summary>
        /// Draw the pause on the screen
        /// </summary>
        /// <param name="g">The graphic instance of the game</param>
        /// <param name="gameInstance">The instance of the game</param>
        public static void DrawPause(Graphics g, Game gameInstance)
        {
            g.DrawString("PAUSE", new Font(System.Drawing.FontFamily.GenericSansSerif, 100f, FontStyle.Regular), new SolidBrush(System.Drawing.Color.Red), 350, 200);
            g.DrawString("press P to resume", new Font(System.Drawing.FontFamily.GenericSansSerif, 30f, FontStyle.Regular), new SolidBrush(System.Drawing.Color.Red), 450, 500);
        }

        /// <summary>
        /// Draw the restart screen
        /// </summary>
        /// <param name="g">The graphic instance of the game</param>
        /// <param name="gameInstance">The instance of the game</param>
        public static void DrawRestart(Graphics g, Game gameInstance)
        {
            g.DrawString("Game Over", new Font(System.Drawing.FontFamily.GenericSansSerif, 100f, FontStyle.Regular), new SolidBrush(System.Drawing.Color.Red), 300, 200);
            g.DrawString("press SPACE to restart", new Font(System.Drawing.FontFamily.GenericSansSerif, 30f, FontStyle.Regular), new SolidBrush(System.Drawing.Color.Red), 450, 500);
        }

        /// <summary>
        /// Draw the win on the screen
        /// </summary>
        /// <param name="g">The graphic instance of the game</param>
        /// <param name="gameInstance">The instance of the game</param>
        public static void DrawWin(Graphics g, Game gameInstance)
        {
            g.DrawString("Win", new Font(System.Drawing.FontFamily.GenericSansSerif, 100f, FontStyle.Regular), new SolidBrush(System.Drawing.Color.Green), 500, 200);
            g.DrawString("press SPACE to restart", new Font(System.Drawing.FontFamily.GenericSansSerif, 30f, FontStyle.Regular), new SolidBrush(System.Drawing.Color.Green), 450, 500);
        }










        /// <summary>
        /// Draw the lives on the screen
        /// </summary>
        /// <param name="g">The graphic instance of the game</param>
        /// <param name="gameInstance">The instance of the game</param>
        public static void DrawLives(Graphics g, Game gameInstance)
        {
            Bitmap image = SpaceInvaders.Properties.Resources.heart;
            for (int i = 0; i < gameInstance.nbLife-1; i++)
                g.DrawImage(image, i * 50+5, 600, image.Width, image.Height);
        }



        /// <summary>
        /// Create Directory if not exist
        /// </summary>
        /// <param name="directory"></param>
        private static void CreateDirectory(String directory)
        {
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        }

        /// <summary>
        /// Save an UnmanagedMemoryStream to a file
        /// </summary>
        /// <param name="stream">UnmanagedMemoryStream</param>
        /// <param name="filePath">File name</param>
        private static void SaveUnmanagedMemoryStream(UnmanagedMemoryStream stream, String filePath)
        {
            Stream file = File.Create(filePath);
            stream.CopyTo(file);
            file.Close();
        }

        /// <summary>
        /// Init all the sounds at the beginning
        /// </summary>
        public static void InitSound()
        {
            CreateDirectory(@".\sound");
            CreateDirectory(@".\sound\shoot");
            CreateDirectory(@".\sound\explosion");
            CreateDirectory(@".\sound\R2");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.BattleAlarm, @".\sound\BattleAlarm.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.Explosion_1, @".\sound\explosion\1.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.Explosion_2, @".\sound\explosion\2.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.Explosion_3, @".\sound\explosion\3.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.Explosion_4, @".\sound\explosion\4.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.HyperdriveTrouble, @".\sound\HyperdriveTrouble.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.JumpToLightspeed, @".\sound\JumpToLightspeed.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.shoot, @".\sound\shoot.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.theme, @".\sound\theme.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.R2_1, @".\sound\R2\1.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.R2_2, @".\sound\R2\2.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.R2_3, @".\sound\R2\3.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.R2_4, @".\sound\R2\4.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.R2_5, @".\sound\R2\5.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.R2_6, @".\sound\R2\6.wav");
            SaveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.R2_7, @".\sound\R2\7.wav");
        }
    }
}
