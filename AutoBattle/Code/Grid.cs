using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> grids = new List<GridBox>();
        public int xLength;
        public int yLength;
        public Grid(int Lines, int Columns)
        {
            xLength = Columns;
            yLength = Lines;
            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < Lines; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    GridBox newBox = new GridBox(j, i, false, (Columns * i + j));
                    Console.Write($"{newBox.Index}\n");
                    grids.Add(newBox);
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void drawBattlefield()
        {
            for (int i = 0; i < yLength; i++)
            {
                for (int j = 0; j < xLength; j++)
                {
                    GridBox currentgrid = grids[i*xLength + j];
                    if (currentgrid.ocupied)
                    {
                        //if()
                        Console.Write("[X]\t");
                    }
                    else
                    {
                        Console.Write($"[ ]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }

    }
}
