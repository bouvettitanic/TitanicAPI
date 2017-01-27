using System;
using System.Data;
using System.Data.SqlClient;

namespace TitanicWebService
{
    public class SQLUtil
    {
        private readonly static int CONNECTION_TIMEOUT = 240;

        public static DataTable ExecuteStoredProcedure(string navn, SqlParameter[] par)
        {
            SqlCommand sqlCommand = new SqlCommand(navn);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddRange(par);

            return SQLUtil.ExecuteSQLCommand(sqlCommand);
        }

        //returnerer et objekt
        public static object ExecuteSqlCommandScalar(SqlCommand cmd)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TitanicConnection"].ConnectionString);

            cmd.Connection = connection;
            cmd.Connection.Open();

            object obj = cmd.ExecuteScalar();

            cmd.Connection.Close();
            return obj;
        }

        //uten retur
        public static void ExecuteSqlCommandNonQuery(SqlCommand cmd)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FjorfeConnection"].ConnectionString);

            cmd.Connection = connection;
            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }


        //returnerer datatable
        public static DataTable ExecuteSQLCommand(SqlCommand cmd)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TitanicConnection"].ConnectionString);
            cmd.Connection = connection;
            connection.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            dt.Load(dr);
            connection.Close();
            return dt;
        }

        //bygger opp sqlcommand med gitt sql-string
        public static DataTable ExecuteSQLSelectQuery(string sql_query)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FjorfeConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql_query, connection);

            connection.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            dt.Load(dr);

            connection.Close();
            return dt;

        }

        public static object HentNullableParameter(object obj)
        {

            if (obj == null)
                return DBNull.Value;
            else
                return obj;//NB- NULLABLE parametre
        }

        public static int? HentNullableInt(object obj)
        {

            if (obj == DBNull.Value)
                return null;
            else
                return (int)obj;//NB- NULLABLE parametre
        }

        public static object HentNullableIntIkkeNegativ(object obj)
        {
            int? ret = null;
            if (obj == null)
                return DBNull.Value;
            else
            {
                ret = (int)obj;//NB- NULLABLE parametre
                if(ret < 0)
                {
                    throw new Exception("Ikke tillatt med negativt tall");
                }
                else
                {
                    return ret;
                }
            }
        }

        public static object HentNullableDecimalIkkeNegativ(object obj)
        {
            decimal? ret = null;
            if (obj == null)
                return DBNull.Value;
            else
            {
                ret = (decimal)obj;//NB- NULLABLE parametre
                if (ret < 0)
                {
                    throw new Exception("Ikke tillatt med negativt tall");
                }
                else
                {
                    return ret;
                }
            }
        }

        public static DateTime? HentNullableDateTime(object obj)
        {

            if (obj == DBNull.Value)
                return null;
            else
                return (DateTime)obj;//NB- NULLABLE parametre
        }

        public static Decimal? HentNullableDecimal(object obj)
        {

            if (obj == DBNull.Value)
                return null;
            else
                return (Decimal)obj;//NB- NULLABLE parametre
        }

        public static bool? HentNullableBool(object obj)
        {

            if (obj == DBNull.Value)
                return null;
            else
                return (int)obj == 1 ? true : false;//NB- NULLABLE parametre
        }

        public static string HentNullableString(object obj)
        {

            if (obj == DBNull.Value)
                return "";
            else
                return (string)obj;//NB- NULLABLE parametre
        }
    }
}