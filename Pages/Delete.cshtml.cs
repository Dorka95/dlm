using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace logindlm.Pages
{
    public class Delete : PageModel
    {
        public string ErrorMessage { get; set; } = "";

        public void OnGet()
        {
        }
        public void OnPost(int id){
            deleteTermek(id);
            Response.Redirect("/termek");
        }

        private void deleteTermek(int id){
            try{
                string connectionString = "Server=desktop-jt66ekn\\mssqlserver01;Database=AspNetAuth;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //Termék törlése az adatbázisból

                    string sql = "DELETE FROM dlmdata WHERE id=@id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("A terméket nem lehet törölni: " + ex.Message);
            }
        }
    }
}