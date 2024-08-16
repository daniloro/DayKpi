<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Bar.ascx.cs" Inherits="PortalAtividade.UserControl.Chart.Bar" %>
<script>
    var dadosBar;

    function drawBar() {        
        var data = google.visualization.arrayToDataTable(dadosBar);

        var options = {
            chart: {
                title: 'Em Fila Mensal',
                subtitle: 'Chamados na Fila dos Últimos 6 Meses',
            },
            isStacked: false
        };

        var chartBar = new google.charts.Bar(document.getElementById('divBar'));
        chartBar.draw(data, google.charts.Bar.convertOptions(options));
    }
</script>

<div id="divBar" style="width: 100%; height: 550px;" class="pt-4 pb-4"></div>
