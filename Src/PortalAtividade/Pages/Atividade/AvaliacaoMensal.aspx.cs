using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Business;
using PortalAtividade.Model;

namespace PortalAtividade.Pages.Atividade
{
    public partial class AvaliacaoMensal : BasePage
    {
        private enum ColAtividade
        {
            NroChamado = 0,
            NroAtividade,
            DscChamado,
            DscPlanejador,            
            DataConclusao,
            Avaliacao,
            Ponderacao            
        }

        private enum ColAndamento
        {
            NroChamado = 0,
            NroAtividade,
            DscChamado,
            DscPlanejador,
            DataConclusao,
            QtdConfirmacao
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ucFiltro.CarregarFiltro(UsuarioAD, Usuario.TipoOperador);

                CarregarAtividadePonderacaoMensal();                
            }
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {
            CarregarAtividadePonderacaoMensal();
        }        

        private void CarregarAtividadePonderacaoMensal()
        {
            if (CurrentSession.Filtro is AvaliacaoMensalFiltroInfo filtro)
            {
                ucFiltro.Mes = filtro.DataAvaliacao.ToString("dd/MM/yyyy");
                ucFiltro.Operador = filtro.LoginAd;                             

                CurrentSession.Filtro = null;
            }

            var loginAd = Usuario.TipoOperador == (int)TiposInfo.TipoOperador.Analista ? UsuarioAD : ucFiltro.Operador;
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);

            var listaAtividade = AvaliacaoAtividadeBo.GetInstance.ConsultarAtividadePonderacaoMes(loginAd, dataConsulta, dataConsulta.AddMonths(1));
            gdvConcluido.Columns[(int)ColAtividade.Ponderacao].FooterText = listaAtividade.Sum(p => p.PonderacaoTotal).ToString();
            gdvConcluido.DataSource = listaAtividade;
            gdvConcluido.DataBind();

            if (dataConsulta.Month == DateTime.Now.Month)
            {
                phAndamento.Visible = true;

                var listaAndamento = AtividadeBo.GetInstance.ConsultarAtividadeAtual(loginAd, PlanejadorBo.GetInstance.ObterPlanejadorPrincipal(Usuario.CodTipoEquipe));
                gdvAndamento.DataSource = listaAndamento;
                gdvAndamento.DataBind();
            }
            else
            {
                phAndamento.Visible = false;
            }            
        }

        protected void GdvConcluido_PreRender(object sender, EventArgs e)
        {
            if (gdvConcluido.Rows.Count > 0)
            {
                gdvConcluido.UseAccessibleHeader = true;
                gdvConcluido.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvConcluido.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvConcluido_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                AvaliacaoAtividadeInfo atividade = (AvaliacaoAtividadeInfo)e.Row.DataItem;

                e.Row.Cells[(int)ColAtividade.NroChamado].Text = atividade.NroChamado;
                e.Row.Cells[(int)ColAtividade.NroAtividade].Text = atividade.NroAtividade;
                e.Row.Cells[(int)ColAtividade.DscChamado].Text = atividade.DscChamado;
                e.Row.Cells[(int)ColAtividade.DscPlanejador].Text = atividade.DscPlanejador;
                e.Row.Cells[(int)ColAtividade.DataConclusao].Text = atividade.DataConclusao.ToString("dd/MM/yyyy");                
                e.Row.Cells[(int)ColAtividade.Ponderacao].Text = atividade.PonderacaoTotal.ToString();
                ((LinkButton)e.Row.FindControl("btnEditar")).CommandArgument = atividade.NroAtividade;

                if (atividade.CodAvaliacao > 0)
                    ((Image)e.Row.FindControl("imgVisto")).Visible = true;
                else if (atividade.CodAvaliacaoAuto > 0)
                    ((Image)e.Row.FindControl("imgWait")).Visible = true;
                else
                    ((Image)e.Row.FindControl("imgCheck")).Visible = true;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[(int)ColAtividade.Avaliacao].Text = "Total";                
            }
        }

        protected void GdvAndamento_PreRender(object sender, EventArgs e)
        {
            if (gdvAndamento.Rows.Count > 0)
            {
                gdvAndamento.UseAccessibleHeader = true;
                gdvAndamento.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvAndamento.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvAndamento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                AtividadeInfo atividade = (AtividadeInfo)e.Row.DataItem;

                e.Row.Cells[(int)ColAndamento.NroChamado].Text = atividade.NroChamado;
                e.Row.Cells[(int)ColAndamento.NroAtividade].Text = atividade.NroAtividade;
                e.Row.Cells[(int)ColAndamento.DscChamado].Text = atividade.DscChamado;
                e.Row.Cells[(int)ColAndamento.DscPlanejador].Text = atividade.DscAtividade;
                e.Row.Cells[(int)ColAndamento.DataConclusao].Text = atividade.DataFinal.ToString("dd/MM/yyyy");
                e.Row.Cells[(int)ColAndamento.QtdConfirmacao].Text = atividade.QtdConfirmacao.ToString();                
            }            
        }

        protected void EditarAtividade(object sender, CommandEventArgs e)
        {
            string nroAtividade = e.CommandArgument.ToString();

            var filtro = new AvaliacaoMensalFiltroInfo
            {
                DataAvaliacao = Convert.ToDateTime(ucFiltro.Mes),
                LoginAd = ucFiltro.Operador,
                NroAtividade = nroAtividade
            };
            CurrentSession.Filtro = filtro;

            Response.Redirect("~/Pages/Atividade/AvaliacaoAtividade.aspx", false);            
        }
    }
}