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
        public int CodPlanejador { get; set; }
        public string DscPlanejador { get; set; }
        public string TipoChamado { get; set; }        
        public DateTime DataConclusao { get; set; }        
        public int Ponderacao { get; set; }        
        public int QtdConfirmacao { get; set; }
        public int QtdRepactuacao { get; set; }
        public int QtdRepactuacaoSemJustificativa { get; set; }
        public int CodAvaliacaoAuto { get; set; }
        public int CodAvaliacao { get; set; }
        public int NotaAvaliacao { get; set; }
        public int PonderacaoTotal { get; set; }
        public string ObservacaoAuto { get; set; }
        public string Observacao { get; set; }
        public string LoginAd { get; set; }
        public string LoginAvaliador { get; set; }
        public int TipoAvaliacao { get; set; }
        public string DestaqueGestor { get; set; }
    }
}