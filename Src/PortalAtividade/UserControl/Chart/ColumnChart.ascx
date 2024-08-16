<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ColumnChart.ascx.cs" Inherits="PortalAtividade.UserControl.Chart.ColumnChart" %>
<script>
    function drawColumnChart<%= ID %>() {
        var data = new google.visualization.arrayToDataTable(<%= hdDados.Value %>);                        
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
            height: 400,
            //width: 1200,
            chartArea: { width: '100%', height: '100%', left: 50, right: 20, top: 10, bottom: 50 },
            legend: {
                position: 'bottom',
                alignment: 'start',
                textStyle: {
                    fontSize: 13,
                    fontName: 'Verdana',
                    color: '#999999'
                }
            },
            bar: { groupWidth: '50%' },
            colors: ['#76B7B2', '#EDC948', '#F28E2B', '#E15759'],
            isStacked: true,
            hAxis: {
                textStyle: {
                    fontSize: 12,
                    fontName: 'Verdana',
                    color: '#999999'
                },
                baselineColor: '#F5F5F5'
            },
            vAxis: {
                gridlines: {
                    count: 4,
                    color: '#F0F0F0'
                },
                textStyle: {
                    fontSize: 13,
                    fontName: 'Verdana',
                    color: '#999999'
                },
                baselineColor: '#F0F0F0',
                ticks: <%= hdEscala.Value %>
            },
            annotations: {
                alwaysOutside: false,
                textStyle: {
                    fontSize: 12,
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

        var chart = new google.visualization.ColumnChart(document.getElementById('divColumnChart<%= ID %>'));
        chart.draw(view, options);
    }
</script>

<div id="divColumnChart<%= ID %>" style="width: 100%; height: 430px" class="pt-2 pb-4"></div>

<asp:HiddenField ID="hdDados" runat="server" />
<asp:HiddenField ID="hdEscala" runat="server" />