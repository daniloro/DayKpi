using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.Pages.Atividade
{
    public partial class AtividadeAndamento : BasePage
    {
        private bool habilitaEdicao;

        private enum ColAtividade
        {
            Operador = 0,
            NroChamado,
            DscChamado,
            NroAtividade,
            Tipo,
            DataFinal,
            Status
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarAtividades();
                CarregarGrupos();
            }
        }

        private void CarregarAtividades()
        {
            var listaAtividade = AtividadeBo.GetInstance.ConsultarAtividadeAndamento(UsuarioAD, PlanejadorBo.GetInstance.ObterPlanejadorPrincipal(Usuario.CodTipoEquipe));

            if (ucFiltro.Grupo != "" && ucFiltro.Grupo != "0")
            {
                listaAtividade.RemoveAll(p => p.Grupo != ucFiltro.Grupo);
            }
            CurrentSession.Objects = listaAtividade;

            ltlTotal.Text = listaAtividade.Count.ToString();
            ltlPendente.Text = listaAtividade.FindAll(p => p.Confirmado == 0).Count.ToString();

            habilitaEdicao = Usuario.TipoOperador != (int)TiposInfo.TipoOperador.Analista;

            gdvAtividade.DataSource = listaAtividade;
            gdvAtividade.DataBind();
        }

        private void CarregarGrupos()
        {
            if (CurrentSession.Objects is List<AtividadeInfo> listaAtividade)
            {
                ucFiltro.CarregarFiltro(listaAtividade.Select(p => p.Grupo).Distinct().ToList());
            }
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {
            CarregarAtividades();
        }

        protected void GdvAtividade_PreRender(object sender, EventArgs e)
        {
            if (gdvAtividade.Rows.Count > 0)
            {
                gdvAtividade.UseAccessibleHeader = true;
                gdvAtividade.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvAtividade.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvAtividade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                AtividadeInfo atividade = (AtividadeInfo)e.Row.DataItem;

                e.Row.Cells[(int)ColAtividade.Operador].Text = atividade.Operador.Contains(" ") ? atividade.Operador.Substring(0, atividade.Operador.IndexOf(" ")) : "";
                e.Row.Cells[(int)ColAtividade.NroChamado].Text = atividade.NroChamado;
                e.Row.Cells[(int)ColAtividade.DscChamado].Text = atividade.DscChamado;
                e.Row.Cells[(int)ColAtividade.NroAtividade].Text = atividade.NroAtividade;
                e.Row.Cells[(int)ColAtividade.Tipo].Text = atividade.TipoChamado == "Atividade" ? atividade.DscPlanejador : atividade.TipoChamado;
                e.Row.Cells[(int)ColAtividade.DataFinal].Text = atividade.DataFinal == DateTime.MinValue ? "" : atividade.DataFinal.ToString("dd/MM/yyyy");

                if (atividade.Confirmado == 1)
                {
                    ((Image)e.Row.FindControl("imgVisto")).Visible = true;
                }
                else
                {
                    if (habilitaEdicao)
                    {
                        if (string.IsNullOrEmpty(atividade.NroAtividade))
                        {
                            LinkButton btnIncluir = (LinkButton)e.Row.FindControl("btnIncluir");
                            btnIncluir.Visible = true;
                            btnIncluir.CommandArgument = atividade.LoginAd;
                        }
                        else
                        {
                            if (atividade.DataFinal >= DateTime.Today)
                            {
                                LinkButton btnConfirmar = (LinkButton)e.Row.FindControl("btnConfirmar");
                                btnConfirmar.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'A data de conclusão planejada será mantida?', " + btnConfirmar.ClientID + "); return false;";
                                btnConfirmar.Visible = true;
                                btnConfirmar.CommandArgument = atividade.NroAtividade;
                            }

                            LinkButton btnAlterarData = (LinkButton)e.Row.FindControl("btnAlterarData");
                            btnAlterarData.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Irá corrigir a data de conclusão no TopDesk?', " + btnAlterarData.ClientID + "); return false;";
                            btnAlterarData.Visible = true;
                            btnAlterarData.CommandArgument = atividade.NroAtividade;

                            LinkButton btnExcluir = (LinkButton)e.Row.FindControl("btnExcluir");
                            btnExcluir.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Está atuando em outra atividade neste momento?', " + btnExcluir.ClientID + "); return false;";
                            btnExcluir.Visible = true;
                            btnExcluir.CommandArgument = atividade.NroAtividade;
                        }
                    }
                }
            }
        }        

        protected void AlterarDataAtividade(object sender, CommandEventArgs e)
        {
            string nroAtividade = e.CommandArgument.ToString();
            var atividade = AtividadeBo.GetInstance.ObterAtividadeManual(nroAtividade);

            CurrentSession.Objects = atividade;
            CurrentSession.Redirect = "~/Pages/Atividade/AtividadeAndamento.aspx";
            Response.Redirect("~/Pages/Atividade/AtividadeAlterada.aspx", false);
        }

        protected void ConfirmarAtividade(object sender, CommandEventArgs e)
        {
            string nroAtividade = e.CommandArgument.ToString();            
            AtividadeBo.GetInstance.ConfirmarAtividade(nroAtividade, 1);
            CarregarAtividades();
        }

        protected void ExcluirAtividade(object sender, CommandEventArgs e)
        {
            string nroAtividade = e.CommandArgument.ToString();
            AtividadeBo.GetInstance.ConfirmarAtividade(nroAtividade, 2);
            CarregarAtividades();
        }

        protected void IncluirAtividade(object sender, CommandEventArgs e)
        {
            CurrentSession.Objects = e.CommandArgument;
            CurrentSession.Redirect = "~/Pages/Atividade/AtividadeAndamento.aspx";
            Response.Redirect("~/Pages/Atividade/AtividadeManual.aspx", false);
        }
    }
}