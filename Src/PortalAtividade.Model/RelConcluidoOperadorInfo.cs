using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAtividade.Model
{
    public class RelConcluidoOperadorInfo
    {
        public string NroChamado { get; set; }
        public string DscChamado { get; set; }
        public string NroAtividade { get; set; } 
        public string TipoChamado { get; set; }
        public string Grupo { get; set; }
        public string Operador { get; set; }        
        public DateTime DataAbertura { get; set; }        
        public DateTime DataConclusao { get; set; }
        public int CodStatus { get; set; }
    }
}