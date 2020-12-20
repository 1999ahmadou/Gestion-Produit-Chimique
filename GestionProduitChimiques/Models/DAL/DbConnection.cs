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

       // static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GPCDB";
        static string connectionString = "Server=localhost;Database=mygpc;Uid=root;Pwd=1ngenieur.c0m;";

        public static MySqlConnection GetConnection()
        {
            MySqlConnection cn = null;
            try
            {
                cn = new MySqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return cn;

        }
    }
}
