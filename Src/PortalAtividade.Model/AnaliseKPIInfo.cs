using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAtividade.Model
{
    public class AnaliseKPIInfo
    {
        public int CodAnalise { get; set; }
        public DateTime DataAnalise { get; set; }
        public string Grupo { get; set; }
        public int CodPergunta { get; set; }
        public string DscPergunta { get; set; }
        public string DscAnalise { get; set; }
        public string LoginAd { get; set; }
        public string LoginOperador { get; set; }
        public bool Concluido { get; set; }        
    }
}