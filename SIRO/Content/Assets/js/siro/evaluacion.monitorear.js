window.onload = function () {
    //Added by TORE 20210326: Adecuacion para filtro del proceso de monitorio
    //if (gUsuario.cRHCargoCod != "005011") {
    //    $("#selAreas").change();
    //} 

    //$("#ModalFiltroTpoEval").modal("show");

    //SeleccionarEval();
    MostrarListaEvluaciones(null);


    $("#selTpoEvaluacion").change();
}
var gCodTpoEval = "";



//function SeleccionarEval() {
//    var EvalTipos = new Array();
//    for (i = 0; i < gModelo.oLstEvaluacion.length; i++) {
//        var tipos = {
//            text: gModelo.oLstEvaluacion[i].cEvalDesc,
//            value: gModelo.oLstEvaluacion[i].nCodEval
//        }
//        EvalTipos.push(tipos);
//    }
//    bootbox.prompt({
//        title: "<strong>Seleccione la Autoevaluación</strong>",
//        inputType: 'select',
//        closeButton: false,
//        size: "sm",
//        buttons: {
//            confirm: {
//                label: 'Seleccionar',
//                className: 'btn-primary'
//            },
//            cancel: {
//                label: 'Cancelar',
//                className: 'btn-default'
//            }
//        },
//        inputOptions: EvalTipos,
//        callback: function (result) {
//            if (result != null) {
//                $("#search-col-02, #search-col-03").hide();
//                if (result == 1001) {
//                    $("#sec-filtro-areas").show();
//                    $("#sec-filtro-procesos").show();
//                    $("#sec-filtro-subprocesos").show();
//                } else if (result == 1004) {
//                    $("#sec-filtro-areas").show();
//                }
//            }
//        }
//    });
//}

//$("#selTpoEvaluacion").on('change', function (e) {
//    $.fn.Conexion({
//        direccion: '/RiesgoOperacional/ListarProcesosAreas',
//        datos: { psCodArea: $(this).val() },
//        bloqueo: true,
//        terminado: function (data) {
//            datos = JSON.parse(data);
//            $("#selProcesos").selselect2({ dataShow: "cDescProceso", dataValue: "cCodProceso", datalist: datos.oLstProcesoArea });
//        },
//    });
//});

//$("#selTpoEvaluacion").change(function () {
//    var nTipoEval = eval($(this).val());
//    if (nTipoEval == gTiposEval.Procesos || nTipoEval == gTiposEval.Areas) {
//        $("#search-col-02, #search-col-03").show();
//    } else {
//        $("#search-col-02, #search-col-03").hide();
//    }
//});

$("#selTpoEvaluacion").change(function () {
    if (eval($(this).val()) === gTiposEval.Procesos || eval($(this).val()) === gTiposEval.Areas) {
        if (($("#selAreas").val() != "" && $("#selAreas").is(":visible")) && ($("#selTaller").val() != "" && $("#selTaller").is(":visible"))) {
            
        } else {
            $("#search-col-02, #search-col-03").show();
        }
    } else if (eval($(this).val()) === gTiposEval.SubContratacionSignificativa || eval($(this).val()) === gTiposEval.NuevoProducto) {
        $("#search-col-02, #search-col-03").hide();


    }


    //$.fn.Conexion({
    //    direccion: '/RiesgoOperacional/ListarProcesosAreas',
    //    datos: { psCodArea: $(this).val() },
    //    terminado: function (data) {
    //        datos = JSON.parse(data);
    //        $("#selProcesos").selselectboxit({ dataShow: "cDescProceso", dataValue: "cCodProceso", datalist: datos.oLstProcesoArea });
    //    }
    //});
});

$("#selAreas").change(function () {
    //$.fn.Conexion({
    //    direccion: '/RiesgoOperacional/ListarProcesosAreas',
    //    datos: { psCodArea: $(this).val() },
    //    terminado: function (data) {
    //        datos = JSON.parse(data);
    //        //$("#selProcesos").selselectboxit({ dataShow: "cDescProceso", dataValue: "cCodProceso", datalist: datos.oLstProcesoArea });
    //    }
    //});

    $.fn.Conexion({
        direccion: '/Evaluacion/ObtenerTallerUsuario',
        datos: { psCodTpoEval: ($("#selTpoEvaluacion").val() + $(this).val()) },
        mensaje: 'Obteniendo la lista de talleres gestionados.',
        bloqueo: false,
        terminado: function (data) {
            if (data.Talleres.length > 0) {
                $("#selTaller").selselect2({ dataShow: "cCodTaller", dataValue: "cCodTaller", datalist: data.Talleres });
                //if (localStorage.getItem("CodTaller") !== null) {
                //    $("#selTaller").val(localStorage.CodTaller).trigger("change");
                //}
                //if (gUsuario.cRHCargoCod != gConstGeneral.AnalistaRiesgoOperacional) {
                //    MostrarListaEvluaciones(null);
                //}
            } else {
                $("#selTaller").selselect2({ dataShow: "cCodTaller", dataValue: "cCodTaller", datalist: null });
                $("#selTaller").val(null).trigger("change");
                $.fn.MensajeProcesos({
                    posicion: 'A',
                    mensaje: 'No se encontró taller',
                    titulo: 'Aviso',
                    tipo_mensaje: 'informacion'
                });
            }
        },
    });
});

//$("#selProcesos").change(function () {
//    //var $CodProceso = $(this).val();
//    $.fn.Conexion({
//        direccion: '/RiesgoOperacional/ListarSubProcesosAreas',
//        datos: { "psCodProceso": $(this).val() },
//        terminado: function (data) {
//            datos = JSON.parse(data);
//            $("#selSubProcesos").selselectboxit({ dataShow: "cDescSubProceso", dataValue: "nCodSubProceso", datalist: datos.oLstSubProcesoArea });
//        }
//    });
//});


//$("#selUsuarios").change(function () {
//    var lsEstadoDisponibles = '100,104';
//    $.fn.Conexion({
//        direccion: '/Evaluacion/ObtenerTallerUsuario',
//        datos: { psCodTpoEval: gCodTpoEval, psUser: $(this).val(), psEstado: lsEstadoDisponibles },
//        mensaje: 'Obteniendo talleres del usuario',
//        bloqueo: true,
//        terminado: function (data) {
//            if (data.Talleres.length > 0) {
//                $("#selTaller").selselect2({ dataShow: "cCodTaller", dataValue: "cCodTaller", datalist: data.Talleres });
//                //if (localStorage.getItem("CodTaller") !== null) {
//                //    $("#selTaller").val(localStorage.CodTaller).trigger("change");
//                //}
//                if (gUsuario.cRHCargoCod != gConstGeneral.AnalistaRiesgoOperacional) {
//                    MostrarListaEvluaciones(null);
//                }
//            } else {
//                $.fn.MensajeProcesos({
//                    posicion: 'A',
//                    mensaje: 'No se encontro talleres del usuario',
//                    titulo: 'Taller',
//                    tipo_mensaje: 'informacion'
//                });
//                $("#selTaller").val(null).trigger("change");
//            }
//        },
//    });
//});

$("#selTaller").change(function () {
    //var lsEstadoDisponibles = '100';
    $.fn.Conexion({
        direccion: '/Evaluacion/MostrarEvaluacionesVerificacion',
        datos: { psCodTaller: $(this).val() },
        mensaje: 'Obteniedo las evaluaciones del taller ' + $(this).val(),
        bloqueo: true,
        terminado: function (data) {
            datos = JSON.parse(data);
            if (datos.oLstDatosEvaluaciones.length > 0) {
                $('#InfoTaller').show();
                $('#tdCodTaller').html(datos.oTaller.cCodTaller);
                $('#tdEstadoTaller').html(datos.oTaller.cEstado);
                $('#tdUserNotifica').html(datos.oTaller.oDatosRiesgo.oUsuarios.cUser);
                $('#tdFechaCrea').html(moment(datos.oTaller.dFechaCreacion).format("DD/MM/YYYY"));
                //$('#tdUserCrea').val(datos.oTaller.oDatosRiesgo.oUsuarios.cUser);
                $('#btnNotificarUser').prop('disabled', (datos.oTaller.nEstado == 104 ? false : true));
                //if (datos.oTaller.cEstado === 104) {
                //    $('#btnNotificarUser').prop('disabled', false);
                //} else {
                //    $('#btnNotificarUser').prop('disabled', true);
                //}
                MostrarListaEvluaciones(datos.oLstDatosEvaluaciones);
            } else {
                //$('#inEstadoRiesgo').val("");
                $('#tdCodTaller').html("");
                $('#tdEstadoTaller').html("");
                $('#tdUserNotifica').html("");
                $('#tdFechaCrea').html("");
                //$('#tdUserCrea').val("");
                MostrarListaEvluaciones(null);
            }
        },
    });
});



function MostrarListaEvluaciones(oLstEvaluaciones) {
    $("#content-tbl-evaluaciones-monitoreo").Tabla({
        tblId: "tbl-evaluaciones-monitoreo",
        cabecera: "Nro Riesgo,<strong>Código Riesgo</strong>,<strong>Riesgo Identificado</strong>,<strong>Tipo Eval.</strong>,<strong>Proceso Act.</strong>,<strong>Estado Eval.</strong>,<strong>Estado Taller</strong>,<strong>Fecha Reg.</strong>",
        campos: "nNroRiesgo,cCodRiesgo,cRiesgoIdentiticado,oEvaluaciones.cEvalDesc,cProcRiesgo,cEstadoRiesgo,cEstadoTaller,dFechaReg",
        datos: oLstEvaluaciones,
        classtbl: "table table-responsive responsive",
        //cantRegVertical: 10,
        //scrollVertical: "Si",
        alineado: "C,C,L,C,C,C,C,C",
        formato: "0,0,0,0,0,0,0,3",
        controles: "0,0,4,0,0,0,0,0",
        visible: "0,1,1,1,1,1,0,1",
        anchocolumna: "0,12,30,10,10,10,0,10",
        sindata: "No se encontró información para mostrar",
        numerado: "Si",
        paginacion: "Si",
        ajustar: 'No',
        paginacion: { filas: 5, pagina: 1, nombre: "tabla-evaluacion-monitoreo" },
        opciones: [
            {
                Columna: "Tipo2", id: "id-verificar-evaluacion", claseIcono: "entypo-forward", clase: "verificar-evaluacion", titulo: "Verificar Eval.", function: function (e) {
                    var ObjParam = { evaluacion: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
                    if (gUsuario.cRHCargoCod == gConstGeneral.AnalistaRiesgoOperacional) {
                        localStorage.CodArea = $("#selAreas").val();
                        //localStorage.CodUsuario = $("#selUsuarios").val();
                        //localStorage.CodTaller = $("#selTaller").val();
                    } else {
                        localStorage.clear();
                    }

                    $.fn.Conexion({
                        direccion: '/RiesgoOperacional/DevolverVistaParam',
                        datos: { Vista: "Gestion", Controlador: "Evaluacion", Parametros: JSON.stringify(ObjParam) },
                        terminado: function (data) {
                            window.location = data.url;
                        },
                    });

                }
            },
        ],

    });

    if (oLstEvaluaciones != null) {
        $("#pnlDetTaller").show();
    } else {
        $("#pnlDetTaller").hide();
    }



}


////$("#btnEnvioFiltro").click(function (e) {
//$("#btnMostrar").click(function (e) {
//    e.preventDefault();
//    var lnTpoEvalucion = $("#selTpoEvaluacion").val();
//    if (!ValidadarCamposFiltro(eval(lnTpoEvalucion))) {
//        return false;
//    }
//    //var lnTpoEvalucion = $("#selTpoEvaluacion").val();
//    switch (eval(lnTpoEvalucion)) {
//        case gTiposEval.Procesos:
//            gCodTpoEval = lnTpoEvalucion + $("#selAreas").val() + $("#selProcesos").val() + $("#selSubProcesos").val();
//            break;
//        case gTiposEval.SubContratacionSignificativa:
//            gCodTpoEval = lnTpoEvalucion;
//            break;
//        case gTiposEval.NuevoProducto:
//            gCodTpoEval = lnTpoEvalucion;
//            break;
//        case gTiposEval.Areas:
//            gCodTpoEval = lnTpoEvalucion + $("#selAreas").val();
//            break;
//        default:
//            gCodTpoEval = "";
//            break;
//    }

//    $.fn.Conexion({
//        direccion: '/Evaluacion/ObtenerUsuarioTpoEvaluacion',
//        datos: { psCodTpoEval: gCodTpoEval },
//        //bloqueo: true,
//        terminado: function (datos) {
//            if (datos.ListaUsuarios.length > 0) {
//                $("#selUsuarios").selselect2({ dataShow: "cUsuario", dataValue: "cUser", datalist: datos.ListaUsuarios });
//                $("#btnCerrarFiltro").click();
//            } else {
//                $.fn.MensajeProcesos({
//                    //clase: 'green',
//                    posicion: "A",
//                    mensaje: "No se halló datos de evaluaciones realizadas para el filtro aplicado.",
//                    titulo: 'Filtro',
//                    tipo_mensaje: 'informacion',
//                });
//            }

//        },
//    });

//});


$("#btnNotificarUser").click(function (e) {
    e.preventDefault();
    if ($("#selTaller").val() == null) {
        $.fn.MensajeProcesos({
            //clase: 'green',
            posicion: "A",
            mensaje: "Seleccione el taller",
            titulo: 'Finalizar Taller',
            tipo_mensaje: 'informacion',
        });
        return;
    }
    bootbox.confirm({
        title: "<strong>Finalizar Evaluación</strong>",
        message: "<strong>¿Está seguro de finalizar con la evaluación?</strong>",
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
                    direccion: '/Evaluacion/FinalizarEvaluacion',
                    datos: { psCodTaller: $('#selTaller').val() },
                    bloqueo: true,
                    terminado: function (datos) {
                        //if (datos.ExitoGestion > 0) {
                        //    $.fn.MensajeProcesos({
                        //        clase: 'green',
                        //        posicion: 'A',
                        //        mensaje: 'Se guardó correctamente la información.',
                        //        titulo: gDatosRiego.cCodRiesgo + ': Paso 5',
                        //        tipo_mensaje: 'exito',
                        //        onhidden: function () {
                        //            if (gDatosRiego.nTpoRiesgo == 2) {
                        //                $.fn.Conexion({
                        //                    direccion: '/RiesgoOperacional/DevolverVista',
                        //                    datos: { Vista: "MonitorearEval", Controlador: "Evaluacion" },
                        //                    terminado: function (data) {
                        //                        window.location = data;
                        //                    }
                        //                });
                        //            }
                        //        }
                        //    });
                        //}
                    },
                });


            }
        }
    });



});

function ValidadarCamposFiltro(lnTpoEvalucion) {
    var validacion = true;
    switch (lnTpoEvalucion) {
        case gTiposEval.Procesos || gTiposEval.Areas:
            validacion = $.fn.ValidarInput({ html: "#selTpoEvaluacion", isSelect: true });
            validacion = $.fn.ValidarInput({ html: "#selAreas", isSelect: true });
            validacion = $.fn.ValidarInput({ html: "#selTaller", isSelect: true });
            break;
        case gTiposEval.SubContratacionSignificativa:
            validacion = $.fn.ValidarInput({ html: "#selTpoEvaluacion", isSelect: true });
            break;
        case gTiposEval.NuevoProducto:
            validacion = $.fn.ValidarInput({ html: "#selTpoEvaluacion", isSelect: true });
            break;
        //case gTiposEval.Areas:
        //    gCodTpoEval = lnTpoEvalucion + $("#selAreas").val();
        //    break;
        default:
            validacion = false;
            break;
    }
    return validacion;
}





