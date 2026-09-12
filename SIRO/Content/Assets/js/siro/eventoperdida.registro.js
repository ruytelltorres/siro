window.onload = function () {

}

var gValorTipoCambio = 0.0;

function AgregarCuentaContable() {
    bootbox.prompt({
        size: "small",
        title: "Cuenta Contable",
        inputType: 'text',
        placeholder: 'Código de cuenta contable',
        closeButton: false,
        buttons: {
            confirm: {
                label: 'Correcto',
                className: 'btn-primary'
            },
            cancel: {
                label: 'Cancelar',
                className: 'btn-default'
            }
        },
        callback: function (result) {
            if (result != "") {
                $.fn.Conexion({
                    direccion: '/EventoPerdida/ListarCuentasContables',
                    datos: { psFiltro: result },
                    terminado: function (data) {
                        datos = JSON.parse(data);
                        if (datos != null) {
                            let filaCuenta = $("#tbl-cta-contable").find('tbody tr').length;
                            $("#tbl-cta-contable>tbody").prepend('<tr id="cta' + (filaCuenta + 1) + '"><td class="text-center text-primary">' + datos.cCtaCod + '</td>' +
                                '<td class="text-primary">' + datos.cCtaCodDesc + '</td>' +
                                '<td style="text-align: center">' +
                                '<div class="btn-group">' +
                                '<button type="button" class="btn btn-primary btn-sm">Acción</button>' +
                                '<button type="button" class="btn btn-primary btn-sm dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                                '<span class="caret"></span>' +
                                '<span class="sr-only">Toggle Dropdown</span>' +
                                '</button>' +
                                '<ul class="dropdown-menu dropdown-default pull-right">' +
                                '<li><a href="javascript:EliminarCuentaContable(' + (filaCuenta + 1) + ')"><i class="entypo-trash"></i>Eliminar</a></li>' +
                                '</ul>' +
                                '</div>' +
                                '</td>');

                        } else {
                            $.fn.MensajeProcesos({
                                posicion: 'A',
                                mensaje: 'La cuenta contable ingresada no existe',
                                titulo: 'Cuenta Contable',
                                tipo_mensaje: 'informacion'
                            });
                        }
                    },
                });
            }
        }
    });
}

function EliminarCuentaContable(fila) {
    $("#cta" + fila).remove();
}

$("#selEventoPerdida").change(function () {
    $.fn.Conexion({
        direccion: '/EventoPerdida/ObtenerSubClasesEventoPerdida',
        datos: { pnClaseEventoP: $(this).val() },
        terminado: function (data) {
            datos = JSON.parse(data);
            $("#selSubEventoPerdida").selselectboxit({ dataShow: "cDescSubClasEventoP", dataValue: "nCodSubClasEventoP", datalist: datos });
        },
    });

});

$("#selAreas").change(function () {
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ListarProcesosAreas',
        datos: { psCodArea: $(this).val() },
        terminado: function (data) {
            datos = JSON.parse(data);
            $("#selProcesos").selselect2({ dataShow: "cDescProceso", dataValue: "cCodProceso", datalist: datos.oLstProcesoArea });
        }
    });
});

$("#selProcesos").change(function () {
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ListarSubProcesosAreas',
        datos: { "psCodProceso": $(this).val() },
        terminado: function (data) {
            datos = JSON.parse(data);
            $("#selSubProcesos").selselect2({ dataShow: "cDescSubProceso", dataValue: "nCodSubProceso", datalist: datos.oLstSubProcesoArea });
        }
    });
});

$("#selLineaNeg").change(function () {
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ListarSubLineaNegocio',
        datos: { "psCodLineaNegocio": $(this).val() },
        terminado: function (data) {
            datos = JSON.parse(data);
            $("#selSubLineaNeg").selselectboxit({ dataShow: "cDescSubLineaNeg", dataValue: "cCodSubLineaNeg", datalist: datos.oListSubLineaNegocio });
        }
    });
});

$("#selMonedaMontoPerdida").change(function () {
    if ($(this).val() == 2) {
        if (gValorTipoCambio == 0) {
            $.fn.MensajeProcesos({
                posicion: 'A',
                mensaje: 'Ingrese la FECHA DE OCURRENCIA para obtener el tipo de cambio',
                titulo: 'Tipo de Cambio',
                tipo_mensaje: 'advertencia'
            });
            $(this).data("selectBox-selectBoxIt").selectOption("1");
        } else {
            CalulaMontoBruto();
        }
    }
});

$("#selMonedaMontoRecuperado").change(function () {
    if ($(this).val() == 2) {
        if (gValorTipoCambio == 0) {
            $.fn.MensajeProcesos({
                posicion: 'A',
                mensaje: 'Ingrese la FECHA DE OCURRENCIA para obtener el tipo de cambio',
                titulo: 'Tipo de Cambio',
                tipo_mensaje: 'advertencia'
            });
            $(this).data("selectBox-selectBoxIt").selectOption("1");
        }
    }
});


$("#inEventoPadre").on({
    //"onblur": function (event) {
    //    //Se ejecuta al perder el foco

    //    debugger;

    //    $.fn.Conexion({
    //        direccion: '/EventoPerdida/ValidarEventoPerdida',
    //        datos: { evento: $("#inEventoPadre").val().trim() },
    //        terminado: function (data) {
    //            debugger;
    //            if (data.Valor == false) {
    //                $.fn.MensajeProcesos({
    //                    posicion: 'A',
    //                    mensaje: 'El evento de pérdida ingresado no existe',
    //                    titulo: 'Agrupar Evento',
    //                    tipo_mensaje: 'informacion'
    //                });
    //            }
    //        },
    //    });

    //},
    "keyup": function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == "13") {
            event.preventDefault();
            $.fn.Conexion({
                direccion: '/EventoPerdida/ValidarEventoPerdida',
                datos: { evento: $("#inEventoPadre").val().trim() },
                terminado: function (data) {
                    debugger;
                    if (data.Valor == false) {
                        $.fn.MensajeProcesos({
                            posicion: 'A',
                            mensaje: 'El evento de pérdida ingresado no existe',
                            titulo: 'Agrupar Evento',
                            tipo_mensaje: 'informacion'
                        });
                    }
                },
            });
        }
    }
});



// #region Validacion de Fechas

$("#inFecOcurrencia").on({
    "keyup": function (event) {
        $(event.target).val(function (index, value) {
            if (moment(ValidarFormatoFecha(value)).isValid()) {
                if (moment(moment(ValidarFormatoFecha(value)).format("L")).diff(moment().format("MM-DD-YYYY"), "days") > 0) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'La FECHA DE OCURRENCIA no debe ser mayor la fecha actual',
                        titulo: 'Mensaje del Sistema',
                        tipo_mensaje: 'informacion'
                    });
                    return "";
                }
            }
            return value;
        });
    }
});

$("#inFecDescubrimiento").on({
    "keyup": function (event) {
        $(event.target).val(function (index, value) {
            if (moment(ValidarFormatoFecha(value)).isValid()) {
                if (moment(moment(ValidarFormatoFecha(value)).format("L")).diff(moment().format("MM-DD-YYYY"), "days") > 0) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'La FECHA DE DESCUBRIMIENTO no debe ser mayor la fecha actual',
                        titulo: 'Mensaje del Sistema',
                        tipo_mensaje: 'informacion'
                    });
                    return "";
                }
            }
            return value;
        });
    }
});

$("#inFecRegContable").on({
    "keyup": function (event) {
        $(event.target).val(function (index, value) {
            if (moment(ValidarFormatoFecha(value)).isValid()) {
                if (moment(moment(ValidarFormatoFecha(value)).format("L")).diff(moment().format("MM-DD-YYYY"), "days") > 0) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'La fecha del REGISTRO CONTABLE no debe ser mayor la fecha actual',
                        titulo: 'Mensaje del Sistema',
                        tipo_mensaje: 'informacion'
                    });
                    return "";
                }
            }
            return value;
        });
    }
});
// #endregion

// #region Montos
$("#inMontoPerdida, #inMontoRecuperado, #inMontoProvisiona").on({
    "focus": function (event) {
        CalulaMontoBruto();
        CalculaMontoPerdidaNeta();
    },
    "keyup": function (event) {
        $(event.target).val(function (index, value) {
            return value.replace(/\D/g, "")
                .replace(/([0-9])([0-9]{2})$/, '$1.$2')
                .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");
        });
        CalulaMontoBruto();
        CalculaMontoPerdidaNeta();
    }
});
// #endregion

function CalulaMontoBruto() {
    var table = document.getElementById("tbl-det-gastos");
    var rowCount = table.rows.length;
    var lnGastos = 0.00;
    if (rowCount > 1) {
        $("#tbl-det-gastos tbody tr").each(function () {
            if (eval($(this).find('select[id="xMoneda"]').val()) == 2) {
                lnGastos = lnGastos + (numeral($(this).find('input[id="xMonto"]').val()).value() * numeral(gValorTipoCambio).value());
            } else {
                lnGastos = lnGastos + numeral($(this).find('input[id="xMonto"]').val()).value();
            }
        });
    }
    var lnMontoBruto = numeral(inMontoPerdida.value).value() + numeral(lnGastos).value();
    $("#td_MontoBruto").html($("#selMonedaMontoPerdida").val() == 2 ? numeral(lnMontoBruto * gValorTipoCambio).format("0,0.00") : numeral(lnMontoBruto).format("0,0.00"));
}

function CalculaMontoPerdidaNeta() {
    var lnMontoBruto = numeral($("#td_MontoBruto").html()).value();
    var lnPerdidaneta = lnMontoBruto - numeral(inMontoRecuperado.value).value();
    if (numeral(lnPerdidaneta).value() < 0) {
        inMontoRecuperado.value = "";
        return false;
    }
    $("#td_PerdidaNeta").html(numeral(lnPerdidaneta).format("0,0.00"));
}


function AgregarGasto() {
    if ($("#inFecOcurrencia").val() == "" | $("#inFecDescubrimiento").val() == "" | $("#inFecRegContable").val()) {
        $.fn.MensajeProcesos({
            posicion: 'A',
            mensaje: 'No se puede agregar gastos hasta que todas las fechas esten debidamente completadas.',
            titulo: 'Detalle de Gasto',
            tipo_mensaje: 'informacion'
        });
        return false;
    }
    let filaGasto = $("#tbl-det-gastos").find('tbody tr').length;

    var inputGlosa = '<input id="xGlosa" class="form-control" style="border:none" placeholder="Glosa del gasto" autocomplete="off"/>';
    var selectMoneda = '<select id="xMoneda" class="form-control" style="border:none"><option value = "1" selected> (S /) Soles</option><option value="2">($) D&oacute;lares</option></select>';
    var inputMonto = '<input id="xMonto" class="form-control text-right" style="border:none" placeholder="0.00" onchange="CalulaMontoBruto();" autocomplete="off"/>';

    $("#tbl-det-gastos>tbody").prepend('<tr id="gasto' + (filaGasto + 1) + '"><td>' + inputGlosa + '</td>' +
        '<td>' + selectMoneda + '</td>' +
        '<td>' + inputMonto + '</td>' +
        '<td style="text-align: center">' +
        '<div class="btn-group">' +
        '<button type="button" class="btn btn-primary btn-sm">Acción</button>' +
        '<button type="button" class="btn btn-primary btn-sm dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
        '<span class="caret"></span>' +
        '<span class="sr-only">Toggle Dropdown</span>' +
        '</button>' +
        '<ul class="dropdown-menu dropdown-default pull-right">' +
        '<li><a href="javascript:EliminarGasto(' + (filaGasto + 1) + ')"><i class="entypo-trash"></i>Eliminar</a></li>' +
        '</ul>' +
        '</div>' +
        '</td>');

    $("#xMonto").on({
        "focus": function (event) {
            $(event.target).select();
        },
        "keyup": function (event) {
            $(event.target).val(function (index, value) {
                return value.replace(/\D/g, "").replace(/([0-9])([0-9]{2})$/, '$1.$2').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");
            });
            CalulaMontoBruto();
        }
    });
}

function EliminarGasto(fila) {
    $("#gasto" + fila).remove();

    CalulaMontoBruto();
    CalculaMontoPerdidaNeta();
}

function Validar() {
    var validacion = true;
    var vacias = 0;

    validacion = $.fn.ValidarInput({ html: "#selDescCortaEvento", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#inEventoDesc" });
    validacion = $.fn.ValidarInput({ html: "#selAgencia", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selAreas", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#inFecOcurrencia" });
    validacion = $.fn.ValidarInput({ html: "#inFecDescubrimiento" });
    validacion = $.fn.ValidarInput({ html: "#inFecRegContable" });
    validacion = $.fn.ValidarInput({ html: "#inAnio" });
    validacion = $.fn.ValidarInput({ html: "#selEventoPerdida", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selSubEventoPerdida", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#inCtaContable" });
    validacion = $.fn.ValidarInput({ html: "#selLineaNeg", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selSubLineaNeg", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selCobertura", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selProcesos", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selSubProcesos", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#inMontoPerdida" });
    validacion = $.fn.ValidarInput({ html: "#inMontoRecuperado" });
    validacion = $.fn.ValidarInput({ html: "#inMontoProvisiona" });

    if (parseInt($("#inAnio").val()) < 1999) {
        validacion = false;
    }

    /*Validacion de las Cuentas Contables*/
    var tblCtaCont = document.getElementById("tbl-cta-contable");
    if (tblCtaCont.rows.length >= 1) {
        $.fn.MensajeProcesos({
            posicion: 'B',
            mensaje: 'Debe registrar las cuentas contables del Evento de Pérdida.',
            titulo: 'Cuentas Contables',
            tipo_mensaje: 'advertencia'
        });
        validacion = false;
    } 

    /*Validacion del detalle de los gastos*/
    var table = document.getElementById("tbl-det-gastos");
    var rowCount = table.rows.length;
    if (rowCount > 1) {
        //var vacias = 0;
        //var errorContainer = $('<div class="validar-input">requerido</div>');
        $("#tbl-det-gastos tbody tr").each(function () {
            //loGastos[0] = {
            //vacias += $(this).find('input[id="xGlosa"]').val() == "" ? $(this).find('input[id="xGlosa"]').addClass("validar") : $(this).find('input[id="xGlosa"]').removeClass("validar");
            $(this).find('input[id="xGlosa"]').val() == "" ? $(this).find('input[id="xGlosa"]').css("background-color", "#E2ACA8") : $(this).find('input[id="xGlosa"]').css("border-color", "#FFFFFF");
        });
        validacion = false;
        //if (vacias > 0) {
        //    $.fn.MensajeProcesos({
        //        posicion: 'A',
        //        mensaje: 'El detalle de los gastos cuenta con glosas vacías, por favor corregir.',
        //        titulo: 'Detalle de Gasto',
        //        tipo_mensaje: 'informacion'
        //    });
        //    validacion = false;
        //}
    }


    return validacion;
}

function getTpoCambio(fecha) {
    $.fn.Conexion({
        direccion: '/EventoPerdida/ObtenerTipoCambio',
        datos: { psFecha: fecha },
        terminado: function (data) {
            gValorTipoCambio = data.Valor;
        }
    });
}

$("#btnGrabar").click(function (e) {
    e.preventDefault();

    if (!Validar()) { return false; }

    var loGastos = [];
    var loCuentas = [];

    var selDescCortaEvento = $("#selDescCortaEvento");
    var inEventoPadre = $("#inEventoPadre");
    var inEventoDesc = $("#inEventoDesc");
    var inMedidasDesc = $("#inMedidasCorrectivas");
    var inAccionesDesc = $("#inAccionesRealizadas");
    var selAgencia = $("#selAgencia");
    var selAreas = $("#selAreas");
    var inFecOcurrencia = $("#inFecOcurrencia");
    var inFecDescubrimiento = $("#inFecDescubrimiento");
    var inFecRegContable = $("#inFecRegContable");
    var inAnio = $("#inAnio");
    var selEventoPerdida = $("#selEventoPerdida");
    var selSubEventoPerdida = $("#selSubEventoPerdida");
    var inReportado = $("#inReportado").is(':checked') ? 1 : 0;
    var inCtaContable = $("#inCtaContable");
    var selLineaNeg = $("#selLineaNeg");
    var selSubLineaNeg = $("#selSubLineaNeg");
    var selCobertura = $("#selCobertura");
    var selProcesos = $("#selProcesos");
    var selSubProcesos = $("#selSubProcesos");
    var selPenMontoPerdida = $("#selMonedaMontoPerdida");
    var inMontoPerdida = $("#inMontoPerdida").val() != "" ? numeral($("#inMontoPerdida").val()).value() : 0;
    var selPenMontoRecuperado = $("#selMonedaMontoRecuperado");
    var inMontoRecuperado = $("#inMontoRecuperado").val() != "" ? numeral($("#inMontoRecuperado").val()).value() : 0;
    var selMontoProvisiona = $("#selMonedaMontoProvisiona");
    var inMontoProvisiona = $("#inMontoProvisiona").val() != "" ? numeral($("#inMontoProvisiona").val()).value() : 0;
    var valMontoMontoBruto = $("#td_MontoBruto").html() != "" ? numeral($("#td_MontoBruto").html()).value() : 0;
    var valMontoPerdidaNeta = $("#td_PerdidaNeta").html() != "" ? numeral($("#td_PerdidaNeta").html()).value() : 0;
    var inAsocRiesgo = $("#inAsocRiesgo").is(':checked') ? 1 : 0;

    /*Obtenermos el arreglo del para las cuentas*/
    let lnTotalCuenta = $("#tbl-cta-contable").find('tbody tr').length;
    if (lnTotalCuenta > 0) {
        $("#tbl-cta-contable tbody tr").each(function (i) {
            loCuentas[i] = {
                cCodCuenta: $("#cta" + (i + 1)).find("td").html()
            };
        });
    }

    /*Obtenermos el arreglo del para los gastos*/
    let lnTotalGastos = $("#tbl-det-gastos").find('tbody tr').length;
    if (lnTotalGastos > 0) {
        $("#tbl-det-gastos tbody tr").each(function (i) {
            loGastos[i] = {
                cGlosaGasto: $(this).find('input[id="xGlosa"]').val(),
                nPenGasto: $(this).find('select[id="xMoneda"]').val(),
                nMontoGasto: parseFloat($(this).find('input[id="xMonto"]').val()).toFixed(2)
            };
        });
    }

    $.fn.Conexion({
        direccion: '/EventoPerdida/GrabarEventoPerdida',
        datos: {
            pnCodDescCorta: selDescCortaEvento.val(), __psEventoDesc: inEventoDesc.val().trim(), __psMedidasDesc: inMedidasDesc.val().trim(), __psAccionesDesc: inAccionesDesc.val().trim(),
            psAgeCod: selAgencia.val(), psAreaCod: selAreas.val(), pdFechaOcurrencia: inFecOcurrencia.val(), pdFechaDescubrimiento: inFecDescubrimiento.val(), pdFechaRegContable: inFecRegContable.val(),
            psAnio: inAnio.val(), pnClasEventoP: selEventoPerdida.val(), pnSubClasEventoP: selSubEventoPerdida.val(), pnReportado: inReportado, psCtaCont: "",
            psLineaNeg: selLineaNeg.val(), psSubLineaNeg: selSubLineaNeg.val(), pnCobertura: selCobertura.val(), psProceso: selProcesos.val(), pnSubProceso: selSubProcesos.val(),
            pnPenMontoPerdida: selPenMontoPerdida.val(), pnMontoPerdida: inMontoPerdida, pnPenMontoRecupera: selPenMontoRecuperado.val(), pnMontoRecuperado: inMontoRecuperado,
            pnPenMontoProvisiona: selMontoProvisiona.val(), pnMontoProvision: inMontoProvisiona, pnMontoBruto: valMontoMontoBruto, pnPerdidaNeta: valMontoPerdidaNeta,
            pbAsocRiesgo: inAsocRiesgo, psCodEventoPadre: inEventoPadre.val().trim(), poDetCta: (lnTotalCuenta > 0 ? JSON.stringify(loCuentas) : ""),
            poDetGastos: (lnTotalGastos > 0 ? JSON.stringify(loGastos) : "")
        },
        terminado: function (data) {
            $.fn.MensajeProcesos({
                posicion: 'A',
                mensaje: 'Se registro correctamente el evento de pérdida ' + data.respuesta,
                titulo: 'Evento de Pérdida',
                tipo_mensaje: 'exito',
                onhidden: function () {
                    bootbox.confirm({
                        message: "<strong>¿Desea registrar otro Evento de Pérdida?</strong>",
                        size: 'sm',
                        closeButton: false,
                        buttons: {
                            confirm: {
                                label: 'SI',
                                className: 'btn-primary'
                            },
                            cancel: {
                                label: 'NO',
                                className: 'btn-default'
                            }
                        },
                        callback: function (result) {
                            if (result) {
                                location.reload();
                            } else {
                                $.fn.Conexion({
                                    direccion: '/RiesgoOperacional/DevolverVista',
                                    datos: { Vista: "Lista", Controlador: "EventoPerdida" },
                                    terminado: function (url) {
                                        window.location.href = url;
                                    },
                                });
                            }
                        }
                    });
                }
            });
            location.reload();
        },
    });

});

$("#btnGrabarModificacion").click(function (e) {
    e.preventDefault();

    if (!Validar()) { return false; }

    var loGastos = [];
    var loCuentas = [];
    debugger;
    var selDescCortaEvento = $("#selDescCortaEvento");
    var inEventoPadre = $("#inEventoPadre"); //$("#inEventoPadre");
    var inEventoDesc = $("#inEventoDesc");
    var inMedidasDesc = $("#inMedidasCorrectivas");
    var inAccionesDesc = $("#inAccionesRealizadas");
    var selAgencia = $("#selAgencia");
    var selAreas = $("#selAreas");
    var inFecOcurrencia = $("#inFecOcurrencia");
    var inFecDescubrimiento = $("#inFecDescubrimiento");
    var inFecRegContable = $("#inFecRegContable");
    var inAnio = $("#inAnio");
    var selEventoPerdida = $("#selEventoPerdida");
    var selSubEventoPerdida = $("#selSubEventoPerdida");
    var inReportado = $("#inReportado").is(':checked') ? 1 : 0;
    var inCtaContable = $("#inCtaContable");
    var selLineaNeg = $("#selLineaNeg");
    var selSubLineaNeg = $("#selSubLineaNeg");
    var selCobertura = $("#selCobertura");
    var selProcesos = $("#selProcesos");
    var selSubProcesos = $("#selSubProcesos");
    var selPenMontoPerdida = $("#selMonedaMontoPerdida");
    var inMontoPerdida = $("#inMontoPerdida").val() != "" ? numeral($("#inMontoPerdida").val()).value() : 0;
    var selPenMontoRecuperado = $("#selMonedaMontoRecuperado");
    var inMontoRecuperado = $("#inMontoRecuperado").val() != "" ? numeral($("#inMontoRecuperado").val()).value() : 0;
    var selMontoProvisiona = $("#selMonedaMontoProvisiona");
    var inMontoProvisiona = $("#inMontoProvisiona").val() != "" ? numeral($("#inMontoProvisiona").val()).value() : 0;
    var valMontoMontoBruto = $("#td_MontoBruto").html() != "" ? numeral($("#td_MontoBruto").html()).value() : 0;
    var valMontoPerdidaNeta = $("#td_PerdidaNeta").html() != "" ? numeral($("#td_PerdidaNeta").html()).value() : 0;
    var inAsocRiesgo = $("#inAsocRiesgo").is(':checked') ? 1 : 0;

    debugger;
    /*Obtenermos el arreglo del para las cuentas*/
    let lnTotalCuenta = $("#tbl-cta-contable").find('tbody tr').length;
    if (lnTotalCuenta > 0) {
        $("#tbl-cta-contable tbody tr").each(function (i) {
            loCuentas[i] = {
                cCodCuenta: $("#cta" + (i + 1)).find("td").html()
            };
        });
    }

    /*Obtenermos el arreglo del para los gastos*/
    let lnTotalGastos = $("#tbl-det-gastos").find('tbody tr').length;
    if (lnTotalGastos > 0) {
        $("#tbl-det-gastos tbody tr").each(function (i) {
            loGastos[i] = {
                cGlosaGasto: $(this).find('input[id="xGlosa"]').val(),
                nPenGasto: $(this).find('select[id="xMoneda"]').val(),
                nMontoGasto: parseFloat($(this).find('input[id="xMonto"]').val()).toFixed(2)
            };
        });
    }
    debugger;
    $.fn.Conexion({
        direccion: '/EventoPerdida/GrabarActualizacionEventoPerdida',
        datos: {
            pnCodEvento: gModelEvento.oDatosRiesgo.nNroRiesgo, pnCodDescCorta: selDescCortaEvento.val(), __psEventoDesc: inEventoDesc.val().trim(), __psMedidasDesc: inMedidasDesc.val().trim(),
            __psAccionesDesc: inAccionesDesc.val().trim(), psAgeCod: selAgencia.val(), psAreaCod: selAreas.val(), pdFechaOcurrencia: inFecOcurrencia.val(), pdFechaDescubrimiento: inFecDescubrimiento.val(),
            pdFechaRegContable: inFecRegContable.val(), psAnio: inAnio.val(), pnClasEventoP: selEventoPerdida.val(), pnSubClasEventoP: selSubEventoPerdida.val(), pnReportado: inReportado, psCtaCont: "",
            psLineaNeg: selLineaNeg.val(), psSubLineaNeg: selSubLineaNeg.val(), pnCobertura: selCobertura.val(), psProceso: selProcesos.val(), pnSubProceso: selSubProcesos.val(),
            pnPenMontoPerdida: selPenMontoPerdida.val(), pnMontoPerdida: inMontoPerdida, pnPenMontoRecupera: selPenMontoRecuperado.val(), pnMontoRecuperado: inMontoRecuperado,
            pnPenMontoProvisiona: selMontoProvisiona.val(), pnMontoProvision: inMontoProvisiona, pnMontoBruto: valMontoMontoBruto, pnPerdidaNeta: valMontoPerdidaNeta, pbAsocRiesgo: inAsocRiesgo,
            psCodEventoPadre: inEventoPadre.is(":visible") ? inEventoPadre.val().trim() : "", poDetCta: (lnTotalCuenta > 0 ? JSON.stringify(loCuentas) : ""), poDetGastos: (lnTotalGastos > 0 ? JSON.stringify(loGastos) : "")
        },
        terminado: function (data) {
            $.fn.MensajeProcesos({
                posicion: 'A',
                mensaje: 'Se registro correctamente el evento de pérdida ' + data.respuesta,
                titulo: 'Evento de Pérdida',
                tipo_mensaje: 'exito'
            });
            location.reload();
        },
    });

});

$("#btnMostrarGrupo").click(function (e) {
    e.preventDefault();
    var ObjParam = { evento: base64_encode(gModelEvento.oDatosRiesgo.nNroRiesgo) };
    debugger;
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVistaParam',
        datos: { Vista: "Grupo", Controlador: "EventoPerdida", Parametros: JSON.stringify(ObjParam) },
        terminado: function (data) {
            window.location = data.url;
        },
    });
});





