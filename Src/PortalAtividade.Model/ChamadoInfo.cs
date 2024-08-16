using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAtividade.Model
{
    public class ChamadoInfo
    {
        public string NroChamado { get; set; }
        public string DscChamado { get; set; }
        public string Categoria { get; set; }
        public string SubCategoria { get; set; }
        public string TipoChamado { get; set; }
        public int CodStatusChamado { get; set; }
        public DateTime DataFinal { get; set; }
        public DateTime DataImplementacao { get; set; }
    }
}