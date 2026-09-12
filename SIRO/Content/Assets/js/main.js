//$(function () {
//    $('#main-menu').metisMenu();
//});

////Loads the correct sidebar on window load,
////collapses the sidebar on window resize.
//// Sets the min-height of #page-wrapper to window size
//$(function () {
//    $(window).bind("load resize", function () {
//        var topOffset = 50;
//        var width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
//        if (width < 768) {
//            $('div.navbar-collapse').addClass('collapse');
//            topOffset = 100; // 2-row-menu
//        } else {
//            $('div.navbar-collapse').removeClass('collapse');
//        }

//        var height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
//        height = height - topOffset;
//        if (height < 1) height = 1;
//        //if (height > topOffset) {
//        //    $("#page-wrapper").css("min-height", (height) + "px");
//        //}
//    });

//    //var url = window.location;
//    //// var element = $('ul.nav a').filter(function() {
//    ////     return this.href == url;
//    //// }).addClass('active').parent().parent().addClass('in').parent();
//    //var element = $('ul.nav a').filter(function () {
//    //    return this.href == url;
//    //}).addClass('active').parent();

//    //while (true) {
//    //    if (element.is('li')) {
//    //        element = element.parent().addClass('in').parent();
//    //    } else {
//    //        break;
//    //    }
//    //}
//});

//bootbox.setDefaults({
//    locale: "fr",
//    show: true,
//    backdrop: true,
//    closeButton: false,
//    animate: true,
//    className: "my-modal"

//});


//function AjustarMenu(page) {
//    topOffset = 50;
//    width = (page.window.innerWidth > 0) ? page.window.innerWidth : page.screen.width;
//    if (width < 768) {
//        $('div.navbar-collapse').addClass('collapse')
//        topOffset = 100; // 2-row-menu
//    } else {
//        $('div.navbar-collapse').removeClass('collapse')
//    }

//    $header = $('header').height();
//    $nav = $('nav').height() + 1;
//    $footer = $('footer').height();


//    height = (page.window.innerHeight > 0) ? page.window.innerHeight : page.screen.height;
//    height = height - ($header + $nav + $footer);

//    $("#menu-sistema").removeAttr("style", "overflow-y:auto;height:650px");

//    if (height < 1) height = 1;
//    if (height > topOffset) {
//        $("#page-wrapper").css("min-height", (height) + "px");
//        if (width >= 768) {
//            $("#menu-sistema").attr("style", "overflow-y:auto;height:650px");
//            $("#menu-sistema").height((height) + 'px');
//        }
//    }
//}


(function ($) {
    $.fn.dropdowlist = function (m) {

        m.dataShow = m.dataShow || "";
        m.dataValue = m.dataValue || "";
        m.dataselect = m.dataselect || "";
        m.datalist = m.datalist || null;
        m.contenedor = m.contenedor || this;

        $(m.contenedor).html('');
        for (var i in m.datalist) {
            if (m.dataselect != "") {
                if (m.dataselect == eval("m.datalist[i]." + m.dataValue)) {
                    m.contenedor.append('<option data-index="' + i + '" value="' + eval("m.datalist[i]." + m.dataValue) + '" selected="true" datashow="' + eval("m.datalist[i]." + m.dataShow) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
                }
                else {
                    m.contenedor.append('<option data-index="' + i + '" value="' + eval("m.datalist[i]." + m.dataValue) + '"  datashow="' + eval("m.datalist[i]." + m.dataShow) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
                }
            }
            else {
                m.contenedor.append('<option data-index="' + i + '" value="' + eval("m.datalist[i]." + m.dataValue) + '"  datashow="' + eval("m.datalist[i]." + m.dataShow) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
            }
        }
    }
})(jQuery);

(function ($) {
    $.fn.selselectboxit = function (m) {
        m.dataShow = m.dataShow || "";
        m.dataValue = m.dataValue || "";
        m.dataselect = m.dataselect || "";
        m.datagroup = m.datagroup || "";
        m.datalist = m.datalist || null;
        m.contenedor = m.contenedor || this;
        //m.datatext = m.datatext || "";
        $(m.contenedor).html('');

        $(m.contenedor).selectBoxIt({ autoWidth: false });
        $(m.contenedor).append('<option>Seleccione una opci\u00F3n</option>');
        for (var i in m.datalist) {
            if (m.dataselect != "") {
                if (m.dataselect == eval("m.datalist[i]." + m.dataValue)) {
                    //$(m.contenedor).data("selectBox-selectBoxIt").add({ value: eval("m.datalist[i]." + m.dataValue), text: eval("m.datalist[i]." + m.dataShow) });
                    //$(m.contenedor).selectBoxIt('selectOption', m.dataselect); //Esta accion selecciona  y a la vez activa el evento
                    $(m.contenedor).append('<option value="' + eval("m.datalist[i]." + m.dataValue) + '" selected>' + eval("m.datalist[i]." + m.dataShow) + '</option>');
                } else {
                    $(m.contenedor).append('<option value="' + eval("m.datalist[i]." + m.dataValue) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
                }
            } else {
                $(m.contenedor).append('<option value="' + eval("m.datalist[i]." + m.dataValue) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
            }
        }
        $(m.contenedor).selectBoxIt("refresh");
    }
})(jQuery);

(function ($) {
    $.fn.selselect2 = function (m) {
        m.dataShow = m.dataShow || "";
        m.dataValue = m.dataValue || "";
        m.dataselect = m.dataselect || "";
        m.datagroup = m.datagroup || "";
        m.datalist = m.datalist || null;
        m.contenedor = m.contenedor || this;
        //m.modal = m.modal || document.body;

        $(m.contenedor).html('');
        $(m.contenedor).append('<option></option>');
        for (var i in m.datalist) {
            if (m.dataselect != "") {
                if (m.dataselect == eval("m.datalist[i]." + m.dataValue)) {
                    $(m.contenedor).append('<option value="' + eval("m.datalist[i]." + m.dataValue) + '" selected="selected">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
                } else {
                    $(m.contenedor).append('<option value="' + eval("m.datalist[i]." + m.dataValue) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
                }
            } else {
                $(m.contenedor).append('<option value="' + eval("m.datalist[i]." + m.dataValue) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
            }
        }

        var opts = {
            allowClear: true,
            placeholder: "Seleccione una opci\u00F3n",
            //dropdownParent: $("#" + m.modal + "")
            //width: 'resolve',
        };

        //$('#selAreas').select2({
        //    dropdownParent: $('#ModalFiltroTpoEval')
        //});

        $(m.contenedor).select2(opts);
    }
})(jQuery);

/**
 * Devuelve el valor del nivel de riesgo por escala de riesfo
 * @param {any} valor
 */
function DevoverNivelRiesgoValorEscala(Probabilidad, Impacto) {
    var $valor = 0;
    if ((Probabilidad == 3 && Impacto == 1) | (Probabilidad == 4 && Impacto == 1) |
        (Probabilidad == 5 && Impacto == 1) | (Probabilidad == 4 && Impacto == 2) |
        (Probabilidad == 5 && Impacto == 2)) {
        $valor = 1;
    }
    if ((Probabilidad == 2 && Impacto == 1) | (Probabilidad == 3 && Impacto == 2) |
        (Probabilidad == 4 && Impacto == 3) | (Probabilidad == 5 && Impacto == 3) |
        (Probabilidad == 1 && Impacto == 1)) {
        $valor = 2;
    }
    if ((Probabilidad == 1 && Impacto == 2) | (Probabilidad == 2 && Impacto == 2) |
        (Probabilidad == 2 && Impacto == 3) | (Probabilidad == 3 && Impacto == 3) |
        (Probabilidad == 4 && Impacto == 4) | (Probabilidad == 5 && Impacto == 4) |
        (Probabilidad == 5 && Impacto == 5)) {
        $valor = 3;
    }
    if ((Probabilidad == 1 && Impacto == 3) | (Probabilidad == 1 & Impacto == 4) |
        (Probabilidad == 1 && Impacto == 5) | (Probabilidad == 2 & Impacto == 4) |
        (Probabilidad == 2 && Impacto == 5) | (Probabilidad == 3 & Impacto == 4) |
        (Probabilidad == 3 && Impacto == 5) | (Probabilidad == 4 & Impacto == 5)) {
        $valor = 4;
    }

    return $valor;
}

function number_format(number, decimals, dec_point, thousands_sep) {
    var n = !isFinite(+number) ? 0 : +number,
        prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
        sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
        dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
        s = '',
        toFixedFix = function (n, prec) {
            var k = Math.pow(10, prec);
            return '' + Math.round(n * k) / k;
        };
    // Fix for IE parseFloat(0.55).toFixed(0) = 0;
    s = (prec ? toFixedFix(n, prec) : '' + Math.round(n)).split('.');
    if (s[0].length > 3) {
        s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
    }
    if ((s[1] || '').length < prec) {
        s[1] = s[1] || '';
        s[1] += new Array(prec - s[1].length + 1).join('0');
    }
    return s.join(dec);
}

function base64_encode(data) {
    //  discuss at: http://phpjs.org/functions/base64_encode/
    // original by: Tyler Akins (http://rumkin.com)
    // improved by: Bayron Guevara
    // improved by: Thunder.m
    // improved by: Kevin van Zonneveld (http://kevin.vanzonneveld.net)
    // improved by: Kevin van Zonneveld (http://kevin.vanzonneveld.net)
    // improved by: Rafał Kukawski (http://kukawski.pl)
    // bugfixed by: Pellentesque Malesuada
    //   example 1: base64_encode('Kevin van Zonneveld');
    //   returns 1: 'S2V2aW4gdmFuIFpvbm5ldmVsZA=='
    //   example 2: base64_encode('a');
    //   returns 2: 'YQ=='
    //   example 3: base64_encode('✓ à la mode');
    //   returns 3: '4pyTIMOgIGxhIG1vZGU='

    var b64 = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=';
    var o1, o2, o3, h1, h2, h3, h4, bits, i = 0,
        ac = 0,
        enc = '',
        tmp_arr = [];

    if (!data) {
        return data;
    }

    data = unescape(encodeURIComponent(data))

    do {
        // pack three octets into four hexets
        o1 = data.charCodeAt(i++);
        o2 = data.charCodeAt(i++);
        o3 = data.charCodeAt(i++);

        bits = o1 << 16 | o2 << 8 | o3;

        h1 = bits >> 18 & 0x3f;
        h2 = bits >> 12 & 0x3f;
        h3 = bits >> 6 & 0x3f;
        h4 = bits & 0x3f;

        // use hexets to index into b64, and append result to encoded string
        tmp_arr[ac++] = b64.charAt(h1) + b64.charAt(h2) + b64.charAt(h3) + b64.charAt(h4);
    } while (i < data.length);

    enc = tmp_arr.join('');

    var r = data.length % 3;

    return (r ? enc.slice(0, r - 3) : enc) + '==='.slice(r || 3);
}

function val_09(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    else if (tecla == 0) return true;
    else if (tecla == 9) return true;
    // else if (tecla == e.keyCode || tecla == e.which) return true;
    patron = /[0-9]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function val_09D(e) {
    tecla = (document.all) ? e.keyCode : e.which;

    if (tecla == 8) return true;
    else if (tecla == 0) return true;
    else if (tecla == 9) return true;
    else if (tecla == 46) return true;
    // else if (tecla == e.keyCode || tecla == e.which) return true;
    patron = /^[-+]?[0-9]+(\.[0-9]{2})?$/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function val_09DC(e, field) {
    key = e.keyCode ? e.keyCode : e.which
    // backspace
    if (key == 8) return true
    if (key == 0) return true
    // 0-9
    if (key > 47 && key < 58) {
        if (field.value == "") return true
        regexp = /[.][0-9]{2}$/
        return !(regexp.test(field.value))
    }
    // .
    if (key == 46) {
        if (field.value == "") return false
        regexp = /^[0-9]+$/
        return regexp.test(field.value)
    }
    // other key
    return false
}

function val_09DCN(e, field) {
    key = e.keyCode ? e.keyCode : e.which
    // backspace
    if (key == 8) return true
    if (key == 0) return true
    // 0-9
    if (key > 47 && key < 58) {
        if (field.value == "") return true
        regexp = /[.][0-9]{2}$/
        return !(regexp.test(field.value))
    }
    // .
    if (key == 46) {
        if (field.value == "") return false
        regexp = /^-?[0-9]+$/
        return regexp.test(field.value)
    }
    //signos
    if (key == 45) {
        if (field.value == "") return true

        var stringValor = String(field.value);
        if (stringValor.indexOf("-") > -1) return false

        regexp = /^-?[0-9]+$/
        return !(regexp.test(field.value))
    }
    // other key
    return false


}


function ValidarFormatoFecha(texto) {
    let partes = (texto || '').split('/'),
        fechaGenerada = new Date(partes[2], --partes[1], partes[0]);

    if (partes.length == 3 && fechaGenerada
        && partes[0] == fechaGenerada.getDate()
        && partes[1] == fechaGenerada.getMonth()
        && partes[2] == fechaGenerada.getFullYear()) {
        return fechaGenerada;
    }
    return false; //Inválida
}

/*!
   * jQuery toDictionary() plugin
   *
   * Version 1.2 (11 Apr 2011)
   *
   * Copyright (c) 2011 Robert Koritnik
   * Licensed under the terms of the MIT license
   * http://www.opensource.org/licenses/mit-license.php
   */

(function ($) {

    // #region String.prototype.format
    // add String prototype format function if it doesn't yet exist
    if ($.isFunction(String.prototype.format) === false) {
        String.prototype.format = function () {
            var s = this;
            var i = arguments.length;
            while (i--) {
                s = s.replace(new RegExp("\\{" + i + "\\}", "gim"), arguments[i]);
            }
            return s;
        };
    }
    // #endregion

    // #region Date.prototype.toISOString
    // add Date prototype toISOString function if it doesn't yet exist
    if ($.isFunction(Date.prototype.toISOString) === false) {
        Date.prototype.toISOString = function () {
            var pad = function (n, places) {
                n = n.toString();
                for (var i = n.length; i < places; i++) {
                    n = "0" + n;
                }
                return n;
            };
            var d = this;
            return "{0}-{1}-{2}T{3}:{4}:{5}.{6}Z".format(
                d.getUTCFullYear(),
                pad(d.getUTCMonth() + 1, 2),
                pad(d.getUTCDate(), 2),
                pad(d.getUTCHours(), 2),
                pad(d.getUTCMinutes(), 2),
                pad(d.getUTCSeconds(), 2),
                pad(d.getUTCMilliseconds(), 3)
            );
        };
    }
    // #endregion

    var _flatten = function (input, output, prefix, includeNulls) {
        if ($.isPlainObject(input)) {
            for (var p in input) {
                if (includeNulls === true || typeof (input[p]) !== "undefined" && input[p] !== null) {
                    _flatten(input[p], output, prefix.length > 0 ? prefix + "." + p : p, includeNulls);
                }
            }
        }
        else {
            if ($.isArray(input)) {
                $.each(input, function (index, value) {
                    _flatten(value, output, "{0}[{1}]".format(prefix, index));
                });
                return;
            }
            if (!$.isFunction(input)) {
                if (input instanceof Date) {
                    output.push({ name: prefix, value: input.toISOString() });
                }
                else {
                    var val = typeof (input);
                    switch (val) {
                        case "boolean":
                        case "number":
                            val = input;
                            break;
                        case "object":
                            // this property is null, because non-null objects are evaluated in first if branch
                            if (includeNulls !== true) {
                                return;
                            }
                        default:
                            val = input || "";
                    }
                    output.push({ name: prefix, value: val });
                }
            }
        }
    };

    $.extend({
        toDictionary: function (data, prefix, includeNulls) {
            /// <summary>Flattens an arbitrary JSON object to a dictionary that Asp.net MVC default model binder understands.</summary>
            /// <param name="data" type="Object">Can either be a JSON object or a function that returns one.</data>
            /// <param name="prefix" type="String" Optional="true">Provide this parameter when you want the output names to be prefixed by something (ie. when flattening simple values).</param>
            /// <param name="includeNulls" type="Boolean" Optional="true">Set this to 'true' when you want null valued properties to be included in result (default is 'false').</param>

            // get data first if provided parameter is a function
            data = $.isFunction(data) ? data.call() : data;

            // is second argument "prefix" or "includeNulls"
            if (arguments.length === 2 && typeof (prefix) === "boolean") {
                includeNulls = prefix;
                prefix = "";
            }

            // set "includeNulls" default
            includeNulls = typeof (includeNulls) === "boolean" ? includeNulls : false;

            var result = [];
            _flatten(data, result, prefix || "", includeNulls);

            return result;
        }
    });
})(jQuery);