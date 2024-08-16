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
        /// Obtem os dados da Atividade Atual do Analista
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public AtividadeInfo ObterAtividadeAtual(string login)
        {
            var atividade = new AtividadeInfo();

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.Text, "SELECT TOP 1 " + 
                                                                    "Chamado.NroChamado, " + 
                                                                    "DscChamado, " + 
                                                                    "NroAtividade, " + 
                                                                    "DscAtividade, " + 
                                                                    "Atividade.DataFinal " + 
                                                                    "FROM Atividade " +
                                                                    "INNER JOIN Chamado ON Chamado.NroChamado = Atividade.NroChamado " +
                                                                    "INNER JOIN Operador ON Operador.Nome = Atividade.Operador " +
                                                                    "WHERE CodStatus = 0 AND LoginAd = @Login " +
                                                                    "ORDER BY Atividade.DataFinal", parms))
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
        /// Obtem as atividades repactuadas pendentes
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeInfo> ConsultarAtividadeRepactuadaPendente(string login)
        {
            var listaAtividade = new List<AtividadeInfo>();
            AtividadeInfo atividade;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.Text, "SELECT " +
                                                                    "CodHistorico, " +
                                                                    "Atividade.NroChamado, " +
                                                                    "Atividade.NroAtividade, " +
                                                                    "DscChamado, " +
                                                                    "Atividade.DscAtividade, " +
                                                                    "AtividadeHistorico.Operador, " +
                                                                    "DataAnterior, " +
                                                                    "AtividadeHistorico.DataFinal, " +
                                                                    "NroAtividadeAnterior, " +
                                                                    "TipoLancamento, " +                                                                    
                                                                    "(SELECT Count(NroAtividade) FROM AtividadeHistorico AH WHERE AH.NroAtividade = AtividadeHistorico.NroAtividade AND AH.TipoLancamento <> 1 AND AH.CodHistorico <> AtividadeHistorico.CodHistorico) QtdRepactuacao " +
                                                                    "FROM AtividadeHistorico " +
                                                                    "INNER JOIN Atividade ON Atividade.NroAtividade = AtividadeHistorico.NroAtividade " +
                                                                    "INNER JOIN Chamado ON Chamado.NroChamado = Atividade.NroChamado " +
                                                                    "INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = Atividade.Grupo " +
                                                                    "INNER JOIN Operador ON Operador.LoginAd = OperadorGrupo.LoginAd " +
                                                                    "WHERE CodStatus = 0 AND " +
                                                                    "TipoLancamento IN (0,6) AND " +
                                                                    "LoginAlteracao IS NULL AND " +
                                                                    "Daily >= '2020-08-01' AND " +
                                                                    "OperadorGrupo.LoginAd = @Login AND " +
                                                                    "(Operador.TipoOperador = 2 OR Operador.Nome = Atividade.Operador) " +
                                                                    "ORDER BY Atividade.NroChamado, Atividade.NroAtividade", parms))
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
                        TipoLancamento = Convert.ToInt32(dataReader["TipoLancamento"]),
                        QtdRepactuacao = Convert.ToInt32(dataReader["QtdRepactuacao"])
                    };
                    listaAtividade.Add(atividade);
                }
            }
            return listaAtividade;
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
                new SqlParameter("@NroAtividadeAnterior", SqlDbType.VarChar, 12),
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

            ExecuteNonQuery(CommandType.Text, "UPDATE AtividadeHistorico " +
                                            "SET TipoLancamento = @TipoLancamento, " +
                                            "NroAtividadeAnterior = @NroAtividadeAnterior, " +
                                            "Observacao = @Observacao, " +
                                            "LoginAlteracao = @LoginAd, " +
                                            "DataAlteracao = getdate() " +
                                            "WHERE CodHistorico = @CodHistorico", parms);
        }
    }
}