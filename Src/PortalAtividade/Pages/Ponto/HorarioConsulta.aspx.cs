using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Business;
using PortalAtividade.Model;

namespace PortalAtividade.Pages.Ponto
{
    public partial class HorarioConsulta : BasePage
    {
        private enum ColPonto
        {
            Nome = 0,
            Inicio,
            Almoco,
            Retorno,
            Saida
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                CarregarPonto();
            }
        }

        private void CarregarPonto()
        {
            var listaPonto = PontoBo.GetInstance.ConsultarPonto(UsuarioAD, DateTime.Today);

            gdvPonto.DataSource = listaPonto;
            gdvPonto.DataBind();
        }

        protected void GdvPonto_PreRender(object sender, EventArgs e)
        {
            if (gdvPonto.Rows.Count > 0)
            {
                gdvPonto.UseAccessibleHeader = true;
                gdvPonto.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvPonto.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvPonto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PontoInfo ponto = (PontoInfo)e.Row.DataItem;

                e.Row.Font.Bold = ponto.HomeOffice;

                e.Row.Cells[(int)ColPonto.Nome].Text = ponto.Nome;
                e.Row.Cells[(int)ColPonto.Inicio].Text = ponto.HoraInicio == DateTime.MinValue ? "" : ponto.HoraInicio.ToString("hh:mm") + "h";
                e.Row.Cells[(int)ColPonto.Almoco].Text = ponto.HoraAlmoco == DateTime.MinValue ? "" : ponto.HoraAlmoco.ToString("hh:mm") + "h";
                e.Row.Cells[(int)ColPonto.Retorno].Text = ponto.HoraRetorno == DateTime.MinValue ? "" : ponto.HoraRetorno.ToString("hh:mm") + "h";
                e.Row.Cells[(int)ColPonto.Saida].Text = ponto.HoraSaida == DateTime.MinValue ? "" : ponto.HoraSaida.ToString("hh:mm") + "h";
            }
        }
    }
}