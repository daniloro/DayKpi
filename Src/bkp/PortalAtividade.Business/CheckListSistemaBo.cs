using System.Collections.Generic;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class CheckListSistemaBo
    {
        private static readonly CheckListSistemaDao CheckListSistemaDao = new CheckListSistemaDao();

        #region " Singleton "

        private static CheckListSistemaBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static CheckListSistemaBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new CheckListSistemaBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Selecione o checklist do dia por grupo
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <param name="tipo">Manhã ou Noite</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<CheckListSistemaInfo> ConsultarCheckListSistema(string login, int tipo)
        {
            if (!string.IsNullOrEmpty(login))
                return CheckListSistemaDao.ConsultarCheckListSistema(login, tipo);
            else
                return CheckListSistemaDao.ConsultarCheckListSistemaCompleto(tipo);
        }

        /// <summary> 
        /// Inclui um novo registro no checklist diário do sistema
        /// </summary> 
        /// <param name="checklist">Dados do Checklist</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirCheckList(CheckListSistemaInfo checklist)
        {
            CheckListSistemaDao.IncluirCheckList(checklist);
        }

        /// <summary> 
        /// Altera um registro no checklist diário do sistema
        /// </summary> 
        /// <param name="checklist">Dados do Checklist</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarCheckList(CheckListSistemaInfo checklist)
        {
            CheckListSistemaDao.AlterarCheckList(checklist);
        }
    }
}