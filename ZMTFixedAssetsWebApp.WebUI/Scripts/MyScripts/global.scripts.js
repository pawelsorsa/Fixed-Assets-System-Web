function OnFailure(xhr) {
    $(document).ready(function (e) {
        $('#wrapper').html(xhr.responseText);
    });
}

$(document).ready(function (e) {
    global_url_getjson_sortby = "";
});

function getsortlist() {
    $(document).ready(function (e) {
        $.getJSON(global_url_getjson_sortby, function (data) {
            var select = "<select id='select_search' >";
            var items = "";
            var table = $("#mytable tr");
            var wynik = false;

            $.each(data, function (i, item) {
                wynik = false;
                table.each(function () {
                    var result = $(this).find("td").eq(0).html();
                    if (result.toString() == item.Text) { wynik = true; }
                });
                if (!wynik) {
                    items += "<option value=>" + item.Text + "</option>";
                }
            });
            var result = select.toString() + items.toString() + "</select><input type='submit' value='dodaj' id='add_field'/>"
            $('#sortlist').html(result);
        });
    });
}


$(document).ready(function (e) {
    $(".btn_delete").livequery("click", function () {
        var text = $(this).parent().parent().text();
        var tab_validtion = $("#validation tr");

        tab_validtion.each(function (i, l) {
            var result = $(this).find("td").eq(0).text();
            var tab_split = result.split(':');
            if (text.trim() == tab_split[1].trim()) {
                $(this).remove();
            }
        });
        $(this).parent().parent().remove();
        getsortlist();
    });
});

$(document).ready(function (e) {
    $("#add_field").livequery("click", function () {
        getsortlist();

        var x = $("#sortlist option:selected").text();
        var table = $("#mytable tr");

        var error = false;
        table.each(function () {
            var result = $(this).find("td").eq(0).html();
            if ((result.toString() == x)) { error = true; return; }
        });

        if (!error && x != "") {
            var items = "<td width='30%'>" + x + "</td><td width='50%' id='sort_row_name'><input type='text' width:400px/></td><td width='20%'><input type='button' class='btn_delete' value='usun'></input></td>";
            var result = "<tr>" + items.toString() + "</tr>"
            $('#mytable').append(result);
        }
    });
});


function OnCompleteEdit(url) {
    $(document).ready(function (e) {
        $("#form_unobtrusive").removeData("validator");
        $("#form_unobtrusive").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($("#form_unobtrusive"));
        var validator = $("#form_unobtrusive").validate();
        if (!validator) {
            LoadHistory(url);
        }

        // $('#OnSuccessEdit').show();
        // $('#OnSuccessEdit').slideUp(2000);
    });
}

function OnCompleteLogOnLogout(url) {
    $(document).ready(function (e) {
        $("#form_unobtrusive").removeData("validator");
        $("#form_unobtrusive").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($("#form_unobtrusive"));
        var validator = $("#form_unobtrusive").validate();
        if (!validator) {
            $('#logindisplay').html(geturl('Account/GetLogOnPartial'));
           // LoadHistory(url);
        }
        // $('#OnSuccessEdit').show();
        // $('#OnSuccessEdit').slideUp(2000);
    });
}


function LogOff(url) {
    $(document).ready(function (e) {
        geturl('Account/SignOut');
        $('#logindisplay').html(geturl('Account/GetLogOnPartial'));
       // LoadHistory(url);
    });
}

function geturl(addr) {
    var r = $.ajax({
        type: 'GET',
        url: addr,
        async: false
    }).responseText;
    return r;
}




