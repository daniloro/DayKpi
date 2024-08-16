using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class AvaliacaoAtividadeDao : BaseDao
    {
        // <summary> 
        /// Obtem as atividades concluídas do mês com sua ponderação
        /// </summary> 
        /// <param name="login">Login do Analista</param>
        /// <param name="dataInicio">dataInicio</param> 
        /// <param name="dataFim">dataFim</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AvaliacaoAtividadeInfo> ConsultarAtividadePonderacaoMesEquipe(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaAvaliacao = new List<AvaliacaoAtividadeInfo>();
            AvaliacaoAtividadeInfo avaliacao;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar),
                new SqlParameter("@DataInicio", SqlDbType.DateTime),
                new SqlParameter("@DataFim", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataInicio;
            parms[2].Value = dataFim;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_AVALIACAO_ATIVIDADE_PONDERACAO_MES_EQUIPE", parms))
            {
                while (dataReader.Read())
                {
                    avaliacao = new AvaliacaoAtividadeInfo
                    {
                        LoginAd = dataReader["LoginAd"].ToString(),
                        Ponderacao = Convert.ToInt32(dataReader["Ponderacao"]),
                        QtdConfirmacao = Convert.ToInt32(dataReader["QtdConfirmacao"]),
                        QtdRepactuacaoSemJustificativa = Convert.ToInt32(dataReader["QtdRepactuacaoSemJustificativa"]),
                        CodAvaliacaoAuto = dataReader["CodAvaliacaoAuto"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["CodAvaliacaoAuto"]),
                        CodAvaliacao = dataReader["CodAvaliacao"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["CodAvaliacao"]),
                        NotaAvaliacao = dataReader["NotaAvaliacao"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["NotaAvaliacao"])
                    };
                    listaAvaliacao.Add(avaliacao);
                }
            }
            return listaAvaliacao;
        }

        // <summary> 
        /// Obtem as atividades concluídas do mês com sua ponderação
        /// </summary> 
        /// <param name="login">Login do Analista</param>
        /// <param name="dataInicio">dataInicio</param> 
        /// <param name="dataFim">dataFim</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AvaliacaoAtividadeInfo> ConsultarAtividadePonderacaoMes(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaAvaliacao = new List<AvaliacaoAtividadeInfo>();
            AvaliacaoAtividadeInfo avaliacao;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar),
                new SqlParameter("@DataInicio", SqlDbType.DateTime),
                new SqlParameter("@DataFim", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataInicio;
            parms[2].Value = dataFim;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_AVALIACAO_ATIVIDADE_PONDERACAO_MES", parms))
            {
                while (dataReader.Read())
                {
                    avaliacao = new AvaliacaoAtividadeInfo
                    {
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        NroAtividade = dataReader["NroAtividade"].ToString(),                        
                        DscPlanejador = dataReader["Abreviado"] == DBNull.Value ? "Outros" : dataReader["Abreviado"].ToString(),
                        DataConclusao = Convert.ToDateTime(dataReader["DataConclusao"]),
                        TipoChamado = dataReader["TipoChamado"].ToString(),
                        Ponderacao = Convert.ToInt32(dataReader["Ponderacao"]),
                        QtdConfirmacao = Convert.ToInt32(dataReader["QtdConfirmacao"]),                        
                        QtdRepactuacaoSemJustificativa = Convert.ToInt32(dataReader["QtdRepactuacaoSemJustificativa"]),
                        CodAvaliacaoAuto = dataReader["CodAvaliacaoAuto"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["CodAvaliacaoAuto"]),
                        CodAvaliacao = dataReader["CodAvaliacao"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["CodAvaliacao"]),
                        NotaAvaliacao = dataReader["NotaAvaliacao"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["NotaAvaliacao"])
                    };
                    listaAvaliacao.Add(avaliacao);
                }
            }
            return listaAvaliacao;
        }

        // <summary> 
        /// Obtem os detalhes da Atividade
        /// </summary> 
        /// <param name="nroAtividade">Número da Atividade</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public AvaliacaoAtividadeInfo ConsultarAtividade(string nroAtividade)
        {
            AvaliacaoAtividadeInfo atividade = null;            

            SqlParameter[] parms =
            {
                new SqlParameter("@NroAtividade", SqlDbType.VarChar, 14)};
            parms[0].Value = nroAtividade;
            
            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_AVALIACAO_ATIVIDADE", parms))
            {
                if (dataReader.Read())
                {
                    atividade = new AvaliacaoAtividadeInfo
                    {
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        NroAtividade = dataReader["NroAtividade"].ToString(),
                        DscAtividade = dataReader["DscAtividade"].ToString(),
                        DscPlanejador = dataReader["DscPlanejador"].ToString(),
                        Operador = dataReader["Nome"].ToString(),
                        LoginAd = dataReader["LoginAd"].ToString(),
                        DataConclusao = Convert.ToDateTime(dataReader["DataConclusao"]),
                        TipoChamado = dataReader["TipoChamado"].ToString(),
                        Ponderacao = Convert.ToInt32(dataReader["Ponderacao"]),
                        QtdConfirmacao = Convert.ToInt32(dataReader["QtdConfirmacao"]),
                        QtdRepactuacao = Convert.ToInt32(dataReader["QtdRepactuacao"]),
                        QtdRepactuacaoSemJustificativa = Convert.ToInt32(dataReader["QtdRepactuacaoSemJustificativa"]),
                        CodAvaliacaoAuto = dataReader["CodAvaliacaoAuto"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["CodAvaliacaoAuto"]),
                        CodAvaliacao = dataReader["CodAvaliacao"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["CodAvaliacao"]),                        
                        ObservacaoAuto = dataReader["ObservacaoAuto"].ToString(),
                        Observacao = dataReader["Observacao"].ToString()
                    };                    
                }
            }
            return atividade;
        }

        /// <summary> 
        /// Inclui um novo registro na AvaliacaoAtividade
        /// </summary> 
        /// <param name="avaliacao">Dados da Avaliação</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public int IncluirAvaliacaoAtividade(AvaliacaoAtividadeInfo avaliacao)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@NroAtividade", SqlDbType.VarChar, 14),
                new SqlParameter("@NroChamado", SqlDbType.VarChar, 14),
                new SqlParameter("@TipoAvaliacao", SqlDbType.Int),
                new SqlParameter("@LoginAd", SqlDbType.VarChar, 8),
                new SqlParameter("@LoginAvaliador", SqlDbType.VarChar, 8),
                new SqlParameter("@DataAvaliacao", SqlDbType.DateTime),                
                new SqlParameter("@Observacao", SqlDbType.VarChar)
            };
            parms[0].Value = avaliacao.NroAtividade;
            parms[1].Value = avaliacao.NroChamado;
            parms[2].Value = avaliacao.TipoAvaliacao;
            parms[3].Value = avaliacao.LoginAd;
            parms[4].Value = avaliacao.LoginAvaliador;
            parms[5].Value = DateTime.Now;
            parms[6].Value = avaliacao.Observacao;

            var codAvaliacao = ExecuteScalar(CommandType.StoredProcedure, "P_INCLUIR_AVALIACAO_ATIVIDADE", parms);
            return Convert.ToInt32(codAvaliacao);
        }

        // <summary> 
        /// Lista os Destaques das Avaliações
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeDestaqueInfo> ConsultarDestaque()
        {
            var listaDestaque = new List<AtividadeDestaqueInfo>();
            AtividadeDestaqueInfo destaque;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_AVALIACAO_DESTAQUE", null))
            {
                while (dataReader.Read())
                {
                    destaque = new AtividadeDestaqueInfo
                    {                        
                        CodDestaque = Convert.ToInt32(dataReader["CodDestaque"]),
                        DscDestaque = dataReader["DscDestaque"].ToString()
                    };
                    listaDestaque.Add(destaque);
                }
            }
            return listaDestaque;
        }

        // <summary> 
        /// Obtem os destaques da avaliação
        /// </summary> 
        /// <param name="codAvaliacao">Código da Avaliação</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeDestaqueInfo> ConsultarAtividadeDestaque(int codAvaliacao)
        {
            var listaDestaque = new List<AtividadeDestaqueInfo>();
            AtividadeDestaqueInfo destaque;

            SqlParameter[] parms =
            {
                new SqlParameter("@CodAvaliacao", SqlDbType.Int)};
            parms[0].Value = codAvaliacao;
            
            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_ATIVIDADE_DESTAQUE", parms))
            {
                while (dataReader.Read())
                {
                    destaque = new AtividadeDestaqueInfo
                    {
                        CodAvaliacao = Convert.ToInt32(dataReader["CodAvaliacao"]),
                        CodDestaque = Convert.ToInt32(dataReader["CodDestaque"]),
                        DscDestaque = dataReader["DscDestaque"].ToString(),
                        Ponderacao = Convert.ToInt32(dataReader["Ponderacao"])
                    };
                    listaDestaque.Add(destaque);
                }
            }
            return listaDestaque;
        }

        /// <summary> 
        /// Inclui um novo destaque para a Atividade
        /// </summary> 
        /// <param name="avaliacao">Dados do Destaque</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirAtividadeDestaque(AtividadeDestaqueInfo destaque)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@CodAvaliacao", SqlDbType.Int),
                new SqlParameter("@CodDestaque", SqlDbType.Int)
            };
            parms[0].Value = destaque.CodAvaliacao;
            parms[1].Value = destaque.CodDestaque;
            
            ExecuteNonQuery(CommandType.StoredProcedure, "P_INCLUIR_AVALIACAO_ATIVIDADE_DESTAQUE", parms);
        }
    }
}