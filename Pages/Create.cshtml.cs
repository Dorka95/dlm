using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;

namespace logindlm.Pages
{
    public class Create : PageModel
    {
        [BindProperty, Required(ErrorMessage = "Cikkszám szükséges!")]
        public string Cikkszam { get; set; } = "";
        
        [BindProperty, Required(ErrorMessage = "Terméknév szükséges!")]
        public string Termeknev { get; set; } = "";
        
        [BindProperty]
        public string? Leiras { get; set; } = "";
        
        [BindProperty, Required(ErrorMessage = "Ár szükséges!")]
        public string Ar { get; set; } = "";

        public void OnGet()
        {
        }

        public string ErrorMessage { get; set; } = "";

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            if (Leiras == null) Leiras = "";

            // Új termék létrehozása
            try
            {
                string connectionString = "Server=desktop-jt66ekn\\mssqlserver01;Database=AspNetAuth;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO dlmdata (cikkszam, termeknev, leiras, ar) VALUES (@cikkszam, @termeknev, @leiras, @ar);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@cikkszam", Cikkszam);
                        command.Parameters.AddWithValue("@termeknev", Termeknev);
                        command.Parameters.AddWithValue("@leiras", Leiras);
                        command.Parameters.AddWithValue("@ar", Ar);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }

            Response.Redirect("/termek");
        }
    }
}
