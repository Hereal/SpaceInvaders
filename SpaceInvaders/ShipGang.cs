using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{


    /// <summary>
    /// Class for managing the group of ships. 
    /// </summary>
    class ShipGang
    {
        private readonly List<Ship> setShip;
        private int nbCollumn = 10;
        private bool moveRight = true;

        #region Constructor
        /// <summary>
        /// ShipGang
        /// </summary>
        /// <param name="nbRows">Number of row desired</param>
        /// <param name="gameInstance"> The instance for the game </param>
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

        #region methods
        /// <summary>
        /// Get the image corresponding to the line of the ship
        /// </summary>
        /// <param name="nbRow"> The desired line </param>
        /// <returns>
        /// The method returns an image.
        /// </returns>
        private Bitmap getImage(int nbRow)
        {
            if (nbRow % 3 == 0) return SpaceInvaders.Properties.Resources.tieInterceptor;
            if (nbRow % 3 == 1) return SpaceInvaders.Properties.Resources.tieFighter;
            return SpaceInvaders.Properties.Resources.tieBomber;
        }

        /// <summary>
        /// Function called on each update
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        public void Update(Game gameInstance, double deltaT)
        {
            RemoveDead();
            if (setShip.Count > 0) ShipGangAction(gameInstance, deltaT);
        }

        /// <summary>
        /// Carries out all actions relating to the ships
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        public void ShipGangAction(Game gameInstance, double deltaT)
        {
            int min = (int)setShip[0].vector.x, max = (int)setShip[0].vector.x + (int)setShip[0].image.Width;
            for (int i = 1; i < setShip.Count; i++)
            {
                int xmin = (int)setShip[i].vector.x, xmax = (int)setShip[i].vector.x + (int)setShip[i].image.Width;
                if (xmin < min) min = xmin;
                if (xmax > max) max = xmax;
            }
            MoveLeft(gameInstance, max);
            MoveRight(min);
            Move(gameInstance, deltaT);
        }



        /// <summary>
        /// Set the direction to the right
        /// </summary>
        /// <param name="min"> The minimal x value of all the ships </param>
        private void MoveRight(int min)
        {
            if (min < 0)
            {
                moveRight = true;
                ChangeDirection();
            }
        }

        /// <summary>
        /// Set the direction to the left
        /// </summary>
        /// <param name="gameInstance">The instance for the game</param>
        /// <param name="max">The maximal x value of all the ships</param>
        private void MoveLeft(Game gameInstance, int max)
        {
            if (max > gameInstance.gameSize.Width)
            {
                moveRight = false;
                ChangeDirection();
            }
        }


        /// <summary>
        /// Move all the ships
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        private void Move(Game gameInstance, double deltaT)
        {
            foreach (Ship s in setShip)
            {
                if (moveRight) s.MoveRight(gameInstance, deltaT);
                else s.MoveLeft(gameInstance, deltaT);
            }
        }



        /// <summary>
        /// Remove all dead ships from the list of ships
        /// </summary>
        private void RemoveDead()
        {
            for (int i = 0; i < setShip.Count; i++)
            {
                if (!setShip[i].IsAlive())
                    setShip.RemoveAt(i);

            }
        }

        /// <summary>
        /// Change the direction of all the ships
        /// </summary>
        private void ChangeDirection()
        {
            for (int i = 0; i < setShip.Count; i++)
            {
                setShip[i].Accelerate();
                setShip[i].MoveDown();

            }
        }



        public bool IsAlive()
        {
            return setShip.Count == 0;
        }
        #endregion
    }
}
