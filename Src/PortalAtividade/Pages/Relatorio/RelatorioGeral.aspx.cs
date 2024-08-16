using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.Pages.Relatorio
{
    public partial class RelatorioGeral : BasePage
    {
        private List<RelEmFilaInfo> listaEmFila;
        private List<RelAbertoConcluidoInfo> listaAbertoConcluido;
        
        private enum ColAtividade
        {
            Grupo = 0,
            Dev,
            Definicao,
            CodeReview,
            EmMerge,            
            NaFila,
            Vencido
        }

        private enum ColAbertoConcluido
        {
            Grupo = 0,
            Mes1,
            Mes2,
            Mes3
        }

        private enum ColConcluido
        {
            Nome = 0,            
            Desenvolvimento,            
            Incidente,
            Definicao,
            Comite,            
            MergePacote,
            Gmud,
            Outros,
            Total
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ucFiltro.CarregarFiltro(UsuarioAD, false, true);                
            }
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {
            CarregarAtividade();            
        }

        private void CarregarAtividade()
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var dataProximoMes = dataConsulta.AddMonths(1);

            // Em fila
            listaEmFila = RelatorioBo.GetInstance.ConsultarEmFila(UsuarioAD, dataProximoMes, dataProximoMes);                   

            if (ucFiltro.Grupo != "" && ucFiltro.Grupo != "0")
                listaEmFila.RemoveAll(p => p.Grupo != ucFiltro.Grupo);            

            listaEmFila = listaEmFila.Select(p => { p.Grupo = p.Grupo.Replace("Desenv_", ""); return p; }).OrderBy(p => p.Grupo).ToList();
            var listaGrupo = listaEmFila.Select(p => p.Grupo).Distinct().ToList();
            
            gdvAtividade.ShowFooter = listaGrupo.Count > 1;
            
            gdvAtividade.DataSource = listaGrupo;
            gdvAtividade.DataBind();

            if (Usuario.CodTipoEquipe != (int)TiposInfo.TipoEquipe.Desenvolvimento)
            {
                gdvAtividade.Columns[1].Visible = false;
                gdvAtividade.Columns[2].Visible = false;
                gdvAtividade.Columns[3].Visible = false;
                gdvAtividade.Columns[4].Visible = false;
            }

            var listaEmFilaChart = RelatorioBo.GetInstance.ConsultarEmFilaChart(listaEmFila, dataProximoMes);
            ucBarChart.CarregarBarChart(listaEmFilaChart);

            var listaEmFilaSeisMeses = RelatorioBo.GetInstance.ConsultarEmFilaSeisMesesChart(UsuarioAD, dataConsulta.AddMonths(-5), dataProximoMes);

            if (ucFiltro.Grupo != "" && ucFiltro.Grupo != "0")
                listaEmFilaSeisMeses.RemoveAll(p => p.Descricao != ucFiltro.Grupo);

            listaEmFilaSeisMeses = listaEmFilaSeisMeses.Select(p => { p.Descricao = p.Descricao.Replace("Desenv_", ""); return p; }).OrderBy(p => p.Descricao).ToList();
            ucBar.CarregarBar(dataConsulta, listaEmFilaSeisMeses);


            // Aberto Concluido
            listaAbertoConcluido = RelatorioBo.GetInstance.ConsultarAbertoConcluido(UsuarioAD, dataConsulta);

            if (ucFiltro.Grupo != "" && ucFiltro.Grupo != "0")
                listaAbertoConcluido.RemoveAll(p => p.Grupo != ucFiltro.Grupo);

            listaAbertoConcluido = listaAbertoConcluido.Select(p => { p.Grupo = p.Grupo.Replace("Desenv_", ""); return p; }).OrderBy(p => p.Grupo).ToList();

            gdvAbertoConcluido.ShowFooter = listaAbertoConcluido.Count > 1;
            gdvAbertoConcluido.DataSource = listaAbertoConcluido;
            
            gdvAbertoConcluido.Columns[(int)ColAbertoConcluido.Mes1].HeaderText = UtilBo.GetInstance.ObterNomeMes(dataConsulta.AddMonths(-2).Month);
            gdvAbertoConcluido.Columns[(int)ColAbertoConcluido.Mes2].HeaderText = UtilBo.GetInstance.ObterNomeMes(dataConsulta.AddMonths(-1).Month);
            gdvAbertoConcluido.Columns[(int)ColAbertoConcluido.Mes3].HeaderText = UtilBo.GetInstance.ObterNomeMes(dataConsulta.Month);
            gdvAbertoConcluido.DataBind();
            

            // Concluidos no mês selecionado
            ltlConcluidoMes1.Text = "Concluídos em " + UtilBo.GetInstance.ObterNomeMes(dataConsulta.Month);

            var listaRelatorioNovo = RelatorioBo.GetInstance.ConsultarConcluidoConsolidado(UsuarioAD, dataConsulta, dataProximoMes);
            GerarConcluidoMes(listaRelatorioNovo, ucFiltro.Grupo);
        }

        private void GerarConcluidoMes(List<RelConcluidoConsolidadoInfo> listaRelatorio, string grupo)
        {
            var listaOperador = OperadorBo.GetInstance.ConsultarOperadorGestor(UsuarioAD);

            if (grupo != "" && grupo != "0")
            {
                listaRelatorio.RemoveAll(p => p.Grupo != grupo);
                listaOperador.RemoveAll(p => p.Grupo != grupo);
            }

            var listaOperadorCompleto = listaRelatorio.Select(p => p.Nome).Distinct().ToList();
            listaOperadorCompleto.AddRange(listaOperador.FindAll(p => !listaOperadorCompleto.Contains(p.Nome)).Select(p => p.Nome));
            listaOperadorCompleto.Sort();

            var listaPlanejador = PlanejadorBo.GetInstance.ConsultarPlanejador(UsuarioAD);

            var retorno = "[";

            if (listaOperadorCompleto.Any())
            {
                retorno += "[{label: 'Nome', id: 'Nome', type: 'string'}, ";

                foreach (var item in listaPlanejador)
                {
                    retorno += "{label: '" + item.Abreviado + "', id: 'Atividade_" + item.CodPlanejador.ToString() + "', type: 'number'}, ";
                }
                retorno += "{label: 'ReqSimples', id: 'ReqSimples', type: 'number'}, ";
                retorno += "{label: 'Incidente', id: 'Incidente', type: 'number'}, ";
                retorno += "{label: 'Outros', id: 'Outros', type: 'number'}, ";
                retorno += "{label: 'Total', id: 'Total', type: 'number'}], ";
                                
                var qtd = 0;

                foreach (var operador in listaOperadorCompleto)
                {
                    retorno += "['" + operador + "', ";

                    foreach (var plan in listaPlanejador)
                    {
                        qtd = listaRelatorio.FindAll(p => p.Nome == operador && p.CodPlanejador == plan.CodPlanejador)?.Sum(p => p.Quantidade) ?? 0;
                        retorno += qtd.ToString() + ", ";
                    }

                    qtd = listaRelatorio.FindAll(p => p.Nome == operador && p.TipoChamado == "Req. Simples")?.Sum(p => p.Quantidade) ?? 0;
                    retorno += qtd.ToString() + ", ";

                    qtd = listaRelatorio.FindAll(p => p.Nome == operador && (p.TipoChamado == "Incidente" || p.TipoChamado == "Incidente Parcial"))?.Sum(p => p.Quantidade) ?? 0;                    
                    retorno += qtd.ToString() + ", ";

                    qtd = listaRelatorio.FindAll(p => p.Nome == operador && p.TipoChamado == "Atividade" && !listaPlanejador.Any(q => q.CodPlanejador == p.CodPlanejador))?.Sum(p => p.Quantidade) ?? 0;                    
                    retorno += qtd.ToString() + ", ";

                    // TOTAL                    
                    qtd = listaRelatorio.FindAll(p => p.Nome == operador)?.Sum(p => p.Quantidade) ?? 0;                    
                    retorno += qtd.ToString() + "], ";
                }
                    
                retorno += "[{ v: 'Total', p: { className: 'TotalCell' } }, ";

                foreach (var plan in listaPlanejador)
                {                    
                    qtd = listaRelatorio.FindAll(p => p.CodPlanejador == plan.CodPlanejador)?.Sum(p => p.Quantidade) ?? 0;                    
                    retorno += qtd.ToString() + ", ";
                }

                qtd = listaRelatorio.FindAll(p => p.TipoChamado == "Req. Simples")?.Sum(p => p.Quantidade) ?? 0;
                retorno += qtd.ToString() + ", ";

                qtd = listaRelatorio.FindAll(p => p.TipoChamado == "Incidente" || p.TipoChamado == "Incidente Parcial")?.Sum(p => p.Quantidade) ?? 0;                
                retorno += qtd.ToString() + ", ";

                qtd = listaRelatorio.FindAll(p => p.TipoChamado == "Atividade" && !listaPlanejador.Any(q => q.CodPlanejador == p.CodPlanejador))?.Sum(p => p.Quantidade) ?? 0;
                retorno += qtd.ToString() + ", ";

                qtd = listaRelatorio?.Sum(p => p.Quantidade) ?? 0;
                retorno += qtd.ToString() + "] ";
                
                retorno += "]";
            }
            else
            {
                retorno = "[[{label: 'Concluídos', id: 'Vazio', type: 'string'}], ['Nenhum registro encontrado!']]";
            }

            ucConcluido.CarregarTabela(retorno);
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
                string grupo = (string)e.Row.DataItem;

                e.Row.Cells[(int)ColAtividade.Grupo].Text = grupo;
                e.Row.Cells[(int)ColAtividade.Dev].Text = listaEmFila.Count(p => p.Grupo == grupo && !ObterPlanejadorAuxiliar().Contains(p.CodPlanejador)).ToString();
                e.Row.Cells[(int)ColAtividade.Definicao].Text = listaEmFila.Count(p => p.Grupo == grupo && p.CodPlanejador == (int)TiposInfo.Planejador.Definicao).ToString();
                e.Row.Cells[(int)ColAtividade.CodeReview].Text = listaEmFila.Count(p => p.Grupo == grupo && p.CodPlanejador == (int)TiposInfo.Planejador.CodeReview).ToString();
                e.Row.Cells[(int)ColAtividade.EmMerge].Text = listaEmFila.Count(p => p.Grupo == grupo && p.CodPlanejador == (int)TiposInfo.Planejador.Merge).ToString();
                e.Row.Cells[(int)ColAtividade.NaFila].Text = listaEmFila.Count(p => p.Grupo == grupo).ToString();
                e.Row.Cells[(int)ColAtividade.Vencido].Text = listaEmFila.Count(p => p.Grupo == grupo && p.CodStatus == 0 && p.DataFinal.Date < DateTime.Today).ToString();

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[(int)ColAtividade.Dev].Text = listaEmFila.Count(p => !ObterPlanejadorAuxiliar().Contains(p.CodPlanejador)).ToString();
                e.Row.Cells[(int)ColAtividade.Definicao].Text = listaEmFila.Count(p => p.CodPlanejador == (int)TiposInfo.Planejador.Definicao).ToString();
                e.Row.Cells[(int)ColAtividade.CodeReview].Text = listaEmFila.Count(p => p.CodPlanejador == (int)TiposInfo.Planejador.CodeReview).ToString();
                e.Row.Cells[(int)ColAtividade.EmMerge].Text = listaEmFila.Count(p => p.CodPlanejador == (int)TiposInfo.Planejador.Merge).ToString();
                e.Row.Cells[(int)ColAtividade.NaFila].Text = listaEmFila.Count().ToString();
                e.Row.Cells[(int)ColAtividade.Vencido].Text = listaEmFila.Count(p => p.CodStatus == 0 && p.DataFinal.Date < DateTime.Today).ToString();
            }
        }

        private int[] ObterPlanejadorAuxiliar()
        {            
            return new int[] { (int)TiposInfo.Planejador.Definicao, (int)TiposInfo.Planejador.CodeReview, (int)TiposInfo.Planejador.Merge };
        }

        protected void GdvAbertoConcluido_PreRender(object sender, EventArgs e)
        {
            if (gdvAbertoConcluido.Rows.Count > 0)
            {
                gdvAbertoConcluido.UseAccessibleHeader = true;
                gdvAbertoConcluido.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvAbertoConcluido.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvAbertoConcluido_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RelAbertoConcluidoInfo abertoConcluido = (RelAbertoConcluidoInfo)e.Row.DataItem;
                e.Row.Cells[(int)ColAbertoConcluido.Grupo].Text = abertoConcluido.Grupo;

                var grupo1 = new GrupoMovtoInfo
                {
                    LtlAberto = (Literal)e.Row.FindControl("ltlAberto1"),
                    LtlConcluido = (Literal)e.Row.FindControl("ltlConcluido1"),
                    LtlCancelado = (Literal)e.Row.FindControl("ltlCancelado1"),
                    LtlBacklog = (Label)e.Row.FindControl("lblBacklog1"),
                    LtlEmFila = (Literal)e.Row.FindControl("ltlEmFila1"),
                    Aberto = abertoConcluido.AbertoMes1,
                    Concluido = abertoConcluido.ConcluidoMes1,
                    Cancelado = abertoConcluido.CanceladoMes1,
                    EmFila = abertoConcluido.EmFilaMes1
                };

                var grupo2 = new GrupoMovtoInfo
                {
                    LtlAberto = (Literal)e.Row.FindControl("ltlAberto2"),
                    LtlConcluido = (Literal)e.Row.FindControl("ltlConcluido2"),
                    LtlCancelado = (Literal)e.Row.FindControl("ltlCancelado2"),
                    LtlBacklog = (Label)e.Row.FindControl("lblBacklog2"),
                    LtlEmFila = (Literal)e.Row.FindControl("ltlEmFila2"),
                    Aberto = abertoConcluido.AbertoMes2,
                    Concluido = abertoConcluido.ConcluidoMes2,
                    Cancelado = abertoConcluido.CanceladoMes2,
                    EmFila = abertoConcluido.EmFilaMes2
                };
                
                var grupo3 = new GrupoMovtoInfo
                {
                    LtlAberto = (Literal)e.Row.FindControl("ltlAberto3"),
                    LtlConcluido = (Literal)e.Row.FindControl("ltlConcluido3"),
                    LtlCancelado = (Literal)e.Row.FindControl("ltlCancelado3"),
                    LtlBacklog = (Label)e.Row.FindControl("lblBacklog3"),
                    LtlEmFila = (Literal)e.Row.FindControl("ltlEmFila3"),
                    Aberto = abertoConcluido.AbertoMes3,
                    Concluido = abertoConcluido.ConcluidoMes3,
                    Cancelado = abertoConcluido.CanceladoMes3,
                    EmFila = abertoConcluido.EmFilaMes3
                };

                AgruparMovimentoMes(grupo1);
                AgruparMovimentoMes(grupo2);
                AgruparMovimentoMes(grupo3);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[(int)ColAbertoConcluido.Grupo].Text = "Total";
                e.Row.Cells[(int)ColAbertoConcluido.Grupo].Font.Bold = true;

                var grupo1 = new GrupoMovtoInfo
                {
                    LtlAberto = (Literal)e.Row.FindControl("ltlAberto1"),
                    LtlConcluido = (Literal)e.Row.FindControl("ltlConcluido1"),
                    LtlCancelado = (Literal)e.Row.FindControl("ltlCancelado1"),
                    LtlBacklog = (Label)e.Row.FindControl("lblBacklog1"),
                    LtlEmFila = (Literal)e.Row.FindControl("ltlEmFila1"),
                    Aberto = listaAbertoConcluido.Sum(p => p.AbertoMes1),
                    Concluido = listaAbertoConcluido.Sum(p => p.ConcluidoMes1),
                    Cancelado = listaAbertoConcluido.Sum(p => p.CanceladoMes1),
                    EmFila = listaAbertoConcluido.Sum(p => p.EmFilaMes1)
                };                

                var grupo2 = new GrupoMovtoInfo
                {
                    LtlAberto = (Literal)e.Row.FindControl("ltlAberto2"),
                    LtlConcluido = (Literal)e.Row.FindControl("ltlConcluido2"),
                    LtlCancelado = (Literal)e.Row.FindControl("ltlCancelado2"),
                    LtlBacklog = (Label)e.Row.FindControl("lblBacklog2"),
                    LtlEmFila = (Literal)e.Row.FindControl("ltlEmFila2"),
                    Aberto = listaAbertoConcluido.Sum(p => p.AbertoMes2),
                    Concluido = listaAbertoConcluido.Sum(p => p.ConcluidoMes2),
                    Cancelado = listaAbertoConcluido.Sum(p => p.CanceladoMes2),
                    EmFila = listaAbertoConcluido.Sum(p => p.EmFilaMes2)
                };                

                var grupo3 = new GrupoMovtoInfo
                {
                    LtlAberto = (Literal)e.Row.FindControl("ltlAberto3"),
                    LtlConcluido = (Literal)e.Row.FindControl("ltlConcluido3"),
                    LtlCancelado = (Literal)e.Row.FindControl("ltlCancelado3"),
                    LtlBacklog = (Label)e.Row.FindControl("lblBacklog3"),
                    LtlEmFila = (Literal)e.Row.FindControl("ltlEmFila3"),
                    Aberto = listaAbertoConcluido.Sum(p => p.AbertoMes3),
                    Concluido = listaAbertoConcluido.Sum(p => p.ConcluidoMes3),
                    Cancelado = listaAbertoConcluido.Sum(p => p.CanceladoMes3),
                    EmFila = listaAbertoConcluido.Sum(p => p.EmFilaMes3)
                };

                AgruparMovimentoMes(grupo1);
                AgruparMovimentoMes(grupo2);
                AgruparMovimentoMes(grupo3);
            }
        }

        private void AgruparMovimentoMes(GrupoMovtoInfo grupo)
        {
            grupo.LtlAberto.Text = grupo.Aberto.ToString();
            grupo.LtlConcluido.Text = grupo.Concluido.ToString();
            grupo.LtlCancelado.Text = grupo.Cancelado.ToString();
            grupo.LtlEmFila.Text = grupo.EmFila.ToString();

            var backlog1 = grupo.Aberto - grupo.Concluido - grupo.Cancelado;

            if (backlog1 > 0)
            {
                grupo.LtlBacklog.ForeColor = Color.Red;
                grupo.LtlBacklog.Text = "+" + backlog1.ToString();
            }
            else
            {
                if (backlog1 < 0)
                    grupo.LtlBacklog.ForeColor = Color.Green;
                grupo.LtlBacklog.Text = backlog1.ToString();
            }
        }

        private class GrupoMovtoInfo
        {
            public Literal LtlAberto { get; set; }
            public Literal LtlConcluido { get; set; }
            public Literal LtlCancelado { get; set; }
            public Label LtlBacklog { get; set; }
            public Literal LtlEmFila { get; set; }
            public int Aberto { get; set; }
            public int Concluido { get; set; }
            public int Cancelado { get; set; }
            public int EmFila { get; set; }
        }

        protected void BtnExcel_EmFila_Click(object sender, EventArgs e)
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes).AddMonths(1);
            var listaRelatorio = RelatorioBo.GetInstance.ConsultarEmFila(UsuarioAD, dataConsulta, dataConsulta);
            listaRelatorio = listaRelatorio.Select(p => { p.DataConclusao = p.DataConclusao == DateTime.MinValue ? null : p.DataConclusao; return p; }).ToList();

            if (ucFiltro.Grupo != "" && ucFiltro.Grupo != "0")
                listaRelatorio.RemoveAll(p => p.Grupo != ucFiltro.Grupo);

            var listaExcel = listaRelatorio.Select(p => new {
                p.NroChamado,
                p.DscChamado,
                p.TipoChamado,
                p.NroAtividade,
                p.DscAtividade,
                p.Grupo,
                p.Operador,
                p.DataAbertura,
                p.DataFinal,
                p.DataConclusao
            }).ToList();

            GridView gvRelatorio = new GridView();
            gvRelatorio.DataSource = listaExcel;
            gvRelatorio.DataBind();

            if (listaExcel.Any())
            {
                gvRelatorio.Rows[0].Cells[1].Width = 500; //DscChamado
                gvRelatorio.Rows[0].Cells[4].Width = 500; //DscAtividade
            }

            GerarExcel(gvRelatorio);
        }

        protected void BtnExcel_Concluido_Click(object sender, EventArgs e)
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var listaRelatorio = RelatorioBo.GetInstance.ConsultarConcluido(UsuarioAD, dataConsulta, dataConsulta.AddMonths(1));
            
            if (ucFiltro.Grupo != "" && ucFiltro.Grupo != "0")
                listaRelatorio.RemoveAll(p => p.Grupo != ucFiltro.Grupo);

            var listaExcel = listaRelatorio.Select(p => new {
                p.Nome,
                p.Grupo,
                p.DscPlanejador,
                p.NroAtividade,
                p.DscAtividade,
                p.DataAbertura,
                p.DataConclusao,
                p.NroChamado,
                p.DscChamado
            }).ToList();

            GridView gvRelatorio = new GridView();
            gvRelatorio.DataSource = listaExcel;
            gvRelatorio.DataBind();

            if (listaExcel.Any())
            {
                gvRelatorio.Rows[0].Cells[4].Width = 500; //DscAtividade
                gvRelatorio.Rows[0].Cells[8].Width = 500; //DscChamado
            }

            GerarExcel(gvRelatorio);
        }
    }
}