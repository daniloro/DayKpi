using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade
{
    public partial class Atividade : BasePage
    {
        private enum ColAtividade
        {
            NroChamado = 0,
            DscChamado,
            NroAtividade,
            DscAtividade,
            QtdRepactuacao,
            DataAnterior,
            DataFinal
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarAtividades();
            }
        }

        private void CarregarAtividades()
        {
            LimparDados();

            var login = Context.User.Identity.Name;

            var listaAtividade = AtividadeBo.GetInstance.ConsultarAtividadeRepactuadaPendente(login);

            CurrentSession.Objects = listaAtividade;

            phLista.Visible = true;
            phAtividade.Visible = false;
            gdvAtividade.DataSource = listaAtividade;
            gdvAtividade.DataBind();
        }

        private void LimparDados()
        {
            ddlTipoLancamento.Enabled = true;
            txtNroAtividadeAnterior.Text = "";
            txtObservacao.Text = "";            
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

                e.Row.Cells[(int)ColAtividade.NroChamado].Text = atividade.NroChamado;
                e.Row.Cells[(int)ColAtividade.DscChamado].Text = atividade.DscChamado;
                e.Row.Cells[(int)ColAtividade.NroAtividade].Text = atividade.NroAtividade;
                e.Row.Cells[(int)ColAtividade.DscAtividade].Text = atividade.DscAtividade;
                e.Row.Cells[(int)ColAtividade.DataAnterior].Text = atividade.DataAnterior.ToString("dd/MM/yyyy");
                e.Row.Cells[(int)ColAtividade.DataFinal].Text = atividade.DataFinal.ToString("dd/MM/yyyy");
                e.Row.Cells[(int)ColAtividade.QtdRepactuacao].Text = atividade.QtdRepactuacao.ToString();
                ((LinkButton)e.Row.FindControl("btnEditar")).CommandArgument = atividade.NroAtividade;
            }
        }

        //protected void GdvAtividade_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    WebControl wc = (WebControl)e.CommandSource;
        //    GridViewRow row = (GridViewRow)wc.NamingContainer;
        //    string nroAtividade = ((LinkButton)row.FindControl("btnEditar")).CommandArgument;

        //    if (e.CommandName == "Editar")
        //    {

        //    }
        //}

        protected void EditarAtividade(object sender, CommandEventArgs e)
        {
            string nroAtividade = e.CommandArgument.ToString();

            if (CurrentSession.Objects is List<AtividadeInfo> listaAtividade)
            {
                var atividade = listaAtividade.Find(p => p.NroAtividade == nroAtividade);
                CurrentSession.Objects = atividade;

                phAtividade.Visible = true;
                phLista.Visible = false;

                ltlNroChamado.Text = atividade.NroChamado;
                ltlDscChamado.Text = atividade.DscChamado;
                ltlNroAtividade.Text = atividade.NroAtividade;
                ltlDscAtividade.Text = atividade.DscAtividade;
                ltlQtdRepactuacao.Text = atividade.QtdRepactuacao.ToString();
                ltlOperador.Text = atividade.Operador;
                lblDataAnterior.Text = atividade.DataAnterior.ToString("dd/MM/yyyy");
                lblDataFinal.Text = atividade.DataFinal.ToString("dd/MM/yyyy");

                if (atividade.QtdRepactuacao == 0)
                {
                    ddlTipoLancamento.Items.Add(new ListItem("Definição Inicial da Data de Conclusão", "2"));
                    ddlTipoLancamento.SelectedValue = "2"; //Definição da Data
                    ddlTipoLancamento.Enabled = false;

                    phAtividadeAnterior.Visible = false;
                }
                
                if (atividade.TipoLancamento == 6) // Reinicio
                {
                    ddlTipoLancamento.Items.Add(new ListItem("Reinicio", "6"));
                    ddlTipoLancamento.SelectedValue = "6";
                    ddlTipoLancamento.Enabled = false;
                }
            }
        }

        protected void DdlTipoLancamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tipoLancamento = Convert.ToInt32(ddlTipoLancamento.SelectedValue);

            switch (tipoLancamento)
            {
                case 9:
                    phAtividadeAnterior.Visible = true;
                    ltlAtividadeAnterior.Text = "Nro. Atividade da outra Equipe";
                    break;
                case 10:
                    phAtividadeAnterior.Visible = true;
                    ltlAtividadeAnterior.Text = "Nro. Atividade Anterior";
                    break;
                case 11:
                    phAtividadeAnterior.Visible = true;
                    ltlAtividadeAnterior.Text = "Nro. Atividade Priorizada";
                    break;
                default:
                    phAtividadeAnterior.Visible = false;
                    break;
            }

            txtNroAtividadeAnterior.Text = "";
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            CarregarAtividades();
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidarAtividade())
                SalvarAtividade();
        }

        private bool ValidarAtividade()
        {
            if (ddlTipoLancamento.SelectedValue == "0")
            {
                RetornarMensagemAviso("Selecione um motivo!");
                return false;
            }

            string[] tiposLancamento = { "9", "10", "11" };
            if (tiposLancamento.Contains(ddlTipoLancamento.SelectedValue))
            {
                if (string.IsNullOrEmpty(txtNroAtividadeAnterior.Text))
                {
                    RetornarMensagemAviso("Digite o número da atividade!");
                    return false;
                }
                if (txtNroAtividadeAnterior.Text.Length < 11)
                {
                    RetornarMensagemAviso("Número de atividade inválido!");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtObservacao.Text) || txtObservacao.Text.Length < 5)
            {
                RetornarMensagemAviso("Adicione uma observação!");
                return false;
            }

            return true;
        }

        private void SalvarAtividade()
        {
            if (CurrentSession.Objects is AtividadeInfo atividade)
            {
                atividade.TipoLancamento = Convert.ToInt32(ddlTipoLancamento.SelectedValue);
                atividade.Observacao = txtObservacao.Text;
                atividade.NroAtividadeAnterior = txtNroAtividadeAnterior.Text;
                atividade.LoginAd = Context.User.Identity.Name;

                AtividadeBo.GetInstance.AlterarAtividadeHistorico(atividade);

                CarregarAtividades();
            }
        }        
    }
}