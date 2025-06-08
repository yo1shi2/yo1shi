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
