using System;

namespace PortalAtividade.Model
{
    public class GmudInfo
    {
        public string NroGmud { get; set; }
        public string VersionId { get; set; }        
        public string NomeSistema { get; set; }
        public string Versao { get; set; }
        public string DscGmud { get; set; }
        public DateTime DataGmud { get; set; }
        public string NroChamado { get; set; }
        public string DscChamado { get; set; }
        public int QtdChamado { get; set; }
        public bool ChamadoEquipe { get; set; }
    }
}