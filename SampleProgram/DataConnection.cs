using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace SampleProgram
{
    internal class DataConnection
    {
        MySqlConnection connection;
        string connectionString;
        public DataConnection()
        {
            string server = "localhost";
            string userid = "root";
            string password = "";
            string database = "dbsample";
            connectionString = String.Format("server={0};database={1};userid={2};password={3};",
                server,database,userid,password);
        }

        public void connect()
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public DataTable load(string source)
        {
            DataTable dt;
            connect();

            string commandText = String.Format("SELECT * FROM {0} WHERE deleted=0;", source);

            MySqlCommand cmd;
            cmd = new MySqlCommand(commandText, connection);

            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
            {
                dt = new DataTable();
                da.Fill(dt);
            }
            return dt;
        }

        
        public bool save(Employee employee)
        {
            connect();
            string query = "INSERT INTO employee(fullname,status,gender,address) " +
                        "VALUES(@fullname,@status,@gender,@address);";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@fullname", employee.FullName);
                cmd.Parameters.AddWithValue("@status", employee.Status);
                cmd.Parameters.AddWithValue("@gender", employee.Gender);
                cmd.Parameters.AddWithValue("@address", employee.Address);

                cmd.ExecuteNonQuery();
                return true;
            }

        }

        public bool update(Employee employee)
        {
            connect();
            string query = "UPDATE employee SET fullname=@fullname,status=@status," +
                        "gender=@gender,address=@address WHERE employee_id=@Id;";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@fullname", employee.FullName);
                cmd.Parameters.AddWithValue("@status", employee.Status);
                cmd.Parameters.AddWithValue("@gender", employee.Gender);
                cmd.Parameters.AddWithValue("@address", employee.Address);
                cmd.Parameters.AddWithValue("@Id", employee.Id);

                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public Employee loadEmployee(int Id)
        {
            DataTable dt;
            connect();

            string commandText = String.Format("SELECT * FROM employee WHERE employee_id={0};", Id);

            MySqlCommand cmd;
            cmd = new MySqlCommand(commandText, connection);

            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
            {
                dt = new DataTable();
                da.Fill(dt);
            }

            Employee employee = new Employee();
            foreach (DataRow row in dt.Rows)
            {
                employee.FullName = row["fullname"].ToString();
                employee.Status = row["status"].ToString();
                employee.Gender = row["gender"].ToString();
                employee.Address = row["address"].ToString();
            }

            return employee;
            
        }

        public bool delete(Employee employee)
        {
            connect();
            string query = "UPDATE employee SET deleted='1' WHERE employee_id=@Id;";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", employee.Id);

                cmd.ExecuteNonQuery();
                return true;
            }
        }


    }
}
