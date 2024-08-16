using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAtividade.Model
{
    public class PontoMensalInfo
    {
		public int CodPontoMensal { get; set; }
		public DateTime DataPonto { get; set; }
		public string Nome { get; set; }
		public string LoginAd { get; set; }
		public string LoginGestor { get; set; }
		public int Atraso { get; set; }
		public int CinquentaPorcento { get; set; }
		public int CemPorcento { get; set; }
		public int Noturno { get; set; }
		public DateTime DataAtualizacao { get; set; }
		public string Observacao { get; set; }		
    }
}
