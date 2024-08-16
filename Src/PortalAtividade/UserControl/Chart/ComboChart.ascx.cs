using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;

namespace PortalAtividade.UserControl.Chart
{
    public partial class ComboChart : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CarregarComboChart(DateTime dataConsulta, List<ChartQuatroInfo> listaChart)
        {
            if (!listaChart.Any())
                return;

            int i = -listaChart.Count + 1;
            int maiorNro = 0;
            var retorno = "[";

            retorno += "[{label: 'Meses', id: 'Meses', type: 'string'}, ";
            retorno += "{label: 'Abertos', id: 'Abertos', type: 'number'}, ";
            retorno += "{label: 'Concluídos', id: 'Concluidos', type: 'number'}, ";
            retorno += "{label: 'Cancelados', id: 'Cancelados', type: 'number'}, ";
            retorno += "{label: 'Backlog', id: 'Backlog', type: 'number'}], ";

            foreach (var item in listaChart)
            {
                retorno += "['" + dataConsulta.AddMonths(i).ToString("MM/yyyy") + "', ";
                retorno += item.Item1.ToString() + ", ";
                retorno += item.Item2.ToString() + ", ";
                retorno += item.Item3.ToString() + ", ";
                retorno += item.Item4.ToString() + "], ";
                i++;

                if (maiorNro < item.Item1) maiorNro = item.Item1;
                if (maiorNro < item.Item2) maiorNro = item.Item2;
                if (maiorNro < item.Item3) maiorNro = item.Item3;
                if (maiorNro < item.Item4) maiorNro = item.Item4;
            }
            retorno = retorno.Remove(retorno.Length - 2) + "]";

            maiorNro += 5;
            while (maiorNro % 4 != 0)
                maiorNro++;

            var escala = "[" + (maiorNro / 4).ToString() + ", " + (maiorNro / 4 * 2).ToString() + ", " + (maiorNro / 4 * 3).ToString() + ", " + maiorNro.ToString() + "]";

            hdDados.Value = retorno;
            hdEscala.Value = escala;

            ScriptManager.RegisterStartupScript(Page, this.GetType(), string.Format("ScriptComboChart{0}", ID), string.Format("google.charts.setOnLoadCallback(drawComboChart{0});", ID), true);
        }
    }
}