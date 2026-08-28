function blacksmith_build(box, target, what, output) {

    switch (what) {
        case "image_single":
            $("#" + target).change(function () {
                var formData = new FormData();
                var file = document.getElementById(target).files[0];
                formData.append(target, file);
                //var totalFiles = document.getElementById("imageUploadForm").files.length;
                //for (var i = 0; i < totalFiles; i++) {
                //    var file = document.getElementById("imageUploadForm").files[i];
                //    formData.append("imageUploadForm", file);
                //}
                $.ajax({
                    type: "POST",
                    url: '/Content/Upload',
                    data: formData,
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        if (response.upload_ext.indexOf("jpg") >= 0 ||
                            response.upload_ext.indexOf("jpeg") >= 0 ||
                            response.upload_ext.indexOf("gif") >= 0 ||
                            response.upload_ext.indexOf("png") >= 0 ||
                            response.upload_ext.indexOf("bitmap") >= 0)
                        {
                            var resultFile = '<li><a id="trg_kill' + response.upload_id + '" href="#" class="off" rel="' + response.upload_id + '">';
                            resultFile += '<img src="/' + response.upload_content + '" /><div><img src="content/img/trash.png" /></div></a></li>';

                            $("#page_gallery .listbox ul").html(resultFile + $("#page_gallery .listbox ul").html());
                            setTimeout(function () {
                                $("#page_gallery .listbox ul li a").each(function () {
                                    $(this).unbind();
                                    $(this).bind("click", function () {
                                        if ($(this).hasClass("on")) {
                                            $.post("/Content/Kill", { p_param: $(this).attr("rel") }, function (data) { });
                                            $('#page_gallery #trg_kill' + $(this).attr("rel")).fadeOut();
                                        }
                                        if ($('#page_gallery #trg_kill' + $(this).attr("rel")).hasClass("off") == true) {
                                            $('#page_gallery #trg_kill' + $(this).attr("rel")).addClass("on");
                                        }
                                        resetGallery();

                                        return false;
                                    });
                                });
                            }, 360);
                            //$(output).attr("src", response.upload_content);
                        }
                        else
                            $(output).attr("src", "content/img/file.png");

                        formData = null;
                    },
                    error: function (error) {
                        alert(error.msg);
                    }
                });
            });
            $("#" + box + " #trg_upload").unbind();
            $("#" + box + " #trg_upload").bind("click", function () { $("#" + target).click(); });

            break;
    }
}
function blacksmith_list(p_target, p_build_page) {
    var l_route = "/" + (p_target == "order" ? "order" : (p_target != "partner" ? (p_target != "categ" ? "product" : p_target) : "partner")) + "/getall";
    var l_request = "http://haylie.bosswebapps.net/api" + l_route;
    var p_targetbox = '#page_' + (p_target == 'order_init' ? p_target : p_target + "s");

    $.get(l_request, { p_param: $("#hdnplayer").val() }, function (data) {
        response = JSON.parse(data.Value);

        if (p_build_page == false) {
            if (p_target == "categ") {
                $(".page_wrapper .frm #product_type").html('<option value=""></option>');
                $("#page_order_init .step1 .search select").html('');
                $("#page_products .step3 #search_categs select").html('');
                //$(".page_categprods").remove();
            }
            else {
                if (p_target == "product") $(".page_wrapper .step3 #search_products select").html('');

                $(".page_wrapper .frm #product_partner").html('<option value=""></option> ');
                $("#page_order_init .step2 .search select").html('');
                $('#page_order_init .step2 .ghostbox').html('');
                $('#page_order_init .step2 .ghostbox').html('');
                //$('#page_productscateg_' + response[regcount].id_type + ' .list ul') //duhflag
                $(".page_categprods:not(#page_productscateg_id) .list ul li").remove();
            }
            
        }
        else
            $(p_targetbox + ".page_wrapper .list ul").html(''); 

        console.log('running> ' + p_targetbox + ' > ' + l_route + ' > ' + p_build_page + ' > ' + response.length);

        if (response == null && p_build_page) { blacksmith_list(p_target, true); return false; }

        if (response.length > 0) {
            for (regcount = 0; regcount < response.length; regcount++) {
                var currentzip = p_target != "categ" ? (p_target == "order_init" ? response[regcount].id_type : response[regcount].name + '_' + response[regcount].partner) : response[regcount].name;

                var linehead_default = '';
                if (p_target == "order")
                    linehead_default = '<li id="li' + response[regcount].id + '">';
                else {
                    if (p_target == "categ")
                        linehead_default = '<li id="li' + response[regcount].id + '" name="search_' + currentzip + '" data-filter="search_' + response[regcount].id + '">';
                    else 
                        linehead_default = '<li id="li' + response[regcount].id + '" name="search_' + currentzip + '" data-filter="search_' + response[regcount].id_tag + '" data-categ="' + response[regcount].id_type + '">';

                }

                //var linehead_searchpartner = '<li id="li' + response[regcount].id + '" name="search_' + response[regcount].id_tag + '">';
                var currentline = linehead_default;

                if (p_target == "order_init") {
                    currentdevice = $("#hdndesktop").val() == "1" ? "text" : "number";
                    currentline += '<a href="#" class="trg_up" rel="' + response[regcount].id + '">+</a><input class="display" type="' + currentdevice + '" placeholder="0" maxlength="6" value="' + response[regcount].stock + '" /><a href="#" class="trg_down" rel="' + response[regcount].id + '">-</a><input type="hidden"value="' + response[regcount].id + '" /><p>';
                    currentline += '';
                }
                else
                    currentline += '<a href="#" class="trg_row" rel="' + response[regcount].id + '"><p>';


                if (p_target == "partner") {
                    if (p_build_page == false) {
                        //$("#page_partners .frm #product_partner")
                        var currentline = '<option value="' + response[regcount].id + '">' + response[regcount].partner + '</option> ';
                    }
                    else {
                        currentline += response[regcount].partner + '<span>' + response[regcount].name + '</span></p></a><a class="trg_delete" href="#" rel="' + response[regcount].id + '"><img src="content/img/icons/trash.png" /></a>';
                        currentline += '<input type="hidden" id="hdn_partner_id" value="' + response[regcount].id + '" />';
                        currentline += '<input type="hidden" id="hdn_partner_partner" value="' + response[regcount].partner + '" />';
                        currentline += '<input type="hidden" id="hdn_partner_cnpj" value="' + response[regcount].cnpj + '" />';
                        currentline += '<input type="hidden" id="hdn_partner_name" value="' + response[regcount].name + '" />';
                        currentline += '<input type="hidden" id="hdn_partner_mail" value="' + response[regcount].mail + '" />';
                        currentline += '<input type="hidden" id="hdn_partner_phone" value="' + response[regcount].phone + '" />';
                        currentline += '</li>';
                    }
                }
                else {
                    if (p_target == "order") {
                        currentline += response[regcount].dateins.substr(0, 16).replace("T", " ") + '<span>' + response[regcount]._partner.name + ' </span></p></a><a class="trg_delete" href="#" rel="' + response[regcount].id + '"><img src="content/img/icons/trash.png" /></a>';
                        currentline += '<ul>';
                        currentline += '<li style="text-align: center;font-family: monospace;margin: 6px 0;">PRODUTO QTND</li>';
                        for (p = 0; p < response[regcount].list_products.length; p++) {
                            var oi_name = '<span class="name">' + response[regcount].list_products[p].name + '</span>';
                            var oi_qtd = '<span class="qtd">' + response[regcount].list_products[p].unitqtd + '</span>';
                            currentline += '<li id="lioi' + response[regcount].list_products[p].id + '" class="orderitem">' + oi_name + oi_qtd + '</li>';
                        }
                        currentline += '</ul>';
                        if (response[regcount]._partner.mail.length > 0) {
                            currentline += '<input type="hidden" id="hdnmail" value="' + response[regcount]._partner.mail + '" />';
                            currentline += '<a href="#" class="btn_fixed_single" style="left: 70%;" id="trg_send_mail" rel="' + response[regcount].id + '"><img class="btn_icon" src="content/img/icons/mail.png"></a>';
                        }
                        if (response[regcount]._partner.phone.length > 0) {
                            currentline += '<input type="hidden" id="hdnwpp" value="' + response[regcount]._partner.phone + '" />';
                            currentline += '<a href="#" class="btn_fixed_single" style="left: 75%;" id="trg_send_wpp" rel="' + response[regcount].id + '"><img class="btn_icon" src="content/img/icons/whats.png"></a>';                        
                        }

                    }
                    else {
                        if (p_target == "categ") {
                            if (p_build_page == false) {
                                var currentline = '<option value="' + response[regcount].id + '">' + response[regcount].name + '</option> ';
                            }
                            else {
                                currentline += response[regcount].name + '</p></a>';
                                currentline += '<input type="hidden" id="hdn_categ_id" value="' + response[regcount].id + '" />';
                                currentline += '<input type="hidden" id="hdn_categ_name" value="' + response[regcount].name + '" />';
                            }
                        }
                        else {
                            if (p_target == "product") {
                                currentline += response[regcount].name + '<span>' + response[regcount].unitqtd + ' ' + response[regcount].unittype + ' </span></p></a><a class="trg_delete" href="#" rel="' + response[regcount].id + '"><img src="content/img/icons/trash.png" /></a><a class="trg_expand" href="#" rel="' + response[regcount].id + '"><i class="fas fa-check-square"></i></a>';

                                var currentline_opt = '<option class="opt' + response[regcount].id + '" value="' + response[regcount].id + '">' + response[regcount].name + '</option>';

                                $(".page_wrapper .step3 #search_products select").append(currentline_opt);
                            } else
                                currentline += response[regcount].name + '<span>' + response[regcount].unitqtd + ' ' + response[regcount].unittype + ' </span></p></a>';

                            currentline += '<input type="hidden" id="hdn_product_id" value="' + response[regcount].id + '" />';
                            currentline += '<input type="hidden" id="hdn_product_name" value="' + response[regcount].name + '" />';
                            currentline += '<input type="hidden" id="hdn_product_unittype" value="' + response[regcount].unittype + '" />';
                            currentline += '<input type="hidden" id="hdn_product_unitqtd" value="' + response[regcount].unitqtd + '" />';
                            currentline += '<input type="hidden" id="hdn_product_stock" value="' + response[regcount].stock + '" />';
                            currentline += '<input type="hidden" id="hdn_product_stockmin" value="' + response[regcount].stockmin + '" />';
                            currentline += '<input type="hidden" id="hdn_product_pricein" value="' + response[regcount].pricein + '" />';
                            currentline += '<input type="hidden" id="hdn_product_partner" value="' + response[regcount].id_tag + '" />';
                            currentline += '<input type="hidden" id="hdn_product_categ" value="' + response[regcount].id_type + '" />';
                        }
                    }
                    currentline += '</li>';

                    if (p_target == "order_init" && p_build_page == true)
                        $('#page_productscateg_' + response[regcount].id_type + ' .list ul').append(currentline);

                }

                if (p_build_page == false) {
                    if (p_target == "partner") {
                        $(".page_wrapper .frm #product_partner").append(currentline);
                        $("#page_order_init .step2 select").append(currentline);

                        var orderhelper = '<input type="hidden" id="hdnorderpartner' + response[regcount].id + '_mail" value="' + response[regcount].mail + '" />';
                        orderhelper += '<input type="hidden" id="hdnorderpartner' + response[regcount].id + '_phone" value="' + response[regcount].phone + '" />';
                        $('#page_order_init .step2 .ghostbox').append(orderhelper);
                    }
                    else {
                        $(".page_wrapper .frm #product_type").append(currentline);
                        $("#page_order_init .step1 select").append(currentline);
                        $("#page_products .step3 #search_categs select").append(currentline);
                    }
                }
                else $(p_targetbox + ".page_wrapper .list ul").append(currentline); 
                
                

                if (regcount == response.length - 1) {
                    if (p_target == "order") blacksmith_clear();

                    morgana_bringMeToLife(p_target); setTimeout(blacksmith_paint(p_targetbox), 741);

                    //if (p_target == "products")
                    //    setTimeout($("#page_" + p_target + " #search_categs select").change(), 741);

                }
            }
            morgana_bringMeToLife(p_target);
        }
        else {
            var msglabel = p_target == "order" ? "pedido" : (p_target == "partner" ? "fornecedor" : "produto");
            var noresultsbox = '<li><div><p class="notfound">Nenhum ' + msglabel + ' encontrado</p></div></li>';

            if (p_build_page == true) $(p_targetbox + ".page_wrapper .list ul").html(noresultsbox);
        }
    });
}
function blacksmith_home() {
    var l_route = "/content/getall";
    var l_request = "http://haylie.bosswebapps.net/api" + l_route;

    if ($("#hdndesktop").val() == "1") {
        $(".page_wrapper").css("display", "none");
        $("#page_home").css("display", "none");

        blacksmith_page('categs');
        blacksmith_page('partners');
        blacksmith_page('products');
        blacksmith_page('orders');

        $("#header #trg_run").unbind();
        $("#header #trg_run").bind("click", function () {
            $('#shortcuts a').removeClass('on');
            $('.page_partnerbox .list ul').html('');
            blacksmith_page('order_init');

            $(".page_wrapper").css("display", "none");
            $("#page_home").css("display", "none");
            $(".page_categprods").fadeIn();
            if ($("#hdnplayert").val() == "2") $("#header #trg_process").fadeIn();
        });
        $("#header #trg_process").unbind();
        $("#header #trg_process").bind("click", function () {
            var itemT = 0;
            var totallines = $('.page_categprods .list ul li input[type="text"]').length;
            $('.page_categprods .list ul li input[type="text"]').each(function () {
                var itemV = $(this).val().length > 0 ? parseFloat($(this).val()) : 0;
                if (itemV > 0) itemT += itemV;

                totallines -= 1;

                if (totallines == 0) {
                    if (itemT > 0) {
                        $('.page_partnerbox #btn_desksend_mail').each(function () {
                            $(this).unbind();
                            $(this).bind("click", function () { orders_desksend(false, '0', $(this).attr("rel")); });
                        });
                        $('.page_partnerbox #btn_desksend_whats').each(function () {
                            $(this).unbind();
                            $(this).bind("click", function () { orders_desksend(true, '0', $(this).attr("rel")); });
                        });

                        $(".page_wrapper").css("display", "none");
                        $("#page_home").css("display", "none");
                        //$(".page_partnerbox").fadeIn();
                        $(".page_partnerbox").each(function () {
                            var pboxid = $(this).attr("id");
                            if ($('#' + pboxid + ' ul li').length <= 0) $('#' + pboxid).fadeOut();
                            else $('#' + pboxid).fadeIn();
                        });
                        $("#header #trg_process").fadeOut();
                    }
                    else $.notify("Pedido Vazio / Contagem Inválida", { position: "top left" }, "info");

                }
            });

        });

        setTimeout(function () { orders_partnerboxes(); }, 2500);

        setTimeout(function () {
            orders_categboxes();

            if ($("#hdnplayert").val() == "1") {
                $("#header #trg_run").click();
            }
            else $("#page_products, #page_partners, #page_orders").fadeIn();


        }, 3000);

        
    }
    else {
        for (ba = 0; ba < 10; ba++) {
            var bgc = "#";
            for (bgcc = 1; bgcc <= 3; bgcc++) bgc += (9 - ba).toString();

            if (9 - ba > 0) {

            $("#page_products .step3 .bottle").append('<a href="#" rel="' + (9 - ba) + '" style="top: ' + ((ba * 21) + 90) + 'px"></a>');
            $("#page_products .step3 .bottle").append('<div class="strip" id="s' + (9 - ba) + '" style="top: ' + ((ba * 21) + 90) + 'px; background: ' + bgc +';display: none;"></div>');
            }
        }

        if ($("#hdnplayert").val() == "2") {
            $.get(l_request, { p_param: $("#hdnplayer").val() }, function (data) {
                response = JSON.parse(data.Value);

                $('#page_home a[rel="corona"] span').html('0');
                $('#page_home a[rel="staff"] span').html('0');
                $('#page_home a[rel="products"] span').html(response.count_products);
                $('#page_home a[rel="partners"] span').html(response.count_partners);
                $('#page_home a[rel="orders"] span').html(response.count_orders);

                $('#page_home #instockbox .amount').html(parseFloat(response.currentamount));

                $("#page_home ul li a").each(function () {
                    $(this).unbind();
                    $(this).bind("click", function () {
                        //cronos_loading(1);

                        //refreshLine(true);
                        $("#header ul.right a").css("display", "none");
                        var show_trg = $(this).attr("rel") != "corona" ? ($(this).attr("rel") != "config" ? "upload" : "lock") : "play";
                        var show_trg = $(this).attr("rel") != "orders" ? show_trg : "orders";
                        var show_trg = $(this).attr("rel") != "products" ? show_trg : "products";
                        var show_trg = $(this).attr("rel") != "partners" ? show_trg : "partners";
                        var show_trg = $(this).attr("rel") != "staff" ? show_trg : "staff";
                        var show_trg = $(this).attr("rel") != "config" ? show_trg : "lock";
                        var show_trg = $(this).attr("rel") != "settings" ? show_trg : "settings";
                        var show_trg = $(this).attr("rel") != "categs" ? show_trg : "categs";
                        var show_trg = $(this).attr("rel") != "home" ? show_trg : "home";
                        $("#header #trg_" + show_trg).fadeIn();

                        blacksmith_page($(this).attr("rel"));

                        $("#sidebar ul li a").removeClass("on");
                        $('#sidebar ul li a[rel="' + $(this).attr("rel") + '"]').addClass("on");
                        //blacksmith_arena("#box_main", "User", $(this).attr("rel"), "");
                        //morgana_magic("sidebar");
                    });
                });
            });
        }
        else {
            $("#header ul.right a").css("display", "none");
            $("#header #trg_orders").fadeIn();
            blacksmith_page("products");

            //$("#header #trg_products").fadeIn();
            //$("#sidebar ul li a").removeClass("on");
            //$('#sidebar ul li a[rel="products"]').addClass("on");

        }

    }
}
function blacksmith_paint(p_target) {
    console.log('painting..');
    var lpaint = 1;
    $(p_target + ".page_wrapper .list > ul > li").each(function () {
        if ($(this).css("display") != "none") {
            if (lpaint == 1) {
                $(this).css("background", "#efefef");
                lpaint = 0;
            }
            else { $(this).css("background", "#fff"); lpaint = 1; }
        }
    });
}
function blacksmith_clear() {
    $("#page_orders .list ul li ul li").each(function () {
        var oline = $(this).attr("id");
        if (oline != null) {
            if (oline.indexOf("lioi") < 0) 
                if (oline.indexOf("li") >= 0) { $(this).remove(); }

        }
    });
}
function blacksmith_page(p_target) {
    if ($("#hdndesktop").val() == "0") $(".page_wrapper").css("display", "none");
    
    if (p_target == "partners" || p_target == "products" || p_target == "orders" || p_target == "categs") {
        blacksmith_list(p_target.substr(0, p_target.length - 1), true);

        if (p_target == "products" || p_target == "orders") {
            blacksmith_list("partner", false);
            blacksmith_list("categ", false);
        }

        $("#page_" + p_target + " .frm #hdn_id").val('0');
        $('#page_' + p_target + ' .frm input[type="text"]').val('');
        $('#page_' + p_target + ' .search input[type = "text"]').val('');
        $("#page_" + p_target + " .frm select").val('');
        $("#page_" + p_target + " .step2, #page_" + p_target + " .step3").css("display", "none");

        //$("#page_" + p_target + " .step" + ($("#hdnplayert").val() == "1" ? "3" : "1")).fadeIn();
        if ($("#hdnplayert").val() == "1") {
            $("#page_" + p_target + " .step1").css("display", "none");
            $("#page_" + p_target + " .trg_goback").css("display", "none");
            $("#page_" + p_target + " .step3").fadeIn();

            //$("#page_" + p_target + " #search_categs select").change();
        }
        else 
            $("#page_" + p_target + " .step1").fadeIn();


    }
    else {
        if (p_target == "order_init") {
            blacksmith_list(p_target, true); blacksmith_list("partner", false);
            //setTimeout(900, $('#page_order_init .step1 .search select').change());
        }
        else {
            if (p_target == "home") blacksmith_home();
        }
        morgana_bringMeToLife(p_target);
    }

    if ($("#hdndesktop").val() == "0") {

        if ($("#hdnplayert").val() == "1") {
            $("#page_products").fadeIn();
            
        }
        else
            $("#page_" + p_target).fadeIn();

        
    }
    else $("#page_" + p_target + " .btn_create").fadeIn();

    
}

