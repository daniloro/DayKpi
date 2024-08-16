<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BarChart.ascx.cs" Inherits="PortalAtividade.UserControl.Chart.BarChart" %>
<script>
    function drawBarChart<%= ID %>() {
        var data = new google.visualization.arrayToDataTable(<%= hdDados.Value %>);

        // set inner height to 30 pixels per row
        var chartAreaHeight = data.getNumberOfRows() * 30;
        var chartHeight = chartAreaHeight + 80;

        var view = new google.visualization.DataView(data);

        var columns = [];
        columns.push(0);
        IncluirColuna(1);
        IncluirColuna(2);
        IncluirColuna(3);
        IncluirColuna(4);

        function IncluirColuna(item) {
            columns.push(item);
            columns.push({
                calc: function (dt, row) {
                    return dt.getValue(row, item);
                },
                type: "number",
                role: "annotation"
            });
        }
        columns.push({
            calc: function (dt, row) {
                return 0;
            },
            label: "Total",
            type: "number",
        });
        columns.push({
            calc: function (dt, row) {
                return dt.getValue(row, 1) + dt.getValue(row, 2) + dt.getValue(row, 3) + dt.getValue(row, 4);
            },
            type: "number",
            role: "annotation"
        });
        view.setColumns(columns);

        var options = {
            //animation: {
            //    duration: 1000,
            //    easing: 'out',
            //    startup: true
            //},
            title: 'Chamados em Fila',
            titlePosition: 'none',
            titleTextStyle: {
                color: '#666666',
                fontName: 'Verdana',
                fontSize: 16,
                bold: true,
                italic: false
            },
            backgroundColor: 'transparent',
            height: chartHeight,
            //width: 900,                
            chartArea: { width: '80%', height: chartAreaHeight, left: 150, top: 0, bottom: 50 },
            legend: {
                position: 'bottom',
                alignment: 'start',
                textStyle: {
                    fontSize: 13,
                    fontName: 'Verdana',
                    color: '#999999'
                }
            },
            bar: { groupWidth: '70%' },
            colors: ['#76B7B2', '#EDC948', '#F28E2B', '#E15759'],
            isStacked: true,
            hAxis: {
                textPosition: 'none',
                gridlines: {
                    count: 0,
                    color: '#F5F5F5'
                },
                textStyle: {
                    fontSize: 12,
                    fontName: 'Verdana',
                    color: '#FFF'
                },
                baselineColor: '#F5F5F5'
            },
            vAxis: {
                textStyle: {
                    fontSize: 13,
                    fontName: 'Verdana',
                    color: '#999999'
                }
            },
            annotations: {
                alwaysOutside: false,
                textStyle: {
                    fontSize: 14,
                    fontName: 'Verdana',
                    color: 'transparent',
                    bold: false
                },
                datum: {
                    stem: {
                        color: 'transparent'
                    }
                }
            },
            series: {
                4: {
                    annotations: {
                        stem: {
                            color: 'transparent',
                            length: 10
                        },
                        textStyle: {
                            color: '#666666',
                            fontName: 'Verdana',
                            bold: true
                        }
                    },
                    enableInteractivity: false,
                    tooltip: 'none',
                    visibleInLegend: false
                }
            }
        };
        var chart = new google.visualization.BarChart(document.getElementById('divBarChart<%= ID %>'));
        chart.draw(view, options);
    }        
</script>

<div id="divBarChart<%= ID %>" class="pt-2" style="width: 100%"></div>

<asp:HiddenField ID="hdDados" runat="server" />