using System;

namespace PortalAtividade.Model
{
    public class AtividadeInfo
    {
        public int CodHistorico { get; set; }
        public string NroChamado { get; set; }
        public string DscChamado { get; set; }
        public string NroAtividade { get; set; }
        public string DscAtividade { get; set; }
        public DateTime DataAnterior { get; set; }
        public DateTime DataFinal { get; set; }
        public string Operador { get; set; }
        public int QtdRepactuacao { get; set; }
        public int TipoLancamento { get; set; }
        public string Observacao { get; set; }
        public string NroAtividadeAnterior { get; set; }
        public string LoginAd { get; set; }
    }
}