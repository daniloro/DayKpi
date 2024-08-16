using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.Pages
{
    public partial class AvaliacaoAtividade : BasePage
    {
        private enum ColAtividade
        {
            NroChamado = 0,
            DscChamado,
            NroAtividade,            
            Operador,
            DataConclusao
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

            var listaAtividade = AvaliacaoAtividadeBo.GetInstance.ConsultarAtividadeAvaliacaoPendente(login);

            CurrentSession.Objects = listaAtividade;

            phLista.Visible = true;
            phAtividade.Visible = false;
            gdvAtividade.DataSource = listaAtividade;
            gdvAtividade.DataBind();
        }

        private void LimparDados()
        {            
            txtObservacao.Text = "";
            ddlComplexidade.SelectedValue = "-1";
            ddlNotaQualidade.SelectedValue = "8";
            ddlNotaPerformance.SelectedValue = "6";
            ddlNotaPo.SelectedValue = "6";
            ddlNotaEspec.SelectedValue = "6";
            ddlNotaNegocio.SelectedValue = "6";
            ddlAnalistaNegocio.SelectedValue = "-1";
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
                AvaliacaoAtividadeInfo atividade = (AvaliacaoAtividadeInfo)e.Row.DataItem;

                e.Row.Cells[(int)ColAtividade.NroChamado].Text = atividade.NroChamado;
                e.Row.Cells[(int)ColAtividade.DscChamado].Text = atividade.DscChamado;
                e.Row.Cells[(int)ColAtividade.NroAtividade].Text = atividade.NroAtividade;                
                e.Row.Cells[(int)ColAtividade.Operador].Text = atividade.Operador;
                e.Row.Cells[(int)ColAtividade.DataConclusao].Text = atividade.DataConclusao.ToString("dd/MM/yyyy");                
                ((LinkButton)e.Row.FindControl("btnEditar")).CommandArgument = atividade.NroAtividade;
            }
        }

        protected void EditarAtividade(object sender, CommandEventArgs e)
        {
            string nroAtividade = e.CommandArgument.ToString();

            if (CurrentSession.Objects is List<AvaliacaoAtividadeInfo> listaAtividade)
            {
                var atividade = listaAtividade.Find(p => p.NroAtividade == nroAtividade);
                CurrentSession.Objects = atividade;

                phAtividade.Visible = true;
                phLista.Visible = false;

                ltlNroChamado.Text = atividade.NroChamado;
                ltlDscChamado.Text = atividade.DscChamado;
                ltlNroAtividade.Text = atividade.NroAtividade;
                ltlDscAtividade.Text = atividade.DscAtividade;
                lblOperador.Text = atividade.Operador;
                lblDataConclusao.Text = atividade.DataConclusao.ToString("dd/MM/yyyy");
                
                if (atividade.TipoAvaliacao == 1) //Desenvolvedor avalia Analista Negocio
                {                    
                    phLider.Visible = false;
                    phDesenvolvedor.Visible = true;
                    ltlNotaPo.Text = "Auto Avaliação";
                }
                else //Lider avalia Desenvolvedor
                {                    
                    phLider.Visible = true;
                    phDesenvolvedor.Visible = false;
                    ltlNotaPo.Text = "Product Owner - Senso de Dono";
                }
            }
        }
                
        protected void DdlAnalistaNegocio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAnalistaNegocio.SelectedValue == "0")
            {
                ddlNotaEspec.SelectedValue = "0";
                ddlNotaNegocio.SelectedValue = "0";
                ddlNotaEspec.Enabled = false;
                ddlNotaNegocio.Enabled = false;
            }
            else
            {
                ddlNotaEspec.Enabled = true;
                ddlNotaNegocio.Enabled = true;
            }
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
            if (ddlComplexidade.SelectedValue == "-1")
            {
                RetornarMensagemAviso("Selecione a complexidade da atividade!");
                return false;
            }            

            if (phLider.Visible)
            {
                if (ddlNotaPo.SelectedValue == "-1")
                {
                    RetornarMensagemAviso("Dê uma nota para o senso de dono do desenvolvedor nesta atividade!");
                    return false;
                }
                if (ddlNotaQualidade.SelectedValue == "-1")
                {
                    RetornarMensagemAviso("Dê uma nota para a qualidade do código do desenvolvedor!");
                    return false;
                }
                if (ddlNotaPerformance.SelectedValue == "-1")
                {
                    RetornarMensagemAviso("Dê uma nota para a performance do desenvolvedor!");
                    return false;
                }
            }
            else
            {
                if (ddlAnalistaNegocio.SelectedValue == "-1")
                {
                    RetornarMensagemAviso("Selecione o analista de negócio da atividade!");
                    return false;
                }
                if (ddlNotaPo.SelectedValue == "-1")
                {
                    RetornarMensagemAviso("Dê uma nota para sua atuação na atividade!");
                    return false;
                }
                if (ddlNotaEspec.SelectedValue == "-1")
                {
                    RetornarMensagemAviso("Dê uma nota para a especificação da atividade/projeto!");
                    return false;
                }
                if (ddlNotaNegocio.SelectedValue == "-1")
                {
                    RetornarMensagemAviso("Dê uma nota para o acompanhamento do analista de negócio!");
                    return false;
                }
            }

            //if (string.IsNullOrEmpty(txtObservacao.Text) || txtObservacao.Text.Length < 5)
            //{
            //    RetornarMensagemAviso("Adicione uma observação!");
            //    return false;
            //}

            return true;
        }

        private void SalvarAtividade()
        {
            if (CurrentSession.Objects is AvaliacaoAtividadeInfo atividade)
            {
                atividade.Complexidade = Convert.ToInt32(ddlComplexidade.SelectedValue);
                atividade.NotaPo = Convert.ToInt32(ddlNotaPo.SelectedValue);                
                atividade.Observacao = txtObservacao.Text;                
                atividade.LoginAd = Context.User.Identity.Name;

                if (atividade.TipoAvaliacao == 1)
                {
                    atividade.NotaEspec = Convert.ToInt32(ddlNotaEspec.SelectedValue);
                    atividade.NotaNegocio = Convert.ToInt32(ddlNotaNegocio.SelectedValue);
                    atividade.Analista = ddlAnalistaNegocio.SelectedItem.Text;
                }
                else
                {
                    atividade.NotaQualidade = Convert.ToInt32(ddlNotaQualidade.SelectedValue);
                    atividade.NotaPerformance = Convert.ToInt32(ddlNotaPerformance.SelectedValue);
                }

                AvaliacaoAtividadeBo.GetInstance.IncluirAvaliacaoAtividade(atividade);
                CarregarAtividades();
            }
        }
    }
}