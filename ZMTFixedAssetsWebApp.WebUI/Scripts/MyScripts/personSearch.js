$(document).ready(function () {
    alert("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
});

        function getsortlist()
        {
            $(document).ready(function (e) {
                $.getJSON("/Person/Lists/SortByList", function (data) {
                    var select = "<select id='fff'>";
                    var items = "";
                    var divek = $("#mytable tr");
                    var wynik = false;

                    $.each(data, function (i, item) {
                        wynik = false;
                        divek.each(function () {
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
            $("#btn_delete").live("click", function () {
                var index = $(this).parent().parent().index();
                var tab_validtion = $("#validation tr");
                
                tab_validtion.each(function (i,l) {
                    if (i == index) 
                    {
                        $(this).remove();    
                    }
                });
                $(this).parent().parent().remove();

                getsortlist();
            });
        });

        $(document).ready(function (e) {
            $("#add_field").live("click", function () {
                getsortlist();

                var x = $("#sortlist option:selected").text();
                var table = $("#mytable tr");
                var wynik = false;
                table.each(function () {
                    var result = $(this).find("td").eq(0).html();
                    if ((result.toString() == x)) { wynik = true; }
                });

                if (!wynik && x != "") {
                    var items = "<td width='30%'>" + x + "</td><td width='50%'><input type='text'/></td><td width='20%'><input type='button' id='btn_delete' value='usun'></input></td>";
                    var result = "<tr>" + items.toString() + "</tr>"
                    $('#mytable').append(result);
                }
            });
        });




        $(document).ready(function (e) {
            $("#sortby").hide();
            $("#search_div").click(function () {
                $("#sortby").toggle();
                getsortlist();
            });
        });




        $(document).ready(function (e) {
            $("#btn_search").live("click", function () {

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
                            case "Imię":
                                query += "Name:" + result2;
                                break;
                            case "Nazwisko":
                                query += "Surname:" + result2;
                                break;
                            case "Sekcja":
                                query += "Section:" + result2;
                                break;
                            case "Email":
                                query += "Email:" + result2;
                                break;
                            case "Telefon":
                                query += "Phone:" + result2;
                                break;
                            case "Telefon kom.":
                                query += "Mobile:" + result2;
                                break;
                        }
                    }
                    else {
                        validation += "<tr><td>Proszę wprowadzić wartość w pole: <b>" + result + "</b></td></tr>";
                    }
                });

                if (validation == "") {
                    $.ajax(
                    {
                        type: "GET",
                        url: "/Person/List",
                        contentType: 'application/text; charset=utf-8',
                        data: { Query: query },
                        dataType: "text",
                        success: function (result) {
                            var domElement = $(result); // create element from html
                            $("#list").html(domElement); // append to end of list   
                        }
                    });
                }
                else {
                    $('#validation').empty();
                    $('#validation').css("color", "red");
                    $('#validation').append(validation.toString());
                }
            });
        });
