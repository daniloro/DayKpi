using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class AvaliacaoAtividadeDao : BaseDao
    {
        /// <summary> 
        /// Obtem as atividades concluídas com avaliação pendente do Líder
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AvaliacaoAtividadeInfo> ConsultarAtividadeAvaliacaoPendente(string login)
        {
            var listaAvaliacao = new List<AvaliacaoAtividadeInfo>();
            AvaliacaoAtividadeInfo avaliacao;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.Text, "SELECT " +                                                                    
                                                                    "Atividade.NroChamado, " +
                                                                    "Atividade.NroAtividade, " +
                                                                    "DscChamado, " +
                                                                    "Atividade.DscAtividade, " +
                                                                    "Atividade.Operador, " +
                                                                    "Atividade.Grupo, " +
                                                                    "DataConclusao " +
                                                                    "FROM Atividade " +                                                                    
                                                                    "INNER JOIN Chamado ON Chamado.NroChamado = Atividade.NroChamado " +
                                                                    "LEFT OUTER JOIN AvaliacaoAtividade ON AvaliacaoAtividade.NroAtividade = Atividade.NroAtividade " +
                                                                    "INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = Atividade.Grupo " +
                                                                    "INNER JOIN Operador ON Operador.LoginAd = OperadorGrupo.LoginAd " +
                                                                    "WHERE CodStatus = 1 AND " +
                                                                    "Atividade.TipoFase = 3 AND Atividade.TipoChamado = 'Atividade' AND " +
                                                                    "AvaliacaoAtividade.NroAtividade IS NULL AND " +
                                                                    "DataConclusao >= '2020-08-01' AND " +
                                                                    "OperadorGrupo.LoginAd = @Login AND " +
                                                                    "TipoOperador = 2 AND " +
                                                                    "Operador.Nome <> Atividade.Operador " +
                                                                    "ORDER BY Operador, Atividade.NroChamado, Atividade.NroAtividade", parms))
            {
                while (dataReader.Read())
                {
                    avaliacao = new AvaliacaoAtividadeInfo
                    {                        
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        NroAtividade = dataReader["NroAtividade"].ToString(),
                        DscAtividade = dataReader["DscAtividade"].ToString(),
                        Operador = dataReader["Operador"].ToString(),
                        Grupo = dataReader["Grupo"].ToString(),
                        DataConclusao = Convert.ToDateTime(dataReader["DataConclusao"]),
                        TipoAvaliacao = 2
                    };
                    listaAvaliacao.Add(avaliacao);
                }
            }
            return listaAvaliacao;
        }

        /// <summary> 
        /// Inclui um novo registro na AvaliacaoAtividade do Lider
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirAvaliacaoAtividade(AvaliacaoAtividadeInfo avaliacao)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@NroAtividade", SqlDbType.VarChar, 14),
                new SqlParameter("@NroChamado", SqlDbType.VarChar, 14),
                new SqlParameter("@LoginAd", SqlDbType.VarChar, 8),
                new SqlParameter("@Grupo", SqlDbType.VarChar, 50),
                new SqlParameter("@Operador", SqlDbType.VarChar, 100),
                new SqlParameter("@Complexidade", SqlDbType.Int),
                new SqlParameter("@NotaQualidade", SqlDbType.Int),
                new SqlParameter("@NotaPerformance", SqlDbType.Int),
                new SqlParameter("@NotaPo", SqlDbType.Int),
                new SqlParameter("@Observacao", SqlDbType.VarChar)
            };
            parms[0].Value = avaliacao.NroAtividade;
            parms[1].Value = avaliacao.NroChamado;
            parms[2].Value = avaliacao.LoginAd;
            parms[3].Value = avaliacao.Grupo;
            parms[4].Value = avaliacao.Operador;
            parms[5].Value = avaliacao.Complexidade;
            parms[6].Value = avaliacao.NotaQualidade;
            parms[7].Value = avaliacao.NotaPerformance;
            parms[8].Value = avaliacao.NotaPo;
            parms[9].Value = avaliacao.Observacao;

            ExecuteNonQuery(CommandType.Text, "INSERT INTO AvaliacaoAtividade " +
                                            "(NroAtividade, NroChamado, LoginAd, Grupo, Operador, Complexidade, NotaQualidade, NotaPerformance, NotaPo, Observacao, DataAtualizacao) " +
                                            "VALUES (@NroAtividade, @NroChamado, @LoginAd, @Grupo, @Operador, @Complexidade, @NotaQualidade, @NotaPerformance, @NotaPo, @Observacao, getdate())", parms);
        }

        /// <summary> 
        /// Obtem as atividades concluídas com avaliação pendente do Desenvolvedor
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AvaliacaoAtividadeInfo> ConsultarAtividadeAvaliacaoDevPendente(string login)
        {
            var listaAvaliacao = new List<AvaliacaoAtividadeInfo>();
            AvaliacaoAtividadeInfo avaliacao;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.Text, "SELECT " +
                                                                    "Atividade.NroChamado, " +
                                                                    "Atividade.NroAtividade, " +
                                                                    "DscChamado, " +
                                                                    "Atividade.DscAtividade, " +
                                                                    "Atividade.Operador, " +
                                                                    "Atividade.Grupo, " +
                                                                    "DataConclusao " +
                                                                    "FROM Atividade " +
                                                                    "INNER JOIN Chamado ON Chamado.NroChamado = Atividade.NroChamado " +
                                                                    "LEFT OUTER JOIN AvaliacaoAtividadeDev ON AvaliacaoAtividadeDev.NroAtividade = Atividade.NroAtividade " +
                                                                    "INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = Atividade.Grupo " +
                                                                    "INNER JOIN Operador ON Operador.LoginAd = OperadorGrupo.LoginAd " +
                                                                    "WHERE CodStatus = 1 AND " +
                                                                    "Atividade.TipoFase = 3 AND Atividade.TipoChamado = 'Atividade' AND " +
                                                                    "AvaliacaoAtividadeDev.NroAtividade IS NULL AND " +
                                                                    "DataConclusao >= '2020-08-01' AND " +
                                                                    "OperadorGrupo.LoginAd = @Login AND " +
                                                                    "Operador.Nome = Atividade.Operador " +
                                                                    "ORDER BY Atividade.NroChamado, Atividade.NroAtividade", parms))
            {
                while (dataReader.Read())
                {
                    avaliacao = new AvaliacaoAtividadeInfo
                    {
                        NroChamado = dataReader["NroChamado"].ToString(),
                        DscChamado = dataReader["DscChamado"].ToString(),
                        NroAtividade = dataReader["NroAtividade"].ToString(),
                        DscAtividade = dataReader["DscAtividade"].ToString(),
                        Operador = dataReader["Operador"].ToString(),
                        Grupo = dataReader["Grupo"].ToString(),
                        DataConclusao = Convert.ToDateTime(dataReader["DataConclusao"]),
                        TipoAvaliacao = 1
                    };
                    listaAvaliacao.Add(avaliacao);
                }
            }
            return listaAvaliacao;
        }

        /// <summary> 
        /// Inclui um novo registro na AvaliacaoAtividadeNegocio do Desenvolvedor
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirAvaliacaoAtividadeDev(AvaliacaoAtividadeInfo avaliacao)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@NroAtividade", SqlDbType.VarChar, 14),
                new SqlParameter("@NroChamado", SqlDbType.VarChar, 14),
                new SqlParameter("@LoginAd", SqlDbType.VarChar, 8),
                new SqlParameter("@Grupo", SqlDbType.VarChar, 50),                
                new SqlParameter("@Complexidade", SqlDbType.Int),
                new SqlParameter("@NotaPo", SqlDbType.Int),
                new SqlParameter("@Analista", SqlDbType.VarChar, 100),
                new SqlParameter("@NotaEspec", SqlDbType.Int),
                new SqlParameter("@NotaNegocio", SqlDbType.Int),
                new SqlParameter("@Observacao", SqlDbType.VarChar)
            };
            parms[0].Value = avaliacao.NroAtividade;
            parms[1].Value = avaliacao.NroChamado;
            parms[2].Value = avaliacao.LoginAd;
            parms[3].Value = avaliacao.Grupo;            
            parms[4].Value = avaliacao.Complexidade;
            parms[5].Value = avaliacao.NotaPo;
            parms[6].Value = avaliacao.Analista;
            parms[7].Value = avaliacao.NotaEspec;
            parms[8].Value = avaliacao.NotaNegocio;
            parms[9].Value = avaliacao.Observacao;

            ExecuteNonQuery(CommandType.Text, "INSERT INTO AvaliacaoAtividadeDev " +
                                            "(NroAtividade, NroChamado, LoginAd, Grupo, Complexidade, NotaPo, Analista, NotaEspec, NotaNegocio, Observacao, DataAtualizacao) " +
                                            "VALUES (@NroAtividade, @NroChamado, @LoginAd, @Grupo, @Complexidade, @NotaPo, @Analista, @NotaEspec, @NotaNegocio, @Observacao, getdate())", parms);
        }
    }
}