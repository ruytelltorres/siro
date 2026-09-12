$(document).ready(function () {


});

//$("#btnEliminarRiesgo").click(function (e) {
//    e.preventDefault();
//    bootbox.confirm({
//        message: "¿Está seguro de eliminar el riesgo <strong>" + gRiesgo.cCodRiesgo + "</strong>?",
//        closeButton: false,
//        size: "sm",
//        buttons: {
//            confirm: {
//                label: 'Si',
//                className: 'btn-primary'
//            },
//            cancel: {
//                label: 'No',
//                className: 'btn-default'
//            }
//        },
//        callback: function (result) {
//            if (result) {
//                /************** Aplicamos las validaciones *****************
//                 Condiciones de eliminacion:
//                    1. El riesgo solo debe estar en estado registrado -> (0)
//                 **********************************************************/

//                $.fn.Conexion({
//                    direccion: '/RiesgoOperacional/EliminarRiesgo',
//                    datos: { pnNroRiesgo: gRiesgo.nNroRiesgo },
//                    terminado: function (data) {
//                        datos = JSON.parse(data);
//                        if (datos > 0) {
//                            $.fn.MensajeProcesos({
//                                clase: 'green',
//                                posicion: 'A',
//                                mensaje: 'El riesgo operacional fue eliminado',
//                                titulo: 'Riesgo Operacional :' + gRiesgo.cCodRiesgo,
//                                tipo_mensaje: 'informacion',
//                                onhidden: function () {
//                                    $.fn.Conexion({
//                                        direccion: '/RiesgoOperacional/DevolverVista',
//                                        datos: { Vista: "ListaGestion", Controlador: "RiesgoOperacional" },
//                                        terminado: function (url) {
//                                            window.location = url;
//                                        },
//                                    });
//                                }
//                            });
//                        }
//                    },
//                });
//            }
//        }
//    });

//});

$("#btnReportePlanAccion").click(function (e) {
    e.preventDefault();
    var param = { riesgo: base64_encode(gRiesgo.nNroRiesgo)};
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVistaParam',
        datos: { Vista: "ReportePlanesAsignadosRiesgo", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(param) },
        bloqueo: true,
        mensaje: 'Por favor espere',
        terminado: function (data) {
            window.location = data.url;
        },
    });
});

$("#btnEditarRiesgo").click(function (e) {
    e.preventDefault();
    bootbox.confirm({
        message: "<strong>¿Está seguro de editar el riesgo " + gRiesgo.cCodRiesgo + "?</strong>",
        closeButton: false,
        size: "sm",
        buttons: {
            confirm: {
                label: 'Si',
                className: 'btn-primary'
            },
            cancel: {
                label: 'No',
                className: 'btn-default'
            }
        },
        callback: function (result) {
            if (result) {
                var ObjParam = { riesgo: base64_encode(gRiesgo.nNroRiesgo) };
                $.fn.Conexion({
                    direccion: '/RiesgoOperacional/DevolverVistaParam',
                    datos: { Vista: "Modificar", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
                    bloqueo: true,
                    mensaje: 'Por favor espere',
                    terminado: function (data) {
                        window.location = data.url;
                    },
                });
            }
        }
    });
});


$("#btnRecharRiesgo").click(function (e) {
    e.preventDefault();
    bootbox.prompt({
        title: "<strong>Motivo del rechazo</strong>",
        buttons: {
            cancel: {
                //label: '<i class="fa fa-times"></i> Cancelar',
                label: 'Cancelar',
                className: 'btn-default'
            },
            confirm: {
                //label: '<i class="fa fa-check"></i> Rechazar',
                label: 'Rechazar',
                className: 'btn-primary'
            }
        },
        inputType: 'textarea',
        closeButton: false,
        callback: function (result) {
            if (result != null) {
                $MotivoRechazo = result.trim();
                $.fn.Conexion({
                    direccion: '/RiesgoOperacional/RechazarRiesgo',
                    datos: { pnNroRiesgo: gRiesgo.nNroRiesgo, psMotivoRechazoRiesgo: $MotivoRechazo },
                    bloqueo: true,
                    mensaje: 'Informando el motivo del rechazo del riesgo',
                    terminado: function (data) {
                        if (data.Exito > 0) {
                            $.fn.MensajeProcesos({
                                //clase: 'green',
                                posicion: 'A',
                                mensaje: 'Se registro el rechazo del riesgo correctamente.',
                                titulo: 'Riesgo Operacional: ' + gRiesgo.cCodRiesgo,
                                tipo_mensaje: 'informacion',
                                onhidden: function () {
                                    $.fn.Conexion({
                                        direccion: '/RiesgoOperacional/DevolverVista',
                                        datos: { Vista: "ListaGestion", Controlador: "RiesgoOperacional" },
                                        terminado: function (url) {
                                            window.location = url;
                                        },
                                    });
                                }
                            });
                        }
                    },

                });
            }
        }
    });
});

$("#btnModificarRiesgo").click(function (e) {
    e.preventDefault();
    bootbox.prompt({
        title: "<strong>Observaciones del Riesgo</strong>",
        buttons: {
            cancel: {
                label: 'Cancelar',
                className: 'btn-default'
            },
            confirm: {
                label: 'Notificar',
                className: 'btn-primary'
            }
        },
        inputType: 'textarea',
        closeButton: false,
        callback: function (result) {
            if (result != null) {
                var loObservaciones = [];
                loObservaciones[0] = { nProceso: 0, cObservacion: result.trim() };
                $.fn.Conexion({
                    direccion: '/RiesgoOperacional/GrabarObservacionesGestion',
                    datos: { pnNroRiesgo: gRiesgo.nNroRiesgo, poObservaciones: JSON.stringify(loObservaciones) },
                    bloqueo: true,
                    mensaje: 'Emitiendo la solicitud de modificación',
                    terminado: function (datos) {
                        $.fn.MensajeProcesos({
                            posicion: 'A',
                            mensaje: datos.Mensaje,
                            titulo: 'Modificar - Riesgo Operacional',
                            tipo_mensaje: datos.ExitoGestion > 0 ? 'exito' : 'informacion',
                            onhidden: function () {
                                $.fn.Conexion({
                                    direccion: '/RiesgoOperacional/DevolverVista',
                                    datos: { Vista: "ListaGestion", Controlador: "RiesgoOperacional" },
                                    terminado: function (url) {
                                        window.location = url;
                                    },
                                });
                            }
                        });
                    },
                });
            }
        }
    });
});