﻿$(document).ready(function (e) {
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
                    case "Nazwa roli":
                        query += "RoleName:" + result2;
                        break;
                    case "Nazwa użytkownika":
                        query += "UserName:" + result2;
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
                        url: "/MembershipRole/List",
                        contentType: 'application/text; charset=utf-8',
                        data: { Query: query },
                        dataType: "text",
                        success: function (result) {

                            //$("#ccc").html(result);
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