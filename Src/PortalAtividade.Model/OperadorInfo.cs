
namespace PortalAtividade.Model
{
    public class OperadorInfo
    {
        public string LoginAd { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }        
        public int TipoOperador { get; set; }
        public int CodTipoEquipe { get; set; }
        public string Grupo { get; set; }
        public bool Ativo { get; set; }
    }
}