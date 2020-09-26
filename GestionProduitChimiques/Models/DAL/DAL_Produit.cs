using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GestionProduitChimiques.Models.BLL;
using GestionProduitChimiques.Models.Entities;
using GestionProduitChimiques.Utilities;

namespace GestionProduitChimiques.Models.DAL
{
    public class DAL_Produit
    {
        private static Produit GetEntityFromDataRow(DataRow dataRow)
        {
            Produit produit = new Produit();
            produit.Id = (int)dataRow["Id"];
            produit.Reference = dataRow["Reference"] == DBNull.Value ? null : (string)dataRow["Reference"];
            produit.Nom = dataRow["Nom"] == DBNull.Value ? null : (string)dataRow["Nom"];
            produit.Formule = dataRow["Formule"] == DBNull.Value ? null : (string)dataRow["Formule"];
            produit.CAS = (int)dataRow["CAS"];
            produit.Toxicite = dataRow["Toxicite"] == DBNull.Value ? null : (string)dataRow["Toxicite"];
            produit.EtatPhysique = dataRow["EtatPhysique"] == DBNull.Value ? null : (string)dataRow["EtatPhysique"];
            produit.UniteMesure = dataRow["UniteMesure"] == DBNull.Value ? null : (string)dataRow["UniteMesure"];
            produit.Perissable = Convert.ToByte(dataRow["Perissable"]);
            produit.TempMinStockage = (int)(dataRow["TempMinStockage"]);
            produit.TempMaxStockage = (int)(dataRow["TempMaxStockage"]);
            produit.ConditionStockage = dataRow["ConditionStockage"] == DBNull.Value ? null : (string)dataRow["ConditionStockage"];
            produit.TypeGestion = dataRow["TypeGestion"] == DBNull.Value ? null : (string)dataRow["TypeGestion"];
            produit.StockMin = (int)(dataRow["StockMin"]);
            produit.Stock = (int)(dataRow["Stock"]);
            produit.lots = BLL_Produit.GetAllLotOfProduit(produit.Id);
            return produit;
        }

        private static List<Produit> GetListFromDataTable(DataTable dt)
        {
            List<Produit> list = new List<Produit>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                    list.Add(GetEntityFromDataRow(dr));
            }
            return list;
        }

        public static void Add(Produit produit)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "INSERT INTO Produit (Reference, Nom, Formule, CAS,Toxicite,EtatPhysique,UniteMesure,Perissable,TempMinStockage,TempMaxStockage,ConditionStockage,TypeGestion,StockMin,Stock)" +
                    " VALUES (@Reference, @Nom, @Formule, @CAS,@Toxicite,@EtatPhysique,@UniteMesure,@Perissable,@TempMinStockage,@TempMaxStockage,@ConditionStockage,@TypeGestion,@StockMin,@Stock)";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@Reference", produit.Reference ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Nom", produit.Nom ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Formule", produit.Formule ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CAS", produit.CAS);
                command.Parameters.AddWithValue("@Toxicite", produit.Toxicite ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EtatPhysique", produit.EtatPhysique ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UniteMesure", produit.UniteMesure ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Perissable", produit.Perissable);
                command.Parameters.AddWithValue("@TempMinStockage", produit.TempMinStockage);
                command.Parameters.AddWithValue("@TempMaxStockage", produit.TempMaxStockage);
                command.Parameters.AddWithValue("@ConditionStockage", produit.ConditionStockage ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TypeGestion", produit.TypeGestion ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@StockMin", produit.StockMin);
                command.Parameters.AddWithValue("@Stock", produit.Stock);
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }

        public static void Update(int id, Produit produit)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "UPDATE Produit SET Reference= @Reference, Nom= @Nom, Formule= @Formule, CAS= @CAS,Toxicite= @Toxicite,EtatPhysique= @EtatPhysique, UniteMesure= @UniteMessure, Perissable= @Perissable,TempMinStockage= @TempMinStockage,TempMaxStockage= @TempMaxStockage, ConditionStockage= @ConditionStockage, TypeGestion= @TypeGestion, StockMin= @StockMin, Stock= @Stock WHERE Id = @CurId";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@CurId", id);
                command.Parameters.AddWithValue("@Reference", produit.Reference ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Nom", produit.Nom ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Formule", produit.Formule ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CAS", produit.CAS);
                command.Parameters.AddWithValue("@Toxicite", produit.Toxicite ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EtatPhysique", produit.EtatPhysique ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UniteMessure", produit.UniteMesure ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Perissable", produit.Perissable);
                command.Parameters.AddWithValue("@TempMinStockage", produit.TempMinStockage);
                command.Parameters.AddWithValue("@TempMaxStockage", produit.TempMaxStockage);
                command.Parameters.AddWithValue("@ConditionStockage", produit.ConditionStockage ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TypeGestion", produit.TypeGestion ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@StockMin", produit.StockMin);
                command.Parameters.AddWithValue("@Stock", produit.Stock);
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }

        public static void Delete(int EntityKey)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "DELETE FROM Produit WHERE Id=@EntityKey";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@EntityKey", EntityKey);
                DataBaseAccessUtilities.NonQueryRequest(command);

            }
        }

        public static Produit SelectById(int EntityKey)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Produit WHERE Id = @EntityKey";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@EntityKey", EntityKey);
                DataTable dt = DataBaseAccessUtilities.SelectRequest(command);
                if (dt != null && dt.Rows.Count != 0)
                    return GetEntityFromDataRow(dt.Rows[0]);
                else
                    return null;
            }
        }

        public static List<Produit> SelectAll()
        {
            DataTable dataTable;
            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Produit";
                SqlCommand command = new SqlCommand(StrSQL, con);
                dataTable = DataBaseAccessUtilities.SelectRequest(command);
            }
            return GetListFromDataTable(dataTable);
        }

        public static List<Lot> GetAllLotOfProduit(int EntityKey)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Lot WHERE IdProduit = @EntityKey";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@EntityKey", EntityKey);
                DataTable dt = DataBaseAccessUtilities.SelectRequest(command);
                if (dt != null && dt.Rows.Count != 0)
                    return DAL_Lot.GetListFromDataTable(dt);
                else
                    return null;
            }
        }
    }
}
