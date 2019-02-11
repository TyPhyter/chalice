using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chalice_Android.Entities
{
    public class GameBoard
    {
        public Grid GameGrid;
        
        public GameBoard()
        {
            GameGrid = new Grid();
        }
    }

    public class Grid
    {
        public Vector2 Position = new Vector2(235, 480);
        public List<Cell> Cells;

        public Grid()
        {
            Cells = new List<Cell>();

            for (var i = 0; i < 9; i++)
            {
                if (i < 3)
                {
                    Cells.Add(new Cell(new Rectangle { X = (int)Position.X + i * 325, Y = (int)Position.Y, Width = 308, Height = 420 }));
                }
                else if (i < 6)
                {
                    Cells.Add(new Cell(new Rectangle { X = (int)Position.X + ((i % 3) * 325), Y = (int)Position.Y + 395, Width = 308, Height = 420 }));
                }
                else if (i < 9)
                {
                    Cells.Add(new Cell(new Rectangle { X = (int)Position.X + ((i % 3) * 325), Y = (int)Position.Y + (2 * 395), Width = 308, Height = 420 }));
                }

            }
        }
    }

    public class Cell
    {
        public Rectangle Rectangle;
        public bool isOccupied;
        public Card Occupant;

        public Cell (Rectangle rect)
        {
            Rectangle = rect;
        }
    }
}