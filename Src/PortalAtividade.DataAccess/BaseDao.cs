using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PortalAtividade.DataAccess
{
    public class BaseDao
    {
        // Retorna DataReader com informações
        public IDataReader ExecuteReader(CommandType tipoComando, string comandoSql, SqlParameter[] parametros)
        {
            var _conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

            using (var command = new SqlCommand
            {
                Connection = _conn,
                CommandType = tipoComando,
                CommandText = comandoSql
            })
            {
                command.Parameters.Clear();

                if (parametros != null)
                    command.Parameters.AddRange(parametros);
                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();
                IDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
        }

        public int ExecuteNonQuery(CommandType tipoComando, string comandoSql, SqlParameter[] parametros)
        {
            var retorno = 0;

            using (var _conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                using (var command = new SqlCommand
                {
                    Connection = _conn,
                    CommandType = tipoComando,
                    CommandText = comandoSql
                })
                {
                    command.Parameters.Clear();

                    if (parametros != null)
                        command.Parameters.AddRange(parametros);
                    if (command.Connection.State != ConnectionState.Open)
                        command.Connection.Open();
                    retorno = command.ExecuteNonQuery();
                }
            }
            return retorno;
        }

        public object ExecuteScalar(CommandType tipoComando, string comandoSql, SqlParameter[] parametros)
        {
            object retorno;

            using (var _conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                using (var command = new SqlCommand
                {
                    Connection = _conn,
                    CommandType = tipoComando,
                    CommandText = comandoSql
                })
                {
                    command.Parameters.Clear();

                    if (parametros != null)
                        command.Parameters.AddRange(parametros);
                    if (command.Connection.State != ConnectionState.Open)
                        command.Connection.Open();
                    retorno = command.ExecuteScalar();
                }
            }
            return retorno;
        }
    }
}