var gnFilasPorPagina = 10;
var gLstDatosEvaluaciones = null;

window.onload = function () {
    $.fn.Conexion({
        direccion: "/Evaluacion/ListaEvaluacionesEnProceso",
        datos: { pnTpoBuscar: 0, psValorBuscar: "" },
        mensaje: " Obteniendo las evaluaiones en proceso de gestión",
        terminado: function (data) {
            datos = JSON.parse(data);
            gLstDatosEvaluaciones = datos.oLstDatosEvaluaciones;
            RenderizarTablaEvaluaciones(gnFilasPorPagina);
        },
    });

}

function RenderizarTablaEvaluaciones(pnFilas) {
    $("#content-tbl-evaluacion-lista-gestion").Tabla({
        tblId: "tbl-evaluacion-lista-gestion",
        cabecera: "Nro Riesgo,<strong>Código Riesgo</strong>,<strong>Riesgo Identificado</strong>,<strong>Usuario</strong>,<strong>Tipo Eval.</strong>,<strong>Fecha Reg.</strong>,<strong>Estado</strong>",
        campos: "nNroRiesgo,cCodRiesgo,cRiesgoIdentiticado,oUsuario.cUser,oTipoEvaluacion.cConsDescripcion,dFechaRegistro,oProcRiesgo.cConstDescripcion",
        datos: gLstDatosEvaluaciones,
        classtbl: "table table-responsive responsive",
        alineado: "C,C,L,C,C,C,C",
        formato: "0,0,0,0,0,3,0",
        controles: "0,0,4,0,0,0",
        visible: "0,1,1,1,1,1,1",
        anchocolumna: "0,10,30,10,10,10,10",
        sindata: "No se encontró información para mostrar",
        numerado: "Si",
        ajustar: 'No',
        paginacion: { filas: pnFilas, pagina: 1, nombre: 'tbl-evaluacion-lista-gestion' },
        opciones: [
            {
                Columna: "OpcDetalle", id: "id-detalle-evaluacion", claseIcono: "entypo-info-circled", clase: "detalle-evaluacion", titulo: "Detalle", function: function (e) {
                    var ObjParam = {
                        evaluacion: base64_encode($(e).parents("tr").find("td").eq(1).html())
                    };
                    $.fn.Conexion({
                        direccion: '/RiesgoOperacional/DevolverVistaParam',
                        datos: { Vista: "Detalle", Controlador: "Evaluacion", Parametros: JSON.stringify(ObjParam) },
                        terminado: function (data) {
                            window.location = data.url;
                        },
                    });
                }
            },
            {
                Columna: "OpcGestionar", id: "id-gestionar-evaluacion", claseIcono: "entypo-cog", clase: "gestionar-evaluacion", titulo: "Gestionar", function: function (e) {
                    var ObjParam = {
                        evaluacion: base64_encode($(e).parents("tr").find("td").eq(1).html())
                    };
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
}

$('#selMostrarRegistros').on('change', function () {
    gnFilasPorPagina = parseInt($(this).val(), 10) || 10;

    if (!gLstDatosEvaluaciones) {
        return;
    }

    RenderizarTablaEvaluaciones(gnFilasPorPagina);
    $('#inBuscar').val('');
});

$('#inBuscar').keyup(function () {
    $.uiTableFilter('#tbl-evaluacion-lista-gestion', this.value);
});





$("#selTpoEvaluacion").change(function () {
   var lnTpoEvalucion = $(this).val();
   switch (eval(lnTpoEvalucion)) {
       case 1001:
           $.fn.Conexion({
               direccion: '/Evaluacion/DevolverVistaParcial',
               //datos: { VistaParcial: "_FiltrarEvaluacionesxProceso", Modelo: gModeloVista.oLstAgencias},
               datos: { VistaParcial: "_FiltrarEvaluacionesxProceso", Modelo: JSON.stringify(gModeloVista.oLstAreas) },
               mensaje: "Por favor espere !!!, estamos obteniendo la información",
               bloqueo: true,
               terminado: function (data) {
                   //datos = JSON.parse(data);
                   $("#FiltroProcesos").html(data.view);
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
           $("#FiltroProcesos").hide();
           break;
       default:
           $("#FiltroProcesos").hide();
           break;
   }
});


//$(document).ready(function () {
//    $("#btnBuscarEval").click();
//    //MostrarListaEvluaciones(null);

//    $("#inCodEval").on('keyup', function (e) {
//        var lsCodEval = $(this).val();
//        $("#btnBuscarEval").prop("disabled", !(lsCodEval.length == 11));
//    });
//});

//function MostrarListaEvluaciones(oLstEvaluaciones) {
//    $("#tbl-evaluacion-lista-gestion").Tabla({
//        tblId: "tbl-evaluacion-lista-gestion",
//        cabecera: "Nro Riesgo,<strong>Código Riesgo</strong>,<strong>Riesgo Identificado</strong>,<strong>Usuario</strong>,<strong>Tipo Eval.</strong>,<strong>Fecha Reg.</strong>,<strong>Estado</strong>",
//        campos: "nNroRiesgo,cCodRiesgo,cRiesgoIdentiticado,oUsuarios.cUser,oEvaluaciones.cEvalDesc,dFechaReg,cProcRiesgo",
//        datos: oLstEvaluaciones,
//        classtbl: "table table-responsive responsive",
//        //cantRegVertical: 10,
//        //scrollVertical: "Si",
//        alineado: "C,C,L,C,C,C,C",
//        formato: "0,0,0,0,0,3,0",
//        controles: "0,0,4,0,0,0",
//        visible: "0,1,1,1,1,1,1",
//        anchocolumna: "0,10,30,10,10,10,10",
//        sindata: "No se encontró registro de evaluaciones en proceso de gestión",
//        numerado: "Si",
//        paginacion: "Si",
//        ajustar: 'No',
//        paginacion: { filas: 5, pagina: 1, nombre: 'tbl-evaluacion-lista-gestion' },
//        opciones: [
//            {
//                Columna: "Tipo1", id: "id-detalle-evaluacion", claseIcono: "entypo-info-circled", clase: "detalle-evaluacion", titulo: "Ver Detalle", function: function (e) {
//                    var ObjParam = {
//                        evaluacion: base64_encode($(e).parents("tr").find("td").eq(1).html())
//                    };
//                    $.fn.Conexion({
//                        direccion: '/RiesgoOperacional/DevolverVistaParam',
//                        datos: { Vista: "DetalleEvaluacion", Controlador: "Evaluacion", Parametros: JSON.stringify(ObjParam) },
//                        terminado: function (data) {
//                            window.location = data.url;
//                        },
//                    });
//                }
//            },
//            {
//                Columna: "Tipo2", id: "idGestionarEvaluacion", claseIcono: "entypo-cog", clase: "GestionarEvaluacion", titulo: "Gestionar Eval.", function: function (e) {
//                    var ObjParam = {
//                        evaluacion: base64_encode($(e).parents("tr").find("td").eq(1).html())
//                    };
//                    $.fn.Conexion({
//                        direccion: '/RiesgoOperacional/DevolverVistaParam',
//                        datos: { Vista: "GestionEvaluacion", Controlador: "Evaluacion", Parametros: JSON.stringify(ObjParam) },
//                        terminado: function (data) {
//                            window.location = data.url;
//                        },
//                    });
//                }
//            },
//        ],
//    });
//}

//$("#btnBuscarEval").click(function (e) {
//    e.preventDefault();

//    var lnTpoBusqueda = 0;
//    var lsValorBuscar = "";

//    $.fn.Conexion({
//        direccion: "/Evaluacion/ListaEvaluacionesEnProceso",
//        datos: { pnTpoBuscar: lnTpoBusqueda, psValorBuscar: lsValorBuscar },
//        mensaje: " Obteniendo las evaluaiones en proceso de gestión",
//        terminado: function (data) {
//            datos = JSON.parse(data);
//            MostrarListaEvluaciones(datos.oLstDatosEvaluaciones);
//        },
//    });
//});





