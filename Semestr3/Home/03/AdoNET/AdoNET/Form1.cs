using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AdoNET
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string Database { set; get; } = "master";
        string ConnectionString 
        {
            get
            {
                if (panel1.Enabled)
                {
                    return $"Server={textBox3.Text};Database={Database};User Id={textBox1.Text}; Password={textBox2.Text}";
                }
                else
                {
                    return $"Server={textBox3.Text};Database={Database};Trusted_Connection=True";
                }
            } 
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                panel1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Database = "master";
            comboBox2.DataSource = null;

            var shema = GetShema("Databases", 0);
            if (shema != null)
            {
                comboBox2.DataSource = shema;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database = comboBox2.Text;

            comboBox3.DataSource = null;
            var shema = GetShema("Tables", 2);
            if (shema != null)
            {
                comboBox3.DataSource = shema;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(comboBox3.Text))
                { return; }

                string sqlExpression = $"SELECT * FROM {comboBox3.SelectedValue}";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        List<string> GetShema(string colectionName, int index)
        {
            List<string> result = new List<string>();
            try
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    DataTable databases = sqlConnection.GetSchema(colectionName);

                    if (databases != null)
                    {
                        foreach (DataRow row in databases.Rows)
                        {
                            result.Add(row.ItemArray[index].ToString());
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
