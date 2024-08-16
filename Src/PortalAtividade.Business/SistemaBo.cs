using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class SistemaBo
    {
        private static readonly SistemaDao SistemaDao = new SistemaDao();

        #region " Singleton "

        private static SistemaBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static SistemaBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new SistemaBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Consultar os Sistemas do Operador
        /// </summary> 
        /// <param name="login">Login</param> 
        /// <returns></returns> 
        /// <remarks></remarks>  
        public List<SistemaInfo> ConsultarSistemaOperador(string login)
        {
            return SistemaDao.ConsultarSistemaOperador(login);
        }
    }
}