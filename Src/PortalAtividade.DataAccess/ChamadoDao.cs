using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class ChamadoDao : BaseDao
    {
        /// <summary> 
        /// Obtem os dados do Chamado
        /// </summary> 
        /// <param name="nroChamado">Nro do Chamado</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public ChamadoInfo ObterChamado(string nroChamado)
        {
            var chamado = new ChamadoInfo();

            SqlParameter[] parms =
            {
                new SqlParameter("@NroChamado", SqlDbType.VarChar)};
            parms[0].Value = nroChamado;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_CHAMADO", parms))
            {
                if (dataReader.Read())
                {
                    chamado.NroChamado = dataReader["NroChamado"].ToString();
                    chamado.DscChamado = dataReader["DscChamado"].ToString();
                    chamado.TipoChamado = dataReader["TipoChamado"].ToString();
                    chamado.CodStatusChamado = Convert.ToInt32(dataReader["CodStatusChamado"]);
                }
            }
            return chamado;
        }

        /// <summary> 
        /// Consulta os chamados relevantes do mês
        /// </summary> 
        /// <param name="login">Login</param> 
        /// <param name="dataConsulta">Data do mes de Consulta</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<ChamadoInfo> ConsultarChamadoRelevanteMes(string login, DateTime dataConsulta)
        {
            var listaChamado = new List<ChamadoInfo>();
            ChamadoInfo chamado;            

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@DataConsulta", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataConsulta;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_CHAMADO_RELEVANTE_MES", parms))
            {
                while (dataReader.Read())
                {
                    chamado = new ChamadoInfo
                    {
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        Categoria = dataReader["Categoria"].ToString(),
                        SubCategoria = dataReader["SubCategoria"].ToString(),
                        CodStatusChamado = Convert.ToInt32(dataReader["CodStatusChamado"]),
                        DataImplementacao = Convert.ToDateTime(dataReader["DataImplementacao"])
                    };
                    listaChamado.Add(chamado);
                }
            }
            return listaChamado;
        }

        /// <summary> 
        /// Consulta os chamados relevantes pendentes
        /// </summary> 
        /// <param name="login">Login</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<ChamadoInfo> ConsultarChamadoRelevantePendente(string login)
        {
            var listaChamado = new List<ChamadoInfo>();
            ChamadoInfo chamado;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8)};
            parms[0].Value = login;            

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_CHAMADO_RELEVANTE_PENDENTE", parms))
            {
                while (dataReader.Read())
                {
                    chamado = new ChamadoInfo
                    {
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        Categoria = dataReader["Categoria"].ToString(),
                        SubCategoria = dataReader["SubCategoria"].ToString(),
                        CodStatusChamado = Convert.ToInt32(dataReader["CodStatusChamado"]),
                        DataFinal = dataReader["DataFinal"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["DataFinal"])
                    };
                    listaChamado.Add(chamado);
                }
            }
            return listaChamado;
        }

        /// <summary> 
        /// Verifica se o chamado possui alguma atividade com o grupo específico
        /// </summary> 
        /// <param name="nroChamado">Nro do Chamado</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public ChamadoInfo ObterChamadoPossuiGrupo(string nroChamado, string login)
        {
            var chamado = new ChamadoInfo();

            SqlParameter[] parms =
            {
                new SqlParameter("@NroChamado", SqlDbType.VarChar),
                new SqlParameter("@Login", SqlDbType.VarChar, 8)};
            parms[0].Value = nroChamado;
            parms[1].Value = login;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_CHAMADO_POSSUI_GRUPO", parms))
            {
                if (dataReader.Read())
                {
                    chamado.NroChamado = dataReader["NroChamado"].ToString();                    
                    chamado.CodStatusChamado = Convert.ToInt32(dataReader["CodStatusChamado"]);
                    chamado.DataImplementacao = dataReader["DataImplementacao"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["DataImplementacao"]);
                }
            }
            return chamado;
        }

        /// <summary> 
        /// Altera a data Final da Atividade
        /// </summary> 
        /// <param name="nroChamado">Nro do chamado</param>
        /// <param name="relevante">Indicador Importante</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarChamadoRelevante(string nroChamado, bool relevante)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@NroChamado", SqlDbType.VarChar, 14),
                new SqlParameter("@Relevante", SqlDbType.Bit)
            };
            parms[0].Value = nroChamado;
            parms[1].Value = relevante;            

            ExecuteNonQuery(CommandType.StoredProcedure, "P_ALTERAR_CHAMADO_RELEVANTE", parms);            
        }
    }
}