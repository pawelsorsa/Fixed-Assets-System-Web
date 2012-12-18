$(document).ready(function () {
    //Check if url hash value exists (for bookmark)
    $.history.init(pageload);
    $('a[rel=ajax]').livequery("click", function () {
        var hash = this.href.replace("http://", "");
        var url = hash.substring(window.location.host.length);
        LoadHistory(url);
        return false;
    });
});

function LoadHistory(url) {
    if (window.location.pathname == "/") {
        $.history.load(url);
        $('#ajax_main_content').hide();
        $('#loading').show();
        getPage(url);
    }
    else 
    {
        window.location.href = "/";
    }
}

function pageload(hash) {
    if (hash) getPage(hash);
}

function getPage(url_var) {
    $.ajax({
        url: url_var,
        type: "GET",
        data: "",
        cache: false,
        success: function (html) {
            $('#loading').hide();
            $('#ajax_main_content').html(html);
            $('#ajax_main_content').fadeIn('slow');
        }
    });
}

