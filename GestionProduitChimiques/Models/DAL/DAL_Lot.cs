using GestionProduitChimiques.Models.Entities;
using GestionProduitChimiques.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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
            lot.UniteMesure = dataRow["UniteMesure"] == DBNull.Value ? null : (string)dataRow["UniteMesure"];
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
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "INSERT INTO Lot (IdProduit, Purete, Concentration, Stock, DatePeremption,UniteMesure) VALUES (@IdProduit, @Purete, @Concentration, @Stock, @DatePeremption, @UniteMesure)";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@IdProduit", lot.IdProduit);
                command.Parameters.AddWithValue("@Purete", lot.Purete ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Concentration", lot.Concentration ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DatePeremption", lot.DatePeremption ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Stock", lot.Stock);
                command.Parameters.AddWithValue("@UniteMesure", lot.UniteMesure);
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }

        public static void Update(int id, Lot lot)
        {
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "UPDATE Lot SET  Purete= @Purete, Concentration= @Concentration, DatePeremption=@DatePeremption, Stock= @Stock WHERE Id = @CurId";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
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
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "DELETE FROM Lot WHERE Id=@EntityKey";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@EntityKey", EntityKey);
                DataBaseAccessUtilities.NonQueryRequest(command);

            }
        }

        public static Lot SelectById(int EntityKey)
        {
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Lot WHERE Id = @EntityKey";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
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
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Lot";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                dataTable = DataBaseAccessUtilities.SelectRequest(command);
            }
            return GetListFromDataTable(dataTable);
        }

        public static List<Lot> SelectAllOfSpecificProduit(int IdProduit)
        {
            DataTable dataTable;
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Lot WHERE IdProduit=@IdProduit";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@IdProduit", IdProduit);
                dataTable = DataBaseAccessUtilities.SelectRequest(command);
            }
            return GetListFromDataTable(dataTable);
        }

        public static int ShowIfLotExistAndReturnId(Lot lot)
        {
            List<Lot> table = SelectAllOfSpecificProduit(lot.IdProduit);
            int rep=-1;
            foreach(Lot item in table)
            {
                if(lot.Concentration == item.Concentration && lot.DatePeremption == item.DatePeremption && lot.Purete == item.Purete)
                {
                    rep= item.Id;
                    break;
                }
                else
                {
                    rep= -1;
                }
            }
            return rep;
        }

        public static int VerifIfProduitExiste(int Id)
        {
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                MySqlCommand Command = new MySqlCommand();
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
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Produit WHERE Id = @EntityKey";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@EntityKey", EntityKey);
                MySqlDataReader reader = command.ExecuteReader();
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
