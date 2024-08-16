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
    public partial class AvaliacaoAtividade : BasePage
    {
        private enum ColDestaque
        {
            Qualidade = 0,
            Performance,
            PO
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarAtividade();
            }
        }

        private void CarregarAtividade()
        {
            if (CurrentSession.Filtro is AvaliacaoMensalFiltroInfo filtro)
            {
                var atividade = AvaliacaoAtividadeBo.GetInstance.ConsultarAtividade(filtro.NroAtividade);

                ltlNroChamado.Text = atividade.NroChamado;
                ltlDscChamado.Text = atividade.DscChamado;
                ltlNroAtividade.Text = atividade.NroAtividade;
                ltlDscAtividade.Text = atividade.DscAtividade;
                lblOperador.Text = atividade.Operador;
                lblDataConclusao.Text = atividade.DataConclusao.ToString("dd/MM/yyyy");

                if (atividade.TipoChamado == "Atividade")                
                    ltlTipoAtividade.Text = atividade.DscPlanejador;
                else
                    ltlTipoAtividade.Text = atividade.TipoChamado;

                ltlPonderacao.Text = atividade.Ponderacao.ToString();
                ltlConfirmacao.Text = atividade.QtdConfirmacao.ToString();
                ltlRepactuacao.Text = atividade.QtdRepactuacao.ToString();
                ltlRepactuacaoSemJustificativa.Text = atividade.QtdRepactuacaoSemJustificativa.ToString();
                ltlAvaliacaoGestor.Text = atividade.NotaAvaliacao.ToString();
                ltlPonderacaoTotal.Text = atividade.PonderacaoTotal.ToString();                
                ltlDestaqueGestor.Text = String.IsNullOrEmpty(atividade.DestaqueGestor)? "Nenhum selecionado" : atividade.DestaqueGestor;

                if (atividade.CodAvaliacaoAuto > 0)
                {
                    phAvaliacaoAuto.Visible = true;                    
                    ltlObservacaoAuto.Text = atividade.ObservacaoAuto;

                    var listaDestaque = AvaliacaoAtividadeBo.GetInstance.ConsultarAtividadeDestaque(atividade.CodAvaliacaoAuto);
                    if (listaDestaque.Any())
                        ltlDestaqueAuto.Text = string.Join(", ", listaDestaque.Select(p => p.DscDestaque).ToArray());
                }

                if (atividade.CodAvaliacao > 0)
                {
                    phAvaliacaoGestor.Visible = true;
                    ltlObservacaoGestor.Text = atividade.Observacao;                    
                }

                if ((UsuarioAD == atividade.LoginAd && atividade.CodAvaliacaoAuto == 0) || 
                    (UsuarioAD != atividade.LoginAd && atividade.CodAvaliacao == 0 && Usuario.TipoOperador >= (int)TiposInfo.TipoOperador.Lider))
                {
                    btnIncluirAvaliacao.Visible = true;
                }

                CurrentSession.Objects = atividade;
            }
        }        

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {            
            Response.Redirect("~/Pages/Atividade/AvaliacaoMensal.aspx", false);
        }

        protected void BtnIncluirAvaliacao_Click(object sender, EventArgs e)
        {            
            phAvaliacao.Visible = true;

            btnIncluirAvaliacao.Visible = false;
            btnSalvar.Visible = true;

            CarregarDestaques();
        }

        private void CarregarDestaques()
        {
            var listaDestaque = AvaliacaoAtividadeBo.GetInstance.ConsultarDestaque();

            cblDestaque.DataSource = listaDestaque;
            cblDestaque.DataTextField = "DscDestaque";
            cblDestaque.DataValueField = "CodDestaque";
            cblDestaque.DataBind();
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidarAvaliacao())
                SalvarAvaliacao();
        }

        private bool ValidarAvaliacao()
        {
            if (string.IsNullOrEmpty(txtObservacao.Text) && !cblDestaque.Items.Cast<ListItem>().Any(i => i.Selected))
            {
                RetornarMensagemAviso("Adicione uma observação!");
                return false;
            }

            return true;
        }

        private void SalvarAvaliacao()
        {
            if (CurrentSession.Objects is AvaliacaoAtividadeInfo atividade)
            {
                atividade.LoginAvaliador = UsuarioAD;
                atividade.TipoAvaliacao = atividade.LoginAd == UsuarioAD ? 1 : 2;
                atividade.Observacao = txtObservacao.Text;

                var codAvaliacao = AvaliacaoAtividadeBo.GetInstance.IncluirAvaliacaoAtividade(atividade);

                foreach (var item in cblDestaque.Items.Cast<ListItem>().Where(li => li.Selected).ToList())
                {
                    var destaque = new AtividadeDestaqueInfo
                    {
                        CodDestaque = Convert.ToInt32(item.Value),
                        CodAvaliacao = codAvaliacao
                    };

                    AvaliacaoAtividadeBo.GetInstance.IncluirAtividadeDestaque(destaque);
                }

                phAvaliacao.Visible = false;
                btnSalvar.Visible = false;

                CurrentSession.Objects = atividade.NroAtividade;
                CarregarAtividade();
            }
        }
    }
}