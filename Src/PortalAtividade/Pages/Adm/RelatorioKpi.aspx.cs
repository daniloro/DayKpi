using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.Pages.Adm
{
    public partial class RelatorioKpi : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarKpi();                
            }
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Adm/ResumoKpi.aspx", false);
        }

        private void CarregarKpi()
        {
            if (CurrentSession.Objects is FiltroLoginPeriodo filtro)
            {
                ltlGestor.Text = OperadorBo.GetInstance.ObterOperador(filtro.LoginAd).Nome;
                ltlMesAno.Text = UtilBo.GetInstance.ObterNomeMes(filtro.DataConsulta.Month) + " de " + filtro.DataConsulta.Year.ToString();

                CarregarObjetivoMissao(filtro);
                CarregarOrganograma(filtro);
                CarregarPrincipaisDemandas(filtro);
                CarregarPrincipaisDemandasProximoMes(filtro);
                CarregarMeta(filtro);
                CarregarBacklog(filtro);
                CarregarEmFilaeAbertoConcluido(filtro);
                CarregarAtendimentoePerformance(filtro);
                CarregarGmud(filtro);                
                CarregarHoraExtra(filtro);
            }            
        }

        private void CarregarObjetivoMissao(FiltroLoginPeriodo filtro)
        {
            ltlObjetivoResp.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(filtro.DataConsulta, filtro.LoginAd, (int)TiposInfo.PerguntaKPI.Objetivo).DscAnalise ?? "Sem resposta.";
            ltlMissaoResp.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(filtro.DataConsulta, filtro.LoginAd, (int)TiposInfo.PerguntaKPI.Missao).DscAnalise ?? "Sem resposta.";

            ltlObjetivoResp.Text = ltlObjetivoResp.Text.Replace(Environment.NewLine, "<br />");
            ltlMissaoResp.Text = ltlMissaoResp.Text.Replace(Environment.NewLine, "<br />");
        }

        private void CarregarOrganograma(FiltroLoginPeriodo filtro)
        {
            var listaOrganograma = OrganogramaBo.GetInstance.ConsultarOrganogramaPeriodo(filtro.DataConsulta, filtro.LoginAd);
            if (!listaOrganograma.Any())
                listaOrganograma = OrganogramaBo.GetInstance.ConsultarOrganograma(filtro.LoginAd);

            ucOrganograma.CarregarOrgChart(listaOrganograma);

            ltlOrganograma.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Organograma).DscPergunta;
            ltlOrganogramaResp.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(filtro.DataConsulta, filtro.LoginAd, (int)TiposInfo.PerguntaKPI.Organograma).DscAnalise ?? "Sem resposta.";
            ltlOrganogramaResp.Text = ltlOrganogramaResp.Text.Replace(Environment.NewLine, "<br />");
        }        

        private void CarregarPrincipaisDemandas(FiltroLoginPeriodo filtro)
        {
            ltlMesAno3.Text = UtilBo.GetInstance.ObterNomeMes(filtro.DataConsulta.Month) + " de " + filtro.DataConsulta.Year.ToString();

            var listaChamado = ChamadoBo.GetInstance.ConsultarChamadoRelevanteMes(filtro.LoginAd, filtro.DataConsulta);

            var retorno = "[";

            if (listaChamado.Any())
            {
                retorno += "[{label: 'NroChamado', id: 'NroChamado', type: 'string'}, ";
                retorno += "{label: 'DscChamado', id: 'DscChamado', type: 'string'}, ";
                retorno += "{label: 'Categoria', id: 'Categoria', type: 'string'}, ";
                retorno += "{label: 'SubCategoria', id: 'SubCategoria', type: 'string'}, ";
                retorno += "{label: 'Data Conclusão', id: 'DataConclusao', type: 'string'}], ";                                

                foreach (var chamado in listaChamado)
                {
                    retorno += "['" + chamado.NroChamado + "', ";
                    retorno += "'" + chamado.DscChamado + "', ";
                    retorno += "'" + chamado.Categoria + "', ";
                    retorno += "'" + chamado.SubCategoria + "', ";
                    retorno += "'" + chamado.DataImplementacao.ToString("dd/MM/yyyy") + "'], ";                    
                }
                retorno = retorno.Remove(retorno.Length - 2, 2);
                retorno += "]";
            }
            else
            {
                retorno = "[[{label: 'Principais Demandas', id: 'Vazio', type: 'string'}], ['Nenhum registro encontrado!']]";
            }

            ucPrincipaisDemandas.CarregarTabela(retorno);            
        }

        private void CarregarPrincipaisDemandasProximoMes(FiltroLoginPeriodo filtro)
        {
            var dataProxima = filtro.DataConsulta.AddMonths(1);
            ltlMesAno4.Text = UtilBo.GetInstance.ObterNomeMes(dataProxima.Month) + " de " + dataProxima.Year.ToString();

            var listaChamado = ChamadoBo.GetInstance.ConsultarChamadoRelevantePendente(filtro.LoginAd);
            listaChamado.RemoveAll(p => p.DataFinal < dataProxima);

            var retorno = "[";

            if (listaChamado.Any())
            {
                retorno += "[{label: 'NroChamado', id: 'NroChamado', type: 'string'}, ";
                retorno += "{label: 'DscChamado', id: 'DscChamado', type: 'string'}, ";
                retorno += "{label: 'Categoria', id: 'Categoria', type: 'string'}, ";
                retorno += "{label: 'SubCategoria', id: 'SubCategoria', type: 'string'}, ";
                retorno += "{label: 'Data Conclusão', id: 'DataConclusao', type: 'string'}], ";

                foreach (var chamado in listaChamado)
                {
                    retorno += "['" + chamado.NroChamado + "', ";
                    retorno += "'" + chamado.DscChamado + "', ";
                    retorno += "'" + chamado.Categoria + "', ";
                    retorno += "'" + chamado.SubCategoria + "', ";
                    retorno += "'" + chamado.DataFinal.ToString("dd/MM/yyyy") + "'], ";
                }
                retorno = retorno.Remove(retorno.Length - 2, 2);
                retorno += "]";
            }
            else
            {
                retorno = "[[{label: 'Principais Demandas', id: 'Vazio', type: 'string'}], ['Nenhum registro encontrado!']]";
            }
            ucPrincipaisDemandasProximoMes.CarregarTabela(retorno);
        }

        private void CarregarMeta(FiltroLoginPeriodo filtro)
        {
            ltlMetaAnterior.Text += UtilBo.GetInstance.ObterNomeMes(filtro.DataConsulta.Month);
            ltlMetaAnteriorResp.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(filtro.DataConsulta.AddMonths(-1), filtro.LoginAd, (int)TiposInfo.PerguntaKPI.Meta2).DscAnalise ?? "Sem resposta.";
            ltlMetaAnteriorResp.Text = ltlMetaAnteriorResp.Text.Replace(Environment.NewLine, "<br />");

            ltlMeta1.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Meta1).DscPergunta;
            ltlMetaResp1.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(filtro.DataConsulta, filtro.LoginAd, (int)TiposInfo.PerguntaKPI.Meta1).DscAnalise ?? "Sem resposta.";
            ltlMetaResp1.Text = ltlMetaResp1.Text.Replace(Environment.NewLine, "<br />");
            ltlMeta2.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Meta2).DscPergunta;
            ltlMetaResp2.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(filtro.DataConsulta, filtro.LoginAd, (int)TiposInfo.PerguntaKPI.Meta2).DscAnalise ?? "Sem resposta.";
            ltlMetaResp2.Text = ltlMetaResp2.Text.Replace(Environment.NewLine, "<br />");
        }

        private void CarregarBacklog(FiltroLoginPeriodo filtro)
        {
            ucBacklog.CarregarBacklog(filtro.LoginAd, filtro.DataConsulta);            
        }

        private void CarregarEmFilaeAbertoConcluido(FiltroLoginPeriodo filtro)
        {
            phEmFila.Controls.Clear();
            phAbertoConcluido.Controls.Clear();

            var listaGrupo = OperadorBo.GetInstance.ConsultarGruposOperador(filtro.LoginAd);

            var i = 1;
            foreach (var grupo in listaGrupo)
            {
                UserControl.Kpi.EmFila ucEmFila = (UserControl.Kpi.EmFila)LoadControl("~/UserControl/Kpi/EmFila.ascx");
                ucEmFila.ID = "ucEmFila" + i.ToString();
                ucEmFila.CarregarEmFila(grupo.Grupo, filtro.DataConsulta);
                phEmFila.Controls.Add(ucEmFila);

                UserControl.Kpi.AbertoConcluido ucAbertoConcluido = (UserControl.Kpi.AbertoConcluido)LoadControl("~/UserControl/Kpi/AbertoConcluido.ascx");
                ucAbertoConcluido.ID = "ucAbertoConcluido" + i.ToString();
                ucAbertoConcluido.CarregarAbertoConcluido(grupo.Grupo, filtro.DataConsulta);
                phAbertoConcluido.Controls.Add(ucAbertoConcluido);

                i++;
            }
        }

        private void CarregarAtendimentoePerformance(FiltroLoginPeriodo filtro)
        {
            phAtendimento.Controls.Clear();
            phPerformance.Controls.Clear();

            var listaEquipe = OperadorBo.GetInstance.ConsultarOperadorGestor(filtro.LoginAd);

            var i = 1;
            foreach (var operador in listaEquipe)
            {
                UserControl.Kpi.Atendimento ucAtendimento = (UserControl.Kpi.Atendimento)LoadControl("~/UserControl/Kpi/Atendimento.ascx");
                ucAtendimento.ID = "ucAtendimento" + i.ToString();
                ucAtendimento.CarregarAtendimento(operador.LoginAd, filtro.DataConsulta);
                phAtendimento.Controls.Add(ucAtendimento);

                UserControl.Kpi.Performance ucPerformance = (UserControl.Kpi.Performance)LoadControl("~/UserControl/Kpi/Performance.ascx");
                ucPerformance.CarregarPerformance(filtro.DataConsulta, operador.LoginAd, operador.Nome);
                phPerformance.Controls.Add(ucPerformance);

                i++;
            }
        }

        private void CarregarGmud(FiltroLoginPeriodo filtro)
        {
            var listaGmud = GmudBo.GetInstance.ConsultarGmudPeriodo(filtro.LoginAd, filtro.DataConsulta, filtro.DataConsulta.AddMonths(1));

            var retorno = "[";

            if (listaGmud.Any())
            {
                retorno += "[{label: 'NroChamado', id: 'NroChamado', type: 'string'}, ";
                retorno += "{label: 'Sistema', id: 'Sistema', type: 'string'}, ";
                retorno += "{label: 'Versão', id: 'Versao', type: 'string'}, ";
                retorno += "{label: 'Descrição', id: 'Descricao', type: 'string'}, ";
                retorno += "{label: 'Data GMUD', id: 'DataGMUD', type: 'string'}, ";
                retorno += "{label: 'Qtd Chamado', id: 'QtdChamado', type: 'string'}], ";

                foreach (var gmud in listaGmud)
                {
                    retorno += "['" + gmud.NroGmud + "', ";
                    retorno += "'" + gmud.NomeSistema + "', ";
                    retorno += "'" + gmud.Versao + "', ";
                    retorno += "'" + gmud.DscGmud + "', ";
                    retorno += "'" + gmud.DataGmud.ToString("dd/MM/yyyy") + "', ";
                    retorno += "'" + gmud.QtdChamado.ToString() + "'], ";
                }
                retorno = retorno.Remove(retorno.Length - 2, 2);
                retorno += "]";
            }
            else
            {
                retorno = "[[{label: 'GMUD', id: 'Vazio', type: 'string'}], ['Nenhum registro encontrado!']]";
            }
            
            ucGmud.CarregarTabela(retorno);

            ltlGmud.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Gmud).DscPergunta;
            ltlGmudResp.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(filtro.DataConsulta, filtro.LoginAd, (int)TiposInfo.PerguntaKPI.Gmud).DscAnalise ?? "Sem resposta.";
            ltlGmudResp.Text = ltlGmudResp.Text.Replace(Environment.NewLine, "<br />");
        }

        private void CarregarHoraExtra(FiltroLoginPeriodo filtro)
        {
            var listaPonto = PontoBo.GetInstance.ConsultarPontoMensal(filtro.LoginAd, filtro.DataConsulta);
            
            var retorno = "[";

            if (listaPonto.Any())
            {
                retorno += "[{label: 'Nome', id: 'Nome', type: 'string'}, ";
                retorno += "{label: 'Atraso', id: 'Atraso', type: 'string'}, ";
                retorno += "{label: '50%', id: 'CinquentaPorcento', type: 'string'}, ";
                retorno += "{label: '100%', id: 'CemPorcento', type: 'string'}, ";
                retorno += "{label: 'Noturno', id: 'Noturno', type: 'string'}, ";
                retorno += "{label: 'Total', id: 'Total', type: 'string'}, ";
                retorno += "{label: 'Observação', id: 'Observacao', type: 'string'}], ";

                foreach (var ponto in listaPonto)
                {
                    retorno += "['" + ponto.Nome + "', ";
                    retorno += "'" + UtilBo.GetInstance.ObterTempoTotal(ponto.Atraso) + "', ";
                    retorno += "'" + UtilBo.GetInstance.ObterTempoTotal(ponto.CinquentaPorcento) + "', ";
                    retorno += "'" + UtilBo.GetInstance.ObterTempoTotal(ponto.CemPorcento) + "', ";
                    retorno += "'" + UtilBo.GetInstance.ObterTempoTotal(ponto.Noturno) + "', ";
                    retorno += "'" + UtilBo.GetInstance.ObterTempoTotal(ponto.CinquentaPorcento + ponto.CemPorcento + ponto.Noturno) + "', ";
                    retorno += "'" + ponto.Observacao.Replace(Environment.NewLine, "<br />") + "'], ";
                }
                retorno = retorno.Remove(retorno.Length - 2, 2);
                retorno += "]";
            }
            else
            {
                retorno = "[[{label: 'HoraExtra', id: 'Vazio', type: 'string'}], ['Nenhum registro encontrado!']]";
            }

            ucHoraExtra.CarregarTabela(retorno);
        }
    }
}