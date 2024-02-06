using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using statesClassLibrary;


namespace Alex_Carter_CPT_206_States_HomeWork
{
    public partial class Form1 : Form
    {
        private DatabaseManager databaseManager;
        private DataTable statesDataTable;
        private List<State> allStates;

        public Form1()
        {
            InitializeComponent();
            InitializeDataDirectory();
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\States.accdb;";
            databaseManager = new DatabaseManager(connectionString);
        }

        private void InitializeDataDirectory()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            try
            {
                allStates = new List<State>();
                statesDataTable = databaseManager.GetAllStatesDataTable();

                foreach (DataRow row in statesDataTable.Rows)
                {
                    string stateName = row["StateName"].ToString();
                    statesComboBox.Items.Add(stateName);

                    State state = new State { StateName = stateName };
                    allStates.Add(state);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading ComboBox data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                DataTable statesDataTable = databaseManager.GetAllStatesDataTable();
                statesBindingSource.DataSource = statesDataTable;
                statesBindingNavigator.BindingSource = statesBindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveData()
        {
            try
            {
                this.Validate();
                this.statesBindingSource.EndEdit();

                using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                {
                    adapter.Update(statesDataTable);
                }

                MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void statesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void statesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (statesComboBox.SelectedIndex != -1)
            {
                State selectedState = allStates[statesComboBox.SelectedIndex];
                Form2 form2 = new Form2(selectedState.StateName, statesDataTable, databaseManager);
                form2.Show();
            }
        }

        private void statesBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            SaveData();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
