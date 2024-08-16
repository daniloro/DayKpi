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

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_PONTO", parms))
            {
                if (dataReader.Read())
                {
                    ponto.CodPonto = Convert.ToInt32(dataReader["CodPonto"]);
                    ponto.HoraInicio = Convert.ToDateTime(dataReader["HoraInicio"]);
                    ponto.HoraAlmoco = Convert.ToDateTime(dataReader["HoraAlmoco"] == DBNull.Value ? DateTime.MinValue : dataReader["HoraAlmoco"]);
                    ponto.HoraRetorno = Convert.ToDateTime(dataReader["HoraRetorno"] == DBNull.Value ? DateTime.MinValue : dataReader["HoraRetorno"]);
                    ponto.HoraSaida = Convert.ToDateTime(dataReader["HoraSaida"] == DBNull.Value ? DateTime.MinValue : dataReader["HoraSaida"]);
                    ponto.HomeOffice = Convert.ToBoolean(dataReader["HomeOffice"] == DBNull.Value ? false : dataReader["HomeOffice"]);
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
                new SqlParameter("@HoraInicio", SqlDbType.DateTime),
                new SqlParameter("@HomeOffice", SqlDbType.Bit)
            };
            parms[0].Value = ponto.LoginAd;
            parms[1].Value = ponto.DataPonto;
            parms[2].Value = ponto.HoraInicio;
            parms[3].Value = ponto.HomeOffice;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_INCLUIR_PONTO", parms);
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

            ExecuteNonQuery(CommandType.StoredProcedure, "P_ALTERAR_PONTO", parms);
        }

        /// <summary> 
        /// Inclui um novo registro na tabela Ponto
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirPontoManual(PontoInfo ponto)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@CodPonto", SqlDbType.Int),
                new SqlParameter("@HoraManual", SqlDbType.DateTime),
                new SqlParameter("@TipoPonto", SqlDbType.Int)
            };
            parms[0].Value = ponto.CodPonto;
            parms[1].Value = ponto.HoraManual;
            parms[2].Value = ponto.TipoPonto;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_INCLUIR_PONTO_MANUAL", parms);
        }

        /// <summary> 
        /// Consultar lista de ponto
        /// </summary> 
        /// <param name="codEquipe">Codigo da Equipe</param>
        /// <param name="dataPonto">Data do Ponto</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<PontoInfo> ConsultarPonto(string login, DateTime dataPonto)
        {
            var listaPonto = new List<PontoInfo>();
            PontoInfo ponto;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar),
                new SqlParameter("@DataPonto", SqlDbType.DateTime)
            };
            parms[0].Value = login;
            parms[1].Value = dataPonto;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_PONTO", parms))
            {
                while (dataReader.Read())
                {
                    ponto = new PontoInfo
                    {
                        Nome = dataReader["Nome"].ToString(),
                        HoraInicio = Convert.ToDateTime(dataReader["HoraInicio"] == DBNull.Value ? DateTime.MinValue : dataReader["HoraInicio"]),
                        HoraAlmoco = Convert.ToDateTime(dataReader["HoraAlmoco"] == DBNull.Value ? DateTime.MinValue : dataReader["HoraAlmoco"]),
                        HoraRetorno = Convert.ToDateTime(dataReader["HoraRetorno"] == DBNull.Value ? DateTime.MinValue : dataReader["HoraRetorno"]),
                        HoraSaida = Convert.ToDateTime(dataReader["HoraSaida"] == DBNull.Value ? DateTime.MinValue : dataReader["HoraSaida"]),
                        HomeOffice = Convert.ToBoolean(dataReader["HomeOffice"] == DBNull.Value ? false : dataReader["HomeOffice"])
                };
                    listaPonto.Add(ponto);
                }
            }
            return listaPonto;
        }

        /// <summary> 
        /// Consultar o detalhe do Ponto Mensal do Operador
        /// </summary> 
        /// <param name="login">Login</param> 
        /// <param name="dataInicio">Data Inicial</param>
        /// <param name="dataFim">Data Final</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<PontoInfo> ConsultarPontoMensalOperador(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaPonto = new List<PontoInfo>();
            PontoInfo ponto;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@DataInicio", SqlDbType.DateTime),
                new SqlParameter("@DataFim", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataInicio;
            parms[2].Value = dataFim;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_PONTO_MENSAL_OPERADOR", parms))
            {
                while (dataReader.Read())
                {
                    ponto = new PontoInfo
                    {
                        LoginAd = dataReader["LoginAd"].ToString(),
                        DataPonto = Convert.ToDateTime(dataReader["DataPonto"]),
                        CodPonto = Convert.ToInt32(dataReader["CodPonto"] == DBNull.Value ? 0 : dataReader["CodPonto"]),
                        HoraInicio = Convert.ToDateTime(dataReader["HoraInicio"] == DBNull.Value ? DateTime.MinValue : dataReader["HoraInicio"]),
                        HoraAlmoco = Convert.ToDateTime(dataReader["HoraAlmoco"] == DBNull.Value ? DateTime.MinValue : dataReader["HoraAlmoco"]),
                        HoraRetorno = Convert.ToDateTime(dataReader["HoraRetorno"] == DBNull.Value ? DateTime.MinValue : dataReader["HoraRetorno"]),
                        HoraSaida = Convert.ToDateTime(dataReader["HoraSaida"] == DBNull.Value ? DateTime.MinValue : dataReader["HoraSaida"]),
                        HoraInicioManual = Convert.ToBoolean(dataReader["HoraInicioManual"] == DBNull.Value ? 0 : 1),
                        HoraAlmocoManual = Convert.ToBoolean(dataReader["HoraAlmocoManual"] == DBNull.Value ? 0 : 1),
                        HoraRetornoManual = Convert.ToBoolean(dataReader["HoraRetornoManual"] == DBNull.Value ? 0 : 1),
                        HoraSaidaManual = Convert.ToBoolean(dataReader["HoraSaidaManual"] == DBNull.Value ? 0 : 1),
                    };
                    listaPonto.Add(ponto);
                }
            }
            return listaPonto;
        }

        /// <summary> 
        /// Consultar o resumo do Ponto Mensal da Equipe
        /// </summary> 
        /// <param name="login">Login</param> 
        /// <param name="dataPonto">Data da Consulta</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<PontoMensalInfo> ConsultarPontoMensal(string login, DateTime dataPonto)
        {
            var listaPonto = new List<PontoMensalInfo>();
            PontoMensalInfo ponto;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@DataPonto", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataPonto;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_PONTO_MENSAL", parms))
            {
                while (dataReader.Read())
                {
                    ponto = new PontoMensalInfo
                    {
                        CodPontoMensal = Convert.ToInt32(dataReader["CodPontoMensal"] == DBNull.Value ? 0 : dataReader["CodPontoMensal"]),
                        DataPonto = Convert.ToDateTime(dataReader["DataPonto"] == DBNull.Value ? DateTime.MinValue : dataReader["DataPonto"]),
                        LoginAd = dataReader["LoginAd"].ToString(),
                        Nome = dataReader["Nome"].ToString(),                        
                        Atraso = Convert.ToInt32(dataReader["Atraso"] == DBNull.Value ? 0 : dataReader["Atraso"]),
                        CinquentaPorcento = Convert.ToInt32(dataReader["CinquentaPorcento"] == DBNull.Value ? 0 : dataReader["CinquentaPorcento"]),
                        CemPorcento = Convert.ToInt32(dataReader["CemPorcento"] == DBNull.Value ? 0 : dataReader["CemPorcento"]),
                        Noturno = Convert.ToInt32(dataReader["Noturno"] == DBNull.Value ? 0 : dataReader["Noturno"]),
                        Observacao = dataReader["Observacao"].ToString()
                    };
                    listaPonto.Add(ponto);
                }
            }
            return listaPonto;
        }

        /// <summary> 
        /// Inclui um novo registro na tabela PontoMensal
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirPontoMensal(PontoMensalInfo ponto)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@DataPonto", SqlDbType.DateTime),
                new SqlParameter("@LoginAd", SqlDbType.VarChar, 8),
                new SqlParameter("@LoginGestor", SqlDbType.VarChar, 8),
                new SqlParameter("@Atraso", SqlDbType.Int),
                new SqlParameter("@CinquentaPorcento", SqlDbType.Int),
                new SqlParameter("@CemPorcento", SqlDbType.Int),
                new SqlParameter("@Noturno", SqlDbType.Int),
                new SqlParameter("@Observacao", SqlDbType.VarChar, 500)
            };
            parms[0].Value = ponto.DataPonto;
            parms[1].Value = ponto.LoginAd;
            parms[2].Value = ponto.LoginGestor;
            parms[3].Value = ponto.Atraso;
            parms[4].Value = ponto.CinquentaPorcento;
            parms[5].Value = ponto.CemPorcento;
            parms[6].Value = ponto.Noturno;
            parms[7].Value = ponto.Observacao;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_INCLUIR_PONTO_MENSAL", parms);
        }

        /// <summary> 
        /// Altera um registro na tabela PontoMensal
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarPontoMensal(PontoMensalInfo ponto)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@CodPontoMensal", SqlDbType.Int),                
                new SqlParameter("@LoginGestor", SqlDbType.VarChar, 8),
                new SqlParameter("@Atraso", SqlDbType.Int),
                new SqlParameter("@CinquentaPorcento", SqlDbType.Int),
                new SqlParameter("@CemPorcento", SqlDbType.Int),
                new SqlParameter("@Noturno", SqlDbType.Int),
                new SqlParameter("@Observacao", SqlDbType.VarChar, 500)
            };
            parms[0].Value = ponto.CodPontoMensal;            
            parms[1].Value = ponto.LoginGestor;
            parms[2].Value = ponto.Atraso;
            parms[3].Value = ponto.CinquentaPorcento;
            parms[4].Value = ponto.CemPorcento;
            parms[5].Value = ponto.Noturno;
            parms[6].Value = ponto.Observacao;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_ALTERAR_PONTO_MENSAL", parms);
        }
    }
}