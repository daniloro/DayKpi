using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;

namespace PortalAtividade.UserControl.Chart
{
    public partial class BarChart : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CarregarBarChart(List<ChartQuatroInfo> listaChart)
        {
            if (!listaChart.Any())
                return;

            var retorno = "[";

            retorno += "[{label: 'Chamados em Fila', id: 'Fila', type: 'string'}, ";
            retorno += "{label: '0 a 30', id: 'Zero', type: 'number'}, ";
            retorno += "{label: '30 a 60', id: 'Trinta', type: 'number'}, ";
            retorno += "{label: '60 a 90', id: 'Sessenta', type: 'number'}, ";
            retorno += "{label: 'Acima de 90', id: 'Noventa', type: 'number'}], ";

            foreach (var item in listaChart)
            {
                retorno += "['" + item.Descricao + "', " + item.Item1.ToString() + ", " + item.Item2.ToString() + ", " + item.Item3.ToString() + ", " + item.Item4.ToString() + "],";
            }
            retorno = retorno.Remove(retorno.Length - 1) + "]";

            hdDados.Value = retorno;

            ScriptManager.RegisterStartupScript(Page, this.GetType(), string.Format("ScriptBarChart{0}", ID), string.Format("google.charts.setOnLoadCallback(drawBarChart{0});", ID), true);
        }
    }
}