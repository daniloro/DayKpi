using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAtividade.Model
{
    public class PontoInfo
    {
        public int CodPonto { get; set; }
        public string LoginAd { get; set; }
        public DateTime DataPonto { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraAlmoco { get; set; }
        public DateTime HoraRetorno { get; set; }
        public DateTime HoraSaida { get; set; }
    }
}
