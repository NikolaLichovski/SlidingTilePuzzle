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

        // Loads and resizes the main puzzle image
        public void LoadImage(string fileName)
        {
            MainBitmap = new Bitmap(fileName);
            MainBitmap = ResizeBitmap(MainBitmap, 500, 500);
        }

        // Resizes a given bitmap to the specified max width and height
        public Bitmap ResizeBitmap(Bitmap originalBitmap, int maxWidth, int maxHeight)
        {
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(() => false);
            Image thumbnail = originalBitmap.GetThumbnailImage(maxWidth, maxHeight, callback, IntPtr.Zero);
            return new Bitmap(thumbnail);
        }

        // Creates the puzzle grid
        public void Create()
        {
            Clear();// Clears any existing PictureBoxes

            int pieceWidth = MainBitmap.Width / PuzzleSize;
            int pieceHeight = MainBitmap.Height / PuzzleSize;

            for (int i = 0; i < PuzzleSize * PuzzleSize; i++)
            {
                int row = i / PuzzleSize;
                int col = i % PuzzleSize;

                Bitmap pieceBitmap;

                if (i == 0) // The first tile (tag 0)
                {
                    pieceBitmap = new Bitmap(pieceWidth, pieceHeight);
                    Graphics g = Graphics.FromImage(pieceBitmap);
                    g.Clear(Color.Transparent);
                    g.Dispose();
                }
                else // Other tiles
                {
                    pieceBitmap = new Bitmap(pieceWidth, pieceHeight);
                    Graphics g = Graphics.FromImage(pieceBitmap);
                    g.DrawImage(MainBitmap, new Rectangle(0, 0, pieceWidth, pieceHeight), new Rectangle(col * pieceWidth, row * pieceHeight, pieceWidth, pieceHeight), GraphicsUnit.Pixel);
                    g.Dispose();
                }

                // Create and configure a PictureBox for each tile
                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(pieceWidth, pieceHeight);
                pictureBox.Image = pieceBitmap;
                pictureBox.Tag = i.ToString();

                pictureBoxes.Add(pictureBox);
            }
        }

        // Shuffles the puzzle tiles randomly
        public void Shuffle()
        {
            Random random = new Random();
            pictureBoxes = pictureBoxes.OrderBy(i => random.Next()).ToList();
        }

        // Ensures the puzzle is solvable by checking inversions and making adjustments if necessary
        public void EnsureSolvable()
        {
            int inversionCount = CountInversions();
            int blankRowFromBottom = EmptyTileRowFromBottom();

            if (PuzzleSize % 2 != 0) // // Odd-sized puzzle (3x3, 5x5)
            {
                if (inversionCount % 2 == 0)
                {
                    // Puzzle is solvable as is
                    return;
                }
                else
                    SwapPictureBoxes(pictureBoxes[PuzzleSize * PuzzleSize - 2], pictureBoxes[PuzzleSize * PuzzleSize - 1]);
            }
            else // Even-sized puzzle (4x4)
            {
                if (blankRowFromBottom % 2 == inversionCount % 2)
                {
                    UpdateCurrentPosition();
                    // Puzzle is solvable as is
                    return;
                }
                else
                {
                    // Attempt to adjust tiles to make the puzzle solvable
                    for (int i = 0; i < pictureBoxes.Count - 1; i++)
                    {
                        for (int j = i + 1; j < pictureBoxes.Count; j++)
                        {
                            SwapPictureBoxes(pictureBoxes[i], pictureBoxes[j]);

                            int newInversionCount = CountInversions();
                            int newEmptyRowFromBottom = EmptyTileRowFromBottom();

                            // Check if puzzle is solvable now
                            if ((PuzzleSize % 2 == 1 && newInversionCount % 2 == 0) || // Odd-sized puzzle
                                (PuzzleSize % 2 == 0 && newEmptyRowFromBottom % 2 == newInversionCount % 2)) // Even-sized puzzle
                            {
                                UpdateCurrentPosition();
                                return;
                            }

                            SwapPictureBoxes(pictureBoxes[i], pictureBoxes[j]);
                        }
                    }
                }
            }
        }

        // Calculates the row position of the empty tile from the bottom
        private int EmptyTileRowFromBottom()
        {
            PictureBox emptyPictureBox = pictureBoxes.FirstOrDefault(pb => pb.Tag != null && pb.Tag.ToString() == "0");
            if (emptyPictureBox == null)
            {
                return -1; // Empty tile not found
            }

            int emptyIndex = pictureBoxes.IndexOf(emptyPictureBox);
            int row = emptyIndex / PuzzleSize; // Row position from the top
            return PuzzleSize - row; // Row position from the bottom
        }

        // Counts the number of inversions in the current puzzle state
        private int CountInversions()
        {
            int inversionCount = 0;

            for (int i = 0; i < pictureBoxes.Count - 1; i++)
            {
                for (int j = i + 1; j < pictureBoxes.Count; j++)
                {
                    int valI = int.Parse(pictureBoxes[i].Tag.ToString());
                    int valJ = int.Parse(pictureBoxes[j].Tag.ToString());
                    if (valI != 0 && valJ != 0 && valI > valJ)
                    {
                        inversionCount++;
                    }
                }
            }

            return inversionCount;
        }

        // Checks if a tile can be moved to the empty space
        public bool CanMove(PictureBox clickedPictureBox, PictureBox emptyPictureBox)
        {
            int clickedIndex = pictureBoxes.IndexOf(clickedPictureBox);
            int emptyIndex = pictureBoxes.IndexOf(emptyPictureBox);

            int rowClicked = clickedIndex / PuzzleSize;
            int colClicked = clickedIndex % PuzzleSize;

            int rowEmpty = emptyIndex / PuzzleSize;
            int colEmpty = emptyIndex % PuzzleSize;

            // Check if clickedPictureBox is adjacent to emptyPictureBox
            return (Math.Abs(rowClicked - rowEmpty) == 1 && colClicked == colEmpty) ||
                   (Math.Abs(colClicked - colEmpty) == 1 && rowClicked == rowEmpty);
        }

        // Swaps positions of two PictureBoxes in the puzzle
        public void SwapPictureBoxes(PictureBox clickedPictureBox, PictureBox emptyPictureBox)
        {
            Point tempLocation = clickedPictureBox.Location;
            clickedPictureBox.Location = emptyPictureBox.Location;
            emptyPictureBox.Location = tempLocation;

            int clickedIndex = pictureBoxes.IndexOf(clickedPictureBox);
            int emptyIndex = pictureBoxes.IndexOf(emptyPictureBox);
            pictureBoxes[clickedIndex] = emptyPictureBox;
            pictureBoxes[emptyIndex] = clickedPictureBox;

            UpdateCurrentPosition();
        }

        // Updates the current puzzle state string representation
        private void UpdateCurrentPosition()
        {
            currentPosition = string.Join("", pictureBoxes.Select(pb => pb.Tag.ToString()));
        }

        // Clears PictureBoxes and resets puzzle state
        public void Clear()
        {
            pictureBoxes.Clear();
            puzzleImages.Clear();
            currentPosition = string.Empty;
        }
    }
}
