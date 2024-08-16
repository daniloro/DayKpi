using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.Atividade
{
    public partial class AtividadeAlterada : BasePage
    {
        private enum ColAtividade
        {
            NroChamado = 0,
            DscChamado,
            NroAtividade,
            DscAtividade,
            Operador,
            QtdRepactuacao,
            QtdConfirmacao,
            DataAnterior,
            DataFinal
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniciarControle();
            }
        }

        private void IniciarControle()
        {
            if (CurrentSession.Objects is AtividadeInfo atividade)
            {                
                CarregarAtividade(atividade);
            }
            else
            {                
                CurrentSession.Objects = null;
                                
                CarregarListaAtividade();
                CarregarGrupos();
            }
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
            CarregarListaAtividade();
        }

        private void CarregarListaAtividade()
        {
            var listaAtividade = AtividadeBo.GetInstance.ConsultarAtividadeRepactuadaPendente(UsuarioAD);

            if (ucFiltro.Grupo != "" && ucFiltro.Grupo != "0")
            {
                listaAtividade.RemoveAll(p => p.Grupo != ucFiltro.Grupo);
            }

            CurrentSession.Objects = listaAtividade;

            ltlTotal.Text = listaAtividade.Count.ToString();

            phLista.Visible = true;
            phAtividade.Visible = false;
            gdvAtividade.DataSource = listaAtividade;
            gdvAtividade.DataBind();
        }

        private void LimparDados()
        {
            phAtividadeAnterior.Visible = false;
            txtNroAtividadeAnterior.Text = "";
            txtObservacao.Text = "";

            ddlTipoLancamento.Enabled = true;
            ddlTipoLancamento.SelectedValue = "0";

            if (ddlTipoLancamento.Items.Contains(ItemAntecipacao))            
                ddlTipoLancamento.Items.Remove(ItemAntecipacao);
            if (ddlTipoLancamento.Items.Contains(DefinicaoData))
                ddlTipoLancamento.Items.Remove(DefinicaoData);
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
                e.Row.Cells[(int)ColAtividade.Operador].Text = atividade.Operador;
                e.Row.Cells[(int)ColAtividade.DataAnterior].Text = atividade.DataAnterior.ToString("dd/MM/yyyy");
                e.Row.Cells[(int)ColAtividade.DataFinal].Text = atividade.DataFinal.ToString("dd/MM/yyyy");
                e.Row.Cells[(int)ColAtividade.QtdRepactuacao].Text = atividade.QtdRepactuacao.ToString();
                e.Row.Cells[(int)ColAtividade.QtdConfirmacao].Text = atividade.QtdConfirmacao.ToString();
                ((LinkButton)e.Row.FindControl("btnEditar")).CommandArgument = atividade.CodHistorico.ToString();
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
            int codHistorico = Convert.ToInt32(e.CommandArgument);

            if (CurrentSession.Objects is List<AtividadeInfo> listaAtividade)
            {
                var atividade = listaAtividade.Find(p => p.CodHistorico == codHistorico);
                CurrentSession.Objects = atividade;

                CarregarAtividade(atividade);
            }
        }        

        private ListItem ItemAntecipacao
        {
            get
            {
                return new ListItem("Antecipação da Atividade", "4");
            }
        }
        private ListItem DefinicaoData
        {
            get
            {
                return new ListItem("Definição da Data", "2");
            }
        }

        private void CarregarAtividade(AtividadeInfo atividade)
        {
            LimparDados();

            phAtividade.Visible = true;
            phLista.Visible = false;

            ltlNroChamado.Text = atividade.NroChamado;
            ltlDscChamado.Text = atividade.DscChamado;
            ltlNroAtividade.Text = atividade.NroAtividade;
            ltlDscAtividade.Text = atividade.DscAtividade;
            ltlQtdRepactuacao.Text = atividade.QtdRepactuacao.ToString();
            ltlOperador.Text = atividade.Operador;

            ucHistorico.CarregarHistorico(atividade.NroAtividade);

            if (atividade.CodHistorico > 0)
            {
                lblDataAnterior.Text = atividade.DataAnterior.ToString("dd/MM/yyyy");
                lblDataFinal.Text = atividade.DataFinal.ToString("dd/MM/yyyy");

                phDataFinalLabel.Visible = true;
                phDataFinalTextBox.Visible = false;                
            }
            else // Novo
            {
                lblDataAnterior.Text = atividade.DataFinal.ToString("dd/MM/yyyy");

                if (atividade.DataFinal.Date < DateTime.Today)
                    txtDataFinal.Text = DateTime.Today.ToString("dd/MM/yyyy");
                else
                    txtDataFinal.Text = atividade.DataFinal.AddDays(1).ToString("dd/MM/yyyy");

                phDataFinalLabel.Visible = false;
                phDataFinalTextBox.Visible = true;

                if (!AtividadeBo.GetInstance.ObterDefinicaoDataAtividade(atividade.NroAtividade))
                {
                    ddlTipoLancamento.Items.Add(DefinicaoData);
                    ddlTipoLancamento.SelectedValue = DefinicaoData.Value;
                    ddlTipoLancamento.Enabled = false;
                }
                else if (atividade.DataFinal > DateTime.Today)
                {
                    ddlTipoLancamento.Items.Add(ItemAntecipacao);
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
            if (!string.IsNullOrEmpty(CurrentSession.Redirect))
            {
                var pagina = CurrentSession.Redirect;

                CurrentSession.Redirect = null;
                Response.Redirect(pagina, false);
            }
            else
                CarregarListaAtividade();
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidarAtividade())
            {
                SalvarAtividade();

                if (!string.IsNullOrEmpty(CurrentSession.Redirect))
                {
                    var pagina = CurrentSession.Redirect;

                    CurrentSession.Redirect = null;
                    Response.Redirect(pagina, false);
                }
                else
                    CarregarListaAtividade();
            }                
        }        

        private bool ValidarAtividade()
        {
            if (phDataFinalTextBox.Visible)
            {
                DateTime.TryParse(txtDataFinal.Text, out DateTime dataFinal);

                if (dataFinal == DateTime.MinValue)
                {
                    RetornarMensagemAviso("Digite a nova data de conclusão!");
                    return false;
                }
                else if (dataFinal < DateTime.Today)
                {
                    RetornarMensagemAviso("A nova data de conclusão não pode estar vencida!");
                    return false;
                }
                else if (dataFinal == Convert.ToDateTime(lblDataAnterior.Text))
                {
                    RetornarMensagemAviso("Altere a nova data de conclusão!");
                    return false;
                }
                else if (dataFinal < Convert.ToDateTime(lblDataAnterior.Text) && ddlTipoLancamento.SelectedValue != "4" && ddlTipoLancamento.SelectedValue != "2")
                {
                    RetornarMensagemAviso("Selecione o motivo Antecipação da Atividade!");
                    return false;
                }
                if (dataFinal > Convert.ToDateTime(lblDataAnterior.Text) && ddlTipoLancamento.SelectedValue == "4")
                {
                    RetornarMensagemAviso("Motivo inválido!");
                    return false;
                }
            }

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

            string[] tiposLancamentoOk = { "2", "12" };
            if (!tiposLancamentoOk.Contains(ddlTipoLancamento.SelectedValue))
            {
                if (string.IsNullOrEmpty(txtObservacao.Text) || txtObservacao.Text.Length < 5)
                {
                    RetornarMensagemAviso("Adicione uma observação!");
                    return false;
                }
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
                atividade.LoginAd = UsuarioAD;

                if (atividade.CodHistorico > 0)
                {
                    AtividadeBo.GetInstance.AlterarAtividadeHistorico(atividade);                    
                }
                else //manual
                {
                    DateTime.TryParse(txtDataFinal.Text, out DateTime dataFinal);
                    atividade.DataAnterior = atividade.DataFinal;
                    atividade.DataFinal = dataFinal;

                    if (atividade.TipoLancamento == Convert.ToInt32(DefinicaoData.Value))
                    {
                        // double check no momento da inclusão
                        if (AtividadeBo.GetInstance.ObterDefinicaoDataAtividade(atividade.NroAtividade))
                        {
                            return;
                        }
                    }
                    
                    AtividadeBo.GetInstance.IncluirAtividadeHistorico(atividade);
                    AtividadeBo.GetInstance.AlterarAtividadeManual(atividade);                    
                }
            }
        }
    }
}