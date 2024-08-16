using System;

namespace PortalAtividade.Model
{
    public class RelConcluidoInfo
    {        
        public string Nome { get; set; }
        public string Grupo { get; set; }
        public string TipoChamado { get; set; }
        public string NroChamado { get; set; }
        public string DscChamado { get; set; }
        public string NroAtividade { get; set; }
        public string DscPlanejador { get; set; }
        public string DscAtividade { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataConclusao { get; set; }
    }
}
