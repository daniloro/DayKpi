using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class RelatorioBo
    {
        private static readonly RelatorioDao RelatorioDao = new RelatorioDao();

        #region " Singleton "

        private static RelatorioBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static RelatorioBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new RelatorioBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Consulta Backlog do Periodo
        /// </summary>
        /// <param name="login">login</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<RelBacklogInfo> ConsultarBacklog(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaConcluido = RelatorioDao.ConsultarBacklog(login, dataInicio, dataFim);            
            listaConcluido = listaConcluido.Select(p => { p.DscPlanejador = p.TipoChamado != "Atividade" ? p.TipoChamado : string.IsNullOrEmpty(p.DscPlanejador) ? "Outros" : p.DscPlanejador; return p; }).ToList();

            return listaConcluido;
        }

        /// <summary> 
        /// Consulta Chart Backlog do Periodo
        /// </summary>
        /// <param name="login">login</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<ChartQuatroInfo> ConsultarBacklogChart(string login, DateTime dataInicio, DateTime dataFim)
        {
            ChartQuatroInfo relatorio;
            var listaRelatorio = new List<ChartQuatroInfo>();
            string mes;
            var listaBacklog = RelatorioDao.ConsultarBacklog(login, dataInicio, dataFim);

            for (int i = 1; i <= 12; i++)
            {
                mes = "Mes" + i.ToString();

                if (dataInicio.AddMonths(i) > DateTime.Today) // Mês atual
                    dataInicio = DateTime.Today.AddMonths(-12);

                relatorio = new ChartQuatroInfo
                {
                    Item1 = listaBacklog.Count(p => p.DataAbertura < dataInicio.AddMonths(i) && p.DataAbertura >= dataInicio.AddMonths(i - 1) && (p.CodStatus == 0 || p.DataConclusao >= dataInicio.AddMonths(i))), //1
                    Item2 = listaBacklog.Count(p => p.DataAbertura < dataInicio.AddMonths(i - 1) && p.DataAbertura >= dataInicio.AddMonths(i - 2) && (p.CodStatus == 0 || p.DataConclusao >= dataInicio.AddMonths(i))), //30
                    Item3 = listaBacklog.Count(p => p.DataAbertura < dataInicio.AddMonths(i - 2) && p.DataAbertura >= dataInicio.AddMonths(i - 3) && (p.CodStatus == 0 || p.DataConclusao >= dataInicio.AddMonths(i))), //60
                    Item4 = listaBacklog.Count(p => p.DataAbertura < dataInicio.AddMonths(i - 3) && (p.CodStatus == 0 || p.DataConclusao >= dataInicio.AddMonths(i))) //90
                };
                listaRelatorio.Add(relatorio);
            }
            return listaRelatorio;
        }

        /// <summary> 
        /// Consulta dos Chamados em Fila
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<RelEmFilaInfo> ConsultarEmFila(string login, DateTime dataInicio, DateTime dataFim)
        {
            return RelatorioDao.ConsultarEmFila(login, dataInicio, dataFim);
        }

        /// <summary> 
        /// Consulta EmFila do Periodo
        /// </summary>
        /// <param name="listaEmFila">Lista de Atividades na fila</param>
        /// <param name="dataFim">Fim consulta</param>
        /// <returns></returns> 
        /// <remarks></remarks>
        public List<ChartQuatroInfo> ConsultarEmFilaChart(List<RelEmFilaInfo> listaEmFila, DateTime dataFim)
        {
            ChartQuatroInfo relatorio;
            var listaRelatorio = new List<ChartQuatroInfo>();

            foreach (var grupo in listaEmFila.Select(p => p.Grupo).Distinct().ToList())
            {
                if (dataFim > DateTime.Today) // Mês atual
                    dataFim = DateTime.Today;

                relatorio = new ChartQuatroInfo
                {
                    Descricao = grupo,
                    Item1 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura >= dataFim.AddMonths(-1)), //1
                    Item2 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim.AddMonths(-1) && p.DataAbertura >= dataFim.AddMonths(-2)), //30
                    Item3 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim.AddMonths(-2) && p.DataAbertura >= dataFim.AddMonths(-3)), //60
                    Item4 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim.AddMonths(-3)) //90
                };
                listaRelatorio.Add(relatorio);
            }            

            return listaRelatorio;
        }

        /// <summary> 
        /// Consulta Chamados em Fila dos últimos 6 Meses
        /// </summary>
        /// <param name="login">l</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<ChartSeisInfo> ConsultarEmFilaSeisMesesChart(string login, DateTime dataInicio, DateTime dataFim)
        {
            ChartSeisInfo relatorio;
            var listaRelatorio = new List<ChartSeisInfo>();

            var listaEmFila = ConsultarEmFila(login, dataInicio, dataFim);

            foreach (var grupo in listaEmFila.Select(p => p.Grupo).Distinct().ToList())
            {
                relatorio = new ChartSeisInfo
                {
                    Descricao = grupo,
                    Item1 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim.AddMonths(-5) && (p.CodStatus == 0 || p.DataConclusao >= dataFim.AddMonths(-5))), // Mes1
                    Item2 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim.AddMonths(-4) && (p.CodStatus == 0 || p.DataConclusao >= dataFim.AddMonths(-4))), // Mes2
                    Item3 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim.AddMonths(-3) && (p.CodStatus == 0 || p.DataConclusao >= dataFim.AddMonths(-3))), // Mes3
                    Item4 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim.AddMonths(-2) && (p.CodStatus == 0 || p.DataConclusao >= dataFim.AddMonths(-2))), // Mes4
                    Item5 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim.AddMonths(-1) && (p.CodStatus == 0 || p.DataConclusao >= dataFim.AddMonths(-1))), // Mes5
                    Item6 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim && (p.CodStatus == 0 || p.DataConclusao >= dataFim)) // Mes6
                };
                listaRelatorio.Add(relatorio);
            }
            return listaRelatorio;
        }


        /// <summary> 
        /// Consulta EmFila do Periodo
        /// </summary>
        /// <param name="grupo">grupo</param>
        /// <param name="dataInicio">Inicio consulta</param>
        /// <param name="dataFim">Fim consulta</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<RelEmFilaInfo> ConsultarEmFilaGrupo(string grupo, DateTime dataInicio, DateTime dataFim)
        {
            return RelatorioDao.ConsultarEmFilaGrupo(grupo, dataInicio, dataFim);
        }

        /// <summary> 
        /// Consulta EmFila do Periodo
        /// </summary>
        /// <param name="grupo">grupo</param>
        /// <param name="dataConsulta">Data da Consulta</param>
        /// <returns></returns> 
        /// <remarks></remarks>
        public List<ChartQuatroInfo> ConsultarEmFilaGrupoChart(string grupo, DateTime dataConsulta)
        {
            var dataInicio = dataConsulta.AddMonths(-11);
            var dataFim = dataConsulta.AddMonths(1);

            ChartQuatroInfo relatorio;
            var listaRelatorio = new List<ChartQuatroInfo>();
            string mes;
            var listaEmFila = ConsultarEmFilaGrupo(grupo, dataInicio, dataFim);
            List<RelEmFilaInfo> listaAux;

            for (int i = 1; i <= 12; i++)
            {
                mes = "Mes" + i.ToString();
                listaAux = listaEmFila.FindAll(p => p.CodStatus == 0 || p.DataConclusao >= dataInicio.AddMonths(i));

                if (dataInicio.AddMonths(i) > DateTime.Today) // Mês atual
                    dataInicio = DateTime.Today.AddMonths(-12);                

                relatorio = new ChartQuatroInfo
                {
                    Item1 = listaAux.Count(p => p.DataAbertura < dataInicio.AddMonths(i) && p.DataAbertura >= dataInicio.AddMonths(i - 1)), //1
                    Item2 = listaAux.Count(p => p.DataAbertura < dataInicio.AddMonths(i - 1) && p.DataAbertura >= dataInicio.AddMonths(i - 2)), //30
                    Item3 = listaAux.Count(p => p.DataAbertura < dataInicio.AddMonths(i - 2) && p.DataAbertura >= dataInicio.AddMonths(i - 3)), //60
                    Item4 = listaAux.Count(p => p.DataAbertura < dataInicio.AddMonths(i - 3)) //90
                };
                listaRelatorio.Add(relatorio);
            }
            return listaRelatorio;
        }

        /// <summary> 
        /// Consulta Atividades Concluidas por Operador
        /// </summary>
        /// <param name="login">Login do Operador</param>
        /// <param name="dataInicio">Inicio consulta</param>
        /// <param name="dataFim">Fim consulta</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<RelConcluidoOperadorInfo> ConsultarConcluidoOperador(string login, DateTime dataInicio, DateTime dataFim)
        {
            return RelatorioDao.ConsultarConcluidoOperador(login, dataInicio, dataFim);
        }

        /// <summary> 
        /// Consulta Atividades Concluidas dos últimos Meses do Operador
        /// </summary>
        /// <param name="login">Login do Operador</param>
        /// <param name="dataConsulta">Data da Consulta</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<ChartQuatroInfo> ConsultarConcluidoOperadorChart(string login, DateTime dataConsulta)
        {
            var dataInicio = dataConsulta.AddMonths(-11);
            var dataFim = dataConsulta.AddMonths(1);

            ChartQuatroInfo relatorio;
            var listaRelatorio = new List<ChartQuatroInfo>();
            string mes;
            var listaConcluido = ConsultarConcluidoOperador(login, dataInicio, dataFim);
            List<RelConcluidoOperadorInfo> listaAux;

            for (int i = 1; i <= 12; i++)
            {
                mes = "Mes" + i.ToString();
                listaAux = listaConcluido.FindAll(p => p.DataConclusao < dataInicio.AddMonths(i) && p.DataConclusao >= dataInicio.AddMonths(i - 1));

                relatorio = new ChartQuatroInfo
                {                    
                    Item1 = listaAux.Count(p => p.DataAbertura < dataInicio.AddMonths(i) && p.DataAbertura >= dataInicio.AddMonths(i - 1)), //1
                    Item2 = listaAux.Count(p => p.DataAbertura < dataInicio.AddMonths(i - 1) && p.DataAbertura >= dataInicio.AddMonths(i - 2)), //30
                    Item3 = listaAux.Count(p => p.DataAbertura < dataInicio.AddMonths(i - 2) && p.DataAbertura >= dataInicio.AddMonths(i - 3)), //60
                    Item4 = listaAux.Count(p => p.DataAbertura < dataInicio.AddMonths(i - 3)) //90
                };
                listaRelatorio.Add(relatorio);
            }
            return listaRelatorio;
        }

        /// <summary> 
        /// Consulta o número de chamados concluidos por categoria em um determinado periodo
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<RelConcluidoConsolidadoInfo> ConsultarConcluidoConsolidado(string login, DateTime dataInicio, DateTime dataFim)
        {
            return RelatorioDao.ConsultarConcluidoConsolidado(login, dataInicio, dataFim);
        }

        /// <summary> 
        /// Consulta os chamados concluidos da equipe em um determinado periodo
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<RelConcluidoInfo> ConsultarConcluido(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaConcluido = RelatorioDao.ConsultarConcluido(login, dataInicio, dataFim);
            listaConcluido = listaConcluido.Select(p => { p.DscPlanejador = p.TipoChamado != "Atividade" ? p.TipoChamado : string.IsNullOrEmpty(p.DscPlanejador) ? "Outros" : p.DscPlanejador; return p; }).ToList();
            return listaConcluido;
        }

        /// <summary> 
        /// Consulta o Total de chamados abertos e concluidos nos últimos 3 meses
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<RelAbertoConcluidoInfo> ConsultarAbertoConcluido(string login, DateTime dataConsulta)
        {
            var dataInicio = dataConsulta.AddMonths(-2);
            var dataFim = dataConsulta.AddMonths(1);

            RelAbertoConcluidoInfo relatorio;
            var listaRelatorio = new List<RelAbertoConcluidoInfo>();
            
            var listaEmFila = ConsultarEmFila(login, dataInicio, dataFim);

            foreach (var grupo in listaEmFila.Select(p => p.Grupo).Distinct().ToList())
            {
                relatorio = new RelAbertoConcluidoInfo
                {       
                    Grupo = grupo,
                    AbertoMes1 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim.AddMonths(-2) && p.DataAbertura >= dataFim.AddMonths(-3)),
                    ConcluidoMes1 = listaEmFila.Count(p => p.Grupo == grupo && p.DataConclusao < dataFim.AddMonths(-2) && p.DataConclusao >= dataFim.AddMonths(-3) && p.CodStatus == 1),
                    CanceladoMes1 = listaEmFila.Count(p => p.Grupo == grupo && p.DataConclusao < dataFim.AddMonths(-2) && p.DataConclusao >= dataFim.AddMonths(-3) && p.CodStatus == 2),
                    EmFilaMes1 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim.AddMonths(-2) && (p.CodStatus == 0 || p.DataConclusao >= dataFim.AddMonths(-2))),
                    AbertoMes2 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim.AddMonths(-1) && p.DataAbertura >= dataFim.AddMonths(-2)),
                    ConcluidoMes2 = listaEmFila.Count(p => p.Grupo == grupo && p.DataConclusao < dataFim.AddMonths(-1) && p.DataConclusao >= dataFim.AddMonths(-2) && p.CodStatus == 1),
                    CanceladoMes2 = listaEmFila.Count(p => p.Grupo == grupo && p.DataConclusao < dataFim.AddMonths(-1) && p.DataConclusao >= dataFim.AddMonths(-2) && p.CodStatus == 2),
                    EmFilaMes2 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim.AddMonths(-1) && (p.CodStatus == 0 || p.DataConclusao >= dataFim.AddMonths(-1))),
                    AbertoMes3 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim && p.DataAbertura >= dataFim.AddMonths(-1)),
                    ConcluidoMes3 = listaEmFila.Count(p => p.Grupo == grupo && p.DataConclusao < dataFim && p.DataConclusao >= dataFim.AddMonths(-1) && p.CodStatus == 1),
                    CanceladoMes3 = listaEmFila.Count(p => p.Grupo == grupo && p.DataConclusao < dataFim && p.DataConclusao >= dataFim.AddMonths(-1) && p.CodStatus == 2),
                    EmFilaMes3 = listaEmFila.Count(p => p.Grupo == grupo && p.DataAbertura < dataFim && (p.CodStatus == 0 || p.DataConclusao >= dataFim)),
                };
                listaRelatorio.Add(relatorio);
            }

            listaRelatorio.RemoveAll(p => p.AbertoMes1 + p.AbertoMes2 + p.AbertoMes3 + p.ConcluidoMes1 + p.ConcluidoMes2 + p.ConcluidoMes3 + p.CanceladoMes1 + p.CanceladoMes2 + p.CanceladoMes3 == 0);

            return listaRelatorio;
        }

        /// <summary> 
        /// Consulta o Total de chamados abertos e concluidos dos últimos 6 Meses do Grupo
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<ChartQuatroInfo> ConsultarAbertoConcluidoGrupoChart(string grupo, DateTime dataConsulta)
        {
            var dataInicio = dataConsulta.AddMonths(-5);
            var dataFim = dataConsulta.AddMonths(1);

            ChartQuatroInfo relatorio;
            var listaRelatorio = new List<ChartQuatroInfo>();
            string mes;
            var listaEmFila = ConsultarEmFilaGrupo(grupo, dataInicio, dataFim);

            for (int i = 1; i <= 6; i++)
            {
                mes = "Mes" + i.ToString();

                relatorio = new ChartQuatroInfo
                {
                    Item1 = listaEmFila.Count(p => p.DataAbertura < dataInicio.AddMonths(i) && p.DataAbertura >= dataInicio.AddMonths(i - 1)), // Aberto
                    Item2 = listaEmFila.Count(p => p.DataConclusao < dataInicio.AddMonths(i) && p.DataConclusao >= dataInicio.AddMonths(i - 1) && p.CodStatus == 1), // Concluido
                    Item3 = listaEmFila.Count(p => p.DataConclusao < dataInicio.AddMonths(i) && p.DataConclusao >= dataInicio.AddMonths(i - 1) && p.CodStatus == 2), // Cancelado
                    Item4 = listaEmFila.Count(p => p.DataAbertura < dataInicio.AddMonths(i) && (p.CodStatus == 0 || p.DataConclusao >= dataInicio.AddMonths(i))) // Em Fila
                };
                listaRelatorio.Add(relatorio);
            }
            return listaRelatorio;
        }         

        /// <summary> 
        /// Consulta o ponto mensal da Equipe. Horas Extras.
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="dataInicio">Data Inicial</param>
        /// <param name="dataFim">Data Final</param>
        /// <returns></returns> 
        /// <remarks></remarks>
        public List<RelPontoMensalInfo> ConsultarPontoMensal(string login, DateTime dataInicio, DateTime dataFim, string grupo)
        {
            var listaRelatorio = RelatorioDao.ConsultarPontoMensal(login, dataInicio, dataFim);

            if (grupo != "" && grupo != "0")
                listaRelatorio.RemoveAll(p => p.Grupo != grupo);

            return listaRelatorio;
        }

        /// <summary> 
        /// Consulta o status de todos chamados que teve atuação
        /// </summary>
        /// <param name="login">login</param>
        /// <returns></returns> 
        /// <remarks></remarks>
        public List<RelMeusChamadosInfo> ConsultarMeusChamados(string login)
        {
            return RelatorioDao.ConsultarMeusChamados(login);
        }

        /// <summary> 
        /// Consulta o Resumo de Preenchimento do KPI Mensal
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<RelResumoKpiInfo> ConsultarResumoKpi(DateTime dataConsulta, string login, int tipoOperador)
        {
            return RelatorioDao.ConsultarResumoKpi(dataConsulta, login, tipoOperador);
        }
    }
}