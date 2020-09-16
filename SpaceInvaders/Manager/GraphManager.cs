using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Manager
{
    class GraphManager
    {
        public static Bitmap bufferedImage = new Bitmap(297, 219);//résolution du jeu avant upscale
        private static Random rnd = new Random(DateTime.Now.Millisecond);
        public static void Draw(Game gameInstance, Graphics graphics)
        {
            ChangeColor(gameInstance);
            Bitmap arcade = SpaceInvaders.Properties.Resources.arcade;
            Graphics arcadeGraphics = Graphics.FromImage(arcade);
            arcadeGraphics.DrawImage(bufferedImage, 5, 28, 297, 219);
            arcadeGraphics.DrawImage(SpaceInvaders.Properties.Resources.arcade, 0, 0, 307, 307);

            int min = gameInstance.gameSize.Width;
            if (gameInstance.gameSize.Height < min)
            {
                min = gameInstance.gameSize.Height;
            }
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            graphics.DrawImage(arcade, 0, 0, min, min);

            bufferedImage = new Bitmap(297, 219);
        }

        public static void DrawBufferedImage(Game gameInstance,Bitmap image, int x, int y)
        {
            Graphics g = Graphics.FromImage(bufferedImage);
            g.DrawImage(image, (int)x, (int)y);
        }

        public static void ChangeColor(Game gameInstance)
        {
            
            foreach (GameObject gm in gameInstance.gameObjects)
            {
                int dx = gm.GetCoord().X;
                int dy = gm.GetCoord().Y;
                int width = gm.GetImage().Width;
                int height = gm.GetImage().Height;
                for (int x = dx; x < dx+width; x++)
                {
                    for (int y = dy; y < dy+height; y++)
                    {
                        if(x>=0&& y >= 0&&x<bufferedImage.Width && y < bufferedImage.Height)
                        {
                            
                            if (bufferedImage.GetPixel(x, y).A >= 150)
                            {
                                bufferedImage.SetPixel(x, y, Color.White);
                            }
                            if (bufferedImage.GetPixel(x, y).A < 150)
                            {
                                bufferedImage.SetPixel(x, y, Color.Empty);
                            }
                            if (bufferedImage.GetPixel(x, y).A != 0)
                            {
                                
                                Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                                bufferedImage.SetPixel(x, y, randomColor);
                            }
                        }
                    }
                }
            }

            
        }

    }
}
