
function MostrarDatosCriteriosEval(datos) {
    $("#content-tbl-criterios-evaluacion").Tabla({
        tblId: "tbl-criterios-evaluacion",
        cabecera: "nValorRelacion,<strong>Código de Grupo</strong>,<strong>Descripción</strong>,<strong>Valor</strong>,<strong>Estado</strong>",
        campos: "nValorRelacion,nCriterioCod,cCriterioDesc,nCriterioValor,bEstado ? 'Activo' : 'Inactivo'",
        datos: datos,
        classtbl: "table table-responsive responsive",
        //cantRegVertical: 4,
        //scrollVertical: "Si",
        alineado: "C,C,L,C,C",
        formato: "0,0,0,0,0",
        controles: "0,0,0,0,0",
        visible: "0,1,1,1,1",
        anchocolumna: "0,10,30,20,10",
        sindata: "No se configuro los criterios de evaluaci\u00F3n",
        numerado: "Si",
        ajustar: 'No',
        //paginacion: "Si",
        //paginacion: { filas: 5, pagina: 1, nombre: 'tabla-criterios-evaluacion' }
    });



    $("#tbl-criterios-evaluacion tbody tr").each(function () {
        if ($(this).find('td:eq(2)').text() != $(this).find('td:eq(4)').text()) {
            //$(this).find("first").css("background-color", "red");
            var tmpValor = $(this).find('td:eq(4)').text();
            $(this).find('td:eq(4)').empty();
            $(this).find('td:eq(4)').append('<input type="text" class="form-control text-center" value="' + tmpValor + '" onkeypress="return val_09(event);" maxlength="1"/>');

        }

    });


}

$("#btnGrabaConfCreiterioEval").click(function (e) {
    bootbox.confirm({
        message: "<strong>¿Está seguro de grabar la configuración para los criterios de evaluación?</strong>",
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
                var loCriterio = [], nRecorrido = 0;
                $("#tbl-criterios-evaluacion tbody tr").each(function () {
                    nRecorrido = nRecorrido + 1;
                    if ($(this).find('td:eq(2)').text() != $(this).find('td:eq(4)').text()) {
                        loCriterio[nRecorrido] = {
                            Cod: eval($(this).find('td:eq(2)').text()),
                            Rel: eval($(this).find('td:eq(1)').text()),
                            Val: eval($($(this).find("td:eq(4)").children("input")[0]).val())
                        };
                    }
                });

                $.fn.Conexion({
                    direccion: '/Configuracion/GrabarConfiguracionCriterioEval',
                    datos: { poConfigCriterioEval: JSON.stringify(loCriterio) },
                    bloqueo: true,
                    mensaje: 'Grabando la configuración de los criterios de evaluación',
                    terminado: function (data) {
                        datos = JSON.parse(data.Data);
                        if (data.Exito > 0) {
                            $.fn.MensajeProcesos({
                                clase: 'green',
                                posicion: 'A',
                                mensaje: 'Se grabo correctamente la configuracion de los criterios de evaluación',
                                titulo: 'Criterios Evaluación',
                                tipo_mensaje: 'exito'
                            });
                            MostrarDatosCriteriosEval(datos.oLstCriteriosEval);
                        } else {
                            $.fn.MensajeProcesos({
                                posicion: "A",
                                mensaje: "Problemas al grabar la configuración de los criterios de evaluación",
                                titulo: "Criterios Evaluación",
                                tipo_mensaje: "adventencia"
                            });
                        }
                    },
                });






            }
        }
    });
});

