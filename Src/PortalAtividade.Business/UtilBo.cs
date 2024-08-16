using System;
using System.Globalization;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class UtilBo
    {
        private static readonly UtilDao UtilDao = new UtilDao();

        #region " Singleton "

        private static UtilBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static UtilBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new UtilBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Obtem a Data Hora de Atualização do Sistema
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public DateTime ObterDataAtualizacao()
        {
            return UtilDao.ObterDataAtualizacao();
        }        

        /// <summary> 
        /// Obtem o Nome do Mês selecionado
        /// </summary> 
        /// <param name="mes">Número do Mes</param> 
        /// <returns></returns> 
        /// <remarks></remarks>
        public string ObterNomeMes(int mes)
        {
            var nomeMes = "";

            switch (mes)
            {
                case 1:
                    nomeMes = "Janeiro";
                    break;
                case 2:
                    nomeMes = "Fevereiro";
                    break;
                case 3:
                    nomeMes = "Março";
                    break;
                case 4:
                    nomeMes = "Abril";
                    break;
                case 5:
                    nomeMes = "Maio";
                    break;
                case 6:
                    nomeMes = "Junho";
                    break;
                case 7:
                    nomeMes = "Julho";
                    break;
                case 8:
                    nomeMes = "Agosto";
                    break;
                case 9:
                    nomeMes = "Setembro";
                    break;
                case 10:
                    nomeMes = "Outubro";
                    break;
                case 11:
                    nomeMes = "Novembro";
                    break;
                case 12:
                    nomeMes = "Dezembro";
                    break;
            }
            return nomeMes;
        }

        /// <summary> 
        /// Obtem o Nome da Semana selecionada
        /// </summary> 
        /// <param name="semana">Número da Semana</param> 
        /// <returns></returns> 
        /// <remarks></remarks>
        public string ObterNomeSemana(int semana)
        {
            var nomeSemana = "";

            switch (semana)
            {
                case 0:
                    nomeSemana = "Dom";
                    break;
                case 1:
                    nomeSemana = "Seg";
                    break;
                case 2:
                    nomeSemana = "Ter";
                    break;
                case 3:
                    nomeSemana = "Qua";
                    break;
                case 4:
                    nomeSemana = "Qui";
                    break;
                case 5:
                    nomeSemana = "Sex";
                    break;
                case 6:
                    nomeSemana = "Sab";
                    break;                
            }
            return nomeSemana;
        }

        /// <summary> 
        /// Obtem a Segunda-Feira da Semana
        /// </summary> 
        /// <param name="dataReferencia">Data de Referencia</param> 
        /// <returns></returns> 
        /// <remarks></remarks>
        public DateTime ObterDataInicioSemana(DateTime dataReferencia)
        {            
            var diaSemana = dataReferencia.DayOfWeek;

            if (diaSemana == DayOfWeek.Tuesday)
                dataReferencia = dataReferencia.AddDays(-1);
            else if (diaSemana == DayOfWeek.Wednesday)
                dataReferencia = dataReferencia.AddDays(-2);
            else if (diaSemana == DayOfWeek.Thursday)
                dataReferencia = dataReferencia.AddDays(-3);
            else if (diaSemana == DayOfWeek.Friday)
                dataReferencia = dataReferencia.AddDays(-4);
            else if (diaSemana == DayOfWeek.Saturday)
                dataReferencia = dataReferencia.AddDays(-5);
            else if (diaSemana == DayOfWeek.Sunday)
                dataReferencia = dataReferencia.AddDays(-6);

            return dataReferencia.Date;
        }

        /// <summary> 
        /// Obtem o tempo em Horas:Minutos
        /// </summary> 
        /// <param name="minutos">Minutos</param> 
        /// <returns></returns> 
        /// <remarks></remarks>
        public string ObterTempoTotal(int minutos)
        {
            var retorno = "";
            var hora = (int)TimeSpan.FromMinutes(minutos).TotalHours;
            var minuto = TimeSpan.FromMinutes(minutos).Minutes;

            if (hora < 0 || minuto < 0)
            {
                hora *= -1;
                minuto *= -1;
                retorno += "-";
            }
            retorno += hora.ToString("00") + ":" + minuto.ToString("00");

            return retorno;
        }

        /// <summary> 
        /// Obtem a descrição do tipo da equipe
        /// </summary> 
        /// <param name="codTipoEquipe">Codigo do tipo da equipe</param> 
        /// <returns></returns> 
        /// <remarks></remarks>
        public string ObterTipoEquipe(int codTipoEquipe)
        {
            var retorno = "";

            switch (codTipoEquipe)
            {
                case (int)TiposInfo.TipoEquipe.Desenvolvimento:
                    retorno = "DEV";
                    break;
                case (int)TiposInfo.TipoEquipe.Business:
                    retorno = "Business";
                    break;
                case (int)TiposInfo.TipoEquipe.Redes:
                    retorno = "Redes";
                    break;
            }
            return retorno;
        }
    }
}