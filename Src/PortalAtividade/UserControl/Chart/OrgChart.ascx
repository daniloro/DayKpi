<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrgChart.ascx.cs" Inherits="PortalAtividade.UserControl.Chart.OrgChart" %>
<script>
    var orgChart;

    function drawOrgChart() {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Name');
        data.addColumn('string', 'Manager');
        data.addColumn('string', 'ToolTip');

        data.addRows(orgChart);

        var options = {
            allowHtml: true
        };

        var chart = new google.visualization.OrgChart(document.getElementById('divOrgChart'));
        chart.draw(data, options);
    }
</script>

<div id="divOrgChart"></div>