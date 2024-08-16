using System;

namespace PortalAtividade.Model
{
    public class CheckListSistemaInfo
    {
        public int CodItem { get; set; }
        public int Tipo { get; set; }
        public DateTime DataItem { get; set; }
        public int CodCheckList { get; set; }        
        public string Sistema { get; set; }
        public string Modulo { get; set; }
        public string Descricao { get; set; }
        public string Grupo { get; set; }
        public string LoginAd { get; set; }
        public string Responsavel { get; set; }
        public int CodStatus { get; set; }        
        public string Observacao { get; set; }        
    }
}