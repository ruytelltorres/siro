window.onload = function () {
    $('#tabs-derecha').bootstrapWizard({
        //'tabClass': 'nav tabs-vertical right-aligned',
        'tabClass': 'nav tabs-vertical',
        'onTabShow': function (tab, navigation, index) {
            if (index == 0) { //Riesgos Operacionales
                for (i = 0; i < 5; i++) {
                    ReporteGraficosRiesgosOperacionales(i);
                }
            } else if (index == 1) { //Evaluaciones
                for (i = 0; i < 2; i++) {
                    ReporteGraficosEvaluaciones(i);
                }
            } else if (index == 2) { //Evento de Perdida
                for (i = 0; i < 5; i++) {
                    ReporteGraficosEventoPerdida(i);
                }
            }
        }
    });
}


function ReporteGraficosRiesgosOperacionales(index) {
    $.fn.Conexion({
        direccion: '/Reporte/GraficosRiesgosOperacionales',
        datos: { pnIndex: index },
        //bloqueo: true,
        terminado: function (data) {
            datos = JSON.parse(data);

            if (index == 0) {
                //$("#val-alert-001").html(datos.modelGraf.ymax),
                Highcharts.chart('graf-riesgo-ope-estado', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: '<strong>Riesgos Operacionales por Estado</strong>'
                    },
                    subtitle: {
                        //text: 'Click the columns to view versions. Source: <a href="http://statcounter.com" target="_blank">statcounter.com</a>'
                        text: 'Total de riesgos operacionales resgistrados por estado :' + datos.modelGraf.ymax
                    },
                    accessibility: {
                        announceNewData: {
                            enabled: true
                        }
                    },
                    xAxis: {
                        type: 'category'
                    },
                    yAxis: {
                        min: datos.modelGraf.ymin,
                        max: datos.modelGraf.ymax,
                        tickInterval: datos.modelGraf.interval,
                        title: {
                            text: 'Rango de registros - RO'
                        }

                    },
                    legend: {
                        enabled: false
                    },
                    credits: {
                        enabled: false
                    },
                    plotOptions: {
                        series: {
                            point: {
                                events: {
                                    click: function () {
                                        if (this.options != null) {
                                            //location.href = 'https://en.wikipedia.org/wiki/' +
                                            //this.options.key;
                                            var parameters = {
                                                psProceso: this.options.drilldown
                                            };
                                            $.fn.Conexion({
                                                direccion: '/RiesgoOperacional/DevolverVistaParam',
                                                datos: { Vista: "ReporteRiesgoOpeEstado", Controlador: "Reporte", Parametros: JSON.stringify(parameters) },
                                                terminado: function (data) {
                                                    window.location = data.url;
                                                },
                                            });
                                        }
                                    }
                                }
                            },
                            borderWidth: 0,
                            dataLabels: {
                                enabled: true,
                                //format: '{point.y:.1f}%'
                                format: '{point.y}'

                            }
                        }
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y}</b> del total<br/>'
                    },

                    series: [{ name: "Estado", colorByPoint: true, data: datos.modelGraf.data }],

                });
            } else if (index == 1) {
                Highcharts.chart('graf-riesgo-ope-riesgos-anio', {
                    title: {
                        text: '<strong>Riesgos Operacionales por Año</strong>'
                    },
                    subtitle: {
                        text: 'Total de riesgos operacionales registrados por año'
                    },
                    xAxis: {
                        categories: datos.modelGraf.categories
                    },
                    yAxis: {
                        min: datos.modelGraf.ymin,
                        //max: datos.modelGraf.ymax,
                        tickInterval: datos.modelGraf.interval,
                        title: {
                            text: 'Rango de registros - RO'
                        }

                    },
                    legend: {
                        enabled: false
                    },
                    credits: {
                        enabled: false
                    },
                    series: [{
                        type: 'column',
                        name: 'Cantidad',
                        colorByPoint: true,
                        data: datos.modelGraf.data,
                        showInLegend: false
                    }]
                });
            } else if (index == 2) {
                Highcharts.chart('graf-riesgo-ope-causas-porcentaje', {
                    chart: {
                        plotBackgroundColor: null, //
                        plotBorderWidth: null, //
                        plotShadow: false, //
                        type: 'pie'
                    },
                    title: {
                        text: '<strong>Riesgos Operacionales - Causas</strong>'
                    },
                    subtitle: {
                        text: 'Se muestran las 10 principales causas de los Riesgos Operacionales'
                    },
                    accessibility: {
                        announceNewData: {
                            enabled: true
                        },
                        point: {
                            valueSuffix: '%'
                        }
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: true,
                                format: '<b>{point.name}</b><br>{point.percentage:.1f} %',
                                //distance: -50,
                                //filter: {
                                //    property: 'percentage',
                                //    operator: '>',
                                //    value: 4
                                //}
                            },
                            showInLegend: false
                        },
                        series: {
                            dataLabels: {
                                enabled: true,
                                format: '{point.name}: {point.y:.1f}%'
                            }
                        }
                    },
                    legend: {
                        enabled: true
                    },
                    credits: {
                        enabled: false
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> del total<br/>'
                    },
                    series: [{ name: "Causa", colorByPoint: true, data: datos.modelGraf.data }]
                });
            } else if (index == 3) {
                Highcharts.chart('graf-riesgo-factor', {
                    chart: {
                        type: 'pie'
                    },
                    title: {
                        text: 'Riesgos Operaciones por Factor de Riesgo'
                    },
                    subtitle: {
                        text:  null, //'Click the slices to view versions. Source: <a href="http://statcounter.com" target="_blank">statcounter.com</a>'
                    },

                    accessibility: {
                        announceNewData: {
                            enabled: true
                        },
                        point: {
                            valueSuffix: '%'
                        }
                    },

                    plotOptions: {
                        series: {
                            dataLabels: {
                                enabled: true,
                                format: '{point.name}: {point.y:.1f}%'
                            }
                        }
                    },
                    legend: {
                        enabled: true
                    },
                    credits: {
                        enabled: false
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
                    },
                    series: datos.modelGraf.data,
                });


            } else if (index == 4) {
                Highcharts.chart('graf-riesgo-ope-plan-accion', {
                    chart: {
                        type: 'pie'
                    },
                    title: {
                        text: '<strong>Planes de Acción - Estado Actual</strong>'
                    },
                    subtitle: {
                        text: 'Representación sobre el estado actual de los planes de accón'
                    },

                    accessibility: {
                        announceNewData: {
                            enabled: true
                        },
                        point: {
                            valueSuffix: '%'
                        }
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            //colors: pieColors,
                            dataLabels: {
                                enabled: true,
                                format: '<b>{point.name}</b><br>{point.percentage:.1f} %',
                                //distance: -50,
                                //filter: {
                                //    property: 'percentage',
                                //    operator: '>',
                                //    value: 4
                                //}
                            }
                        },
                        series: {
                            dataLabels: {
                                enabled: true,
                                format: '{point.name}: {point.y:.1f}%'
                            }
                        }
                    },
                    legend: {
                        enabled: false
                    },
                    credits: {
                        enabled: false
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> del total<br/>'
                    },
                    series: datos.modelGraf.data,
                    //series: [
                    //    {
                    //        name: "Causa", colorByPoint: true, data: datos.modelGraf.data
                    //    }
                    //]
                });
            }
        },
    });
}

function ReporteGraficosEvaluaciones(index) {

}

//#region Evento de Perdida
function ReporteGraficosEventoPerdida(index) {
    $.fn.Conexion({
        direccion: '/Reporte/GraficosEventoPerdida',
        datos: { pnIndex: index },
        //bloqueo: true,
        mensaje: 'Procesando la información de los reportes',
        terminado: function (data) {
            datos = JSON.parse(data);

            if (index == 0) {
                Highcharts.chart('graf-perdida-neta-clase-evento', {
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        type: 'pie'
                    },
                    title: {
                        text: 'Perdida Neta por Tipo de Evento de Perdida'
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    },
                    accessibility: {
                        point: {
                            valueSuffix: '%'
                        }
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: true,
                                format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                            }
                        }
                    },
                    credits: {
                        enabled: false
                    },
                    series: [{
                        name: 'Val. Total',
                        colorByPoint: true,
                        data: datos.modelGraf.data
                    }]
                });

                $("#content-tbl-perdida-neta-tipo-clase-evento").Tabla({
                    tblId: "tbl-perdida-neta-tipo-clase-evento",
                    cabecera: "<strong>Tipo Evento </strong>,<strong>Perdida Neta</strong>,<strong>Cantidad</strong>",
                    campos: "DescClaseEvento,PerdidaNeta,Cantidad",
                    datos: datos.modelGraf.dataReport,
                    classtbl: "table table-responsive responsive",
                    alineado: "L,D,D",
                    formato: "0,2,2",
                    controles: "0,0,0",
                    visible: "1,1,1,",
                    anchocolumna: '15,12,12',
                    sindata: "No se encontró información para mostrar",
                    numerado: "Si",
                    paginacion: "No",
                    ajustar: 'No',
                });
            } else if (index == 1) {
                Highcharts.chart('graf-perdida-neta-vs-monto-bruto', {
                    chart: {
                        type: 'area'
                    },
                    title: {
                        text: 'Perdida Neta  Vs Monto Bruto por Año'
                    },
                    subtitle: {
                        //text: 'Source: Wikipedia.org'
                        text: ''
                    },
                    xAxis: {
                        //categories: lstAnios,
                        categories: datos.modelGraf.categories,
                        tickmarkPlacement: 'on',
                        title: {
                            enabled: false
                        }
                    },
                    yAxis: {
                        title: {
                            //text: 'Billions'
                            text: 'Soles'
                        },
                        labels: {
                            formatter: function () {
                                return this.value / 1000;
                            }
                        }
                    },
                    tooltip: {
                        split: true,
                        valueSuffix: ' Soles'
                    },
                    plotOptions: {
                        area: {
                            stacking: 'normal',
                            lineColor: '#666666',
                            lineWidth: 1,
                            marker: {
                                lineWidth: 1,
                                lineColor: '#666666'
                            }
                        }
                    },
                    credits: {
                        enabled: false
                    },
                    series: datos.modelGraf.data
                });
                //$("#content-tbl-perdida-neta-vs-monto-bruto").Tabla({
                //    tblId: "tbl-perdida-neta-vs-monto-bruto",
                //    cabecera: "<strong>Año</strong>,<strong>Perdida Neta</strong>,<strong>Monto Bruto</strong>",
                //    campos: "Anio,PerdidaNeta,MontoBruto",
                //    datos: datos.modelGraf.dataReport,
                //    classtbl: "table table-responsive responsive",
                //    alineado: "L,D,D",
                //    formato: "0,2,2",
                //    controles: "0,0,0",
                //    visible: "1,1,1,",
                //    anchocolumna: '15,12,12',
                //    sindata: "No se encontró información para mostrar",
                //    numerado: "Si",
                //    paginacion: "No",
                //    ajustar: 'No',
                //});


            } else if (index == 2) {
                Highcharts.chart('graf-monto-bruto-vs-monto-recup', {
                    chart: {
                        //marginBottom: 80,
                        marginLeft: 130,
                        type: 'bar',
                        width: 750
                    },
                    title: {
                        text: 'Monto Bruto - Monto Recuperado por Tipo de Evento de Perdida'
                    },
                    subtitle: {
                        text: ''
                    },
                    xAxis: {
                        categories: datos.modelGraf.categories,
                        //labels: {
                        //    useHTML: true,
                        //    allowOverlap: true,
                        //    style: {
                        //        color: 'red',
                        //        wordBreak: 'break-all',
                        //        textOverflow: 'allow'
                        //    }
                        //},
                        title: {
                            text: 'Tipo de Eventos de Perdida'
                        }
                    },
                    yAxis: {
                        //min: datos.modelGraf.ymin,
                        ////max: datos.modelGraf.ymax,
                        //tickInterval: datos.modelGraf.interval,
                        title: {
                            text: 'Representación (soles)',

                            align: 'high',
                        },
                        labels: {
                            //format: 'S/{value:.1f}',
                            format: '{value}',
                            overflow: 'justify',
                            //formatter: function () {
                            //    return '$' + this.axis.defaultLabelFormatter.call(this);
                            //}
                        }
                    },
                    tooltip: {
                        valueSuffix: 'miles'
                    },
                    plotOptions: {
                        bar: {
                            dataLabels: {
                                enabled: true
                            }
                        },
                        series: {
                            dataLabels: {
                                enabled: true,
                                format: '{y:.2f}'
                            }
                        },

                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'top',
                        x: -40,
                        y: 80,
                        floating: true,
                        borderWidth: 1,
                        backgroundColor:
                            Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF',
                        shadow: true
                    },
                    credits: {
                        enabled: false
                    },
                    series: datos.modelGraf.data
                });

                //$("#content-tbl-monto-bruto-vs-monto-recuperado").Tabla({
                //    tblId: "tbl-monto-bruto-vs-monto-recuperado",
                //    cabecera: "<strong>Cod Tipo Evento</strong>,<strong>Tipo Evento Perdida</strong>,<strong>Monto Bruto</strong>,<strong>Monto Recuperado</strong>",
                //    campos: "CodClaseEvento,DescClaseEvento,MontoBruto,MontoRecuperado",
                //    datos: datos.modelGraf.dataReport,
                //    classtbl: "table table-responsive responsive",
                //    alineado: "C,L,D,D",
                //    formato: "0,0,2,2",
                //    controles: "0,0,0,0",
                //    visible: "1,1,,1,1,",
                //    anchocolumna: '10,15,12,12',
                //    sindata: "No se encontró información para mostrar",
                //    numerado: "Si",
                //    paginacion: "No",
                //    ajustar: 'No',
                //});
            } else if (index == 3) {
                Highcharts.chart('graf-perdida-neta-vs-monto-bruto-agencia', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'Pérdida  Neta vs Monto Bruto por Agencia'
                    },
                    subtitle: {
                        text: null //'Source: WorldClimate.com'
                    },
                    xAxis: {
                        categories: datos.modelGraf.categories,
                        crosshair: true
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: '(soles)'
                        }
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                            '<td style="padding:0"><b>{point.y:.1f} soles</b></td></tr>',
                        footerFormat: '</table>',
                        shared: true,
                        useHTML: true
                    },
                    plotOptions: {
                        column: {
                            pointPadding: 0.2,
                            borderWidth: 0
                        }
                    },
                    credits: {
                        text: "SIRO v2",
                        enabled: true
                    },
                    series: datos.modelGraf.data
                });

                $("#content-tbl-perdida-neta-vs-monto-bruto-agencia").Tabla({
                    tblId: "tbl-perdida-neta-vs-monto-bruto-agencia",
                    cabecera: "<strong>Agencia</strong>,<strong>Perdida Neta</strong>,<strong>Monto Bruto</strong>",
                    campos: "Agencia,PerdidaNeta,MontoBruto",
                    datos: datos.modelGraf.dataReport,
                    classtbl: "table table-responsive responsive",
                    alineado: "L,D,D",
                    formato: "0,2,2",
                    controles: "0,0,0",
                    visible: "1,1,1,",
                    anchocolumna: '15,12,12',
                    sindata: "No se encontró información para mostrar",
                    numerado: "Si",
                    paginacion: "No",
                    ajustar: 'No',
                });

            } else if (index == 4) {
                var lstCantidades = [];
                for (var i in datos.modelGraf.dataReport) {
                    lstCantidades.push(datos.modelGraf.dataReport[i].Cantidad);
                }

                Highcharts.chart('graf-cantidad-evento-perdida-anio', {
                    chart: {
                        type: 'column',
                        options3d: {
                            enabled: true,
                            alpha: 10,
                            beta: 25,
                            depth: 70
                        }
                    },
                    title: {
                        text: 'Cantidad Evento de Pérdida por Año'
                    },
                    subtitle: {
                        text: null
                    },
                    plotOptions: {
                        column: {
                            depth: 25
                        }
                    },
                    xAxis: {
                        categories: datos.modelGraf.categories,//Highcharts.getOptions().lang.shortMonths,
                        labels: {
                            skew3d: true,
                            style: {
                                fontSize: '16px'
                            }
                        }
                    },
                    yAxis: {
                        title: {
                            text: null
                        }
                    },
                    credits: {
                        enabled: false
                    },
                    series: datos.modelGraf.data
                    //[
                    //    { name: 'Total', data: lstCantidades }
                    //]
                });





            }



        }
    });
}


$("#btnIrGrupoEventoPerdida").click(function (e) {
    e.preventDefault();

    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVista',
        datos: { Vista: "PBEventoPerdida", Controlador: "Reporte" },
        terminado: function (url) {
            window.location.href = url;
        },
    });



});


//#endregion





//function ReportesGrafDonutNivelReisgoResidual(tab, tporiesgo) {
//    $.fn.Conexion({
//        direccion: '/Reporte/GrafRiesgoNivelRiesgoResidual',
//        datos: { pnTpoRiesgo: tporiesgo },
//        bloqueo: true,
//        terminado: function (data) {
//            datos = JSON.parse(data);
//            if (tab == 0) {
//                if (typeof Morris != 'undefined') {
//                    $("#DonutGrafNivelRiesgo").empty();
//                    Morris.Donut({
//                        element: 'DonutGrafNivelRiesgo',
//                        data: datos.modelGraf.data,
//                        colors: datos.modelGraf.colors,
//                        formatter: function (x, data) { return data.formatted; },
//                        //formatter: function (x) { return x + ' riesgos' },
//                    }).on('click', function (i, row) {
//                        /*i -> Obtiene la posicion de la informacion 
//                         0: Nivel de Riesgo Bajo
//                         1: Nivel de Riesgo Moderado
//                         2: Nivel de Riesgo Alto
//                         3: Nivel de Riesgo Extremo
//                         */
//                        //alert("Mostrar esto i:" + i + ", row label: " + row.label + ", row value:" + row.value);
//                    });
//                }
//            } else if (tab == 1) {

//            }


//        },
//        //bloqueo: false
//    });
//}

