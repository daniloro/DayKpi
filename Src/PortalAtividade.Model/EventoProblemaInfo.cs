using System;

namespace PortalAtividade.Model
{
    public class EventoProblemaInfo
    {
        public int CodEvento { get; set; }
        public int CodTipoEvento { get; set; }
        public string DscTipoEvento { get; set; }
        public int CodSistema { get; set; }
        public string NomeSistema { get; set; }
        public DateTime DataEvento { get; set; }
        public string TituloEvento { get; set; }
        public string DscEvento { get; set; }
        public string Correcao { get; set; }
        public string NroChamado { get; set; }
        public string NroCorrecao { get; set; }
        public string LoginAd { get; set; }
    }
}