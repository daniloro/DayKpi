using System.Collections.Generic;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class PlanejadorBo
    {
        private static readonly PlanejadorDao PlanejadorDao = new PlanejadorDao();

        #region " Singleton "

        private static PlanejadorBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static PlanejadorBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new PlanejadorBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Consulta todos itens do Planejador da Equipe
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<PlanejadorInfo> ConsultarPlanejador(string login)
        {
            return PlanejadorDao.ConsultarPlanejador(login);
        }

        /// <summary> 
        /// Obtem o principal tipo de atividade do operador
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public int ObterPlanejadorPrincipal(int codTipoEquipe)
        {
            var codPlanejador = 0;

            switch (codTipoEquipe)
            {
                case (int)TiposInfo.TipoEquipe.Desenvolvimento:
                    codPlanejador = 6;
                    break;
                case (int)TiposInfo.TipoEquipe.Business:
                    codPlanejador = 1;
                    break;                
                default:                    
                    break;
            }

            return codPlanejador;
        }
    }
}