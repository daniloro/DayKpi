using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using PortalAtividade.Business;
using PortalAtividade.Model;

namespace PortalAtividade
{
    public partial class _Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VerificarMenus();
                CarregarAtividade();
                CarregarPonto();
                CarregarSumario();
            }
        }

        private void VerificarMenus()
        {
            if (Usuario.CodTipoEquipe == (int)TiposInfo.TipoEquipe.Redes)
            {
                lvwAtividade.Visible = false;
                phAtalhos.Visible = false;
                divAvaliacao.Visible = false;
            }            
        }

        #region Atividade        

        private void CarregarAtividade()
        {
            if (Usuario.CodTipoEquipe == (int)TiposInfo.TipoEquipe.Redes) return;

            var listaAtividade = AtividadeBo.GetInstance.ConsultarAtividadeAtual(UsuarioAD, PlanejadorBo.GetInstance.ObterPlanejadorPrincipal(Usuario.CodTipoEquipe));
            lvwAtividade.DataSource = listaAtividade;
            lvwAtividade.DataBind();

            var dataAtualizacao = UtilBo.GetInstance.ObterDataAtualizacao();

            if (listaAtividade.Any())
            {                
                ((Literal)lvwAtividade.FindControl("ltlDataAtualizacao")).Text = dataAtualizacao.ToString("dd/MM/yyyy hh:mm");

                if (!listaAtividade.Any(p => p.Confirmado == 0))
                {
                    var lblStatusAtividade = (Label)lvwAtividade.FindControl("lblStatusAtividade");
                    lblStatusAtividade.Text = "Confirmação Efetuada";
                }
            }
            else
            {
                ((Literal)lvwAtividade.Controls[0].FindControl("ltlDataAtualizacao")).Text = dataAtualizacao.ToString("dd/MM/yyyy hh:mm");
            }

            if (!listaAtividade.Any(p => (new int[] { 0, 1 }).Contains(p.Confirmado)))
            {
                phNovaAtividade.Visible = true;
            }
        }

        protected void LvwAtividade_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                AtividadeInfo atividade = (AtividadeInfo)e.Item.DataItem;

                ((Literal)e.Item.FindControl("ltlNroChamado")).Text = atividade.NroChamado;
                ((Literal)e.Item.FindControl("ltlDscChamado")).Text = atividade.DscChamado;
                ((Literal)e.Item.FindControl("ltlNroAtividade")).Text = atividade.NroAtividade;
                ((Literal)e.Item.FindControl("ltlDscAtividade")).Text = atividade.DscAtividade;
                
                var lblDataFinal = (Label)e.Item.FindControl("lblDataFinal");
                lblDataFinal.Text = atividade.DataFinal.ToString("dd/MM/yyyy");                
                                
                if (atividade.Confirmado == 1)
                {
                    lblDataFinal.ForeColor = Color.Green;
                    lblDataFinal.Text += " - Confirmado";

                    if (atividade.TipoEntrada == "M")
                    {
                        lblDataFinal.Text += " - Atualizar esta data no TopDesk";
                    }
                }
                else if (atividade.Confirmado > 1)
                {
                    lblDataFinal.ForeColor = Color.Red;
                    lblDataFinal.Text = "Atualizar o TopDesk para adequação na próxima carga do sistema";
                }
                else if (atividade.DataFinal.Date < DateTime.Today)
                {
                    lblDataFinal.ForeColor = Color.Red;
                }

                if (atividade.Confirmado == 0)
                {
                    var phConfirmacaoAtividade = (PlaceHolder)e.Item.FindControl("phConfirmacaoAtividade");
                    phConfirmacaoAtividade.Visible = true;

                    var phConfirmarAtividade = (PlaceHolder)e.Item.FindControl("phConfirmarAtividade");                    

                    if (atividade.DataFinal >= DateTime.Today)
                    {                        
                        phConfirmarAtividade.Visible = true;                        

                        var lbConfirmarAtividade = (LinkButton)e.Item.FindControl("lbConfirmarAtividade");
                        lbConfirmarAtividade.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'A data de conclusão planejada será mantida?', " + lbConfirmarAtividade.ClientID + "); return false;";
                        lbConfirmarAtividade.CommandArgument = "1";                        
                    }
                    else
                    {                        
                        phConfirmarAtividade.Visible = false;                        
                    }             

                    var lbAlterarAtividade = (LinkButton)e.Item.FindControl("lbAlterarAtividade");
                    lbAlterarAtividade.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Está atuando em outra atividade neste momento?', " + lbAlterarAtividade.ClientID + "); return false;";
                    lbAlterarAtividade.CommandArgument = "2";

                    var lbAlterarData = (LinkButton)e.Item.FindControl("lbAlterarData");
                    lbAlterarData.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Irá corrigir a data de conclusão no TopDesk?', " + lbAlterarData.ClientID + "); return false;";
                    lbAlterarData.CommandArgument = "3";
                }
            }
        }

        protected void LvwAtividade_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            WebControl wc = (WebControl)e.CommandSource;
            ListViewItem item = (ListViewItem)wc.NamingContainer;

            int confirmacao = Convert.ToInt32(e.CommandArgument);
            var nroAtividade = ((Literal)item.FindControl("ltlNroAtividade")).Text;
            
            if (confirmacao != 3)
            {
                AtividadeBo.GetInstance.ConfirmarAtividade(nroAtividade, confirmacao);
                CarregarAtividade();
            }
            else // Manual
            {
                var atividade = AtividadeBo.GetInstance.ObterAtividadeManual(nroAtividade);

                CurrentSession.Objects = atividade;
                CurrentSession.Redirect = "~/Default.aspx";
                Response.Redirect("~/Pages/Atividade/AtividadeAlterada.aspx", false);
            }
        }

        protected void IncluirAtividade(object sender, CommandEventArgs e)
        {
            CurrentSession.Objects = UsuarioAD;
            CurrentSession.Redirect = "~/Default.aspx";
            Response.Redirect("~/Pages/Atividade/AtividadeManual.aspx", false);
        }
        protected void IncluirPlanejamento(object sender, EventArgs e)
        {            
            Response.Redirect("~/Pages/Atividade/Planejamento.aspx", false);
        }


        #endregion

        #region Ponto      

        private void CarregarPonto()
        {
            CurrentSession.Objects = null;

            var ponto = PontoBo.GetInstance.ObterPonto(UsuarioAD);
            if (ponto.CodPonto > 0)
            {
                CurrentSession.Objects = ponto;

                lbHoraInicio.Text = "Início: " + ponto.HoraInicio.ToString("HH:mm") + "h";
                lbHoraAlmoco.Text = ponto.HoraAlmoco == DateTime.MinValue ? "Almoço &raquo;" : "Almoço: " + ponto.HoraAlmoco.ToString("HH:mm") + "h";
                lbHoraRetorno.Text = ponto.HoraRetorno == DateTime.MinValue ? "Retorno &raquo;" : "Retorno: " + ponto.HoraRetorno.ToString("HH:mm") + "h";
                lbHoraSaida.Text = ponto.HoraSaida == DateTime.MinValue ? "Saída &raquo;" : "Saída: " + ponto.HoraSaida.ToString("HH:mm") + "h";

                DefinirTipoAcesso(ponto.HomeOffice);
                
                AlterarBotaoPonto(lbHoraInicio);

                if (ponto.HoraAlmoco == DateTime.MinValue)
                {
                    lbHoraAlmoco.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja realmente incluir o horário de Almoço?', " + lbHoraAlmoco.ClientID + "); return false;";
                    lbHoraRetorno.OnClientClick = "return false;";
                    lbHoraSaida.OnClientClick = "return false;";
                }
                else
                {
                    AlterarBotaoPonto(lbHoraAlmoco);

                    if (ponto.HoraRetorno == DateTime.MinValue)
                    {
                        lbHoraRetorno.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja realmente incluir o horário de Retorno do Almoço?', " + lbHoraRetorno.ClientID + "); return false;";                        
                        lbHoraSaida.OnClientClick = "return false;";
                    }
                    else
                    {
                        AlterarBotaoPonto(lbHoraRetorno);

                        if (ponto.HoraSaida == DateTime.MinValue)
                        {
                            lbHoraSaida.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja realmente incluir o horário de Saída?', " + lbHoraSaida.ClientID + "); return false;";                            
                        }
                        else
                        {
                            AlterarBotaoPonto(lbHoraSaida);
                        }
                    }
                }
            }
            else
            {
                //lbHoraInicio.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja realmente incluir o horário de Entrada?', " + lbHoraInicio.ClientID + "); return false;";
                lbHoraAlmoco.OnClientClick = "return false;";
                lbHoraRetorno.OnClientClick = "return false;";
                lbHoraSaida.OnClientClick = "return false;";
            }
        }

        private void DefinirTipoAcesso(bool homeOffice)
        {
            rbHomeOffice.Checked = homeOffice;
            rbPresencial.Checked = !homeOffice;
            rbHomeOffice.Enabled = false;
            rbPresencial.Enabled = false;
        }

        private void CarregarSumario()
        {
            var qtdRepactuacao = AtividadeBo.GetInstance.ObterQtdAtividadeRepactuadaPendente(UsuarioAD);

            if (qtdRepactuacao > 0)
            {
                lblQtdRepactuacaoPendente.ForeColor = Color.Red;
                if (qtdRepactuacao == 1) lblQtdRepactuacaoPendente.Text = "Você possui 1 justificativa pendente.";
                else lblQtdRepactuacaoPendente.Text = "Você possui " + qtdRepactuacao + " justificativas pendentes.";
            }            
        }

        private void AlterarBotaoPonto(LinkButton lb)
        {
            lb.CssClass = "btn btn-outline-secondary btn-lg w-100";            

            lb.Enabled = false;
            lb.OnClientClick = "return false;";
        }

        protected void LbHoraInicio_Click(object sender, EventArgs e)
        {
            if (!(CurrentSession.Objects is PontoInfo))
            {
                PontoInfo ponto = new PontoInfo
                {
                    DataPonto = DateTime.Today,
                    HoraInicio = DateTime.Now,
                    HomeOffice = rbHomeOffice.Checked,
                    LoginAd = UsuarioAD
                };

                PontoBo.GetInstance.IncluirPonto(ponto);
                CarregarPonto();
            }
        }

        protected void LbHoraAlmoco_Click(object sender, EventArgs e)
        {
            if (CurrentSession.Objects is PontoInfo ponto)
            {
                if (ponto.HoraAlmoco == DateTime.MinValue)
                {
                    ponto.HoraAlmoco = DateTime.Now;

                    PontoBo.GetInstance.AlterarPonto(ponto);
                    CarregarPonto();
                }                
            }
        }

        protected void LbHoraRetorno_Click(object sender, EventArgs e)
        {
            if (CurrentSession.Objects is PontoInfo ponto)
            {
                if (ponto.HoraRetorno == DateTime.MinValue)
                {
                    ponto.HoraRetorno = DateTime.Now;

                    PontoBo.GetInstance.AlterarPonto(ponto);
                    CarregarPonto();
                }
            }
        }

        protected void LbHoraSaida_Click(object sender, EventArgs e)
        {
            if (CurrentSession.Objects is PontoInfo ponto)
            {
                if (ponto.HoraSaida == DateTime.MinValue)
                {
                    ponto.HoraSaida = DateTime.Now;

                    PontoBo.GetInstance.AlterarPonto(ponto);
                    CarregarPonto();
                }
            }
        }

        #endregion
    }
}