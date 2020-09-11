using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    /// <summary>
    /// Dummy class for demonstration
    /// </summary>
    class ShipClan
    {
        #region Fields
        Ship[,] clan;
        int dx = 10;
        int dy = 10;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public ShipClan(int x, int y) : base()
        {
            this.clan = new Ship[x, y];
        }
        #endregion

        #region Methods

        public void InitClan(Game gameInstance)
        {
            int x = 0;
            for(int i = 0; i < clan.GetLength(0); i++)
            {
                int y = 0;
                for (int j = 0; j < clan.GetLength(1); j++)
                {
                    clan[i, j] = new Ship(x+dx, y+dy, j);
                    gameInstance.AddNewGameObject(clan[i, j]);
                    y += 36 + 10;
                }
                x += 32 + 10;
            }
        }

        

        

        #endregion
    }
}
