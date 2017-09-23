var grid = {
    t: "<table id=\"data-grid\" class=\"table table-bordered table-striped\"><thead><tr>{0}</tr><tbody>{1}</tbody></table><div style=\"display:none;\" id=\"record-info\">{2}</div>",
    col: [],
    selectedRecord: null,
    load: function (div, e) {
        grid.col = [];
        grid.selectedRecord = null;
        var obj = e[0];
        var h = grid.getheader(obj.fields);
        var records = grid.getrecords(obj.records)
        $(div).html(String.format(grid.t, h, records, JSON.stringify(obj)));
        $('.hide_col').hide();
        var table = $(div).find("#data-grid").DataTable({
            "paging": false,
            "ordering": false,
            "filter": false,
            "info":false,
        });
        table.on('draw', function () {
        });
    },
    getheader: function (e) {
        var headerTemplate = "<th class=\"{1}\">{0}</th>";
        var retVal = "";
        for (var i = 0; i <= e.length - 1; i++) {
            var visible = "show_col";
            if (e[i].visible == "0")
                visible = "hide_col";
            retVal = retVal + String.format(headerTemplate, e[i].label, visible);
            grid.col.push(e[i]);
        }
        return retVal;
    },
    getrecords: function (e) {
        var retVal = "";
        for (var i = 0; i <= e.length - 1; i++) {
            retVal = retVal + "<tr class=\"grid-rows\" style=\"cursor:pointer;\" row_index=\"" + i + "\" onclick=\"grid.selectrow(this);\">" + grid.getfields(e[i]) + "</tr>";
        }
        return retVal;
    },
    getfields: function (e) {
        var retVal = "";
        var fieldTemplate = "<td class=\"{1}\" enum=\"{2}\">{0}</td>";
        for (var i = 0; i <= grid.col.length - 1; i++) {
            var visible = "show_col";
            if (grid.col[i].visible == "0")
                visible = "hide_col";
            var dbfield = e[grid.col[i].dbfield];
            if (grid.col[i].enum == "")
                ENUM = "0"
            else
                ENUM = grid.col[i].enum;
            retVal = retVal + String.format(fieldTemplate, dbfield, visible, ENUM);
        }
        return retVal;
    },
    selectrow: function (e) {
        if ($(e).hasClass('grid-selected-row')) {
            $(e).removeClass('grid-selected-row');
            grid.selectedRecord = null;
        } else {
            $('.grid-rows').removeClass('grid-selected-row');
            $(e).addClass('grid-selected-row');
            var temp = $.parseJSON($('#record-info').html())
            grid.selectedRecord = temp.records[parseInt($(e).attr('row_index'))];
        }
    },
}

String.format = function () {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }
    return s;
}