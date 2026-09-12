$(document).ready(function () {





    $("#selLineaNegocio, #selAreas, #selProcesos").change();
});

$("#selAreas").change(function () {
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ListarProcesosAreas',
        datos: { psCodArea: $(this).val() },
        terminado: function (data) {
            datos = JSON.parse(data);
            $("#selProcesos").selselect2({ dataShow: "cDescProceso", dataValue: "cCodProceso", datalist: datos.oLstProcesoArea });
        },
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

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ListaControlesProceso',
        datos: { psCodProceso: $CodProceso },
        terminado: function (data) {
            datos = JSON.parse(data);
            $("#selControlProcesos").selselect2({ dataShow: "cControlDescripcion", dataValue: "nCodControl", datalist: datos.oLstControlesAreas });
        }
    });
});


$("#selLineaNegocio").change(function () {
    var select = $("#selSubLineaNegocio");
    //if ($(this).val() == "Seleccione opción") {
    //    select.prop("disabled", $(this).val() != "Seleccione opción");
    //} else {
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ListarSubLineaNegocio',
        datos: { psCodLineaNegocio: $(this).val() },
        terminado: function (data) {
            datos = JSON.parse(data);
            select.selselectboxit({ dataShow: "cDescSubLineaNeg", dataValue: "cCodSubLineaNeg", datalist: datos.oListSubLineaNegocio });
        },
    });
    //}
});



$("#btnGrabar").click(function (e) {
    e.preventDefault();

    if (!ValidarCampos()) { return; }

    var $medidas_correctivas = $("#inMedidasCorrectivas").val().trim();
    var $acciones_realizadas = $("#inAccionesRealizadas").val().trim();
    var $agencia = $("#selAgencias").val();
    var $area = $("#selAreas").val();
    var $linea_negocio = $("#selLineaNegocio").val();
    var $sub_linea_negocio = $("#selSubLineaNegocio").val();
    var $causas = $("#selCausaRiesgo").val();
    var lsCausas = "";
    for (var i in $causas) { lsCausas += $causas[i] + ","; }
    if (lsCausas) { lsCausas = lsCausas.slice(0, lsCausas.length - 1); }
    var $proceso = $("#selProcesos").val();
    var $subproceso = $("#selSubProcesos").val();
    var $desc_corta_evento = $("#selDescCortaEvento").val();
    var $riesgo_crediticio = $("#inRiesgoCrediticio").is(':checked') ? 1 : 0;
    var $exp_event_perdida = $("#inExpEvenPerdida").is(':checked') ? 1 : 0;

    $.fn.Conexion({
        direccion: "/EventoPerdida/GrabarGestionEventoPerdida",
        datos: {
            pnNroRiesgo: gDatosEvento.oDatosRiesgo.nNroRiesgo, psAgeCod: $agencia, psAreaCod: $area, psLineaNeg: $linea_negocio, psSubLineaNeg: $sub_linea_negocio, psCausas: lsCausas,
            psProcesos: $proceso, psSubProcesos: $subproceso, pnSubEventoPerdida: $desc_corta_evento, pbRiesgoCrediticio: $riesgo_crediticio, pbExEventoPerdida: $exp_event_perdida,
            psMedidasCorrectivas: $medidas_correctivas, psAccionRealizada: $acciones_realizadas
        },
        bloqueo: true,
        mensaje: "Grabando la información, por favor espere.",
        terminado: function (data) {
            $.fn.MensajeProcesos({
                clase: 'green',
                posicion: 'A',
                mensaje: data.MensajeSis,
                titulo: 'Evento de Pérdida',
                tipo_mensaje: data.Tipo,
                onhidden: function () {
                    $.fn.Conexion({
                        direccion: '/RiesgoOperacional/DevolverVista',
                        datos: { Vista: "ListaEvenPerdida", Controlador: "EventoPerdida" },
                        terminado: function (url) {
                            window.location.href = url;
                        },
                    });
                }

            });
        },
    });

});


function MostrarDetalleEventoPerdida(evento) {
    if (evento == "") { return; }
    var ObjParam = { evento: base64_encode(evento) };
    $.fn.Conexion({
        direccion: "/RiesgoOperacional/DevolverVistaParam",
        datos: { Vista: "DetalleEventoPerdida", Controlador: "EventoPerdida", Parametros: JSON.stringify(ObjParam) },
        terminado: function (data) {
            window.location = data.url;
        },
    });
}



function ValidarCampos() {
    var validado = true;
    //if (!$.fn.ValidarInput({ html: "#inMedidasCorrectivas" })) {
    //    $("label[for='inMedidasCorrectivas']").addClass("validar");
    //    validacion = false;
    //} else {
    //    $("label[for='inMedidasCorrectivas']").removeClass("validar");
    //}

    //if (!$.fn.ValidarInput({ html: "#inAccionesRealizadas" })) {
    //    //$("#inRiesgoIdentificado").addClass("validar");
    //    $("label[for='inAccionesRealizadas']").addClass("validar");
    //    validacion = false;
    //} else {
    //    $("label[for='inAccionesRealizadas']").removeClass("validar");
    //}

    if (!$.fn.ValidarInput({ html: "#selAgencias", isSelect: true })) {
        $("label[for='selAgencias']").addClass("validar");
        validacion = false;
    } else {
        $("label[for='selAgencias']").removeClass("validar")
    }

    if (!$.fn.ValidarInput({ html: "#selAreas", isSelect: true })) {
        $("label[for='selAreas']").addClass("validar");
        validacion = false;
    } else {
        $("label[for='selAreas']").removeClass("validar");
    }

    if (!$.fn.ValidarInput({ html: "#selLineaNegocio", isSelect: true })) {
        $("label[for='selLineaNegocio']").addClass("validar");
        validacion = false;
    } else {
        $("label[for='selLineaNegocio']").removeClass("validar");
    }

    if (!$.fn.ValidarInput({ html: "#selSubLineaNegocio", isSelect: true })) {
        $("label[for='selSubLineaNegocio']").addClass("validar");
        validacion = false;
    } else {
        $("label[for='selSubLineaNegocio']").removeClass("validar");
    }

    if (!$.fn.ValidarInput({ html: "#selCausaRiesgo", isSelect: true })) {
        $("label[for='selCausaRiesgo']").addClass("validar");
        validacion = false;
    } else {
        $("label[for='selCausaRiesgo']").removeClass("validar");
    }

    if (!$.fn.ValidarInput({ html: "#selProcesos", isSelect: true })) {
        $("label[for='selProcesos']").addClass("validar");
        validacion = false;
    } else {
        $("label[for='selProcesos']").removeClass("validar");
    }

    if (!$.fn.ValidarInput({ html: "#selSubProcesos", isSelect: true })) {
        $("label[for='selSubProcesos']").addClass("validar");
        validacion = false;
    } else {
        $("label[for='selSubProcesos']").removeClass("validar");
    }

    if (!$.fn.ValidarInput({ html: "#selDescCortaEvento", isSelect: true })) {
        $("label[for='selDescCortaEvento']").addClass("validar");
        validacion = false;
    } else {
        $("label[for='selDescCortaEvento']").removeClass("validar");
    }


    //var lbSugerenciaGM = $("#chkSugeridoGM").is(':checked') ? 1 : 0;






    return validado;


}