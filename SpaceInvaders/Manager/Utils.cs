using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Manager
{
    class Utils
    {
        public static double AverageFrametime = 0;
        public static double StackFrametime = 0;
        public static double NumberOfFrame = 0;
        public static double LowestFrameTime = 999999;
        public static double MaxFrameTime = 0;
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

        public static void HandleFrametime(int start)
        {

            int frameTime = DateTime.Now.Millisecond - start;
            if (frameTime >= 0)
            {
                if (frameTime < LowestFrameTime) LowestFrameTime = frameTime;
                if (frameTime > MaxFrameTime) MaxFrameTime = frameTime;
                StackFrametime += frameTime;
                NumberOfFrame++;
            }
            if (NumberOfFrame >= 10)
            {
                AverageFrametime = StackFrametime / NumberOfFrame;
                StackFrametime = 0;
                NumberOfFrame = 0;
            }
        }

        public static void drawGameObjectDebug(Graphics g, GameObject gm)
        {
            g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Red), new Rectangle((int)gm.vector.x, (int)gm.vector.y, gm.GetImage().Width - 1, gm.GetImage().Height - 1));
        }

        public static void drawDebug(Graphics g)
        {
            string s = "AverrageFrameTime: " + AverageFrametime + "\n LowestFrameTime: " + Utils.LowestFrameTime + "\n MaxFrameTime: " + Utils.MaxFrameTime + "\n HyperSpace: " + Game.hyperDrive;
            
            g.DrawString(s, new Font(System.Drawing.FontFamily.GenericSansSerif, 12f, FontStyle.Regular), new SolidBrush(System.Drawing.Color.Red), 0, 0);


        }
        public static void drawScore(Graphics g,Game gameInstance)
        {
            g.DrawString("SCORE: " + gameInstance.score, new Font(System.Drawing.FontFamily.GenericSansSerif, 18f, FontStyle.Regular), new SolidBrush(System.Drawing.Color.Blue), 1100, 0);
        }
    }
}
