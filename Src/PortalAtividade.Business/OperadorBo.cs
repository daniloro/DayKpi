using System.Collections.Generic;
using System.Linq;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class OperadorBo
    {
        private static readonly OperadorDao OperadorDao = new OperadorDao();

        #region " Singleton "

        private static OperadorBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static OperadorBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new OperadorBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Obtem os dados do Operador
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public OperadorInfo ObterOperador(string login)
        {
            return OperadorDao.ObterOperador(login);
        }        

        /// <summary> 
        /// Consulta todos Operadores do Gestor
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<OperadorInfo> ConsultarOperadorGestor(string login)
        {
            return OperadorDao.ConsultarOperadorGestor(login);
        }

        /// <summary> 
        /// Consulta todos Operadores do Gestor
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<OperadorInfo> ConsultarOperadorGestorLider(string login)
        {
            return OperadorDao.ConsultarOperadorGestorLider(login);
        }

        /// <summary> 
        /// Consulta todos Lideres do Sistema
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<OperadorInfo> ConsultarOperadorLider()
        {
            return OperadorDao.ConsultarOperadorLider();
        }

        /// <summary> 
        /// Consulta todos Grupos do TopDesk do Operador
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<OperadorInfo> ConsultarGruposOperador(string login)
        {
            return OperadorDao.ConsultarGruposOperador(login);
        }
    }
}