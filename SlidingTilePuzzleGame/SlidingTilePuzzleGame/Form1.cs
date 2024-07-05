using System.Text;
using System.Windows.Forms;

namespace SlidingTilePuzzleGame
{
    public partial class Form1 : Form
    {
        private Puzzle puzzle; // Instance of the Puzzle class for the game

        public Form1()
        {
            InitializeComponent();
            InitializeUI(); // Initialize user interface elements
            InitializeFormProperties(); // Configure form properties

            puzzle = new Puzzle(3); // Initialize puzzle with a 3x3 grid size
        }

        private void InitializeFormProperties()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void InitializeUI()
        {
            lblWelcome.Text = "To start with the game just click 'Choose Picture' in the menu above." +
                "\n\nAdditionally, if prefered, the grid size can also be changed from the menu." +
                "\n\n*Help is provided by displaying the current order of tiles, \nwith the correct order being a sequence [0 to (GridSize-1)]." +
                "\nExample (3x3 Grid):\nCurrent order: 5,2,0,8,1,4,3,6,7\nCorrect order: 0,1,2,3,4,5,6,7,8";
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            lblWelcome.Location = new Point((this.ClientSize.Width - lblWelcome.Width) / 2, (this.ClientSize.Height - lblWelcome.Height) / 2);

            lblHelp.Visible = false;
        }

        // Event handler for 'Choose Picture' menu item click
        private void choosePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (puzzle.PictureBoxes.Count > 0)
            {
                ClearPictureBoxes(); // Clear existing PictureBoxes if any
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
            // Open file dialog to choose an image file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                lblWelcome.Visible = false; // Hide welcome label once image is chosen

                puzzle.LoadImage(openFileDialog.FileName); // Load the selected image
                puzzle.Create(); // Create the puzzle grid
                puzzle.Shuffle(); // Shuffle the puzzle tiles
                puzzle.EnsureSolvable(); // Ensure the puzzle is solvable

                UpdatePuzzleLayout(); // Update the UI with puzzle tiles
                AdjustFormSizeToFitPuzzle(); // Adjust form size based on puzzle size
            }
        }

        // Adjusts the form size to fit the puzzle grid
        private void AdjustFormSizeToFitPuzzle()
        {
            int puzzleWidth = puzzle.PuzzleSize * puzzle.PictureBoxes[0].Width + (puzzle.PuzzleSize - 1) * 5; // Adjust for gaps between tiles
            int puzzleHeight = puzzle.PuzzleSize * puzzle.PictureBoxes[0].Height + (puzzle.PuzzleSize - 1) * 5;

            this.ClientSize = new Size(puzzleWidth + 60, puzzleHeight + 120); // Adjust margins as needed

            // Position help label at the bottom center of the form
            lblHelp.Location = new Point((this.ClientSize.Width - lblHelp.Width) / 2, this.ClientSize.Height - lblHelp.Height - 20); // Position at the bottom, adjust margin (20) as needed
        }

        // Updates the puzzle layout on the form
        private void UpdatePuzzleLayout()
        {

            int pieceWidth = puzzle.PictureBoxes[0].Width;
            int pieceHeight = puzzle.PictureBoxes[0].Height;
            int x = 30;
            int y = 50;

            // Position PictureBoxes on the form in a grid layout
            for (int i = 0; i < puzzle.PictureBoxes.Count; i++)
            {
                puzzle.PictureBoxes[i].BackColor = Color.Silver;
                puzzle.PictureBoxes[i].BorderStyle = BorderStyle.FixedSingle;
                puzzle.PictureBoxes[i].Location = new Point(x, y);

                puzzle.PictureBoxes[i].Click += PictureBox_Click; // Attach click event handler to each PictureBox

                x += pieceWidth + 5;

                // Move to the next row if the end of the current row is reached
                if ((i + 1) % puzzle.PuzzleSize == 0)
                {
                    x = 30;
                    y += pieceHeight + 5;
                }

                Controls.Add(puzzle.PictureBoxes[i]); // Add PictureBox to the form's controls
            }

            UpdateCurrentPositionLabel(); // Update the label displaying the current tile order
        }

        // Event handler for PictureBox click (tile movement)
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            PictureBox emptyPictureBox = puzzle.PictureBoxes.FirstOrDefault(pb => pb.Tag.ToString() == "0");

            // Check if the clicked tile can move to the empty space
            if (puzzle.CanMove(clickedPictureBox, emptyPictureBox))
            {
                puzzle.SwapPictureBoxes(clickedPictureBox, emptyPictureBox); // Swap clicked tile with empty space
                UpdateCurrentPositionLabel(); // Update the label displaying the current tile order
                CheckForWin(); // Check if the puzzle is solved
            }
        }

        // Checks if the puzzle is solved (tiles in correct order)
        private void CheckForWin()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < puzzle.PuzzleSize * puzzle.PuzzleSize; i++)
            {
                sb.Append(i.ToString());
            }

            // If current puzzle state matches expected order, puzzle is solved
            if (puzzle.CurrentPosition == sb.ToString())
            {
                MessageBox.Show("Congratulations! You solved the puzzle!", "Puzzle Solved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowOriginalImage(); // Show the original image once puzzle is solved
            }
        }

        // Shows the original image on the form
        private void ShowOriginalImage()
        {
            ClearPictureBoxes();

            PictureBox originalPictureBox = new PictureBox();
            originalPictureBox.Size = new Size(puzzle.MainBitmap.Width, puzzle.MainBitmap.Height);
            originalPictureBox.Image = puzzle.MainBitmap;
            originalPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            originalPictureBox.BorderStyle = BorderStyle.FixedSingle;

            // Center the original image on the form
            originalPictureBox.Location = new Point((ClientSize.Width - originalPictureBox.Width) / 2,
                                                    (ClientSize.Height - originalPictureBox.Height) / 2);

            puzzle.PictureBoxes.Add(originalPictureBox);
            Controls.Add(originalPictureBox);
        }

        // Updates the help label with the current order of tiles
        private void UpdateCurrentPositionLabel()
        {
            lblHelp.Text = string.Join(" ", puzzle.PictureBoxes.Select(pb => pb.Tag.ToString()));

        }

        // Clears all PictureBoxes from the form and resets puzzle state
        private void ClearPictureBoxes()
        {
            foreach (PictureBox pictureBox in puzzle.PictureBoxes)
            {
                Controls.Remove(pictureBox);
                pictureBox.Dispose();
            }

            puzzle.Clear();
        }

        // Event handler for 'Help' menu item click
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblHelp.Visible = !lblHelp.Visible; // Toggle visibility of help label

            if (lblHelp.Visible)
            {
                helpToolStripMenuItem.BackColor = Color.LightGray; // Change menu item background color when help is visible
            }
            else
            {
                helpToolStripMenuItem.BackColor = SystemColors.Control; // Restore default menu item background color
            }
        }

        // Event handler for '3x3' menu item click (change puzzle size)
        private void _3x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _3x3ToolStripMenuItem.Checked = true;
            _4x4ToolStripMenuItem.Checked = false;
            _5x5ToolStripMenuItem.Checked = false;
            ChangePuzzleSize(3);
        }

        // Event handler for '4x4' menu item click (change puzzle size)
        private void _4x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _3x3ToolStripMenuItem.Checked = false;
            _4x4ToolStripMenuItem.Checked = true;
            _5x5ToolStripMenuItem.Checked = false;
            ChangePuzzleSize(4);
        }

        // Event handler for '5x5' menu item click (change puzzle size)
        private void _5x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _3x3ToolStripMenuItem.Checked = false;
            _4x4ToolStripMenuItem.Checked = false;
            _5x5ToolStripMenuItem.Checked = true;
            ChangePuzzleSize(5);
        }

        // Changes the puzzle size and resets the puzzle
        private void ChangePuzzleSize(int newSize)
        {
            if (puzzle.PuzzleSize != newSize)
            {
                puzzle.PuzzleSize = newSize;
                ClearPictureBoxes();

                if (puzzle.MainBitmap != null)
                {
                    puzzle.Create();
                    puzzle.Shuffle();
                    puzzle.EnsureSolvable();

                    UpdatePuzzleLayout();
                    AdjustFormSizeToFitPuzzle();
                }
            }
        }

    }
}
