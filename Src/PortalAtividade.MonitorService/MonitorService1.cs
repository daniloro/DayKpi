using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using PortalAtividade.Business;

namespace PortalAtividade.MonitorService
{
    public partial class MonitorService1 : ServiceBase
    {
        /// <summary>
        /// Temporizadores das operações
        /// </summary>
        private Timer timerEmailDiario = new Timer();
        private string _source, _inicioExecucao;

        public MonitorService1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                IniciarVariaveis();
                IniciarTemporizadores();

                EventLog.WriteEntry(_source, "DEVKPI: Serviço Iniciado", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(_source, ex.ToString(), EventLogEntryType.Error);
            }
        }

        protected override void OnStop()
        {
            using (timerEmailDiario)
            {
                timerEmailDiario.Enabled = false;
            }

            EventLog.WriteEntry(_source, "DEVKPI: Serviço Parado", EventLogEntryType.Warning);
        }

        private void IniciarVariaveis()
        {
            _source = "Dayconnect"; // ConfigurationManager.AppSettings["CHAVE_EVENTVIEWER"];
            _inicioExecucao = "17:00"; // ConfigurationManager.AppSettings["INICIO_EXECUCAO"].ToString();
        }

        /// <summary>
        /// Configura os temporizadores
        /// </summary>
        private void IniciarTemporizadores()
        {
            EventLog.WriteEntry(_source, "Teste0", EventLogEntryType.Information);

            timerEmailDiario.Elapsed += new ElapsedEventHandler(OnElapsedTimeEmailDiario);
            timerEmailDiario.Interval = 1000;
            timerEmailDiario.Enabled = true;            
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {            
            var teste = ConfigurationManager.AppSettings["CHAVE_EVENTVIEWER"];

            EventLog.WriteEntry(_source, teste, EventLogEntryType.Information);
        }

        private void OnElapsedTimeEmailDiario(object source, ElapsedEventArgs e)
        {
            EventLog.WriteEntry(_source, "Teste1", EventLogEntryType.Information);

            try
            {
                timerEmailDiario.Enabled = false;

                // Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

                if (ExecutarTarefa())
                {
                    EventLog.WriteEntry(_source, "Teste2", EventLogEntryType.Information);                    
                    
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(_source, ex.ToString(), EventLogEntryType.Error);
            }
            finally
            {
                timerEmailDiario.Enabled = false; //deixar false para executar apenas 1 vez
            }
        }

        /// <summary>
        /// Retornar booleano para execução de tarefa se estiver no período de execução
        /// </summary>
        /// <returns></returns>
        private bool ExecutarTarefa()
        {
            DateTime agora = DateTime.Now;

            try
            {
                if (agora.DayOfWeek == DayOfWeek.Sunday || agora.DayOfWeek == DayOfWeek.Saturday)
                {
                    return false;
                }

                string horaAtual = agora.ToString("HH:mm");

                return DateTime.Parse(horaAtual) >= DateTime.Parse(_inicioExecucao);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(_source, ex.ToString(), EventLogEntryType.Error);
                return false;
            }
        }
    }
}
