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
        /// Obtem a atividade atual do Analista
        /// </summary> 
        /// <param name="login">Login do analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public AtividadeInfo ObterAtividadeAtual(string login)
        {
            return AtividadeDao.ObterAtividadeAtual(login);
        }

        /// <summary> 
        /// Obtem as atividades repactuadas pendentes
        /// </summary> 
        /// <param name="login">Login do analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeInfo> ConsultarAtividadeRepactuadaPendente(string login)
        {
            return AtividadeDao.ConsultarAtividadeRepactuadaPendente(login);
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
    }
}