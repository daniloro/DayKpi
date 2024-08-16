using System;

namespace PortalAtividade.Model
{
    public class RelMeusChamadosInfo
    {
        public string NroChamado { get; set; }
        public string DscChamado { get; set; }
        public string Grupo { get; set; }
        public string Operador { get; set; }
        public string NroAtividade { get; set; }
        public string DscPlanejador { get; set; }        
        public DateTime DataFinal { get; set; }
    }
}
