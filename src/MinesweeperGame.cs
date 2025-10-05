using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public class MinesweeperGame
    {
        public GameField gamefield {  get; private set; }
        public GameState gamestate { get; private set; }

        public void StartGame(Difficulty difficulty) 
        {
            var (cols, rows, mines) = GameSetting.GetSettingsForDifficulty(difficulty);
            gamefield = new GameField(cols, rows, mines);
            gamestate = GameState.Running;
        }

        public void RevealCell(int row, int col) 
        {
            if (gamestate != GameState.Running) return;
            var cell = gamefield.cells[row, col];
            if (cell.IsRevealed || cell.IsFlagged) return;

            cell.IsRevealed = true;

            if (cell.IsMine) 
            {
                gamestate = GameState.Lost;
                return;
            }
            if (cell.NeighboorMines == 0) 
            {
                for (int dr = -1; dr <= 1; dr++) 
                {
                    for (int dc = -1; dc <= 1; dc++) 
                    {
                        int nr = row + dr;
                        int nc = col + dc;
                        if (nr >= 0 && nr < gamefield.Rows && nc >= 0 && nc < gamefield.Columns) 
                        {
                            RevealCell(nr, nc);
                        }
                    }
                }
            }
            if (CheckWin()) 
            {
                gamestate = GameState.Won;
            }
        }

        private bool CheckWin() 
        {
            for (int r = 0; r < gamefield.Rows; r++) 
            {
                for (int c = 0; c < gamefield.Columns; c++) 
                {
                    var cell = gamefield.cells[r, c];
                    if (!cell.IsMine && !cell.IsRevealed) return false;
                }
            }
            return true;
        }

        public void ToggleFlag(int row, int col) 
        {
            if (gamestate != GameState.Running) return;

            var cell = gamefield.cells[row, col];
            if (cell.IsRevealed) return;
            cell.IsFlagged = !cell.IsFlagged;
        }
    }
}
