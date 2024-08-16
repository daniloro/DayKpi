using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.Pages.Kpi
{
    public partial class Organograma : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Usuario.TipoOperador < (int)TiposInfo.TipoOperador.Lider)
                    Response.Redirect("~/Default.aspx", false);
                
                CarregarPerguntaKPI();
                ucFiltro.CarregarFiltro(UsuarioAD);                
            }
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {
            CarregarControles();
        }        

        protected void LbAlterarGestor_Click(object sender, EventArgs e)
        {
            CarregarGestor();

            phEditarGestor.Visible = false;
            phSalvarGestor.Visible = true;
            phAnaliseKPI.Visible = false;
        }

        protected void LbVoltarGestor_Click(object sender, EventArgs e)
        {
            phEditarGestor.Visible = true;
            phSalvarGestor.Visible = false;
            phAnaliseKPI.Visible = true;

            CarregarControles();
        }

        private void CarregarGestor()
        {
            var operador = OrganogramaBo.GetInstance.ObterGestor(hdLogin.Value);

            if (operador != null)
            {
                ltlNome.Text = operador.Nome;
                ltlNomeGestor.Text = operador.NomeGestor;
                txtGestor.Text = operador.Gestor;
            }
            else
            {
                LbVoltarGestor_Click(null, null);
            }
        }

        protected void LbSalvarGestor_Click(object sender, EventArgs e)
        {
            SalvarGestor();
            LbVoltarGestor_Click(null, null);
        }

        private void SalvarGestor()
        {
            if (string.IsNullOrEmpty(txtGestor.Text))
            {
                RetornarMensagemAviso("Digite o login do gestor!");
                return;
            }

            if (OperadorBo.GetInstance.ObterOperador(txtGestor.Text) == null)
            {
                RetornarMensagemAviso("Nenhum usuário cadastrado com este login!");
                return;
            }

            var lista = OrganogramaBo.GetInstance.ConsultarOrganograma(hdLogin.Value);
            var operador = lista.Find(p => p.LoginAd == hdLogin.Value);
            var gestor = lista.Find(p => p.LoginAd == txtGestor.Text);

            if (gestor != null && gestor.Nivel > operador.Nivel)
            {
                RetornarMensagemAviso("Não é possível incluir este colaborador como gestor!");
                return;
            }

            OrganogramaBo.GetInstance.AlterarGestor(hdLogin.Value, txtGestor.Text);
        }

        private void CarregarPerguntaKPI()
        {
            ltlPergunta.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Organograma).DscPergunta;
        }

        private void CarregarControles()
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);

            var analise = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(dataConsulta, ucFiltro.Gestor, (int)TiposInfo.PerguntaKPI.Organograma);
            txtAnalise.Text = analise.DscAnalise;

            phAnaliseKPI.Visible = true;
            if (ucFiltro.Gestor == UsuarioAD)
                PermitirAlteracao(!analise.Concluido);
            else
                PermitirAlteracao(false);

            CarregarOrganograma(dataConsulta, analise.Concluido);
        }

        private void PermitirAlteracao(bool habilita)
        {
            txtAnalise.Enabled = habilita;            
            btnSalvar.Visible = habilita;
            btnSalvarEnviar.Visible = habilita;
            phEditarGestor.Visible = habilita;
        }

        private void CarregarOrganograma(DateTime dataConsulta, bool Confirmado)
        {
            List<OrganogramaInfo> listaOrganograma;

            if (!Confirmado)
            {
                listaOrganograma = OrganogramaBo.GetInstance.ConsultarOrganograma(ucFiltro.Gestor);
            }
            else
            {
                listaOrganograma = OrganogramaBo.GetInstance.ConsultarOrganogramaPeriodo(dataConsulta, ucFiltro.Gestor);
            }

            var retorno = OrganogramaBo.GetInstance.ConsultarOrganogramaJson(listaOrganograma);

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Chart1", $"dadosChart1 = " + retorno + "; google.charts.setOnLoadCallback(drawChart);", true);
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            SalvarAnalise(false);
        }

        protected void BtnSalvarEnviar_Click(object sender, EventArgs e)
        {
            SalvarAnalise(true);
        }

        private void SalvarAnalise(bool concluido)
        {
            if (ValidarAnalise())
            {
                AnaliseKPIInfo analise = new AnaliseKPIInfo
                {
                    DataAnalise = Convert.ToDateTime(ucFiltro.Mes),
                    LoginAd = UsuarioAD,
                    Concluido = concluido
                };

                AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Organograma, txtAnalise.Text, analise);

                if (concluido)
                {
                    OrganogramaBo.GetInstance.IncluirOrganograma(ucFiltro.Gestor, analise.DataAnalise, analise.CodAnalise);
                }

                CarregarControles();
            }            
        }

        private bool ValidarAnalise()
        {
            if (string.IsNullOrEmpty(txtAnalise.Text))
            {
                RetornarMensagemAviso("Adicione uma observação!");
                return false;
            }
            return true;
        }
    }
}