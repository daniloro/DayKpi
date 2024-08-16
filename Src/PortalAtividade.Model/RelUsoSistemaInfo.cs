
namespace PortalAtividade.Model
{
    public class RelUsoSistemaInfo
    {
        public string Nome { get; set; }
        public string Grupo { get; set; }
        public int TipoOperador { get; set; }
        public int AvaliacaoDevRealizada { get; set; }
        public int AvaliacaoDevPendente { get; set; }
        public int AvaliacaoDevAutoRealizada { get; set; }
        public int AvaliacaoDevAutoPendente { get; set; }
        public int RepactuacaoRealizada { get; set; }
        public int RepactuacaoPendente { get; set; }        
    }
}