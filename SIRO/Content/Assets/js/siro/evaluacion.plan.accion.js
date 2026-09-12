
$(document).ready(function () {
    /*Cargas iniciales*/

    //var $PlanAccion = $("#inPlanAccion");
    //var $EstadoPlan = $("#h3_EstadoActual");
    //var $IconoEstadoPlan = $("#cIconoEstado");



    //$PlanAccion.html(gDetPlanAccion.cPlanDescripcion);
    //$EstadoPlan.html(gDetPlanAccion.cEstado);
    //$IconoEstadoPlan.removeClass().addClass(gDetPlanAccion.cIconoEstado);
    MostrarListaResponsablePlanAccion();
    //ListaFechasImplementacion();

    gARCHIVOS = $.fn.CargarArchivos({ cDom: "#content-doc-adjunto", btn: "#btnAdjuntar", nCantidad: 1 });

    $("#inFechaImplementacion").on('keyup', function (e) {
        var lsFechaImp = $(this).val();
        $("#btnAddFechaImplement").prop("disabled", !(ValidarFecha(lsFechaImp)));
    });
    //$("#selAreaRespon").selselect2({ dataShow: "cAreaDescripcion", dataValue: "cAreaCod", datalist: gmodel.oLstAreas });
});

$("#selAreaRespon").change(function () {
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ListarUsuarioArea',
        datos: { psAreaCod: $(this).val() },
        terminado: function (data) {
            datos = JSON.parse(data);
            $("#selUserResponsablePlan").selselect2({ dataShow: "cUsuario", dataValue: "cUser", datalist: datos.oLstUsuarios });
        },
    });
});




function MostrarListaResponsablePlanAccion() {
    $("#content-tbl-responsable-plan-accion").Tabla({
        tblId: "tbl-responsable-plan-accion",
        cabecera: "nNroRiesgo,nPlanCod,nItemRespon,<strong>Usuario</strong>,<strong>Nombre</strong>",
        campos: "oDatosRiesgo.nNroRiesgo,oPlanAccion.nPlanCod,nItemResponPlan,cUserResponsable,oPersona.cPersNombre",
        datos: gmodel.oLstReponPlanAccion,
        cantRegVertical: 4,
        classtbl: "table table-responsive",
        alineado: "C,C,C,C,L",
        formato: "0,0,0,0,0",
        controles: "0,0,0,0,0",
        visible: "0,0,0,1,1",
        anchocolumna: '0,0,0,15,60',
        numerado: "Si",
        ajustar: 'No',
        opciones: [
            {
                Columna: "Tipo1", id: "idQuitarResponPlanAccion", claseIcono: "entypo-cancel-squared", clase: "QuitarResponPlanAccion", titulo: "Quitar", function: function (e) {
                    var lnNroRiesgo = $(e).parents("tr").find("td").eq(1).html();
                    var lnCodPlan = $(e).parents("tr").find("td").eq(2).html();
                    var lnItem = $(e).parents("tr").find("td").eq(3).html();

                    bootbox.confirm({
                        message: "<strong>¿Está seguro de quitar al resposable del plan de acción? </strong>",
                        //classextra: true,
                        closeButton: false,
                        size: "sm",
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
                                $.fn.Conexion({
                                    direccion: '/RiesgoOperacional/QuitarResponsablePlanAccion',
                                    datos: { pnNroRiesgo: lnNroRiesgo, pnPlanCod: lnCodPlan, pnItemResponsable: lnItem },
                                    //bloqueo: true,
                                    terminado: function (data) {
                                        LstResponsablePlan = JSON.parse(data);
                                        MostrarResponsablePlanesAccion(LstResponsablePlan)
                                        MostrarReponsablePlanAccion(LstResponsablePlan);
                                        $.fn.MensajeProcesos({
                                            clase: 'green',
                                            posicion: 'A',
                                            mensaje: 'Se quitó al responsable del plan de acción correctamente.',
                                            titulo: 'Responsable Plan Acción',
                                            tipo_mensaje: 'informacion'
                                        });

                                    },
                                    //bloqueo: false
                                });
                            } /*else { }*/
                        }
                    });
                }
            },
        ],
    });
}

function ListaFechasImplementacion() {
    for (var i in gmodel.lstObservacionesGestion) {
        var classes = ['', 'label label-success', 'label label-warning', 'label label-danger', ''],
            _class = classes[eval(gmodel.lstObservacionesGestion[i].ValorEstado)],
            $fecha_implementacion = $('<li>' + moment(gmodel.lstObservacionesGestion[i].FechaImplementa).format("DD/MM/YYYY") + ' <span class="pull-right ' + _class + '">' + gmodel.lstObservacionesGestion[i].Estado + '</span></li>');
        $fecha_implementacion.appendTo($("#lista_fecha_implemetacion"));
        $fecha_implementacion.hide().slideDown('fast');
    }
}


function MostrarEstadosPlan() {
    $.fn.Conexion({
        direccion: '/Evaluacion/EstadosPlanAccion',
        //datos: { pnCodPlanAccion: lnCodPlanAccion},
        terminado: function (data) {
            datos = JSON.parse(data.EstadosPlanAccion);
            bootbox.prompt({
                title: "Estado del Plan de Acción",
                message: "<p>Seleccione el estado acutal del plan de acción</p>",
                size: "sm",
                closeButton: false,
                buttons: {
                    confirm: {
                        label: 'Confirmar',
                        className: 'btn-primary'
                    },
                    cancel: {
                        label: 'Cancelar',
                        className: 'btn-default'
                    }
                },
                //inputType: 'radio',
                inputType: 'select',
                inputOptions: datos.data,
                value: gmodel.oPlanAccion.nEstado,
                callback: function (result) {
                    if (result != null) {
                        $.fn.Conexion({
                            direccion: '/Evaluacion/ActualizarEstadoPlanAccion',
                            datos: { pnPlanCod: gmodel.oPlanAccion.nPlanCod, pnEstado: result },
                            terminado: function (data) {
                                if (data.Exito > 0) {
                                    $.fn.MensajeProcesos({
                                        posicion: 'A',
                                        mensaje: "Se actualizo el estado del plan de acción",
                                        titulo: "Actualización - Plan Acción",
                                        tipo_mensaje: data.Tipo,
                                        onhidden: function () {

                                            location.reload();
                                        }
                                    });
                                }
                            },
                        });
                    }

                }
            });

        },
    });
}

$('#inFechaImplementacion').keypress(function (e) {
    var keycode = (e.keyCode ? e.keyCode : e.which);
    if (keycode == '13') {
        e.preventDefault();
        $("#btnAddFechaImplement").click();
        return false;
    }
});

$("#btnAddFechaImplement").click(function (e) {
    e.preventDefault();
    var lnCodPlanAccion = gmodel.oPlanAccion.nPlanCod;
    var fecha_implementa = $("#inFechaImplementacion");

    if (fecha_implementa.val().length == 0 || !(ValidarFecha(fecha_implementa.val()))) {
        return false;
    }
    /*Condicionamos el registro de las fecha de implementacion*/
    $.fn.Conexion({
        direccion: '/Evaluacion/AgregarFechaImplementacion',
        datos: { pnCodPlan: lnCodPlanAccion, pdFechaImplementacion: fecha_implementa.val() },
        terminado: function (data) {
            if (data.Tipo === "exito") {
                location.reload();
            } else {
                $.fn.MensajeProcesos({
                    posicion: 'A',
                    mensaje: data.Mensaje,
                    titulo: 'Mensaje del sistema',
                    tipo_mensaje: data.Tipo
                });
            }
        },
    });
});


$("#btnImplementado").click(function (e) {
    e.preventDefault();

    $.fn.Conexion({
        direccion: '/Evaluacion/ActualizarEstadoPlanAccion',
        datos: { pnPlanCod: gmodel.oPlanAccion.nPlanCod, pnEstado: 3 },
        bloqueo: true,
        mensaje: 'Confirmando la implementación del Plan de Acción',
        terminado: function (data) {
            //Inicializador("", 1, false);
            window.reload();
            $.fn.MensajeProcesos({
                posicion: 'A',
                mensaje: data.Mensaje,
                titulo: "Plan Acción",
                tipo_mensaje: data.TpoMensaje
            });
        },
    });
});

function ConfirmarImplementacion() {



}

