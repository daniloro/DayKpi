google.charts.load('current', { packages: ['orgchart'] });

var dadosChart1;

function drawChart() {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Name');
    data.addColumn('string', 'Manager');
    data.addColumn('string', 'ToolTip');

    data.addRows(dadosChart1);

    var options = {
        allowHtml: true
    };

    // Instantiate and draw the chart.
    var chart = new google.visualization.OrgChart(document.getElementById('container'));
    google.visualization.events.addListener(chart, 'select', toggleDisplay);
    chart.draw(data, options);

    function toggleDisplay() {
        var selection = chart.getSelection();

        if (selection.length > 0) {
            $("[id$=hdLogin]").val(data.getValue(selection[0].row, 0));

            var nome = data.getFormattedValue(selection[0].row, 0);

            if (~nome.indexOf("<")) {
                nome = nome.substring(0, nome.indexOf("<"));
            }
            $("[id$=txtNome]").val(nome);
        }
    }
}