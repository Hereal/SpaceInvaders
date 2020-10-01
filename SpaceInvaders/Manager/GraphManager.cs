using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Manager
{
    class GraphManager
    {

        public static void ChangeColor(Game gameInstance,Bitmap image)
        {

            foreach (GameObject gm in gameInstance.gameObjects)
            {
                int dx = (int)gm.vector.x;
                int dy = (int)gm.vector.y;
                int width = gm.GetImage().Width;
                int height = gm.GetImage().Height;
                for (int x = dx; x < dx + width; x++)
                {
                    for (int y = dy; y < dy + height; y++)
                    {
                        if (x >= 0 && y >= 0 && x < image.Width && y < image.Height)
                        {

                            if (image.GetPixel(x, y).A >= 150)
                            {
                                image.SetPixel(x, y, Color.White);
                            }
                            if (image.GetPixel(x, y).A < 150)
                            {
                                image.SetPixel(x, y, Color.Empty);
                            }
                            if (image.GetPixel(x, y).A != 0)
                            {

                                Color randomColor = Color.FromArgb(Utils.rand.Next(256), Utils.rand.Next(256), Utils.rand.Next(256));
                                image.SetPixel(x, y, randomColor);
                            }
                        }
                    }
                }
            }


        }

    }
}
