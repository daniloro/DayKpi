using System.Collections.Generic;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;
namespace PortalAtividade.Business
{
    public class PontoBo
    {
        private static readonly PontoDao PontoDao = new PontoDao();

        #region " Singleton "

        private static PontoBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static PontoBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new PontoBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Obtem os dados do Ponto do dia
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks>
        public PontoInfo ObterPonto(string login)
        {
            return PontoDao.ObterPonto(login);
        }

        /// <summary> 
        /// Inclui um novo registro na tabela Ponto
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirPonto(PontoInfo ponto)
        {
            PontoDao.IncluirPonto(ponto);
        }

        /// <summary> 
        /// Altera um registro na tabela Ponto
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarPonto(PontoInfo ponto)
        {
            PontoDao.AlterarPonto(ponto);
        }
    }
}
