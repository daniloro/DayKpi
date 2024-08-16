using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class RelatorioDao : BaseDao
    {
        /// <summary> 
        /// Consulta Backlog do Periodo
        /// </summary>
        /// <param name="login">login</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<RelBacklogInfo> ConsultarBacklog(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaRelatorio = new List<RelBacklogInfo>();
            RelBacklogInfo relatorio;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar),
                new SqlParameter("@DataInicio", SqlDbType.DateTime),
                new SqlParameter("@DataFim", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataInicio;
            parms[2].Value = dataFim;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_RELATORIO_BACKLOG", parms))
            {
                while (dataReader.Read())
                {
                    relatorio = new RelBacklogInfo
                    {
                        NroChamado = dataReader["NroChamado"].ToString(),
                        TipoChamado = dataReader["TipoChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        Grupo = dataReader["Grupo"].ToString(),
                        Operador = dataReader["Operador"].ToString(),
                        DscPlanejador = dataReader["DscPlanejador"].ToString(),
                        DataAbertura = Convert.ToDateTime(dataReader["DataAbertura"]),
                        DataConclusao = dataReader["DataConclusao"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["DataConclusao"]),
                        CodStatus = Convert.ToInt32(dataReader["CodStatus"])
                    };
                    listaRelatorio.Add(relatorio);
                }
            }
            return listaRelatorio;
        }

        /// <summary> 
        /// Consulta dos Chamados em Fila
        /// </summary> 
        /// <returns></returns>
        /// <remarks></remarks>
        public List<RelEmFilaInfo> ConsultarEmFila(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaRelatorio = new List<RelEmFilaInfo>();
            RelEmFilaInfo relatorio;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@DataInicio", SqlDbType.DateTime),
                new SqlParameter("@DataFim", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataInicio;
            parms[2].Value = dataFim;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_RELATORIO_EMFILA", parms))
            {
                while (dataReader.Read())
                {
                    relatorio = new RelEmFilaInfo
                    {
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        TipoChamado = dataReader["TipoChamado"].ToString(),
                        NroAtividade = dataReader["NroAtividade"].ToString(),
                        DscAtividade = dataReader["DscAtividade"].ToString(),                        
                        CodPlanejador = Convert.ToInt32(dataReader["CodPlanejador"]),
                        Grupo = dataReader["Grupo"].ToString(),
                        Operador = dataReader["Operador"].ToString(),                        
                        DataAbertura = Convert.ToDateTime(dataReader["DataAbertura"]),
                        DataFinal = Convert.ToDateTime(dataReader["DataFinal"]),
                        DataConclusao = dataReader["DataConclusao"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["DataConclusao"]),
                        CodStatus = Convert.ToInt32(dataReader["CodStatus"])
                    };
                    listaRelatorio.Add(relatorio);
                }
            }
            return listaRelatorio;
        }

        /// <summary> 
        /// Consulta EmFila do Periodo
        /// </summary>
        /// <param name="grupo">grupo</param>
        /// <param name="dataInicio">Inicio consulta</param>
        /// <param name="dataFim">Fim consulta</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<RelEmFilaInfo> ConsultarEmFilaGrupo(string grupo, DateTime dataInicio, DateTime dataFim)
        {
            var listaRelatorio = new List<RelEmFilaInfo>();
            RelEmFilaInfo relatorio;

            SqlParameter[] parms =
            {
                new SqlParameter("@Grupo", SqlDbType.VarChar, 50),
                new SqlParameter("@DataInicio", SqlDbType.DateTime),
                new SqlParameter("@DataFim", SqlDbType.DateTime)};
            parms[0].Value = grupo;
            parms[1].Value = dataInicio;
            parms[2].Value = dataFim;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_RELATORIO_EMFILA_GRUPO", parms))
            {
                while (dataReader.Read())
                {
                    relatorio = new RelEmFilaInfo
                    {
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        NroAtividade = dataReader["NroAtividade"].ToString(),
                        TipoChamado = dataReader["TipoChamado"].ToString(),
                        CodPlanejador = Convert.ToInt32(dataReader["CodPlanejador"]),
                        Grupo = dataReader["Grupo"].ToString(),
                        Operador = dataReader["Operador"].ToString(),
                        DataAbertura = Convert.ToDateTime(dataReader["DataAbertura"]),
                        DataFinal = Convert.ToDateTime(dataReader["DataFinal"]),
                        DataConclusao = dataReader["DataConclusao"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["DataConclusao"]),
                        CodStatus = Convert.ToInt32(dataReader["CodStatus"])
                    };                    
                    listaRelatorio.Add(relatorio);
                }
            }
            return listaRelatorio;
        }

        /// <summary> 
        /// Consulta Atividades Concluidas por Operador
        /// </summary>
        /// <param name="login">Login do Operador</param>
        /// <param name="dataInicio">Inicio consulta</param>
        /// <param name="dataFim">Fim consulta</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<RelConcluidoOperadorInfo> ConsultarConcluidoOperador(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaRelatorio = new List<RelConcluidoOperadorInfo>();
            RelConcluidoOperadorInfo relatorio;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@DataInicio", SqlDbType.DateTime),
                new SqlParameter("@DataFim", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataInicio;
            parms[2].Value = dataFim;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_RELATORIO_CONCLUIDO_OPERADOR", parms))
            {
                while (dataReader.Read())
                {
                    relatorio = new RelConcluidoOperadorInfo
                    {
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        NroAtividade = dataReader["NroAtividade"].ToString(),
                        TipoChamado = dataReader["TipoChamado"].ToString(),
                        Grupo = dataReader["Grupo"].ToString(),
                        Operador = dataReader["Operador"].ToString(),                        
                        DataAbertura = Convert.ToDateTime(dataReader["DataAbertura"]),
                        DataConclusao = Convert.ToDateTime(dataReader["DataConclusao"]),
                        CodStatus = Convert.ToInt32(dataReader["CodStatus"])
                    };
                    listaRelatorio.Add(relatorio);
                }
            }
            return listaRelatorio;
        }

        /// <summary> 
        /// Consulta o número de chamados concluidos por categoria em um determinado periodo
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<RelConcluidoConsolidadoInfo> ConsultarConcluidoConsolidado(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaRelatorio = new List<RelConcluidoConsolidadoInfo>();
            RelConcluidoConsolidadoInfo relatorio;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar),
                new SqlParameter("@DataInicio", SqlDbType.DateTime),
                new SqlParameter("@DataFim", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataInicio;
            parms[2].Value = dataFim;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_RELATORIO_CONCLUIDO_CONSOLIDADO", parms))
            {
                while (dataReader.Read())
                {
                    relatorio = new RelConcluidoConsolidadoInfo
                    {
                        Nome = dataReader["Operador"].ToString(),
                        Grupo = dataReader["Grupo"].ToString().Trim(),
                        TipoChamado = dataReader["TipoChamado"].ToString(),
                        CodPlanejador = Convert.ToInt32(dataReader["CodPlanejador"]),
                        Quantidade = Convert.ToInt32(dataReader["Qtd"])
                    };
                    listaRelatorio.Add(relatorio);
                }
            }
            return listaRelatorio;
        }

        /// <summary> 
        /// Consulta os chamados concluidos da equipe em um determinado periodo
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<RelConcluidoInfo> ConsultarConcluido(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaRelatorio = new List<RelConcluidoInfo>();
            RelConcluidoInfo relatorio;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar),
                new SqlParameter("@DataInicio", SqlDbType.DateTime),
                new SqlParameter("@DataFim", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataInicio;
            parms[2].Value = dataFim;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_RELATORIO_CONCLUIDO", parms))
            {
                while (dataReader.Read())
                {
                    relatorio = new RelConcluidoInfo
                    {
                        Nome = dataReader["Operador"].ToString(),
                        Grupo = dataReader["Grupo"].ToString(),
                        TipoChamado = dataReader["TipoChamado"].ToString(),
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        NroAtividade = dataReader["NroAtividade"].ToString(),
                        DscPlanejador = dataReader["DscPlanejador"].ToString(),
                        DscAtividade = dataReader["DscAtividade"].ToString(),
                        DataAbertura = Convert.ToDateTime(dataReader["DataAbertura"]),
                        DataConclusao = Convert.ToDateTime(dataReader["DataConclusao"])
                    };
                    listaRelatorio.Add(relatorio);
                }
            }
            return listaRelatorio;
        }

        /// <summary> 
        /// Consulta o ponto mensal da Equipe. Horas Extras.
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="dataInicio">Data Inicial</param>
        /// <param name="dataFim">Data Final</param>
        /// <returns></returns> 
        /// <remarks></remarks>
        public List<RelPontoMensalInfo> ConsultarPontoMensal(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaRelatorio = new List<RelPontoMensalInfo>();
            RelPontoMensalInfo relatorio;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar),
                new SqlParameter("@DataInicio", SqlDbType.DateTime),
                new SqlParameter("@DataFim", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataInicio;
            parms[2].Value = dataFim;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_RELATORIO_PONTO_MENSAL", parms))
            {
                while (dataReader.Read())
                {
                    relatorio = new RelPontoMensalInfo
                    {
                        LoginAd = dataReader["LoginAd"].ToString(),
                        Grupo = dataReader["Grupo"].ToString(),
                        Nome = dataReader["Nome"].ToString(),
                        HEMensal = dataReader["HEMensal"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["HEMensal"]),
                        HEUltimoDia = dataReader["HEUltimoDia"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["HEUltimoDia"]),                        
                        PontoPendente = Convert.ToInt32(dataReader["PontoPendente"]),
                        PontoIncompleto = Convert.ToInt32(dataReader["PontoIncompleto"]),
                        PontoManual = Convert.ToInt32(dataReader["PontoManual"])
                    };
                    listaRelatorio.Add(relatorio);
                }
            }
            return listaRelatorio;
        }

        /// <summary> 
        /// Consulta o status de todos chamados que teve atuação
        /// </summary>
        /// <param name="login">login</param>
        /// <returns></returns> 
        /// <remarks></remarks>
        public List<RelMeusChamadosInfo> ConsultarMeusChamados(string login)
        {
            var listaRelatorio = new List<RelMeusChamadosInfo>();
            RelMeusChamadosInfo relatorio;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;            

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_RELATORIO_MEUS_CHAMADOS", parms))
            {
                while (dataReader.Read())
                {
                    relatorio = new RelMeusChamadosInfo
                    {
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        Grupo = dataReader["Grupo"].ToString(),
                        Operador = dataReader["Operador"].ToString(),
                        NroAtividade = dataReader["NroAtividade"].ToString(),
                        DscPlanejador = dataReader["Abreviado"].ToString(),
                        DataFinal = dataReader["DataFinal"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["DataFinal"])
                    };
                    listaRelatorio.Add(relatorio);
                }
            }
            return listaRelatorio;
        }

        /// <summary> 
        /// Consulta o Resumo de Preenchimento do KPI Mensal
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<RelResumoKpiInfo> ConsultarResumoKpi(DateTime dataConsulta, string login, int tipoOperador)
        {
            var listaRelatorio = new List<RelResumoKpiInfo>();
            RelResumoKpiInfo relatorio;

            SqlParameter[] parms =
            {                
                new SqlParameter("@DataConsulta", SqlDbType.DateTime),
                new SqlParameter("@LoginAd", SqlDbType.VarChar, 8),
                new SqlParameter("@TipoOperador", SqlDbType.Int)};
            parms[0].Value = dataConsulta;
            parms[1].Value = login;
            parms[2].Value = tipoOperador;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_RESUMO_KPI", parms))
            {
                while (dataReader.Read())
                {
                    relatorio = new RelResumoKpiInfo
                    {
                        LoginAd = dataReader["LoginAd"].ToString(),
                        Nome = dataReader["Nome"].ToString(),
                        CodTipoEquipe = Convert.ToInt32(dataReader["CodTipoEquipe"]),
                        Objetivo = dataReader["Objetivo"] != DBNull.Value && Convert.ToBoolean(dataReader["Objetivo"]),
                        Organograma = dataReader["Organograma"] != DBNull.Value && Convert.ToBoolean(dataReader["Organograma"]),
                        Meta = dataReader["Meta"] != DBNull.Value && Convert.ToBoolean(dataReader["Meta"]),
                        Backlog = dataReader["Organograma"] != DBNull.Value && Convert.ToBoolean(dataReader["Organograma"]),
                        QtdEmFila = Convert.ToInt32(dataReader["QtdEmFila"]),
                        QtdAbertoConcluido = Convert.ToInt32(dataReader["QtdAbertoConcluido"]),
                        QtdAtendimento = Convert.ToInt32(dataReader["QtdAtendimento"]),
                        Gmud = dataReader["Gmud"] != DBNull.Value && Convert.ToBoolean(dataReader["Gmud"]),
                        QtdPerformance = Convert.ToInt32(dataReader["QtdPerformance"]),
                        QtdHoraExtra = Convert.ToInt32(dataReader["QtdHoraExtra"]),
                        QtdVencido = Convert.ToInt32(dataReader["QtdVencido"])
                    };
                    listaRelatorio.Add(relatorio);
                }
            }
            return listaRelatorio;
        }
    }
}
