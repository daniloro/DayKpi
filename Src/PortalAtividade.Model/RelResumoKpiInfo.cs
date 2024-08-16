using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAtividade.Model
{
    public class RelResumoKpiInfo
    {
        public string LoginAd { get; set; }
        public string Nome { get; set; }
        public int CodTipoEquipe { get; set; }
        public bool Objetivo { get; set; }
        public bool Organograma { get; set; }
        public bool Meta { get; set; }
        public bool Backlog { get; set; }
        public int QtdEmFila { get; set; }        
        public int QtdAbertoConcluido { get; set; }
        public int QtdAtendimento { get; set; }
        public bool Gmud { get; set; }
        public int QtdPerformance { get; set; }
        public int QtdHoraExtra { get; set; }
        public int QtdVencido { get; set; }
    }
}