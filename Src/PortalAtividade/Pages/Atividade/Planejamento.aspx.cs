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
    public partial class Planejamento : BasePage
    {
        private DateTime _dataSegunda;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Usuario.TipoOperador >= (int)TiposInfo.TipoOperador.Lider)
                    ucFiltro.CarregarFiltro(UsuarioAD);
                else
                {
                    ucFiltro.Visible = false;
                    CarregarPlanejamento();
                }
            }
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {
            CarregarPlanejamento();
        }

        private void CarregarPlanejamento()
        {
            LimparInclusao();

            var login = Usuario.TipoOperador >= (int)TiposInfo.TipoOperador.Lider ? ucFiltro.Operador : UsuarioAD;

            _dataSegunda = UtilBo.GetInstance.ObterDataInicioSemana(DateTime.Now);
            ltlSemana.Text = "Planejamento da semana (" + _dataSegunda.ToString("dd/MM") + " à " + _dataSegunda.AddDays(4).ToString("dd/MM") + ")";
            var listaPlanejamento = PlanejamentoBo.GetInstance.ConsultarPlanejamento(login, _dataSegunda);
            lvwPlanejamento.DataSource = listaPlanejamento;
            lvwPlanejamento.DataBind();
            
            ltlSemanaAnterior.Text = "Planejamento da semana passada (" + _dataSegunda.AddDays(-7).ToString("dd/MM") + " à " + _dataSegunda.AddDays(-3).ToString("dd/MM") + ")";
            listaPlanejamento = PlanejamentoBo.GetInstance.ConsultarPlanejamento(login, _dataSegunda.AddDays(-7));
            lvwPlanejamentoAnterior.DataSource = listaPlanejamento;
            lvwPlanejamentoAnterior.DataBind();

            // Demais atuações na semana (Sem Planejamento)
            var listaAtuacao = AtividadeBo.GetInstance.ConsultarAtividadeAtuacao(login, _dataSegunda.AddDays(-7), _dataSegunda);
            listaAtuacao.RemoveAll(p => listaPlanejamento.Select(q => q.NroAtividade).Contains(p.NroAtividade)); // remove as planejadas

            lvwAtuacao.DataSource = listaAtuacao;
            lvwAtuacao.DataBind();
        }

        private void LimparInclusao()
        {
            phNovo.Visible = false;
            btnNovo.Visible = true;
            ddlTipoPlanejamento.SelectedValue = "1";
            txtNroAtividade.Text = "";
            txtDscPlanejamento.Text = "";
        }

        protected void LvwPlanejamento_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                PlanejamentoInfo planejamento = (PlanejamentoInfo)e.Item.DataItem;
                ObterDetalhePlanejamento(planejamento, true);                

                ((Literal)e.Item.FindControl("ltlDescricao")).Text = planejamento.DscPlanejamento;

                var btnExcluir = (LinkButton)e.Item.FindControl("btnExcluir");
                btnExcluir.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja excluir este planejamento?', " + btnExcluir.ClientID + "); return false;";
                btnExcluir.CommandArgument = planejamento.CodPlanejamento.ToString();

                ((Image)e.Item.FindControl("imgOk")).Visible = planejamento.Atendido;
                btnExcluir.Visible = !planejamento.Atendido;
            }
        }

        protected void LvwPlanejamentoAnterior_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                PlanejamentoInfo planejamento = (PlanejamentoInfo)e.Item.DataItem;
                ObterDetalhePlanejamento(planejamento, false);

                ((Literal)e.Item.FindControl("ltlDescricao")).Text = planejamento.DscPlanejamento;

                ((Image)e.Item.FindControl("imgOk")).Visible = planejamento.Atendido;
                ((Image)e.Item.FindControl("imgNok")).Visible = !planejamento.Atendido;
            }
        }

        protected void LvwAtuacao_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                AtividadeInfo atividade = (AtividadeInfo)e.Item.DataItem;
                
                string retorno = atividade.CodStatus == 1 ? "Conclusão " : "Andamento ";
                retorno += "<b>" + atividade.NroAtividade + "</b> - " + atividade.DscChamado + " - " + atividade.DscAtividade;

                if (atividade.QtdConfirmacao > 0)
                    retorno += "<font style='color: #999999; font-style: italic'> - Em " + atividade.QtdConfirmacao.ToString() + (atividade.QtdConfirmacao > 1 ? " dias" : " dia") + "</font>";
                else
                    retorno += "<font style='color: #999999; font-style: italic'> - Em 1 dia ou algumas horas</font>";

                ((Literal)e.Item.FindControl("ltlDescricao")).Text = retorno;                
            }
        }

        private void ObterDetalhePlanejamento(PlanejamentoInfo planejamento, bool atual)
        {
            string retorno;
            planejamento.Atendido = false;

            if (planejamento.CodTipoPlanejamento != 3)
            {
                var atividade = AtividadeBo.GetInstance.ObterAtividadeManual(planejamento.NroAtividade);

                retorno = planejamento.CodTipoPlanejamento == 1 ? "Conclusão " : "Andamento ";
                retorno += "<b>" + planejamento.NroAtividade + "</b>";

                if (!string.IsNullOrEmpty(atividade.NroAtividade))
                {
                    retorno += " - " + (string.IsNullOrEmpty(atividade.DscChamado) ? atividade.DscAtividade : atividade.DscChamado);                    

                    if (atual)
                    {
                        planejamento.Atendido = atividade.CodStatus == 1;

                        if (!planejamento.Atendido)
                        {
                            retorno += " - Data de previsão: " + "<b>" + atividade.DataFinal.ToString("dd/MM/yyyy") + "</b>";

                            if (atividade.DataFinal < _dataSegunda)
                                retorno += "<br /><font style='color: red'>A data de conclusão desta atividade está vencida. Corrigir no TopDesk.</font>";
                            if (planejamento.CodTipoPlanejamento == 1 && atividade.DataFinal > _dataSegunda.AddDays(5))
                                retorno += "<br /><font style='color: red'>A data de conclusão desta atividade é superior a esta semana. Corrigir no TopDesk.</font>";
                        }
                        if (atividade.CodStatus == 1 && atividade.DataConclusao < _dataSegunda)
                            retorno += "<br /><font style='color: red'>Esta atividade já está concluída no Topdesk, verificar.</font>";                        
                    }
                    else
                    {
                        planejamento.Atendido = (planejamento.CodTipoPlanejamento == 1 && atividade.CodStatus == 1 && atividade.DataConclusao < _dataSegunda) || planejamento.CodTipoPlanejamento == 2;

                        if (atividade.CodStatus == 0)
                            retorno += " - Data de previsão atual: " + "<b>" + atividade.DataFinal.ToString("dd/MM/yyyy") + "</b>";                        
                    }
                }
            }
            else
            {
                retorno = planejamento.DscPlanejamento;
                planejamento.Atendido = true;
            }

            planejamento.DscPlanejamento = retorno;
        }

        protected void ExcluirPlanejamento(object sender, CommandEventArgs e)
        {
            var codPlanejamento = Convert.ToInt32(e.CommandArgument);
            PlanejamentoBo.GetInstance.ExcluirPlanejamento(codPlanejamento);
            CarregarPlanejamento();
        }

        protected void BtnNovo_Click(object sender, EventArgs e)
        {
            phNovo.Visible = true;
            btnNovo.Visible = false;
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            phNovo.Visible = false;
            btnNovo.Visible = true;
        }

        protected void BtnIncluirPlanejamento_Click(object sender, EventArgs e)
        {
            if (ValidarInclusao())
            {
                IncluirPlanejamento();
                CarregarPlanejamento();
            }                
        }

        private bool ValidarInclusao()
        {
            if (ddlTipoPlanejamento.SelectedValue != "3")
            {
                if (txtNroAtividade.Text.Length < 11)
                {
                    RetornarMensagemAviso("Número de atividade inválido!");
                    return false;
                }
            }
            else
            {
                if (txtDscPlanejamento.Text.Length < 3)
                {
                    RetornarMensagemAviso("Digite uma descrição do Planejamento!");
                    return false;
                }
            }            

            if (!string.IsNullOrEmpty(txtNroAtividade.Text))
            {
                var atividade = AtividadeBo.GetInstance.ObterAtividadeManual(txtNroAtividade.Text);

                if (string.IsNullOrEmpty(atividade.NroAtividade))
                {
                    if (ChamadoBo.GetInstance.ObterChamado(txtNroAtividade.Text).TipoChamado == "Req. Complexa")
                    {
                        RetornarMensagemAviso("Digite o número da atividade ao invés do número do chamado principal!");
                        return false;
                    }
                }                
            }

            return true;
        }

        private void IncluirPlanejamento()
        {
            var planejamento = new PlanejamentoInfo
            {
                LoginAd = Usuario.TipoOperador >= (int)TiposInfo.TipoOperador.Lider ? ucFiltro.Operador : UsuarioAD,
                DataPlanejamento = UtilBo.GetInstance.ObterDataInicioSemana(DateTime.Now),
                CodTipoPlanejamento = Convert.ToInt32(ddlTipoPlanejamento.SelectedValue),
                NroAtividade = txtNroAtividade.Text,                
                DscPlanejamento = txtDscPlanejamento.Text                
            };

            PlanejamentoBo.GetInstance.IncluirPlanejamento(planejamento);
        }

        protected void DdlTipoPlanejamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoPlanejamento.SelectedValue != "3")
                txtNroAtividade.Enabled = true;
            else
                txtNroAtividade.Enabled = false;
        }
    }
}