using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public static class GameSetting 
    {
        public static (int row, int cols, int mines) GetSettingsForDifficulty(Difficulty Level) 
        {
            return Level switch
            {
                Difficulty.Beginner => (9, 9, 10),
                Difficulty.Intermadiate => (16, 16, 40),
                Difficulty.Expert => (16, 30, 99),
                _ => (9, 9, 10)
            };
        }
    }
}
