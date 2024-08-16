using System;
using System.Collections.Generic;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class AtividadeBo
    {
        private static readonly AtividadeDao AtividadeDao = new AtividadeDao();

        #region " Singleton "

        private static AtividadeBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static AtividadeBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new AtividadeBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Obtem os dados da Atividade
        /// </summary> 
        /// <param name="nroAtividade">Nro da Atividade</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public AtividadeInfo ObterAtividade(string nroAtividade)
        {
            return AtividadeDao.ObterAtividade(nroAtividade);
        }

        /// <summary> 
        /// Obtem os dados da Atividade
        /// </summary> 
        /// <param name="nroAtividade">Nro da Atividade</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public AtividadeInfo ObterAtividadeManual(string nroAtividade)
        {
            return AtividadeDao.ObterAtividadeManual(nroAtividade);
        }

        /// <summary> 
        /// Verifica se a Atividade possui Definição de Data
        /// </summary> 
        /// <param name="nroAtividade">Nro da Atividade</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public bool ObterDefinicaoDataAtividade(string nroAtividade)
        {
            return AtividadeDao.ObterDefinicaoDataAtividade(nroAtividade);
        }

        /// <summary> 
        /// Obtem a atividade atual do Analista
        /// </summary> 
        /// <param name="login">Login do analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeInfo> ConsultarAtividadeAtual(string login, int codPlanejador)
        {
            var listaAtividade = AtividadeDao.ConsultarAtividadeAtual(login);            
            return FiltrarAtividadesAndamento(listaAtividade, codPlanejador);
        }

        /// <summary> 
        /// Lista as atividades em andamento para Daily
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeInfo> ConsultarAtividadeAndamento(string login, int codPlanejador)
        {
            var listaAtividade = AtividadeDao.ConsultarAtividadeAndamento(login);            
            return FiltrarAtividadesAndamento(listaAtividade, codPlanejador);
        }

        private List<AtividadeInfo> FiltrarAtividadesAndamento(List<AtividadeInfo> listaAtividade, int codPlanejador)
        {
            List<AtividadeInfo> listaRetorno = new List<AtividadeInfo>();

            foreach (var atividade in listaAtividade)
            {
                // Mostra somente a atividade de Apoio/Suporte se a data de conclusão for menor que a atividade Principal
                if ((atividade.CodPlanejador == codPlanejador && !listaRetorno.Exists(p => p.Operador == atividade.Operador && p.CodPlanejador == codPlanejador && p.Confirmado != 2) 
                    || !listaRetorno.Exists(p => p.Operador == atividade.Operador && p.Confirmado != 2)))
                {
                    listaRetorno.Add(atividade);
                }
            }
            return listaRetorno;
        }

        /// <summary> 
        /// Obtem a quantidade de atividades repactuadas pendentes
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public int ObterQtdAtividadeRepactuadaPendente(string login)
        {
            return AtividadeDao.ObterQtdAtividadeRepactuadaPendente(login, DateTime.Today.AddMonths(-1));
        }

        /// <summary> 
        /// Obtem as atividades repactuadas pendentes
        /// </summary> 
        /// <param name="login">Login do analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeInfo> ConsultarAtividadeRepactuadaPendente(string login)
        {
            return AtividadeDao.ConsultarAtividadeRepactuadaPendente(login, DateTime.Today.AddMonths(-1));
        }

        /// <summary> 
        /// Inclui um Historico
        /// </summary> 
        /// <param name="atividadeHistorico">Dados da tabela AtividadeHistorico</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirAtividadeHistorico(AtividadeInfo atividade)
        {
            AtividadeDao.IncluirAtividadeHistorico(atividade);
        }

        /// <summary> 
        /// Altera os dados de TipoLancamento e Observação da AtividadeHistorico
        /// </summary> 
        /// <param name="atividadeHistorico">Dados da tabela AtividadeHistorico</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarAtividadeHistorico(AtividadeInfo atividade)
        {
            AtividadeDao.AlterarAtividadeHistorico(atividade);
        }

        /// <summary> 
        /// Obtem as atividades pendentes Em Fila
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeInfo> ConsultarAtividadePendente(string login)
        {
            return AtividadeDao.ConsultarAtividadePendente(login);
        }

        /// <summary> 
        /// Consulta o Histórico de uma Atividade
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeInfo> ConsultarAtividadeHistorico(string nroAtividade)
        {
            return AtividadeDao.ConsultarAtividadeHistorico(nroAtividade);
        }                

        /// <summary> 
        /// Confirma se o planejamento para a atividade está ok ou se será alterado
        /// </summary> 
        /// <param name="nroAtividade">Numero da Atividade</param>
        /// <param name="confirmado">Status Confirmado</param>
        /// <param name="tipoEntrada">Tipo de Entrada Manual ou Automático</param>
        /// <remarks></remarks> 
        public void ConfirmarAtividade(string nroAtividade, int confirmado)
        {
            AtividadeDao.ConfirmarAtividade(nroAtividade, confirmado);

            if (confirmado == 1)
                AtividadeDao.IncluirAtividadeHistoricoConfirmacao(nroAtividade);
        }

        /// <summary> 
        /// Altera a data Final da Atividade
        /// </summary> 
        /// <param name="atividade">Dados da tabela Atividade</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarAtividadeManual(AtividadeInfo atividade)
        {
            AtividadeDao.AlterarAtividadeManual(atividade);
        }

        /// <summary> 
        /// Inclui uma atividade manual
        /// </summary> 
        /// <param name="atividade">Dados da Atividade</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirAtividadeManual(AtividadeInfo atividade)
        {
            AtividadeDao.IncluirAtividadeManual(atividade);
        }

        /// <summary> 
        /// Obtem o Tipo de Lancamento da Alteracao da Atividade
        /// </summary> 
        /// <param name="minutos">Minutos</param> 
        /// <returns></returns> 
        /// <remarks></remarks>
        public string ObterTipoLancamento(int codTipoLancamento, string nroAtividadeAnterior)
        {
            string tipoLancamento;

            switch (codTipoLancamento)
            {
                case 0:
                    tipoLancamento = "<strong>JUSTIFICATIVA PENDENTE</strong>";
                    break;
                case 1:
                    tipoLancamento = "Início da Atividade";
                    break;
                case 2:
                    tipoLancamento = "Definição de Data";
                    break;
                case 3:
                    tipoLancamento = "Informativo";
                    break;
                case 4:
                    tipoLancamento = "Antecipação de Desenvolvimento";
                    break;
                case 5:
                    tipoLancamento = "Concluído";
                    break;
                case 6:
                    tipoLancamento = "Reiniciado";
                    break;
                case 7:
                    tipoLancamento = "Aumento de Escopo";
                    break;
                case 8:
                    tipoLancamento = "Repactuação de Desenvolvimento";
                    break;
                case 9:
                    tipoLancamento = "Aguardando outra Equipe";
                    tipoLancamento += "</br>" + nroAtividadeAnterior;
                    break;
                case 10:
                    tipoLancamento = "Repactuação da Atividade Anterior";
                    tipoLancamento += "</br>" + nroAtividadeAnterior;
                    break;
                case 11:
                    tipoLancamento = "Repriorizado";
                    tipoLancamento += "</br>" + nroAtividadeAnterior;
                    break;
                case 12:
                    tipoLancamento = "Aguardando GMUD";                    
                    break;
                case 13:
                    tipoLancamento = "Sem Justificativa";                    
                    break;
                default:
                    tipoLancamento = "";
                    break;
            }

            return tipoLancamento;
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
            return AtividadeDao.ConsultarAtividadeAtuacao(login, dataInicio, dataFim);
        }
    }
}