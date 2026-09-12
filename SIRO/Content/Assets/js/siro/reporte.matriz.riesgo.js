window.onload = function () {
    
    $("#selTpoRiesgo").change();

}

$("#selTpoRiesgo").change(function () {
    MostrarMatriz(null, $(this).val());
});

$("#btnGenerarMatriz").click(function (e) {
    e.preventDefault();

    var lnTpoRiesgo = $("#selTpoRiesgo").val(); //$('input:radio[name=optsTpoRiesgo]:checked').val();
    if (!ValidarFiltroMatriz(lnTpoRiesgo)) { return; }
    var filtro = ObtenerFiltroFechasMatriz();
    $.fn.Conexion({
        direccion: '/Reporte/ObtenerDatosMatrizRiesgo',
        datos: { pnTpoRiesgo: lnTpoRiesgo, pbFiltro: filtro.aplicar, psAreaCod: $("#selArea").val(), psDesde: filtro.desde, psHasta: filtro.hasta },
        bloqueo: true,
        mensaje: 'Por favor espere. Estamos obteniendo la información.',
        terminado: function (data) {
            datos = JSON.parse(data);
            MostrarMatriz(datos.oLstMatrizRiesgos, lnTpoRiesgo);
        },
    });
});

function MostrarMatriz(dataList = null, tpoRiesgo = 1) {
    var ConfigTabla = null;
    if (tpoRiesgo == 1) {
        ConfigTabla = {
            _Cabecera: "Nro Riesgo,<strong>Código</strong>,<strong>Riesgo Identificado</strong>,<strong>Usuario</strong>,<strong>Agencia Afectada</strong>,<strong>Área Afectada</strong>,<strong>Fecha Detec.</strong>",
            _Campos: "nNroRiesgo,cCodRiesgo,cRiesgoIdentiticado,cUser,cAgeDescripcion,cAreaDescripcion,dFechaDeteccion",
            _Alineado: "C,C,L,C,C,C,C",
            _Formato: "0,0,0,0,0,0,3",
            _Controles: "0,0,4,0,0,0,0",
            _Visible: "0,1,1,1,1,1,1",
            _AnchoCol: "0,10,25,8,10,10,10",
        };
    } else if (tpoRiesgo == 2) {
        ConfigTabla = {
            _Cabecera: "Nro Riesgo,<strong>Código</strong>,<strong>Riesgo Identificado</strong>,<strong>Usuario</strong>,<strong>Tipo Evaluación</strong>,<strong>Fecha Registro</strong>",
            _Campos: "nNroRiesgo,cCodRiesgo,cRiesgoIdentiticado,cUser,cEvalDesc,dFechaRegistro",
            _Alineado: "C,C,L,C,C,C",
            _Formato: "0,0,0,0,0,3",
            _Controles: "0,0,4,0,0,0",
            _Visible: "0,1,1,1,1,1",
            _AnchoCol: "0,10,25,8,10,10"
        };
    }

    $("#content-tbl-matriz-riesgo").Tabla({
        tblId: "tbl-matriz-riesgo",
        cabecera: ConfigTabla._Cabecera,
        campos: ConfigTabla._Campos,
        datos: dataList,
        alineado: ConfigTabla._Alineado,
        formato: ConfigTabla._Formato,
        controles: ConfigTabla._Controles,
        visible: ConfigTabla._Visible,
        anchocolumna: ConfigTabla._AnchoCol,
        sindata: "No se encontrol información",
        numerado: "Si",
        paginacion: "Si",
        ajustar: 'No',
        paginacion: { filas: 5, pagina: 1, nombre: 'tabla-matriz-riesgo' }
    });
}

$("#btnGenerarReporte").click(function (e) {
    e.preventDefault();
    var btn = $(this);


    var lnTpoRiesgo = $("#selTpoRiesgo").val();//$('input:radio[name=optsTpoRiesgo]:checked').val();

    if (!ValidarFiltroMatriz(lnTpoRiesgo)) { return; }
    
    var filtro = ObtenerFiltroFechasMatriz();
    var ObjParam = {
        pnTpoRiesgo: lnTpoRiesgo,//eval($('input:radio[name=optsTpoRiesgo]:checked').val()),
        pbFiltro: filtro.aplicar,
        psAreaCod: $("#selArea").val(),
        psDesde: filtro.desde,
        psHasta: filtro.hasta
    };

    //$.fn.Conexion({
    //    direccion: '/RiesgoOperacional/DevolverVistaParam',
    //    datos: { Vista: "ExportarInformeMatrizRiesgo", Controlador: "Reporte", Parametros: JSON.stringify(ObjParam) },
    //    //bloqueo: true,
    //    terminado: function (data) {
    //        //window.location = data.url;
    //        //setTimeout(function () {
    //        //    btn.prop("disabled", false);
    //        //}, 1000);
    //    },
    //});


    $.fn.Conexion({
        direccion: '/Reporte/ExportarInformeMatrizRiesgo',
        datos: { pnTpoRiesgo: ObjParam.pnTpoRiesgo, pbFiltro: ObjParam.pbFiltro, psAreaCod: ObjParam.psAreaCod, psDesde: ObjParam.psDesde, psHasta: ObjParam.psHasta },
        bloqueo: true,
        mensaje: 'Exportando la información',
        terminado: function (data) {
            window.location = data.url;
            //setTimeout(function () {
            //    btn.prop("disabled", false);
            //}, 1000);
        },
    });

});

function ObtenerFiltroFechasMatriz() {
    var desde = $.trim($("#inDesde").val());
    var hasta = $.trim($("#inHasta").val());
    var aplicar = desde !== "" && hasta !== "";
    return { aplicar: aplicar, desde: aplicar ? desde : "", hasta: aplicar ? hasta : "" };
}

function ValidarFiltroMatriz() {
    $("#inDesde, #inHasta").removeClass("validar");
    var filtro = ObtenerFiltroFechasMatriz();
    if (!filtro.aplicar) { return true; }

    var desde = moment(filtro.desde, "DD/MM/YYYY", true);
    var hasta = moment(filtro.hasta, "DD/MM/YYYY", true);
    if (!desde.isValid() || !hasta.isValid() || desde.isAfter(hasta)) {
        $("#inDesde, #inHasta").addClass("validar");
        $.fn.MensajeProcesos({
            posicion: 'A',
            mensaje: 'Ingrese fechas v?lidas en formato dd/mm/aaaa; Desde no debe ser posterior a Hasta.',
            titulo: 'Validaci?n de fechas',
            tipo_mensaje: 'advertencia'
        });
        return false;
    }
    return true;
}
