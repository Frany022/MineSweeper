using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public class GameField
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Mines { get; set; }
        private Cell[,] cells { get; set; }
        private Random random = new Random();

        public GameField(int rows, int columns, int mines)
        {
            Rows = rows;
            Columns = columns;
            Mines = mines;
            cells = new Cell[Rows, Columns];
            InitializeCells();
            PlaceMines();
            AdjacentMines();

        }
        private void InitializeCells() 
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    cells[i, j] = new Cell();
                }
            }
        }

        private void PlaceMines() 
        {
            List<(int row, int col)> AllPositions = new List<(int row, int col)> ();
            for (int i = 0; i < Rows; i++) 
            {
                for (int j = 0; j < Columns; j++) 
                {
                    AllPositions.Add((i, j));
                }
            }

            Shuffle(AllPositions);

            for (int k = 0; k < Mines; k++) 
            {
                var (col, row) = AllPositions[k];
                cells[row, col].IsMine = true;
            }

        }

        private void Shuffle<T>(IList<T> list) 
        {
            int n = list.Count;
            for (int i = n - 1; i > 0; i--) 
            {
                int j = random.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        private void AdjacentMines() 
        {
            for (int i = 0; i < Rows; i++) 
            {

            }
        }
    }
}
