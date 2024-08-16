
namespace PortalAtividade.Model
{
    public class RelPontoMensalInfo
    {
        public string LoginAd { get; set; }
        public string Grupo { get; set; }
        public string Nome { get; set; }

        public int HEUltimoDia { get; set; }
        public int HEMensal { get; set; }
        public int PontoPendente { get; set; }
        public int PontoIncompleto { get; set; }
        public int PontoManual { get; set; }
    }
}