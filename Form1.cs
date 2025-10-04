using System.Text;

namespace minesweeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameField field = new GameField(9, 9, 10);
            string boardstr = GetBoardString(field);
            MessageBox.Show(boardstr, "minefield");

            int mineCount = field.CountMines();
            MessageBox.Show($"Total mines placed: {mineCount}");
        }

        private string GetBoardString(GameField field) 
        {
            var sb = new StringBuilder();

            for (int i = 0; i < field.Rows; i++)
            {
                for (int j = 0; j < field.Columns; j++)
                {
                    sb.Append(field.cells[i, j].IsMine ? "*" : ".");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
