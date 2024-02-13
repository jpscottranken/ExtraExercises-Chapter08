using System;
using System.Collections.Generic;
using System.Windows.Forms;

/*
 *		Extra 8-2 Display a test scores list
 *		------------------------------------
 *
 *		In this exercise, you'll modify the
 *		Score Calculator form so it saves the
 *		scores the user enters in a list and
 *		then lets the user display the sorted
 *		scores in a dialog.
 *
 *		1. 	Open the ScoreCalculator project in
 *			the ExtraStarts\Ch08\ScoreCalculatorList
 *			directory.
 *
 *		2.	Replace the declaration for the array
 *			variable with a declaration for a 
 *			List<int> object, and delete the class
 *			variable for the score count.
 *
 *		3.	Modify the Click event handler for the
 *			Add button so it adds the score that's
 *			entered by the user to the list.
 *
 *			In addition, delete the statement that
 *			increments the score count variable you
 *			deleted.
 *
 *		4.	Modify the Click event handler for the
 *			Clear button so it removes any scores
 *			that have been added to the list.
 *
 *		5.	Modify the Click event handler for the
 *			Display Scores button so it sorts the
 *			scores in the list and then displays
 *			them in a dialog.
 *
 *		6.	Test the app to be sure it works 
 *			correctly. In particular, see what 
 *			happens if you enter more than 10 
 *			scores.
 */

namespace EE8_2
{
    public partial class Form1 : Form
    {
        //  Declare and initialize program constants
        const int SIZE = 10;
        const int MINSCORE = 0;
        const int MAXSCORE = 100;

        //  Declare and initialize class variables.
        List<int> testScores = new List<int>(10);
        int numberOfElements = 0;
        int testScoreCount = 0;
        int testScoreTotal = 0;
        decimal testScoreAverage = 0.00m;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDisplayScores_Click(object sender, EventArgs e)
        {
            SortTestScoresList();
        }

        private void SortTestScoresList()
        {
            string outputStr = "";

            outputStr = "Sorted Scores";

            foreach (int score in testScores)
            {
                outputStr += score + "\n";
            }

            testScores.Sort();
            ShowMessage(outputStr + "\r\n", "SORTED TEST ARRAY");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AttemptToAddTestScore();
        }

        private void AttemptToAddTestScore()
        {
            int score = 0;

            try
            {
                score = Convert.ToInt32(txtScore.Text.Trim());

                if (score < MINSCORE || score > MAXSCORE)
                {
                    throw new ArgumentOutOfRangeException();
                }

                testScores.Add(score);
                ShowMessage("A GRADE OF " + score.ToString() +
                            " Has Been Added To The TestScores Array",
                            "GRADE SUCCESSFULLY ADDED TO ARRAY");
                numberOfElements++;

                testScoreCount = numberOfElements;
                testScoreTotal += score;

                txtScoreCount.Text = testScoreCount.ToString();
                txtScoreTotal.Text = testScoreTotal.ToString();
                testScoreAverage   = ((decimal)testScoreTotal / testScoreCount);
                txtAverage.Text    = testScoreAverage.ToString("n2");

                ClearForm();
            }
            catch (FormatException fe)
            {
                ShowMessage("System Message:\t" + fe.Message + "\n\n" +
                            "Input Non-Numeric",
                            "FORMATEXCEPTION");
                ClearForm();
            }
            catch (ArgumentOutOfRangeException aoore)
            {
                ShowMessage("System Message:\t" + aoore.Message + "\n\n" +
                            "Test Score Inputted Out-Of-Range",
                            "ARGUMENTOUTOFRANGEEXCEPTION");
                ClearForm();
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtScore.Text = "";
            txtScore.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitProgramOrNot();
        }

        private void ExitProgramOrNot()
        {
            DialogResult dialog = MessageBox.Show(
                        "Do You Really Want To Exit The Program?",
                        "EXIT NOW?",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void ShowMessage(string msg, string title)
        {
            MessageBox.Show(msg, title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }
    }
}
