using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anketa
{
    class db_Client
    {
       public void PushToDB(User user)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=FWTest;Trusted_Connection=True";
                    cn.Open();

                    string result = "";
                    foreach (var i in user.HobyListCheck)
                        result += i + "|";

                    string query = $" insert into Users values ('{user.Name}','{user.Surname}','{user.BirthDate}','{user.Country}','{user.Sex}','{result}','{user.Other}')";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();

                    cn.Close();
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}

//create database FWTest

//string query = @"CREATE TABLE Users(
//Name nvarchar(100),
//Surname nvarchar(100),
//BirthDate Date,
//Country int,
//Sex nvarchar(50),
//HobyListCheck nvarchar(max),
//Other nvarchar(max)
//)"; 