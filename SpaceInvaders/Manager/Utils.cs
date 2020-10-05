using SpaceInvaders.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Manager
{
    class Utils
    {
        public static Random rand = new Random(Guid.NewGuid().GetHashCode());

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        public static int min(int a, int b)
        {
            if (a < b)
                return a;
            return b;
        }

        public static void drawScore(Graphics g, Game gameInstance)
        {
            g.DrawString("SCORE: " + gameInstance.score, new Font(System.Drawing.FontFamily.GenericSansSerif, 18f, FontStyle.Regular), new SolidBrush(System.Drawing.Color.Blue), 1100, 0);
        }

        private static void createDirectory(String directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static void saveUnmanagedMemoryStream(UnmanagedMemoryStream stream, String filePath)
        {
            Stream file = File.Create(filePath);
            stream.CopyTo(file);
            file.Close();
        }

        public static void initSound()
        {
            String path = @".\shoot\shootCopy.wav";
            createDirectory(@".\sound");
            createDirectory(@".\sound\shoot");
            createDirectory(@".\sound\explosion");
            createDirectory(@".\sound\R2");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.BattleAlarm, @".\sound\BattleAlarm.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.Explosion_1, @".\sound\explosion\1.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.Explosion_2, @".\sound\explosion\2.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.Explosion_3, @".\sound\explosion\3.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.Explosion_4, @".\sound\explosion\4.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.HyperdriveTrouble, @".\sound\HyperdriveTrouble.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.JumpToLightspeed, @".\sound\JumpToLightspeed.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.shoot, @".\sound\shoot.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.theme, @".\sound\theme.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.R2_1, @".\sound\R2\1.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.R2_2, @".\sound\R2\2.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.R2_3, @".\sound\R2\3.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.R2_4, @".\sound\R2\4.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.R2_5, @".\sound\R2\5.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.R2_6, @".\sound\R2\6.wav");
            saveUnmanagedMemoryStream(SpaceInvaders.Properties.Resources.R2_7, @".\sound\R2\7.wav");
        }






        
    }
}
