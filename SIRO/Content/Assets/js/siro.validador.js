(function ($) {
    $.fn.InputValidator = function (m) {
        m.dom = m.dom || "";
        m.longitud = m.longitud || 2;
        m.onlynumber = m.onlynumber || false;
        m.validate = m.validate || false;
        m.min = m.min || 0;
        m.max = m.max || 1000;

        $(m.dom).attr("maxLength", m.longitud);
        $(m.dom).unbind("paste").bind("paste", function (e) {
            var Yo = $(this);
            setTimeout(function () {
                if (m.onlynumber && isNaN(parseInt(Yo.val()))) {
                    Yo.val("");
                }
            }, 50);
            return true;
        });

        $(m.dom).unbind("drop").bind("drop", function (e) {
            return false;
        });
        $(m.dom).unbind("change").change(function () {
            var yo = $(this);
            var value = yo.val();
            if (m.onlynumber) {
                if (m.validate) {
                    var val = yo.val();
                    if (val > m.max) { val = m.max; }
                    if (val < m.min) { val = m.min; }
                    yo.val(val);
                }
            }
            setTimeout(function () {
                if (yo.val().toString().length >= m.longitud) {
                    yo.val(yo.val().substring(0, m.longitud));
                }
            }, 40);
        });
        $(m.dom).unbind("keypress").keypress(function (e) {
            var tecla = (document.all) ? e.keyCode : e.which;
            var patron = /[0-9]/;

            if (m.onlynumber && !patron.test(String.fromCharCode(tecla)) && !(tecla == 8 || tecla == 13 || tecla == 0)) {
                //if (){
                return false;
                //}
            }
            return true;
        });
    }

    $.fn.ValidarInput = function (m) {
        m.html = m.html || "";
        m.isSelect = m.isSelect || false;
        m.titulo = m.titulo || "Notificación de Validación";
        m.tipo = m.tipo || "advertencia";
        m.posicion = m.posicion || "B";

        var name_com = $('label[for="' + m.html.replace('#', '').trim() + '"]').text();
        if (m.html == "") {
            $.fn.MensajeProcesos({
                posicion: 'B',
                mensaje: 'No se cargo el control para la validación. Por favor reporte del problema a las áreas respectiva',
                titulo: 'Error de Validación',
                tipo_mensaje: 'error'
            });
            return false;
        } else {
            if (m.isSelect) {
                if ($(m.html).val() === null) {
                    $.fn.MensajeProcesos({
                        posicion: m.posicion,
                        mensaje: '<strong>' + name_com + '</strong>, es un campo obligatorio, por favor seleccione una opción.',
                        titulo: m.titulo ,
                        tipo_mensaje: m.tipo
                    });
                    return false;
                } else if ($(m.html).val() === '') {
                    $.fn.MensajeProcesos({
                        posicion: m.posicion,
                        mensaje: '<strong>' + name_com + '</strong>, es un campo obligatorio, por favor seleccione una opción.',
                        titulo: m.titulo,
                        tipo_mensaje: m.tipo
                    });
                    return false;
                } else if ($(m.html).val() === 'undefined') {
                    $.fn.MensajeProcesos({
                        posicion: m.posicion,
                        mensaje: '<strong>' + name_com + '</strong>, es un campo obligatorio, por favor seleccione una opción.',
                        titulo: m.titulo,
                        tipo_mensaje: m.tipo
                    });
                    return false;
                }
            } else {
                if ($(m.html).val() === null || $(m.html).val() === '') {
                    $.fn.MensajeProcesos({
                        posicion: m.posicion,
                        mensaje: '<strong>' + name_com + '</strong>, es un campo obligatorio, por favor ingrese los datos necesarios.',
                        titulo: m.titulo,
                        tipo_mensaje: m.tipo
                    });
                    return false;
                }
            }
        }
        return true;
    }

    //$.fn.ValidarInput = function (m) {
    //    m.html = m.html || "";
    //    m.isSelect = m.isSelect || false;

    //    //var errorContainer = $('<span class="validar-input">requerido</span>');
    //    var errorContainer = $('<div class="validar-input">requerido</div>');
    //    if (m.html == "") {
    //        $.fn.MensajeProcesos({
    //            posicion: 'B',
    //            mensaje: 'No se a definido control ' + m.html.replace('#', '') + 'para la validación',
    //            titulo: 'Mensaje del Sistema',
    //            tipo_mensaje: 'error'
    //        });
    //        $(m.html).after(errorContainer);
    //        return false;
    //    } else {
    //        if (m.isSelect) {
    //            if ($(m.html).val() === null) {
    //                $(m.html).after(errorContainer);
    //                return false;
    //            } else if ($(m.html).val() === '') {
    //                $(m.html).after(errorContainer);
    //                return false;
    //            } else if ($(m.html).val() === 'undefined') {
    //                $(m.html).after(errorContainer);
    //                return false;
    //            }
    //        } else {
    //            if ($(m.html).val() === null || $(m.html).val() === '') {
    //                //$('label[for="' + m.html.replace('#', '').trim() + '"]').addClass('validar-input');//.css('right', '15px').css('bottom', '30px');
    //                $(m.html).after(errorContainer);
    //                return false;
    //            }
    //        }
    //    }
    //    //$(m.html).removeClass("validar");
    //    return true;
    //}

    //$.fn.ValidarInput = function (m) {
    //    m.html = m.html || "";
    //    m.isSelect = m.isSelect || false;
    //    if (m.html == "") {
    //        $.fn.MensajeProcesos({
    //            posicion: 'B',
    //            mensaje: 'No se a definido control ' + m.html.replace('#', '') + 'para la validación',
    //            titulo: 'Mensaje del Sistema',
    //            tipo_mensaje: 'error'
    //        });
    //        $(m.html).addClass('validar');
    //        return false;
    //    } else {
    //        if (m.isSelect) {
    //            if ($(m.html).val() === null) {
    //                $(m.html).addClass('validar');
    //                return false;
    //            } else if ($(m.html).val() === '') {
    //                $(m.html).addClass('validar');
    //                return false;
    //            } else if ($(m.html).val() === 'undefined') {
    //                $(m.html).addClass('validar');
    //                return false;
    //            }
    //        } else {
    //            if ($(m.html).val() === null || $(m.html).val() === '') {
    //                //$('label[for="' + m.html.replace('#', '').trim() + '"]').addClass('validar-input');//.css('right', '15px').css('bottom', '30px');
    //                $(m.html).addClass("validar-input");
    //                return false;
    //            }
    //        }
    //    }
    //    //$(m.html).removeClass("validar");
    //    return true;
    //}

})(jQuery);