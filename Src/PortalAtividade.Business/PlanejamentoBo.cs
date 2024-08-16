using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class PlanejamentoBo
    {
        private static readonly PlanejamentoDao PlanejamentoDao = new PlanejamentoDao();

        #region " Singleton "

        private static PlanejamentoBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static PlanejamentoBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new PlanejamentoBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Consulta o planejamento do analista na semana
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<PlanejamentoInfo> ConsultarPlanejamento(string login, DateTime dataConsulta)
        {
            return PlanejamentoDao.ConsultarPlanejamento(login, dataConsulta);
        }

        /// <summary> 
        /// Inclui um novo registro na tabela Planejamento
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirPlanejamento(PlanejamentoInfo planejamento)
        {
            PlanejamentoDao.IncluirPlanejamento(planejamento);
        }

        /// <summary> 
        /// Exclui um planejamento
        /// </summary>
        /// <param name="codPlanejamento">Codigo do Planejamento</param>
        /// <returns></returns> 
        /// <remarks></remarks>  
        public void ExcluirPlanejamento(int codPlanejamento)
        {
            PlanejamentoDao.ExcluirPlanejamento(codPlanejamento);
        }
    }
}