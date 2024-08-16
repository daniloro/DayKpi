using System;

namespace PortalAtividade.Model
{
    public class PontoInfo
    {
        public int CodPonto { get; set; }
        public string LoginAd { get; set; }
        public string Nome { get; set; }
        public DateTime DataPonto { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraAlmoco { get; set; }
        public DateTime HoraRetorno { get; set; }
        public DateTime HoraSaida { get; set; }
        public DateTime HoraManual { get; set; }
        public int TipoPonto { get; set; }
        public bool HoraInicioManual { get; set; }
        public bool HoraAlmocoManual { get; set; }
        public bool HoraRetornoManual { get; set; }
        public bool HoraSaidaManual { get; set; }
        public bool HomeOffice { get; set; }
    }
}