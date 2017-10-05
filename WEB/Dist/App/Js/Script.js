(function ($) {
    window.DCG = {
        Init: function () {
            $("button[name=btnCancelar]").on("click", function () {
                var frm = $(this).parents("div[role=dialog]").eq(0).find("form")[0].reset();
                $(frm).find("input[name*='id']").val(0);
            });
            $("[numeroint]").attr("onkeypress", "return DCG.Util.numeroInt(event)");

            $(".btn-cerrar-sesion").click(function () {
      
                DCG.Util.logOut();
            });


        },
        'Login': {
            login_CTL: "",
            login_FRM: "",
            login_TOKEN:"",
            Init: function () {
                this.checkFullPageBackgroundImage();

                var _this = this;
                _this.login_CTL = $("#Login");
                _this.login_FRM = _this.login_CTL.find("form");

                _this.login_FRM.find('button[name=btnIniciarSesion]').click(function () {
        
                    _this.iniciarSesion();
                });

                _this.login_FRM.find("input[name=clave]").keyup(function (e) {
                    if (e.keyCode == 13) {
                        _this.iniciarSesion();
                    }
                });
            },
            checkFullPageBackgroundImage: function () {
                $page = $('.full-page');
                image_src = $page.data('image');

                if (image_src !== undefined) {
                    image_container = '<div class="full-page-background" style="background-image: url(' + image_src + ') "/>'
                    $page.append(image_container);
                }
            },
            iniciarSesion: function () {
                var _this = this;
                _this.login_TOKEN = _this.login_FRM.find('[name=__RequestVerificationToken]').val();
                var UserName = _this.login_FRM.find('input[name=usurio]').val();
                var Password = _this.login_FRM.find('input[name=clave]').val();
                if (UserName != '' && Password != '') {
                    DCG.preload.start();
                    var url = _this.login_CTL.attr('data-url-login');
                    $.post(url, { 'UserName': UserName, 'Password': Password, __RequestVerificationToken: _this.login_TOKEN }, function (response) {
                        if (response.url != "" && response.isValid == 1) {
                            location.href = response.url;
                        } else {
                            DCG.preload.end();
                            swal('ERROR!', response.message, 'error')
                        }
                    })
                }
            }
        },
        'Util': {
            getHtml: function (url) {
                DCG.preload.start();
                var _html = "";
                getData({ url: url, data: {}, dataType: 'html', asyn: true }, function (response) {
                    _html = response;
                    DCG.preload.end();
                });
                return _html;
            },
            sendDataForm: function (url, form, table, reterFrm) {
      
                if (reterFrm == undefined) { undefined = true; };
                var _this = this;
                if (_this.validateForm(form)) {
                    DCG.preload.start();
                    var query = $(form).serialize();
                 
                    getData({ url: url, data: query },
        
                     function (response) {
                        DCG.preload.end();
                        if (response.status == "SUCCESS") {
                            if (reterFrm) {
                                _this.resetForm(form);
                            }                            
                            $(table).dataTable();
                            swal('ÉXITO', response.message, 'success')
                            setTimeout(function () {
                                if (response.op == "U") {
                                    $(form).parents("div.modal").modal("hide");
                                }
                            },1000);
                        } else {
                            swal('ERROR!', response.message, 'error')
                        }

                        $(form).trigger('finished', response);
                    });
                }
            },
            resetForm: function (form) {
                $(form)[0].reset();
                $("input[name=id]").val(0);
                $.each(form.find("textarea"), function (i, e) {
                    var sm = $(e).attr("data-sn")
                    if (sm == "yes") {
                        $(e).summernote('code', '');
                    }
                });
                $("div.has-error").removeClass("has-error");
            },
            validateForm: function (form) {
                var error = 0, r = true;                
                $(form).find('[required]').each(function (i, elem) {                    
                    if ($(elem).val() == '') {
                        $(this).parents("div").eq(0).addClass("has-error");
                        error++;
                    }
                });
                if (error > 0) {
                    r = false;
                }
                return r;
            },
            deleteData: function (url,id,nombre,tb,callbak) {
                swal({
                    title: 'Estas seguro?',
                    text: "vas a eliminar " + nombre,
                    type: 'question',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Sí, eliminar ahora!'
                }).then(function () {
                    getData({ url: url, data: { id: id } }, function (response) {
                        if (response.status == "SUCCESS") {
                            swal('Eliminado!', response.message, 'success');
                            $(tb).dataTable();
                            if (callbak != undefined) {
                                return callbak(response);
                            }                            
                        }
                    });
                });
            },
            numeroInt: function (evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false; 
            },
            logOut: function () {
                var form = $("#logoutForm");
                form.submit();
            },
        },
        'preload': {
            start: function () {
                $("div.loader").css("display", "");
            },
            end: function () {
                $("div.loader").css("display", "none");
            }
        },
        'Ticket': {
            ticket_CTL: "",
            ticket_FRM: "",
            ticket_TB:"",
            Init: function () {
                var _this = this;
                _this.ticket_CTL = $("#Ticket");
                _this.ticket_FRM = _this.ticket_CTL.find("form");
                _this.ticket_TB = _this.ticket_CTL.find("#tbTicket");
                _this.ticket_TB.dataTable();

                _this.ticket_CTL.find("button[name=btnNuevo]").click(function () {
                    
                    $("#dlgticket").modal("show");
                });
               
                _this.ticket_CTL.find("button[name=btnRegistrar]").click(function () {
            
                    var url = _this.ticket_CTL.attr("data-url-register");
                    DCG.Util.sendDataForm(url, _this.ticket_FRM, _this.ticket_TB);
   
                });

                _this.ticket_TB.on("click", "a.select", function () {
                    var obj = $(this).parents("tr").eq(0).data("data");
                    _this.ticket_FRM.find("input[name=id]").val(obj.id);
                    _this.ticket_FRM.find("input[name=ticket]").val(obj.ticket);
                    $("#dlgticket").modal("show");
                });
                
                _this.ticket_TB.on("click", "a.delete", function () {
                    var obj = $(this).parents("tr").eq(0).data('data');
                    var url = _this.ticket_CTL.attr("data-url-delete");
                    DCG.Util.deleteData(url, obj.id, obj.ticket, _this.ticket_TB);
                });


            }
        },
        'Mensajes': {
            ticket_CTL: "",
            ticket_FRM: "",
            ticket_TB: "",
            Init: function () {
                $("#panel_mensajes").scrollTop($("#contenido_mensajes").height());
                $("#filter").keyup(function () {
                    var filter = $(this).val(), count = 0;
                    $("#conten ul li").each(function () {
                        if ($(this).text().search(new RegExp(filter, "i")) < 0) {
                            $(this).fadeOut();
                        } else {
                            $(this).show();
                            count++;
                        }
                    });
                    var numberItems = count;
                   
                    $("#filter-count").text("USUARIOS ENCONTRADOS  " + count);
                   

                });
                var _this = this;
                _this.ticket_CTL = $("#Ticket");
                _this.ticket_FRM = _this.ticket_CTL.find("form");
                _this.ticket_TB = _this.ticket_CTL.find("#tbTicket");
                _this.ticket_TB.dataTable();

                _this.ticket_CTL.find("button[name=btnNuevo]").click(function () {

                    $("#dlgticket").modal("show");
                });

                _this.ticket_CTL.find("button[name=btnRegistrar]").click(function () {
                    debugger;
                    alert("si");
                    var url = _this.ticket_CTL.attr("data-url-register");
                    DCG.Util.sendDataForm(url, _this.ticket_FRM, _this.ticket_TB);
                });

                _this.ticket_TB.on("click", "a.select", function () {
                    var obj = $(this).parents("tr").eq(0).data("data");
                    _this.ticket_FRM.find("input[name=id]").val(obj.id);
                    _this.ticket_FRM.find("input[name=ticket]").val(obj.ticket);
                    $("#dlgticket").modal("show");
                });

                _this.ticket_TB.on("click", "a.delete", function () {
                    var obj = $(this).parents("tr").eq(0).data('data');
                    var url = _this.ticket_CTL.attr("data-url-delete");
                    DCG.Util.deleteData(url, obj.id, obj.ticket, _this.ticket_TB);
                });


            }
        },
        'Persona': {
            persona_CTL:"",
            persona_FRM: "",
            persona_TB:"",
            Init: function () {
                var _this = this;
                _this.persona_CTL = $("#Persona");
                _this.persona_FRM = _this.persona_CTL.find("#frmPersonaRegistrar");
                _this.persona_TB = _this.persona_CTL.find("#tbPersona");
                //_this.persona_TB.dataTable();

                _this.persona_CTL.find("button[name=btnNuevaPersona]").click(function () {
                    $("#dlgPersona").modal("show");
                });

                _this.persona_CTL.find("button[name=btnRegistrarCliente]").click(function () {
                   

                    var url = _this.persona_CTL.attr("data-url-register");
        
                    DCG.Util.sendDataForm(url,_this.persona_FRM,_this.persona_TB);
                });

                _this.persona_TB.on("click", "a.select", function () {
                    var obj = $(this).parents("tr").eq(0).data("data");
                    _this.persona_FRM.find("input[name=email]").val(obj.id);
                    _this.persona_FRM.find("input[name=nombre]").val(obj.nombre);

                    if (obj.rol === "Proveedor" || obj.rol === "Cliente") {
                        _this.persona_FRM.find(".detallePersona").css("display", "");
                    } else {
                        _this.persona_FRM.find(".detallePersona").css("display", "none");
                    }
                    $("#dlgPersona").modal("show");
                });

                _this.persona_TB.on("click", "a.delete", function () {

                    var obj = $(this).parents("tr").eq(0).data('data');
                    var url = _this.persona_CTL.attr("data-url-delete");

                    DCG.Util.deleteData(url, obj.id, obj.persona, _this.persona_TB);
                });
                _this.persona_FRM.find("input[name=img_tmp]").on('change', function () {
                    if (this.files && this.files[0]) {
                        // 500 kb
                        if ((this.files[0].size / 1024).toFixed(2) > 500) {
                            swal({
                                title: 'Importante',
                                text: "Tamaño máximo del logo debe ser 10kb",
                                type: 'warning',
                                showCancelButton: false,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'ok'
                            });
                            $(this).val('');
                        } else {
                            var FR = new FileReader();
                            FR.onload = function (e) {
                                $("input[name=img]").val(e.target.result);
                            };
                            FR.readAsDataURL(this.files[0]);
                        }
                    }
                });

                $(_this.persona_TB).bind('rowloaded', function (e, p) {
                    var tr = p.tr;
                    var obj = p.obj;
                    if (obj.img.trim() != "" && obj.img.trim() != "-") {
                        $(tr).find('td.img').html('<img style="    width: 25px;" src="' + p.obj.img + '" />');
                    } else {
                        $(tr).find('td.img').html("-");
                    }
                });

                _this.persona_FRM.find("select[name=rol]").change(function () {
                    if ($(this).val() === "Proveedor" || $(this).val() === "Cliente") {
                        _this.persona_FRM.find(".detallePersona").css("display", "");
                        _this.persona_FRM.find(".detallePersona input[type=text]").attr("required",true);
                        _this.persona_FRM.find("input[name=usuario]").removeAttr("required");
                        _this.persona_FRM.find("input[name=usuario]").attr("readonly",true).val('');
                    } else {
                        _this.persona_FRM.find(".detallePersona").css("display", "none");
                        _this.persona_FRM.find(".detallePersona input[type=text]").removeAttr("required");
                        _this.persona_FRM.find("input[name=usuario]").attr("required", true);
                        _this.persona_FRM.find("input[name=usuario]").removeAttr("readonly");
                    }
                });
            }
        }




    }
})(jQuery);

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};
$(function () {
    DCG.Init();
});

(function () {
    $('.hider').click(function () {
        return $(this).parent('.message').removeClass('blur');
    });

}).call(this);
 