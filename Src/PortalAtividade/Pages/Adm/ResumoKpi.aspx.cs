using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.Pages.Adm
{
    public partial class ResumoKpi : BasePage
    {
        private enum ColGestor
        {
            Nome = 0,
            TipoEquipe,
            Objetivo,            
            Organograma,
            Meta,
            Backlog,
            EmFila,
            AbertoConcluido,
            Atendimento,
            Performance,
            GMUD,
            HoraExtra,
            AtividadeVencida
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Usuario.TipoOperador < (int)TiposInfo.TipoOperador.Lider)
                    Response.Redirect("~/Default.aspx", false);

                ucFiltro.CarregarFiltro();
            }
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {
            CarregarResumoKpi();
        }                

        private void CarregarResumoKpi()
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);            
            var listaResumo = RelatorioBo.GetInstance.ConsultarResumoKpi(dataConsulta, UsuarioAD, Usuario.TipoOperador);

            gdvGestor.DataSource = listaResumo;
            gdvGestor.DataBind();
        }

        protected void GdvGestor_PreRender(object sender, EventArgs e)
        {
            if (gdvGestor.Rows.Count > 0)
            {
                gdvGestor.UseAccessibleHeader = true;
                gdvGestor.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvGestor.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvGestor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RelResumoKpiInfo resumo = (RelResumoKpiInfo)e.Row.DataItem;

                e.Row.Cells[(int)ColGestor.Nome].Text = resumo.Nome;
                e.Row.Cells[(int)ColGestor.TipoEquipe].Text = UtilBo.GetInstance.ObterTipoEquipe(resumo.CodTipoEquipe);
                e.Row.Cells[(int)ColGestor.Objetivo].Text = (resumo.Objetivo == true ? 0 : 1).ToString();
                e.Row.Cells[(int)ColGestor.Organograma].Text = (resumo.Organograma == true ? 0 : 1).ToString();
                e.Row.Cells[(int)ColGestor.Meta].Text = (resumo.Meta == true ? 0 : 1).ToString();
                e.Row.Cells[(int)ColGestor.Backlog].Text = (resumo.Backlog == true ? 0 : 1).ToString();
                e.Row.Cells[(int)ColGestor.EmFila].Text = resumo.QtdEmFila.ToString();
                e.Row.Cells[(int)ColGestor.AbertoConcluido].Text = resumo.QtdAbertoConcluido.ToString();
                e.Row.Cells[(int)ColGestor.Atendimento].Text = resumo.QtdAtendimento.ToString();
                e.Row.Cells[(int)ColGestor.Performance].Text = resumo.QtdPerformance.ToString();
                e.Row.Cells[(int)ColGestor.GMUD].Text = (resumo.Gmud == true ? 0 : 1).ToString();                
                e.Row.Cells[(int)ColGestor.HoraExtra].Text = resumo.QtdHoraExtra.ToString();
                e.Row.Cells[(int)ColGestor.AtividadeVencida].Text = resumo.QtdVencido.ToString();
                ((LinkButton)e.Row.FindControl("btnDetalhe")).CommandArgument = resumo.LoginAd;
            }
        }

        protected void ObterKpi(object sender, CommandEventArgs e)
        {
            FiltroLoginPeriodo filtro = new FiltroLoginPeriodo
            {
                LoginAd = e.CommandArgument.ToString(),
                DataConsulta = Convert.ToDateTime(ucFiltro.Mes)
            };
            CurrentSession.Objects = filtro;
            
            Response.Redirect("~/Pages/Adm/RelatorioKpi.aspx", false);        
        }
    }
}