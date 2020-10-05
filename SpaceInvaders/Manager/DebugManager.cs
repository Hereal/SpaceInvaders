using SpaceInvaders.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Manager
{
    class DebugManager
    {
        public static double AverageFrametime = 0;
        public static double StackFrametime = 0;
        public static double NumberOfFrame = 0;
        public static double MinFrameTime = 999999;
        public static double MaxFrameTime = 0;
        public static List<int> FrameTimeLst = new List<int>();
        public static int timestamp = 0;


        public static void HandleFrametime(int start)
        {
            int frameTime = DateTime.Now.Millisecond - start;
            if (frameTime >= 0)
            {
                FrameTimeLst.Add(frameTime);
                if (frameTime < MinFrameTime) MinFrameTime = frameTime;
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
            if (FrameTimeLst.Count >= 450) FrameTimeLst.RemoveAt(0);
        }

        public static void drawGameObjectDebug(Graphics g, GameObject gm)
        {
            if (gm is SpaceObject)
            {
                SpaceObject so = (SpaceObject)gm;
                g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Red), new Rectangle((int)so.vector.x, (int)so.vector.y, so.image.Width - 1, so.image.Height - 1));
            }
        }

        public static void drawDebug(Graphics g)
        {
            string s = "AverrageFrameTime: " + AverageFrametime + "\n LowestFrameTime: " + MinFrameTime + "\n MaxFrameTime: " + MaxFrameTime + "\n HyperSpace: " + Game.hyperDrive;

            g.DrawString(s, new Font(System.Drawing.FontFamily.GenericSansSerif, 12f, FontStyle.Regular), new SolidBrush(System.Drawing.Color.Red), 0, 0);
            drawDebugGraph(g);
        }

        public static void drawDebugGraph(Graphics g)
        {
            Color bg = Color.FromArgb(20, 255, 255, 255);
            g.FillRectangle(new SolidBrush(bg), 15, 541, 450, 150);
            int y = 541 + 150;
            int max = FrameTimeLst.Max();
            g.DrawString("" + max, new Font(System.Drawing.FontFamily.GenericSansSerif, 12f, FontStyle.Regular), new SolidBrush(System.Drawing.Color.Red), 0, 536);
            for (int x = 0; x < FrameTimeLst.Count; x++)
            {
                Color c;
                if (FrameTimeLst[x] < 10) c = Color.Green;
                else if (FrameTimeLst[x] < 16) c = Color.Orange;
                else c = Color.Red;
                int lenght = (int)(150 / (double)max) * FrameTimeLst[x];
                g.DrawLine(new Pen(c), x + 15, y, x + 15, y - lenght);
            }
        }
    }
}
