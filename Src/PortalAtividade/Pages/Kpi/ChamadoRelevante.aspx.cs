using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Business;
using PortalAtividade.Model;

namespace PortalAtividade.Pages.Kpi
{
    public partial class ChamadoRelevante : BasePage
    {
        private enum ColChamado
        {
            NroChamado = 0,
            DscChamado,
            Categoria,
            SubCategoria,
            DataConclusao
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ucFiltro.CarregarFiltro(UsuarioAD);
            }
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {
            CarregarChamados();
        }

        private void CarregarChamados()
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var listaChamado = ChamadoBo.GetInstance.ConsultarChamadoRelevanteMes(ucFiltro.Gestor, dataConsulta);

            if (DateTime.Now.Month - dataConsulta.Month <= 1)
                listaChamado.AddRange(ChamadoBo.GetInstance.ConsultarChamadoRelevantePendente(ucFiltro.Gestor));

            gdvChamado.DataSource = listaChamado;
            gdvChamado.DataBind();
        }

        protected void GdvChamado_PreRender(object sender, EventArgs e)
        {
            if (gdvChamado.Rows.Count > 0)
            {
                gdvChamado.UseAccessibleHeader = true;
                gdvChamado.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvChamado.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvChamado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ChamadoInfo chamado = (ChamadoInfo)e.Row.DataItem;

                e.Row.Cells[(int)ColChamado.NroChamado].Text = chamado.NroChamado;
                e.Row.Cells[(int)ColChamado.DscChamado].Text = chamado.DscChamado;
                e.Row.Cells[(int)ColChamado.Categoria].Text = chamado.Categoria;
                e.Row.Cells[(int)ColChamado.SubCategoria].Text = chamado.SubCategoria;

                if (chamado.CodStatusChamado == 1)
                    e.Row.Cells[(int)ColChamado.DataConclusao].Text = chamado.DataImplementacao.ToString("dd/MM/yyyy");
                else
                    e.Row.Cells[(int)ColChamado.DataConclusao].Text = chamado.DataFinal == DateTime.MinValue ? "" : "Planejado para " + chamado.DataFinal.ToString("dd/MM/yyyy");

                LinkButton btnExcluir = (LinkButton)e.Row.FindControl("btnExcluir");
                btnExcluir.CommandArgument = chamado.NroChamado;
                btnExcluir.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja desmarcar este chamado?', " + btnExcluir.ClientID + "); return false;";
            }
        }

        protected void BtnIncluirChamado_Click(object sender, EventArgs e)
        {            
            if (txtNroChamado.Text.Length < 12)
            {
                RetornarMensagemAviso("Número de chamado inválido!");
                return;
            }

            var chamado = ChamadoBo.GetInstance.ObterChamadoPossuiGrupo(txtNroChamado.Text, UsuarioAD);

            if (chamado.NroChamado != txtNroChamado.Text)
            {
                RetornarMensagemAviso("Número de chamado não encontrado nos Grupos da equipe!");
                return;
            }

            ChamadoBo.GetInstance.AlterarChamadoRelevante(chamado.NroChamado, true);

            txtNroChamado.Text = "";
            CarregarChamados();

            if (chamado.DataImplementacao != DateTime.MinValue && chamado.DataImplementacao < Convert.ToDateTime(ucFiltro.Mes))
            {
                RetornarMensagemAviso("Número de chamado incluído para o relatório de  " + chamado.DataImplementacao.ToString("MM/yyyy"));
                return;
            }
        }

        protected void ExcluirChamadoRelevante(object sender, CommandEventArgs e)
        {            
            ChamadoBo.GetInstance.AlterarChamadoRelevante(e.CommandArgument.ToString(), false);
            CarregarChamados();
        }
    }
}