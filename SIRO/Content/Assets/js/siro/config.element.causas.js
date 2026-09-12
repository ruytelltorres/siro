
$('#inCodEvaluacion').keyup(function () {
    $.uiTableFilter('#tbl-causas-riesgos-gestion', this.value);
});


$("#btnAgregarCausasRiesgo").click(function (e) {
    e.preventDefault();
    $("#btnRegCausaRiesgo").show();
    $("#btnActCausaRiesgo").hide();
    $("#inCausaDesc").val("");
    //jQuery('#modal-registro-causas-riesgos').modal('show', { backdrop: 'static' });
    $("#modal-registro-causas-riesgos").modal("show");
});

//function MostrarModalAddCausaRiesgo() {
//    $("#btnRegCausaRiesgo").show();
//    $("#btnActCausaRiesgo").hide();
//    $("#inCausaDesc").val("");

//    jQuery('#modal-registro-causas-riesgos').modal('show', { backdrop: 'static' });
//}

function MostrarCausasRiesgo(datos) {
    var $opciones = [
        {
            Columna: "Tipo1", id: "id-editar-causa-riesgo", claseIcono: "entypo-pencil", clase: "editar-causa-riesgo", titulo: "Editar", function: function (e) {
                $("#btnRegCausaRiesgo").hide();
                $("#btnActCausaRiesgo").show();

                $("#inCausaDesc").val($(e).parents("tr").find("td").eq(2).html());

                $("#modal-registro-causas-riesgos").modal("show");
            }
        },
        {
            Columna: "Tipo2", id: "id-eliminar-causa-riesgo", claseIcono: "entypo-trash", clase: "eliminar-causa-riesgo", titulo: "Eliminar", function: function (e) {
                var lsCodCausa = $(e).parents("tr").find("td").eq(1).html();

                bootbox.confirm({
                    message: "<strong>¿Está seguro de eliminar la causa del riesgo?</strong>",
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
                                direccion: '/Configuracion/RegistrarGestionCausasRiesgo',
                                datos: { psCodCausa: lsCodCausa, psCausaDesc: '', pnAccion: 3 },
                                //bloqueo: true,
                                terminado: function (data) {
                                    datos = JSON.parse(data);
                                    MostrarCausasRiesgo(datos);
                                    $.fn.MensajeProcesos({
                                        clase: 'green',
                                        posicion: 'A',
                                        mensaje: 'Se eliminó correctamente la causa de riesgo.',
                                        titulo: 'Causa del Riesgo ' + lsCodCausa,
                                        tipo_mensaje: 'informacion'
                                    });
                                    $("#btnCancelarCausaRiesgo").click();
                                },
                                //bloqueo: false
                            });
                        }
                    }
                });
            }
        },
    ];

    if (datos.LstCausasRiesgos != null) {
        $("#content-tbl-causas-riesgos-gestion").Tabla({
            tblId: "tbl-causas-riesgos-gestion",
            cabecera: "<strong>Código</strong>,<strong>Descripción</strong>,<strong>Fecha Reg</strong>,<strong>Estado</strong>",
            campos: "cCodCausa,cCausaDesc,dFechaReg,bEstado ? 'Activo' : 'Inactivo'",
            //scrollVertical: "Si",
            //subLista: "Si",
            datos: datos.LstCausasRiesgos,
            classtbl: "table table-responsive responsive",
            //cantRegVertical: 5,
            alineado: "C,L,C,C",
            formato: "0,0,3,0",
            controles: "0,0,0,0",
            visible: "1,1,1,1",
            anchocolumna: "10,40,15,10",
            numerado: "Si",
            ajustar: "No",
            paginacion: "Si",
            paginacion: { filas: 5, pagina: 1, nombre: 'tabla-causas-riesgo' },
            opciones: $opciones
        });
    }
}

$("#btnRegCausaRiesgo").click(function (e) {
    e.preventDefault();
    if (!ValidarCamposCausaRiesgo()) { return; }

    var lsCausaDesc = $("#inCausaDesc").val();

    $.fn.Conexion({
        direccion: '/Configuracion/RegistrarGestionCausasRiesgo',
        datos: { psCodCausa: '', psCausaDesc: lsCausaDesc, pnAccion: 1 },
        //bloqueo: true,
        terminado: function (data) {
            datos = JSON.parse(data);
            MostrarCausasRiesgo(datos);
            $.fn.MensajeProcesos({
                clase: 'green',
                posicion: 'A',
                mensaje: 'Datos de la causas de riesgo registrado correctamente.',
                titulo: 'Causas de los Riesgos',
                tipo_mensaje: 'exito'
            });
            $("#btnCancelarCausaRiesgo").click();
        },
        //bloqueo: false
    });
});

$("#btnActCausaRiesgo").click(function (e) {
    e.preventDefault();
    if (!ValidarCamposCausaRiesgo()) { return; }

    var lsCodCausa = $("#tbl-causas-riesgos-gestion tr.seleccionado").find("td").eq(1).html();
    var lsCausaDesc = $("#inCausaDesc").val();

    $.fn.Conexion({
        direccion: '/Configuracion/RegistrarGestionCausasRiesgo',
        datos: { psCodCausa: lsCodCausa, psCausaDesc: lsCausaDesc, pnAccion: 2 },
        //bloqueo: true,
        terminado: function (data) {
            datos = JSON.parse(data);
            MostrarCausasRiesgo(datos);
            $.fn.MensajeProcesos({
                clase: 'green',
                posicion: 'A',
                mensaje: 'Datos de la causas de riesgo registrado correctamente.',
                titulo: 'Causas de los Riesgos',
                tipo_mensaje: 'informacion'
            });
            $("#btnCancelarCausaRiesgo").click();
        },
        //bloqueo: false
    });
});

$("#btnCancelarCausaRiesgo").click(function (e) {
    e.preventDefault();

    $("#inCausaDesc").val("");
    $("#modal-registro-causas-riesgos").modal("hide");
});

//$(document).on("click", ".editarCausaRiesgo", function (e) {


//});

//$(document).on("click", ".eliminarCausaRiesgo", function (e) {

//});

function ValidarCamposCausaRiesgo() {
    var validacion = true;

    if (!$.fn.ValidarInput({ html: "#inCausaDesc" })) {
        $("#inCausaDesc").parent(this).addClass("has-error");
        validacion = false;
    } else {
        $("#inCausaDesc").parent(this).removeClass("has-error");
    }

    return validacion;
}

