using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PortalAtividade.UserControl.Chart
{
    public partial class Tabela : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CarregarTabela(string dados)
        {            
            hdDados.Value = dados;

            ScriptManager.RegisterStartupScript(Page, this.GetType(), string.Format("ScriptTabela{0}", ID), string.Format("google.charts.setOnLoadCallback(drawChartTabela{0});", ID), true);
        }
    }
}