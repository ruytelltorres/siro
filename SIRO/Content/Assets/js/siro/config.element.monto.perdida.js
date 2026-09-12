
$("#inMontoPerdida").on({
    "keyup": function (event) {
        $(event.target).val(function (index, value) {
            return value.replace(/\D/g, "")
                .replace(/([0-9])([0-9]{2})$/, '$1.$2')
                .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");
        });
    }
});

$("#selProbabilidad, #selImpacto").change(function () {
	var probabilidad = $("#selProbabilidad");
	var impacto = $("#selImpacto");
	if (probabilidad.val() != null && impacto.val() != null) {
		ObtenerNivelRiesgoInherente(probabilidad.val(), impacto.val());
	} else {
		$("#td_NivelRiesgoInherente").html("");
		$("#td_MontoPerdida").html("");
		$("#td_EstadoMontoPerdida").html("");
	}
});


function ObtenerNivelRiesgoInherente(nProbabilidad, nImpacto) {
	$.fn.Conexion({
		direccion: '/RiesgoOperacional/ObtenerNivelRiesgoInherente',
		datos: { pnProbabilidad: nProbabilidad, pnImpacto: nImpacto },
		terminado: function (data) {
			let datos = JSON.parse(data);
			if (datos.oRiesgoInherente.nValorEscala > 0 && datos.oMontoPerdida != null && datos.oLstMontoPerdida != null) {
				DatosMontoPerdida(datos.oLstMontoPerdida);
                $(".btn").attr("disabled", false);
            }
        },
        error: function () {
            DatosMontoPerdida(null);
            $(".btn").attr("disabled", true);
        }
	});
}

function DatosMontoPerdida(datos) {
	$("#conten-tbl-montos-perdida").Tabla({
		tblId: "tbl-montos-perdida",
		cabecera: "nMontPerdCod,<strong>Monto de Pérdida</strong>,<strong>Reseña del Cese</strong>,<strong>Estado</strong>,<strong>Fecha de Reg.</strong>,<strong>Fecha de Cese</strong>",
		campos: "nMontPerdCod,nMontoPerdida,cComentario,bEstado?'Vigente':'Cesado',dFechaRegistro,dFechaCese",
		datos: datos,
		classtbl: "table table-responsive responsive",
		alineado: "C,C,L,C,C,C",
		formato: "0,2,0,0,3,3",
		controles: "0,0,0,0,0,0",
		visible: "0,1,1,1,1,1",
		anchocolumna: "0,10,30,20,10,10",
		sindata: "No se encontró información.",
		numerado: "Si",
		ajustar: 'Si',
		paginacion: "Si",
		paginacion: { filas: 10, pagina: 1, nombre: 'tabla-montos-perida' }
	});
}

$("#btnRegMontoPerdida").click(function (e) {
	e.preventDefault();

	var validacion = true;
	validacion = $.fn.ValidarInput({ html: "#selProbabilidad", isSelect: true });
	validacion = $.fn.ValidarInput({ html: "#selImpacto", isSelect: true });
    validacion = $.fn.ValidarInput({ html: "#inMontoPerdida" });

	if (!validacion) {
		return false;
	}
    
    var inMontoPerdida = $("#inMontoPerdida");
    var inDescCese = $("#inDescCese");

    $.fn.Conexion({
        direccion: '/Configuracion/GrabarNuevoMontoPerdida',
        datos: {
            pnProbabilidad: $("#selProbabilidad").val(),
            pnImpacto: $("#selImpacto").val(),
            pnMontoPerdida: numeral(inMontoPerdida.val()).value(),
            psComentarios: inDescCese.val()
        },
        terminado: function (data) {
            datos = JSON.parse(data);
            DatosMontoPerdida(datos.oLstObjeto);

            $("#btnCerrar").click();

            $.fn.MensajeProcesos({
                posicion: 'A',
                tipo_mensaje: datos.TipoNotif,
                mensaje: datos.MensajeNotif,
                titulo: 'Monto de Pérdida',
                onhidden: function () {
                    inMontoPerdida.val("");
                    inDescCese.val("");        
                }
            });
        },
        error: function (fail) {
            datos = JSON.parse(fail);
            $("#btnCerrar").click();
            $.fn.MensajeProcesos({
                posicion: 'A',
                tipo_mensaje: data.TipoNotif,
                mensaje: data.MensajeNotif,
                titulo: 'Monto de Pérdida',
                onhidden: function () {
                    inMontoPerdida.val("");
                    inDescCese.val("");
                    
                }
            });
        }
        
    });
});



