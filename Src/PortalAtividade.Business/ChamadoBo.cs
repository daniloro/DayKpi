using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class ChamadoBo
    {
        private static readonly ChamadoDao ChamadoDao = new ChamadoDao();

        #region " Singleton "

        private static ChamadoBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static ChamadoBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new ChamadoBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Obtem os dados do Chamado
        /// </summary> 
        /// <param name="nroChamado">Nro do Chamado</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public ChamadoInfo ObterChamado(string nroChamado)
        {
            return ChamadoDao.ObterChamado(nroChamado);
        }

        /// <summary> 
        /// Consulta os chamados relevantes do mês
        /// </summary> 
        /// <param name="login">Login</param> 
        /// <param name="dataConsulta">Data do mes de Consulta</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<ChamadoInfo> ConsultarChamadoRelevanteMes(string login, DateTime dataConsulta)
        {
            return ChamadoDao.ConsultarChamadoRelevanteMes(login, dataConsulta);
        }

        /// <summary> 
        /// Consulta os chamados relevantes pendentes
        /// </summary> 
        /// <param name="login">Login</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<ChamadoInfo> ConsultarChamadoRelevantePendente(string login)
        {
            return ChamadoDao.ConsultarChamadoRelevantePendente(login);
        }

        /// <summary> 
        /// Verifica se o chamado possui alguma atividade com o grupo específico
        /// </summary> 
        /// <param name="nroChamado">Nro do Chamado</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public ChamadoInfo ObterChamadoPossuiGrupo(string nroChamado, string login)
        {
            return ChamadoDao.ObterChamadoPossuiGrupo(nroChamado, login);
        }

        /// <summary> 
        /// Altera a data Final da Atividade
        /// </summary> 
        /// <param name="nroChamado">Nro do chamado</param>
        /// <param name="relevante">Indicador Importante</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarChamadoRelevante(string nroChamado, bool relevante)
        {
            ChamadoDao.AlterarChamadoRelevante(nroChamado, relevante);
        }
    }
}