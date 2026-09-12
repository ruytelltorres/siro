window.onload = function () {
    $('#wizardConfig').bootstrapWizard({
        'tabClass': 'nav tabs-vertical',
        'onTabShow': function (tab, navigation, index) {
            MostrarDatosTab(index);
        }
    });
}
function MostrarDatosTab(tab) {
    var selAreasProcesos = $("#selAreasProcesos");
    var selAreasControl = $("#selAreasControl");
    var selProbabilidad = $("#selProbabilidad");
    var selImpacto = $("#selImpacto");

    $.fn.Conexion({
        direccion: '/Configuracion/MostrarModelosVista',
        datos: { "pnTab": tab },
        terminado: function (data) {
            datos = JSON.parse(data);
            if (tab == 0) {
                selAreasProcesos.selselect2({ dataShow: "cAreaDescripcion", dataValue: "cAreaCod", datalist: datos.LstAreas, dataselect: goUsuario.oAreas.cAreaCod });
                MostarDatosProcesoAreas(datos);
            } else if (tab == 1) {
                selAreasControl.selselect2({ dataShow: "cAreaDescripcion", dataValue: "cAreaCod", datalist: datos.LstAreas, /*dataselect: goUsuario.oAreas.cAreaCod*/ });
                selAreasControl.val(goUsuario.oAreas.cAreaCod).trigger("change");
                MostrarDatosControles(null);
            } else if (tab == 2) {
                MostrarCausasRiesgo(datos);
            } else if (tab == 3) {
                selProbabilidad.selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: datos.LstProbabilidad });
                selImpacto.selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: datos.LstImpacto });
                DatosMontoPerdida(null);
                //selProbabilidad.change();
            } else if (tab == 4) {
                //debugger;
                MostrarDatosCriteriosEval(datos.LstCriteriosEval);
            } else if (tab == 5) {
                //$("#selConfigIncenProbabilidad").selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: datos.LstProbabilidad, dataselect: 1 });
                //$("#selConfigIncenImpacto").selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: datos.LstImpacto, dataselect: 1 });

                MostrarConfiguracionIncentivos();
            }

        },
    });
}



//#region Controles de Areas 
$("#btnAgregarControl").click(function (e) {
    e.preventDefault();
    if (!$.fn.ValidarInput({ html: "#selAreasControl", isSelect: true })) {
        $("label[for='selAreasControl']").addClass("validar");
        //$("#selAreasControl").addClass("validar");
        return;
    } else if (!$.fn.ValidarInput({ html: "#selProcesosControl", isSelect: true })) {
        $("label[for='selProcesosControl']").addClass("validar");
        //$("#selProcesosControl").addClass("validar");
        return;
    } else {
        $("label[for='selAreasControl']").removeClass("validar");
        $("label[for='selProcesosControl']").removeClass("validar");

        bootbox.prompt({
            //size: "small",
            title: "<strong>Control del Área</strong>",
            placeholder: "Nuevo control para el proceso " + $('select[id="selProcesosControl"] option:selected').text(),
            closeButton: false,
            //backdrop: true,
            buttons: {
                confirm: {
                    label: 'Guardar',
                    className: 'btn-primary'
                },
                cancel: {
                    label: 'Cerrar',
                    className: 'btn-default'
                }
            },
            callback: function (result) {
                if (result != null) {
                    //if (!ValidarCamposControlesAreas()) { return; }
                    var lsCodProceso = $("#selProcesosControl").val();
                    var lsControlDesc = result.trim();

                    $.fn.Conexion({
                        direccion: '/Configuracion/RegistrarGestionControlesProceso',
                        datos: { psCodProceso: lsCodProceso, psControlDesc: lsControlDesc, pnCodControl: 0, pnAccion: 1 },
                        ////bloqueo: true,
                        terminado: function (data) {
                            datos = JSON.parse(data);
                            MostrarDatosControles(datos.LstControlProceso);
                            $.fn.MensajeProcesos({
                                clase: 'green',
                                posicion: 'A',
                                mensaje: 'Datos control registrado correctamente.',
                                titulo: 'Control del Área',
                                tipo_mensaje: 'exito'
                            });
                        },
                    });
                }
            }
        });
    }
});

function MostrarDatosControles(datos) {
    var $opciones = [
        {
            Columna: "Tipo1", id: "id-editar-control", claseIcono: "entypo-pencil", clase: "editarControl", titulo: "Editar", function: function (e) {
                var lsNombreControl = $(e).parents("tr").find("td").eq(3).html();

                bootbox.prompt({
                    //size: "small",
                    title: "<strong>Actualizar Control del Área</strong>",
                    closeButton: false,
                    //backdrop: true,
                    buttons: {
                        confirm: {
                            label: 'Guardar',
                            className: 'btn-primary'
                        },
                        cancel: {
                            label: 'Cerrar',
                            className: 'btn-default'
                        }
                    },
                    value: lsNombreControl,
                    callback: function (result) {
                        if (result != null) {

                            var lsCodProceso = $("#selProcesosControl").val();
                            var lsDescControl = result.trim();
                            var lnCodControl = $("#tbl-controles-area-gestion tr.seleccionado").find("td").eq(1).html();

                            $.fn.Conexion({
                                direccion: '/Configuracion/RegistrarGestionControlesProceso',
                                datos: { psCodProceso: lsCodProceso, psControlDesc: lsDescControl, pnCodControl: lnCodControl, pnAccion: 2 },

                                terminado: function (data) {
                                    datos = JSON.parse(data);
                                    MostrarDatosControles(datos.LstControlProceso);
                                    $.fn.MensajeProcesos({
                                        clase: 'green',
                                        posicion: 'A',
                                        mensaje: 'Se actualizó correctamente el control para el proceso ' + $('select[id="selProcesosControl"] option:selected').text(),
                                        titulo: 'Control del Área',
                                        tipo_mensaje: 'informacion'
                                    });

                                },

                            });
                        }
                    }
                });
            }
        },
        {
            Columna: "Tipo2", id: "id-eliminar-control", claseIcono: "entypo-trash", clase: "eliminarControl", titulo: "Eliminar", function: function (e) {
                var lsCodProceso = $("#selProcesosControl").val();
                var lnCodControl = $(e).parents("tr").find("td").eq(1).html();

                bootbox.confirm({
                    message: "<strong>¿Está seguro de eliminar el control?</strong>",
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
                            $.fn.Conexion({
                                direccion: '/Configuracion/RegistrarGestionControlesProceso',
                                datos: { psCodProceso: lsCodProceso, psControlDesc: '', pnCodControl: lnCodControl, pnAccion: 3 },
                                ////bloqueo: true,
                                terminado: function (data) {
                                    datos = JSON.parse(data);
                                    MostrarDatosControles(datos.LstControlProceso);
                                    $.fn.MensajeProcesos({
                                        clase: 'green',
                                        posicion: 'A',
                                        mensaje: 'Se eliminó correctamente el control.',
                                        titulo: 'Control del Área ' + lsCodControl,
                                        tipo_mensaje: 'informacion'
                                    });
                                    $("#btnCancelarRegControlProceso").click();
                                },
                                ////bloqueo: false
                            });
                        }
                    }
                });
            }
        }
    ];
    $("#content-tbl-controles-area-gestion").Tabla({
        tblId: "tbl-controles-area-gestion",
        cabecera: "<strong>Cód Control</strong>,<strong>Proceso</strong>,<strong>Nombre del Control</strong>,<strong>Fecha Reg</strong>,<strong>Estado</strong>",
        campos: "nCodControl,oProceso.cDescProceso,cControlDescripcion,dFechaRegistro,bEstado?'Activo':'Inactivo'",
        datos: datos,
        classtbl: "table table-responsive responsive",
        //cantRegVertical: 5,
        //scrollVertical: "Si",
        alineado: "C,C,L,C,C",
        formato: "0,0,0,3,0",
        controles: "0,0,0,0,0",
        visible: "1,1,1,1,1",
        anchocolumna: "10,15,30,10,10",
        numerado: "Si",
        paginacion: { filas: 5, pagina: 1, nombre: 'tabla-controles-areas' },
        opciones: $opciones
    });


}

//$(document).on("click", ".editarControl", function (e) {

//});

//$(document).on("click", ".eliminarControl", function (e) {


//});

//$("#btnRegControlProceso").click(function (e) {
//    e.preventDefault();

//    if (!ValidarCamposControlesAreas()) { return; }

//    var lsCodProceso = $("#selProcesosControl").val();
//    var lsControlDesc = $("#inNombreControl").val();

//    $.fn.Conexion({
//        direccion: '/Configuracion/RegistrarGestionControlesProceso',
//        datos: { psCodProceso: lsCodProceso, psControlDesc: lsControlDesc.trim(), pnCodControl: 0, pnAccion: 1 },
//        ////bloqueo: true,
//        terminado: function (data) {
//            datos = JSON.parse(data);
//            MostrarDatosControles(datos.LstControlProceso);
//            $.fn.MensajeProcesos({
//                clase: 'green',
//                posicion: 'A',
//                mensaje: 'Datos control registrado correctamente.',
//                titulo: 'Control del Área',
//                tipo_mensaje: 'exito'
//            });
//            $("#btnCancelarRegControlProceso").click();

//        },
//        ////bloqueo: false
//    });


//});

//$("#btnActControlProceso").click(function (e) {
//    e.preventDefault();
//    if (!ValidarCamposControlesAreas()) { return; }

//    var lsCodProceso = $("#selProcesosControl").val();
//    var lsDescControl = $("#inNombreControl").val();
//    var lnCodControl = $("#tbl-controles-area-gestion tr.seleccionado").find("td").eq(1).html();
//    debugger;
//    $.fn.Conexion({
//        direccion: '/Configuracion/RegistrarGestionControlesProceso',
//        datos: { psCodProceso: lsCodProceso, psControlDesc: lsDescControl.trim(), pnCodControl: lnCodControl, pnAccion: 2 },
//        ////bloqueo: true,
//        terminado: function (data) {
//            datos = JSON.parse(data);
//            MostrarDatosControles(datos.LstControlProceso);
//            $.fn.MensajeProcesos({
//                clase: 'green',
//                posicion: 'A',
//                mensaje: 'Datos control registrado correctamente.',
//                titulo: 'Control del Área ' + lnCodControl,
//                tipo_mensaje: 'informacion'
//            });
//            $("#btnCancelarRegControlProceso").click();
//        },
//        ////bloqueo: false
//    });
//});

//$("#btnCancelarRegControlProceso").click(function (e) {
//    e.preventDefault();
//    $("#inNombreControl").val("");
//    jQuery('#modal-registro-controles-areas').modal('hide');
//});

$("#selAreasControl").change(function () {
    var lsCodArea = $(this).val();
    $.fn.Conexion({
        direccion: '/Configuracion/ObtenerProcesoAreas',
        datos: { psCodArea: lsCodArea },
        //bloqueo: true,
        terminado: function (data) {
            datos = JSON.parse(data);
            $("#selProcesosControl").selselect2({ dataShow: "cDescProceso", dataValue: "cCodProceso", datalist: datos.LstProcesosAreas });
            //MostrarDatosControles(datos);
        },
        //bloqueo: false
    });
});

$("#selProcesosControl").change(function () {
    var lsCodProceso = $(this).val();
    $.fn.Conexion({
        direccion: '/Configuracion/MostrarControlProceso',
        datos: { psCodProceso: lsCodProceso },
        ////bloqueo: true,
        terminado: function (data) {
            datos = JSON.parse(data);
            MostrarDatosControles(datos.LstControlProceso);
        },
        ////bloqueo: false
    });
});


//function ValidarCamposControlesAreas() {
//    var validacion = true;

//    if (!$.fn.ValidarInput({ html: "#inNombreControl" })) {
//        $("#inNombreControl").parent(this).addClass("has-error");
//        validacion = false;
//    } else {
//        $("#inNombreControl").parent(this).removeClass("has-error");
//    }

//    return validacion;
//}

//#endregion 











