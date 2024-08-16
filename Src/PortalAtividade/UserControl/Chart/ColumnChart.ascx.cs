using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;

namespace PortalAtividade.UserControl.Chart
{
    public partial class ColumnChart : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CarregarColumnChart(DateTime dataConsulta, List<ChartQuatroInfo> listaChart)
        {
            if (!listaChart.Any())
                return;

            int i = -listaChart.Count + 1;
            int maiorNro = 0;
            var retorno = "[";

            retorno += "[{label: 'Chamados em Fila', id: 'Fila', type: 'string'}, ";
            retorno += "{label: '0 a 30', id: 'Zero', type: 'number'}, ";
            retorno += "{label: '30 a 60', id: 'Trinta', type: 'number'}, ";
            retorno += "{label: '60 a 90', id: 'Sessenta', type: 'number'}, ";
            retorno += "{label: 'Acima de 90', id: 'Noventa', type: 'number'}], ";

            foreach (var item in listaChart)
            {
                retorno += "['" + dataConsulta.AddMonths(i).ToString("MM/yyyy") + "', ";
                retorno += item.Item1.ToString() + ", ";
                retorno += item.Item2.ToString() + ", ";
                retorno += item.Item3.ToString() + ", ";
                retorno += item.Item4.ToString() + "], ";
                i++;

                if (maiorNro < item.Item1 + item.Item2 + item.Item3 + item.Item4)
                    maiorNro = item.Item1 + item.Item2 + item.Item3 + item.Item4;
            }
            retorno = retorno.Remove(retorno.Length - 2);

            retorno += "]";

            maiorNro += 5;
            while (maiorNro % 4 != 0)
                maiorNro++;

            var escala = "[" + (maiorNro / 4).ToString() + ", " + (maiorNro / 4 * 2).ToString() + ", " + (maiorNro / 4 * 3).ToString() + ", " + maiorNro.ToString() + "]";

            hdDados.Value = retorno;
            hdEscala.Value = escala;

            ScriptManager.RegisterStartupScript(Page, this.GetType(), string.Format("ScriptColumnChart{0}", ID), string.Format("google.charts.setOnLoadCallback(drawColumnChart{0});", ID), true);
        }
    }
}