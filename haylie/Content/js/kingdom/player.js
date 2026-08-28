function player_verifyMe() {
    if ($("#box_login #key").val().length > 0 || $("#box_login #key").val().length > 0) {
        $("#box_login #btn_go").fadeOut();
        cronos_loading(1);
        player_logmein($("#box_login #mail").val(), $("#box_login #key").val());
    }    
}
function player_logmein(p_user, p_key) {
    //var setplayer = $("#hdnsuper" + $("#hdnplayert").val()).val();

    $.get("http://haylie.bosswebapps.net/api/player/logmein", {
        p_user: p_user, p_key: p_key
    }, function (data) {
        console.log(data);
        response = JSON.parse(data.Value);
        var setpermission = $("#hdnplayert").val() == "2" ? 2 : 1;

        if (response.id > 0) {
            console.log('access granted');
           /* $.get("/Account/LogMeIn", {
                p_param: '{ mail: "' + setplayer + '", keycode: "' + p_key + '", id: ' + response.id + ', template: "' + response.template + '", token: "' + response.token + '", id_permission: ' + setpermission + ' }'
            }, function (data) {
                //document.location.href = "/Home/Index";
                document.location.reload();
            }); */
        }
        else {
            console.log('access denied');

            cronos_loading(0);
            $("#box_login #btn_go").css("color", "#ff0000");
            $("#box_login #btn_go").fadeIn();
            setTimeout(function () { $("#box_login #btn_go").css("color", "#117a8b"); }, 1800); 
        }
        
    });
}
function player_setConfig() {
    if ($("#page_config #superkey").val().length >= 9) {
        cronos_loading(1);

        //$.get("http://localhost:33910/api/player/super",
        $.get("http://haylie.bosswebapps.net/api/player/super",
            { p_param: $("#page_config #superkey").val(), p_token: $("#hdnplayer").val() },
        function (data) {
            
            $("#page_config #btn_go").css("color", "#3d0");
            $("#page_config  input").val('');
            cronos_loading(0);
            setTimeout(function () { $("#page_config #btn_go").css("color", "#117a8b"); }, 1800);
        });
    }
    else {
        $("#page_config #btn_go").css("color", "#ff0000");
    }
    setTimeout(function () { $("#page_config #btn_go").css("color", "#117a8b"); }, 1800);
}
function player_getMeOut() {
    $.get("/Account/Bye", {
        p_param: ""
    }, function (data) {
        //courier_alert('', '.trinity > Bye', data.msg, true);
        if (data.result == true) {
            document.location.href = "/Home/Index";
        }
    });
}
