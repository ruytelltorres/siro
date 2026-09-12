var gnFilasPorPagina = 10;
var gLstEventosPerdida = null;

window.onload = function () {
    ListarEventosPerdida();
}

$('#inBuscar').keyup(function () {
    $.uiTableFilter('#tbl-evento-perdida', this.value);
});

$('#selMostrarRegistros').on('change', function () {
    gnFilasPorPagina = parseInt($(this).val(), 10) || 10;

    if (!gLstEventosPerdida) {
        return;
    }

    RenderizarTablaEventosPerdida(gLstEventosPerdida, gnFilasPorPagina);
    $('#inBuscar').val('');
});

function ListarEventosPerdida() {
    $.fn.Conexion({
        direccion: '/EventoPerdida/ListarEventosPerdidas',
        bloqueo: false,
        terminado: function (data) {
            datos = JSON.parse(data);
            gLstEventosPerdida = datos;
            RenderizarTablaEventosPerdida(gLstEventosPerdida, gnFilasPorPagina);
        }
    });
}

function RenderizarTablaEventosPerdida(pDatos, pnFilas) {
    var $opciones = [
        {
            Columna: "OptionColumn1", id: "id-editar-evento-perdida", claseIcono: "entypo-pencil", clase: "editar-evento-perdida", titulo: "Editar", function: function (e) {
                var ObjParam = { evento: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
                $.fn.Conexion({
                    direccion: "/RiesgoOperacional/DevolverVistaParam",
                    datos: { Vista: "Modificar", Controlador: "EventoPerdida", Parametros: JSON.stringify(ObjParam) },
                    terminado: function (data) {
                        window.location = data.url;
                    },
                });
            }
        },
        {
            Columna: "OptionColumn2", id: "id-detalle-evento-perdida", claseIcono: "entypo-info-circled", clase: "detalle-evento-perdida", titulo: "Detalle", function: function (e) {
                var ObjParam = { evento: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
                $.fn.Conexion({
                    direccion: "/RiesgoOperacional/DevolverVistaParam",
                    datos: { Vista: "Detalle", Controlador: "EventoPerdida", Parametros: JSON.stringify(ObjParam) },
                    terminado: function (data) {
                        window.location = data.url;
                    },
                });
            }
        },
        //{
        //    Columna: "OptionColumn3", id: "id-gestionar-evento-perdida", claseIcono: "entypo-cog", clase: "gestionar-evento-perdida", titulo: "Gestionar evento", function: function (e) {
        //        var ObjParam = { evento: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
        //        $.fn.Conexion({
        //            direccion: "/RiesgoOperacional/DevolverVistaParam",
        //            datos: { Vista: "GestionEventoPerdida", Controlador: "EventoPerdida", Parametros: JSON.stringify(ObjParam) },
        //            terminado: function (data) {
        //                window.location = data.url;
        //            },
        //        });
        //    }
        //},
    ];

    $("#content-tbl-evento-perdida").Tabla({
        tblId: "tbl-evento-perdida",
        cabecera: "NroRiesgo,<strong>Código</strong>,<strong>Descripción</strong>,<strong>Evento Perdida</strong>,<strong>Sub Evento de Pérdida</strong>,<strong>Fecha Desc.</strong>,<strong>Monto Bruto</strong>,<strong>Pérdida Neta</strong>,<strong>Año</strong>",
        campos: "oDatosRiesgo.nNroRiesgo,oDatosRiesgo.cCodRiesgo,oDatosRiesgo.cRiesgoIdentiticado,oClasesEventoPerdida.cDescClasEvento,oClasesEventoPerdida.oSubClaseEventoPerdida.cDescSubClasEvento,dFechaDescubrimiento,nMontoBruto,nPerdidaNeta,cAnio",
        datos: pDatos,
        classtbl: "table table-responsive responsive",
        alineado: "C,C,L,C,C,C,D,D,D,C",
        formato: "0,0,0,0,0,3,2,2,0,0",
        controles: "0,0,4,0,0,0,0,0,0,0",
        visible: "0,1,1,1,1,1,1,1,1,1",
        anchocolumna: '0,8,20,12,12,8,5,5,8,5',
        sindata: "No se encontró información para mostrar",
        numerado: "Si",
        ajustar: 'No',
        paginacion: { filas: pnFilas, pagina: 1, nombre: "tbl-eventos-perdida" },
        opciones: $opciones

    });
}

