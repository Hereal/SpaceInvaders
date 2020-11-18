using SpaceInvaders.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Manager
{
    /// <summary>
    /// Class for managing the debug mode
    /// </summary>
    static class DebugManager
    {
        private static double AverageFrametime = 0;
        private static double StackFrametime = 0;
        private static double NumberOfFrame = 0;
        private static double MinFrameTime = 999999;
        private static double MaxFrameTime = 0;
        private static List<int> FrameTimeLst = new List<int>();


        /// <summary>
        /// Handle the Debug FrameTime
        /// </summary>
        /// <param name="start"> Time stamp of the drawing beginning </param>
        public static void HandleFrametime(int start)
        {
            int frameTime = DateTime.Now.Millisecond - start;
            if (frameTime >= 0) SavingFrameTime(frameTime);
            if (NumberOfFrame >= 10) ResetFrameTime();
            if (FrameTimeLst.Count >= 450) FrameTimeLst.RemoveAt(0);
        }

        /// <summary>
        /// Save the frametime to compute the average frametime
        /// </summary>
        /// <param name="start"> Time stamp of the drawing beginning </param>
        private static void SavingFrameTime(int frameTime)
        {
            FrameTimeLst.Add(frameTime);
            if (frameTime < MinFrameTime) MinFrameTime = frameTime;
            if (frameTime > MaxFrameTime) MaxFrameTime = frameTime;
            StackFrametime += frameTime;
            NumberOfFrame++;
        }

        /// <summary>
        /// Reset the average frametime
        /// </summary>
        private static void ResetFrameTime()
        {
            AverageFrametime = StackFrametime / NumberOfFrame;
            StackFrametime = 0;
            NumberOfFrame = 0;
        }

        /// <summary>
        /// Draw a square around all the SpaceObjects
        /// </summary>
        /// <param name="g"> The graphic instance for the game</param>
        /// <param name="gm"> The GameObject to draw </param>
        public static void DrawGameObjectDebug(Graphics g, GameObject gm)
        {
            if (gm is SpaceObject)
            {
                SpaceObject so = (SpaceObject)gm;
                g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Red), new Rectangle((int)so.vector.x, (int)so.vector.y, so.image.Width - 1, so.image.Height - 1));
            }
        }
        /// <summary>
        /// Write some text
        /// </summary>
        /// <param name="g"> The graphic instance for the game</param>
        public static void DrawDebug(Graphics g)
        {
            string s = "AverrageFrameTime: " + AverageFrametime + "\n LowestFrameTime: " + MinFrameTime + "\n MaxFrameTime: " + MaxFrameTime + "\n HyperSpace: " + Game.hyperDrive;
            g.DrawString(s, new Font(System.Drawing.FontFamily.GenericSansSerif, 12f, FontStyle.Regular), new SolidBrush(System.Drawing.Color.Red), 0, 0);
            DrawDebugGraph(g);
        }

        /// <summary>
        /// Draw a debug graph with all the past 450 frametimes
        /// </summary>
        /// <param name="g"> The graphic instance for the game</param>
        public static void DrawDebugGraph(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(20, 255, 255, 255)), 15, 541, 450, 150);
            int y = 541 + 150, max = FrameTimeLst.Max();
            g.DrawString("" + max, new Font(System.Drawing.FontFamily.GenericSansSerif, 12f, FontStyle.Regular), new SolidBrush(System.Drawing.Color.Red), 0, 536);
            for (int x = 0; x < FrameTimeLst.Count; x++)
            {
                Color c = GetDebugGraphLineColor(FrameTimeLst[x]);
                int lenght = (int)(150 / (double)max) * FrameTimeLst[x];
                g.DrawLine(new Pen(c), x + 15, y, x + 15, y - lenght);
            }
        }

        /// <summary>
        /// Get the color to draw for a specified frameTime
        /// </summary>
        /// <param name="frametime"></param>
        /// <returns>The color to draw the frametime line</returns>
        private static Color GetDebugGraphLineColor(int frametime)
        {
            Color c;
            if (frametime < 10) c = Color.Green;
            else if (frametime < 16) c = Color.Orange;
            else c = Color.Red;
            return c;
        }
    }
}
