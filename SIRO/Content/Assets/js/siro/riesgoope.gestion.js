var goDatosRiesgoOpe = new Array(); //Contiene los datos detallados del riesgo
var gnEfectividadControl = 0;
var lArchivo = null;
var gnNroRiesgo = gDatosRiego.nNroRiesgo;
var gnProcRiesgo = gDatosRiego.nProcRiesgo;


$(document).ready(function () {
    $('#rootwizard').bootstrapWizard({
        'tabClass': '',
        'onShow': null,
        'onInit': null,
        'onNext': null,
        'onPrevious': null,
        'onLast': null,
        'onLast': null,
        'onTabChange': function (tab, navigation, index) {

        },
        'onTabClick': function (tab, navigation, index) {
            var lntab = index + 1;
            if (!ValidacionControlesPasosGestion()) {
                //aqui haremos quepregunet si va guardar al intentar pasar  
                return false;
            } else { }

            //return false;

        },
        'onTabShow': function (tab, navigation, index) {
            var total = navigation.find('li').length;
            var lntab = index + 1;
            var porcentaje = (lntab / total) * 100;

            jQuery('li', $('#rootwizard')).removeClass("completed");
            //jQuery('li', $('#rootwizard')).addClass("enable");
            var li_list = navigation.find('li');
            for (var i = 0; i < index; i++) {
                jQuery(li_list[i]).addClass("completed");
                //jQuery(li_list[i]).removeClass("enable");
            }

            $('#rootwizard .progress-indicator').css({ width: porcentaje + '%' });

            CargarInfoGestionRiesgoOperacional(gnNroRiesgo, lntab)
            OcultarBotonGrabar(lntab)
        }

    });
    $("#rootwizard").bootstrapWizard('show', gnProcRiesgo);

    gARCHIVOS = $.fn.CargarArchivos({
        cClase: "table responsive",
        cDom: "#FileAttach",
        btn: "#btnAddFile",
        nCantidad: 1
    });


});

function ValidacionControlesPasosGestion() {
    var validacion = true;
    var lnPasoActual = goDatosRiesgoOpe.nProcRiesgo;
    var lnCurrentIndex = $('#rootwizard').bootstrapWizard('currentIndex');
    var lnTabSelect = $('#rootwizard').bootstrapWizard('tabselect')
    switch (lnCurrentIndex) {
        case 0: //Paso 1
            if (lnTabSelect > lnPasoActual) {
                if (!ValidarCamposPaso1()) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'Es necesario completar la información de los campos remarcados',
                        titulo: 'Mensaje del sistema - Paso 1',
                        tipo_mensaje: 'informacion'
                    });
                    validacion = false;
                } else {
                    if (lnPasoActual == lnCurrentIndex) {
                        bootbox.confirm({
                            message: "Se guardará la información gestionada  <strong>¿Está seguro de pasar al paso 2?</strong>",
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
                if (!ValidarCamposPaso2()) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'Es necesario completar la información de los campos remarcados',
                        titulo: 'Mensaje del sistema - Paso 2',
                        tipo_mensaje: 'informacion'

                    });
                    validacion = false;
                } else {
                    if (lnPasoActual == lnCurrentIndex) {
                        bootbox.confirm({
                            message: "Se guardará la información gestionada  <strong>¿Esta seguro de pasar al paso 3?</strong>",
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
                if (!ValidarCamposPaso3()) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'Es necesario el registro de los controles.',
                        titulo: 'Mensaje del sistema - Paso 3',
                        tipo_mensaje: 'informacion'

                    });
                    validacion = false;
                } else {
                    if (lnPasoActual == lnCurrentIndex) {
                        bootbox.confirm({
                            message: "Se guardará la información gestionada  <strong>¿Esta seguro de pasar al paso 4?</strong>",
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
                                    $("#btnGrabaPaso3").click();
                                } else { $("#rootwizard").bootstrapWizard('show', 2); }
                            }
                        });
                    }
                }
            }
            break;
        case 3: //Paso 4
            if (lnTabSelect > lnPasoActual) {
                if (lnPasoActual == lnCurrentIndex) {
                    bootbox.confirm({
                        message: "Se guardará la información gestionada  <strong>¿Esta seguro de pasar al paso 5?</strong>",
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
                                $("#btnGrabaPaso4").click();
                            } else { $("#rootwizard").bootstrapWizard('show', 3); }
                        }
                    });
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
                //clase: 'green',
                posicion: 'A',
                mensaje: 'No se encontró información del proceso seleccionado, por favor consultar con TI.',
                titulo: 'Mensaje del sistema',
                tipo_mensaje: 'error'

            });
    }
    
    return validacion;
}

// #region Modales
function ModalDetalleRiesgo() {
    $("#cCodRiesgo").html(goDatosRiesgoOpe.cCodRiesgo);
    $("#cRiesgoIdenticado").html(goDatosRiesgoOpe.cRiesgoIdentiticado);
    $("#cProcesoActual").html("Paso " + goDatosRiesgoOpe.nProcRiesgo);
    $("#cAgeDescripcion").html(goDatosRiesgoOpe.oAgencias.cAgeDescripcion);
    $("#cAreaDescripion").html(goDatosRiesgoOpe.oAreas.cAreaDescripcion);
    var lsCausas = goDatosRiesgoOpe.oCausas.cCausaDesc.split("|");
    for (i in lsCausas) {
        $("#salidaCausas").prepend("<p class='description'>" + lsCausas[i] + "</p>");
    }
    //for (i in goDatosRiesgoOpe.oCausas.cCausaDesc) {
    //    debugger;
    //    $("#salidaCausas").prepend("<p>" + i + "</p>");
    //}
    $("#cProcesoDesc").html(goDatosRiesgoOpe.oProceso.cDescProceso);
    $("#cSubProcDesc").html(goDatosRiesgoOpe.oSubProceso.cDescSubProceso);

    jQuery('#ModalDetalleRiesgo').modal('show', { backdrop: 'static' });
}

function ModalAddPlanAccion() {
    LimparCamposAddPlanAccion();
    $("#btnUpdPlanAccion").hide();
    $("#btnAddPlanAccion").show();
    jQuery('#AddPlanesAccion').modal('show', { backdrop: 'static' });
}

function ModalCriteriosEvalucion() {
    $("#btnUpdControlRiesgoResidual").hide();
    $("#btnAddControlRiesgoResidual").show();
    jQuery('#AddCriteriosEval').modal('show', { backdrop: 'static' });
}
// #endregion


function CargarInfoGestionRiesgoOperacional(nNroRiesgo, nProceso) {
    var LstModelo = new Array();
    $.fn.Conexion({
        bloqueo: true,
        direccion: '/RiesgoOperacional/ObtenerInfoGestionRiesgo',
        datos: { pnNroRiesgo: nNroRiesgo, pnNroProceso: nProceso },

        terminado: function (data) {
            LstModelo = JSON.parse(data);
            goDatosRiesgoOpe = LstModelo.oRiesgoOperacional;
            if (nProceso == 1) {
                $("#selFactorRiesgo").selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: LstModelo.oLstFactorRiesgo, dataselect: LstModelo.sLstInfoGestionRiesgo != null ? LstModelo.sLstInfoGestionRiesgo[1] : null });
                $("#selEventoPerdida").selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: LstModelo.oLstEventoPerdida, dataselect: LstModelo.sLstInfoGestionRiesgo != null ? LstModelo.sLstInfoGestionRiesgo[2] : null });
                $("#selLineaNegocio").selselectboxit({ dataShow: "cDescLineaNeg", dataValue: "cCodLineaNeg", datalist: LstModelo.oLstLineaNegocio, dataselect: LstModelo.sLstInfoGestionRiesgo != null ? LstModelo.sLstInfoGestionRiesgo[3] : null });
                /* Creamos la funcion para la lista diente*/

                //if (LstModelo.sLstInfoGestionRiesgo[4] != null) {

                //    $("#selProducto").selectBoxIt('selectOption', LstModelo.sLstInfoGestionRiesgo[4]);
                //    $("#selSubProducto").selectBoxIt('selectOption', LstModelo.sLstInfoGestionRiesgo[5]);

                $("#selProducto").selselectboxit({ dataShow: "cDescProducto", dataValue: "cCodProducto", datalist: LstModelo.oLstProducto, dataselect: LstModelo.sLstInfoGestionRiesgo != null ? LstModelo.sLstInfoGestionRiesgo[4] : null });
                $("#selSubProducto").selselectboxit({ dataShow: "cDescSubProducto", dataValue: "cCodSubProducto", datalist: LstModelo.oLstSubProducto, dataselect: LstModelo.sLstInfoGestionRiesgo != null ? LstModelo.sLstInfoGestionRiesgo[5] : null });
                //}
            } else if (nProceso == 2) {
                $("#selProbabilidad").selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: LstModelo.oLstProbabilidad, dataselect: LstModelo.sLstInfoGestionRiesgo != null ? LstModelo.sLstInfoGestionRiesgo[1] : null });
                $("#selImpacto").selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: LstModelo.oLstImpacto, dataselect: LstModelo.sLstInfoGestionRiesgo != null ? LstModelo.sLstInfoGestionRiesgo[2] : null });
                //CrearListaSimple('#selProbabilidad', LstModelo.oLstProbabilidad, 'nConsValor', 'cConsDescripcion');
                //CrearListaSimple('#selImpacto', LstModelo.oLstImpacto, 'nConsValor', 'cConsDescripcion');
                if (LstModelo.sLstInfoGestionRiesgo != null) {
                    //$("#selProbabilidad").val(LstModelo.sLstInfoGestionRiesgo[1]);
                    //$("#selImpacto").val(LstModelo.sLstInfoGestionRiesgo[2]);

                    ObtenerNivelRiesgoInherente($("#selProbabilidad").val(), $("#selImpacto").val())
                }

            } else if (nProceso == 3) {
                $("#tbl-controles-riesgo-residual").Tabla({
                    tblId: "tbl-controles-riesgo-residual",
                    //cabecera: "<strong>Control Área</strong>, <strong>Comentario</strong>, <strong>[RC] Responsable Ejecución</strong>, <strong>[FD] Periodo Ejecución</strong>, <strong>[EC] Evidencia del Control</strong>, <strong>[TC] Ejecución del Control</strong>, <strong>[CO] Cumplimiento del Objetivo</strong>",
                    //campos: "oControlArea.cControlDescripcion, cComentario, cResponsableDef, cPeriodoEjecucion, cEvidenciaControl, cEjecucionControl, cCumpleObjetivo",
                    cabecera: "nNroRiesgo, nItem,<strong>Comentario</strong>, <strong>RC</strong>, <strong>FD</strong>, <strong>EC</strong>, <strong>TC</strong>, <strong>CO</strong>, nEfecControl",
                    campos: "oDatosRiesgo.nNroRiesgo, nItemControl,cComentario, cResponsableDef, cPeriodoEjecucion, cEvidenciaControl, cEjecucionControl, cCumpleObjetivo,nEfectividadControl",
                    datos: LstModelo.oLstRiesgoResidual,
                    cantRegVertical: 8,
                    //scrollVertical: "Si",
                    classtbl: "table table-striped",
                    alineado: "C,C,L,C,C,C,C,C,C",
                    formato: "0,0,0,0,0,0,0,0,0",
                    controles: "0,0,4,0,0,0,0,0,0",
                    visible: "0,0,1,1,1,1,1,1,0",
                    anchocolumna: "0,0,30,10,10,10,10,10",
                    sindata: "No se gestionó controles para el riesgo operacional",
                    numerado: "Si",
                    ajustar: 'No',
                    opciones: [
                        { Columna: "Tipo1", id: "idEditarControlRR", claseIcono: "entypo-pencil", clase: "editarControlRR", titulo: "Editar", function: function (e) { } },
                        { Columna: "Tipo2", id: "idEliminarControlRR", claseIcono: "entypo-trash", clase: "eliminarControlRR", titulo: "Eliminar", function: function (e) { } },
                    ],
                });

                if (LstModelo.oLstRiesgoResidual != null) {
                    var lnCalificacionEfectividadControl = LstModelo.CalifEfecControl;
                    //var lnValorEstacala = LstModelo.oEscalaNivRiesgo.nValorEstala;
                    var $Probabilidad = LstModelo.oEscalaNivRiesgo.nProbabilidad;
                    var lsProbabilidadEscala = LstModelo.oEscalaNivRiesgo.cProbabilidad;
                    var $Impacto  = LstModelo.oEscalaNivRiesgo.nImpacto;
                    var lsImpactoEscala = LstModelo.oEscalaNivRiesgo.cImpacto;
                    var lnValorEstacala = DevoverNivelRiesgoValorEscala($Probabilidad, $Impacto);

                    if (lnCalificacionEfectividadControl == 1) {
                        $(".calif-efect-control").removeClass("i-nivel-efectividad-control-moderado");
                        $(".calif-efect-control").removeClass("i-nivel-efectividad-control-fuerte");

                        $(".calif-efect-control").addClass("i-nivel-efectividad-control-debil");
                        //document.getElementById('DescCalifEfectividadControl').innerText = 'Débil';
                        $('#DescCalifEfectividadControl').html('Débil');
                        //gnEfectividadControl = 1;

                    } else if (lnCalificacionEfectividadControl == 2) {
                        $(".calif-efect-control").removeClass("i-nivel-efectividad-control-debil");
                        $(".calif-efect-control").removeClass("i-nivel-efectividad-control-fuerte");

                        $(".calif-efect-control").addClass("i-nivel-efectividad-control-moderado");
                        //document.getElementById('DescCalifEfectividadControl').innerText = 'Moderado';
                        $('#DescCalifEfectividadControl').html('Moderado');
                        //gnEfectividadControl = 2;

                    } else if (lnCalificacionEfectividadControl == 3) {
                        $(".calif-efect-control").removeClass("i-nivel-efectividad-control-debil");
                        $(".calif-efect-control").removeClass("i-nivel-efectividad-control-moderado");

                        $(".calif-efect-control").addClass("i-nivel-efectividad-control-fuerte");
                        //document.getElementById('DescCalifEfectividadControl').innerText = 'Fuerte';
                        $('#DescCalifEfectividadControl').html('Fuerte');
                        //gnEfectividadControl = 3;

                    }
                    $('#DesProbRiesgoResidual').html(lsProbabilidadEscala);
                    $('#DescImpacRiesgoResidual').html(lsImpactoEscala);
                    //document.getElementById('DesProbRiesgoResidual').innerText = lsProbabilidadEscala;
                    //document.getElementById('DescImpacRiesgoResidual').innerText = lsImpactoEscala;

                    if (lnValorEstacala == 1) {
                        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-moderado");
                        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-alto");
                        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-extremo");

                        $(".nivel-riesgo-residual").addClass("i-nivel-riesgo-bajo");
                        //document.getElementById('DesNivelRiesgoResidual').innerText = 'Bajo';
                        //document.getElementById('DescApetitoRiesgo').innerText = 'Aceptado';
                        $('#DesNivelRiesgoResidual').html('Bajo');
                        $('#DescApetitoRiesgo').html('Aceptado');
                    } else if (lnValorEstacala == 2) {
                        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-bajo");
                        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-alto");
                        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-extremo");

                        $(".nivel-riesgo-residual").addClass("i-nivel-riesgo-moderado");
                        //document.getElementById('DesNivelRiesgoResidual').innerText = 'Moderado';
                        //document.getElementById('DescApetitoRiesgo').innerText = 'Aceptado';
                        $('#DesNivelRiesgoResidual').html('Moderado');
                        $('#DescApetitoRiesgo').html('Aceptado');
                    } else if (lnValorEstacala == 3) {
                        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-bajo");
                        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-moderado");
                        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-extremo");

                        $(".nivel-riesgo-residual").addClass("i-nivel-riesgo-alto");
                        //document.getElementById('DesNivelRiesgoResidual').innerText = 'Alto';
                        //document.getElementById('DescApetitoRiesgo').innerText = 'Rechazo';
                        $('#DesNivelRiesgoResidual').html('Alto');
                        $('#DescApetitoRiesgo').html('Rechazar');
                    } else if (lnValorEstacala == 4) {
                        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-bajo");
                        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-moderado");
                        $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-alto");

                        $(".nivel-riesgo-residual").addClass("i-nivel-riesgo-extremo");
                        //document.getElementById('DesNivelRiesgoResidual').innerText = 'Extremo';
                        //document.getElementById('DescApetitoRiesgo').innerText = 'Rechazo';
                        $('#DesNivelRiesgoResidual').html('Extremo');
                        $('#DescApetitoRiesgo').html('Rechazar');
                    }


                }

                $("#selRC").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: LstModelo.oLstResponsableDef });
                $("#selFD").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: LstModelo.oLstFrecuenciaDef });
                $("#selED").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: LstModelo.oLstEvidenciaControl });
                $("#selTC").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: LstModelo.oLstTipoEjecucion });
                $("#selCO").selselectboxit({ dataShow: "cCriterioDesc", dataValue: "nCriterioValor", datalist: LstModelo.oLstCumpleObjetivo });


            } else if (nProceso == 4) {
                MostrarPlanesAccion(LstModelo);

            } else if (nProceso == 5) {
                CrearLista('#selAgenciasRespon', LstModelo.oLstAgencia, 'cAgeCod', 'cAgeDescripcion', 'Agencias');
                MostrarResponsablePlanesAccion(LstModelo);
            }






        },
        //bloqueo: false,
    });
}

function OcultarBotonGrabar(nProceso) {
    if (nProceso == 1) {
        $("#btnGrabaPaso2").hide();
        $("#btnGrabaPaso3").hide();
        $("#btnGrabaPaso4").hide();
        $("#btnGrabaPaso5").hide();

        $("#btnGrabaPaso1").show();
    } else if (nProceso == 2) {
        $("#btnGrabaPaso1").hide();
        $("#btnGrabaPaso3").hide();
        $("#btnGrabaPaso4").hide();
        $("#btnGrabaPaso5").hide();

        $("#btnGrabaPaso2").show();
    } else if (nProceso == 3) {
        $("#btnGrabaPaso1").hide();
        $("#btnGrabaPaso2").hide();
        $("#btnGrabaPaso4").hide();
        $("#btnGrabaPaso5").hide();

        $("#btnGrabaPaso3").show();
    } else if (nProceso == 4) {
        $("#btnGrabaPaso1").hide();
        $("#btnGrabaPaso2").hide();
        $("#btnGrabaPaso3").hide();
        $("#btnGrabaPaso5").hide();

        $("#btnGrabaPaso4").show();
    } else if (nProceso == 5) {
        $("#btnGrabaPaso1").hide();
        $("#btnGrabaPaso2").hide();
        $("#btnGrabaPaso3").hide();
        $("#btnGrabaPaso4").hide();

        $("#btnGrabaPaso5").show();
    }
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
        //bloqueo: false
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
    if (!ValidarCamposPaso1()) { return; }

    var lnFactorRiesgo = $("#selFactorRiesgo").val();
    var lnEventoPerdida = $("#selEventoPerdida").val();
    var lsLineaNeg = $("#selLineaNegocio").val();
    var lsProducto = $("#selProducto").val();
    var lsSubProducto = $("#selSubProducto").val();

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/GrabarProcesoPaso1',
        datos: { pnNroRiesgo: gnNroRiesgo, pnFactorRiesgo: lnFactorRiesgo, pnEventoPerdida: lnEventoPerdida, psLineaNeg: lsLineaNeg, psProducto: lsProducto, psSubProducto: lsSubProducto },
        bloqueo: true,
        terminado: function (datos) {

            if (datos > 0) {
                $.fn.MensajeProcesos({
                    clase: 'green',
                    posicion: 'A',
                    mensaje: 'Se guardó correctamente la información.',
                    titulo: 'RO-' + gnNroRiesgo + ': Paso 1',
                    tipo_mensaje: 'exito'

                });
            }
        },
        bloqueo: false
    });
    $("#rootwizard").bootstrapWizard('show', 1);
});

function ValidarCamposPaso1() {
    var validacion = true;
    if (!$.fn.ValidarInput({ html: "#selFactorRiesgo", isSelect: true })
        || $("#selFactorRiesgo").val() == null) {
        $("#selFactorRiesgo").parent(this).addClass("has-error");
        validacion = false;
    } else {
        $("#selFactorRiesgo").parent(this).removeClass("has-error");
    }

    if (!$.fn.ValidarInput({ html: "#selEventoPerdida", isSelect: true })
        || $("#selEventoPerdida").val() == null) {
        $("#selEventoPerdida").parent(this).addClass("has-error");
        validacion = false;
    } else {
        $("#selEventoPerdida").parent(this).removeClass("has-error");
    }

    if (!$.fn.ValidarInput({ html: "#selLineaNegocio" })
        || $("#selLineaNegocio").val() == null) {
        $("#selLineaNegocio").parent(this).addClass("has-error");
        validacion = false;
    } else {
        $("#selLineaNegocio").parent(this).removeClass("has-error");
    }
    if (!$.fn.ValidarInput({ html: "#selProducto", isSelect: true })
        || $("#selProducto").val() == null) {
        $("#selProducto").parent(this).addClass("has-error");
        validacion = false;
    } else {
        $("#selProducto").parent(this).removeClass("has-error");
    }
    if (!$.fn.ValidarInput({ html: "#selSubProducto", isSelect: true })
        || $("#selSubProducto").val() == null) {
        $("#selSubProducto").parent(this).addClass("has-error");
        validacion = false;
    } else {
        $("#selSubProducto").parent(this).removeClass("has-error");
    }
    return validacion;
}

// #endregion

// #region Gestion Paso 2
$("#selProbabilidad").change(function () {
    if ($("#selImpacto").val() != null) {
        ObtenerNivelRiesgoInherente($("#selProbabilidad").val(), $("#selImpacto").val())
    }
});

$("#selImpacto").change(function () {
    if ($("#selProbabilidad").val() != null) {
        ObtenerNivelRiesgoInherente($("#selProbabilidad").val(), $("#selImpacto").val())
    }
});

function ObtenerNivelRiesgoInherente(nProbabilidad, nImpacto) {
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ObtenerNivelRiesgoInherente',
        datos: { pnProbabilidad: nProbabilidad, pnImpacto: nImpacto },
        terminado: function (data) {
            oNivelRiesgoInherente = JSON.parse(data);

            if (oNivelRiesgoInherente.oRiesgoInherente.nValorEscala > 0 && oNivelRiesgoInherente.oMontoPerdida != null) {
                if (oNivelRiesgoInherente.oRiesgoInherente.nValorEscala == 1) {
                    $(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-moderado");
                    $(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-alto");
                    $(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-extremo");

                    $(".nivel-riesgo-inherente").addClass("i-nivel-riesgo-bajo");
                    //document.getElementById('DescNivelRiesgoInherente').innerText = 'Bajo';
                    $('#DescNivelRiesgoInherente').html('Bajo');
                } else if (oNivelRiesgoInherente.oRiesgoInherente.nValorEscala == 2) {
                    $(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-bajo");
                    $(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-alto");
                    $(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-extremo");

                    $(".nivel-riesgo-inherente").addClass("i-nivel-riesgo-moderado");
                    //document.getElementById('DescNivelRiesgoInherente').innerText = 'Moderado';
                    $('#DescNivelRiesgoInherente').html('Moderado');
                } else if (oNivelRiesgoInherente.oRiesgoInherente.nValorEscala == 3) {
                    $(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-bajo");
                    $(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-moderado");
                    $(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-extremo");

                    $(".nivel-riesgo-inherente").addClass("i-nivel-riesgo-alto");
                    //document.getElementById('DescNivelRiesgoInherente').innerText = 'Alto';
                    $('#DescNivelRiesgoInherente').html('Alto');
                } else if (oNivelRiesgoInherente.oRiesgoInherente.nValorEscala == 4) {
                    $(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-bajo");
                    $(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-moderado");
                    $(".nivel-riesgo-inherente").removeClass("i-nivel-riesgo-alto");

                    $(".nivel-riesgo-inherente").addClass("i-nivel-riesgo-extremo");
                    //document.getElementById('DescNivelRiesgoInherente').innerText = 'Extremo';
                    $('#DescNivelRiesgoInherente').html('Extremo');
                }
                //document.getElementById('DescMontoPerdida').innerText = oNivelRiesgoInherente.oMontoPerdida.cMontoPerdida;
                $('#DescMontoPerdida').html(oNivelRiesgoInherente.oMontoPerdida.cMontoPerdida);

            }
        },
        //bloqueo: false
    });

}

$("#btnGrabaPaso2").click(function (e) {
    e.preventDefault();
    if (!ValidarCamposPaso2()) { return; }

    var lnProbabilidad = $("#selProbabilidad").val();
    var lnImpacto = $("#selImpacto").val();

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/GrabarProcesoPaso2',
        datos: { pnNroRiesgo: gnNroRiesgo, pnProbabilidad: lnProbabilidad, pnImpacto: lnImpacto },
        bloqueo: true,
        terminado: function (datos) {

            if (datos > 0) {
                $.fn.MensajeProcesos({
                    clase: 'green',
                    posicion: 'A',
                    mensaje: 'Se guardó correctamente la información.',
                    titulo: 'RO-' + gnNroRiesgo + ': Paso 2',
                    tipo_mensaje: 'exito'

                });
            }
        },
        bloqueo: false
    });
    $("#rootwizard").bootstrapWizard('show', 2);
});

function ValidarCamposPaso2() {
    var validacion = true;

    if (!$.fn.ValidarInput({ html: "#selProbabilidad", isSelect: true })
        || $("#selProbabilidad").val() == "nn") {
        $("#selProbabilidad").parent(this).addClass("has-error");
        validacion = false;
    } else {
        $("#selProbabilidad").parent(this).removeClass("has-error");
    }

    if (!$.fn.ValidarInput({ html: "#selImpacto", isSelect: true })
        || $("#selImpacto").val() == "nn") {
        $("#selImpacto").parent(this).addClass("has-error");
        validacion = false;
    } else {
        $("#selImpacto").parent(this).removeClass("has-error");
    }

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
    if ($("#selRC").val() == null) { return false; }
    if ($("#selFD").val() == null) { return false; }
    if ($("#selED").val() == null) { return false; }
    if ($("#selTC").val() == null) { return false; }
    if ($("#selCO").val() == null) { return false; }
    return true;
}

function CargarValorNivelRiesgo() {
    if (ComprobarSelectControl()) {
        var nValorNivel = $("#selRC").val() * $("#selFD").val() * $("#selED").val() * $("#selTC").val() * $("#selCO").val()
        if (nValorNivel >= 0 && nValorNivel <= 18) {
            $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-moderado");
            $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-fuerte");

            $(".nivel-efectividad").addClass("i-nivel-efectividad-control-debil");
            //document.getElementById('DescEfectividadControl').innerText = 'Débil';
            $('#DescEfectividadControl').html('Débil');
            gnEfectividadControl = 1;
        } else if (nValorNivel >= 19 && nValorNivel <= 72) {
            $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-debil");
            $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-fuerte");

            $(".nivel-efectividad").addClass("i-nivel-efectividad-control-moderado");
            //document.getElementById('DescEfectividadControl').innerText = 'Moderado';
            $('#DescEfectividadControl').html('Moderado');
            gnEfectividadControl = 2;
        } else if (nValorNivel >= 73 && nValorNivel <= 256) {
            $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-debil");
            $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-moderado");

            $(".nivel-efectividad").addClass("i-nivel-efectividad-control-fuerte");
            //document.getElementById('DescEfectividadControl').innerText = 'Fuerte';
            $('#DescEfectividadControl').html('Fuerte');
            gnEfectividadControl = 3;
        }
    }
}

$(document).on("click", ".editarControlRR", function (e) {
    $("#btnAddControlRiesgoResidual").hide();
    $("#btnUpdControlRiesgoResidual").show();

    var lnNroRiesgo = $(this).parents("tr").find("td").eq(1).html();
    var lnItem = $(this).parents("tr").find("td").eq(2).html();

    var oCritriosControl = null;
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
});

function ValidarCamposControlRiesgoResidual() {
    var validacion = true;

    if (!$.fn.ValidarInput({ html: "#inDescControl" })) {
        $("#inDescControl").addClass("input-validator");
        validacion = false;
    } else {
        $("#inDescControl").removeClass("input-validator");
    }
    if (!$.fn.ValidarInput({ html: "#selRC", isSelect: true })
        || $("#selRC").val() == "nn") {
        $("#selRC").addClass("input-validator");
        validacion = false;
    } else {
        $("#selRC").removeClass("input-validator");
    }
    if (!$.fn.ValidarInput({ html: "#selFD" })
        || $("#selFD").val() == "nn") {
        $("#selFD").addClass("input-validator");
        validacion = false;
    } else {
        $("#selFD").removeClass("input-validator");
    }
    if (!$.fn.ValidarInput({ html: "#selED", isSelect: true })
        || $("#selED").val() == "nn") {
        $("#selED").addClass("input-validator");
        validacion = false;
    } else {
        $("#selED").removeClass("input-validator");
    }
    if (!$.fn.ValidarInput({ html: "#selTC", isSelect: true })
        || $("#selTC").val() == "nn") {
        $("#selTC").addClass("input-validator");
        validacion = false;
    } else {
        $("#selTC").removeClass("input-validator");
    }
    if (!$.fn.ValidarInput({ html: "#selCO", isSelect: true })
        || $("#selCO").val() == "nn") {
        $("#selCO").addClass("input-validator");
        validacion = false;
    } else {
        $("#selCO").removeClass("input-validator");
    }
    return validacion;
}

$(document).on("click", ".eliminarControlRR", function (e) {
    var lnNroRiesgo = $(this).parents("tr").find("td").eq(1).html();
    var lnItem = $(this).parents("tr").find("td").eq(2).html();
    bootbox.confirm({
        message: "<strong>¿Está seguro de eliminar el control asignado al riesgo?</strong>",
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
                    direccion: '/RiesgoOperacional/EliminarControlRiesgoResidual',
                    datos: { pnNroRiesgo: lnNroRiesgo, pnItem: lnItem },
                    bloqueo: true,
                    terminado: function (data) {
                        oRiesgoResidual = JSON.parse(data);
                        MostrarControlRiesgoResidual(oRiesgoResidual);
                        $.fn.MensajeProcesos({
                            clase: 'green',
                            posicion: 'A',
                            mensaje: 'El control del riesgo residualfue eliminado correctamente.',
                            titulo: 'Control Eliminado',
                            tipo_mensaje: 'informacion'
                        });

                    },
                    bloqueo: false
                });
            }
        }
    });
});

$("#btnAddControlRiesgoResidual").click(function (e) {
    e.preventDefault();

    if (!ValidarCamposControlRiesgoResidual()) { return; }

    var lsComenControl = $("#inDescControl").val();
    var lnResponDef = $("#selRC").val();
    var lnPeriEjec = $("#selFD").val();
    var lnEvidenControl = $("#selED").val();
    var lnTpoEjec = $("#selTC").val();
    var lnCumpleObj = $("#selCO").val();

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/RegistrarControlesRiesgoResidual',
        datos: {
            pnNroRiesgo: gnNroRiesgo, psComentario: lsComenControl, pnReponControl: lnResponDef, pnPeriEjec: lnPeriEjec, pnEvidenControl: lnEvidenControl, pnEjecControl: lnTpoEjec,
            pnCumpleObj: lnCumpleObj, pnEfecControl: gnEfectividadControl
        },
        //bloqueo: true,
        terminado: function (data) {
            oRiesgoResidual = JSON.parse(data);

            MostrarControlRiesgoResidual(oRiesgoResidual);

            $.fn.MensajeProcesos({
                clase: 'green',
                posicion: 'A',
                mensaje: 'Se proceso la información del riesgo de manera exitosa.',
                titulo: 'Exito!!!',
                tipo_mensaje: 'exito'

            });

            LimpiarModalAddControlRiesgoResidual();

        },
        //bloqueo: false
    });
});

function MostrarControlRiesgoResidual(Modelo) {
    $("#tbl-controles-riesgo-residual").Tabla({
        tblId: "tbl-controles-riesgo-residual",
        cabecera: "nNroRiesgo, nItem,<strong>Comentario</strong>, <strong>[RC] Responsable Ejecución</strong>, <strong>[FD] Periodo Ejecución</strong>, <strong>[EC] Evidencia del Control</strong>, <strong>[TC] Ejecución del Control</strong>, <strong>[CO] Cumplimiento del Objetivo</strong>, nEfecControl",
        campos: "oDatosRiesgo.nNroRiesgo, nItemControl,cComentario, cResponsableDef, cPeriodoEjecucion, cEvidenciaControl, cEjecucionControl, cCumpleObjetivo,nEfectividadControl",
        datos: Modelo.oLstRiesgoResidual,
        cantRegVertical: 8,
        scrollVertical: "Si",
        classtbl: "table table-bordered table-responsive",
        //classtbl: "table table-responsive",
        alineado: "C,C,L,C,C,C,C,C,C",
        formato: "0,0,0,0,0,0,0,0,0",
        controles: "0,0,4,0,0,0,0,0,0",
        visible: "0,0,1,1,1,1,1,1,0",
        numerado: "Si",
        ajustar: 'No',
        opciones: [
            { Columna: "Tipo1", id: "idEditarControlRR", claseIcono: "entypo-pencil", clase: "editarControlRR", titulo: "Editar", function: function (e) { } },
            { Columna: "Tipo2", id: "idEliminarControlRR", claseIcono: "entypo-trash", clase: "eliminarControlRR", titulo: "Eliminar", function: function (e) { } },
        ],
    });

    if (Modelo.oLstRiesgoResidual != null) {
        var lnCalificacionEfectividadControl = Modelo.CalifEfecControl;
        //var lnValorEstacala = Modelo.oEscalaNivRiesgo.nValorEstala;
        var $Probabilidad = Modelo.oEscalaNivRiesgo.nProbabilidad;
        var lsProbabilidadEscala = Modelo.oEscalaNivRiesgo.cProbabilidad;
        var $Impacto = Modelo.oEscalaNivRiesgo.nImpacto;
        var lsImpactoEscala = Modelo.oEscalaNivRiesgo.cImpacto;
        var lnValorEstacala = DevoverNivelRiesgoValorEscala($Probabilidad, $Impacto);

        if (lnCalificacionEfectividadControl == 1) {
            $(".calif-efect-control").removeClass("i-nivel-efectividad-control-moderado");
            $(".calif-efect-control").removeClass("i-nivel-efectividad-control-fuerte");

            $(".calif-efect-control").addClass("i-nivel-efectividad-control-debil");
            //document.getElementById('DescCalifEfectividadControl').innerText = 'Débil';
            $('#DescCalifEfectividadControl').html('Débil');
            //gnEfectividadControl = 1;

        } else if (lnCalificacionEfectividadControl == 2) {
            $(".calif-efect-control").removeClass("i-nivel-efectividad-control-debil");
            $(".calif-efect-control").removeClass("i-efectividad-control-fuerte");

            $(".calif-efect-control").addClass("i-nivel-efectividad-control-moderado");
            //document.getElementById('DescCalifEfectividadControl').innerText = 'Moderado';
            $('#DescCalifEfectividadControl').html('Moderado');
            //gnEfectividadControl = 2;

        } else if (lnCalificacionEfectividadControl == 3) {
            $(".calif-efect-control").removeClass("i-nivel-efectividad-control-debil");
            $(".calif-efect-control").removeClass("i-nivel-efectividad-control-moderado");

            $(".calif-efect-control").addClass("i-nivel-efectividad-control-fuerte");
            //document.getElementById('DescCalifEfectividadControl').innerText = 'Fuerte';
            $('#DescCalifEfectividadControl').html('Fuerte');
            //gnEfectividadControl = 3;

        }
        $('#DesProbRiesgoResidual').html(lsProbabilidadEscala);
        $('#DescImpacRiesgoResidual').html(lsImpactoEscala);
        //document.getElementById('DesProbRiesgoResidual').innerText = lsProbabilidadEscala;
        //document.getElementById('DescImpacRiesgoResidual').innerText = lsImpactoEscala;

        if (lnValorEstacala == 1) {
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-moderado");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-alto");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-extremo");

            $(".nivel-riesgo-residual").addClass("i-nivel-riesgo-bajo");
            //document.getElementById('DesNivelRiesgoResidual').innerText = 'Bajo';
            //document.getElementById('DescApetitoRiesgo').innerText = 'Aceptado';
            $('#DesNivelRiesgoResidual').html('Bajo');
            $('#DescApetitoRiesgo').html('Aceptado');
        } else if (lnValorEstacala == 2) {
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-bajo");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-alto");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-extremo");

            $(".nivel-riesgo-residual").addClass("i-nivel-riesgo-moderado");
            //document.getElementById('DesNivelRiesgoResidual').innerText = 'Moderado';
            //document.getElementById('DescApetitoRiesgo').innerText = 'Aceptado';
            $('#DesNivelRiesgoResidual').html('Moderado');
            $('#DescApetitoRiesgo').html('Aceptado');
        } else if (lnValorEstacala == 3) {
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-bajo");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-moderado");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-extremo");

            $(".nivel-riesgo-residual").addClass("i-nivel-riesgo-alto");
            //document.getElementById('DesNivelRiesgoResidual').innerText = 'Alto';
            //document.getElementById('DescApetitoRiesgo').innerText = 'Rechazo';
            $('#DesNivelRiesgoResidual').html('Alto');
            $('#DescApetitoRiesgo').html('Rechazar');
        } else if (lnValorEstacala == 4) {
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-bajo");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-moderado");
            $(".nivel-riesgo-residual").removeClass("i-nivel-riesgo-alto");

            $(".nivel-riesgo-residual").addClass("i-nivel-riesgo-extremo");
            //document.getElementById('DesNivelRiesgoResidual').innerText = 'Extremo';
            //document.getElementById('DescApetitoRiesgo').innerText = 'Rechazo';
            $('#DesNivelRiesgoResidual').html('Extremo');
            $('#DescApetitoRiesgo').html('Rechazar');
        }
    } else {
        $('#DescCalifEfectividadControl').html('');
        $('#DesProbRiesgoResidual').html('');
        $('#DescImpacRiesgoResidual').html('');
        $('#DesNivelRiesgoResidual').html('');
        $('#DescApetitoRiesgo').html('');

        //document.getElementById('DescCalifEfectividadControl').innerText = '';
        //document.getElementById('DesProbRiesgoResidual').innerText = "";
        //document.getElementById('DescImpacRiesgoResidual').innerText = "";
        //document.getElementById('DesNivelRiesgoResidual').innerText = '';
        //document.getElementById('DescApetitoRiesgo').innerText = '';
    }
}

function LimpiarModalAddControlRiesgoResidual() {
    $("#AddCriteriosEval").modal('hide');
    $("#inDescControl").val("");
    $("#selRC").val("nn");
    $("#selRC").removeClass("input-validator");
    $("#selFD").val("nn");
    $("#selFD").removeClass("input-validator");
    $("#selED").val("nn");
    $("#selED").removeClass("input-validator");
    $("#selTC").val("nn");
    $("#selTC").removeClass("input-validator");
    $("#selCO").val("nn");
    $("#selCO").removeClass("input-validator");

    $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-debil");
    $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-fuerte");
    $(".nivel-efectividad").removeClass("i-nivel-efectividad-control-moderado");
    $("#DescEfectividadControl").html("");
    //document.getElementById('DescEfectividadControl').innerText = '';
}

$("#btnGrabaPaso3").click(function (e) {
    e.preventDefault();
    var lsComentarios = $("#inComentariosRiesgoresidual").val();
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/GrabarProcesoPaso3',
        datos: { pnNroRiesgo: gnNroRiesgo, psComentarios: lsComentarios },
        bloqueo: true,
        terminado: function (datos) {

            if (datos > 0) {
                $.fn.MensajeProcesos({
                    clase: 'green',
                    posicion: 'A',
                    mensaje: 'Se guardó correctamente la información.',
                    titulo: 'RO-' + gnNroRiesgo + ': Paso 3',
                    tipo_mensaje: 'exito'

                });

            }
        },
        bloqueo: false
    });
    $("#rootwizard").bootstrapWizard('show', 3);
});

function ValidarCamposPaso3() {
    var validacion = true;
    var lnFilas = $("tbl-controles-riesgo-residual tr").length;
    if (lnFilas == 0) {
        validacion = false;
    }
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

$("#btnGrabaPaso4").click(function (e) {
    e.preventDefault();

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/GrabarProcesoPaso4',
        datos: { pnNroRiesgo: gnNroRiesgo },
        bloqueo: true,
        terminado: function (datos) {
            if (datos > 0) {
                $.fn.MensajeProcesos({
                    clase: 'green',
                    posicion: 'A',
                    mensaje: 'Se guardó correctamente la información.',
                    titulo: 'RO-' + gnNroRiesgo + ': Paso 4',
                    tipo_mensaje: 'exito'
                });
            }
        },
        bloqueo: false
    });
    $("#rootwizard").bootstrapWizard('show', 4);
});


$("#btnAddPlanAccion").click(function (e) {
    e.preventDefault();
    if (!ValidarCamposPlanAccion()) { return; }

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
                    classextra: true
                });
                //BloquearCarga("Subiendo archivos al servidor", "Procesando Información", true);
            },
            success: function (data) {
                try {
                    var oDocAdj = JSON.parse(data);

                    $.fn.Conexion({
                        direccion: '/RiesgoOperacional/GrabarPlanAccion',
                        datos: {
                            pnNroRiesgo: gnNroRiesgo, psDescPlanAccion: lsDesPlanAccion, pdFechaImplement: lsFechaImple,
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
                    //DesbloquearCarga();
                    //dialog.modal('hide');
                } catch (ex) { dialog.modal('hide'); }
            },
            error: function () {
                //DesbloquearCarga();
                dialog.modal('hide');

                $.fn.MensajeProcesos({
                    posicion: 'B',
                    mensaje: 'Error al intentar subir el documento al servidor.',
                    titulo: 'Mensaje del Sistema',
                    tipo_mensaje: 'Error'
                });
                //bootbox.alert("Error al intentar subir el documento al servidor");
            },
            cache: false,
            contentType: false,
            processData: false
        });
    } else {
        $.fn.Conexion({
            direccion: '/RiesgoOperacional/GrabarPlanAccion',
            datos: { pnNroRiesgo: gnNroRiesgo, psDescPlanAccion: lsDesPlanAccion, pdFechaImplement: lsFechaImple, pnSugerenciaGM: lbSugerenciaGM, psComentarios: lsComentPlan },
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

function MostrarPlanesAccion(oPlanesAccion) {
    $("#tbl-plan-accion").Tabla({
        tblId: "tbl-plan-accion",
        cabecera: "nNroRiesgo,nPlanCod,Código,<strong>Descripción del plan de acción</strong>,<strong>Estado</strong>,<strong>Fecha Registo</strong>",
        campos: "oDatosRiesgo.nNroRiesgo,nPlanCod,cPlanCod,cPlanDescripcion,cEstado,dFechaRegistro",
        datos: oPlanesAccion.oLstPlanAccion,
        classtbl: "table table-striped",
        cantRegVertical: 8,
        //scrollVertical: "Si",
        alineado: "C,C,C,L,C,C",
        formato: "0,0,0,0,0,3",
        controles: "0,0,5,4,0,0",
        visible: "0,0,1,1,1,1",
        anchocolumna: "0,0,15,40,15,10",
        sindata: "No se gestionó plane de acción para el riesgo operacional",
        numerado: "Si",
        ajustar: 'No',
        opciones: [
            { Columna: "Tipo1", id: "idAdjunDocPlanAccion", claseIcono: "fa fa-upload", clase: "AdjunDocPlanAccion", titulo: "Adjuntar documento", function: function (e) { } },
            { Columna: "Tipo2", id: "idEditarPlanAccion", claseIcono: "entypo-pencil", clase: "editarPlanAccion", titulo: "Editar", function: function (e) { } },
            { Columna: "Tipo3", id: "idEliminarPlanAccion", claseIcono: "entypo-trash", clase: "eliminarPlanAccion", titulo: "Eliminar", function: function (e) { } },
        ],
    });
}

$(document).on("click", ".AdjunDocPlanAccion", function (e) {

    //jQuery('#AdjuntarDoc').modal('show', { backdrop: 'static' });
});

$(document).on("click", ".editarPlanAccion", function (e) {
    $("#btnAddPlanAccion").hide();
    $("#btnUpdPlanAccion").show();

    var lnNroRiesgo = $(this).parents("tr").find("td").eq(1).html();
    var lnCodPlan = $(this).parents("tr").find("td").eq(2).html();

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/ObtenerPlanAccionRiesgoPlan',
        datos: { pnNroRiesgo: lnNroRiesgo, pnCodPlanAccion: lnCodPlan },
        bloqueo: true,
        terminado: function (data) {
            oPlanAccion = JSON.parse(data);

            if (oPlanAccion.oPlanAccion != null) {
                $("#inDescPlanAccion").val(oPlanAccion.oPlanAccion.cPlanDescripcion);
                $("#inFechaImplementacion").val(oPlanAccion.oPlanAccion.dFechaImplement);
                $("#inComentPlanAcccion").val(oPlanAccion.oPlanAccion.cComentario);
                if (oPlanAccion.oPlanAccion.bSugerenciaGM == true) {
                    //$('.sugerencia').on('switch-change', function () {
                    //    $('.sugerencia').bootstrapSwitch('toggleRadioStateAllowUncheck', true);
                    //});
                    /*$('#chkSugeridoGM').bootstrapSwitch('toggleRadioStateAllowUncheck');*/
                } else { /*$('#chkSugeridoGM').bootstrapSwitch('setActive', false); */ }

                jQuery('#AddPlanesAccion').modal('show', { backdrop: 'static' });
            }
        },
        bloqueo: false
    });
});

$(document).on("click", ".eliminarPlanAccion", function (e) {
    var lnNroRiesgo = $(this).parents("tr").find("td").eq(1).html();
    var lnPlanCod = $(this).parents("tr").find("td").eq(2).html();

    bootbox.confirm({
        message: "<strong>¿Está seguro de eliminar el plan de acción?</strong>",
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
                    //bloqueo: true,
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
                    //bloqueo: false
                });
            }
        }
    });

});

function ValidarCamposPlanAccion() {
    var validacion = true;

    if (!$.fn.ValidarInput({ html: "#inDescPlanAccion" })) {
        $("#inDescPlanAccion").addClass("input-validator");
        validacion = false;
    } else {
        $("#inDescPlanAccion").removeClass("input-validator");
    }
    if (!$.fn.ValidarInput({ html: "#inFechaImplementacion" })) {
        $("#inFechaImplementacion").addClass("input-validator");
        validacion = false;
    } else {
        $("#inFechaImplementacion").removeClass("input-validator");
    }

    return validacion;
}


// #endregion

// #region Paso 5
$("#selAgenciasRespon").change(function () {
    var Agencia = $("#selAgenciasRespon").val();
    if (Agencia != "") {
        ObtenerAreas(Agencia);
    }
});

function ObtenerAreas(sAgeCod) {
    var ListaAreas = new Array();
    $.fn.Conexion({
        direccion: '/General/ListarAreasAgencia',
        datos: { psAgeCod: sAgeCod },
        terminado: function (data) {
            ListaAreas = JSON.parse(data);

            CrearLista('#selAreaRespon', ListaAreas.oLstAreas, 'cAreaCod', 'cAreaDescripcion', 'Áreas');
        },
        bloqueo: false
    });
}

$("#selAreaRespon").change(function () {

    var Agencia = $("#selAgenciasRespon").val();
    var Area = $("#selAreaRespon").val();
    if (Agencia != "" && Area != "") {
        ObtenerUsuariosAreasAgencias(Agencia, Area);
    }
});

function ObtenerUsuariosAreasAgencias(sAgeCod, sAreaCod) {
    var ListaUsuario = new Array();
    //var Areas = ""
    $.fn.Conexion({
        direccion: '/General/ListarUsuarioAreaAgencia',
        datos: { psAgeCod: sAgeCod, psAreaCod: sAreaCod },
        terminado: function (data) {
            ListaUsuario = JSON.parse(data);
            //
            CrearLista('#selUserResponsablePlan', ListaUsuario.oLstUsuarios, 'cUser', 'cUsuario', 'Usuarios');
        },
        bloqueo: false
    });
}

function MostrarResponsablePlanesAccion(oPlanesAccion) {
    $("#tbl-responsable-plan-accion").Tabla({
        tblId: "tbl-responsable-plan-accion",
        cabecera: "nNroRiesgo,<strong>Cód. Plan Acción</strong>,<strong>Descripción del plan de acción</strong>,<strong>Responsable(s)</strong>",
        campos: "oDatosRiesgo.nNroRiesgo,oPlanAccion.nPlanCod,oPlanAccion.cPlanDescripcion,cUserResponsable",
        datos: oPlanesAccion.oLstReponPlanAccionRiesgo,
        cantRegVertical: 8,
        //scrollVertical: "Si",
        classtbl: "table table-responsive",
        //classtbl: "table table-responsive",
        alineado: "C,C,L,L",
        formato: "0,0,0,0",
        controles: "0,0,4,0",
        visible: "0,1,1,1",
        numerado: "Si",
        ajustar: 'No',
        opciones: [
            { Columna: "Tipo1", id: "idResponPlanAccion", claseIcono: "entypo-user", clase: "ResponPlanAccion", titulo: "Añadir/Editar Responsables", function: function (e) { } },
        ],
    });
}

$(document).on("click", ".ResponPlanAccion", function (e) {
    var lnNroRiesgo = $(this).parents("tr").find("td").eq(1).html();
    var lnCodPlan = $(this).parents("tr").find("td").eq(2).html();

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/MostarResponsablePlanAccion',
        datos: { pnNroRiesgo: lnNroRiesgo, pnCodPlanAccion: lnCodPlan },
        bloqueo: true,
        terminado: function (data) {
            oResponPlanAccion = JSON.parse(data);
            if (oResponPlanAccion.oLstReponPlanAccion != null) {
                MostrarReponsablePlanAccion(oResponPlanAccion);
                jQuery('#DetResponsablesPlanesAccion').modal('show');
            }
        },
        bloqueo: false
    });
});

$(document).on("click", ".QuitarResponPlanAccion", function (e) {
    var lnNroRiesgo = $(this).parents("tr").find("td").eq(1).html();
    var lnCodPlan = $(this).parents("tr").find("td").eq(2).html();
    var lnItem = $(this).parents("tr").find("td").eq(3).html();

    bootbox.confirm({
        message: "<strong>¿Está seguro de quitar al resposable del plan de acción? </strong>",
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
                    bloqueo: true,
                    terminado: function (data) {
                        LstResponsablePlan = JSON.parse(data);
                        MostrarResponsablePlanesAccion(LstResponsablePlan)
                        MostrarReponsablePlanAccion(LstResponsablePlan);
                        $.fn.MensajeProcesos({
                            clase: 'green',
                            posicion: 'A',
                            mensaje: 'Se quito al responsable del plan de acción correctamente.',
                            titulo: 'Responsable Plan Acción',
                            tipo_mensaje: 'informacion'
                        });

                    },
                    bloqueo: false
                });
            } /*else { }*/
        }
    });
});

function MostrarReponsablePlanAccion(LstResponsablePlan) {
    $("#tbl-add-responsables-planes-accion").Tabla({
        tblId: "tbl-add-responsables-planes-accion",
        cabecera: "nNroRiesgo,nPlanCod,nItemRespon,<strong>Usuario</strong>,<strong>Nombre</strong>",
        campos: "oDatosRiesgo.nNroRiesgo,oPlanAccion.nPlanCod,nItemResponPlan,cUserResponsable,oPersona.cPersNombre",
        datos: LstResponsablePlan.oLstReponPlanAccion,
        cantRegVertical: 4,
        scrollVertical: "Si",
        //classtbl: "table table-bordered table-responsive",
        classtbl: "table table-responsive",
        alineado: "C,C,C,C,L",
        formato: "0,0,0,0,0",
        controles: "0,0,0,0,0",
        visible: "0,0,0,1,1",
        numerado: "Si",
        ajustar: 'No',
        opciones: [
            { Columna: "Tipo1", id: "idQuitarResponPlanAccion", claseIcono: "entypo-cancel-squared", clase: "QuitarResponPlanAccion", titulo: "Quitar Responsables", function: function (e) { } },
        ],
    });
}

$("#btnAddUserResponsable").click(function (e) {
    e.preventDefault();
    if (!ValidarCamposResponsablePlanAccion()) { return; }

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
                    mensaje: 'Se resgistro correctamente el responsable al plan de acción ' + lnCodPlan,
                    titulo: 'Asignación de Resposanble',
                    tipo_mensaje: 'exito'

                });
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
    if (!$.fn.ValidarInput({ html: "#selAgenciasRespon", isSelect: true })) {
        $("#selAgenciasRespon").addClass("input-validator");
        validacion = false;
    } else {
        $("#selAgenciasRespon").removeClass("input-validator");
    }
    if (!$.fn.ValidarInput({ html: "#selAreaRespon", isSelect: true })) {
        $("#selAreaRespon").addClass("input-validator");
        validacion = false;
    } else {
        $("#selAreaRespon").removeClass("input-validator");
    }
    if (!$.fn.ValidarInput({ html: "#selUserResponsablePlan", isSelect: true })) {
        $("#selUserResponsablePlan").addClass("input-validator");
        validacion = false;
    } else {
        $("#selUserResponsablePlan").removeClass("input-validator");
    }
    if (seleccion == -1) {
        bootbox.alert("Debe seleccionar un plan de acción para proseguir con el registro");
        validacion = false;
    }
    return validacion;
}

$("#btnGrabaPaso5").click(function (e) {
    e.preventDefault();

    bootbox.confirm({
        title: "Asinación de responsable - Planes de acción",
        message: "Al autorizar la asignación previamente configurado, se informara sobre la asignacion al usuario correspondiente <br/>  <strong>¿Está seguro de grabar los datos?</strong>",
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
                    direccion: '/RiesgoOperacional/GrabarProcesoPaso5',
                    datos: { pnNroRiesgo: gnNroRiesgo },
                    bloqueo: true,
                    terminado: function (datos) {
                        if (datos > 0) {
                            $.fn.MensajeProcesos({
                                clase: 'green',
                                posicion: 'A',
                                mensaje: 'Se guardó correctamente la información.',
                                titulo: 'RO-' + gnNroRiesgo + ': Paso 5',
                                tipo_mensaje: 'exito'
                            });
                        }
                    },
                    bloqueo: false
                });
                //$("#rootwizard").bootstrapWizard('show', 4);





            } //else { $("#rootwizard").bootstrapWizard('show', 0); }
        }
    });



});








// #endregion


function MostrarPanelInfo(paso) {
    switch (paso) {
        case 3:
            var opciones = {
                $titulo: "<strong>Criterios de Evaluación</strong>",
                $mensaje: "<p class='text-primary'><spam class='badge badge-primary'>RC</spam> Responsable de la ejecución</p><hr/>" +
                    "<p class='text-primary'><spam class='badge badge-primary'>FD</spam> Periodo de Ejecución</p><hr/>" +
                    "<p class='text-primary'><spam class='badge badge-primary'>EC</spam> Evidencia de Control</p><hr/>" +
                    "<p class='text-primary'><spam class='badge badge-primary'>TC</spam> Ejecucion del Control</p><hr/>" +
                    "<p class='text-primary'><spam class='badge badge-primary'>CO</spam> Cumplimiento Objetivo</p>"
            }
            bootbox.dialog({
                title: opciones.$titulo,
                message: opciones.$mensaje,
                size: 'small',
                closeButton: false,
                buttons: {
                    ok: {
                        label: "Entendido!",
                        //className: 'btn-white',
                    }
                }
            });
            break;
        case 4:
            var opciones = {
                $titulo: "<strong>Sugerencia del control</strong>",
                $mensaje: "<spam class='badge badge-success'>Sugerido</spam><p class='text-primary'>El control fue sugerido por la genrencia mancomunada</p> <hr/>" +
                          "<spam class='badge badge-primary'>No sugerido</spam><p class='text-primary'>El control no es sugerencia de la gerencia mancomunada</p>"
            }
            bootbox.dialog({
                title: opciones.$titulo,
                message: opciones.$mensaje,
                size: 'small',
                closeButton: false,
                buttons: {
                    ok: {
                        label: "Entendido!",
                        //className: 'btn-white',
                    }
                }
            });
            break;

    }
}



