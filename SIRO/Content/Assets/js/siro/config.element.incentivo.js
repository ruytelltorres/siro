
function MostrarConfiguracionIncentivos() {
    $.fn.Conexion({
        direccion: '/Configuracion/MostrarConfigIncentivo',
        //datos: { psAreaCod: lsCodArea, psNombreControl: '', psCodControl: lsCodControl, pnAccion: 3 },
        //bloqueo: true,
        terminado: function (data) {
            datos = JSON.parse(data);
            $("#content-tbl-config-incentivos").Tabla({
                tblId: "tbl-config-incentivos",
                cabecera: "<strong>Código</strong>,<strong>Nivel Riesgo</strong>,nProbabilidad,<strong>Probabilidad</strong>,nImpacto,<strong>Impacto</strong>,<strong>Monto Incentivo</strong>,<strong>Fecha Reg.</strong>",
                campos: "nCodConfigIncentivo,oNivelRiesgoInherente.cConsDescripcion,oProbabilidadInherente.nConsValor,oProbabilidadInherente.cConsDescripcion,oImpactoInherente.nConsValor,oImpactoInherente.cConsDescripcion,nMontoIncentivo,dFechaGestion",
                //scrollVertical: "Si",
                datos: datos.LstConfigIncentivos,
                //cantRegVertical: 5,
                classtbl: "table table-responsive responsive",
                alineado: "C,C,C,C,C,C,C,C",
                formato: "0,0,0,0,0,0,2,3",
                controles: "0,0,0,0,0,0,0",
                visible: "1,1,0,1,0,1,1,1",
                anchocolumna: "10,15,0,15,0,15,10,15",
                sindata: "No se encontrol información para la configuración del incentivo",
                numerado: "Si",
                ajustar: "No",
                paginacion: "Si",
                paginacion: { filas: 5, pagina: 1, nombre: 'tabla-configuracion-incentivo' },
                opciones: [
                    {
                        Columna: "Tipo1", id: "id-ingresar-monto-icentivo", claseIcono: "fa fa-money", clase: "IngresarMontoIcentivo", titulo: "Ingresar Monto", function: function (e) {
                            var lnProbailidad = $(this).parents("tr").find("td").eq(3).html();
                            var lnImpacto = $(this).parents("tr").find("td").eq(5).html();

                            bootbox.prompt({
                                title: "Monto del Incentivo",
                                inputType: 'number',
                                buttons: {
                                    confirm: {
                                        label: 'Procesar',
                                        className: 'btn-primary'
                                    },
                                    cancel: {
                                        label: 'Cancelar',
                                        className: 'btn-default'
                                    }
                                },
                                callback: function (monto_incentivo) {
                                    if (parseFloat(monto_incentivo) > 0.0) {
                                        $.fn.Conexion({
                                            direccion: '/Configuracion/GrabaConfigIncentivo',
                                            datos: { pnProbabilidad: lnProbailidad, pnImpacto: lnImpacto, psMontoInc: parseFloat(monto_incentivo) },
                                            //bloqueo: true,
                                            terminado: function (data) {
                                                if (data.Mensaje != "") {
                                                    $.fn.MensajeProcesos({
                                                        clase: 'green',
                                                        posicion: 'A',
                                                        mensaje: 'Configuración registrada',
                                                        titulo: 'Configuración del Incentivo',
                                                        tipo_mensaje: 'exito'
                                                    });
                                                    MostrarConfiguracionIncentivos();
                                                } else {
                                                    $.fn.MensajeProcesos({
                                                        clase: 'green',
                                                        posicion: 'A',
                                                        mensaje: data.Mensaje,
                                                        titulo: 'Configuración del Incentivo',
                                                        tipo_mensaje: 'informacion'
                                                    });
                                                }
                                            },
                                            //bloqueo: false
                                        });
                                    } else {
                                        $.fn.MensajeProcesos({
                                            clase: 'green',
                                            posicion: 'A',
                                            mensaje: 'El valor del incentivo debe ser mayo a 0',
                                            titulo: 'Configuración del Incentivo',
                                            tipo_mensaje: 'informacion'
                                        });
                                    }
                                }
                            });

                            //bootbox.confirm({
                            //    message: "<strong>¿Está seguro de eliminar el control?</strong>",
                            //    classextra: true,
                            //    buttons: {
                            //        confirm: {
                            //            label: 'SI',
                            //            className: 'btn-primary'
                            //        },
                            //        cancel: {
                            //            label: 'NO',
                            //            className: 'btn-default'
                            //        }
                            //    },
                            //    callback: function (result) {
                            //        if (result) {
                            //            $.fn.Conexion({
                            //                direccion: '/Configuracion/RegistrarGestionControlesProceso',
                            //                datos: { psAreaCod: lsCodArea, psNombreControl: '', psCodControl: lsCodControl, pnAccion: 3 },
                            //                //bloqueo: true,
                            //                terminado: function (data) {
                            //                    datos = JSON.parse(data);
                            //                    MostrarDatosControles(datos);
                            //                    $.fn.MensajeProcesos({
                            //                        clase: 'green',
                            //                        posicion: 'A',
                            //                        mensaje: 'Se eliminó correctamente el control.',
                            //                        titulo: 'Control del Área ' + lsCodControl,
                            //                        tipo_mensaje: 'informacion'
                            //                    });
                            //                    $("#btnCancelarRegControlProceso").click();
                            //                },
                            //                //bloqueo: false
                            //            });
                            //        }
                            //    }
                            //});

                        }
                    },
                    //{ Columna: "Tipo2", id: "idEliminarCausaRiesgo", claseIcono: "entypo-trash", clase: "eliminarCausaRiesgo", titulo: "Eliminar", function: function (e) { } },
                ],
            });

        },
        ////bloqueo: false
    });
}

//$(document).on("click", ".IngresarMontoIcentivo", function (e) {
//    var lnProbailidad = $(this).parents("tr").find("td").eq(3).html();
//    var lnImpacto = $(this).parents("tr").find("td").eq(5).html();

//    //if (eval(lnProbailidad) = 0 | eval(lnImpacto) == 0) {
//    //    $.fn.MensajeProcesos({
//    //        clase: 'green',
//    //        posicion: 'A',
//    //        mensaje: 'Es necesario seleccionar ',
//    //        titulo: 'Configuración del Incentivo',
//    //        tipo_mensaje: 'informacion'
//    //    });


//    //    return;
//    //}

//    bootbox.prompt({
//        title: "Monto del Incentivo",
//        inputType: 'number',
//        buttons: {
//            confirm: {
//                label: 'Procesar',
//                className: 'btn-primary'
//            },
//            cancel: {
//                label: 'Cancelar',
//                className: 'btn-default'
//            }
//        },
//        callback: function (monto_incentivo) {
//            if (parseFloat(monto_incentivo) > 0.0) {
//                debugger;


//                $.fn.Conexion({
//                    direccion: '/Configuracion/GrabaConfigIncentivo',
//                    datos: { pnProbabilidad: lnProbailidad, pnImpacto: lnImpacto, psMontoInc: parseFloat(monto_incentivo) },
//                    //bloqueo: true,
//                    terminado: function (data) {
//                        if (data.Mensaje != "") {
//                            $.fn.MensajeProcesos({
//                                clase: 'green',
//                                posicion: 'A',
//                                mensaje: 'Configuración registrada',
//                                titulo: 'Configuración del Incentivo',
//                                tipo_mensaje: 'exito'
//                            });
//                            MostrarConfiguracionIncentivos();
//                        } else {
//                            $.fn.MensajeProcesos({
//                                clase: 'green',
//                                posicion: 'A',
//                                mensaje: data.Mensaje,
//                                titulo: 'Configuración del Incentivo',
//                                tipo_mensaje: 'informacion'
//                            });
//                        }
//                    },
//                    //bloqueo: false
//                });
//            } else {
//                $.fn.MensajeProcesos({
//                    clase: 'green',
//                    posicion: 'A',
//                    mensaje: 'El valor del incentivo debe ser mayo a 0',
//                    titulo: 'Configuración del Incentivo',
//                    tipo_mensaje: 'informacion'
//                });
//            }
//        }
//    });

//    //bootbox.confirm({
//    //    message: "<strong>¿Está seguro de eliminar el control?</strong>",
//    //    classextra: true,
//    //    buttons: {
//    //        confirm: {
//    //            label: 'SI',
//    //            className: 'btn-primary'
//    //        },
//    //        cancel: {
//    //            label: 'NO',
//    //            className: 'btn-default'
//    //        }
//    //    },
//    //    callback: function (result) {
//    //        if (result) {
//    //            $.fn.Conexion({
//    //                direccion: '/Configuracion/RegistrarGestionControlesProceso',
//    //                datos: { psAreaCod: lsCodArea, psNombreControl: '', psCodControl: lsCodControl, pnAccion: 3 },
//    //                //bloqueo: true,
//    //                terminado: function (data) {
//    //                    datos = JSON.parse(data);
//    //                    MostrarDatosControles(datos);
//    //                    $.fn.MensajeProcesos({
//    //                        clase: 'green',
//    //                        posicion: 'A',
//    //                        mensaje: 'Se eliminó correctamente el control.',
//    //                        titulo: 'Control del Área ' + lsCodControl,
//    //                        tipo_mensaje: 'informacion'
//    //                    });
//    //                    $("#btnCancelarRegControlProceso").click();
//    //                },
//    //                //bloqueo: false
//    //            });
//    //        }
//    //    }
//    //});


//});