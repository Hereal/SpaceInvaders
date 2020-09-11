using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SpaceInvaders
{
    class ArrayGraphics
    {
        public static GameObject[,] pixelsArray = null;

        
        public static void Draw(GameObject gameObject,Bitmap image, int x, int y)
        {
            //Console.WriteLine("Taille table: " + pixelsArray.GetLength(0) + " x " + pixelsArray.GetLength(1));
            for (int i = 0; i < image.Width; i++)
                for (int j = 0; j < image.Height; j++)
                {
                    if(image.GetPixel(i, j).A != 0)
                    {
                        //pixelsArray[x + i, y + j] = gameObject;
                    }
                }
        }


    }
}
