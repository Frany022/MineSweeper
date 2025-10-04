using System;
namespace minesweeper
{
	public class Cell
	{
		public bool IsMine { get; set; }
		public bool IsRevealed { get; set; }
		public bool IsFlagged { get; set; }
		public int NeighboorMines { get; set; }

		public Cell()
		{
			IsMine = false;
			IsRevealed = false;
			IsFlagged = false;
			NeighboorMines = 0;
		}
	}
}