function OnFailure(xhr) {
    $(document).ready(function (e) {
        $('#wrapper').html(xhr.responseText);
    });
}

$(document).ready(function (e) {
    
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
            LoadHistory(url);
        }
        // $('#OnSuccessEdit').show();
        // $('#OnSuccessEdit').slideUp(2000);
    });
}


function LogOff(url) {
    $(document).ready(function (e) {
        geturl('Account/SignOut');
        $('#logindisplay').html(geturl('Account/GetLogOnPartial'));
        LoadHistory(url);
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
