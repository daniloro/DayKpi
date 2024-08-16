using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.UserControl.Chart
{
    public partial class OrgChart : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CarregarOrgChart(List<OrganogramaInfo> listaOrganograma)
        {
            var retorno = OrganogramaBo.GetInstance.ConsultarOrganogramaJson(listaOrganograma);

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptOrganograma", $"orgChart = " + retorno + "; google.charts.setOnLoadCallback(drawOrgChart);", true);
        }
    }
}