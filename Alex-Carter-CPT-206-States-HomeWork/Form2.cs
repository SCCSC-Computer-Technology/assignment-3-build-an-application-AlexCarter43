using statesClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Alex_Carter_CPT_206_States_HomeWork.StatesDataSet;



namespace Alex_Carter_CPT_206_States_HomeWork
{
    public partial class Form2 : Form
    {

        private string selectedState;
        private DataTable statesDataTable;
        private DatabaseManager databaseManager;

        public Form2(string selectedState, DataTable statesDataTable, DatabaseManager databaseManager)
        {
            InitializeComponent();
            this.selectedState = selectedState;
            this.statesDataTable = statesDataTable;
            this.databaseManager = databaseManager;
            ShowStateDetails(selectedState);
        }

        private void ShowStateDetails(string selectedState)
        {
            try
            {
                DataTable stateDetails = databaseManager.GetStateDetails(selectedState);

                if (stateDetails.Rows.Count > 0)
                {
                    DataRow stateRow = stateDetails.Rows[0];

                    nameLabel.Text = selectedState;
                    int population = Convert.ToInt32(stateRow["Population"]);
                    popLabel.Text = population.ToString("#,0");
                    desTextBox.Text = stateRow["StateFlagDescription"].ToString();
                    capitolLabel.Text = stateRow["StateCapitol"].ToString();
                    citiesLabel.Text = stateRow["ThreeLargestCities"].ToString();
                    flowerLabel.Text = stateRow["StateFlower"].ToString();
                    birdLabel.Text = stateRow["StateBird"].ToString();
                    colorLabel.Text = stateRow["StateColors"].ToString();

                    decimal income = Convert.ToDecimal(stateRow["MedianIncome"]);
                    incomeLabel.Text = income.ToString("C");

                    decimal jobPercentage = Convert.ToDecimal(stateRow["ComputerRelatedJobs"]);
                    jobLabel.Text = jobPercentage.ToString("") + "%";
                }
                else
                {
                    MessageBox.Show($"Details for state '{selectedState}' not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying state details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


}
