using GestionProduitChimiques.Models.Entities;
using GestionProduitChimiques.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionProduitChimiques.Models.DAL
{
    public class DAL_Lot
    {
        public static Lot GetEntityFromDataRow(DataRow dataRow)
        {
            Lot lot = new Lot();
            lot.Id = (int)dataRow["Id"];
            lot.IdProduit = (int)dataRow["IdProduit"];
            lot.Purete = dataRow["Purete"] == DBNull.Value ? null : (string)dataRow["Purete"];
            lot.Concentration = dataRow["Concentration"] == DBNull.Value ? null : (string)dataRow["Concentration"];
            lot.DatePeremption = dataRow["DatePeremption"] == DBNull.Value ? null : (DateTime?)dataRow["DatePeremption"];
            lot.Stock = (int)dataRow["Stock"];
            return lot;
        }

        public static List<Lot> GetListFromDataTable(DataTable dt)
        {
            List<Lot> list = new List<Lot>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                    list.Add(GetEntityFromDataRow(dr));
            }
            return list;
        }

        public static void Add(Lot lot)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "INSERT INTO Lot (IdProduit, Purete, Concentration, Stock, DatePeremption) VALUES (@IdProduit, @Purete, @Concentration, @Stock, @DatePeremption)";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@IdProduit", lot.IdProduit);
                command.Parameters.AddWithValue("@Purete", lot.Purete ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Concentration", lot.Concentration ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DatePeremption", lot.DatePeremption ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Stock", lot.Stock);
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }

        public static void Update(int id, Lot lot)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "UPDATE Lot SET  Purete= @Purete, Concentration= @Concentration, DatePeremption=@DatePeremption, Stock= @Stock WHERE Id = @CurId";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@CurId", id);
                command.Parameters.AddWithValue("@Purete", lot.Purete ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Concentration", lot.Concentration ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DatePeremption", lot.DatePeremption ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Stock", lot.Stock);
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }

        public static void Delete(int EntityKey)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "DELETE FROM Lot WHERE Id=@EntityKey";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@EntityKey", EntityKey);
                DataBaseAccessUtilities.NonQueryRequest(command);

            }
        }

        public static Lot SelectById(int EntityKey)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Lot WHERE Id = @EntityKey";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@EntityKey", EntityKey);
                DataTable dt = DataBaseAccessUtilities.SelectRequest(command);
                if (dt != null && dt.Rows.Count != 0)
                    return GetEntityFromDataRow(dt.Rows[0]);
                else
                    return null;
            }
        }

        public static List<Lot> SelectAll()
        {
            DataTable dataTable;
            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Lot";
                SqlCommand command = new SqlCommand(StrSQL, con);
                dataTable = DataBaseAccessUtilities.SelectRequest(command);
            }
            return GetListFromDataTable(dataTable);
        }

        public static int VerifIfProduitExiste(int Id)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                SqlCommand Command = new SqlCommand();
                Command.CommandText = "select count(*) from Produit where Id=@IdProduit";
                Command.Connection = con;
                Command.Parameters.AddWithValue("@IdProduit", Id);
                int rep = (int)DataBaseAccessUtilities.ScalarRequest(Command);
                if (rep==0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public static Produit SelectProduit(int EntityKey)
        {
            Produit produit = new Produit();
            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Produit WHERE Id = @EntityKey";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@EntityKey", EntityKey);
                SqlDataReader reader = command.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        produit.Reference = (string)reader["Reference"];
                        produit.Nom = (string)reader["Nom"];
                        produit.Formule = (string)reader["Formule"];
                        produit.CAS = (string)reader["CAS"];
                    }
                }
                return produit;
            }
        }

    }
}
