using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProcessNorthwindDB_RonPruitt
{
    internal class Program
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["NorthWind"].ConnectionString;

        private static void Main(string[] args)
        {
            Console.WriteLine("Connection Opened");
            //Display Original Data Rows
            Console.WriteLine("\n");
            Console.WriteLine("Display Rows Before Insertion:");
            SelectRows();
            Console.WriteLine("\n");
            Console.WriteLine("Insert Row operation:***");
            InsertRows();
            //Display Rows Before Insertion
            Console.WriteLine("\n");
            Console.WriteLine("Display Rows After Insertion:");
            SelectRows();
            //Update Rows
            Console.WriteLine("\n");
            Console.WriteLine("Perform Update***");
            UpdateRows();
            //Display Rows Before Insertion
            Console.WriteLine("\n");
            Console.WriteLine("Display Rows After Update:");
            SelectRows();
            //Clean up with delete of all inserted rows
            Console.WriteLine("\n");
            Console.WriteLine("Clean Up By Deleting Inserted Rows***");
            DeleteRows();
            //Display Rows After Cleanup
            Console.WriteLine("\n");
            Console.WriteLine("Display Rows After Cleanup:");
            SelectRows();
            Console.WriteLine("Connection Closed");
            Console.WriteLine("\nPress <ENTER> to quit...");
            Console.ReadKey();
        }

        private static void DeleteRows()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //Create Command objects
                    SqlCommand scalarCommand = new SqlCommand("SELECT COUNT(*) FROM Employees", connection);
                    scalarCommand.Connection.Open();
                    // Execute Scalar Query
                    Console.WriteLine("Before Delete, Number of Employees = {0}", scalarCommand.ExecuteScalar());
                    // Set up and execute DELETE Command
                    //Create Command object
                    SqlCommand nonqueryCommand = connection.CreateCommand();
                    nonqueryCommand.CommandText = "DELETE FROM Employees WHERE " +
                        "FirstName ='Annabelle' AND LastName ='Jahlberg' OR " +
                        "FirstName ='Carey' AND LastName='Saxon' OR " +
                        "FirstName ='Jose' AND LastName='Gonzales'";
                    Console.WriteLine("Executing {0}", nonqueryCommand.CommandText);
                    Console.WriteLine("Number of rows affected : {0}", nonqueryCommand.ExecuteNonQuery());
                    // Execute Scalar Query
                    Console.WriteLine("After Delete, Number of Employee = {0}", scalarCommand.ExecuteScalar());
                }
                catch (SqlException ex)
                {
                    // Display error
                    Console.WriteLine("Error: " + ex.ToString());
                }
            }
        }

        private static void InsertRows()
        {
            //Insert Rows processing
            //Create Command object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand nonqueryCommand = connection.CreateCommand();
                nonqueryCommand.Connection.Open();

                try
                {
                    // Create INSERT statement with named parameters
                    nonqueryCommand.CommandText = "INSERT INTO Employees (FirstName, LastName) " +
                    "VALUES (@FirstName, @LastName)";
                    // Add Parameters to Command Parameters collection
                    nonqueryCommand.Parameters.Add("@FirstName", SqlDbType.VarChar, 10);
                    nonqueryCommand.Parameters.Add("@LastName", SqlDbType.VarChar, 20); // Prepare command for repeated execution
                    nonqueryCommand.Prepare();
                    // Data to be inserted

                    string[] firstNames = { "Maxine", "Carey", "Jose" };
                    string[] lastNames = { "Jahlberg", "Saxon", "Gonzales" };
                    for (int i = 0; i <= 2; i++)
                    {
                        nonqueryCommand.Parameters["@FirstName"].Value = firstNames[i];
                        nonqueryCommand.Parameters["@LastName"].Value = lastNames[i];
                        Console.WriteLine("Executing {0}", nonqueryCommand.CommandText);
                        Console.WriteLine("Number of rows affected : {0}", nonqueryCommand.ExecuteNonQuery());
                    }
                }
                catch (SqlException ex)
                {
                    // Display error
                    Console.WriteLine("Error: " + ex.ToString());
                }
                finally
                {
                    // Not used now but you might want some clean up processing in future work
                }
            }
        }

        private static void SelectRows()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Sql Select Query
                    string sql = "SELECT * FROM Employees";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Connection.Open();
                    SqlDataReader dr; dr =
                   cmd.ExecuteReader(); string
                   strEmployeeID = "EmployeeID"; string
                   strFirstName = "FirstName"; string
                   strLastName = "LastName";
                    Console.WriteLine("{0} | {1} | {2}", strEmployeeID.PadRight(10), strFirstName.PadRight(10), strLastName);
                    Console.WriteLine("==========================================");
                    while (dr.Read())
                    {
                        //reading from the datareader
                        Console.WriteLine("{0} | {1} | {2}",
                        dr["EmployeeID"].ToString().PadRight(10),
                        dr["FirstName"].ToString().PadRight(10),
                        dr["LastName"]);
                    }
                    dr.Close();
                    Console.WriteLine("==========================================");
                }
                catch (SqlException ex)
                {
                    // Display error
                    Console.WriteLine("Error: " + ex.ToString());
                }
            }
        }

        private static void UpdateRows()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Sql Update Statement
                    string updateSql = "UPDATE Employees " + "SET FirstName = @FirstName " + "WHERE LastName = @LastName";
                    SqlCommand UpdateCmd = new SqlCommand(updateSql, connection);
                    UpdateCmd.Connection.Open();
                    // 2. Map Parameters
                    UpdateCmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 10, "FirstName");
                    UpdateCmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 20, "LastName");
                    UpdateCmd.Parameters["@FirstName"].Value = "Annabelle";
                    UpdateCmd.Parameters["@LastName"].Value = "Jahlberg";
                    UpdateCmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    // Display error
                    Console.WriteLine("Error: " + ex.ToString());
                }
            }
        }
    }
}