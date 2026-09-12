

window.onload = function () {

    gFiles = $.fn.CargarArchivos({
        cClase: "table table-bordered",
        cDom: "#tbl-doc-adjunto",
        btn: "#btnAjuntarDoc",
        nCantidad: 1
    });

    MostrarRiesgosOperacionales(null);

}


$("#inAnio").on({
    "keyup": function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == "13") {
            event.preventDefault();
            $("#btnProcesar").click();
        }
    }
});



function MostrarRiesgosOperacionales(dataList) {
    var $opciones = [
        {
            Columna: "Option1", id: "id-detalle-riesgo-incentivo", claseIcono: "entypo-info-circled", clase: "detalle-gestion-riesgo-incentivo", titulo: "Detalle del riesgo", function: function (e) {
                var ObjParam = { riesgo: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
                $.fn.Conexion({
                    direccion: '/RiesgoOperacional/DevolverVistaParam',
                    datos: { Vista: "DetalleRiesgo", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
                    bloqueo: true,
                    terminado: function (data) {
                        window.location = data.url;
                    },
                });

            }
        },
        {
            Columna: "Option2", id: "id-gestionar-incentivo", claseIcono: "entypo-cog", clase: "gestionar-incentivo", titulo: "Gestionar Incentivo", function: function (e) {
                var lsCodRiesgo = $(e).parents("tr").find("td").eq(2).html();
                var lnMontoPropuesto = $(e).parents("tr").find("td").eq(9).html();
                $.fn.Conexion({
                    direccion: '/RiesgoOperacional/ListarConstante',
                    datos: { pnConsCod: 1515 },
                    terminado: function (data) {
                        datos = JSON.parse(data);
                        $("#selMotivoIncentivo").selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: datos.oLstConstante });
                        $(".modal-title strong").html("<strong>Incentivo: " + lsCodRiesgo + "</strong>");
                        $("#inMontoIncPaga").val(lnMontoPropuesto);
                        $("#modal-gestion-incentivo").modal("show");
                    },
                });
            }
        },
        {
            Columna: "Option3", id: "id-modificar-gestionar-incentivo", claseIcono: "entypo-pencil", clase: "modificar-gestionar-incentivo", titulo: "Modificar", function: function (e) {
                var lnNroRiesgo = $(e).parents("tr").find("td").eq(1).html();
                var lsCodRiesgo = $(e).parents("tr").find("td").eq(2).html();
                $.fn.Conexion({
                    direccion: '/RiesgoOperacional/ObtenerDetalleIncentivo',
                    datos: { pnNroRiesgo: eval(lnNroRiesgo) },
                    terminado: function (data) {
                        $(".modal-title strong").html("<strong>Incentivo: " + lsCodRiesgo + "</strong>");
                        $("#selMotivoIncentivo").selselectboxit({ dataShow: "cConsDescripcion", dataValue: "nConsValor", datalist: data.oTpoIncentivo, dataselect: data.oIncentivoDet.oTipoIncentivo.nConsValor });
                        $("#inComentario").val(data.oIncentivoDet.cComentarioIncentivo);
                        $("#inMontoIncPaga").val(data.oIncentivoDet.nMontoIncentivo).prop("disabled", false);
                        $("#modal-gestion-incentivo").modal("show");
                    },
                });
            }
        }
    ];

    $("#content-tbl-gestion-riesgo-incentivo").Tabla({
        tblId: "tbl-gestion-riesgo-incentivo",
        cabecera: "NroRiesgo,<strong>Código Riesgo</strong>,<strong>Riesgo Identificado</strong>,<strong>Usuario Reporta</strong>,nValorRiesgoInherente,<strong>Nivel Riesgo Inherente</strong>,<strong>Posible Pérdida</strong>,<strong>Fecha Detec.</strong>,<strong>Monto Propuesto</strong>,<strong>Monto Incentivo</strong>",
        campos: "oDatosRiesgo.nNroRiesgo,oDatosRiesgo.cCodRiesgo,oDatosRiesgo.cRiesgoIdentiticado,oDatosRiesgo.oUsuarios.cUser,oNivelRiesgoInherente.nConsValor,oNivelRiesgoInherente.cConsDescripcion,oMontoPerdida.nMontoPerdida,oDatosRiesgo.dFechaDeteccion,nMontoPropuesto,nMontoIncentivo",
        datos: dataList,
        classtbl: "table table-hover datatable",
        alineado: "C,C,L,C,C,L,C,C,C,C",
        formato: "0,0,0,0,0,0,2,3,2,2",
        controles: "0,0,4,0,0,0,0,0,0,0",
        visible: "0,1,1,1,0,1,1,1,1,1",
        anchocolumna: "0,10,25,8,0,10,8,8,8,8",
        sindata: "No se encontró información para mostrar",
        numerado: "Si",
        paginacion: "Si",
        ajustar: 'No',
        paginacion: { filas: 5, pagina: 1, nombre: 'tabla-gestion-riesgo-incentivo' },
        opciones: $opciones
    });

    if (dataList != null) { SumaMontosIncentivos(); }
}

function SumaMontosIncentivos() {
    var incPagar = 0, incPropuesto = 0;
    $("#tbl-gestion-riesgo-incentivo tbody").find("tr").each(function (e) {
        incPropuesto += numeral($(this).find("td").eq(9).text()).value();
        incPagar += numeral($(this).find("td").eq(10).text()).value();

    });
    $("#td_MontoPropuesto").html("<strong>" + numeral(incPropuesto).format("0,0[.]00") + "</strong>");
    $("#td_MontoPagado").html("<strong>" + numeral(incPagar).format("0,0[.]00") + "</strong>");
}



$("#btnProcesar").click(function (e) {
    e.preventDefault();

    if (!ValidarCamposFiltro("P")) { return; }

    var lnTrimestre = $("#selTrimestre").val();
    var lnAnio = $("#inAnio").val();

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/MostrarRiesgosOperacionalesIncentivos',
        datos: { pnTrimestre: lnTrimestre, pnAnio: lnAnio },
        bloqueo: true,
        mensaje: 'Obteniendo la información. Por favor espere.',
        terminado: function (data) {
            datos = JSON.parse(data);

            MostrarRiesgosOperacionales(datos.oLstIncentivos);
        },
    });

    //return new Promise(function (resolve, reject) {
      
    //});



});


$("#btnGrabaGestion").click(function (e) {
    e.preventDefault();

    if (!ValidarCamposFiltro("G")) { return; }

    var lnNroRiesgo = $("#tbl-gestion-riesgo-incentivo tr.seleccionado").find("td").eq(1).html();
    var lnMotivoIncentivo = $("#selMotivoIncentivo").val();
    var lsComentario = $("#inComentario").val();
    var lnMontoIncentivo = $("#inMontoIncPaga").val();
    var lnDocumento = $("#FileAttach tbody tr .tdTmpFile").length;//$("#inArchivoAdjunto").val();
    var loArchivo = gFiles.getData();

    //Adjuntamos en documento
    if (lnDocumento > 0) {
        $.ajax({
            url: '/RiesgoOperacional/AdjuntarDocumento',
            type: 'POST',
            data: loArchivo,
            beforeSend: function () {
                dialog = bootbox.dialog({
                    message: '<p class="text-center mb-0"><i class="fa fa-spin fa-spinner"></i> Subiendo archivo al servidor...</p>',
                    closeButton: false,
                    //classextra: true
                });
            },
            success: function (data) {
                try {
                    var oDocAdj = JSON.parse(data);
                    $.fn.Conexion({
                        direccion: '/RiesgoOperacional/GrabaGestionIncentivo',
                        datos: { pnNroRiesgo: lnNroRiesgo, pnMotivo: lnMotivoIncentivo, psComentario: lsComentario, psMontoInc: parseFloat(lnMontoIncentivo), psNombreDoc: oDocAdj.lsNombreArchivo, psNombreDocDB: oDocAdj.lsNombreArchivoDB, pnEstado: 1 },
                        terminado: function (data) {
                            if (data == -1) {
                                $.fn.MensajeProcesos({
                                    posicion: 'A',
                                    mensaje: 'Encontramos registros de una gestión anterior. Por favor intente realizar la actualización de la gestión',
                                    titulo: 'Gestión de Incentivo - Registro',
                                    tipo_mensaje: 'informacion'
                                });
                            } else {
                                $("#modal-gestion-incentivo").modal("hide");
                                $("#btnProcesar").click();
                            }
                            dialog.modal('hide');
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
            direccion: '/RiesgoOperacional/GrabaGestionIncentivo',
            datos: { pnNroRiesgo: lnNroRiesgo, pnMotivo: lnMotivoIncentivo, psComentario: lsComentario, psMontoInc: parseFloat(lnMontoIncentivo), psNombreDoc: "", psNombreDocDB: "" },
            terminado: function (data) {
                if (data == -1) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'Encontramos registros de una gestión anterior. Por favor intente realizar la actualización de la gestión',
                        titulo: 'Gestión de Incentivo - Registro',
                        tipo_mensaje: 'informacion'
                    });
                } else {
                    $("#modal-gestion-incentivo").modal("hide");
                    $("#btnProcesar").click();
                }
                //$("#btnCerrarAddControlRiesgoResidual").click();
            },
        });

    }



});


$("#btnGenerarReporte").click(function (e) {
    e.preventDefault();

    var parameters = {
        pnTrimestre: $("#selTrimestre").val(),
        pnAnio: $("#inAnio").val()
    };

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVistaParam',
        datos: { Vista: "ExportarInformeIncentivo", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(parameters) },
        terminado: function (data) {
            window.location = data.url;
        },
    });

});


function ValidarCamposFiltro(filtro = "G") {
    var validacion = true;
    if (filtro == "G") {
        //validacion = 
        if (!$.fn.ValidarInput({ html: "#selMotivoIncentivo", isSelect: true })) {
            $("label[for='selMotivoIncentivo']").addClass("validar");
            validacion = false;
        } else {
            $("label[for='selMotivoIncentivo']").removeClass("validar");
        }

        if (!$.fn.ValidarInput({ html: "#inMontoIncPaga" })) {
            $("label[for='inMontoIncPaga']").addClass("validar")
            validacion = false;
        } else {
            $("label[for='inMontoIncPaga']").removeClass("validar")
        }
    } else if (filtro == "P") {
        if (!$.fn.ValidarInput({ html: "#selTrimestre", isSelect: true })) {
            $("label[for='selTrimestre']").addClass("validar");
            validacion = false;
        } else {
            $("label[for='selTrimestre']").removeClass("validar");
        }

        if (!$.fn.ValidarInput({ html: "#inAnio" })) {
            $("#inAnio").addClass("validar")
            validacion = false;
        } else {
            $("#inAnio").removeClass("validar")
        }
    }
    return validacion;
}