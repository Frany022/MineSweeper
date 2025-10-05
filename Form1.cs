using System.Text;

namespace minesweeper
{
    public partial class Form1 : Form
    {
        private MinesweeperGame minesweeperGame;
        private Button[,] buttons;
        public Form1()
        {
            InitializeComponent();
            DifficultySelector.Items.Add("Beginner");
            DifficultySelector.Items.Add("Intermediate");
            DifficultySelector.Items.Add("Expert");
            DifficultySelector.SelectedIndex = 0;
            Start.Click += Start_Click;
        }

        private Difficulty GetSelectedDifficulty() 
        {
            return DifficultySelector.SelectedItem.ToString() switch
            {
                "Beginner" => Difficulty.Beginner,
                "Intermediate" => Difficulty.Intermediate,
                "Expert" => Difficulty.Expert,
                _ => Difficulty.Beginner
            };
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Start_Click(object? sender, EventArgs e)
        {
            Difficulty difficulty = GetSelectedDifficulty();
            minesweeperGame = new MinesweeperGame();
            minesweeperGame.StartGame(difficulty);

            int rows = minesweeperGame.gamefield.Rows;
            int cols = minesweeperGame.gamefield.Columns;

            panel1.Controls.Clear();
            buttons = new Button[rows, cols];
            panel1.Width = cols * 30 + 20;
            panel1.Height = rows * 30 + 20;

            for (int r = 0; r < rows; r++) 
            {
                for (int c = 0; c < cols; c++) 
                {
                    Button btn = new Button();
                    btn.Width = 30;
                    btn.Height = 30;
                    btn.Left = c * 30;
                    btn.Top = r * 30;
                    btn.Tag = (r, c);
                    btn.MouseDown += Cell_MouseDown;
                    panel1.Controls.Add(btn);
                    buttons[r, c] = btn;
                }
            }

        }

        private void Cell_MouseDown(object? sender, MouseEventArgs e) 
        {
            if (minesweeperGame == null || minesweeperGame.gamestate != GameState.Running) return;

            Button btn = sender as Button;
            if (btn == null || btn.Tag is not ValueTuple<int, int> coords) return;

            var (row, col) = coords;

            if (e.Button == MouseButtons.Left)
            {
                minesweeperGame.RevealCell(row, col);
            }
            else if (e.Button == MouseButtons.Right) 
            {
                minesweeperGame.ToggleFlag(row, col);
            }

            Update_UI();

            if (minesweeperGame.gamestate == GameState.Won)
            {
                MessageBox.Show("You won");
                DisableAllButtons();
            }
            else if (minesweeperGame.gamestate == GameState.Lost) 
            {
                MessageBox.Show("You lost");
                RevealAllMines();
                DisableAllButtons();
            }
        }

        private void Update_UI() 
        {
            for (int r = 0; r < minesweeperGame.gamefield.Rows; r++) 
            {
                for (int c = 0; c < minesweeperGame.gamefield.Columns; c++) 
                {
                    var cell = minesweeperGame.gamefield.cells[r, c];
                    var btn = buttons[r,c];

                    if (cell.IsRevealed)
                    {
                        btn.Enabled = false;
                        if (cell.IsMine)
                        {
                            btn.Text = "*";
                            btn.BackColor = Color.Red;
                        }
                        else
                        {
                            btn.Text = cell.NeighboorMines > 0 ? cell.NeighboorMines.ToString() : "";
                            btn.BackColor = Color.LightGray;
                        }
                    }
                    else 
                    {
                        btn.Enabled = true;
                        btn.Text = cell.IsFlagged ? "F" : "";
                        btn.BackColor = DefaultBackColor;
                    }

                }
            }
        }

        private void RevealAllMines() 
        {
            for (int r = 0; r < minesweeperGame.gamefield.Rows; r++) 
            {
                for (int c = 0; c < minesweeperGame.gamefield.Columns; c++) 
                {
                    var cell = minesweeperGame.gamefield.cells[r, c];
                    var btn = buttons[r,c];

                    if (cell.IsMine) 
                    {
                        btn.Text = "*";
                        btn.BackColor = Color.Gray;
                    }
                }
            }
        }

        private void DisableAllButtons() 
        {
            foreach (var btn in buttons) 
            {
                btn.Enabled = false;
            }
        }

        private void Difficulty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
