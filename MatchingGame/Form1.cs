using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();

        }

        // Reference variables representing the first and second labels that are clicked
        Label firstClicked = null;
        Label secondClicked = null;


        // Use this Random object ot choose random icons for the squares
        Random random = new Random();

        // Each of the icons is a letter in the Webdings font
        List<string> icons = new List<string>()
        {
            "@","@","N","N",",",",","j","j",
            "i","i","v","v","w","w","z","z"
        };

        // Assign each icon from the list of icons to a random square
        private void AssignIconsToSquares()
        {
            // The table has 16 labels
            // Icon list has 16 icons, an 
            // icon is pulled at ranbom from list and assigned to each label
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        // Method is called when any label is clicked
        private void label_Click(object sender, EventArgs e)
        {

            // The timer is only on after two non-matching 
            // icons have been selected and displayed
            // so ignore any clicked if the timer is running
            if (timer1.Enabled == true)
                return;


            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // If the clicked label is black, the player clicked
                // an icon that's already been revealed --
                // ignore the click
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // If firstClicked is null, this is the first icon
                // in the pair that is clicked, set the firstClicked to the label
                // that the player clicked, change its color to black, and return
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // If this point is reached, firstClicked != null and timer is not yet running
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // Getting this far means that the first and second label don't match
                timer1.Start();
            }
        }

        // Method is called after the timer duration has been reached
        private void timer1_Tick(object sender, EventArgs e)
        {
            // timer's Enabled property is false, thus it doesn't start until timer1.Start() is called
            // Stop the timer
            timer1.Stop();

            // Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            //Reset firstclicked and secondclicked
            firstClicked = null;
            secondClicked = null;
        }

        // Check to see if every icon has been matched
        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show($"You matched all the icons!", "Congratulations");
            Close();
        }

    }
}
