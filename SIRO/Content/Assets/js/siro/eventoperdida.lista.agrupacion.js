var gnFilasPorPagina = 10;
var gLstEventoAgrupar = null;

window.onload = function () {

    MostrarEventoPerdidaAgrupar();
}


$("#inEventoAgrupa").on('keyup', function (e) {
    var lsCodEval = $(this).val();
    $("#btnBuscarEvento").prop("disabled", !(lsCodEval.length > 0));
});

//$(document).ready(function () {

//	ListaEventoAgrupar(null);

//	setTimeout(function () {
//		//$("#rootwizard").bootstrapWizard('remove', 4, true);
//		$("#btnGrabaPaso" + (gDatosRiesgo.nProcRiesgo + 1)).show();

//		//$(".features-blocks").hide();
//		$(".panel-options a").show();

//	}, 1);



//	$("#inBuscar").on('keyup', function (e) {
//		var lsCodEval = $(this).val();
//		$("#btnBuscarEvento").prop("disabled", !(lsCodEval.length > 0));
//	});

//});

$("#inEventoAgrupa").on({
    "focus": function (event) {
        $(event.target).select();
    },
    "keyup": function (event) {
        //$(event.target).val(function (index, value) {
        //	return value.replace(/\D/g, "")
        //        .replace(/([0-9])([0-9]{2})$/, '$1.$2')
        //        .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");
        //});

        $("#btnGrabarAgrupacion").prop("disabled", !($(this).val().length > 0));

        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            event.preventDefault();
            $("#btnGrabarAgrupacion").click();
            return false;
        }
    }
});

$('#inBuscar').keyup(function () {
    $.uiTableFilter('#tbl-evento-perdida-agrupar', this.value);
});

$('#selMostrarRegistros').on('change', function () {
    gnFilasPorPagina = parseInt($(this).val(), 10) || 10;

    if (!gLstEventoAgrupar) {
        return;
    }

    ListaEventoAgrupar(gLstEventoAgrupar);
    $('#inBuscar').val('');
});



//$('#inEventoAgrupa').keyup(function (e) {
//	var keycode = (e.keyCode ? e.keyCode : e.which);
//	if (keycode == '13') {
//		e.preventDefault();
//		$("#btnAddEvento").click();
//		return false;
//	}
//});

function ListaEventoAgrupar(datos) {
    $("#content-tbl-evento-perdida-agrupar").Tabla({
        tblId: "tbl-evento-perdida-agrupar",
        cabecera: "NroRiesgo,<strong>Código</strong>,<strong>Descripción</strong>,<strong>Evento Perdida</strong>,<strong>Sub Evento de Pérdida</strong>,<strong>Monto Bruto</strong>,<strong>Pérdida Neta</strong>,<strong>Cta Cont.</strong>,<strong>Año</strong>",
        campos: "oDatosRiesgo.nNroRiesgo,oDatosRiesgo.cCodRiesgo,oDatosRiesgo.cRiesgoIdentiticado,oClasesEventoPerdida.cDescClasEvento,oClasesEventoPerdida.oSubClaseEventoPerdida.cDescSubClasEvento,nMontoBruto,nPerdidaNeta,cCtaContDesc,cAnio",
        datos: datos,
        classtbl: "table table-responsive responsive",
        //cantRegVertical: 10,
        //scrollVertical: "Si",
        alineado: "C,C,L,C,C,D,D,C,C",
        formato: "0,0,0,0,0,2,2,0,0",
        controles: "0,0,4,0,0,0,0,0,0",
        visible: "0,1,1,1,1,1,1,1,1",
        anchocolumna: '0,8,25,10,10,8,8,8,8',
        sindata: "No se encontró información para mostrar",
        numerado: "Si",
        paginacion: "Si",
        ajustar: 'No',
        paginacion: { filas: gnFilasPorPagina, pagina: 1, nombre: "tbl-eventos-perdida-agrupar" },
        opciones: [
            {
                Columna: "OptionColumn1", id: "id-agrupar-evento", claseIcono: "entypo-link", clase: "agrupar-evento", titulo: "Agrupar Evento", function: function (e) {
                    var lnEvento = $(e).parents("tr").find("td").eq(1).html();
                    var lsEvento = $(e).parents("tr").find("td").eq(2).html();

                    $("#modal-agrupar-evento .modal-header").data("grupo-evento", lnEvento);
                    $(".modal-title").html("Agrupacion del Evento de Perdida " + lsEvento);

                    $.fn.Conexion({
                        direccion: '/EventoPerdida/ListarAgrupacionEvento',
                        datos: { pnNroRiesgo: lnEvento },
                        bloqueo: false,
                        terminado: function (data) {
                            datos = JSON.parse(data);
                            $("#content-tbl-eventos-agrupados").Tabla({
                                tblId: "tbl-eventos-agrupados",
                                cabecera: "NroRiesgo,<strong>Código</strong>,<strong>Descripción</strong>,<strong>Evento Perdida</strong>,<strong>Sub Evento de Pérdida</strong>",
                                campos: "oDatosRiesgo.nNroRiesgo,oDatosRiesgo.cCodRiesgo,oDatosRiesgo.cRiesgoIdentiticado,oClasesEventoPerdida.cDescClasEvento,oClasesEventoPerdida.oSubClaseEventoPerdida.cDescSubClasEvento",
                                datos: datos,
                                classtbl: "table table-responsive responsive",
                                alineado: "C,C,L,C,C",
                                formato: "0,0,0,0,0",
                                controles: "0,0,4,0,0,",
                                visible: "0,1,1,1,1",
                                anchocolumna: '0,8,25,10,10',
                                sindata: "No se encontró información para mostrar",
                                numerado: "Si",
                                paginacion: "Si",
                                ajustar: 'No',
                                paginacion: { filas: 5, pagina: 1, nombre: "tabla-eventos-agrupados" },
                                opciones: [
                                    {
                                        Columna: "OptionColumn1", id: "id-eliminar-evento-agrupado", claseIcono: "entypo-link", clase: "eliminar-evento-agrupado", titulo: "Elimanar Evento", function: function (e) {

                                        }
                                    }]

                            });



                        }
                    });

                    $("#modal-agrupar-evento").modal("show");
                }
            }
        ]

    });

}

function MostrarEventoPerdidaAgrupar() {
    $.fn.Conexion({
        direccion: '/EventoPerdida/ListarEventosAgrupar',
        //datos: { pnNroRiesgo: nNroRiesgo, pnNroProceso: nProceso },
        bloqueo: false,
        terminado: function (data) {
            datos = JSON.parse(data);
            gLstEventoAgrupar = datos;
            ListaEventoAgrupar(datos);
        }
    });
}



$("#btnGrabarAgrupacion").click(function (e) {
    e.preventDefault();
    var lnCodGrupo = $("#modal-agrupar-evento .modal-header").data("grupo-evento");
    var lsCodAgrupado = $("#inEventoAgrupa").val();
    $.fn.Conexion({
        direccion: "/EventoPerdida/GrabarAgrupacionEventoPerdida",
        datos: { pnNroRiesgo: lnCodGrupo, psAgrupado: lsCodAgrupado.trim() },
        //bloqueo: true,
        //mensaje: "Grabando la información, por favor espere.",
        terminado: function (data) {
            $.fn.Conexion({
                direccion: '/EventoPerdida/ListarAgrupacionEvento',
                datos: { pnNroRiesgo: lnCodGrupo },
                bloqueo: false,
                terminado: function (data) {
                    datos = JSON.parse(data);
                    $("#content-tbl-eventos-agrupados").Tabla({
                        tblId: "tbl-eventos-agrupados",
                        cabecera: "NroRiesgo,<strong>Código</strong>,<strong>Descripción</strong>,<strong>Evento Perdida</strong>,<strong>Sub Evento de Pérdida</strong>",
                        campos: "oDatosRiesgo.nNroRiesgo,oDatosRiesgo.cCodRiesgo,oDatosRiesgo.cRiesgoIdentiticado,oClasesEventoPerdida.cDescClasEvento,oClasesEventoPerdida.oSubClaseEventoPerdida.cDescSubClasEvento",
                        datos: datos,
                        classtbl: "table table-responsive responsive",
                        alineado: "C,C,L,C,C",
                        formato: "0,0,0,0,0",
                        controles: "0,0,4,0,0,",
                        visible: "0,1,1,1,1",
                        anchocolumna: '0,8,25,10,10',
                        sindata: "No se encontró información para mostrar",
                        numerado: "Si",
                        paginacion: "Si",
                        ajustar: 'No',
                        paginacion: { filas: 5, pagina: 1, nombre: "tabla-eventos-agrupados" },
                        opciones: [
                            {
                                Columna: "OptionColumn1", id: "id-eliminar-evento-agrupado", claseIcono: "entypo-link", clase: "eliminar-evento-agrupado", titulo: "Elimanar Evento", function: function (e) {

                                }
                            }]
                    });
                }
            });

            $.fn.MensajeProcesos({
                //clase: 'green',
                posicion: 'A',
                mensaje: data.MensajeSis,
                titulo: 'Evento de Pérdida - Agrupar',
                tipo_mensaje: data.Tipo,
                //onhidden: function () {
                //	//$.fn.Conexion({
                //	//	direccion: '/RiesgoOperacional/DevolverVista',
                //	//	datos: { Vista: "ListaEvenPerdida", Controlador: "EventoPerdida" },
                //	//	terminado: function (url) {
                //	//		window.location.href = url;
                //	//	},
                //	//});
                //}

            });
        },
    });

});