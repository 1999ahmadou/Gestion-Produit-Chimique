using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GestionProduitChimiques.Models.DAL
{
    public class DbConnection
    {

        static string connectionString = "Data Source=SQL5057.site4now.net;Initial Catalog=DB_A681FA_bkagnome;User Id=DB_A681FA_bkagnome_admin;Password=Bi!PDgmder52hC*";
        public static SqlConnection GetConnection()
        {
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return cn;

        }
    }
}
