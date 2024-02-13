using System;
using System.Windows.Forms;

/*
 *		Extra 8-1 Display a test scores array
 *		-------------------------------------
 *
 *		In this exercise, you'll enhance the Score 
 *		Calculator form so it saves the scores the 
 *		user enters in an array and then lets the 
 *		user display the sorted scores in a dialog.
 *
 *		1. 	Open the ScoreCalculator project in the
 *			ExtraStarts\Ch08\ScoreCalculatorArray
 *			directory.
 *
 *		2. 	Declare a class variable for an array 
 *			that can hold 0 - 10 scores, and delete
 *			the class variable for the score total.
 *
 *		3.	Modify the Click event handler for the
 *			Add button so the code is within the 
 *			try block of a try-catch statement whose
 *			catch block catches any exception.
 *
 *			Then, modify the code so it adds the 
 *			score that's entered by the user to the
 *			next element in the array.
 *
 *			To do that, you can use the score count
 *			variable to refer to the element.
 *
 *			Finally, convert the code that refers to
 *			the deleted total class variable to a
 *			local total variable and use a foreach
 *			loop to add each score in the array to
 *			this variable.
 *
 *		4. 	Move the Clear button as shown. Then,
 *			modify the Click event handler for this 
 *			button so it removes any scores that have
 *			been added to the array. The easiest way
 *			to do that is to create a new array and
 *			assign it to the array variable.
 *
 *			Also, remove the reference to the deleted
 *			total class variable.
 *
 *		5.	Add a Display Scores button that sorts
 *			the scores in the array, displays the
 *			scores in a dialog, and moves the focus
 *			to the Score text box.
 *
 *			Be sure that only the elements that
 *			contain scores are displayed.
 *
 *		6. 	Test the app to be sure it works 
 *			correctly. In particular, see what 
 *			happens if you enter more than 10
 *			scores.
 */

namespace EE8_1
{
    public partial class Form1 : Form
    {
        //  Declare and initialize program constants
        const int SIZE     =  10;
        const int MINSCORE =   0;
        const int MAXSCORE = 100;

        //  Declare and initialize class variables.
        int[] testScores            = new int[SIZE];
        int numberOfElements        = 0;
        int testScoreCount          = 0;
        int testScoreTotal          = 0;
        decimal testScoreAverage    = 0.00m;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDisplayScores_Click(object sender, EventArgs e)
        {
            DisplaySortedTestScores();
        }

        private void DisplaySortedTestScores()
        {
            int[] copyScores = new int[SIZE];
            string outputStr = "";

            //  Copy the entire testScores array
            //  to the new copyScores array.
            Array.Copy(testScores, 0, copyScores, 0, testScores.Length);

            Array.Sort(copyScores);

            outputStr = "Sorted Scores";

            foreach (int score in copyScores)
            {
                outputStr += score + "\n";
            }

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

                if (numberOfElements >= testScores.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                testScores[numberOfElements] = score;
                ShowMessage("A GRADE OF " + score.ToString() +
                            " Has Been Added To The TestScores Array",
                            "GRADE SUCCESSFULLY ADDED TO ARRAY");
                numberOfElements++;

                testScoreCount = numberOfElements;
                testScoreTotal += score;

                txtScoreCount.Text = testScoreCount.ToString();
                txtScoreTotal.Text = testScoreTotal.ToString();
                txtAverage.Text = ((decimal)testScoreTotal / testScoreCount).ToString("n2");

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
            catch (IndexOutOfRangeException ioore)
            {
                ShowMessage("System Message:\t" + ioore.Message + "\n\n" +
                            "Array Already Full",
                            "INDEXOUTOFRANGEEXCEPTION");
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
