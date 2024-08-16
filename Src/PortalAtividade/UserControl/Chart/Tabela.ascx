<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tabela.ascx.cs" Inherits="PortalAtividade.UserControl.Chart.Tabela" %>
<script>
    function drawChartTabela<%= ID %>() {        

        var data = new google.visualization.arrayToDataTable(<%= hdDados.Value %>);

        ////Set the euro formatting for the data
        //var dtEuro = new google.visualization.NumberFormat(formatterEuro)            
        //dtEuro.format(data, 4);

        var StatusTable = new google.visualization.Table(document.getElementById('divTabela<%= ID %>'));

        google.visualization.events.addListener(StatusTable, 'ready', function () {
            resetStyling('divTabela<%= ID %>');
        });

        google.visualization.events.addListener(StatusTable, 'sort', function (ev) {
            var parentRow = $('#divTabela<%= ID %> td.TotalCell').parent();
            if (!parentRow.is(':last-child')) {
                parentRow.siblings().last().after(parentRow);
            }
            resetStyling('divTabela<%= ID %>');
        });

        var options = {
            showRowNumber: false,
            allowHtml: true,
            width: '100%',
            height: 'auto',
            cssClassNames: { headerCell: 'googleHeaderCell' }
        };

        //var monthYearFormatter = new google.visualization.DateFormat({
        //    pattern: "dd-MM-yyy"
        //});
        //monthYearFormatter.format(data, 0);

        StatusTable.draw(data, options);
    }

    //remove the google chart styling and add the bootstrap styling
    //Also add the css class Totalrow
    function resetStyling(id) {
        $('#' + id + ' table')
            .removeClass('google-visualization-table-table')
            .addClass('table table-bordered table-condensed table-striped table-hover');
        var parentRow = $('#' + id + ' td.TotalCell').parent();
        parentRow.addClass('TotalRow');
    }

    ////Declare formats
    //var formatterEuro = {
    //    decimalSymbol: ',',
    //    groupingSymbol: '.',
    //    negativeColor: 'red',
    //    negativeParens: true,
    //    prefix: '' //'\u20AC '
    //};
</script>

<asp:HiddenField ID="hdDados" runat="server" />

<div class="ChartOverview">
    <div id="divTabela<%= ID %>"></div>
</div>
