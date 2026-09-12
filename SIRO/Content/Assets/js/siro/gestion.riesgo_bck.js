var gnEfectividadControl = 0;
var gConstControles = null;

$(document).ready(function () {
    $('#rootwizard').bootstrapWizard({
        'tabClass': '',
        'onInit': function (tab, navigation, index) {
            $(".btngestion").hide();
            $(".features-blocks").hide(); //Add 20201106, comprobar que esta condicion no afecte a la gestion de las evaluaciones
            /*Condiciones para la habilitacion de los controles*/
            switch (gDatosRiesgo.nTpoRiesgo) {
                case 1: //Riesgos Operacionales
                    $(".features-blocks").remove() //los riesgos operacionales no tienen observaciones, es gestionado por el mismo analista de riesgo operacional
                    if (gDatosRiesgo.nProcRiesgo == 5) {
                        $(".btngestion").remove();
                        $("#sec_add_responsable").remove();
                    } else {
                        setTimeout(function () {
                            $("#btnGrabaPaso" + (gDatosRiesgo.nProcRiesgo + 1)).show();
                            $(".panel-options a").show();
                        }, 1);
                    }
                    break;
                case 2: //Evaluaciones
                    if (gDatosRiesgo.nEstadoRiesgo == 4 | 6 && gDatosRiesgo.nProcRiesgo == 4) {
                        /*Debemos verificar si la evaluacion fue reportado para modificacion*/
                        if (gDatosRiesgo.nCondicionTaller == 201) {
                            /*added by TORE 20201206: Se movio la condicion para las observaciones de la evaluacion dentro del taller*/
                            setTimeout(function () {
                                $("#rootwizard").bootstrapWizard('remove', 4, true);
                                $(".features-blocks").hide();
                                $("#btnGrabaPaso1, #btnGrabaPaso2, #btnGrabaPaso3, #btnGrabaPaso4").hide();
                            }, 1);
                            $.fn.Conexion({
                                direccion: '/RiesgoOperacional/ObtenerObservacionesGestion',
                                datos: { pnNroRiesgo: gDatosRiesgo.nNroRiesgo },
                                terminado: function (data) {
                                    getObs = JSON.parse(data.Observaciones);
                                    if (getObs.length > 0) {
                                        for (var i in getObs) {
                                            switch (getObs[i].Proceso) {
                                                case 1:
                                                    $.fn.MensajeProcesos({
                                                        posicion: 'A',
                                                        mensaje: '- ' + getObs[i].Observacion,
                                                        titulo: 'Observación - Paso ' + getObs[i].Proceso,
                                                        tiempo_limite: '0',
                                                        tiempo_extendido: '0',
                                                        tipo_mensaje: 'advertencia',
                                                        onclick: function () {
                                                            if (gUsuario.cRHCargoCod != '005011') {
                                                                $("#rootwizard").bootstrapWizard('show', 0);
                                                                $("#btnGrabaPaso1").show();
                                                            }
                                                        }
                                                    });
                                                    break;
                                                case 2:
                                                    $.fn.MensajeProcesos({
                                                        posicion: 'A',
                                                        mensaje: '- ' + getObs[i].Observacion,
                                                        titulo: 'Observación - Paso ' + getObs[i].Proceso,
                                                        tiempo_limite: '0',
                                                        tiempo_extendido: '0',
                                                        tipo_mensaje: 'advertencia',
                                                        onclick: function () {
                                                            if (gUsuario.cRHCargoCod != '005011') {
                                                                $("#rootwizard").bootstrapWizard('show', 1);
                                                                $("#btnGrabaPaso2").show();
                                                            }
                                                        }
                                                    });

                                                    break;
                                                case 3:
                                                    $.fn.MensajeProcesos({
                                                        posicion: 'A',
                                                        mensaje: '- ' + getObs[i].Observacion,
                                                        titulo: 'Observación - Paso ' + getObs[i].Proceso,
                                                        tiempo_limite: '0',
                                                        tiempo_extendido: '0',
                                                        tipo_mensaje: 'advertencia',
                                                        onclick: function () {
                                                            if (gUsuario.cRHCargoCod != '005011') {
                                                                $("#rootwizard").bootstrapWizard('show', 2);
                                                                $("#Paso3 .btngestion a").show();
                                                                $("#btnGrabaPaso3").show();
                                                            }
                                                        }
                                                    });
                                                    break;
                                                case 4:
                                                    $.fn.MensajeProcesos({
                                                        posicion: 'A',
                                                        mensaje: '- ' + getObs[i].Observacion,
                                                        titulo: 'Observación - Paso ' + getObs[i].Proceso,
                                                        tiempo_limite: '0',
                                                        tiempo_extendido: '0',
                                                        tipo_mensaje: 'advertencia',
                                                        onclick: function () {
                                                            if (gUsuario.cRHCargoCod != '005011') {
                                                                $("#rootwizard").bootstrapWizard('show', 3);
                                                                $("#Paso4 .btngestion a").show();
                                                                $("#btnGrabaPaso4").show();
                                                            }
                                                        }
                                                    });

                                                    break;
                                            }
                                        }

                                    } else {
                                        if (gUsuario.cUser == gDatosRiesgo.oUsuarios.cUser) {
                                            $("#btnConfirmaObs").show();
                                        }
                                    }
                                    $("#rootwizard").bootstrapWizard('show', 0);
                                },
                            });

                        }
                        else if (gDatosRiesgo.nCondicionTaller == 200 | 202 | 203) {
                            if (gUsuario.cRHCargoCod == '005011') {
                                $(".features-blocks").show();
                                $("#btnGrabaPaso5").show();
                            } else {
                                $(".features-blocks").remove();
                                $(".panel-options a").remove();
                                setTimeout(function () {
                                    $("#rootwizard").bootstrapWizard('remove', 4, true);
                                    $("#rootwizard").bootstrapWizard('show', 0);
                                }, 1);
                            }
                        }

                    } else if (gDatosRiesgo.nEstadoRiesgo == 5 && gDatosRiesgo.nProcRiesgo == 5) {
                        /*si el que ingreso es usuario normal*/
                        //si es usuario normal bloquear todo.
                        if (gUsuario.cRHCargoCod == '005011') {
                            $(".features-blocks").hide();
                            $("#sec_add_responsable").hide();
                            $("#btnGrabaPaso5").hide();
                        } else {
                            $(".features-blocks").hide();
                            $("#sec_add_responsable").hide();
                            $("#btnGrabaPaso5").hide();
                        }

                    } else {
                        setTimeout(function () {
                            $("#rootwizard").bootstrapWizard('remove', 4, true);
                            $("#btnGrabaPaso" + (gDatosRiesgo.nProcRiesgo + 1)).show();

                            $(".features-blocks").hide();
                            $(".panel-options a").show();

                        }, 1);
                        $('#rootwizard .progress-indicator').data('now-value', 4);
                    }
                    break;
            }
            setTimeout(function () { $("#rootwizard").bootstrapWizard('show', gDatosRiesgo.nProcRiesgo); }, 1);
        },
        'onShow': null,
        'onNext': function (tab, navigation, index) {
            if (index > gDatosRiesgo.nProcRiesgo) {
                return false;
            }
        },
        'onPrevious': function (tab, navigation, index) {
            //if (index > gDatosRiesgo.nProcRiesgo) {
            //    return false;
            //}
        },
        'onLast': null,
        'onTabChange': null,
        'onTabClick': function (tab, navigation, index) {
            var lntab = index + 1;
            if (!ValidacionControlesPasosGestion()) {
                return false;
            } else { }
        },
        'onTabShow': function (tab, navigation, index) {
            //var linea_progreso = $('#rootwizard .progress-indicator');
            //var total_proceso = linea_progreso.data('number-of-steps');
            //var procentaje_actual = linea_progreso.data('now-value');
            //
            //var porcentaje = 0;
            //if (seleccion > index) {
            //    porcentaje = procentaje_actual + (100 / total_proceso);
            //} else if (seleccion < index){
            //    porcentaje = procentaje_actual - (100 / total_proceso);
            //}
            //linea_progreso.css({ width: porcentaje + '%' }).data('now-value', porcentaje);

            navigation.find('li').removeClass('completed')

            var li_list = navigation.find('li');
            for (var i = 0; i < (eval(gDatosRiesgo.nProcRiesgo) + 1); i++) {
                jQuery(li_list[i]).addClass("completed");
            }

            CargarInfoGestionRiesgo(gDatosRiesgo.nNroRiesgo, (index + 1));

        }

    });

    //$("#rootwizard").bootstrapWizard('show', gDatosRiesgo.nProcRiesgo);

    if (gDatosRiesgo.nTpoRiesgo == 2) {
        if (gDatosRiesgo.nCondicionTaller == 201) {
            $("#rootwizard").bootstrapWizard('remove', 4, true);
        } else {
            $("#rootwizard").bootstrapWizard('show', 4);
        }
    }

    gARCHIVOS = $.fn.CargarArchivos({
        cClase: "table responsive",
        cDom: "#FileAttach",
        btn: "#btnAddFile",
        nCantidad: 1
    });

    $('#inObserva_1, #inObserva_2, #inObserva_3, #inObserva_4').on('keyup', function (e) {
        var lsDesc = "";
        if (gDatosRiesgo.nProcRiesgo < 5) {
            lsDesc += $("#inObserva_1").val().trim();
            lsDesc += $("#inObserva_2").val().trim();
            lsDesc += $("#inObserva_3").val().trim();
            lsDesc += $("#inObserva_4").val().trim();
            if (!(lsDesc.length >= 1)) { $('#btnObservaciones').hide(); $('#btnGrabaPaso5').show(); } else { $('#btnGrabaPaso5').hide(); $('#btnObservaciones').show(); }
        }
    });

});






//#region Validacion de Campos
$("#inFechaImplementacion").on({
    "keyup": function (event) {
        $(event.target).val(function (index, value) {
            if (moment(ValidarFormatoFecha(value)).isValid()) {
                if (moment(moment(ValidarFormatoFecha(value)).format("L")).diff(moment().format("MM-DD-YYYY"), "days") < 0) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'La fecha de implementación no debe ser menor la fecha actual',
                        titulo: 'Plan Acción - Fecha Implementación',
                        tipo_mensaje: 'informacion'
                    });
                    return "";
                }
            }
            return value;
        });
    }
});
//#endregion



function ValidacionControlesPasosGestion() {
    var validacion = true;
    var lnPasoActual = gDatosRiesgo.nProcRiesgo;
    var lnCurrentIndex = $('#rootwizard').bootstrapWizard('currentIndex');
    var lnTabSelect = $('#rootwizard').bootstrapWizard('tabselect')

    switch (lnCurrentIndex) {
        case 0: //Paso 1
            if (lnTabSelect > lnPasoActual) {
                var $validado = true;

                switch (lnPasoActual + 1) {
                    case 1:
                        $validado = ValidarCamposPaso1();
                        break;
                    case 2:
                        $validado = ValidarCamposPaso2();
                        break;
                    case 3:
                        $validado = ValidarCamposPaso3();

                        break;
                }
                if (!$validado) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'Es necesario completar la información de los campos remarcados',
                        titulo: 'Gestión de ' + (gDatosRiesgo.nTpoRiesgo == 1 ? 'Riesgo Operacional' : 'Evaluación') + ' - Paso ' + (lnPasoActual + 1),
                        tipo_mensaje: 'informacion'
                    });
                    validacion = false;
                } else {
                    if (lnPasoActual == lnCurrentIndex) {
                        bootbox.confirm({
                            message: "Se guardará la información gestionada <strong>¿Está seguro de pasar al paso 2?</strong>",
                            //classextra: true,
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
                                    $("#btnGrabaPaso1").click();
                                } else { $("#rootwizard").bootstrapWizard('show', 0); }
                            }
                        });
                    }
                }
            }
            break;
        case 1: //Paso 2
            if (lnTabSelect > lnPasoActual) {
                var $validado = true;

                switch (lnPasoActual + 1) {
                    case 1:
                        $validado = ValidarCamposPaso1();
                        break;
                    case 2:
                        $validado = ValidarCamposPaso2();
                        break;
                    case 3:
                        $validado = ValidarCamposPaso3();
                        break;
                }
                if (!$validado) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'Es necesario completar la información de los campos remarcados',
                        titulo: 'Gestión de ' + (gDatosRiesgo.nTpoRiesgo == 1 ? 'Riesgo Operacional' : 'Evaluación') + ' - Paso ' + (lnPasoActual + 1),
                        tipo_mensaje: 'informacion'
                    });
                    validacion = false;
                } else {
                    if (lnPasoActual == lnCurrentIndex) {
                        bootbox.confirm({
                            message: "Se guardará la información gestionada <strong>¿Esta seguro de pasar al paso 3?</strong>",
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
                                    $("#btnGrabaPaso2").click();
                                } else { $("#rootwizard").bootstrapWizard('show', 1); }
                            }
                        });
                    }
                }
            }

            break;
        case 2: //Paso 3
            if (lnTabSelect > lnPasoActual) {
                var $validado = true;

                switch (lnPasoActual + 1) {
                    case 1:
                        $validado = ValidarCamposPaso1();
                        break;
                    case 2:
                        $validado = ValidarCamposPaso2();
                        break;
                    case 3:
                        $validado = ValidarCamposPaso3();
                        break;
                }
                if (!$validado) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'Es necesario completar la información de los campos remarcados',
                        titulo: 'Gestión de ' + (gDatosRiesgo.nTpoRiesgo == 1 ? 'Riesgo Operacional' : 'Evaluación') + ' - Paso ' + (lnPasoActual + 1),
                        tipo_mensaje: 'informacion'
                    });
                    validacion = false;
                } else {
                    if (lnPasoActual == lnCurrentIndex) {
                        bootbox.confirm({
                            message: "Se guardará la información gestionada <strong>¿Esta seguro de pasar al paso 4?</strong>",
                            //classextra: true,
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
                                    $("#btnGrabaPaso3").click();
                                } else { $("#rootwizard").bootstrapWizard("show", 2); }
                            }
                        });
                    }
                }
            }
            break;
        case 3: //Paso 4
            if (lnTabSelect > lnPasoActual) {
                var $validado = true;

                switch (lnPasoActual + 1) {
                    case 1:
                        $validado = ValidarCamposPaso1();
                        break;
                    case 2:
                        $validado = ValidarCamposPaso2();
                        break;
                    case 3:
                        $validado = ValidarCamposPaso3();
                        break;
                }
                if (!$validado) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'Es necesario completar la información de los campos remarcados',
                        titulo: 'Gestión de ' + (gDatosRiesgo.nTpoRiesgo == 1 ? 'Riesgo Operacional' : 'Evaluación') + ' - Paso ' + (lnPasoActual + 1),
                        tipo_mensaje: 'informacion'
                    });
                    validacion = false;
                } else {
                    if (lnPasoActual == lnCurrentIndex) {
                        bootbox.confirm({
                            message: "Se guardará la información gestionada  <strong>¿Esta seguro de pasar al paso 5?</strong>",
                            //classextra: true,
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
                                    $("#btnGrabaPaso4").click();
                                } else { $("#rootwizard").bootstrapWizard('show', 3); }
                            }
                        });
                    }

                }

            }

            break;
        case 4: //Paso 5
            //if (lnTabSelect > lnPasoActual) {
            //    if (lnPasoActual == lnCurrentIndex) {
            //        bootbox.confirm({
            //            message: "Se guardará la información gestionada  <strong>¿Esta seguro de pasar al paso 5?</strong>",
            //            classextra: true,
            //            buttons: {
            //                confirm: {
            //                    label: 'SI',
            //                    className: 'btn-primary'
            //                },
            //                cancel: {
            //                    label: 'NO',
            //                    className: 'btn-default'
            //                }
            //            },
            //            callback: function (result) {
            //                if (result) {
            //                    $("#btnGrabaPaso3").click();
            //                } else { $("#rootwizard").bootstrapWizard('show', 2); }
            //            }
            //        });
            //    }
            //}

            break;
        default: // No se hallo el paso
            $.fn.MensajeProcesos({
                posicion: 'A',
                mensaje: 'No se encontró información del proceso seleccionado, por favor consultar con TI.',
                titulo: 'Mensaje del sistema',
                tipo_mensaje: 'error'

            });
    }
    return validacion;
}

// #region Modales

function ModalAddPlanAccion() {
    LimparCamposAddPlanAccion();
    $("#btnUpdPlanAccion").hide();
    $("#btnAddPlanAccion").show();
    $("#AddPlanesAccion").modal("show");
    //jQuery('#AddPlanesAccion').modal('show', { backdrop: 'static' });
}

function ModalCriteriosEvalucion() {
    //if (gDatosRiesgo.nEstadoRiesgo == 2 && gDatosRiesgo.nProcRiesgo <=2) {
    if (gDatosRiesgo.nProcRiesgo >= 3 && gDatosRiesgo.nEstadoRiesgo == 2) { // added  20201107 (gDatosRiesgo.nTpoRiesgo == 2 ? 2 : 0)
        bootbox.alert({
            message: "<strong>No se pudo realizar la acción debido que actualmente " + (gDatosRiesgo.nTpoRiesgo == 1 ? " el riesgo operacional " : " la evaluación ") + " se encuentra en gestión del proceso N° 4</strong>",
            size: "sm",
            closeButton: false
        });
    } else {
        $("#selRC").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: gConstControles.ListaRC });
        $("#selFD").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: gConstControles.ListaFD });
        $("#selED").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: gConstControles.ListaED });
        $("#selTC").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: gConstControles.ListaTC });
        $("#selCO").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: gConstControles.ListaCO });

        LimpiarModalAddControlRiesgoResidual();
        $("#btnUpdControlRiesgoResidual").hide();
        $("#btnAddControlRiesgoResidual").show();
        $("#AddCriteriosEval").modal("show");
    }
}
// #endregion


function CargarInfoGestionRiesgo(nNroRiesgo, nProceso) {
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ObtenerInfoGestionRiesgo',
        datos: { pnNroRiesgo: nNroRiesgo, pnNroProceso: nProceso },
        bloqueo: false,
        terminado: function (data) {
            datos = JSON.parse(data);
            gDatosRiesgo = datos.oDatosRiesgo;
            gConstControles = {
                ListaRC: datos.oLstResponsableDef,
                ListaFD: datos.oLstFrecuenciaDef,
                ListaED: datos.oLstEvidenciaControl,
                ListaTC: datos.oLstTipoEjecucion,
                ListaCO: datos.oLstCumpleObjetivo
            };
            gRC = datos.oLstResponsableDef; gFD = datos.oLstFrecuenciaDef; gED = datos.oLstEvidenciaControl; gTC = datos.oLstTipoEjecucion; gCO = datos.oLstCumpleObjetivo;
            //gDatosObservacion = datos.lstObservacionesGestion;

            switch (nProceso) {
                case 1:
                    $("#selFactorRiesgo").selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: datos.oLstFactorRiesgo, dataselect: datos.sLstInfoGestionRiesgo != null ? datos.sLstInfoGestionRiesgo[1] : null });
                    $("#selEventoPerdida").selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: datos.oLstEventoPerdida, dataselect: datos.sLstInfoGestionRiesgo != null ? datos.sLstInfoGestionRiesgo[2] : null });
                    $("#selLineaNegocio").selselectboxit({ dataShow: "cDescLineaNeg", dataValue: "cCodLineaNeg", datalist: datos.oLstLineaNegocio, dataselect: datos.sLstInfoGestionRiesgo != null ? datos.sLstInfoGestionRiesgo[3] : null });
                    $("#selProducto").selselectboxit({ dataShow: "cDescProducto", dataValue: "cCodProducto", datalist: datos.oLstProducto, dataselect: datos.sLstInfoGestionRiesgo != null ? datos.sLstInfoGestionRiesgo[4] : null });
                    $("#selSubProducto").selselectboxit({ dataShow: "cDescSubProducto", dataValue: "cCodSubProducto", datalist: datos.oLstSubProducto, dataselect: datos.sLstInfoGestionRiesgo != null ? datos.sLstInfoGestionRiesgo[5] : null });
                    break;
                case 2:
                    $("#selProbabilidad").selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: datos.oLstProbabilidad, dataselect: datos.sLstInfoGestionRiesgo != null ? datos.sLstInfoGestionRiesgo[1] : null });
                    $("#selImpacto").selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: datos.oLstImpacto, dataselect: datos.sLstInfoGestionRiesgo != null ? datos.sLstInfoGestionRiesgo[2] : null });
                    if (datos.sLstInfoGestionRiesgo != null) { ObtenerNivelRiesgoInherente($("#selProbabilidad").val(), $("#selImpacto").val(), false) }
                    break;
                case 3:
                    MostrarControlRiesgoResidual(datos);
                    $("#inComentariosRiesgoresidual").val(datos.sLstInfoGestionRiesgo[2]);
                    //$("#selRC").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: datos.oLstResponsableDef });
                    //$("#selFD").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: datos.oLstFrecuenciaDef });
                    //$("#selED").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: datos.oLstEvidenciaControl });
                    //$("#selTC").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: datos.oLstTipoEjecucion });
                    //$("#selCO").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: datos.oLstCumpleObjetivo });
                    break;
                case 4:
                    MostrarPlanesAccion(datos);
                    break;
                case 5:
                    $("#selAgenciasRespon").selselect2({ dataShow: "cAgeDescripcion", dataValue: "cAgeCod", datalist: datos.oLstAgencia });
                    $("#selAreaRespon").selselect2({ dataShow: "cAreaDescripcion", dataValue: "cAreaCod", datalist: datos.oLstAreas });
                    MostrarResponsablePlanesAccion(datos);
                    break;
                default:
                    break;
            }

        }
    });
}


// #region Gestion Paso 1
$("#selLineaNegocio").change(function () {
    ObtenerProducto($("#selLineaNegocio").val());
});

function ObtenerProducto(sCodLineaNeg) {
    var lstProducto = new Array();
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ListarProducto',
        datos: { psCodLineaNegocio: sCodLineaNeg },
        terminado: function (data) {
            lstProducto = JSON.parse(data);
            $("#selProducto").selselectboxit({ dataShow: "cDescProducto", dataValue: "cCodProducto", datalist: lstProducto.oLstProducto });
        },
    });
}

$("#selProducto").change(function () {
    //$('select[id="selProducto"] option:selected').text() /*Obtiene el texto de la opcion seleccionado*/
    ObtenerSubProducto($("#selProducto").val());
});

function ObtenerSubProducto(sCodProducto, sDescProducto) {
    var lstSubProducto = new Array();
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ListarSubProducto',
        datos: { psCodProducto: sCodProducto },
        terminado: function (data) {
            lstSubProducto = JSON.parse(data);
            //CrearLista('#selSubProducto', lstSubProducto.oLstSubProducto, 'cCodSubProducto', 'cDescSubProducto', 'Sub Producto de ' + sDescProducto);
            $("#selSubProducto").selselectboxit({ dataShow: "cDescSubProducto", dataValue: "cCodSubProducto", datalist: lstSubProducto.oLstSubProducto });
            //CrearListaSimple('#selSubProducto', lstSubProducto.oLstSubProducto, 'cCodSubProducto', 'cDescSubProducto');
        },
        //bloqueo: false
    });
}

$("#btnGrabaPaso1").click(function (e) {
    e.preventDefault();
    if (!ValidarCamposPaso1()) {
        $.fn.MensajeProcesos({
            posicion: 'A',
            mensaje: 'Es necesario completar la información de los campos remarcados',
            titulo: 'Gestión de ' + (gDatosRiesgo.nTpoRiesgo == 1 ? 'Riesgo Operacional' : 'Evaluación') + ' - Paso 1',
            tipo_mensaje: 'informacion'

        });
        $("#rootwizard").bootstrapWizard('show', 0);
        return false;
    }

    var lnFactorRiesgo = $("#selFactorRiesgo").val();
    var lnEventoPerdida = $("#selEventoPerdida").val();
    var lsLineaNeg = $("#selLineaNegocio").val();
    var lsProducto = $("#selProducto").val();
    var lsSubProducto = $("#selSubProducto").val();

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/GrabarProcesoPaso1',
        datos: { pnNroRiesgo: gDatosRiesgo.nNroRiesgo, pnFactorRiesgo: lnFactorRiesgo, pnEventoPerdida: lnEventoPerdida, psLineaNeg: lsLineaNeg, psProducto: lsProducto, psSubProducto: lsSubProducto },
        terminado: function (data) {
            $.fn.MensajeProcesos({
                posicion: 'A',
                mensaje: data.MensajeSis,
                titulo: 'Identificación del Riesgo: ' + gDatosRiesgo.cCodRiesgo,
                tipo_mensaje: data.TpoMensaje,
                onhidden: function () {
                    if (data.TpoMensaje === 'exito' | 'informacion') {
                        //$("#btnGrabaPaso1").hide();
                        $("#btnGrabaPaso1").remove();
                        if (gDatosRiesgo.nCondicionTaller == 201) {
                            if (data.Observaciones == 0) {
                                if (gUsuario.cUser == gDatosRiesgo.oUsuarios.cUser) {
                                    $("#btnConfirmaObs").show();
                                }
                            } //else {
                            //  $("#btnGrabaPaso1").remove();
                            //}
                        } else {
                            $("#btnGrabaPaso2").show();
                            $("#rootwizard").bootstrapWizard("show", 1);
                        }
                    }
                }
            });
        },
    });
});

function ValidarCamposPaso1() {
    var validacion = true;

    validacion = $.fn.ValidarInput({ html: "#selFactorRiesgo", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selEventoPerdida", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selLineaNegocio", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selProducto", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selSubProducto", isSelect: true });

    return validacion;
}

// #endregion

// #region Gestion Paso 2
$("#selProbabilidad").change(function () {
    if ($("#selImpacto").val() != null) {
        ObtenerNivelRiesgoInherente($("#selProbabilidad").val(), $("#selImpacto").val(), false)
    }
});

$("#selImpacto").change(function () {
    if ($("#selProbabilidad").val() != null) {
        ObtenerNivelRiesgoInherente($("#selProbabilidad").val(), $("#selImpacto").val(), false)
    }
});

function ObtenerNivelRiesgoInherente(nProbabilidad, nImpacto, bCarga = true) {
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ObtenerNivelRiesgoInherente',
        datos: { pnProbabilidad: nProbabilidad, pnImpacto: nImpacto },
        bloqueo: bCarga,
        mensaje: 'Calculando el nivel de riesgo inherente',
        terminado: function (data) {
            oNivelRiesgoInherente = JSON.parse(data);
            if (oNivelRiesgoInherente.oRiesgoInherente.nValorEscala > 0 && oNivelRiesgoInherente.oMontoPerdida != null) {
                $(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-bajo, i-nivel-riesgo-moderado, i-nivel-riesgo-alto, i-nivel-riesgo-extremo");
                if (oNivelRiesgoInherente.oRiesgoInherente.nValorEscala == 1) {
                    //$(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-moderado");
                    //$(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-alto");
                    //$(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-extremo");

                    $(".nivel-riesgo-inherente").addClass("i-nivel-riesgo-bajo");
                    $('#DescNivelRiesgoInherente').html('Bajo');
                } else if (oNivelRiesgoInherente.oRiesgoInherente.nValorEscala == 2) {
                    //$(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-bajo");
                    //$(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-alto");
                    //$(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-extremo");

                    $(".nivel-riesgo-inherente").addClass("i-nivel-riesgo-moderado");
                    $('#DescNivelRiesgoInherente').html('Moderado');
                } else if (oNivelRiesgoInherente.oRiesgoInherente.nValorEscala == 3) {
                    //$(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-bajo");
                    //$(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-moderado");
                    //$(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-extremo");

                    $(".nivel-riesgo-inherente").addClass("i-nivel-riesgo-alto");
                    $('#DescNivelRiesgoInherente').html('Alto');
                } else if (oNivelRiesgoInherente.oRiesgoInherente.nValorEscala == 4) {
                    //$(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-bajo");
                    //$(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-moderado");
                    //$(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-alto");

                    $(".nivel-riesgo-inherente").addClass("i-nivel-riesgo-extremo");
                    $('#DescNivelRiesgoInherente').html('Extremo');
                }

                //$('#DescMontoPerdida').html(numeral(oNivelRiesgoInherente.oMontoPerdida.cMontoPerdida).format("0,0[.]00"));
                $('#DescMontoPerdida').html(oNivelRiesgoInherente.oMontoPerdida.cMontoPerdida);

            }
        },
    });

}

$("#btnGrabaPaso2").click(function (e) {
    e.preventDefault();
    if (!ValidarCamposPaso2()) {
        $.fn.MensajeProcesos({
            posicion: 'A',
            mensaje: 'Es necesario completar la información de los campos remarcados',
            titulo: 'Gestión de ' + (gDatosRiesgo.nTpoRiesgo == 1 ? 'Riesgo Operacional' : 'Evaluación') + ' - Paso 2',
            tipo_mensaje: 'informacion'

        });
        $("#rootwizard").bootstrapWizard('show', 1);
        return false;
    }

    var lnProbabilidad = $("#selProbabilidad").val();
    var lnImpacto = $("#selImpacto").val();

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/GrabarProcesoPaso2',
        datos: { pnNroRiesgo: gDatosRiesgo.nNroRiesgo, pnProbabilidad: lnProbabilidad, pnImpacto: lnImpacto },
        terminado: function (data) {
            $.fn.MensajeProcesos({
                posicion: 'A',
                mensaje: data.MensajeSis,
                titulo: 'Nivel de Riesgo Inherente: ' + gDatosRiesgo.cCodRiesgo,
                tipo_mensaje: data.TpoMensaje,
                onhidden: function () {
                    if (data.TpoMensaje === 'exito' | 'informacion') {
                        $("#btnGrabaPaso2").remove();
                        //$("#btnGrabaPaso2").hide();
                        if (gDatosRiesgo.nCondicionTaller == 201) {
                            if (data.Observaciones == 0) {
                                if (gUsuario.cUser == gDatosRiesgo.oUsuarios.cUser) {
                                    $("#btnConfirmaObs").show();
                                }
                            }
                        } else {
                            $("#btnGrabaPaso3").show();
                            $("#rootwizard").bootstrapWizard('show', 2);
                        }
                    }
                }
            });
        },
    });

});

function ValidarCamposPaso2() {
    var validacion = true;

    validacion = $.fn.ValidarInput({ html: "#selProbabilidad", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selImpacto", isSelect: true });

    return validacion;
}

// #endregion

// #region Gestion Paso 3
$("#selRC").change(function () {
    CargarValorNivelRiesgo();
});

$("#selFD").change(function () {
    CargarValorNivelRiesgo();
});

$("#selED").change(function () {
    CargarValorNivelRiesgo();
});

$("#selTC").change(function () {
    CargarValorNivelRiesgo();
});

$("#selCO").change(function () {
    CargarValorNivelRiesgo();
});

function ComprobarSelectControl() {
    var comprobar = true;
    if ($("#selRC").val() == null) { comprobar = false; }
    if ($("#selFD").val() == null) { comprobar = false; }
    if ($("#selED").val() == null) { comprobar = false; }
    if ($("#selTC").val() == null) { comprobar = false; }
    if ($("#selCO").val() == null) { comprobar = false; }
    return comprobar;
}

function CargarValorNivelRiesgo() {
    if (ComprobarSelectControl()) {
        var nValorNivel = eval($("#selRC").val()) * eval($("#selFD").val()) * eval($("#selED").val()) * eval($("#selTC").val()) * eval($("#selCO").val());
        if (nValorNivel >= 0 && nValorNivel <= 18) {
            $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-moderado");
            $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-fuerte");

            $(".nivel-efectividad").addClass("i-nivel-efectividad-control-debil");
            $('#DescEfectividadControl').html('Efectividad del control: Débil');
            gnEfectividadControl = 1;
        } else if (nValorNivel >= 19 && nValorNivel <= 72) {
            $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-debil");
            $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-fuerte");

            $(".nivel-efectividad").addClass("i-nivel-efectividad-control-moderado");
            $('#DescEfectividadControl').html('Efectividad del control: Moderado');
            gnEfectividadControl = 2;
        } else if (nValorNivel >= 73 && nValorNivel <= 256) {
            $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-debil");
            $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-moderado");

            $(".nivel-efectividad").addClass("i-nivel-efectividad-control-fuerte");
            $('#DescEfectividadControl').html('Efectividad del control: Fuerte');
            gnEfectividadControl = 3;
        }
    }
}


function ValidarCamposControlRiesgoResidual() {
    var validacion = true;

    validacion = $.fn.ValidarInput({ html: "#inDescControl" });
    validacion = $.fn.ValidarInput({ html: "#selRC", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selFD", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selED", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selTC", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selCO", isSelect: true });

    return validacion;
}


$("#btnAddControlRiesgoResidual").click(function (e) {
    e.preventDefault();

    if (!ValidarCamposControlRiesgoResidual()) { return false; }

    var lsComenControl = $("#inDescControl").val();
    var lnResponDef = $("#selRC").val();
    var lnPeriEjec = $("#selFD").val();
    var lnEvidenControl = $("#selED").val();
    var lnTpoEjec = $("#selTC").val();
    var lnCumpleObj = $("#selCO").val();

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/RegistrarControlesRiesgoResidual',
        datos: {
            pnNroRiesgo: gDatosRiesgo.nNroRiesgo, __psComentario: lsComenControl, pnReponControl: lnResponDef, pnPeriEjec: lnPeriEjec, pnEvidenControl: lnEvidenControl, pnEjecControl: lnTpoEjec,
            pnCumpleObj: lnCumpleObj, pnEfecControl: gnEfectividadControl
        },
        //bloqueo: true,
        terminado: function (data) {
            oRiesgoResidual = JSON.parse(data);
            MostrarControlRiesgoResidual(oRiesgoResidual);
            $.fn.MensajeProcesos({
                clase: 'green',
                posicion: 'A',
                mensaje: 'Control añadido correctamente',
                titulo: 'Control ' + (gDatosRiesgo.nTpoRiesgo == 1 ? ('del riesgo ' + gDatosRiesgo.cCodRiesgo) : ('de la evaluación ' + gDatosRiesgo.cCodRiesgo)),
                tipo_mensaje: 'exito'
            });
            LimpiarModalAddControlRiesgoResidual();
        },
        //bloqueo: false
    });
});

$("#btnUpdControlRiesgoResidual").click(function (e) {
    e.preventDefault();

    if (!ValidarCamposControlRiesgoResidual()) { return false; }

    var lnItem = $("#tbl-controles-riesgo-residual tr.seleccionado").find("td").eq(2).html();
    var lsComenControl = $("#inDescControl").val();
    var lnResponDef = $("#selRC").val();
    var lnPeriEjec = $("#selFD").val();
    var lnEvidenControl = $("#selED").val();
    var lnTpoEjec = $("#selTC").val();
    var lnCumpleObj = $("#selCO").val();

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ActualizarControlesRiesgoResidual',
        datos: {
            pnNroRiesgo: gDatosRiesgo.nNroRiesgo, pnItem: lnItem, psComentario: lsComenControl, pnReponControl: lnResponDef, pnPeriEjec: lnPeriEjec, pnEvidenControl: lnEvidenControl, pnEjecControl: lnTpoEjec,
            pnCumpleObj: lnCumpleObj, pnEfecControl: gnEfectividadControl
        },
        //bloqueo: true,
        terminado: function (data) {
            datos = JSON.parse(data);
            MostrarControlRiesgoResidual(datos);
            $.fn.MensajeProcesos({
                clase: 'green',
                posicion: 'A',
                mensaje: 'Control actualizado correctamente',
                titulo: 'Control ' + (gDatosRiesgo.nTpoRiesgo == 1 ? ('del riesgo ' + gDatosRiesgo.cCodRiesgo) : ('de la evaluación ' + gDatosRiesgo.cCodRiesgo)),
                tipo_mensaje: 'exito'
            });
            LimpiarModalAddControlRiesgoResidual();
        },
        //bloqueo: false
    });
});

function MostrarControlRiesgoResidual(Modelo) {
    var $opciones = [
        {
            Columna: "Tipo1", id: "idEditarControlRR", claseIcono: "entypo-pencil", clase: "editarControlRR", titulo: "Editar", function: function (e) {
                // if ($(e).parent("li").hasClass("disabled")) { return false; }
                if (gDatosRiesgo.nProcRiesgo >= 3 && gDatosRiesgo.nEstadoRiesgo == 2) {
                    bootbox.alert({ message: "<strong>No se pudo realizar la acción debido que actualmente " + (gDatosRiesgo.nTpoRiesgo == 1 ? " el riesgo operacional " : " la evaluación ") + " se encuentra en gestión del proceso N° 4</strong>", size: "sm", closeButton: false });
                } else {
                    $("#btnAddControlRiesgoResidual").hide();
                    $("#btnUpdControlRiesgoResidual").show();
                    $("AddCriteriosEval .modal-body").removeClass(".validar-input");

                    var lnNroRiesgo = $(e).parents("tr").find("td").eq(1).html();
                    var lnItem = $(e).parents("tr").find("td").eq(2).html();

                    $.fn.Conexion({
                        direccion: '/RiesgoOperacional/ObtenerCriteriosEvalControlRiesgoResidual',
                        datos: { pnNroRiesgo: lnNroRiesgo, pnItem: lnItem },
                        terminado: function (data) {
                            datos = JSON.parse(data);
                            if (datos.oRiesgoResidual != null) {
                                $("#inDescControl").val(datos.oRiesgoResidual.cComentario);
                                $("#selRC").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: datos.oLstResponsableDef, dataselect: datos.oRiesgoResidual.nResponsableDef });
                                $("#selFD").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: datos.oLstFrecuenciaDef, dataselect: datos.oRiesgoResidual.nPeriodoEjecucion });
                                $("#selED").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: datos.oLstEvidenciaControl, dataselect: datos.oRiesgoResidual.nEvidenciaControl });
                                $("#selTC").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: datos.oLstTipoEjecucion, dataselect: datos.oRiesgoResidual.nEjecucionControl });
                                $("#selCO").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: datos.oLstCumpleObjetivo, dataselect: datos.oRiesgoResidual.nCumpleObjetivo });

                                CargarValorNivelRiesgo();
                                jQuery('#AddCriteriosEval').modal('show', { backdrop: 'static' });
                            }
                        },
                    });
                }
            }
        },
        {
            Columna: "Tipo2", id: "idEliminarControlRR", claseIcono: "entypo-trash", clase: "eliminarControlRR", titulo: "Eliminar", function: function (e) {
                //if ($(e).parent("li").hasClass("disabled")) { return false; }
                if (gDatosRiesgo.nProcRiesgo >= 3 && gDatosRiesgo.nEstadoRiesgo == 2) {
                    bootbox.alert({ message: "<strong>No se pudo realizar la acción debido a que actualmente " + (gDatosRiesgo.nTpoRiesgo == 1 ? "el riesgo operacional" : "la evaluación") + " se encuentra en gestión del proceso N° 4</strong>", size: "sm", closeButton: false });
                } else {
                    var lnNroRiesgo = $(e).parents("tr").find("td").eq(1).html();
                    var lnItem = $(e).parents("tr").find("td").eq(2).html();
                    bootbox.confirm({
                        message: '<strong>¿Está seguro de eliminar el control asignado al riesgo?</strong>',
                        closeButton: false,
                        size: 'sm',
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
                                if (gDatosRiesgo.nProcRiesgo)
                                $.fn.Conexion({
                                    direccion: '/RiesgoOperacional/EliminarControlRiesgoResidual',
                                    datos: { pnNroRiesgo: lnNroRiesgo, pnItem: lnItem },
                                    //bloqueo: true,
                                    terminado: function (data) {
                                        oRiesgoResidual = JSON.parse(data);
                                        MostrarControlRiesgoResidual(oRiesgoResidual);
                                        $.fn.MensajeProcesos({
                                            clase: 'green',
                                            posicion: 'A',
                                            mensaje: 'El control del riesgo residual fue eliminado correctamente.',
                                            titulo: 'Control Eliminado',
                                            tipo_mensaje: 'informacion'
                                        });

                                    },
                                    //bloqueo: false
                                });
                            }
                        }
                    });
                }
            }
        }
    ];

    if (gDatosRiesgo.nTpoRiesgo == 1) {
        if (gDatosRiesgo.nProcRiesgo == 5) {
            $opciones = null;
        }
    } else if (gDatosRiesgo.nTpoRiesgo == 2) {
        if (gDatosRiesgo.nCondicionTaller == 203) {
            if (gDatosRiesgo.oUsuarios.cUser != gUsuario.cUser) {
                $opciones = null;
            } else {
                if (gDatosRiesgo.nProcRiesgo == 3) {

                } else {

                    $opciones = null;
                }
            }
        } else if (gDatosRiesgo.nCondicionTaller == 201) {
            if (Modelo.lstObservacionesGestion.length > 0) {
                if (!(Modelo.lstObservacionesGestion.find(Observacion => Observacion.Proceso).Proceso == 3)) {
                    if (gDatosRiesgo.oUsuarios.cUser != gUsuario.cUser) {
                        $opciones = null;
                    } else {

                    }
                } else {
                    $opciones = null;
                }
            } else {
                $opciones = null;
            }
        } else if (gDatosRiesgo.nCondicionTaller == 200) {
            $opciones = null;
        } else {
            if (gDatosRiesgo.nEstadoRiesgo == 2 && gDatosRiesgo.nProcRiesgo >= 3) {
                $opciones = null;
            }
        }
    }

    $("#content-tbl-controles-riesgo-residual").Tabla({
        tblId: "tbl-controles-riesgo-residual",
        cabecera: "nNroRiesgo, nItem,<strong>Comentario</strong>,<strong>RC</strong>,<strong>FD</strong>,<strong>EC</strong>,<strong>TC</strong>,<strong>CO</strong>,<strong>Eficacia Control</strong>",
        campos: "oDatosRiesgo.nNroRiesgo, nItemControl,cComentario, cResponsableDef, cPeriodoEjecucion, cEvidenciaControl, cEjecucionControl, cCumpleObjetivo,cEfectividadControl",
        datos: Modelo.oLstRiesgoResidual,
        //cantRegVertical: 8,
        //scrollVertical: "Si",
        classtbl: "table table-responsive responsive",
        alineado: "C,C,L,C,C,C,C,C,C",
        formato: "0,0,0,0,0,0,0,0,0",
        controles: "0,0,4,0,0,0,0,0,0",
        visible: "0,0,1,1,1,1,1,1,1",
        anchocolumna: "0,0,30,8,8,8,8,8,10",
        sindata: "No se gestionó controles para el riesgo residual",
        numerado: "Si",
        ajustar: 'No',
        opciones: $opciones
    });


    if (Modelo.oLstRiesgoResidual != null) {
        var lnCalificacionEfectividadControl = Modelo.CalifEfecControl;
        var $Probabilidad = Modelo.oEscalaNivRiesgo.nProbabilidad;
        var lsProbabilidadEscala = Modelo.oEscalaNivRiesgo.cProbabilidad;
        var $Impacto = Modelo.oEscalaNivRiesgo.nImpacto;
        var lsImpactoEscala = Modelo.oEscalaNivRiesgo.cImpacto;
        //var lnValorEstacala = DevoverNivelRiesgoValorEscala($Probabilidad, $Impacto);
        var lnValorEstacala = Modelo.oEscalaNivRiesgo.nNivelRiesgo;

        if (lnCalificacionEfectividadControl == 1) {
            $(".calif-efect-control").removeClass("i-nivel-efectividad-control-moderado");
            $(".calif-efect-control").removeClass("i-nivel-efectividad-control-fuerte");

            $(".calif-efect-control").addClass("i-nivel-efectividad-control-debil");
            $('.cCalEfect .num').html('Débil');
        } else if (lnCalificacionEfectividadControl == 2) {
            $(".calif-efect-control").removeClass("i-nivel-efectividad-control-debil");
            $(".calif-efect-control").removeClass("i-efectividad-control-fuerte");

            $(".calif-efect-control").addClass("i-nivel-efectividad-control-moderado");
            $('.cCalEfect .num').html('Moderado');
        } else if (lnCalificacionEfectividadControl == 3) {
            $(".calif-efect-control").removeClass("i-nivel-efectividad-control-debil");
            $(".calif-efect-control").removeClass("i-nivel-efectividad-control-moderado");

            $(".calif-efect-control").addClass("i-nivel-efectividad-control-fuerte");
            $('.cCalEfect .num').html('Fuerte');
        }
        $('#DesProbRiesgoResidual').html(lsProbabilidadEscala);
        $('#DescImpacRiesgoResidual').html(lsImpactoEscala);

        if (lnValorEstacala == 1) {
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-moderado");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-alto");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-extremo");

            $(".nivel-riesgo-residual").addClass("i-nivel-riesgo-bajo");
            $(".cNivResidual .num").html("Bajo");
            $(".apetito-riesgo").addClass("i-nivel-riesgo-bajo");
            $(".cApetitoRiesgo .num").html("Aceptado");
        } else if (lnValorEstacala == 2) {
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-bajo");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-alto");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-extremo");

            $(".nivel-riesgo-residual").addClass("i-nivel-riesgo-moderado");
            $('.cNivResidual .num').html('Moderado');
            $(".apetito-riesgo").addClass("i-nivel-riesgo-bajo");
            $('.cApetitoRiesgo .num').html('Aceptado');
        } else if (lnValorEstacala == 3) {
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-bajo");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-moderado");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-extremo");

            $(".nivel-riesgo-residual").addClass("i-nivel-riesgo-alto");
            $('.cNivResidual .num').html('Alto');
            $(".apetito-riesgo").addClass("i-nivel-riesgo-extremo");
            $('.cApetitoRiesgo .num').html('Rechazar');
        } else if (lnValorEstacala == 4) {
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-bajo");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-moderado");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-alto");

            $(".nivel-riesgo-residual").addClass("i-nivel-riesgo-extremo");
            $(".apetito-riesgo").addClass("i-nivel-riesgo-extremo");
            $('.cNivResidual .num').html('Extremo');
            $('.cApetitoRiesgo .num').html('Rechazar');
        }
    } else {
        $('.cCalEfect .num').html('');
        $('#DesProbRiesgoResidual').html('');
        $('#DescImpacRiesgoResidual').html('');
        $('.cNivResidual .num').html('');
        $('.cApetitoRiesgo .num').html('');

        $(".apetito-riesgo").removeClass("i-nivel-riesgo-bajo");
        $(".apetito-riesgo").removeClass("i-nivel-riesgo-extremo");

        $(".calif-efect-control").removeClass("i-nivel-efectividad-control-debil");
        $(".calif-efect-control").removeClass("i-nivel-efectividad-control-fuerte");
        $(".calif-efect-control").removeClass("i-nivel-efectividad-control-moderado");

        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-bajo");
        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-moderado");
        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-alto");
        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-extremo");
    }
}

function LimpiarModalAddControlRiesgoResidual() {

    $("#AddCriteriosEval").modal('hide');
    $("#inDescControl").val("");

    //$("#selRC").selectBoxIt("selectOption", null);
    //$("#selRC").data('selectBox-selectBoxIt').refresh();
    //$("#selRC").removeClass("input-validator");
    //$("#selFD").selectBoxIt("selectOption", null);
    //$("#selFD").data('selectBox-selectBoxIt').refresh();
    //$("#selFD").removeClass("input-validator");
    //$("#selED").selectBoxIt("selectOption", null);
    //$("#selED").data('selectBox-selectBoxIt').refresh();
    //$("#selED").removeClass("input-validator");
    //$("#selTC").selectBoxIt("selectOption", null);
    //$("#selTC").data('selectBox-selectBoxIt').refresh();
    //$("#selTC").removeClass("input-validator");
    //$("#selCO").selectBoxIt("selectOption", null);
    //$("#selCO").data('selectBox-selectBoxIt').refresh();
    //$("#selCO").removeClass("input-validator");

    $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-debil");
    $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-fuerte");
    $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-moderado");

    $("#DescEfectividadControl").html("Eficacia del control");
}

$("#btnGrabaPaso3").click(function (e) {
    e.preventDefault();
    var lsComentarios = $("#inComentariosRiesgoresidual").val();
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/GrabarProcesoPaso3',
        datos: { pnNroRiesgo: gDatosRiesgo.nNroRiesgo, __psComentarios: lsComentarios },
        terminado: function (data) {
            $.fn.MensajeProcesos({
                posicion: 'A',
                mensaje: data.MensajeSis,
                titulo: 'Nivel de Riesgo Residual: ' + gDatosRiesgo.cCodRiesgo,
                tipo_mensaje: data.TpoMensaje,
                onhidden: function () {
                    if (data.TpoMensaje === 'exito' | 'informacion') {
                        //$("#btnGrabaPaso3").hide();
                        $("#btnGrabaPaso3").remove();
                        if (gDatosRiesgo.nCondicionTaller == 201) {
                            ObtenerObservacionesRiesgo();
                        } else {
                            $("#btnGrabaPaso4").show();
                            $("#rootwizard").bootstrapWizard("show", 3);
                        }
                    }
                }
            });
        },
    });
});

function ValidarCamposPaso3() {

    var validacion = true;
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ControlesRiesgoResidual',
        datos: { pnNroRiesgo: gDatosRiesgo.nNroRiesgo },
        async: false,
        terminado: function (data) {
            datos = JSON.parse(data);

            if (datos.oLstRiesgoResidual === null) {
                //return false;
                validacion = false;
            }
        },
    });
    return validacion;
}

// #endregion

// #region Paso 4
function LimparCamposAddPlanAccion() {
    $("#inDescPlanAccion").val('');
    $("#inFechaImplementacion").val('');
    $("#chkSugeridoGM").is(':checked');
    $("#inComentPlanAcccion").val('');

    $("#btnCerrarAddPlanAccion").click();
}

function ValidarCamposPaso4() {
    var validacion = true;
    var lnFilas = $("#tbl-plan-accion .no-data").is(":visible");
    if (lnFilas) {
        validacion = false;
    }
    return validacion;
}

$("#btnGrabaPaso4").click(function (e) {
    e.preventDefault();
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/GrabarProcesoPaso4',
        datos: { pnNroRiesgo: gDatosRiesgo.nNroRiesgo },
        terminado: function (data) {
            $.fn.MensajeProcesos({
                posicion: 'A',
                mensaje: data.MensajeSis,
                titulo: 'Plan de Acción: ' + gDatosRiesgo.cCodRiesgo,
                tipo_mensaje: data.TpoMensaje,
                onhidden: function () {
                    if (data.TpoMensaje === 'exito' | 'informacion') {
                        if (gDatosRiesgo.nTpoRiesgo == 1) {
                            $("#btnGrabaPaso4").remove(); //observar esta opcion 
                            $("#btnGrabaPaso5").show();
                            $("#rootwizard").bootstrapWizard('show', 4);
                        } else if (gDatosRiesgo.nTpoRiesgo == 2) {
                            $("#btnGrabaPaso4").remove();
                            if (gDatosRiesgo.nCondicionTaller == 201) {
                                ObtenerObservacionesRiesgo();
                            } else {
                                /*Verifico si el usuario aun tiene evaluaciones pendiente de gestion*/
                                $.fn.Conexion({
                                    direccion: "/Evaluacion/ListaEvaluacionesEnProceso",
                                    datos: { pnTpoBuscar: 0, psValorBuscar: '' },
                                    mensaje: " Obteniendo las evaluaiones en proceso de gestión",
                                    terminado: function (data) {
                                        datos = JSON.parse(data);
                                        if (datos.oLstDatosEvaluaciones.length == 0) {
                                            $.fn.Conexion({
                                                direccion: '/RiesgoOperacional/DevolverVista',
                                                datos: { Vista: "Notificar", Controlador: "Evaluacion" },
                                                terminado: function (data) {
                                                    window.location = data;
                                                }
                                            });
                                        } else {
                                            $.fn.Conexion({
                                                direccion: '/RiesgoOperacional/DevolverVista',
                                                datos: { Vista: "Lista", Controlador: "Evaluacion" },
                                                terminado: function (data) {
                                                    window.location = data;
                                                }
                                            });
                                        }
                                    },
                                });
                            }
                        }
                    } else {
                        $("#rootwizard").bootstrapWizard('show', 3);
                    }
                }
            });
        },
    });
});


$("#btnAddPlanAccion").click(function (e) {
    e.preventDefault();
    //if (gDatosRiesgo.nProcRiesgo != 3) {
    //    $.fn.MensajeProcesos({
    //        posicion: 'B',
    //        mensaje: 'La acción no esta permitida.',
    //        titulo: 'Mensaje del Sistema',
    //        tipo_mensaje: 'advertencia'
    //    });
    //    return false;
    //}

    if (!ValidarCamposPlanAccion()) { return false; }

    var lsDesPlanAccion = $("#inDescPlanAccion").val();
    var lsFechaImple = $("#inFechaImplementacion").val();
    var lbSugerenciaGM = $("#chkSugeridoGM").is(':checked') ? 1 : 0;
    var lnDocumento = $("#FileAttach tbody tr .tdTmpFile").length;//$("#inArchivoAdjunto").val();
    var lsComentPlan = $("#inComentPlanAcccion").val();
    var loArchivo = gARCHIVOS.getData();
    var dialog;
    if (lnDocumento > 0) {
        $.ajax({
            url: '/RiesgoOperacional/AdjuntarDocumento',
            type: 'POST',
            data: loArchivo,
            beforeSend: function () {
                dialog = bootbox.dialog({
                    message: '<p class="text-center mb-0"><i class="fa fa-spin fa-spinner"></i> Subiendo archivo al servidor...</p>',
                    closeButton: false,
                    size: "sm"
                });
            },
            success: function (data) {
                try {
                    var oDocAdj = JSON.parse(data);
                    $.fn.Conexion({
                        direccion: '/RiesgoOperacional/GrabarPlanAccion',
                        datos: {
                            pnNroRiesgo: gDatosRiesgo.nNroRiesgo, __psDescPlanAccion: lsDesPlanAccion, pdFechaImplement: lsFechaImple,
                            pnSugerenciaGM: lbSugerenciaGM, __psComentarios: lsComentPlan, psNombreDoc: oDocAdj.lsNombreArchivo,
                            psNombreDocBD: oDocAdj.lsNombreArchivoDB
                        },
                        terminado: function (data) {
                            oPlanesAccion = JSON.parse(data);
                            MostrarPlanesAccion(oPlanesAccion);
                            dialog.modal('hide');
                            LimparCamposAddPlanAccion();
                        },
                    });
                } catch (ex) { dialog.modal('hide'); }
            },
            error: function () {
                dialog.modal('hide');

                $.fn.MensajeProcesos({
                    posicion: 'B',
                    mensaje: 'Error al intentar subir el documento al servidor.',
                    titulo: 'Mensaje del Sistema',
                    tipo_mensaje: 'Error'
                });
            },
            cache: false,
            contentType: false,
            processData: false
        });
    } else {
        $.fn.Conexion({
            direccion: '/RiesgoOperacional/GrabarPlanAccion',
            datos: { pnNroRiesgo: gDatosRiesgo.nNroRiesgo, __psDescPlanAccion: lsDesPlanAccion, pdFechaImplement: lsFechaImple, pnSugerenciaGM: lbSugerenciaGM, __psComentarios: lsComentPlan },
            bloqueo: true,
            terminado: function (data) {
                oPlanesAccion = JSON.parse(data);
                MostrarPlanesAccion(oPlanesAccion);
                LimparCamposAddPlanAccion();
            },
            bloqueo: false
        });
    }
});

$("#btnUpdPlanAccion").click(function (e) {
    e.preventDefault();
    if (!ValidarCamposPlanAccion()) { return false; }
    var lnCodPlan = $("#cod-plan-accion").text();
    var lsDesPlanAccion = $("#inDescPlanAccion").val();
    var lsFechaImple = $("#inFechaImplementacion").val();
    var lbSugerenciaGM = $("#chkSugeridoGM").is(':checked') ? 1 : 0;
    var lnDocumento = $("#FileAttach tbody tr .tdTmpFile").length;
    var lsComentPlan = $("#inComentPlanAcccion").val();
    var loArchivo = gARCHIVOS.getData();
    var dialog;
    if (lnDocumento > 0) {
        $.ajax({
            url: '/RiesgoOperacional/AdjuntarDocumento',
            type: 'POST',
            data: loArchivo,
            beforeSend: function () {
                dialog = bootbox.dialog({
                    message: '<p class="text-center mb-0"><i class="fa fa-spin fa-spinner"></i> Subiendo archivo al servidor...</p>',
                    closeButton: false,
                    size: "sm"
                });
            },
            success: function (data) {
                try {
                    var oDocAdj = JSON.parse(data);
                    $.fn.Conexion({
                        direccion: '/RiesgoOperacional/ActualizarPlanAccion',
                        datos: {
                            pnNroRiesgo: gDatosRiesgo.nNroRiesgo, pnCodPlan: lnCodPlan, psDescPlanAccion: lsDesPlanAccion, pdFechaImplement: lsFechaImple,
                            pnSugerenciaGM: lbSugerenciaGM, psComentarios: lsComentPlan, psNombreDoc: oDocAdj.lsNombreArchivo,
                            psNombreDocBD: oDocAdj.lsNombreArchivoDB
                        },
                        terminado: function (data) {
                            oPlanesAccion = JSON.parse(data);
                            MostrarPlanesAccion(oPlanesAccion);
                            dialog.modal('hide');
                            LimparCamposAddPlanAccion();
                        },
                    });
                } catch (ex) { dialog.modal('hide'); }
            },
            error: function () {
                dialog.modal('hide');

                $.fn.MensajeProcesos({
                    posicion: 'B',
                    mensaje: 'Error al intentar subir el documento al servidor.',
                    titulo: 'Mensaje del Sistema',
                    tipo_mensaje: 'Error'
                });
            },
            cache: false,
            contentType: false,
            processData: false
        });
    } else {
        $.fn.Conexion({
            direccion: '/RiesgoOperacional/ActualizarPlanAccion',
            datos: { pnNroRiesgo: gDatosRiesgo.nNroRiesgo, pnCodPlan: lnCodPlan, psDescPlanAccion: lsDesPlanAccion, pdFechImplement: lsFechaImple, pnSugerenciaGM: lbSugerenciaGM, psComentarios: lsComentPlan },
            //bloqueo: true,
            terminado: function (data) {
                oPlanesAccion = JSON.parse(data);
                MostrarPlanesAccion(oPlanesAccion);
                LimparCamposAddPlanAccion();
            },
            //bloqueo: false
        });
    }
});


function MostrarPlanesAccion(oPlanesAccion) {
    var $opciones = [
        {
            Columna: "Tipo1", id: "id-editar-plan-accion", claseIcono: "entypo-pencil", clase: "editar-plan-accion", titulo: "Editar", function: function (e) {
                //if ($(e).parent("li").hasClass("disabled")) { return false; }

                $("#btnAddPlanAccion").hide();
                $("#btnUpdPlanAccion").show();

                var lnNroRiesgo = $(e).parents("tr").find("td").eq(1).html();
                var lnCodPlan = $(e).parents("tr").find("td").eq(2).html();

                $.fn.Conexion({
                    direccion: '/RiesgoOperacional/ObtenerPlanAccionRiesgoPlan',
                    datos: { pnNroRiesgo: lnNroRiesgo, pnCodPlanAccion: lnCodPlan },
                    terminado: function (data) {
                        oPlanAccion = JSON.parse(data);
                        if (oPlanAccion.oPlanAccion != null) {
                            $("#cod-plan-accion").html(":" + lnCodPlan);
                            //$("data-cod-plan").data(lnCodPlan);
                            $("#inDescPlanAccion").val(oPlanAccion.oPlanAccion.cPlanDescripcion);
                            $("#inFechaImplementacion").val(oPlanAccion.oPlanAccion.dFechaImplement);
                            $("#inComentPlanAcccion").val(oPlanAccion.oPlanAccion.cComentario);
                            $("#inComentPlanAcccion").val(oPlanAccion.oPlanAccion.cComentario);
                            //$('#chkSugeridoGM').bootstrapSwitch('animate', oPlanAccion.oPlanAccion.bSugerenciaGM);
                            //checked data-animate="false"
                            $("#chkSugeridoGM").attr("checked", oPlanAccion.oPlanAccion.bSugerenciaGM);
                            //if (oPlanAccion.oPlanAccion.bSugerenciaGM == true) {
                            //    //$('.sugerencia').on('switch-change', function () {
                            //    //    $('.sugerencia').bootstrapSwitch('toggleRadioStateAllowUncheck', true);
                            //    //});
                            //    /*$('#chkSugeridoGM').bootstrapSwitch('toggleRadioStateAllowUncheck');*/
                            //} else { /*$('#chkSugeridoGM').bootstrapSwitch('setActive', false); */ }
                            $("#AddPlanesAccion").modal("show");
                        }
                    },
                });
            }

        },
        {
            Columna: "Tipo2", id: "id-eliminar-plan-accion", claseIcono: "entypo-trash", clase: "eliminar-plan-accion", titulo: "Eliminar", function: function (e) {
                // if ($(e).parent("li").hasClass("disabled")) { return false; }

                var lnNroRiesgo = $(e).parents("tr").find("td").eq(1).html();
                var lnPlanCod = $(e).parents("tr").find("td").eq(2).html();

                bootbox.confirm({
                    message: "¿Está seguro de eliminar el plan de acción con código <strong>" + lnPlanCod + "</strong>?",
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
                                direccion: '/RiesgoOperacional/EliminarPlanAccion',
                                datos: { pnNroRiesgo: lnNroRiesgo, pnPlanCod: lnPlanCod },
                                terminado: function (data) {
                                    oPlanesAccion = JSON.parse(data);
                                    MostrarPlanesAccion(oPlanesAccion);
                                    $.fn.MensajeProcesos({
                                        posicion: 'A',
                                        mensaje: 'Se eliminó el plan de acción.',
                                        titulo: 'Plan de Acción',
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

    if (gDatosRiesgo.nTpoRiesgo == 1) {
        if (gDatosRiesgo.nProcRiesgo == 5) {
            $opciones = null;
        }
    } else if (gDatosRiesgo.nTpoRiesgo == 2) {
        if (gDatosRiesgo.nCondicionTaller == 201) {
            if (oPlanesAccion.lstObservacionesGestion.length > 0) {
                //if (oPlanesAccion.lstObservacionesGestion.find(Observacion => Observacion.Proceso == 4).Proceso == 4) {
                if (!(oPlanesAccion.lstObservacionesGestion.find(Observacion => Observacion.Proceso).Proceso === 4)) {
                    if (gUsuario.cRHCargoCod == '005011') {
                        $opciones = null;
                    }
                } else {
                    $opciones = null;
                    //$("#idEditarPlanAccion, #idEliminarPlanAccion").addClass("disabled");
                }
            } else {
                $opciones = null;
                //$("#idEditarPlanAccion, #idEliminarPlanAccion").addClass("disabled");
            }

        } else if (gDatosRiesgo.nCondicionTaller == 203) {
            if (gDatosRiesgo.oUsuarios.cUser != gUsuario.cUser) {

                //$("#idEditarPlanAccion, #idEliminarPlanAccion").addClass("disabled");
                $opciones = null;
            } else {
                if (gDatosRiesgo.nProcRiesgo == 3) {

                    //$("#idEditarPlanAccion, #idEliminarPlanAccion").removeClass("disabled");
                } else {

                    //$("#idEditarPlanAccion, #idEliminarPlanAccion").addClass("disabled");
                    $opciones = null;
                }
            }
        } else if (gDatosRiesgo.nCondicionTaller == 201) {
            if (Modelo.lstObservacionesGestion.length > 0) {
                if (Modelo.lstObservacionesGestion.find(Observacion => Observacion.Proceso == 4).Proceso == 3) {


                    //$("#idEditarPlanAccion, #idEliminarPlanAccion").removeClass("disabled");
                } else {

                    //$("#idEditarPlanAccion, #idEliminarPlanAccion").addClass("disabled");
                    $opciones = null;
                }
            } else {

                //$("#idEditarPlanAccion, #idEliminarPlanAccion").addClass("disabled");
                $opciones = null;
            }
        } else if (gDatosRiesgo.nCondicionTaller == 200) {

            //$("#idEditarPlanAccion, #idEliminarPlanAccion").addClass("disabled");
            $opciones = null;
        }
    }

    $("#content-tbl-plan-accion").Tabla({
        tblId: "tbl-plan-accion",
        cabecera: "nNroRiesgo,nPlanCod,Código,<strong>Descripción del plan de acción</strong>,<strong>Estado</strong>,<strong>Fecha Registo</strong>, <strong>Doc. Adjunto</strong>",
        campos: "oDatosRiesgo.nNroRiesgo,nPlanCod,cPlanCod,cPlanDescripcion,cEstado,dFechaRegistro,cNombreDoc",
        datos: oPlanesAccion.oLstPlanAccion,
        classtbl: "table table-responsive responsive",
        //cantRegVertical: 8,
        //scrollVertical: "Si",
        alineado: "C,C,C,L,C,C,L",
        formato: "0,0,0,0,0,3,0",
        controles: "0,0,5,4,0,0,5",
        visible: "0,0,1,1,1,1,1",
        anchocolumna: "0,0,10,30,15,10,15",
        sindata: "No se encontró información de planes de acción",
        numerado: "Si",
        ajustar: 'No',
        opciones: $opciones
    });



}

//$(document).on("click", ".VerDocAdjunto", function (e) {

//    //jQuery('#AdjuntarDoc').modal('show', { backdrop: 'static' });
//});

//$(document).on("click", ".editarPlanAccion", function (e) {
//    $("#btnAddPlanAccion").hide();
//    $("#btnUpdPlanAccion").show();

//    var lnNroRiesgo = $(this).parents("tr").find("td").eq(1).html();
//    var lnCodPlan = $(this).parents("tr").find("td").eq(2).html();

//    $.fn.Conexion({
//        direccion: '/RiesgoOperacional/ObtenerPlanAccionRiesgoPlan',
//        datos: { pnNroRiesgo: lnNroRiesgo, pnCodPlanAccion: lnCodPlan },
//        bloqueo: true,
//        terminado: function (data) {
//            oPlanAccion = JSON.parse(data);

//            if (oPlanAccion.oPlanAccion != null) {
//                $("#inDescPlanAccion").val(oPlanAccion.oPlanAccion.cPlanDescripcion);
//                $("#inFechaImplementacion").val(oPlanAccion.oPlanAccion.dFechaImplement);
//                $("#inComentPlanAcccion").val(oPlanAccion.oPlanAccion.cComentario);
//                //if (oPlanAccion.oPlanAccion.bSugerenciaGM == true) {
//                //    //$('.sugerencia').on('switch-change', function () {
//                //    //    $('.sugerencia').bootstrapSwitch('toggleRadioStateAllowUncheck', true);
//                //    //});
//                //    /*$('#chkSugeridoGM').bootstrapSwitch('toggleRadioStateAllowUncheck');*/
//                //} else { /*$('#chkSugeridoGM').bootstrapSwitch('setActive', false); */ }

//                jQuery('#AddPlanesAccion').modal('show', { backdrop: 'static' });
//            }
//        },
//        bloqueo: false
//    });
//});

//$(document).on("click", ".eliminarPlanAccion", function (e) {
//    var lnNroRiesgo = $(this).parents("tr").find("td").eq(1).html();
//    var lnPlanCod = $(this).parents("tr").find("td").eq(2).html();

//    bootbox.confirm({
//        message: "¿Está seguro de eliminar el plan de acción con código <strong>" + lnPlanCod + "</strong>?",
//        size: "sm",
//        closeButton: false,
//        buttons: {
//            confirm: {
//                label: 'SI',
//                className: 'btn-primary'
//            },
//            cancel: {
//                label: 'NO',
//                className: 'btn-default'
//            }
//        },
//        callback: function (result) {
//            if (result) {
//                $.fn.Conexion({
//                    direccion: '/RiesgoOperacional/EliminarPlanAccion',
//                    datos: { pnNroRiesgo: lnNroRiesgo, pnPlanCod: lnPlanCod },
//                    //bloqueo: true,
//                    terminado: function (data) {
//                        oPlanesAccion = JSON.parse(data);
//                        MostrarPlanesAccion(oPlanesAccion);
//                        $.fn.MensajeProcesos({
//                            posicion: 'A',
//                            mensaje: 'Se eliminó el plan de acción.',
//                            titulo: 'Plan de Acción',
//                            tipo_mensaje: 'informacion'
//                        });

//                    },
//                    //bloqueo: false
//                });
//            }
//        }
//    });

//});

function ValidarCamposPlanAccion() {
    var validacion = true;
    validacion = $.fn.ValidarInput({ html: "#inDescPlanAccion" });
    validacion = $.fn.ValidarInput({ html: "#inFechaImplementacion" });
    /*Validacionn de la */
    //if (!ValidarFormatoFecha($("inFechaImplementacion").val())) {
    //    $.fn.MensajeProcesos({
    //        posicion: 'A',
    //        mensaje: 'La fecha ingresada no es valida.',
    //        titulo: 'Fecha de Implementación',
    //        tipo_mensaje: 'informacion'
    //    });

    //    validacion = false;
    //}
    return validacion;
}


// #endregion

// #region Paso 5
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

function MostrarResponsablePlanesAccion(oPlanesAccion) {
    $opciones = [{
        Columna: "Tipo1", id: "id-respon-plan-accion", claseIcono: "entypo-user", clase: "ResponPlanAccion", titulo: "Gestionar", function: function (e) {
            var lnNroRiesgo = $(e).parents("tr").find("td").eq(1).html();
            var lnCodPlan = $(e).parents("tr").find("td").eq(2).html();
            $.fn.Conexion({
                direccion: '/RiesgoOperacional/MostarResponsablePlanAccion',
                datos: { pnNroRiesgo: lnNroRiesgo, pnCodPlanAccion: lnCodPlan },
                terminado: function (data) {
                    oResponPlanAccion = JSON.parse(data);
                    if (oResponPlanAccion.oLstReponPlanAccion != null) {
                        MostrarReponsablePlanAccion(oResponPlanAccion);
                        $("#DetResponsablesPlanesAccion").modal("show");
                    }
                },
            });
        }
    },
    {
        Columna: "Tipo2", id: "id-gestion-plan-accion", claseIcono: "entypo-dot-3", clase: "GestionPlanAccion", titulo: "Gestionar Plan Acción", function: function (e) {
            var lnCodPlan = $(e).parents("tr").find("td").eq(2).html();
            $.fn.Conexion({
                direccion: '/RiesgoOperacional/MostarResponsablePlanAccion',
                datos: { pnNroRiesgo: gDatosRiesgo.nNroRiesgo, pnCodPlanAccion: lnCodPlan },
                terminado: function (data) {
                    datos = JSON.parse(data);
                    if (datos.oLstReponPlanAccion.length > 0) {
                        var ObjParam = { evaluacion: base64_encode(gDatosRiesgo.nNroRiesgo), planaccion: lnCodPlan };
                        $.fn.Conexion({
                            direccion: '/RiesgoOperacional/DevolverVistaParam',
                            datos: { Vista: "PlanAccion", Controlador: "Evaluacion", Parametros: JSON.stringify(ObjParam) },
                            terminado: function (data) {
                                window.location = data.url;
                            },
                        });
                    } else {
                        $.fn.MensajeProcesos({
                            posicion: 'A',
                            mensaje: 'El plan de acción ' + lnCodPlan + ' no puede ser gestionado, no cuenta con responsables asignados.',
                            titulo: 'Gestión Plan Acción',
                            tipo_mensaje: 'informacion'
                        });
                    }
                },
            });

        }
    }
    ];
    if (gDatosRiesgo.nTpoRiesgo == 1) {
        $opciones.splice(1, 1);
        if (gDatosRiesgo.nProcRiesgo == 5) {
            $opciones = null;
        }

    } else if (gDatosRiesgo.nTpoRiesgo == 2) {
        if (gDatosRiesgo.nEstadoRiesgo == 4 && gDatosRiesgo.nProcRiesgo == 4) {
            $opciones.splice(1, 1);
        }
        else if (gDatosRiesgo.nEstadoRiesgo == 5 && gDatosRiesgo.nProcRiesgo == 5) {
            //$opciones = null;
            $opciones.splice(0, 1);
        }
    }

    $("#content-tbl-responsable-plan-accion").Tabla({
        tblId: "tbl-responsable-plan-accion",
        cabecera: "nNroRiesgo,<strong>Cód. Plan</strong>,<strong>Descripción del Plan</strong>,<strong>Responsable(s) Asignado</strong>",
        campos: "oDatosRiesgo.nNroRiesgo,oPlanAccion.nPlanCod,oPlanAccion.cPlanDescripcion,cUserResponsable",
        datos: oPlanesAccion.oLstReponPlanAccionRiesgo,
        //cargando: true,
        //cantRegVertical: 8,
        classtbl: "table table-responsive responsive",
        alineado: "C,C,L,L",
        formato: "0,0,0,0",
        controles: "0,0,4,4",
        visible: "0,1,1,1",
        anchocolumna: '0,10,40,30',
        numerado: "Si",
        ajustar: 'No',
        opciones: $opciones
    });
}

function MostrarReponsablePlanAccion(LstResponsablePlan) {
    $("#tbl-add-responsables-planes-accion").Tabla({
        tblId: "tbl-add-responsables-planes-accion",
        cabecera: "nNroRiesgo,nPlanCod,nItemRespon,<strong>Usuario</strong>,<strong>Nombre</strong>",
        campos: "oDatosRiesgo.nNroRiesgo,oPlanAccion.nPlanCod,nItemResponPlan,cUserResponsable,oPersona.cPersNombre",
        datos: LstResponsablePlan.oLstReponPlanAccion,
        cantRegVertical: 4,
        classtbl: "table table-responsive no-margin",
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
                        closeButton: false,
                        size: "sm",
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

function ValidarCamposResponsablePlanAccion() {

    var validacion = true;
    var seleccion = $("#tbl-responsable-plan-accion tr.seleccionado").index();

    validacion = $.fn.ValidarInput({ html: "#selAgenciasRespon", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selAreaRespon", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selUserResponsablePlan", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selAgenciasRespon", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selAgenciasRespon", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#selAgenciasRespon", isSelect: true });

    if (seleccion == -1) {
        $.fn.MensajeProcesos({
            //clase: 'green',
            posicion: 'A',
            mensaje: 'Por favor, seleccione el <strong>Plan de Acción</strong> para la asignación del responsable.',
            titulo: 'Accion no válida',
            tipo_mensaje: 'informacion'
        });
        //bootbox.alert({ message: "<strong>Seleccione el plan de acción para la asignación del usuario</strong>", size: "sm", closeButton: false });
        validacion = false;
    }
    return validacion;
}

$("#btnGrabaPaso5").click(function (e) {
    e.preventDefault();

    bootbox.confirm({
        title: "<h4><strong>Asignación de responsable</strong></h4>",
        message: "<p class='text-info'>Los respondables de los planes acción serán los únicos encargados de dar respuestas a los planes de acción al que fue asignado.</p><br/>" + (gDatosRiesgo.nTpoRiesgo == 1 ? "Al autorizar la asignación previamente configurado, se informará sobre la asignación al usuario correspondiente. <br/><br/><strong>¿Está seguro de grabar los datos?</strong>" : "Se marcará la evalución como verificado. <strong>¿Está seguro de grabar los datos?</strong>"),
        //classextra: true,
        closeButton: false,
        size: 'sm',
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
                    direccion: '/RiesgoOperacional/GrabarProcesoPaso5',
                    datos: { pnNroRiesgo: gDatosRiesgo.nNroRiesgo },
                    bloqueo: true,
                    mensaje: gDatosRiesgo.nTpoRiesgo == 1 ? 'Informando sobre los planes de acción a los responsables asignados. Por favor, se pasciente.' : 'Grabando la gestión de la evaluación.',
                    terminado: function (datos) {
                        $.fn.MensajeProcesos({
                            posicion: 'A',
                            mensaje: datos.Mensaje,
                            titulo: 'Definición de Responsables: ' + gDatosRiesgo.cCodRiesgo,
                            tipo_mensaje: datos.ExitoGestion > 0 ? 'exito' : 'advertencia',
                            onhidden: function () {
                                if (datos.ExitoGestion > 0) {
                                    if (gDatosRiesgo.nTpoRiesgo == 1) {
                                        $.fn.Conexion({
                                            direccion: '/RiesgoOperacional/DevolverVista',
                                            datos: { Vista: "PlanesAccion", Controlador: "RiesgoOperacional" },
                                            terminado: function (data) {
                                                window.location = data;
                                            }
                                        });
                                    } else if (gDatosRiesgo.nTpoRiesgo == 2) {
                                        $.fn.Conexion({
                                            direccion: '/RiesgoOperacional/DevolverVista',
                                            datos: { Vista: "MonitorearEval", Controlador: "Evaluacion" },
                                            terminado: function (data) {
                                                window.location = data;
                                            }
                                        });
                                    }
                                }


                            }
                        });
                    },
                });
            }
        }
    });

});



$("#btnObservaciones").click(function (e) {
    e.preventDefault();
    var lsObservaciones = "";

    lsObservaciones += $("#inObserva_1").val().trim();
    lsObservaciones += $("#inObserva_2").val().trim();
    lsObservaciones += $("#inObserva_3").val().trim();
    lsObservaciones += $("#inObserva_4").val().trim();

    if (lsObservaciones == "") {
        $.fn.MensajeProcesos({
            clase: 'green',
            posicion: 'A',
            mensaje: 'No se ha detectado observaciones en ningún proceso',
            titulo: 'Emitir Observaciones',
            tipo_mensaje: 'informacion',

        });

        return false;
    } else {
        var loObservaciones = [];
        loObservaciones[0] = { nProceso: 1, cObservacion: $("#inObserva_1").val().trim() };
        loObservaciones[1] = { nProceso: 2, cObservacion: $("#inObserva_2").val().trim() };
        loObservaciones[2] = { nProceso: 3, cObservacion: $("#inObserva_3").val().trim() };
        loObservaciones[3] = { nProceso: 4, cObservacion: $("#inObserva_4").val().trim() };

        $.fn.Conexion({
            direccion: '/RiesgoOperacional/GrabarObservacionesGestion',
            datos: { pnNroRiesgo: gDatosRiesgo.nNroRiesgo, poObservaciones: JSON.stringify(loObservaciones) },
            bloqueo: true,
            mensaje: 'Emitiendo las observaciones <small></small>',
            terminado: function (datos) {
                $.fn.MensajeProcesos({
                    posicion: 'A',
                    mensaje: datos.Mensaje,
                    titulo: 'Observacioes - ' + (gDatosRiesgo.nTpoRiesgo == 1 ? 'Riesgo Operacional' : 'Evaluación'),
                    tipo_mensaje: datos.ExitoGestion > 0 ? 'exito' : 'informacion',
                    onhidden: function () {
                        $.fn.Conexion({
                            direccion: '/RiesgoOperacional/DevolverVista',
                            datos: { Vista: "MonitorearEval", Controlador: "Evaluacion" },
                            terminado: function (data) {
                                window.location = data;
                            }
                        });
                    }
                });
            },
        });


    }



});



$("#btnConfirmaObs").click(function (e) {
    //$(this).prop("disabled", true);
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/NotificarModificarRiesgo',
        datos: { pnNroRiesgo: gDatosRiesgo.nNroRiesgo },
        bloqueo: true,
        mensaje: 'Enviando la notificación de modificación',
        terminado: function (data) {
            $.fn.MensajeProcesos({
                posicion: 'A',
                mensaje: 'Se realizó la confirmación de modificación ' + (gDatosRiesgo.nTpoRiesgo == 1 ? 'del riesgo operacional' : 'de la evaluación') + "<br/>Se emitio el correo de confirmación al analista de riesgo operacional",
                titulo: 'Confirmación de modificación',
                tipo_mensaje: 'exito',
                onhidden: function () {
                    if (data.Exito > 0) {
                        $('#btnConfirmaObs').hide();
                    }
                }
            });
        },
    });
});






// #endregion


function MostrarPanelInfo(paso) {
    switch (paso) {
        case 3:
            var opciones = {
                $mensaje: '<div class="sidebar"><h4><i class= "entypo-info-circled"></i>Información</h4>' +
                    '<div class="sidebar-content">' +
                    '<ul>' +
                    '<li><a href="#"><strong>Responsable Ejecución (RC):</strong></a> Indica si el control tiene responsable.</li>' +
                    '<li><a href="#"><strong>Periodo de Ejecución (FD):</strong></a> Indica el periodo de en que se ejecuta el control</li>' +
                    '<li><a href="#"><strong>Evidencia de Control (EC):</strong></a> Indica la presencia del control</li>' +
                    '<li><a href="#"><strong>Ejecución del Control (TC):</strong></a> Indica el tipo de ejecución del control</li>' +
                    '<li><a href="#"><strong>Cumplimiento Objetivo (CO):</strong></a> Indica si el control cumple con su objetivo</li>' +
                    '</ul></div>'
            }
            bootbox.dialog({
                message: opciones.$mensaje,
            });
            break;
        case 4:
            var opciones = {
                $mensaje: '<div class="sidebar"><h4><i class= "entypo-info-circled"></i>Información</h4>' +
                    '<div class="sidebar-content">' +
                    '<ul>' +
                    '<li><a href="#"><strong>Sugerido (S):</strong></a> Indica que el control implementado, fue sugerencia de la gerencia mancomunada.</li>' +
                    '<li><a href="#"><strong>No sugerido (N):</strong></a> Indica que el control implementado no fue sugerencia de la gerencia mancomunada.</li>' +
                    '</ul></div>'
            }
            bootbox.dialog({
                message: opciones.$mensaje
            });
            break;

    }
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

