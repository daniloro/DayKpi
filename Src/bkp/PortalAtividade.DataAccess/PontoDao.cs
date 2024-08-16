using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class PontoDao : BaseDao
    {
        /// <summary> 
        /// Obtem os dados do Ponto do dia
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public PontoInfo ObterPonto(string login)
        {
            var ponto = new PontoInfo();

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.Text, "SELECT " +
                                                                    "CodPonto, " +
                                                                    "HoraInicio, " +
                                                                    "HoraAlmoco, " +
                                                                    "HoraRetorno, " +
                                                                    "HoraSaida " +
                                                                    "FROM Ponto " +
                                                                    "WHERE " +
                                                                    "LoginAd = @Login AND " +
                                                                    "DataPonto = CONVERT(VARCHAR(10), GETDATE(), 120)", parms))
            {
                if (dataReader.Read())
                {
                    ponto.CodPonto = Convert.ToInt32(dataReader["CodPonto"]);
                    ponto.HoraInicio = Convert.ToDateTime(dataReader["HoraInicio"]);
                    ponto.HoraAlmoco = Convert.ToDateTime(dataReader["HoraAlmoco"] == DBNull.Value ? DateTime.MinValue : dataReader["HoraAlmoco"]);
                    ponto.HoraRetorno = Convert.ToDateTime(dataReader["HoraRetorno"] == DBNull.Value ? DateTime.MinValue : dataReader["HoraRetorno"]);
                    ponto.HoraSaida = Convert.ToDateTime(dataReader["HoraSaida"] == DBNull.Value ? DateTime.MinValue : dataReader["HoraSaida"]);
                }
            }
            return ponto;
        }

        /// <summary> 
        /// Inclui um novo registro na tabela Ponto
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirPonto(PontoInfo ponto)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@LoginAd", SqlDbType.VarChar, 8),
                new SqlParameter("@DataPonto", SqlDbType.DateTime),
                new SqlParameter("@HoraInicio", SqlDbType.DateTime)                
            };
            parms[0].Value = ponto.LoginAd;
            parms[1].Value = ponto.DataPonto;
            parms[2].Value = ponto.HoraInicio;

            ExecuteNonQuery(CommandType.Text, "INSERT INTO Ponto " +
                                            "(LoginAd, DataPonto, HoraInicio) " +
                                            "VALUES (@LoginAd, @DataPonto, @HoraInicio)", parms);
        }

        /// <summary> 
        /// Altera um registro na tabela Ponto
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarPonto(PontoInfo ponto)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@CodPonto", SqlDbType.Int),
                new SqlParameter("@HoraAlmoco", SqlDbType.DateTime),
                new SqlParameter("@HoraRetorno", SqlDbType.DateTime),
                new SqlParameter("@HoraSaida", SqlDbType.DateTime)
            };
            parms[0].Value = ponto.CodPonto;
            parms[1].Value = ponto.HoraAlmoco;

            if (ponto.HoraRetorno == DateTime.MinValue)
                parms[2].Value = DBNull.Value;
            else
                parms[2].Value = ponto.HoraRetorno;

            if (ponto.HoraSaida == DateTime.MinValue)
                parms[3].Value = DBNull.Value;
            else
                parms[3].Value = ponto.HoraSaida;

            ExecuteNonQuery(CommandType.Text, "UPDATE Ponto " +
                                            "SET HoraAlmoco = @HoraAlmoco, " +
                                            "HoraRetorno = @HoraRetorno, " +
                                            "HoraSaida = @HoraSaida " +
                                            "WHERE CodPonto = @CodPonto", parms);
        }
    }
}