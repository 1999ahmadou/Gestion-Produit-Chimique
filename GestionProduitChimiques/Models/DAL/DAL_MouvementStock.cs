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
    public class DAL_MouvementStock
    {
        private static MouvementStock GetEntityFromDataRow(DataRow dataRow)
        {
            MouvementStock mouvement = new MouvementStock();
            mouvement.Id = (int)dataRow["Id"];
            mouvement.DateMouvement = dataRow["DateMvt"] == DBNull.Value ? null : (DateTime?)dataRow["DateMvt"];
            mouvement.TypeMvt = dataRow["TypeMvt"] == DBNull.Value ? null : (string)dataRow["TypeMvt"];
            mouvement.Raison = dataRow["Raison"] == DBNull.Value ? null : (string)dataRow["Raison"];
            mouvement.IdProduit = (int)dataRow["IdProduit"];
            mouvement.Quantite = (int)dataRow["Quantite"];
            mouvement.Observation = dataRow["Observations"] == DBNull.Value ? null : (string)dataRow["Observations"];
            mouvement.UniteMesure = dataRow["UniteMesure"] == DBNull.Value ? null : (string)dataRow["UniteMesure"];
            mouvement.produit = DAL_Produit.SelectById(mouvement.IdProduit);
            return mouvement;
        }

        private static List<MouvementStock> GetListFromDataTable(DataTable dt)
        {
            List<MouvementStock> list = new List<MouvementStock>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                    list.Add(GetEntityFromDataRow(dr));
            }
            return list;
        }

        public static void Add(MouvementStock mouvement)
        {
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "INSERT INTO Mouvement (DateMvt, TypeMvt, Raison, IdProduit, Quantite, Observations, UniteMesure ) VALUES (@DateMvt, @TypeMvt, @Raison, @IdProduit, @Quantite, @Observations, @UniteMesure )";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@DateMvt", mouvement.DateMouvement ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TypeMvt", mouvement.TypeMvt?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Raison", mouvement.Raison?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IdProduit", mouvement.IdProduit);
                command.Parameters.AddWithValue("@Quantite", mouvement.Quantite);
                command.Parameters.AddWithValue("@Observations", mouvement.Observation ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UniteMesure", mouvement.UniteMesure ?? (object)DBNull.Value);
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }

        public static void Update(int id, MouvementStock mouvement)
        {
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "UPDATE Mouvement SET  DateMvt= @DateMvt, TypeMvt=@TypeMvt, Raison= @Raison, Quantite= @Quantite, Observations=@Observations , UniteMesure=@UniteMesure WHERE Id = @CurId";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@CurId", id);
                command.Parameters.AddWithValue("@DateMvt", mouvement.DateMouvement ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TypeMvt", mouvement.TypeMvt ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Raison", mouvement.Raison ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Quantite", mouvement.Quantite);
                command.Parameters.AddWithValue("@Observations", mouvement.Observation ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UniteMesure", mouvement.UniteMesure ?? (object)DBNull.Value);
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }

        public static void Delete(int EntityKey)
        {
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "DELETE FROM Mouvement WHERE Id=@EntityKey";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@EntityKey", EntityKey);
                DataBaseAccessUtilities.NonQueryRequest(command);

            }
        }

        public static MouvementStock SelectById(int EntityKey)
        {
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Mouvement WHERE Id = @EntityKey";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@EntityKey", EntityKey);
                DataTable dt = DataBaseAccessUtilities.SelectRequest(command);
                if (dt != null && dt.Rows.Count != 0)
                    return GetEntityFromDataRow(dt.Rows[0]);
                else
                    return null;
            }
        }

        public static List<MouvementStock> SelectAll()
        {
            DataTable dataTable;
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Mouvement";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                dataTable = DataBaseAccessUtilities.SelectRequest(command);
            }
            return GetListFromDataTable(dataTable);
        }

        public static List<MouvementStock> SelectAllEntrant(string typemvt)
        {
            DataTable dataTable;
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Mouvement where TypeMvt=@Entrant";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@Entrant", typemvt);
                dataTable = DataBaseAccessUtilities.SelectRequest(command);
            }
            return GetListFromDataTable(dataTable);
        }

        public static List<MouvementStock> SelectAllSortant(string typemvt)
        {
            DataTable dataTable;
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Mouvement where TypeMvt=@Sortant";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@Sortant", typemvt);
                dataTable = DataBaseAccessUtilities.SelectRequest(command);
            }
            return GetListFromDataTable(dataTable);
        }

        public static void UpdateStockProduit(int EntityKey,int stock)
        {
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "UPDATE Produit SET Stock=@Stock where Id=@CurId";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@Stock",stock);
                command.Parameters.AddWithValue("@CurId", EntityKey);
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }

        public static void UpdateStockLot(int EntityKey, int stock)
        {
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "UPDATE Lot SET Stock=@Stock where Id=@CurId";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@Stock", stock);
                command.Parameters.AddWithValue("@CurId", EntityKey);
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }

        public static Lot VerifIfLotExiste(int EntityKey)
        {
            Lot lot = new Lot();
            using (MySqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Lot WHERE IdProduit = @EntityKey";
                MySqlCommand command = new MySqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@EntityKey", EntityKey);
                MySqlDataReader dr = command.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        lot.Id = (int)dr["Id"];
                        lot.IdProduit = (int)dr["IdProduit"];
                        lot.Purete = (string)dr["Purete"];
                        lot.Concentration = (string)dr["Concentration"];
                        lot.DatePeremption = (DateTime)dr["DatePeremption"];
                        lot.Stock = (int)dr["Stock"];
                    }
                    return lot;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
