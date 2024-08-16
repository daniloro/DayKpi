using System;

namespace PortalAtividade.Model
{
    public class AvaliacaoAtividadeInfo
    {        
        public string NroChamado { get; set; }
        public string DscChamado { get; set; }
        public string NroAtividade { get; set; }
        public string DscAtividade { get; set; }
        public string Operador { get; set; }
        public string Analista { get; set; }
        public DateTime DataConclusao { get; set; }
        public string LoginAd { get; set; }
        public string Grupo { get; set; }        
        public int Complexidade { get; set; }
        public int NotaQualidade { get; set; }
        public int NotaPerformance { get; set; }
        public int NotaPo { get; set; }        
        public int NotaEspec { get; set; }
        public int NotaNegocio { get; set; }
        public string Observacao { get; set; }
        public int TipoAvaliacao { get; set; } //1 - Desenvolvedor, 2 - Lider
    }
}