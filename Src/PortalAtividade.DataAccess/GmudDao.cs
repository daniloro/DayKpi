using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class GmudDao : BaseDao
    {
        /// <summary> 
        /// Consultar as GMUDs do Periodo
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="dataInicio">Data Inicial</param>
        /// <param name="dataFim">Data Final</param>
        /// <returns></returns> 
        /// <remarks></remarks>
        public List<GmudInfo> ConsultarGmudPeriodo(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaGmud = new List<GmudInfo>();
            GmudInfo gmud;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@DataInicio", SqlDbType.DateTime),
                new SqlParameter("@DataFim", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataInicio;
            parms[2].Value = dataFim;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_GMUD_PERIODO", parms))
            {
                while (dataReader.Read())
                {
                    gmud = new GmudInfo
                    {
                        VersionId = dataReader["VersionId"].ToString(),
                        NroGmud = dataReader["NroChamado"].ToString(),
                        NomeSistema = dataReader["Categoria"].ToString(),
                        Versao = dataReader["DscVersao"].ToString(),
                        DscGmud = dataReader["DscChamado"].ToString(),
                        DataGmud = dataReader["DataImplementacao"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["DataImplementacao"]),
                        QtdChamado = Convert.ToInt32(dataReader["QtdChamadoEquipe"])
                    };
                    listaGmud.Add(gmud);
                }
            }
            return listaGmud;
        }        

        /// <summary> 
        /// Consultar os chamados de uma GMUD
        /// </summary>
        /// <param name="login">Login do Operador</param>
        /// <param name="versionId">Id da Versão de GMUD</param>
        /// <returns></returns> 
        /// <remarks></remarks>  
        public List<GmudInfo> ConsultarChamadoGmud(string login, string versionId)
        {
            var listaGmud = new List<GmudInfo>();
            GmudInfo gmud;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@VersionId", SqlDbType.VarChar, 36)};
            parms[0].Value = login;
            parms[1].Value = versionId;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_GMUD_CHAMADO", parms))
            {
                while (dataReader.Read())
                {
                    gmud = new GmudInfo
                    {
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        ChamadoEquipe = Convert.ToBoolean(Convert.ToInt32(dataReader["ChamadoEquipe"]) > 0)
                    };
                    listaGmud.Add(gmud);
                }
            }
            return listaGmud;
        }
    }
}