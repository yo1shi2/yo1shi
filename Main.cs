


Вывод информации в дата гриф из бд

using Npgsql;
using System.Data;

namespace WinFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = null;
            DataTable dataTable = new DataTable();
            try
            {
                conn = dbconnection.GetConnection();
                if (conn != null)
                {
                    string sqlQuery = "SELECT * FROM Products;";

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sqlQuery, conn))
                    {
                        adapter.Fill(dataTable);
                    }
                    dgvData.DataSource = dataTable;
                }
            }
            catch (NpgsqlException ex)
            {

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}




класс дб конектион



    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace WinFormsApp5
{
    public static class dbconnection
    {
        private static string ConnectionString = "Host=localhost;Port=5432;Database=2;Username=postgres;Password=1234321";

        public static NpgsqlConnection GetConnection()
        {
            NpgsqlConnection conn = null;

            try
            {
                conn = new NpgsqlConnection(ConnectionString);
                conn.Open();
                Console.WriteLine();
                return conn;
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Ошибка подключения к PostgreSQL: {ex.Message}");
                return null;
            }
        }

    }
}

