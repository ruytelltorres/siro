$(document).ready(function () {
    CargarGraficoDistribucionEventoPerdida();
    CargarGraficoTendenciaPerdidaBrutaVsRecuperacion();
});

function CargarGraficoDistribucionEventoPerdida() {
    $.fn.Conexion({
        direccion: '/Reporte/GraficosEventoPerdida',
        datos: { pnIndex: 0 },
        mensaje: 'Cargando distribución de eventos de pérdida',
        terminado: function (data) {
            var respuesta = JSON.parse(data);
            var datosGrafico = (respuesta && respuesta.modelGraf && respuesta.modelGraf.data) ? respuesta.modelGraf.data : [];

            Highcharts.chart('graf-home-distribucion-eventos', {
                chart: {
                    type: 'pie'
                },
                title: {
                    text: 'Distribución de eventos de pérdida'
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
                    name: 'Participación',
                    colorByPoint: true,
                    data: datosGrafico
                }]
            });
        }
    });
}

function CargarGraficoTendenciaPerdidaBrutaVsRecuperacion() {
    $.fn.Conexion({
        direccion: '/Reporte/GraficosEventoPerdida',
        datos: { pnIndex: 2 },
        mensaje: 'Cargando tendencia de pérdida bruta vs recuperación',
        terminado: function (data) {
            var respuesta = JSON.parse(data);
            var categorias = (respuesta && respuesta.modelGraf && respuesta.modelGraf.categories) ? respuesta.modelGraf.categories : [];
            var series = (respuesta && respuesta.modelGraf && respuesta.modelGraf.data) ? respuesta.modelGraf.data : [];

            Highcharts.chart('graf-home-perdida-vs-recuperacion', {
                chart: {
                    type: 'line'
                },
                title: {
                    text: 'Tendencia de pérdida bruta vs recuperación'
                },
                xAxis: {
                    categories: categorias,
                    title: {
                        text: 'Tipo de evento de pérdida'
                    }
                },
                yAxis: {
                    title: {
                        text: 'Monto (soles)'
                    }
                },
                tooltip: {
                    shared: true,
                    valueDecimals: 2
                },
                credits: {
                    enabled: false
                },
                series: series
            });
        }
    });
}


