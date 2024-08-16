using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class PlanejamentoDao : BaseDao
    {
        /// <summary> 
        /// Consulta o planejamento do analista
        /// </summary>
        /// <param name="login">Login do Analista</param>
        /// <param name="dataConsulta">Data da Consulta</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<PlanejamentoInfo> ConsultarPlanejamento(string login, DateTime dataConsulta)
        {
            var listaPlanejamento = new List<PlanejamentoInfo>();
            PlanejamentoInfo planejamento;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@DataPlanejamento", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataConsulta;            

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_PLANEJAMENTO", parms))
            {
                while (dataReader.Read())
                {
                    planejamento = new PlanejamentoInfo
                    {                        
                        CodPlanejamento = Convert.ToInt32(dataReader["CodPlanejamento"]),
                        LoginAd = dataReader["LoginAd"].ToString(),
                        DataPlanejamento = Convert.ToDateTime(dataReader["DataPlanejamento"]),
                        CodTipoPlanejamento = Convert.ToInt32(dataReader["CodTipoPlanejamento"]),
                        NroAtividade = dataReader["NroAtividade"].ToString(),                        
                        DscPlanejamento = dataReader["DscPlanejamento"].ToString()
                    };
                    listaPlanejamento.Add(planejamento);
                }
            }
            return listaPlanejamento;
        }

        /// <summary> 
        /// Inclui um novo registro na tabela Planejamento
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirPlanejamento(PlanejamentoInfo planejamento)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@LoginAd", SqlDbType.VarChar, 8),
                new SqlParameter("@DataPlanejamento", SqlDbType.DateTime),
                new SqlParameter("@CodTipoPlanejamento", SqlDbType.Int),
                new SqlParameter("@NroAtividade", SqlDbType.VarChar, 14),
                new SqlParameter("@DscPlanejamento", SqlDbType.VarChar, 500)
            };
            parms[0].Value = planejamento.LoginAd;
            parms[1].Value = planejamento.DataPlanejamento;
            parms[2].Value = planejamento.CodTipoPlanejamento;

            if (string.IsNullOrEmpty(planejamento.NroAtividade))
                parms[3].Value = DBNull.Value;
            else
                parms[3].Value = planejamento.NroAtividade;            

            if (string.IsNullOrEmpty(planejamento.DscPlanejamento))
                parms[4].Value = DBNull.Value;
            else
                parms[4].Value = planejamento.DscPlanejamento;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_INCLUIR_PLANEJAMENTO", parms);
        }

        /// <summary> 
        /// Exclui um planejamento
        /// </summary>
        /// <param name="codPlanejamento">Codigo do Planejamento</param>
        /// <returns></returns> 
        /// <remarks></remarks>  
        public void ExcluirPlanejamento(int codPlanejamento)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@CodPlanejamento", SqlDbType.Int)
            };
            parms[0].Value = codPlanejamento;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_EXCLUIR_PLANEJAMENTO", parms);
        }
    }
}
