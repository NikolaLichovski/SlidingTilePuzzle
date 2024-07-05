using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingTilePuzzleGame
{
    public class Puzzle
    {
        public int PuzzleSize; // Size of the puzzle grid (e.g., 3 for 3x3)
        private List<PictureBox> pictureBoxes; // List to hold PictureBoxes for each tile
        private List<Bitmap> puzzleImages; // List to hold puzzle piece images (not currently used)
        private string currentPosition; // Current state of the puzzle as a string representation of tile order


        public Puzzle(int puzzleSize)
        {
            PuzzleSize = puzzleSize;
            pictureBoxes = new List<PictureBox>();
            puzzleImages = new List<Bitmap>();
            currentPosition = string.Empty;
        }

        public Bitmap MainBitmap { get; private set; } // Main image loaded for the puzzle
        public List<PictureBox> PictureBoxes => pictureBoxes; // Exposes the list of PictureBoxes
        public string CurrentPosition => currentPosition; // Exposes the current state of the puzzle

    }
}
