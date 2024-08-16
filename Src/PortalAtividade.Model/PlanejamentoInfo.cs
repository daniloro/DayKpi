using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAtividade.Model
{
    public class PlanejamentoInfo
    {
        public int CodPlanejamento { get; set; }
        public string LoginAd { get; set; }
        public DateTime DataPlanejamento { get; set; }
        public int CodTipoPlanejamento { get; set; }
        public string NroAtividade { get; set; }        
        public string DscPlanejamento { get; set; }
        public bool Atendido { get; set; }
    }
}