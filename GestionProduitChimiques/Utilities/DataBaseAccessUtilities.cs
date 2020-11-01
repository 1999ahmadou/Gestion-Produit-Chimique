using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GestionProduitChimiques.Utilities
{
    public class DataBaseAccessUtilities
    {
        public static int NonQueryRequest(MySqlCommand MyCommand)
        {
            try
            {
                try
                {
                    MyCommand.Connection.Open();
                }
                catch (MySqlException e)
                {
                    throw new Exception("Database Connection Error", e);
                }

                return MyCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                throw new Exception("DataBase Connection Error", e);
            }
            finally
            {
                MyCommand.Connection.Close();
            }

        }
        public static int NonQueryRequest(string StrRequest, MySqlConnection MyConnection)
        {
            try
            {
                try
                {
                    MyConnection.Open();
                }
                catch (MySqlException e)
                {
                    throw new Exception("DataBase Connection Error", e);
                }

                MySqlCommand MyCommand = new MySqlCommand(StrRequest, MyConnection);
                return MyCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                throw new Exception("Query Execution Error", e);
            }
            finally
            {
                MyConnection.Close();
            }

        }

        public static object ScalarRequest(MySqlCommand MyCommand)
        {
            try
            {
                try
                {
                    MyCommand.Connection.Open();
                }
                catch (MySqlException e)
                {
                    throw new Exception("Database Connection Error", e);
                }

                return MyCommand.ExecuteScalar();
            }
            catch (MySqlException e)
            {
                throw new Exception("Query Execution Error", e);
            }
            finally
            {
                MyCommand.Connection.Close();
            }
        }
        public static object ScalarRequest(string StrRequest, MySqlConnection MyConnection)
        {
            try
            {
                try
                {
                    MyConnection.Open();
                }
                catch (MySqlException e)
                {
                    throw new Exception("Database Connection Error", e);
                }
                MySqlCommand MyCommand = new MySqlCommand(StrRequest, MyConnection);

                return MyCommand.ExecuteScalar();
            }
            catch (MySqlException e)
            {
                throw new Exception("Query Execution Error", e);
            }
            finally
            {
                MyConnection.Close();
            }
        }


        public static DataTable SelectRequest(MySqlCommand MyCommand)
        {
            try
            {
                DataTable Table;
                MySqlDataAdapter SelectAdapter = new MySqlDataAdapter(MyCommand);
                Table = new DataTable();
                SelectAdapter.Fill(Table);
                return Table;
            }
            catch (MySqlException e)
            {
                throw new Exception("Query Execution Error", e);
            }
            finally
            {
                MyCommand.Connection.Close();
            }
        }
        public static DataTable SelectRequest(string StrSelectRequest, MySqlConnection MyConnection)
        {
            try
            {
                DataTable Table;
                MySqlCommand SelectCommand = new MySqlCommand(StrSelectRequest, MyConnection);
                MySqlDataAdapter SelectAdapter = new MySqlDataAdapter(SelectCommand);
                Table = new DataTable();
                SelectAdapter.Fill(Table);
                return Table;
            }
            catch (MySqlException e)
            {

                throw new Exception("Query Execution Error", e);
            }
            finally
            {
                MyConnection.Close();
            }
        }


        public static void ShowRequest(MySqlCommand Cmd)
        {
            String ListPar = "\t\t****Texte de la Requete****\n";
            ListPar += Cmd.CommandText + "\n";
            ListPar += "\t\t****Liste des parmêtres : ****\n";
            foreach (MySqlParameter Param in Cmd.Parameters)
            {
                ListPar += Param.ParameterName + "\t:\t\"" + Param.Value.ToString() + "\"\t:\t" + Param.DbType.ToString() + "\n";
            }
        }

        public static bool CheckFieldValueExistence(string TableName, string FieldName, MySqlDbType FieldType, object FieldValue, MySqlConnection MyConnection)
        {
            try
            {
                string StrRequest = "SELECT COUNT(" + FieldName + ") FROM " + TableName + " WHERE ((" + FieldName + " = @" + FieldName + ")";
                StrRequest += "OR ( (@" + (FieldName + 1).ToString() + " IS NULL)AND (" + FieldName + " IS NULL)))";
                MySqlCommand Command = new MySqlCommand(StrRequest, MyConnection);
                Command.Parameters.Add("@" + FieldName, FieldType).Value = FieldValue;
                Command.Parameters.Add("@" + FieldName + 1, FieldType).Value = FieldValue;
                return ((int)DataBaseAccessUtilities.ScalarRequest(Command) != 0);
            }
            catch (MySqlException e)
            {
                throw new Exception("Field Value Existence Check Failed", e);
            }
            finally
            {
                MyConnection.Close();
            }

        }

        public static object GetMaxFieldValue(MySqlConnection MyConnection, string TableName, string FieldName)
        {
            try
            {
                string StrMaxRequest = "SELECT MAX(" + FieldName + ") FROM " + TableName;

                MySqlCommand Command = new MySqlCommand(StrMaxRequest, MyConnection);
                return (DataBaseAccessUtilities.ScalarRequest(Command));

            }
            catch (MySqlException e)
            {
                throw new Exception("Query Execution Error", e);
            }
            finally
            {
                MyConnection.Close();
            }
        }
    }
}
