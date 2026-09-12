window.onload = function () {
    RenderizarTablaMatrizRiesgoEventoPerdida(null);
}

$("#inDesde, #inHasta").on({
    "keyup": function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == "13") {
            event.preventDefault();
            $("#btnGenerarMatriz").click();
        }
    }
});

function ValidarCampos() {
    var validacion = true;
    if (!moment(ValidarFormatoFecha(inDesde.value)).isValid()) {
        $.fn.MensajeProcesos({
            posicion: 'A',
            mensaje: 'La fechas ingresada no tiene el formato correcto.',
            titulo: 'Campo "Desde"',
            tipo_mensaje: 'informacion'
        });
        $("#inDesde").focus();
        validacion = false;
    }
    if (!moment(ValidarFormatoFecha(inHasta.value)).isValid()) {
        $.fn.MensajeProcesos({
            posicion: 'A',
            mensaje: 'La fechas ingresada no tiene el formato correcto.',
            titulo: 'Campo "Hasta"',
            tipo_mensaje: 'informacion'
        });
        $("#inHasta").focus();
        validacion = false;
    }
    return validacion;
}



$("#btnGenerarMatriz").click(function (e) {
    e.preventDefault();
    if (!ValidarCampos()) { return false; }
    $.fn.Conexion({
        direccion: '/Reporte/ListaMatrizEventoPerdida',
        datos: { psDesde: $("#inDesde").val(), psHasta: $("#inHasta").val() },
        bloqueo: false,
        terminado: function (data) {
            datos = JSON.parse(data);

            RenderizarTablaMatrizRiesgoEventoPerdida(datos);
            // $("#content-tbl-matriz-evento-perdida").Tabla({
            //     tblId: "tbl-matriz-evento-perdida",
            //     cabecera: "NroRiesgo,<strong>Código</strong>,<strong>Descripción</strong>,<strong>Evento Pérdida</strong>,<strong>Sub Evento Pérdida</strong>,<strong>Fecha Desc.</strong>,<strong>Monto Bruto</strong>,<strong>Pérdida Neta</strong>,<strong>Cta Cont.</strong>,<strong>Año</strong>",
            //     campos: "oDatosRiesgo.nNroRiesgo,oDatosRiesgo.cCodRiesgo,oDatosRiesgo.cRiesgoIdentiticado,oClasesEventoPerdida.cDescClasEvento,oClasesEventoPerdida.oSubClaseEventoPerdida.cDescSubClasEvento,dFechaOcurrencia,nMontoBruto,nPerdidaNeta,cCtaContDesc,cAnio",
            //     datos: datos,
            //     classtbl: "table table-responsive responsive",
            //     alineado: "C,C,L,C,C,C,D,D,C,C",
            //     formato: "0,0,0,0,0,3,2,2,0,0",
            //     controles: "0,0,4,0,0,0,0,0,0,0",
            //     visible: "0,1,1,1,1,1,1,1,1,1",
            //     anchocolumna: '0,8,25,12,8,8,8,8,8,8',
            //     sindata: "No se encontró información para mostrar",
            //     numerado: "Si",
            //     paginacion: "Si",
            //     ajustar: 'No',
            //     paginacion: { filas: 5, pagina: 1, nombre: "tabla-matriz-eventos-perdida" },
            //     //opciones: $opciones

            // });
        }
    });
});

$("#btnGenerarReporte").click(function (e) {
    e.preventDefault();
    if (!ValidarCampos()) { return false; }
    var getParam = {
        psDesde: $("#inDesde").val(),//eval($('input:radio[name=optsTpoRiesgo]:checked').val()),
        psHasta: $("#inHasta").val()
    };

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVistaParam',
        datos: { Vista: "ExportarMatrizEventoPerdida", Controlador: "Reporte", Parametros: JSON.stringify(getParam) },
        //bloqueo: true,
        terminado: function (data) {
            window.location = data.url;
            //setTimeout(function () {
            //    //btn.prop("disabled", false);
            //}, 1000);
        },
    });
});

function RenderizarTablaMatrizRiesgoEventoPerdida(pDatos) {
    $("#content-tbl-matriz-evento-perdida").Tabla({
        tblId: "tbl-matriz-evento-perdida",
        cabecera: "NroRiesgo,<strong>Código</strong>,<strong>Descripción</strong>,<strong>Evento Pérdida</strong>,<strong>Sub Evento Pérdida</strong>,<strong>Fecha Desc.</strong>,<strong>Monto Bruto</strong>,<strong>Pérdida Neta</strong>,<strong>Cta Cont.</strong>,<strong>Año</strong>",
        campos: "oDatosRiesgo.nNroRiesgo,oDatosRiesgo.cCodRiesgo,oDatosRiesgo.cRiesgoIdentiticado,oClasesEventoPerdida.cDescClasEvento,oClasesEventoPerdida.oSubClaseEventoPerdida.cDescSubClasEvento,dFechaDescubrimiento,nMontoBruto,nPerdidaNeta,cCtaCont,cAnio",
        datos: pDatos,
        classtbl: "table table-responsive responsive",
        alineado: "C,C,L,C,C,C,C,C,C,C",
        formato: "0,0,0,0,0,3,2,2,0,0",
        controles: "0,0,4,0,0,0,0,0,0,0",
        visible: "0,1,1,1,1,1,1,1,1,1",
        anchocolumna: '0,8,25,12,8,8,8,8,8,8',
        sindata: "No se encontró información para mostrar",
        numerado: "Si",
        paginacion: "Si",
        ajustar: 'No',
        paginacion: { filas: 5, pagina: 1, nombre: "tabla-matriz-eventos-perdida" },
        //opciones: $opciones

    });
}