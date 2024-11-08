using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace logindlm.Pages
{
    public class Edit : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        
        [BindProperty, Required(ErrorMessage = "Cikkszám szükséges!")]
        public string Cikkszam { get; set; } = "";
        
        [BindProperty, Required(ErrorMessage = "Terméknév szükséges!")]
        public string Termeknev { get; set; } = "";
        
        [BindProperty]
        public string? Leiras { get; set; } = "";
        
        [BindProperty, Required(ErrorMessage = "Ár szükséges!")]
        public string Ar { get; set; } = "";

        public string ErrorMessage { get; set; } = "";

        public void OnGet(int id)
        {
            try
            {
                string connectionString = "Server=desktop-jt66ekn\\mssqlserver01;Database=AspNetAuth;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM dlmdata WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Id = reader.GetInt32(0);
                                Cikkszam = reader.GetString(1);
                                Termeknev = reader.GetString(2); 
                                Leiras = reader.IsDBNull(3) ? "" : reader.GetString(3); // Null érték kezelés
                                Ar = reader.GetString(4);
                            }
                            else
                            {
                                Response.Redirect("/termek");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Hiba történt az adatbetöltés során: {ex.Message}";
            }
        }

        public void OnPost() {
            if (!ModelState.IsValid) {
                return;
            }

            if (Leiras == null) Leiras = "";

            //Termékinfók szerkesztése

            try
            {
                string connectionString = "Server=.;Database=AspNetAuth;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE dlmdata SET cikkszam=@cikkszam, termeknev=@termeknev, leiras=@leiras, ar=@ar WHERE id=@id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@cikkszam", Cikkszam);
                        command.Parameters.AddWithValue("@termeknev", Termeknev);
                        command.Parameters.AddWithValue("@leiras", Leiras);
                        command.Parameters.AddWithValue("@ar", Ar);
                        command.Parameters.AddWithValue("@id", Id);

                        command.ExecuteNonQuery();
                    }
                }

            }
            
            catch (Exception ex)
            {
                ErrorMessage = $"Hiba történt az adatok frissítése során: {ex.Message}";
                return;
            }

            Response.Redirect("/termek");
        }
    }
}