window.onload = function () {
    if (gUsuario.cRHCargoCod != "005011") {
        $("#selTpoEvaluacion").val(1001).change().prop("disabled", true);
    } else {
        $("#selTpoEvaluacion").val(1001).trigger("change");
    }
}


$("#selTpoEvaluacion").change(function () {
    var lnTpoEvalucion = $(this).val();
    switch (eval(lnTpoEvalucion)) {
        case 1001:
            $.fn.Conexion({
                direccion: '/Evaluacion/DevolverVistaParcial',
                datos: { VistaParcial: "Filtros/_FiltrarEvaluacionesxProceso", Modelo: JSON.stringify(gModelo.oLstAreas) },
                bloqueo: true,
                mensaje: "Obteniendo la información, por favor espere",
                terminado: function (data) {
                    $("#FiltroProcesos").html(data.view);
                    if (gUsuario.cRHCargoCod != '005011') { $("#selAreas").val(gUsuario.oAreas.cAreaCod).change().prop("disabled", true); }
                    $("#FiltroProcesos").show();
                },
            });
            break;
        case 1002:
            $("#FiltroProcesos").hide();
            break;
        case 1003:
            $("#FiltroProcesos").hide();
            break;
        case 1004:
            $.fn.Conexion({
                direccion: '/Evaluacion/DevolverVistaParcial',
                datos: { VistaParcial: "Filtros/_FiltrarEvaluacionesxAreas", Modelo: JSON.stringify(gModelo.oLstAreas) },
                mensaje: "Obteniendo la información, por favor espere",
                bloqueo: true,
                terminado: function (data) {
                    $("#FiltroProcesos").html(data.view);
                    $("#FiltroProcesos").show();
                },
            });
            break;
        default:
            $("#FiltroProcesos").hide();
            break;
    }
});

function ValidarDatos(tpo_evaluacion) {
    var validacion = true;
    //validacion = $.fn.ValidarInput({ html: "#inRiesgoIdentificado" });
    //validacion = $.fn.ValidarInput({ html: "#selAgencias", isSelect: true });

    validacion = $.fn.ValidarInput({ html: "#selTpoEvaluacion", isSelect: true });
    if (tpo_evaluacion == 1001) {
        validacion = $.fn.ValidarInput({ html: "#selAreas", isSelect: true });
        validacion = $.fn.ValidarInput({ html: "#selProcesos", isSelect: true });
        validacion = $.fn.ValidarInput({ html: "#selSubProcesos", isSelect: true });
    } else if (tpo_evaluacion == 1004) {
        validacion = $.fn.ValidarInput({ html: "#selAreas", isSelect: true });
    }
    validacion = $.fn.ValidarInput({ html: "#inRiesgoIdentificado" });
    validacion = $.fn.ValidarInput({ html: "#selCausaRiesgo", isSelect: true });

    return validacion;
}

$("#btnRegEvaluacion").click(function (e) {
    e.preventDefault();
    if (!ValidarDatos(eval($('#selTpoEvaluacion').val()))) { return false; }

    var lnCodEvalucion = $('#selTpoEvaluacion').val();
    var lsCodAge = eval(lnCodEvalucion) == 1001 | 1004 ? $("#selAreas").val() : null;
    var lsCodArea = eval(lnCodEvalucion) == 1001 ? $("#selAreas").val() : null;
    var lsCodProceso = eval(lnCodEvalucion) == 1001 ? $("#selProcesos").val() : null;
    var lsCodSubProceso = eval(lnCodEvalucion) == 1001 ? $("#selSubProcesos").val() : null;

    var lsRiesgoIdentificado = $("#inRiesgoIdentificado").val().trim();
    var lsCausasRiesgo = $("#selCausaRiesgo").val();
    var i = 0; var lsCausas = ""; for (i in lsCausasRiesgo) { lsCausas += lsCausasRiesgo[i] + ","; }
    if (lsCausas) { lsCausas = lsCausas.slice(0, lsCausas.length - 1); }

    $.fn.Conexion({
        direccion: '/Evaluacion/RegistrarEvaluacion',
        datos: {
            pnCodEvaluacion: lnCodEvalucion, psRiesgoIdentificado: lsRiesgoIdentificado, psCauasRiesgo: lsCausas,
            psAgeCod: lsCodAge, psAreaCod: lsCodArea, psCodProceos: lsCodProceso, psCodSubProceso: lsCodSubProceso
        },
        mensaje: " Guardando la información...",
        bloqueo: true,
        terminado: function (data) {
            if (data.CodRiesgoEval != null && data.CodRiesgoEval != "") {
                $.fn.MensajeProcesos({
                    //clase: 'green',
                    posicion: 'A',
                    mensaje: 'Se registró correctamente la evaluación',
                    titulo: 'Evaluación: ' + data.CodRiesgoEval,
                    tipo_mensaje: 'exito',
                    onhidden: function () {
                        bootbox.confirm({
                            message: "<strong>¿Desea realizar el registro de otra Autoevaluación?</strong>",
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
                                    location.reload();
                                } else {
                                    $.fn.Conexion({
                                        direccion: '/RiesgoOperacional/DevolverVista',
                                        datos: { Vista: "Lista", Controlador: "Evaluacion" },
                                        terminado: function (url) {
                                            window.location.href = url;
                                        },
                                    });

                                }
                            }
                        });
                    }
                });
            }
        },
    });
});






