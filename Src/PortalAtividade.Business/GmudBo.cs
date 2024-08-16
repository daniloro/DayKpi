using System;
using System.Collections.Generic;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class GmudBo
    {
        private static readonly GmudDao GmudDao = new GmudDao();

        #region " Singleton "

        private static GmudBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static GmudBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new GmudBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Consultar as GMUDs do Periodo
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="dataInicio">Data Inicial</param>
        /// <param name="dataFim">Data Final</param>
        /// <returns></returns> 
        /// <remarks></remarks>
        public List<GmudInfo> ConsultarGmudPeriodo(string login, DateTime dataInicio, DateTime dataFim)
        {
            return GmudDao.ConsultarGmudPeriodo(login, dataInicio, dataFim);
        }

        /// <summary> 
        /// Consultar os chamados de uma GMUD
        /// </summary> 
        /// <param name="versionId">Id da Versão de GMUD</param>
        /// <returns></returns> 
        /// <remarks></remarks>  
        public List<GmudInfo> ConsultarChamadoGmud(string login, string versionId)
        {
            return GmudDao.ConsultarChamadoGmud(login, versionId);
        }                
    }
}