using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class ShipGang
    {
        private List<Ship> setShip;
        private int nbCollumn = 10;
        private bool moveRight = true;

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public ShipGang(int nbRows, Game gameInstance)
        {
            setShip = new List<Ship>();

            for (int y = 0; y < nbRows; y++)
            {
                for (int x = 0; x < nbCollumn; x++)
                {
                    Ship s = new Ship(x * 60, y * 60 + 20, 10, getImage(y));
                    setShip.Add(s);
                    gameInstance.AddNewGameObject(s);
                }
            }


        }
        #endregion
        private Bitmap getImage(int nbRow)
        {
            if (nbRow % 3 == 0)
                return SpaceInvaders.Properties.Resources.tieInterceptor;
            if (nbRow % 3 == 1)
                return SpaceInvaders.Properties.Resources.tieFighter;
            return SpaceInvaders.Properties.Resources.tieBomber;
        }
        public void Update(Game gameInstance, double deltaT)
        {
            RemoveDead();
            if (setShip.Count > 0)
            {
                int min = (int)setShip[0].vector.x;
                int max = (int)setShip[0].vector.x + (int)setShip[0].image.Width;
                for (int i = 1; i < setShip.Count; i++)
                {
                    int xmin = (int)setShip[i].vector.x;
                    int xmax = (int)setShip[i].vector.x + (int)setShip[i].image.Width;
                    if (xmin < min)
                        min = xmin;
                    if (xmax > max)
                        max = xmax;

                }
                if (max > gameInstance.gameSize.Width)
                {
                    moveRight = false;
                    ChangeDirection();
                }
                if (min < 0)
                {
                    moveRight = true;
                    ChangeDirection();
                }
                foreach (Ship s in setShip)
                {
                    if (moveRight)
                        s.MoveRight(gameInstance, deltaT);
                    else
                        s.MoveLeft(gameInstance, deltaT);
                }

            }
        }
        private void RemoveDead()
        {
            for (int i = 0; i < setShip.Count; i++)
            {
                if (!setShip[i].IsAlive())
                    setShip.RemoveAt(i);

            }
        }
        private void ChangeDirection()
        {
            for (int i = 0; i < setShip.Count; i++)
            {
                setShip[i].accelerate();
                setShip[i].moveDown();

            }
        }
    }
}
