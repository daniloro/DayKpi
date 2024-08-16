using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.UserControl
{
    public partial class AtividadeHistorico : System.Web.UI.UserControl
    {
        private enum ColHistorico
        {
            Data = 0,
            Tipo,
            Operador,
            DataFinal,
            Observacao
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CarregarHistorico(string nroAtividade)
        {
            var listaHistorico = AtividadeBo.GetInstance.ConsultarAtividadeHistorico(nroAtividade);
            gdvHistorico.DataSource = listaHistorico;
            gdvHistorico.DataBind();            
        }

        protected void GdvHistorico_PreRender(object sender, EventArgs e)
        {
            if (gdvHistorico.Rows.Count > 0)
            {
                gdvHistorico.UseAccessibleHeader = true;
                gdvHistorico.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvHistorico.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvHistorico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                AtividadeInfo atividade = (AtividadeInfo)e.Row.DataItem;

                e.Row.Cells[(int)ColHistorico.Data].Text = atividade.DataOcorrencia.ToString("dd/MM/yyyy");
                e.Row.Cells[(int)ColHistorico.Operador].Text = atividade.Operador;
                e.Row.Cells[(int)ColHistorico.DataFinal].Text = atividade.DataFinal.ToString("dd/MM/yyyy");
                e.Row.Cells[(int)ColHistorico.Observacao].Text = atividade.Observacao;
                e.Row.Cells[(int)ColHistorico.Tipo].Text = AtividadeBo.GetInstance.ObterTipoLancamento(atividade.TipoLancamento, atividade.NroAtividadeAnterior); ;
            }
        }
    }
}