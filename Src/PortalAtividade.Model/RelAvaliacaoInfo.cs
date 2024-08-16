using System;

namespace PortalAtividade.Model
{
    public class RelAvaliacaoInfo
    {
        public string NroChamado { get; set; }
        public string NroAtividade { get; set; }
        public string DscChamado { get; set; }
        public int QtdRepactuacao { get; set; }
        public DateTime DataFinal { get; set; }
        public DateTime DataConclusao { get; set; }
        public int Complexidade { get; set; }
        public int NotaPO { get; set; }
        public int NotaQualidade { get; set; }
        public int NotaPerformance { get; set; }
        public string Observacao { get; set; }
        public int ComplexidadeAuto { get; set; }
        public int NotaPOAuto { get; set; }
        public string ObservacaoAuto { get; set; }        
    }
}

