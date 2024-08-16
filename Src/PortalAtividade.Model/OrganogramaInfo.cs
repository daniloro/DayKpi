using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAtividade.Model
{
    public class OrganogramaInfo
    {
        public int Nivel { get; set; }
        public string LoginAd { get; set; }
        public string Gestor { get; set; }
        public string GestorAux { get; set; }
        public string Nome { get; set; }
        public string NomeGestor { get; set; }
        public string Grupo { get; set; }
        public int TotalEquipe { get; set; }
        public bool IndGestor { get; set; }
        public DateTime DataOrganograma { get; set; }
        public int CodAnalise { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
    }
}