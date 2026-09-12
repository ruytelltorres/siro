var lnPlanAccion = 0;
var lnNroRiesgo = 0;
var gnFilasPorPagina = 10;
window.onload = function () {
    Inicializador();
}

async function Inicializador(filtro = "", pagina = 1, bloqueo = true) {
    await ListaPlanesAccion(filtro, pagina, bloqueo);
}


//$('#selAreasAginado').select2({
//    dropdownParent: $('#modal-reasignacion-responsable')
//});

$("#inFechaImplementacion").on({
    "keyup": function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == "13") {
            event.preventDefault();
            $("#btnAddFechaImplement").click();
        }

        $(event.target).val(function (index, value) {
            $("#btnAddFechaImplement").prop("disabled", !(moment(ValidarFormatoFecha(value)).isValid()));
            return value;
        });
    }
});


function ListaPlanesAccion(filtro = "", pagina = 1, bloqueo = true) {
    $.fn.Conexion({
        direccion: "/RiesgoOperacional/ListadoPlanesAccion",
        datos: { busqueda: filtro, pagina: pagina, elementos: gnFilasPorPagina },
        bloqueo: bloqueo,
        mensaje: "Obteniendo los planes de acción.",
        terminado: function (result) {
            $("#content-planes-accion").html(result);
        },
    });
}


$("#inBuscar").keypress(function (e) {
    var keycode = (e.keyCode ? e.keyCode : e.which);
    if (keycode == "13") {
        e.preventDefault();
        ListaPlanesAccion($(this).val());
        return false;
    }
});

$('#selMostrarRegistros').on('change', function () {
    gnFilasPorPagina = parseInt($(this).val(), 10) || 10;
    ListaPlanesAccion($('#inBuscar').val());
});

function DetalleRiesgo(NroRiesgo) {
    var obj = { riesgo: base64_encode(NroRiesgo) };
    $.fn.Conexion({
        direccion: "/RiesgoOperacional/DevolverVistaParam",
        datos: { Vista: "DetalleRiesgo", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(obj) },
        terminado: function (data) {
            window.location = data.url;
        },
    });
}

function ComentariosPlanAccion(nroriesgo, codplan) {
    var obj = { riesgo: base64_encode(nroriesgo), planaccion: codplan };
    $.fn.Conexion({
        direccion: "/RiesgoOperacional/DevolverVistaParam",
        datos: { Vista: "ComentarioPlanAccion", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(obj) },
        //bloqueo: true,
        //mensaje: "Obteniendo comentarios por favor espere...",
        terminado: function (data) {
            window.location = data.url;
        },
    });

}

function ConfirmarAsignacion(cod_plan) {
    $.fn.Conexion({
        direccion: '/Evaluacion/ActualizarEstadoPlanAccion',
        datos: { pnPlanCod: cod_plan, pnEstado: 2 },
        bloqueo: true,
        mensaje: 'Procesando la confirmación del Plan de Acción',
        terminado: function (data) {
            Inicializador("", 1, false);
            $.fn.MensajeProcesos({
                posicion: 'A',
                mensaje: data.Mensaje,
                titulo: "Plan Acción",
                tipo_mensaje: data.TpoMensaje
            });
        },
    });
}

function ConfirmarImplementacion(cod_plan) {
    $.fn.Conexion({
        direccion: '/Evaluacion/ActualizarEstadoPlanAccion',
        datos: { pnPlanCod: cod_plan, pnEstado: 3 },
        bloqueo: true,
        mensaje: 'Confirmando la implementación del Plan de Acción',
        terminado: function (data) {
            Inicializador("", 1, false);
            $.fn.MensajeProcesos({
                posicion: 'A',
                mensaje: data.Mensaje,
                titulo: "Plan Acción",
                tipo_mensaje: data.TpoMensaje
            });
        },
    });
}

function MostrarDetalleRiesgo(NroRiesgo, TipoRiesgo) {
    var ObjParam = null;
    if (TipoRiesgo == 1) {
        ObjParam = { riesgo: base64_encode(NroRiesgo) };
    } else if (TipoRiesgo == 2) {
        ObjParam = { evaluacion: base64_encode(NroRiesgo) };
    }

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVistaParam',
        datos: { Vista: TipoRiesgo == 1 ? "DetalleRiesgo" : "DetalleEvaluacion", Controlador: TipoRiesgo == 1 ? "RiesgoOperacional" : "Evaluacion", Parametros: JSON.stringify(ObjParam) },
        terminado: function (data) {
            window.location = data.url;
        },
    });
}

function HistorialFechaImplementacion(codplan, showmodal = 0) {
    var lbImplementado = false;
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ObtenerFechaImplementacionPlanAccion',
        datos: { pnPlanCod: codplan },
        terminado: function (data) {
            datos = JSON.parse(data);
            document.getElementById("content-fechas").dataset.cod = codplan;
            $("#lista-fecha-implemetacion").empty();
            for (var i in datos.lstObservacionesGestion) {
                if (datos.lstObservacionesGestion[i].ValorEstado == 5) { lbImplementado = true };
                var classes = ['', 'label label-success', 'label label-warning', 'label label-danger', 'label label-default', 'label label-success', ''],
                    _class = classes[eval(datos.lstObservacionesGestion[i].ValorEstado)],
                    $fecha_implementacion = $('<li>' + datos.lstObservacionesGestion[i].FechaImplementa + ' <span class="pull-right ' + _class + '">' + datos.lstObservacionesGestion[i].Estado + '</span></li>');
                $fecha_implementacion.appendTo($("#lista-fecha-implemetacion"));
                $fecha_implementacion.hide().slideDown('fast');
            }
            if (lbImplementado) {
                $("#seccion-insert-fecha").hide();
            } else {
                $("#seccion-insert-fecha").show();
            }
        },
    });
    if (showmodal == 0) {
        $("#modal-fecha-implementa").modal("show").find(".modal-header h4").html("<strong>Fecha de Implementaci&oacute;n</strong>");
    }
}

function MostrarResponsables(responsables) {
    var laResponsable = responsables.split("|");
    bootbox.alert({
        title: "<strong>Usuarios Responsables</strong>",
        message: function () {
            var lsResponsables = "";
            for (var i = 0; i < laResponsable.length; i++) {
                lsResponsables += (i + 1) + ". " + laResponsable[i].trim().replace("[", "<strong>[").replace("]", "]</strong>") + "<br/>";
            }
            return lsResponsables;
        },
        closeButton: false,
        size: "sm"
    });
}

//function MostrarDescCompletaPlanAccion(cod, texto) {
//    bootbox.alert({
//        title: "<strong>Plan de Acción: " + cod + "</strong>",
//        message: texto,
//        closeButton: false,
//        //size: "sm"
//    });
//}

$("#selAreasAsignado").change(function () {
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ListarUsuarioArea',
        datos: { psAreaCod: $(this).val() },
        terminado: function (data) {
            datos = JSON.parse(data);
            $("#selUserAsignado").selselect2({ dataShow: "cUsuario", dataValue: "cUser", datalist: datos.oLstUsuarios });
        },
    });
});

function ReasignarResponsable(riesgo = 0, plan = 0) {
    //$(".modal-header h4").html("<strong>Reasignaci&oacute;n de Responsable - Plan Acción " + plan + "</strong>");
    lnNroRiesgo = riesgo;
    lnPlanAccion = plan;
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/MostarResponsablePlanAccion',
        datos: { pnNroRiesgo: riesgo, pnCodPlanAccion: plan },
        terminado: function (data) {
            datos = JSON.parse(data);
            $("#selResponsable").selselectboxit({ dataShow: "oPersona.cPersNombre", dataValue: "cUserResponsable", datalist: datos.oLstReponPlanAccion });
            $("#modal-reasignacion-responsable").modal("show").find(".modal-header h4").html("<strong>Reasignar Responsable - Plan Acción " + plan + "</strong>");
        },
    });
}

$("#btnReasignar").click(function (e) {
    e.preventDefault();

    var selResponsable = $("#selResponsable");
    var selAreaUserAsignado = $("#selAreasAsignado");
    var selUserAsignado = $("#selUserAsignado");

    if (selResponsable.val() == "") {
        $.fn.MensajeProcesos({
            clase: 'green',
            posicion: 'A',
            mensaje: 'Ud. debe seleccionar al responsable del plan de acción.',
            titulo: 'Reasignación Responsable',
            tipo_mensaje: 'advertencia'
        });
        selResponsable.focus();
        return false;
    } else if (selUserAsignado.val() == "") {
        $.fn.MensajeProcesos({
            clase: 'green',
            posicion: 'A',
            mensaje: 'Ud. debe seleccionar al responsable de la reasignación.',
            titulo: 'Reasignación Responsable',
            tipo_mensaje: 'advertencia'
        });
        selUserAsignado.focus();
        return false;
    }

    bootbox.confirm({
        message: "¿Está ud. seguro de realizar la acción de reasignació?",
        size: "sm",
        closeButton: false,
        buttons: {
            confirm: {
                label: "SI",
                className: "btn-primary"
            },
            cancel: {
                label: "NO",
                className: "btn-default"
            }
        },
        callback: function (result) {
            if (result) {
                $.fn.Conexion({
                    direccion: '/RiesgoOperacional/RegistrarResponsableReasignado',
                    datos: { pnRiesgo: lnNroRiesgo, pnPlanCod: lnPlanAccion, psUserResponsable: selResponsable.val(), psUserReasignado: selUserAsignado.val() },
                    bloqueo: true,
                    mensaje: 'Estamos informando al responsable de la reasignación. Por favor espere...',
                    terminado: function (data) {
                        datos = JSON.parse(data);
                        if (Boolean(datos.ValorNotif) === true) {
                            $.fn.MensajeProcesos({
                                clase: 'green',
                                posicion: 'A',
                                mensaje: datos.MensajeNotif, //'Se realizó la reasignación del responsable del plan de acción',
                                titulo: 'Reasignación Responsable',
                                tipo_mensaje: 'exito'
                            });
                            selResponsable.selectBoxIt("selectOption", null);
                            selAreaUserAsignado.val(null);
                            $("#modal-reasignacion-responsable").modal("hide");
                        } else {
                            $.fn.MensajeProcesos({
                                clase: 'green',
                                posicion: 'A',
                                mensaje: datos.MensajeNotif, //'El responsable ya se encuentra asignado al plan de acción',
                                titulo: 'Asignación de Resposanble',
                                tipo_mensaje: 'advertencia'

                            });
                        }
                    },
                });
            }
        }
    });
});


$("#btnAddUserResponsable").click(function (e) {
    e.preventDefault();
    if (!ValidarCamposResponsablePlanAccion()) { return false; }

    /*Validamos el estado actual del riesgo para añadir los planes de accion*/
    if (gDatosRiesgo.nTpoRiesgo == 1) {
        if (gDatosRiesgo.nProcRiesgo == 5) {
            $.fn.MensajeProcesos({
                //clase: 'green',
                posicion: 'B',
                mensaje: 'Esta intentando realizar una acción que no esta permitido',
                titulo: 'Accion no válida',
                tipo_mensaje: 'error'
            });
            return false;
        }
    } else if (gDatosRiesgo.nTpoRiesgo == 2) {
        if (gDatosRiesgo.nEstadoRiesgo == 5 && gDatosRiesgo.nProcRiesgo == 5) {
            $.fn.MensajeProcesos({
                //clase: 'green',
                posicion: 'B',
                mensaje: 'Esta intentando realizar una acción que no esta permitido',
                titulo: 'Accion no válida',
                tipo_mensaje: 'error'
            });
            return false;
        }

    }


    var lnNroRiesgo = $("#tbl-responsable-plan-accion tr.seleccionado").find("td").eq(1).html();
    var lnCodPlan = $("#tbl-responsable-plan-accion tr.seleccionado").find("td").eq(2).html();
    var lsUserRespon = $("#selUserResponsablePlan").val();

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/RegistrarResponsablePlanAccion',
        datos: { pnNroRiesgo: lnNroRiesgo, pnPlanCod: lnCodPlan, psUserResponsable: lsUserRespon },
        //bloqueo: true,
        terminado: function (data) {
            LstResponsablePlan = JSON.parse(data);
            if (LstResponsablePlan.oLstReponPlanAccionRiesgo != null) {
                MostrarResponsablePlanesAccion(LstResponsablePlan);
                $.fn.MensajeProcesos({
                    clase: 'green',
                    posicion: 'A',
                    mensaje: 'Responsable asignado correctamente al plan de acción ' + lnCodPlan,
                    titulo: 'Asignación de Resposanble',
                    tipo_mensaje: 'exito'

                });
                //$("#selAreaRespon").val(null).trigger("change");
                //$("#selUserResponsablePlan").val(null).trigger("change");
            } else {
                $.fn.MensajeProcesos({
                    clase: 'green',
                    posicion: 'A',
                    mensaje: 'El responsable ya se encuentra asignado al plan de acción ' + lnCodPlan,
                    titulo: 'Asignación de Resposanble',
                    tipo_mensaje: 'advertencia'

                });
            }


            //LimpiarModalAddControlRiesgoResidual();

        },
        //bloqueo: false
    });
});





$("#btnAddFechaImplement").click(function (e) {
    e.preventDefault();
    var lnCodPlanAccion = document.getElementById("content-fechas").dataset.cod;
    var fecha_implementa = $("#inFechaImplementacion");
    var lbImplementado = $("#chkFiltro").is(':checked') ? 1 : 0;

    $.fn.Conexion({
        direccion: '/Evaluacion/AgregarFechaImplementacion',
        datos: { pnCodPlan: lnCodPlanAccion, pdFechaImplementacion: fecha_implementa.val(), pbImplementado: lbImplementado },
        bloqueo: !lbImplementado ? true : false,
        mensaje: 'Espero por favor, mientras se realiza la notificación',
        terminado: function (data) {
            if (data.Tipo === "exito") {
                fecha_implementa.val("");
                HistorialFechaImplementacion(lnCodPlanAccion, 1)
                $.fn.MensajeProcesos({
                    posicion: 'A',
                    mensaje: 'Se registro correctamente la fecha de implementación.',
                    titulo: 'Fecha de Implementación',
                    tipo_mensaje: 'informacion'
                });
            } else {
                $.fn.MensajeProcesos({
                    posicion: 'A',
                    mensaje: data.Mensaje,
                    titulo: 'Fecha Implementación',
                    tipo_mensaje: data.Tipo
                });
            }
        },
    });
});

