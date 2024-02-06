using System.Diagnostics.Contracts;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace statesClassLibrary
{
    public class State
    {
        public string StateName { get; set; }
        public int Population { get; set; }
        public string StateFlagDescription { get; set; }
        public string StateFlower { get; set; }
        public string StateBird { get; set; }
        public string StateColors { get; set; }
        public string ThreeLargestCities { get; set; }
        public string StateCapitol { get; set; }
        public decimal MedianIncome { get; set; }
        public decimal ComputerRelatedJobs { get; set; }

        public override string ToString()
        {
            return StateName;
        }
    }
    public class DatabaseManager
    {

        private readonly string connectionString;

        public DatabaseManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable GetAllStatesDataTable()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM States";
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error fetching data from the database.", ex);
            }

            return dataTable;
        }

        public void UpdateStates(DataTable dataTable)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM States";
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                    {
                        using (OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter))
                        {
                            adapter.UpdateCommand = builder.GetUpdateCommand();
                            adapter.InsertCommand = builder.GetInsertCommand();
                            adapter.DeleteCommand = builder.GetDeleteCommand();

                            adapter.Update(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error updating data in the database.", ex);
            }
        }
        public DataTable GetStateDetails(string stateName)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM States WHERE StateName = @stateName";
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@stateName", stateName);
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving state details: {ex.Message}");
            }

            return dataTable;
        }

    }
}

    
