using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class EventoProblemaBo
    {
        private static readonly EventoProblemaDao EventoProblemaDao = new EventoProblemaDao();

        #region " Singleton "

        private static EventoProblemaBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static EventoProblemaBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new EventoProblemaBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Consulta os tipos de Evento
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<EventoProblemaInfo> ConsultarTipoEvento()
        {
            return EventoProblemaDao.ConsultarTipoEvento();
        }

        /// <summary> 
        /// Consulta os eventos do Periodo
        /// </summary> 
        /// <param name="login">Login</param> 
        /// <param name="dataInicio">Data de Inicio</param>
        /// <param name="dataFim">Data Final da Consulta</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<EventoProblemaInfo> ConsultarEvento(string login, DateTime dataInicio, DateTime dataFim)
        {
            return EventoProblemaDao.ConsultarEvento(login, dataInicio, dataFim);
        }

        /// <summary> 
        /// Obtem os dados do Evento
        /// </summary> 
        /// <param name="codEvento">Codigo do Evento</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public EventoProblemaInfo ObterEvento(int codEvento)
        {
            return EventoProblemaDao.ObterEvento(codEvento);
        }

        /// <summary> 
        /// Inclui um novo registro na tabela EventoProblema
        /// </summary> 
        /// <param name="evento">Dados do Evento</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirEvento(EventoProblemaInfo evento)
        {
            EventoProblemaDao.IncluirEvento(evento);
        }

        /// <summary> 
        /// Altera um registro na tabela EventoProblema
        /// </summary> 
        /// <param name="evento">Dados do Evento</param 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarEvento(EventoProblemaInfo evento)
        {
            EventoProblemaDao.AlterarEvento(evento);
        }
    }
}