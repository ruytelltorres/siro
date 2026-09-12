
//#region Procesos
$("#btnAgregarProceso").click(function (e) {
    e.preventDefault();
    if (!$.fn.ValidarInput({ html: "#selAreasProcesos", isSelect: true })) {
        $("label[for='selAreasProcesos']").addClass("validar");
        return;
    } else {
        $("label[for='selAreasProcesos']").removeClass("validar");

        bootbox.prompt({
            title: "<strong>Proceso del Área</strong>",
            placeholder: "Nuevo proceso para " + $('select[id="selAreasProcesos"] option:selected').text(),
            closeButton: false,
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
                    var lsCodArea = $('#selAreasProcesos').val();
                    var lsNombreProceso = result.trim();

                    $.fn.Conexion({
                        direccion: '/Configuracion/RegistrarGestionProcesoArea',
                        datos: { psAreaCod: lsCodArea, psNombreProceso: lsNombreProceso, pnProceso: 1 },
                        terminado: function (data) {
                            datos = JSON.parse(data);
                            MostarDatosProcesoAreas(datos);
                            $.fn.MensajeProcesos({
                                clase: 'green',
                                posicion: 'A',
                                mensaje: 'Datos del proceso registrados correctamente.',
                                titulo: 'Proceso del Área',
                                tipo_mensaje: 'exito'
                            });
                        },
                    });
                }
            }
        });
    }
});

function MostarDatosProcesoAreas(datos) {
    if (datos.LstProcesosAreas != null) {
        var $opciones = [
            {
                Columna: "Tipo1", id: "id-sub-proceso", claseIcono: "entypo-flow-cascade", clase: "verSubproceso", titulo: "Sub Procesos", function: function (e) {
                    var lsCodProceso = $(e).parents("tr").find("td").eq(1).html();
                    $.fn.Conexion({
                        direccion: '/Configuracion/SubProcesosAreas',
                        datos: { psCodProceso: lsCodProceso },
                        terminado: function (data) {
                            datos = JSON.parse(data);
                            MostrarSubProcesos(datos);
                        },
                    });

                    $("#modal-registro-subprocesos-areas .modal-header h4").html("<strong>Sub Procesos: " + $(e).parents("tr").find("td").eq(2).html() + "</strong>");
                    $("#btnActSubProceso").hide();
                    $("#btnRegSubProceso").hide();
                    $("#btnNuevoSubProceso").show();
                    $("#footer-gestion-subprocesos").slideUp();
                    $("#modal-registro-subprocesos-areas").modal('show');
                }
            },
            {
                Columna: "Tipo2", id: "id-editar-proceso", claseIcono: "entypo-pencil", clase: "editar-proceso", titulo: "Editar", function: function (e) {
                    var lsNombreProceso = $(e).parents("tr").find("td").eq(2).html();

                    bootbox.prompt({
                        title: "<strong>Actualizar Proceso del Área</strong>",
                        closeButton: false,
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
                        value: lsNombreProceso.trim(),
                        callback: function (result) {
                            if (result != null) {
                                var lsCodProceso = $("#tbl-procesos-area-gestion tr.seleccionado").find("td").eq(1).html();
                                var lsNombreProceso = result.trim();
                                $.fn.Conexion({
                                    direccion: '/Configuracion/RegistrarGestionProcesoArea',
                                    datos: { psAreaCod: lsCodProceso, psNombreProceso: lsNombreProceso, pnProceso: 2 },
                                    terminado: function (data) {
                                        datos = JSON.parse(data);
                                        MostarDatosProcesoAreas(datos);
                                        $.fn.MensajeProcesos({
                                            clase: 'green',
                                            posicion: 'A',
                                            mensaje: 'Los datos del proceso fueron actualizados correctamente.',
                                            titulo: 'Proceso del Área',
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
                Columna: "Tipo3", id: "id-eliminar-Proceso", claseIcono: "entypo-trash", clase: "eliminar-proceso", titulo: "Eliminar", function: function (e) {
                    var lsCodProceso = $(this).parents("tr").find("td").eq(1).html();
                    bootbox.confirm({
                        message: "<strong>¿Está seguro de eliminar el proceso?</strong>",
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
                                    direccion: '/Configuracion/RegistrarGestionProcesoArea',
                                    datos: { psAreaCod: lsCodProceso, psNombreProceso: '', pnProceso: 3 },
                                    terminado: function (data) {
                                        datos = JSON.parse(data);
                                        MostarDatosProcesoAreas(datos);
                                        $.fn.MensajeProcesos({
                                            clase: 'green',
                                            posicion: 'A',
                                            mensaje: 'Se eliminó correctamente el proceso.',
                                            titulo: 'Proceso del Área',
                                            tipo_mensaje: 'informacion'
                                        });
                                    },
                                });
                            }
                        }
                    });

                }
            }
        ];

        $("#content-tbl-procesos-area-gestion").Tabla({
            tblId: "tbl-procesos-area-gestion",
            cabecera: "<strong>Código</strong>,<strong>Proceso</strong>,<strong>Fecha Reg</strong>,<strong>Estado</strong>",
            campos: "cCodProceso,cDescProceso,dFechaRegistro,bProcesoEstado ? 'Activo' : 'Inactivo'",
            datos: datos.LstProcesosAreas,
            classtbl: "table table-responsive responsive",
            alineado: "C,L,C,C",
            formato: "0,0,3,0",
            controles: "0,0,0,0",
            visible: "1,1,1,1",
            anchocolumna: "10,50,10,10",
            sindata: "No se encontró información de los procesos del área",
            numerado: "Si",
            ajustar: 'No',
            paginacion: "Si",
            paginacion: { filas: 10, pagina: 1, nombre: 'tabla-procesos' },
            opciones: $opciones
        });

    }
}

$("#selAreasProcesos").change(function () {
    if (!$.fn.ValidarInput({ html: "#selAreasProcesos", isSelect: true })) {
        $("#selAreasProcesos").parent(this).addClass("has-error");
    } else {
        $("#selAreasProcesos").parent(this).removeClass("has-error");

        var lsCodArea = $("#selAreasProcesos").val();
        $.fn.Conexion({
            direccion: '/Configuracion/ProcesosAreas',
            datos: { psCodArea: lsCodArea },
            //bloqueo: true,
            terminado: function (data) {
                datos = JSON.parse(data);
                MostarDatosProcesoAreas(datos);
            },
            //bloqueo: false
        });
    }
});

//#endregion

//#region Subprocesos de Areas
function MostrarSubProcesos(datos) {
    if (datos.oLstSubprocesos != null) {
        var $opciones = [
            {
                Columna: "Tipo1", id: "id-editar-subproceso", claseIcono: "entypo-pencil", clase: "editarSubProceso", titulo: "Editar", function: function (e) {
                    //$("#inSubProcesoCod").val($(this).parents("tr").find("td").eq(1).html());
                    $("#inSubProcesoNombre").val($(e).parents("tr").find("td").eq(2).html());
                    $("#inSubProcesoAbrev").val($(e).parents("tr").find("td").eq(3).html());

                    $("#btnActSubProceso").show();
                    $("#btnNuevoSubProceso").hide();
                    $("#btnRegSubProceso").hide();

                    $('#footer-gestion-subprocesos').slideDown();
                }
            },
            {
                Columna: "Tipo2", id: "id-eliminar-subproceso", claseIcono: "entypo-trash", clase: "eliminarSubProceso", titulo: "Eliminar", function: function (e) {
                    var lnCodSubProceso = $(e).parents("tr").find("td").eq(1).html();
                    bootbox.confirm({
                        message: "<strong>¿Está seguro de eliminar el subproceso?</strong>",
                        size: "sm",
                        closeButton: false,
                        backdrop: false,
                        classextra: true,
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
                                    direccion: '/Configuracion/RegistrarGestionSubProcesoAreas',
                                    datos: { psCodProceso: '', psDescSubProceso: '', psAbreviatura: '', pnIdSubProceso: lnCodSubProceso, pnAccion: 3 },
                                    //bloqueo: true,
                                    terminado: function (data) {
                                        datos = JSON.parse(data);
                                        MostrarSubProcesos(datos);
                                        $.fn.MensajeProcesos({
                                            clase: 'green',
                                            posicion: 'A',
                                            mensaje: 'Datos del subproceso elimininado correctamente.',
                                            titulo: 'Subproceso ' + lnCodSubProceso,
                                            tipo_mensaje: 'informacion'
                                        });
                                        $("#btnCancelarRegSubproceso").click();
                                    },
                                    //bloqueo: false
                                });

                            }
                        }
                    });
                }
            },
        ];

        $("#tbl-subprocesos-area-gestion").Tabla({
            tblId: "tbl-subprocesos-area-gestion",
            cabecera: "<strong>Código</strong>,<strong>Sub Proceso</strong>,<strong>Código</strong>,<strong>Fecha Registro</strong>",
            campos: "nCodSubProceso,cDescSubProceso,cAbreviatura,dUltimaActualizacion",
            datos: datos.oLstSubprocesos,
            classtbl: "table table-striped",
            //cantRegVertical: 5,
            //scrollVertical: "Si",
            alineado: "C,L,L,C",
            formato: "0,0,0,3",
            controles: "0,0,0,0",
            visible: "0,1,1,1",
            anchocolumna: "0,40,15,15",
            sindata: "No se encontró información para el subproceso seleccionado",
            numerado: "Si",
            paginacion: "Si",
            ajustar: 'No',
            paginacion: { filas: 5, pagina: 1, nombre: 'tabla-subprocesos' },
            opciones: $opciones
        });
    }
}

$("#btnNuevoSubProceso").click(function (e) {
    e.preventDefault();
    $(this).hide();
    $("#btnActSubProceso").hide();
    $("#btnRegSubProceso").show();

    $('#footer-gestion-subprocesos').slideDown();
});

$("#btnRegSubProceso").click(function (e) {
    e.preventDefault();

    if (!ValidarCamposSubprocesos()) { return; }

    var lsCodProceso = $("#tbl-procesos-area-gestion tr.seleccionado").find("td").eq(1).html();
    var lsDescSubProceso = $("#inSubProcesoNombre").val();
    var lsAbrevia = $("#inSubProcesoAbrev").val();

    $.fn.Conexion({
        direccion: '/Configuracion/RegistrarGestionSubProcesoAreas',
        datos: { psCodProceso: lsCodProceso, psDescSubProceso: lsDescSubProceso.trim(), psAbreviatura: lsAbrevia.trim(), pnIdSubProceso: 0, pnAccion: 1 },
        //bloqueo: true,
        terminado: function (data) {
            datos = JSON.parse(data);
            MostrarSubProcesos(datos);
            //if (datos > 0) {
            $.fn.MensajeProcesos({
                clase: 'green',
                posicion: 'A',
                mensaje: 'Datos registrados correctamente.',
                titulo: 'Subproceso',
                tipo_mensaje: 'exito'
            });
            $("#btnCancelarRegSubproceso").click();
        },
        //bloqueo: false
    });
});

$("#btnActSubProceso").click(function (e) {
    e.preventDefault();
    if (!ValidarCamposSubprocesos()) { return; }

    var lsCodProceso = $("#tbl-procesos-area-gestion tr.seleccionado").find("td").eq(1).html();
    var lnCodSubProceso = $("#tbl-subprocesos-area-gestion tr.seleccionado").find("td").eq(1).html();//$("#inSubProcesoCod").val();
    var lsDescSubProceso = $("#inSubProcesoNombre").val();
    var lsAbrevia = $("#inSubProcesoAbrev").val();

    $.fn.Conexion({
        direccion: '/Configuracion/RegistrarGestionSubProcesoAreas',
        datos: { psCodProceso: lsCodProceso, psDescSubProceso: lsDescSubProceso, psAbreviatura: lsAbrevia, pnIdSubProceso: lnCodSubProceso, pnAccion: 2 },
        //bloqueo: true,
        terminado: function (data) {
            datos = JSON.parse(data);
            //if (datos > 0) {    
            MostrarSubProcesos(datos);
            $.fn.MensajeProcesos({
                clase: 'green',
                posicion: 'A',
                mensaje: 'Datos actualizado correctamente.',
                titulo: 'Subproceso ' + lnCodSubProceso,
                tipo_mensaje: 'exito'
            });
            $("#btnCancelarRegSubproceso").click();
        },
        //bloqueo: false
    });

});

$("#btnCancelarRegSubproceso").click(function (e) {
    e.preventDefault();
    $("#inSubProcesoNombre").val("");
    $("#inSubProcesoAbrev").val("");
    $("#btnNuevoSubProceso").show();
    $("#btnRegSubProceso").hide();
    $("#btnActSubProceso").hide();
    $("#footer-gestion-subprocesos").slideUp();
});

function ValidarCamposSubprocesos() {
    var validacion = true;

    validacion = $.fn.ValidarInput({ html: "#inSubProcesoNombre" });
    validacion = $.fn.ValidarInput({ html: "#inSubProcesoAbrev" });

    return validacion;
}
//#endregion
