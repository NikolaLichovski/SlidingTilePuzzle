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

        }

        // Event handler for PictureBox click (tile movement)
        private void PictureBox_Click(object sender, EventArgs e)
        {
            
        }
             
        // Event handler for 'Help' menu item click
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        // Event handler for '3x3' menu item click (change puzzle size)
        private void _3x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        // Event handler for '4x4' menu item click (change puzzle size)
        private void _4x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        // Event handler for '5x5' menu item click (change puzzle size)
        private void _5x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

    }
}
