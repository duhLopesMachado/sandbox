var navstate = 0;
function morgana_talkToTheBeyond(p_what, p_where, p_param) {
    var tlAux = new TimelineMax({ repeat: 0, repeatDelay: 0 });
    var eLoginContainer = $("#box_login #container");
    var eLoginClientLayer = $("#box_login #layer_client");
    $.get(p_where, {
        p_param: p_param
    }, function (data) {
        if (data.result == true || data.msg === undefined) {
            //if (p_what == "products" || p_what == partners)

            switch (p_what) {
                case "categ":
                case "product":
                case "partner":
                    $("#hdnorigin").val("");
                    blacksmith_page(p_what + "s");
                    break;

                case "order":
                    $('#page_orders .list #li' + p_param).remove();
                    break;

                case "settings":
                    blacksmith_page("home");
                    break;

                case "product_stock":
                    var stockPrdId = $("#page_products .step3 #search_products select").val();
                    var stockPrdV = $("#page_products .step3 .display").val();
                    $('#page_products .step1 .list #li' + stockPrdId + " #hdn_product_stock").val(stockPrdV);

                    if ($('#hdnproductstocknavto').val().length > 0) {
                        $('#page_products .step3 #search_products select').val($('#hdnproductstocknavto').val());

                        $("#page_products .step3 .display").val($('#page_products .step1 .list #li' + $('#hdnproductstocknavto').val() + " #hdn_product_stock").val());

                        var sV = $("#page_products .step3 .display").val();
                        sV = sV.length <= 0 ? "0" : sV;
                        if (sV.indexOf(",") >= 0) { sV = sV.substr(sV.indexOf(",") + 1); }
                        if (sV.indexOf(".") >= 0) { sV = sV.substr(sV.indexOf(".") + 1); }

                        $('#page_products .step3 .bottle a[rel="' + sV + '"]').click();

                    }


                    break;

                case "order_register":
                    break;

                case "order_create_desktop":
                    $("#hdnneworder").val(data.Value);
                    var orderpartner = $("#hdnneworderpartner").val();
                    var partner_name = $('#page_partners .list #li' + orderpartner + " #hdn_partner_name").val();
                    var count_integration = 0;
                    if (parseInt($("#hdnneworderlength").val()) > 1) {
                        $('#page_partnerorder_' + orderpartner + ' .list ul li').each(function () {
                            var liid = $(this).attr("id");

                            if ($('#page_partnerorder_' + orderpartner + ' .list ul li .display').val().length > 0 && $("#hdnneworderfirst").val() != liid.replace("li", "")) {
                                var orderqtd = $('#page_partnerorder_' + orderpartner + ' .list ul #' + liid + ' .display').val()
                                var orderprod = $('#page_partnerorder_' + orderpartner + ' .list ul #' + liid + ' #hdn_product_id').val()

                                var l_endpoint = 'http://haylie.bosswebapps.net/api/order/save?'
                                var l_params = 'p_order=' + $("#hdnneworder").val();
                                l_params += '&p_partner=' + orderpartner;
                                l_params += '&p_product=' + orderprod;
                                l_params += '&p_qtd=' + orderqtd;

                                morgana_talkToTheBeyond("order_register", l_endpoint + l_params, $("#hdnplayer").val());
                                count_integration++;

                                if (count_integration == parseInt($("#hdnneworderlength").val()) - 1) {
                                    
                                    var sentTo = "Pedido enviado para " + partner_name;
                                    courier_sendorder_desktop(sentTo);
                                    /*
                                    $('#page_order_init .step2 .search select option[value="' + orderpartner + '"]').remove();
                                    if ($('#page_order_init .step2 .search select option').length <= 0)
                                        blacksmith_page('orders');
                                    else
                                        $('#page_order_init .step2 .search select').change();*/

                                }
                            }

                        });
                    }
                    else {
                        var sentTo = "Pedido enviado para " + partner_name;
                        courier_sendorder_desktop(sentTo);
                        /*
                        $('#page_order_init .step2 .search select option[value="' + orderpartner + '"]').remove();
                        if ($('#page_order_init .step2 .search select option').length <= 0)
                            blacksmith_page('orders');
                        else
                            $('#page_order_init .step2 .search select').change();*/

                    }

                    break;

                case "order_create":
                    //response = JSON.parse(data.Value);
                    console.log('order created> ' + data.Value);
                    $("#hdnneworder").val(data.Value);

                    var orderpartner = $('#page_order_init .step2 .search select').val();
                    var count_integration = 0;

                    if (parseInt($("#hdnneworderlength").val()) > 1) {
                        $('#page_order_init .step2 .list ul li[data-filter="search_' + orderpartner + '"]').each(function () {
                            var liid = $(this).attr("id");

                            if ($('#page_order_init .step2 .list ul #' + liid + ' .display').val().length > 0 && $("#hdnneworderfirst").val() != liid.replace("li", "")) {
                                var orderqtd = $('#page_order_init .step2 .list ul #' + liid + ' .display').val()
                                var orderprod = $('#page_order_init .step2 .list ul #' + liid + ' #hdn_product_id').val()

                                var l_endpoint = 'http://haylie.bosswebapps.net/api/order/save?'
                                var l_params = 'p_order=' + $("#hdnneworder").val();
                                l_params += '&p_partner=' + orderpartner;
                                l_params += '&p_product=' + orderprod;
                                l_params += '&p_qtd=' + orderqtd;

                                morgana_talkToTheBeyond("order_register", l_endpoint + l_params, $("#hdnplayer").val());
                                count_integration++;

                                if (count_integration == parseInt($("#hdnneworderlength").val()) - 1) {
                                    var sentTo = "Pedido enviado para <br/><br/>" + $('#page_order_init .step2 .search select option[value="' + orderpartner + '"]').html();
                                    courier_sendorder(sentTo);

                                    $('#page_order_init .step2 .search select option[value="' + orderpartner + '"]').remove();
                                    if ($('#page_order_init .step2 .search select option').length <= 0)
                                        blacksmith_page('orders');
                                    else 
                                        $('#page_order_init .step2 .search select').change();

                                }

                            }

                        });
                    }
                    else {
                        var sentTo = "Pedido enviado para <br/><br/>" + $('#page_order_init .step2 .search select option[value="' + orderpartner + '"]').html();
                        courier_sendorder(sentTo);

                        $('#page_order_init .step2 .search select option[value="' + orderpartner + '"]').remove();
                        if ($('#page_order_init .step2 .search select option').length <= 0)
                            blacksmith_page('orders');
                        else
                            $('#page_order_init .step2 .search select').change();

                    }
                    break;

                case "spirit":
                    var spiritmsg = "Reset mail sent to <br/><b style='letter-spacing: 2.7px'>" + $("#box_login #mail").val() + "</b>";
                    courier_alert('', '.trinity > Hey u ;]', spiritmsg, true);
                    setTimeout(function () { $(".courier_alert").fadeOut(); }, 4300);
                    cronos_loading(0);
                    break;
                case "ressurect":
                    //alert("Password changed");
                    var ressurectmsg = "Ok, boss. Now let's try again";
                    courier_alert('', '.trinity > Done', ressurectmsg, true);
                    setTimeout(function () { $(".courier_alert").fadeOut(); window.location = "http://marys.bosswebapps.com/"; }, 3400);
                    break;
            }
        }
    });

}

function morgana_bringMeToLife(p_what) {
    switch (p_what) {
        case "categ": categs_wakeup(); break;
        case "product": products_wakeup(); break;
        case "partner": partners_wakeup(); break;
        case "order": orders_wakeup(false); break;
        case "order_init": orders_wakeup(true); break;
        case "settings": settings_wakeup(); break;
        case "config":
            $("#page_config #btn_go").unbind();
            $("#page_config #btn_go").bind("click", function () { setConfig(); });
            break;

        case "spirit":
            if ($("#box_reanimation #key").val().length > 0)
                morgana_talkToTheBeyond("ressurect", "/User/Reanimation", $("#box_reanimation #key").val());

            break;

        case "alerts":
            $(".header .alerts ul li .actions .checkw").each(function () {
                $(this).unbind();
                $(this).bind("click", function () { morgana_kill("alert", $(this).attr("rel")); });
            });
            $(".header .alerts ul li .trg_detail").each(function () {
                $(this).unbind();
                $(this).bind("click", function () {
                    var new_state = parseInt($("#reg_alerts_" + $(this).attr("rel")).css("height")) < 40 ? "160px" : "34px";

                    eAlert = $("#reg_alerts_" + $(this).attr("rel"));
                    var tlInit = new TimelineMax({ repeat: 0, repeatDelay: 0 });
                    tlInit.add(TweenLite.to(eAlert, .3, { height: new_state, ease: Linear.easeOut }), 0);

                });
            });
            $(".header .alerts #trg_everything").unbind();
            $(".header .alerts #trg_everything").bind("click", function () {
                if ($(this).hasClass("on")) {
                    $(".header .alerts ul li.old").fadeOut();
                    $(this).removeClass("on");
                }
                else {
                    $(".header .alerts ul li.old").fadeIn();
                    $(this).addClass("on");
                }
            });
            break;

        case "sidebar":
            $(".sidebar ul li a").each(function () {
                $(this).unbind();
                $(this).bind("click", function () {
                    //cronos_loading(1);
                    
                    //refreshLine(true);
                    $("#header ul.right a").css("display", "none");
                    var show_trg = $(this).attr("rel") != "corona" ? ($(this).attr("rel") != "config" ? "upload" : "lock") : "play";
                    var show_trg = $(this).attr("rel") != "orders" ? show_trg : "orders";
                    var show_trg = $(this).attr("rel") != "ranking" ? show_trg : "ranking";
                    var show_trg = $(this).attr("rel") != "products" ? show_trg : "products";
                    var show_trg = $(this).attr("rel") != "partners" ? show_trg : "partners";
                    var show_trg = $(this).attr("rel") != "staff" ? show_trg : "staff";
                    var show_trg = $(this).attr("rel") != "config" ? show_trg : "lock";
                    var show_trg = $(this).attr("rel") != "settings" ? show_trg : "settings";
                    var show_trg = $(this).attr("rel") != "categs" ? show_trg : "categs";
                    var show_trg = $(this).attr("rel") != "order_init" ? show_trg : "order_init";
                    var show_trg = $(this).attr("rel") != "home" ? show_trg : "home";
                    $("#header #trg_" + show_trg).fadeIn();
                    
                    blacksmith_page($(this).attr("rel"));

                    $("#sidebar ul li a").removeClass("on");
                    $('#sidebar ul li a[rel="' + $(this).attr("rel") + '"]').addClass("on");
                    //blacksmith_arena("#box_main", "User", $(this).attr("rel"), "");
                    morgana_magic("sidebar");
                });
            });

            //$(".sidebar #box_player #btn_profile").unbind();
            //$(".sidebar #box_player #btn_profile").bind("click", function () { cronos_loading(1); blacksmith_arena("#box_main", "User", "users", $("#hdn_player").val()); morgana_magic("sidebar"); });

            //$(".sidebar #box_player .logout").unbind();
            //$(".sidebar #box_player .logout").bind("click", function () { getMeOut(); });
            break;

        case "shortcuts":
            $('#shortcuts a[rel="settings"]').unbind();
            $('#shortcuts a[rel="settings"]').bind("click", function () {
                $(".page_wrapper").css("display", "none");
                $("#page_home").css("display", "none");
                $("#header #trg_process").fadeOut();
                $("#page_config, #page_settings, #page_categs").fadeIn();

                $('#shortcuts a').removeClass('on');
                $(this).addClass('on');
            });
            $('#shortcuts a[rel="home"]').unbind();
            $('#shortcuts a[rel="home"]').bind("click", function () {
                $(".page_wrapper").css("display", "none");
                $("#page_home").css("display", "none");
                $("#header #trg_process").fadeOut();
                $("#page_products, #page_partners, #page_orders").fadeIn();

                $('#shortcuts a').removeClass('on');
                $(this).addClass('on');
            });
            break;

            case "login":
                $("#box_login #key, #box_login #mail").each(function () { $(this).on('keypress', function (e) { if (e.which == 13) { verifyMe(); } }); });
                $("#box_login #ghost").each(function () { $(this).on('keypress', function (e) { if (e.which == 13) { bringMeBack(); } }); });
                $("#box_login #regname, #box_login #regmail, #box_login #regkey").each(function () { $(this).on('keypress', function (e) { if (e.which == 13) { signMeUp(); } }); });
    
                $("#box_login #btn_gorecover, #box_login #btn_goregister, #box_login #btn_gologin, #box_login #btn_gologin2").each(function () {
                    $(this).unbind();
                    $(this).bind("click", function () {
                        build_screen($(this).attr("rel"));
                    });
                    $("#hdnorigin").val("");
                });
    
                $("#box_login #btn_go").unbind();
                $("#box_login #btn_go").bind("click", function () { verifyMe(); });
    
                $("#box_login #btn_sendrecover").unbind();
                $("#box_login #btn_sendrecover").bind("click", function () { bringMeBack(); });
    
                $("#box_login #btn_sendreanimation").unbind();
                $("#box_login #btn_sendreanimation").bind("click", function () { supercall(); });
    
                $("#box_login #btn_sendregister").unbind();
                $("#box_login #btn_sendregister").bind("click", function () { signMeUp(); });

            break;
    }

}

function morgana_kill(p_what, p_who) {
    var l_endpoint = 'http://haylie.bosswebapps.net/api/content/kill?p_what=' + p_what + '&p_id=' + p_who;
    morgana_talkToTheBeyond(p_what, l_endpoint, p_who);
    return false;
}

//=========================================================================================[ magics ]===
function morgana_magic(p_spell) {
    var tlInit = new TimelineMax({ repeat: 0, repeatDelay: 0 });
    var eSidebar = $(".sidebar");
    
    switch (p_spell) {
        //case "expand":
        //    $('#modal_expand #expand_obj').attr("data", $("#box_" + $("#modal_expand #hdn_base").val() + " #box_detail #trg_expand").attr("href").substr(1));
        //    $('#modal_expand #expand_iframe').attr("src", $("#box_" + $("#modal_expand #hdn_base").val() + " #box_detail #trg_expand").attr("href").substr(1));

        //    $("#modal_expand .close").unbind();
        //    $("#modal_expand .close").bind("click", function () { $(".modalbox.bg,#modal_expand").fadeOut(); });

        //    $(".modalbox.bg").fadeIn("fast");
        //    $("#modal_expand").fadeIn("slow");
        //    break;

        //case "particles":
        //    magic_particles.init();
        //    break;
        
        case "sidebar":
            if (navstate == 0) {
                tlInit.add(TweenLite.to(eSidebar, .2, { right: "0", ease: Linear.easeNone }), 0);
                navstate = 1;
                $(".bg_sidebar").fadeIn();
            } else {
                tlInit.add(TweenLite.to(eSidebar, .2, { right: "-101%", ease: Linear.easeNone }), 0);
                navstate = 0;
                $(".bg_sidebar").fadeOut();
            }
            break;

    }
}