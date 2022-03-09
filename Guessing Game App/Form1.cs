using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guessing_Game_App
{
    public partial class Form1 : Form
    {
        
        int com_number; //Store the computers number in the form.
        
        Random generator; //Create a generator object for selecting random numbers.

        bool ongoing_game; //Store whether the user is currently playing the game.
                           //used to determine whether the game should be reset when
                           //clicking the button.

        int guessed_num;   //Store the previous guess the user made.

        double previous_distance; //Store the previous difference between the user's previous number.
                                  //and the computers chosen number.

        public Form1()
        {
            InitializeComponent();

            //Create the random generator object
            generator = new Random();

            //Reset the board on initialization to clear debug messages and to setup the game.
            //Also used when the user starts another game.
            restart();
        }

        private void restart()
        {
            //Setup the board by clearing it and choosing a new random number.
            ongoing_game = true;
            com_number = generator.Next(0, 1000);
            lblMessage.Text = "";
            lblLog.Text = "";
            txtNumber.Text = "";
            btnGuess.Text = "Guess";

            //The starting distance is 1000 so that the users first number is 
            //always "warm".
            previous_distance = 1000;
        }

        private void btnGuess_Click(object sender, EventArgs e)
        {
            //If a game is in session when this button is clicked, play the game.
            if (ongoing_game == true)
            {
                int previous_guess = guessed_num;
                try
                {                   
                    //Attempt to convert the user's number to a number.
                    guessed_num = Convert.ToInt32(txtNumber.Text);
                }
                catch (FormatException error)
                {
                    //If it cannot be converted, the catch clause will trigger.   Set the error message
                    //in the message box and immediately stop this functions execution.
                    lblMessage.Text = "Error: This is not a valid number.  Enter another.";
                    return;
                }

                //Add the user's number to the history log.
                lblLog.Text = txtNumber.Text + "\n" + lblLog.Text;

                //If the numbers match, set the textbox color to green and congradulate the user.
                //Also, set the button text to "Restart" to indicate that pressing the button
                //again will cause the game to restart.
                if(guessed_num == com_number)
                {
                    txtNumber.BackColor = Color.Green;
                    lblMessage.Text = "You got it correct!";
                    btnGuess.Text = "Restart Game";
                    ongoing_game = false;

                }

                //Otherwise, determine if the number is greater than/less than the computers number.
                else if(guessed_num > com_number)
                {
                    //Determine the distance between the new number and the computers number.
                    double new_distance = Math.Abs(com_number - guessed_num);

                    //If the distance is shorter than the previous distance, then set the color
                    //to red, indicating the user is getting "warmer" (i.e., closer to the
                    //computer's chosen number).
                    if (new_distance < previous_distance)
                        txtNumber.BackColor = Color.Red;
                    //If the distance is greater than the previous distance, then set the color
                    //to blue, indicating the user is getting "colder" (i.e., farther from the
                    //computer's chosen number).
                    else if (new_distance > previous_distance)
                        txtNumber.BackColor = Color.Blue;

                    //If there is no change in distance, set the textbox's color to black.
                    else
                        txtNumber.BackColor = Color.Black;

                    //Set the previous distance to the current distance for the next number.
                    previous_distance = new_distance;
                    
                    //Indicate that the user's next number should be lower to help better guess
                    //the new number.
                    lblMessage.Text = "Incorrect.  Your next guess should be a lower number.";

                }
                else
                {
                    //Apply the same logic as before with distance.
                    double new_distance = Math.Abs(com_number - guessed_num);

                    if (new_distance < previous_distance)
                        txtNumber.BackColor = Color.Red;
                    else if (new_distance > previous_distance)
                        txtNumber.BackColor = Color.Blue;
                    else
                        txtNumber.BackColor = Color.Black;

                    previous_distance = new_distance;

                    lblMessage.Text = "Incorrect.  Your next guess should be a higher number.";
                }
            }
            //If the game is not in session, restart the game when this button is pressed.
            else
            {
                restart();
            }


        }
    }
}
