
window.onload = function () {
    $("#selCondicionPlan").val("3000").trigger("change");
}

$("#selCondicionPlan").change(function () {
    var valCondicion = eval($(this).val());
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ListarConstante',
        datos: { pnConsCod: valCondicion },
        terminado: function (data) {
            datos = JSON.parse(data);
            if (valCondicion === 3000) {
                //datos.oLstConstante
                $("#selgetCondicionPlan").selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: datos.oLstConstante });
                //$("label[for='selgetCondicionPlan']").html("Estado");
                //$("#sec-1-cod-riesgo").hide();
            } else if (valCondicion === 5000) {
                $("#selgetCondicionPlan").selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: datos.oLstConstante });
                //$("label[for='selgetCondicionPlan']").html("Tipo Riesgo");
                $("#sec-1-cod-riesgo").show();
            }
        },
    });
});

$("#btnGenerarSeccion1").click(function (e) {
    e.preventDefault();
    var condicion = $("#selCondicionPlan").val();
    var param = {
        pnTpoBuscar: $("#selCondicionPlan").val(),
        pnTpoRiesgo: $("#selgetCondicionPlan").val(),
        psBuscar: condicion
    };
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVistaParam',
        datos: { Vista: "ReportePlanesAccion", Controlador: "Reporte", Parametros: JSON.stringify(param) },
        terminado: function (data) {
            window.location = data;
        },
    });

});


//Riesgo Operacional

$("#btnGenerarExcelRechazos").click(function (e) {
    e.preventDefault();

    var inDesde = $("#inRechazoDesde"); 
    var inHasta = $("#inRechazoHasta"); 

    var parameters = {
        psDesde: inDesde.val(),
        psHasta: inHasta.val()
    };
    debugger;
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVistaParam',
        datos: { Vista: "ReportesRiesgosRechazados", Controlador: "Reporte", Parametros: JSON.stringify(parameters) },
        terminado: function (data) {

            window.location = data.url;

            //$.fn.MensajeProcesos({
            //    posicion: "A",
            //    mensaje: "No se encontró informacion sobre la opción seleccionado.",
            //    titulo: "Planes de Acción por Estado",
            //    tipo_mensaje: "informacion"
            //});

        },
    });

});


$("#btnGenerarPlanAccionEstado").click(function (e) {
    e.preventDefault();
    var selEstadoPlanAccion = $("#selEstadoPlanAccion");
    if (selEstadoPlanAccion.val() == null) {
        $.fn.MensajeProcesos({
            posicion: "A",
            mensaje: "Seleccione el motivo del incentivo.",
            titulo: "Reporte",
            tipo_mensaje: "informacion"
        });
        selEstadoPlanAccion.focus();
        return false;
    }

    var parameters = {
        pnTpoRiesgo: gConstGeneral.RiesgoOperacional,
        pnEstado: selEstadoPlanAccion.val()
    };

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVistaParam',
        datos: { Vista: "ReportesPlanAccionEstado", Controlador: "Reporte", Parametros: JSON.stringify(parameters) },
        terminado: function (data) {
            if (data.reporte == '{No}') {

                $.fn.MensajeProcesos({
                    posicion: "A",
                    mensaje: "No se encontró informacion sobre la opción seleccionado.",
                    titulo: "Planes de Acción por Estado",
                    tipo_mensaje: "informacion"
                });
            } else {
                window.location = data.url;
            }
        },
    });

});

$("#btnGenerarIncentivoMotivo").click(function (e) {
    e.preventDefault();
    var selMotivoIcentivo = $("#selMotivoIcentivo");
    if (selMotivoIcentivo.val() == null) {
        $.fn.MensajeProcesos({
            posicion: "A",
            mensaje: "Seleccione el motivo del incentivo.",
            titulo: "Reporte",
            tipo_mensaje: "informacion"
        });
        selMotivoIcentivo.focus();
        return false;
    }

    var parameters = {
        pnReporte: 1,
        pnTpoRiesgo: gConstGeneral.RiesgoOperacional,
        pnMotivo: selMotivoIcentivo.val()
    };

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVistaParam',
        datos: { Vista: "ReportesIncentivos", Controlador: "Reporte", Parametros: JSON.stringify(parameters) },
        terminado: function (data) {
            window.location = data.url;
        },
    });

});

$("#btnGenerarIncentivoMonto").click(function (e) {
    e.preventDefault();
    var inMontoIncentivo = $("#inMontoIncentivo");
    if (inMontoIncentivo.val() == '') {
        $.fn.MensajeProcesos({
            posicion: "A",
            mensaje: "Ingrese un motno para el incentivo.",
            titulo: "Reporte",
            tipo_mensaje: "informacion"
        });
        inMontoIncentivo.focus();
        return false;
    } else if (numeral(inMontoIncentivo.val()).value == 0) {
        $.fn.MensajeProcesos({
            posicion: "A",
            mensaje: "El monto del incentivo debe ser mayor a 0",
            titulo: "Reporte",
            tipo_mensaje: "informacion"
        });
        inMontoIncentivo.focus();
        return false;
    }

    var parameters = {
        pnReporte: 1,
        pnTpoRiesgo: gConstGeneral.RiesgoOperacional,
        pnMotivo: 0,
        pnMonto: inMontoIncentivo.val()
    };

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVistaParam',
        datos: { Vista: "ReportesIncentivos", Controlador: "Reporte", Parametros: JSON.stringify(parameters) },
        terminado: function (data) {
            window.location = data.url;
        },
    });

});






//End Riesgo Operacional 

//Evento de Perdida
$("#btnGenerarEventoPerdidaAgrupados").click(function (e) {
    e.preventDefault();

    //$.fn.Conexion({
    //    direccion: '/RiesgoOperacional/DevolverVista',
    //    datos: { Vista: "MatrizEventoConsolidado", Controlador: "Reporte" },
    //    terminado: function (data) {
    //        window.location = data;
    //    },
    //});

});

$("#btnGenerarEventoPerdidaConsol").click(function (e) {
    e.preventDefault();

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVista',
        datos: { Vista: "MatrizEventoConsolidado", Controlador: "Reporte" },
        terminado: function (data) {
            window.location = data;
        },
    });

});

$("#btnGenerarEventoPerdidaMayorMenor").click(function (e) {
    e.preventDefault();
    var selCondicion = $("#selCondicionReporte");
    if (selCondicion.val() == null) {
        $.fn.MensajeProcesos({
            posicion: "A",
            mensaje: "Seleccione el tipo del reporte a generar.",
            titulo: "Reporte",
            tipo_mensaje: "informacion"
        });
        selCondicion.focus();
        return false;
    }

    var parameters = {
        psValFiltro: selCondicion.val()
    };

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVistaParam',
        datos: { Vista: "MatrizEventoValorFiltro", Controlador: "Reporte", Parametros: JSON.stringify(parameters) },
        terminado: function (data) {
            window.location = data.url;
        },
    });

});
//End Evento de Perdida