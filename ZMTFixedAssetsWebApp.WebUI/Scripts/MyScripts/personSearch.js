        function getsortlist()
        {
            $(document).ready(function (e) {
                $.getJSON("/Person/Lists/SortByList", function (data) {
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
            $("#btn_delete").live("click", function () {
                var text = $(this).parent().parent().text();
                var tab_validtion = $("#validation tr");
               // alert(text);
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
                    var items = "<td width='30%'>" + x + "</td><td width='50%' id='sort_row_name'><input type='text' width:400px/></td><td width='20%'><input type='button' id='btn_delete' value='usun'></input></td>";
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

         $("#btn_search").live("click", function() {
            
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
                    $('#validation').empty();
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
                    $('#validation').css("color", "RED");
                    $('#validation').append(validation.toString());
                }
        });

