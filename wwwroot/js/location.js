$(window).ready(() => {
    var oldUrl = $("#user_local").attr('href');
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition((position) => {
            $("#user_local").attr('href',
                `${oldUrl}&userLocal=${position.coords.latitude.toFixed(2)}:${position.coords.longitude.toFixed(2)}`);
            $("#user_local").removeClass("disabled");
        });
    } else {
        alert("Geolocation is not supported by this browser.");
    }

    $(document).keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            $("#submitbtn").click();
            return false;
        }
    });
})