using System;

namespace PortalAtividade.Model
{
    public class RelEmFilaInfo
    {
        public string NroChamado { get; set; }
        public string DscChamado { get; set; }
        public string TipoChamado { get; set; }
        public string NroAtividade { get; set; }
        public string DscAtividade { get; set; }
        public int CodPlanejador { get; set; }
        public string Grupo { get; set; }
        public string Operador { get; set; }        
        public DateTime DataAbertura { get; set; }
        public DateTime DataFinal { get; set; }
        public DateTime? DataConclusao { get; set; }
        public int CodStatus { get; set; }
    }
}