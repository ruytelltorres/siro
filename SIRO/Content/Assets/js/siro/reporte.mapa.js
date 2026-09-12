window.onload = function () {

}


$("#btnExportar").click(function (e) {
    e.preventDefault();
    
    var param = {
        pnTpoRiesgo: $("#selTpoRiesgo").val(),
        pnTpoNivelRiesgo: $("#selNivelRiesgo").val(),
        pnProbabilidad: $(".valProbalididad").attr("nProbabilidad") ,
        pnImpacto: $(".valImpacto").attr("nImpacto")
    }

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVistaParam',
        datos: { Vista: "ExportarInformeMapaCalorDetalleRiesgo", Controlador: "Reporte", Parametros: JSON.stringify(param) },
        terminado: function (data) {
            GenerarMapaCalorRiesgo();
            window.location = data.url;
        },
    });

});


function ExportarTabla(selector, params) {
    //{ type: 'png', fileName: 'mapa-calor' }
    var fecha = new Date();
    $(selector).tableExport({
        type: 'png',
        fileName: 'mapa-calor-' + gUsuario.cUser + '-' + fecha.getHours() + fecha.getMinutes() + fecha.getMilliseconds()
    });
}


function MostrarDatosRiesgo(nProbalidad, nImpacto) {

    var selTipoiesgo = $("#selTpoRiesgo");//$('input:radio[name=optsTpoRiesgo]:checked').val();
    var selNivelRiesgo = $("#selNivelRiesgo");//$('input:radio[name=optsTpoNivRiesgo]:checked').val();

    $.fn.Conexion({
        direccion: '/Reporte/ObtenerRiesgosProbabilidadImpacto',
        datos: { pnProbabilidad: nProbalidad, pnImpacto: nImpacto, pnTpoRiesgo: selTipoiesgo.val(), pnTpoNivelRiesgo: selNivelRiesgo.val() },
        bloqueo: true,
        terminado: function (data) {
            datos = JSON.parse(data);

            $("#tbl-riesgos-nivel-riesgo").Tabla({
                tblId: "tbl-riesgos-nivel-riesgo",
                cabecera: "Nro Riesgo,<strong>Código Riesgo</strong>,<strong>Riesgo Identificado</strong>,<strong>Usuario Reporta</strong>,<strong>Agencia Afectada</strong>,<strong>\u00C1rea Afectada</strong>,<strong>Fecha de detecci\u00F3n</strong>",
                campos: "nNroRiesgo,cCodRiesgo,cRiesgoIdentiticado,oUsuarios.cUser,oAgencias.cAgeDescripcion,oAreas.cAreaDescripcion,dFechaDeteccion",
                datos: datos.oLstDatosRiesgoDet,
                classtbl: "table table-striped",
                alineado: "C,C,L,C,C,C,C",
                formato: "0,0,0,0,0,0,3",
                controles: "0,0,4,0,0,0,0",
                visible: "0,1,1,1,1,1,1",
                anchocolumna: "0,8,30,8,10,10,10",
                sindata: "No se obtener información de los riesgo",
                numerado: "Si",
                paginacion: "Si",
                ajustar: 'No',
                paginacion: { filas: 5, pagina: 1, nombre: 'tbl-riesgo-nivel-riesgo' }
            });

            $(".modal-title strong").html("Mapa de Calor - " + selTipoiesgo.find('option:selected').text() + " - " + selNivelRiesgo.find('option:selected').text());
            $(".valProbalididad").html(datos.oProbabilidad.cConsDescripcion);
            $(".valProbalididad").attr("nProbabilidad", datos.oProbabilidad.nConsValor);
            $(".valImpacto").html(datos.oImpacto.cConsDescripcion);
            $(".valImpacto").attr("nImpacto", datos.oImpacto.nConsValor);

            $("#InfoRiesgoNivelRiesgo").modal("show");
        },
        //bloqueo: false
    });
}

function GenerarMapaCalorRiesgo() {
    $("table tr td a").empty();

    var lnTpoRiesgo = $("#selTpoRiesgo").val();//$('input:radio[name=optsTpoRiesgo]:checked').val();
    var lnTpoNivRiesgo = $("#selNivelRiesgo").val();//$('input:radio[name=optsTpoNivRiesgo]:checked').val();

    $.fn.Conexion({
        direccion: '/Reporte/GenerarMapaRiesgo',
        datos: { pnTpoRiesgo: lnTpoRiesgo, TpoNivRiesgo: lnTpoNivRiesgo },
        terminado: function (data) {
            datos = JSON.parse(data);
            var $Arreglo = new Array();
            var $NivRiesgoBajo = 0, $NivRiesgoModerado = 0, $NivRiesgoAlto = 0, $NivRiesgoExtremo = 0;
            for (var i in datos.oLstDataMapaRiesgo) {
                $Arreglo = datos.oLstDataMapaRiesgo[i];

                switch ($Arreglo.nConsCod) {
                    case 1:
                        switch ($Arreglo.nConsValor) {
                            case 1: $("#lnk-cel-11").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoModerado = $NivRiesgoModerado + $Arreglo.nCantidad; break;
                            case 2: $("#lnk-cel-12").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoAlto = $NivRiesgoAlto + $Arreglo.nCantidad; break;
                            case 3: $("#lnk-cel-13").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoExtremo = $NivRiesgoExtremo + $Arreglo.nCantidad; break;
                            case 4: $("#lnk-cel-14").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoExtremo = $NivRiesgoExtremo + $Arreglo.nCantidad; break;
                            case 5: $("#lnk-cel-15").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoExtremo = $NivRiesgoExtremo + $Arreglo.nCantidad; break;
                        }
                        break;
                    case 2:
                        switch ($Arreglo.nConsValor) {
                            case 1: $("#lnk-cel-21").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoModerado = $NivRiesgoModerado + $Arreglo.nCantidad; break;
                            case 2: $("#lnk-cel-22").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoAlto = $NivRiesgoAlto + $Arreglo.nCantidad; break;
                            case 3: $("#lnk-cel-23").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoAlto = $NivRiesgoAlto + $Arreglo.nCantidad; break;
                            case 4: $("#lnk-cel-24").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoExtremo = $NivRiesgoExtremo + $Arreglo.nCantidad; break;
                            case 5: $("#lnk-cel-25").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoExtremo = $NivRiesgoExtremo + $Arreglo.nCantidad; break;
                        }
                        break;
                    case 3:
                        switch ($Arreglo.nConsValor) {
                            case 1: $("#lnk-cel-31").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoBajo = $NivRiesgoBajo + $Arreglo.nCantidad; break;
                            case 2: $("#lnk-cel-32").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoModerado = $NivRiesgoModerado + $Arreglo.nCantidad; break;
                            case 3: $("#lnk-cel-33").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoAlto = $NivRiesgoAlto + $Arreglo.nCantidad; break;
                            case 4: $("#lnk-cel-34").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoExtremo = $NivRiesgoExtremo + $Arreglo.nCantidad; break;
                            case 5: $("#lnk-cel-35").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoExtremo = $NivRiesgoExtremo + $Arreglo.nCantidad; break;
                        }
                        break;
                    case 4:
                        switch ($Arreglo.nConsValor) {
                            case 1: $("#lnk-cel-41").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoBajo = $NivRiesgoBajo + $Arreglo.nCantidad; break;
                            case 2: $("#lnk-cel-42").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoBajo = $NivRiesgoBajo + $Arreglo.nCantidad; break;
                            case 3: $("#lnk-cel-43").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoModerado = $NivRiesgoModerado + $Arreglo.nCantidad; break;
                            case 4: $("#lnk-cel-44").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoAlto = $NivRiesgoAlto + $Arreglo.nCantidad; break;
                            case 5: $("#lnk-cel-45").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoExtremo = $NivRiesgoExtremo + $Arreglo.nCantidad; break;
                        }
                        break;
                    case 5:
                        switch ($Arreglo.nConsValor) {
                            case 1: $("#lnk-cel-51").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoBajo = $NivRiesgoBajo + $Arreglo.nCantidad; break;
                            case 2: $("#lnk-cel-52").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoBajo = $NivRiesgoBajo + $Arreglo.nCantidad; break;
                            case 3: $("#lnk-cel-53").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoModerado = $NivRiesgoModerado + $Arreglo.nCantidad; break;
                            case 4: $("#lnk-cel-54").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoAlto = $NivRiesgoAlto + $Arreglo.nCantidad; break;
                            case 5: $("#lnk-cel-55").html("<strong>" + $Arreglo.nCantidad + "</strong>"); $NivRiesgoAlto = $NivRiesgoAlto + $Arreglo.nCantidad; break;
                        }
                        break;

                }
            }

            $("#TotNivRiesgoBajo").html($NivRiesgoBajo);
            $("#TotNivRiesgoModerado").html($NivRiesgoModerado);
            $("#TotNivRiesgoAlto").html($NivRiesgoAlto);
            $("#TotNivRiesgoExtremo").html($NivRiesgoExtremo);

        },
    });
}

function ExportarRiesgosNivelRiesgo(nivelRiesgo) {
    //realizar el reporte

    ////var $nivelRiesgo =  $("TotNivRiesgoAlto").find();

    //$.fn.Conexion({
    //    direccion: '/RiesgoOperacional/DevolverVista',
    //    datos: { Vista: "ExportarInformeMapaCalorRiesgo", Controlador: "Reporte" },
    //    terminado: function (url) {
    //        window.location.href = url;
    //    },
    //});

}

function ExportarExcelRiesgosMapaCalor() {
    var param = {
        pnTpoRiesgo: eval($("#selTpoRiesgo").val()), //eval($('input:radio[name=optsTpoRiesgo]:checked').val()),
        pnTpoNivelRiesgo: eval($("#selNivelRiesgo").val())//eval($('input:radio[name=optsTpoNivRiesgo]:checked').val())
    };

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVistaParam',
        datos: { Vista: "ExportarInformeMapaCalorRiesgo", Controlador: "Reporte", Parametros: JSON.stringify(param) },
        terminado: function (data) {
            GenerarMapaCalorRiesgo();
            window.location = data.url;
        },
    });


    //show_loading_bar({
    //    pct: 50,
    //    delay: 1.2,
    //    before: function (pct) {
    //        GenerarMapaCalorRiesgo();
    //    },
    //    finish: function (pct) {

    //        show_loading_bar({
    //            pct: 30,
    //            delay: 1.7,
    //            wait: .5,
    //            finish: function () {
    //                show_loading_bar({
    //                    wait: .5,
    //                    pct: 100
    //                })
    //            }
    //        });


    //    }
    //});


}


