using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using logindlm.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace logindlm.Pages
{
    [Authorize]
    public class termekModel : PageModel
    {
        public List<TermekInfo> TermekLista { get; set; } = [];
        public void OnGet()
        {
            try
            {
                string connectionString = "Server=desktop-jt66ekn\\mssqlserver01;Database=AspNetAuth;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM dlmdata ORDER BY id DESC";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TermekInfo termekInfo = new TermekInfo();
                                termekInfo.Id = reader.GetInt32(0);
                                termekInfo.Cikkszam = reader.GetString(1);
                                termekInfo.Termeknev = reader.GetString(2);
                                termekInfo.Leiras = reader.GetString(3);
                                termekInfo.Ar = reader.GetString(4);
                                termekInfo.CreateAt = reader.GetDateTime(5).ToString("MM/dd/yyyy");

                                TermekLista.Add(termekInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba!" + ex.Message);
            }
        }
    }

    public class TermekInfo
    {
        public int Id { get; set; }
        public string Cikkszam { get; set; } = "";
        public string Termeknev { get; set; } = "";
        public string Leiras { get; set; } = "";
        public string Ar { get; set; } = "";
        public string CreateAt { get; set; } = "";
    }
}
