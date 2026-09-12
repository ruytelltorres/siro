var aControl = new Array();
window.onload = function () {
    if (gModelo.oRiesgoOperacional != null) {
        getObs = gModelo.lstObservacionesGestion;
        if (getObs.length > 0) {
            for (var i in getObs) {
                switch (getObs[i].Proceso) {
                    case 0:
                        $.fn.MensajeProcesos({
                            posicion: 'A',
                            mensaje: getObs[i].Observacion,
                            titulo: 'Modificación del riesgo',
                            tiempo_limite: '0',
                            tiempo_extendido: '0',
                            tipo_mensaje: 'advertencia',
                            onclick: function () {
                                //$("#rootwizard").bootstrapWizard('show', 0);
                                //$("#btnGrabaPaso1").show();
                            }
                        });
                        break;
                }
            }
        }
    }


}

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
    var $CodProceso = $(this).val();
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ListarSubProcesosAreas',
        datos: { "psCodProceso": $CodProceso },
        terminado: function (data) {
            datos = JSON.parse(data);
            $("#selSubProcesos").selselect2({ dataShow: "cDescSubProceso", dataValue: "nCodSubProceso", datalist: datos.oLstSubProcesoArea });
        }
    });

    //Obtenemos los controles del area
    //$.fn.Conexion({
    //    direccion: '/RiesgoOperacional/ListaControlesProceso',
    //    datos: { psCodProceso: $CodProceso },
    //    terminado: function (data) {
    //        datos = JSON.parse(data);
    //        $("#selControlProcesos").selselect2({ dataShow: "cControlDescripcion", dataValue: "nCodControl", datalist: datos.oLstControlesAreas });
    //    }
    //});

});

$('.check-control-efectivo').on('switch-change', function (e, data) {
    if (data.value == true) {
        let controles = $('#tbl-controles-riesgo').find('tbody tr').length;
        if (controles == 0) {
            $('.check-control-efectivo').bootstrapSwitch('setState', false);
            $.fn.MensajeProcesos({
                posicion: 'A',
                mensaje: 'No es posible realizar la acción; no se definieron controles del riesgo.',
                titulo: 'Controles del Riesgo',
                tipo_mensaje: 'informacion'
            });
        }
    }
});


$("#btnRegRiesgoOpe").click(function (e) {
    e.preventDefault();
    if (!ValidarCampos()) { return false; }
    bootbox.confirm({
        message: "<strong>¿Está seguro de grabar el riesgo operativo?</strong>",
        size: "sm",
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
                var lsRiesgoDesc = $("#inRiesgoIdentificado").val().trim();
                var lsAgencia = $("#selAgencias").val();
                var lsFechaDeteccion = $("#inFechaDeteccion").val();
                var lsAreaAfectada = $("#selAreas").val();
                var lstCausas = $("#selCausaRiesgo").val();
                var lsEfectidadControl = $("#chkEfectividadControl").is(':checked') ? 1 : 0;
                var lsCausas = "";
                var i = 0;
                for (i in lstCausas) {
                    lsCausas += lstCausas[i] + ",";
                }
                if (lsCausas) {
                    lsCausas = lsCausas.slice(0, lsCausas.length - 1);
                }
                var lsProceso = $("#selProcesos").val();
                var lsSubProceso = $("#selSubProcesos").val();
                //var lsControlProceso = $("#selControlProcesos").val();
                var lsControlProceso = 0;
                var lsObservacion = $("#inObservacion").val().trim();

                //let lnTotalGastos = $("#tbl-det-gastos").find('tbody tr').length;
                //if (lnTotalGastos > 0) {
                var loControles = [];
                $("#tbl-controles-riesgo tbody tr").each(function (i) {
                    loControles[i] = {
                        cDescControles: $("#ctrl" + (i + 1)).find("td").html()
                    };
                });
                //}


                $.fn.Conexion({
                    direccion: '/RiesgoOperacional/RegistrarRiesgoOperacional',
                    datos: {
                        __psRiesgoIdent: lsRiesgoDesc, psAgeCod: lsAgencia, psFechaDetec: lsFechaDeteccion,
                        psAreaCod: lsAreaAfectada, psCausasRiesgo: lsCausas, psProceso: lsProceso, pnSubProceso: lsSubProceso,
                        psControlProceso: lsControlProceso, pbEfectividadCtrl: lsEfectidadControl, __psObservacion: lsObservacion,
                        psControles: JSON.stringify(loControles)
                        //psControles: lsControles
                    },
                    //datos: param,
                    bloqueo: true,
                    //mensaje: 'Procesando la información',
                    terminado: function (data) {
                        datos = JSON.parse(data);
                        console.log('datos', datos);
                        console.log('datos.valor', datos.valor);
                        console.log('gRespuestas.exito', gRespuestas.exito)
                        if (datos.valor === gRespuestas.exito) {
                            $.fn.MensajeProcesos({
                                clase: 'green',
                                posicion: 'A',
                                mensaje: '¡Gracias por informar del riesgo operativo!',
                                titulo: 'Riesgo ' + datos.obj,
                                tipo_mensaje: 'exito',
                                onhidden: function () {
                                    bootbox.confirm({
                                        message: "<strong>¿Desea registrar otro riesgo operativo?</strong>",
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
                                                //ResetearCampos();
                                                location.reload();
                                            } else {
                                                $.fn.Conexion({
                                                    direccion: '/RiesgoOperacional/DevolverVista',
                                                    datos: { Vista: "ListaGestion", Controlador: "RiesgoOperacional" },
                                                    terminado: function (url) {
                                                        window.location.href = url;
                                                    },
                                                });
                                            }
                                        }
                                    });
                                }
                            });
                        } else {
                            $.fn.MensajeProcesos({
                                clase: 'green',
                                posicion: 'A',
                                mensaje: datos.mensaje,
                                titulo: 'Error de registro',
                                tipo_mensaje: 'error'
                            });
                        }
                    }
                });
            }
        }
    });
});



$("#btnModificar").click(function (e) {
    e.preventDefault();

    if (!ValidarCampos()) { return false; }

    //var lsRiesgoDesc = $("#inRiesgoIdentificado").val();
    //var lsAgencia = $("#selAgencias").val();
    //var lsFechaDeteccion = $("#inFechaDeteccion").val();
    //var lsAreaAfectada = $("#selAreas").val();
    //var lstCausas = $("#selCausaRiesgo").val();
    //var lsEfectidadControl = $("#chkEfectividadControl").is(':checked') ? 1 : 0;
    //var lsCausas = "";
    //for (var i in lstCausas) {
    //    lsCausas += lstCausas[i] + ",";
    //}
    //if (lsCausas) {
    //    lsCausas = lsCausas.slice(0, lsCausas.length - 1);
    //}
    //var lsProceso = $("#selProcesos").val();
    //var lsSubProceso = $("#selSubProcesos").val();
    ////var lsControlProceso = $("#selControlProcesos").val();
    //var lsControlProceso = 0;
    //var lsObservacion = $("#inObservacion").val();

    //var loControles = [];
    //$("#tbl-controles-riesgo tbody tr").each(function (i) {
    //    loControles[i] = {
    //        cDescControles: $("#ctrl" + (i + 1)).find("td").html()
    //    };
    //});


    bootbox.confirm({
        message: "<strong>¿Está seguro de grabar la información modificada?</strong>",
        size: "sm",
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
                var lsRiesgoDesc = $("#inRiesgoIdentificado").val().trim();
                var lsAgencia = $("#selAgencias").val();
                var lsFechaDeteccion = $("#inFechaDeteccion").val();
                var lsAreaAfectada = $("#selAreas").val();
                var lstCausas = $("#selCausaRiesgo").val();

                var lsCausas = "";
                var i = 0;
                for (i in lstCausas) {
                    lsCausas += lstCausas[i] + ",";
                }
                if (lsCausas) {
                    lsCausas = lsCausas.slice(0, lsCausas.length - 1);
                }
                var lsProceso = $("#selProcesos").val();
                var lsSubProceso = $("#selSubProcesos").val();
                //var lsControlProceso = $("#selControlProcesos").val();
                var lsControlProceso = 0;
                var lsEfectidadControl = $("#chkEfectividadControl").is(':checked') ? 1 : 0;
                var lsObservacion = $("#inObservacion").val().trim();

                //let lnTotalGastos = $("#tbl-det-gastos").find('tbody tr').length;
                //if (lnTotalGastos > 0) {
                var loControles = [];
                $("#tbl-controles-riesgo tbody tr").each(function (i) {
                    loControles[i] = {
                        cDescControles: $("#ctrl" + (i + 1)).find("td").html()
                    };
                });

                $.fn.Conexion({
                    direccion: '/RiesgoOperacional/ActualizarRiesgoOperacional',
                    datos: {
                        pnNroRiesgo: gModelo.oRiesgoOperacional.nNroRiesgo, __psRiesgoIdent: lsRiesgoDesc, psAgeCod: lsAgencia, psFechaDetec: lsFechaDeteccion,
                        psAreaCod: lsAreaAfectada, psCausasRiesgo: lsCausas, psProceso: lsProceso, pnSubProceso: lsSubProceso,
                        psControlProceso: lsControlProceso, pbEfectividadCtrl: lsEfectidadControl, __psObservacion: lsObservacion,
                        psControles: JSON.stringify(loControles)
                    },
                    bloqueo: true,
                    mensaje: 'Emitiendo respuesta de actualización del riesgo operacional.',
                    terminado: function (data) {
                        if (data.Exito > 0) {
                            $.fn.MensajeProcesos({
                                //clase: 'green',
                                posicion: 'A',
                                mensaje: '!Éxito!, se realizó correctamente la actualización del riesgo operacional.',
                                titulo: 'Actualización del Riesgo Operacional',
                                tipo_mensaje: 'exito',
                                onhidden: function () {
                                    $.fn.Conexion({
                                        direccion: '/RiesgoOperacional/DevolverVista',
                                        datos: { Vista: "ListaGestion", Controlador: "RiesgoOperacional" },
                                        terminado: function (url) {
                                            window.location.href = url;
                                        },
                                    });
                                }
                            });
                        }
                    },
                    error: function (error, status) {
                    }
                });
            }
        }
    });
});

// #region Validaciones
$("#inFechaDeteccion").on({
    "keyup": function (event) {
        $(event.target).val(function (index, value) {
            if (moment(ValidarFormatoFecha(value)).isValid()) {
                if (moment(moment(ValidarFormatoFecha(value)).format("L")).diff(moment().format("MM-DD-YYYY"), "days") > 0) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'La Fecha de Detección no debe ser mayor la fecha actual',
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

function AgregarControl() {
    var tbl = $("#tbl-controles-riesgo");
    bootbox.prompt({
        title: "<strong>Descripción del Control</strong>",
        inputType: 'textarea',
        closeButton: false,
        buttons: {
            confirm: {
                label: 'Añadir',
                className: 'btn-primary'
            },
            cancel: {
                label: 'Cancelar',
                className: 'btn-default'
            }
        },
        callback: function (definicion) {
            if (definicion != null) {
                let filaControl = tbl.find("tbody tr").length;
                //tbl.find("tbody").prepend("<tr id='ctrl" + (filaControl + 1) + "'><td class='text-left text-primary'>" + definicion + "</td><td class='text-center text-primary'><a href='javascript:EliminarControl(" + (filaControl + 1) + ")' class='btn btn-primary'>eliminar</a></td></tr>");
                tbl.find("tbody").prepend("<tr id='ctrl" + (filaControl + 1) + "'><td class='text-left text-primary'>" + definicion + "</td><td class='text-center text-primary'><div class='btn-group'><button type='button' class='btn btn-default btn-sm dropdown-toggle' data-toggle='dropdown' aria-haspopup='true' aria-expanded='true'><i class='entypo-dot-3'></i> <span class='sr-only'>Toggle Dropdown</span></button><ul class='dropdown-menu dropdown-default pull-right'><li><a href='javascript:EliminarControl(@nItemControl)'><i class='entypo-trash'></i>Eliminar</a></li></ul></div></td></tr>");
            }
        }
    });
}
function EliminarControl(fila) {
    $("#ctrl" + fila).remove();
}


function ValidarCampos() {
    var validacion = true;

    /*Validacion Regex*/
    var expreg = /^[A-Z]{1,2}\s\d{4}\s([B-D]|[F-H]|[J-N]|[P-T]|[V-Z]){3}$/;
    if (expreg.test($("#inRiesgoIdentificado").val())) {
        $.fn.MensajeProcesos({
            posicion: 'A',
            mensaje: 'Verifique el texto ingresado',
            titulo: 'Riesgo Identificado',
            tipo_mensaje: 'advertencia'
        });
        return false;
    }


    validacion = $.fn.ValidarInput({ html: "#inRiesgoIdentificado" });
    validacion = $.fn.ValidarInput({ html: "#selAgencias", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selAreas", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selCausaRiesgo", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selProcesos", isSelect: true });
    //validacion = $.fn.ValidarInput({ html: "#selControlProcesos", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selSubProcesos", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#inFechaDeteccion" });

    return validacion;
}

// #endregion 

