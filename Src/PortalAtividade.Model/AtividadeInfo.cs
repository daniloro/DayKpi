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
        public DateTime DataAbertura { get; set; }
        public DateTime DataConclusao { get; set; }
        public string Operador { get; set; }
        public int QtdRepactuacao { get; set; }
        public int TipoLancamento { get; set; }
        public string Observacao { get; set; }
        public string NroAtividadeAnterior { get; set; }
        public string LoginAd { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public string Grupo { get; set; }
        public int Confirmado { get; set; }
        public string TipoEntrada { get; set; }
        public int QtdConfirmacao { get; set; }
        public int CodPlanejador { get; set; }
        public string DscPlanejador { get; set; }
        public string TipoChamado { get; set; }
        public int CodStatus { get; set; }
    }
}