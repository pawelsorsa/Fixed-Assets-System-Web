$(document).ready(function (e) {
    $("#sortby").livequery(function () { $(this).hide(); });

    $("#search_div").livequery('click', function () {
        $("#sortby").toggle();
        getsortlist();
    });

    $("#btn_search").livequery('click', function () {
        var table = $("#mytable tr");
        var wynik = false;
        var query = "";
        var validation = "";
        table.each(function () {
            var result = $(this).find("td").eq(0).html();
            var result2 = $(this).find("td").eq(1).children().val();

            if (result2 != "") {
                if (query.length > 0) { query += ","; }
                switch (result) {
                    case "ID":
                        query += "ID:" + result2;
                        break;
                    case "Nr. inwentarzowy":
                        query += "InwNumber:" + result2;
                        break;
                    case "Licencja":
                        query += "Licence:" + result2;
                        break;
                    case "Przypisany środek trwały":
                        query += "AssignFixedAsset:" + result2;
                        break;
                    case "Utworzony przez":
                        query += "CreatedBy:" + result2;
                        break;
                    case "Data ost. modyfikacji":
                        query += "LastModifiedDate:" + result2;
                        break;
                    case "Ostatnio edytowany przez":
                        query += "LastModifiedLogin:" + result2;
                        break;
                }
            }
            else {
                validation += "<tr><td>Proszę wprowadzić wartość w pole: <b>" + result + "</b></td></tr>";
            }
        });

        if (validation == "") {
            $("#sortby").hide();
            $('#validation').empty();
            $.ajax(
                    {
                        type: "GET",
                        url: "/Licence/List",
                        contentType: 'application/text; charset=utf-8',
                        data: { Query: query },
                        dataType: "text",
                        success: function (result) {
                            var domElement = $(result); // create element from html
                            $("#ajax_content").html(domElement); // append to end of list   
                        }
                    });

        }
        else {
            $('#validation').empty();
            $('#validation').css("color", "RED");
            $('#validation').append(validation.toString());
        }
    });
});