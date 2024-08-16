using System.Collections.Generic;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class AvaliacaoAtividadeBo
    {
        private static readonly AvaliacaoAtividadeDao AvaliacaoAtividadeDao = new AvaliacaoAtividadeDao();

        #region " Singleton "

        private static AvaliacaoAtividadeBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static AvaliacaoAtividadeBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new AvaliacaoAtividadeBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Obtem as atividades concluídas com avaliação pendente
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AvaliacaoAtividadeInfo> ConsultarAtividadeAvaliacaoPendente(string login)
        {
            var listaAvaliacao = AvaliacaoAtividadeDao.ConsultarAtividadeAvaliacaoPendente(login);
            listaAvaliacao.AddRange(AvaliacaoAtividadeDao.ConsultarAtividadeAvaliacaoDevPendente(login));
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
            if (avaliacao.TipoAvaliacao == 2)
                AvaliacaoAtividadeDao.IncluirAvaliacaoAtividade(avaliacao);
            else
                AvaliacaoAtividadeDao.IncluirAvaliacaoAtividadeDev(avaliacao);
        }
    }
}