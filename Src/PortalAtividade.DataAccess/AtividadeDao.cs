using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class AtividadeDao : BaseDao
    {
        /// <summary> 
        /// Obtem os dados da Atividade
        /// </summary> 
        /// <param name="nroAtividade">Nro da Atividade</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public AtividadeInfo ObterAtividade(string nroAtividade)
        {
            var atividade = new AtividadeInfo();

            SqlParameter[] parms =
            {
                new SqlParameter("@NroAtividade", SqlDbType.VarChar)};
            parms[0].Value = nroAtividade;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_ATIVIDADE", parms))
            {
                if (dataReader.Read())
                {                    
                    atividade.NroChamado = dataReader["NroChamado"].ToString();
                    atividade.DscChamado = dataReader["DscChamado"].ToString();
                    atividade.NroAtividade = dataReader["NroAtividade"].ToString();
                    atividade.DscAtividade = dataReader["DscAtividade"].ToString();
                    atividade.DataFinal = Convert.ToDateTime(dataReader["DataFinal"]);                    
                }
            }
            return atividade;
        }

        /// <summary> 
        /// Obtem os dados da Atividade
        /// </summary> 
        /// <param name="nroAtividade">Nro da Atividade</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public AtividadeInfo ObterAtividadeManual(string nroAtividade)
        {
            var atividade = new AtividadeInfo();

            SqlParameter[] parms =
            {
                new SqlParameter("@NroAtividade", SqlDbType.VarChar)};
            parms[0].Value = nroAtividade;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_ATIVIDADE_MANUAL", parms))
            {
                if (dataReader.Read())
                {
                    atividade.Operador = dataReader["Nome"].ToString();
                    atividade.NroChamado = dataReader["NroChamado"].ToString();
                    atividade.DscChamado = dataReader["DscChamado"].ToString();
                    atividade.NroAtividade = dataReader["NroAtividade"].ToString();
                    atividade.DscAtividade = dataReader["DscAtividade"].ToString();
                    atividade.DataFinal = Convert.ToDateTime(dataReader["DataFinal"]);
                    atividade.QtdRepactuacao = Convert.ToInt32(dataReader["QtdRepactuacao"]);
                    atividade.CodStatus = dataReader["CodStatus"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["CodStatus"]);
                    atividade.DataConclusao = dataReader["DataConclusao"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["DataConclusao"]);
                }
            }
            return atividade;
        }

        /// <summary> 
        /// Verifica se a Atividade possui Definição de Data
        /// </summary> 
        /// <param name="nroAtividade">Nro da Atividade</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public bool ObterDefinicaoDataAtividade(string nroAtividade)
        {
            var retorno = false;

            SqlParameter[] parms =
            {
                new SqlParameter("@NroAtividade", SqlDbType.VarChar)};
            parms[0].Value = nroAtividade;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_ATIVIDADE_HISTORICO_DEFINICAO_DATA", parms))
            {
                if (dataReader.Read())
                {
                    retorno = true;
                }
            }
            return retorno;
        }

        /// <summary> 
        /// Obtem os dados da Atividade Atual do Analista
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeInfo> ConsultarAtividadeAtual(string login)
        {
            var listaAtividade = new List<AtividadeInfo>();
            AtividadeInfo atividade;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;            

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_ATIVIDADE_ATUAL", parms))
            {
                while (dataReader.Read())
                {
                    atividade = new AtividadeInfo
                    {                        
                        NroChamado = dataReader["NroChamado"] == DBNull.Value ? "" : dataReader["NroChamado"].ToString().Replace("\\", "\\"),
                        DscChamado = dataReader["DscChamado"] == DBNull.Value ? "" : dataReader["DscChamado"].ToString().Replace("\\", "\\"),
                        NroAtividade = dataReader["NroAtividade"].ToString(),
                        DscAtividade = dataReader["DscAtividade"].ToString(),
                        TipoChamado = dataReader["TipoChamado"].ToString(),
                        DscPlanejador = dataReader["Abreviado"].ToString(),
                        DataFinal = Convert.ToDateTime(dataReader["DataFinal"]),
                        CodPlanejador = dataReader["CodPlanejador"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["CodPlanejador"]),
                        Confirmado = Convert.ToInt32(dataReader["Confirmado"]),
                        TipoEntrada = dataReader["TipoEntrada"].ToString(),
                        QtdConfirmacao = Convert.ToInt32(dataReader["Confirmado"])
                    };
                    listaAtividade.Add(atividade);
                }
            }
            return listaAtividade;
        }

        /// <summary> 
        /// Obtem a quantidade de atividades repactuadas pendentes
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public int ObterQtdAtividadeRepactuadaPendente(string login, DateTime dataConclusao)
        {
            int retorno = 0;   

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar),
                new SqlParameter("@DataConclusao", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataConclusao;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_QTD_ATIVIDADE_REPACTUADA_PENDENTE", parms))
            {
                if (dataReader.Read())
                {
                    retorno = Convert.ToInt32(dataReader["Qtd"]);
                }
            }
            return retorno;
        }

        /// <summary> 
        /// Obtem as atividades repactuadas pendentes
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeInfo> ConsultarAtividadeRepactuadaPendente(string login, DateTime dataConclusao)
        {
            var listaAtividade = new List<AtividadeInfo>();
            AtividadeInfo atividade;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar),
                new SqlParameter("@DataConclusao", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataConclusao;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_ATIVIDADE_REPACTUADA_PENDENTE", parms))
            {
                while (dataReader.Read())
                {
                    atividade = new AtividadeInfo
                    {
                        CodHistorico = Convert.ToInt32(dataReader["CodHistorico"]),
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        NroAtividade = dataReader["NroAtividade"].ToString(),
                        DscAtividade = dataReader["DscAtividade"].ToString(),
                        DataAnterior = Convert.ToDateTime(dataReader["DataAnterior"]),
                        DataFinal = Convert.ToDateTime(dataReader["DataFinal"]),
                        Operador = dataReader["Operador"].ToString(),
                        Grupo = dataReader["Grupo"].ToString(),
                        TipoLancamento = Convert.ToInt32(dataReader["TipoLancamento"]),
                        DataOcorrencia = Convert.ToDateTime(dataReader["Daily"]),
                        QtdRepactuacao = Convert.ToInt32(dataReader["QtdRepactuacao"]),
                        QtdConfirmacao = Convert.ToInt32(dataReader["QtdConfirmacao"])
                    };
                    listaAtividade.Add(atividade);
                }
            }
            return listaAtividade;
        }

        /// <summary> 
        /// Inclui um Historico
        /// </summary> 
        /// <param name="atividadeHistorico">Dados da tabela AtividadeHistorico</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirAtividadeHistorico(AtividadeInfo atividade)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@NroChamado", SqlDbType.VarChar, 14),
                new SqlParameter("@NroAtividade", SqlDbType.VarChar, 14),
                new SqlParameter("@Operador", SqlDbType.VarChar, 100),
                new SqlParameter("@DataAnterior", SqlDbType.DateTime),
                new SqlParameter("@DataFinal", SqlDbType.DateTime),
                new SqlParameter("@Observacao", SqlDbType.VarChar),
                new SqlParameter("@TipoLancamento", SqlDbType.Int),                                
                new SqlParameter("@NroAtividadeAnterior", SqlDbType.VarChar, 14),
                new SqlParameter("@LoginAd", SqlDbType.VarChar, 8)
            };
            parms[0].Value = atividade.NroChamado;
            parms[1].Value = atividade.NroAtividade;            
            parms[2].Value = atividade.Operador;
            parms[3].Value = atividade.DataAnterior;
            parms[4].Value = atividade.DataFinal;
            parms[5].Value = atividade.Observacao;
            parms[6].Value = atividade.TipoLancamento;
            parms[7].Value = atividade.NroAtividadeAnterior;
            parms[8].Value = atividade.LoginAd;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_INCLUIR_ATIVIDADE_HISTORICO", parms);
        }

        /// <summary> 
        /// Altera os dados de TipoLancamento e Observação da AtividadeHistorico
        /// </summary> 
        /// <param name="atividadeHistorico">Dados da tabela AtividadeHistorico</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarAtividadeHistorico(AtividadeInfo atividade)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@CodHistorico", SqlDbType.Int),
                new SqlParameter("@TipoLancamento", SqlDbType.Int),
                new SqlParameter("@Observacao", SqlDbType.VarChar),
                new SqlParameter("@NroAtividadeAnterior", SqlDbType.VarChar, 14),
                new SqlParameter("@LoginAd", SqlDbType.VarChar, 8)
            };
            parms[0].Value = atividade.CodHistorico;
            parms[1].Value = atividade.TipoLancamento;
            parms[2].Value = atividade.Observacao;
            
            if (string.IsNullOrEmpty(atividade.NroAtividadeAnterior))
                parms[3].Value = DBNull.Value;
            else
                parms[3].Value = atividade.NroAtividadeAnterior;
            parms[4].Value = atividade.LoginAd;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_ALTERAR_ATIVIDADE_HISTORICO", parms);
        }

        /// <summary> 
        /// Obtem as atividades pendentes Em Fila
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeInfo> ConsultarAtividadePendente(string login)
        {
            var listaAtividade = new List<AtividadeInfo>();
            AtividadeInfo atividade;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_ATIVIDADE_PENDENTE", parms))
            {
                while (dataReader.Read())
                {
                    atividade = new AtividadeInfo
                    {      
                        Grupo = dataReader["Grupo"].ToString(),
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        NroAtividade = dataReader["NroAtividade"].ToString(),
                        DscAtividade = dataReader["DscAtividade"].ToString(),
                        Operador = dataReader["Operador"].ToString(),
                        DataFinal = Convert.ToDateTime(dataReader["DataFinal"]),
                        DataAbertura = Convert.ToDateTime(dataReader["DataAbertura"]),                        
                        QtdRepactuacao = Convert.ToInt32(dataReader["QtdRepactuacao"])
                    };
                    listaAtividade.Add(atividade);
                }
            }
            return listaAtividade;
        }

        /// <summary> 
        /// Consulta o Histórico de uma Atividade
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeInfo> ConsultarAtividadeHistorico(string nroAtividade)
        {
            var listaAtividade = new List<AtividadeInfo>();
            AtividadeInfo atividade;

            SqlParameter[] parms =
            {
                new SqlParameter("@NroAtividade", SqlDbType.VarChar, 14)};
            parms[0].Value = nroAtividade;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_ATIVIDADE_HISTORICO", parms))
            {
                while (dataReader.Read())
                {
                    atividade = new AtividadeInfo
                    {
                        DataOcorrencia = Convert.ToDateTime(dataReader["Daily"]),
                        Operador = dataReader["Operador"].ToString(),
                        DataAnterior = dataReader["DataAnterior"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["DataAnterior"]),
                        DataFinal = Convert.ToDateTime(dataReader["DataFinal"]),
                        Observacao = dataReader["Observacao"].ToString(),
                        TipoLancamento = Convert.ToInt32(dataReader["TipoLancamento"]),
                        NroAtividadeAnterior = dataReader["NroAtividadeAnterior"].ToString()
                    };
                    listaAtividade.Add(atividade);
                }
            }
            return listaAtividade;
        }        

        /// <summary> 
        /// Confirma se o planejamento para a atividade está ok ou se será alterado
        /// </summary> 
        /// <param name="nroAtividade">Numero da Atividade</param>
        /// <param name="confirmado">Status Confirmado</param>
        /// <remarks></remarks> 
        public void ConfirmarAtividade(string nroAtividade, int confirmado)
        {
            SqlParameter[] parms =
            {                
                new SqlParameter("@NroAtividade", SqlDbType.VarChar, 14),
                new SqlParameter("@Confirmado", SqlDbType.Int)
            };
            parms[0].Value = nroAtividade;
            parms[1].Value = confirmado;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_ALTERAR_ATIVIDADE_CONFIRMADO", parms);
        }

        /// <summary> 
        /// Lista as atividades em andamento para Daily
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeInfo> ConsultarAtividadeAndamento(string login)
        {
            var listaAtividade = new List<AtividadeInfo>();
            AtividadeInfo atividade;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_ATIVIDADE_ANDAMENTO", parms))
            {
                while (dataReader.Read())
                {
                    atividade = new AtividadeInfo
                    {
                        LoginAd = dataReader["LoginAd"].ToString(),
                        Grupo = dataReader["Grupo"].ToString(),
                        Operador = dataReader["Nome"].ToString(),
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        NroAtividade = dataReader["NroAtividade"].ToString(),
                        TipoChamado = dataReader["TipoChamado"].ToString(),
                        DscPlanejador = dataReader["Abreviado"] == DBNull.Value ? "Outros" : dataReader["Abreviado"].ToString(),
                        CodPlanejador = dataReader["CodPlanejador"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["CodPlanejador"]),
                        Confirmado = dataReader["Confirmado"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["Confirmado"]),
                        TipoEntrada = dataReader["TipoEntrada"].ToString(),
                        DataFinal = dataReader["DataFinal"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["DataFinal"])
                    };
                    listaAtividade.Add(atividade);
                }
            }
            return listaAtividade;
        }

        /// <summary> 
        /// Inclui um Historico de Confirmacao de Data Planejada
        /// </summary> 
        /// <param name="nroAtividade">Numero da Atividade</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirAtividadeHistoricoConfirmacao(string nroAtividade)
        {
            SqlParameter[] parms =
            {                
                new SqlParameter("@NroAtividade", SqlDbType.VarChar, 14),                
                new SqlParameter("@DataConfirmacao", SqlDbType.DateTime)          
            };
            parms[0].Value = nroAtividade;
            parms[1].Value = DateTime.Now.Date;            
            
            ExecuteNonQuery(CommandType.StoredProcedure, "P_INCLUIR_ATIVIDADE_HISTORICO_CONFIRMACAO", parms);
        }

        /// <summary> 
        /// Altera a data Final da Atividade
        /// </summary> 
        /// <param name="atividade">Dados da Atividade</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarAtividadeManual(AtividadeInfo atividade)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@NroAtividade", SqlDbType.VarChar, 14),
                new SqlParameter("@DataFinal", SqlDbType.DateTime)
            };
            parms[0].Value = atividade.NroAtividade;
            parms[1].Value = atividade.DataFinal;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_ALTERAR_ATIVIDADE_MANUAL", parms);
        }

        /// <summary> 
        /// Inclui uma atividade manual
        /// </summary> 
        /// <param name="atividade">Dados da Atividade</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirAtividadeManual(AtividadeInfo atividade)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@NroAtividade", SqlDbType.VarChar, 14),
                new SqlParameter("@DscManual", SqlDbType.VarChar, 100),
                new SqlParameter("@DataFinal", SqlDbType.DateTime)
            };
            parms[0].Value = atividade.LoginAd;
            parms[1].Value = atividade.NroAtividade;

            if (string.IsNullOrEmpty(atividade.DscAtividade))
                parms[2].Value = DBNull.Value;
            else
                parms[2].Value = atividade.DscAtividade;
            
            parms[3].Value = atividade.DataFinal;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_INCLUIR_ATIVIDADE_MANUAL", parms);
        }

        // <summary> 
        /// Consulta a atuação do analista no período
        /// </summary> 
        /// <param name="login">Login do Operador</param>
        /// <param name="dataInicio">Data Inicio</param>
        /// <param name="dataFim">Data Final</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeInfo> ConsultarAtividadeAtuacao(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaAtividade = new List<AtividadeInfo>();
            AtividadeInfo atividade;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar),
                new SqlParameter("@DataInicio", SqlDbType.DateTime),
                new SqlParameter("@DataFim", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataInicio;
            parms[2].Value = dataFim;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_ATIVIDADE_ATUACAO", parms))
            {
                while (dataReader.Read())
                {
                    atividade = new AtividadeInfo
                    {
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        NroAtividade = dataReader["NroAtividade"].ToString(),
                        DscAtividade = dataReader["DscAtividade"].ToString(),
                        QtdConfirmacao = Convert.ToInt32(dataReader["QtdConfirmacao"]),
                        CodStatus = Convert.ToInt32(dataReader["CodStatus"])
                    };
                    listaAtividade.Add(atividade);
                }
            }
            return listaAtividade;
        }
    }
}