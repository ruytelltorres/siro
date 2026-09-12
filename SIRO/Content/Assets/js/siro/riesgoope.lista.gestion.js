//$(document).ready(function () {
//    $("#btnBuscarRiesgo").click();
//    $("#selOpcionBusqueda").change();

//    $("#inBusqueda").on("keyup", function (e) {
//        var lsBusqueda = $(this).val();
//        $("#btnBuscarRiesgo").prop("disabled", !(lsBusqueda.length >= 1));
//    });

//});

//window.onload = function () {

//    $("#btnBuscarRiesgo").click();
//    $("#selOpcionBusqueda").change();

//    $("#inBusqueda").on("keyup", function (e) {
//        var lsBusqueda = $(this).val();
//        $("#btnBuscarRiesgo").prop("disabled", !(lsBusqueda.length >= 1));
//    });
//}


window.onload = function () {

    //tbl.simple_datagrid({ on_generate_tr: crearFila });
    var load = CargarDatosTabla();

    Promise.all([load]).then(([r1]) => {
        //habilitarEdicion(false);
    });

    //$.fn.Conexion({
    //    direccion: '/RiesgoOperacional/MostrarRiesgosOperacionales',
    //    //datos: { psBuscar: inBusqueda.val().trim(), pnTpoBusqueda: inTpoBusqueda.val() },
    //    bloqueo: true,
    //    terminado: function (data) {
    //        datos = JSON.parse(data);
    //        var $opciones = [
    //            {
    //                Columna: "Tipo1", id: "id-detalle-riesgo", claseIcono: "entypo-info-circled", clase: "DetalleRiesgo", titulo: "Detalles del riesgo", function: function (e) {
    //                    var ObjParam = { riesgo: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
    //                    $.fn.Conexion({
    //                        direccion: '/RiesgoOperacional/DevolverVistaParam',
    //                        datos: { Vista: "DetalleRiesgo", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
    //                        bloqueo: true,
    //                        terminado: function (data) {
    //                            window.location = data.url;
    //                        },
    //                    });
    //                }
    //            },
    //            {
    //                Columna: "Tipo2", id: "id-gestion-riesgo", claseIcono: "entypo-cog", clase: "GestionarRiesgo", titulo: "Gestionar riesgo", function: function (e) {
    //                    var lnEstadoRiesgo = $(e).parents("tr").find("td").eq(9).html();
    //                    if (eval(lnEstadoRiesgo) == 6) {
    //                        $.fn.MensajeProcesos({
    //                            posicion: 'A',
    //                            mensaje: 'La acción no está permita dado a que el riesgo actualmente se encuenta en estado de modificación',
    //                            titulo: 'Riesgo Operacional',
    //                            tipo_mensaje: 'informacion'
    //                        });
    //                    } else {
    //                        var ObjParam = { psNroRiesgo: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
    //                        $.fn.Conexion({
    //                            direccion: '/RiesgoOperacional/DevolverVistaParam',
    //                            datos: { Vista: "GestionRiesgo", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
    //                            bloqueo: true,
    //                            terminado: function (data) {
    //                                window.location = data.url;
    //                            },
    //                        });
    //                    }
    //                }
    //            },
    //            {
    //                Columna: "Tipo3", id: "id-modificacion", claseIcono: "entypo-pencil", clase: "Modificacion", titulo: "Modificar", function: function (e) {
    //                    var lnEstadoRiesgo = $(e).parents("tr").find("td").eq(9).html();
    //                    if (eval(lnEstadoRiesgo) != 6) {
    //                        $.fn.MensajeProcesos({
    //                            posicion: 'A',
    //                            mensaje: 'La acción no está permita dado a que el riesgo actualmente se encuenta en proceso de gestión',
    //                            titulo: 'Riesgo Operacional',
    //                            tipo_mensaje: 'informacion'
    //                        });
    //                    } else {
    //                        var ObjParam = { riesgo: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
    //                        $.fn.Conexion({
    //                            direccion: '/RiesgoOperacional/DevolverVistaParam',
    //                            datos: { Vista: "Modificar", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
    //                            terminado: function (data) {
    //                                window.location = data.url;
    //                            },
    //                        });
    //                    }
    //                }
    //            }
    //        ]

    //        if (gUsuario.cRHCargoCod == "005011") {
    //            $opciones.splice(2, 1);
    //        } else if (gUsuario.cRHCargoCod != "005011") {
    //            $opciones.splice(0, 2);
    //        }

    //        $("#content-tbl-riesgo-operacional-gestion").Tabla({
    //            tblId: "tbl-riesgo-operacional-gestion",
    //            cabecera: "Nro Riesgo,<strong>Código</strong>,<strong>Tipo</strong>,<strong>Riesgo Identificado</strong>,<strong>Usuario</strong>,<strong>Agencia Afectada</strong>,<strong>Área Afectada</strong>,<strong>Proceso Actual</strong>,<strong>Estado Riesgo</strong>,<strong>Fecha Detección</strong>",
    //            campos: "nNroRiesgo,cCodRiesgo,cTpoRiesgo,cRiesgoIdentiticado,oUsuarios.cUser,oAgencias.cAgeDescripcion,oAreas.cAreaDescripcion,cProcRiesgo,nEstadoRiesgo,dFechaDeteccion",
    //            datos: datos.oLstRiesgoOperacional,
    //            classtbl: "table table-hover datatable",
    //            //cantRegVertical: 10,
    //            //scrollVertical: "Si",
    //            alineado: "C,C,C,L,C,C,C,C,C,C",
    //            formato: "0,0,0,0,0,0,0,0,0,3",
    //            controles: "0,0,0,4,0,0,0,0,0,0",
    //            visible: "0,1,0,1,1,1,1,1,0,1",
    //            anchocolumna: "0,10,0,30,5,10,10,10,0,10",
    //            sindata: "No se encontró información para la gestión de los riesgos operacionales",
    //            numerado: "Si",
    //            paginacion: "Si",
    //            ajustar: 'No',
    //            paginacion: { filas: 5, pagina: 1, nombre: 'tabla-riesgo-operacional' },
    //            opciones: $opciones,
    //        });
    //    },
    //});
}


var gnFilasPorPagina = 10;
var gLstRiesgoOperacional = null;

function CargarDatosTabla() {
    return new Promise(function (resolve, reject) {
        $.fn.Conexion({
            direccion: '/RiesgoOperacional/MostrarRiesgosOperacionales',
            //datos: { psBuscar: inBusqueda.val().trim(), pnTpoBusqueda: inTpoBusqueda.val() },
            bloqueo: true,
            mensaje: 'Obteniendo información de los Riesgos Operacionales. Por favor espere.',//'Obteniendo información de los Riesgos Operacionales',
            terminado: function (data) {
                datos = JSON.parse(data);
                gLstRiesgoOperacional = datos.oLstRiesgoOperacional;
                var $opciones = [
                    {
                        Columna: "Tipo1", id: "id-detalle-riesgo", claseIcono: "entypo-info-circled", clase: "DetalleRiesgo", titulo: "Detalles del riesgo", function: function (e) {
                            var ObjParam = { riesgo: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
                            $.fn.Conexion({
                                direccion: '/RiesgoOperacional/DevolverVistaParam',
                                datos: { Vista: "DetalleRiesgo", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
                                bloqueo: true,
                                terminado: function (data) {
                                    window.location = data.url;
                                },
                            });
                        }
                    },
                    {
                        Columna: "Tipo2", id: "id-gestion-riesgo", claseIcono: "entypo-cog", clase: "GestionarRiesgo", titulo: "Gestionar riesgo", function: function (e) {
                            var lnEstadoRiesgo = $(e).parents("tr").find("td").eq(9).html();
                            if (eval(lnEstadoRiesgo) == 6) {
                                $.fn.MensajeProcesos({
                                    posicion: 'A',
                                    mensaje: 'La acción no está permita dado a que el riesgo actualmente se encuenta en estado de modificación',
                                    titulo: 'Riesgo Operacional',
                                    tipo_mensaje: 'informacion'
                                });
                            } else {
                                var ObjParam = { psNroRiesgo: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
                                $.fn.Conexion({
                                    direccion: '/RiesgoOperacional/DevolverVistaParam',
                                    datos: { Vista: "GestionRiesgo", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
                                    bloqueo: true,
                                    terminado: function (data) {
                                        window.location = data.url;
                                    },
                                });
                            }
                        }
                    },
                    {
                        Columna: "Tipo3", id: "id-modificacion", claseIcono: "entypo-pencil", clase: "Modificacion", titulo: "Modificar", function: function (e) {
                            var lnEstadoRiesgo = $(e).parents("tr").find("td").eq(9).html();
                            console.log('datos.oLstRiesgoOperacional.oEstado', datos.oLstRiesgoOperacional.oEstado);
                            //var lnEstadoRiesgo = datos.oLstRiesgoOperacional.oEstado.nConstValor;
                            console.log('lnEstadoRiesgo', lnEstadoRiesgo);
                            if (eval(lnEstadoRiesgo) != 6) {
                                $.fn.MensajeProcesos({
                                    posicion: 'A',
                                    mensaje: 'La acción no está permita dado a que el riesgo actualmente se encuenta en proceso de gestión',
                                    titulo: 'Riesgo Operacional',
                                    tipo_mensaje: 'informacion'
                                });
                            } else {
                                var ObjParam = { riesgo: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
                                $.fn.Conexion({
                                    direccion: '/RiesgoOperacional/DevolverVistaParam',
                                    datos: { Vista: "Modificar", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
                                    terminado: function (data) {
                                        window.location = data.url;
                                    },
                                });
                            }
                        }
                    }
                ]

                if (gUsuario.cRHCargoCod == gConstGeneral.AnalistaRiesgoOperacional) {
                    $opciones.splice(2, 1);
                } else if (gUsuario.cRHCargoCod != gConstGeneral.AnalistaRiesgoOperacional) {
                    $opciones.splice(0, 2);
                }
                
                RenderizarTablaRiesgos($opciones, gnFilasPorPagina);

                resolve(true);
            },
            error: function (failData) {
                reject(failData);
            }
        });
    });
}

function RenderizarTablaRiesgos(p$opciones, pnFilas) {
    $("#content-tbl-riesgo-operacional-gestion").Tabla({
        tblId: "tbl-riesgo-operacional-gestion",
        cabecera: "Nro Riesgo,<strong>Código</strong>,<strong>Tipo</strong>,<strong>Riesgo Identificado</strong>,<strong>Usuario</strong>,<strong>Agencia Afectada</strong>,<strong>Área Afectada</strong>,<strong>Proceso Actual</strong>,<strong>Estado Riesgo</strong>,<strong>Fecha Detección</strong>",
        campos: "nNroRiesgo,cCodRiesgo,oTipoRiesgo.cConsDescripcion,cRiesgoIdentiticado,oUsuario.cUser,oAgencia.cAgeDescripcion,oAgencia.oAreas.cAreaDescripcion,oProcRiesgo.cConsDescripcion,oEstado.cConsValor,dFechaDeteccion",
        datos: gLstRiesgoOperacional,
        classtbl: "table table-hover datatable",
        alineado: "C,C,C,L,C,C,C,C,C,C",
        formato: "0,0,0,0,0,0,0,0,0,3",
        controles: "0,0,0,4,0,0,0,0,0,0",
        visible: "0,1,0,1,1,1,1,1,0,1",
        anchocolumna: "0,10,0,30,5,10,10,10,0,10",
        sindata: "No se encontró información para la gestión de los riesgos operacionales",
        numerado: "Si",
        ajustar: 'No',
        paginacion: { filas: pnFilas, pagina: 1, nombre: 'tabla-riesgo-operacional' },
        opciones: p$opciones,
    });
}

$('#selMostrarRegistros').on('change', function () {
    gnFilasPorPagina = parseInt($(this).val(), 10) || 10;

    if (!gLstRiesgoOperacional) {
        return;
    }

    var $opciones = [
        {
            Columna: "Tipo1", id: "id-detalle-riesgo", claseIcono: "entypo-info-circled", clase: "DetalleRiesgo", titulo: "Detalles del riesgo", function: function (e) {
                var ObjParam = { riesgo: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
                $.fn.Conexion({
                    direccion: '/RiesgoOperacional/DevolverVistaParam',
                    datos: { Vista: "DetalleRiesgo", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
                    bloqueo: true,
                    terminado: function (data) {
                        window.location = data.url;
                    },
                });
            }
        },
        {
            Columna: "Tipo2", id: "id-gestion-riesgo", claseIcono: "entypo-cog", clase: "GestionarRiesgo", titulo: "Gestionar riesgo", function: function (e) {
                var lnEstadoRiesgo = $(e).parents("tr").find("td").eq(9).html();
                if (eval(lnEstadoRiesgo) == 6) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'La acción no está permita dado a que el riesgo actualmente se encuenta en estado de modificación',
                        titulo: 'Riesgo Operacional',
                        tipo_mensaje: 'informacion'
                    });
                } else {
                    var ObjParam = { psNroRiesgo: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
                    $.fn.Conexion({
                        direccion: '/RiesgoOperacional/DevolverVistaParam',
                        datos: { Vista: "GestionRiesgo", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
                        bloqueo: true,
                        terminado: function (data) {
                            window.location = data.url;
                        },
                    });
                }
            }
        },
        {
            Columna: "Tipo3", id: "id-modificacion", claseIcono: "entypo-pencil", clase: "Modificacion", titulo: "Modificar", function: function (e) {
                var lnEstadoRiesgo = $(e).parents("tr").find("td").eq(9).html();
                if (eval(lnEstadoRiesgo) != 6) {
                    $.fn.MensajeProcesos({
                        posicion: 'A',
                        mensaje: 'La acción no está permita dado a que el riesgo actualmente se encuenta en proceso de gestión',
                        titulo: 'Riesgo Operacional',
                        tipo_mensaje: 'informacion'
                    });
                } else {
                    var ObjParam = { riesgo: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
                    $.fn.Conexion({
                        direccion: '/RiesgoOperacional/DevolverVistaParam',
                        datos: { Vista: "Modificar", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
                        terminado: function (data) {
                            window.location = data.url;
                        },
                    });
                }
            }
        }
    ]

    if (gUsuario.cRHCargoCod == gConstGeneral.AnalistaRiesgoOperacional) {
        $opciones.splice(2, 1);
    } else if (gUsuario.cRHCargoCod != gConstGeneral.AnalistaRiesgoOperacional) {
        $opciones.splice(0, 2);
    }

    RenderizarTablaRiesgos($opciones, gnFilasPorPagina);

    $('#inBuscar').val('');
});

$('#inBuscar').on('keyup', function () {
    var textoBuscar = $.trim(this.value);
    var idTabla = 'tbl-riesgo-operacional-gestion';
    var $tabla = $('#' + idTabla);
    var $tbody = $tabla.find('tbody');

    $tbody.find('tr.fila-sin-resultados-busqueda').remove();

    $.uiTableFilter($tabla, textoBuscar);

    var totalFilas = $tbody.find('tr').length;
    var filasVisibles = $tbody.find('tr:visible').length;
    var columnas = $tabla.find('thead th').length;

    if (textoBuscar.length > 0 && totalFilas > 0 && filasVisibles === 0) {
        $tbody.append("<tr class='fila-sin-resultados-busqueda'><td colspan='" + columnas + "' style='text-align:center; vertical-align:middle; padding:20px;'><strong>No hay resultados de la busqueda</strong></td></tr>");
    }

    // Cuando se filtra, la paginación original de Tabla() no recalcula sobre el subconjunto
    // visible. Se oculta para evitar estados inconsistentes y se restaura al limpiar.
    if (textoBuscar.length > 0) {
        $tabla.find('tfoot').hide();
    } else {
        $tabla.find('tfoot').show();
        $('#gPagPrimero-' + idTabla).trigger('click');
    }
});


//#region Opciones de busqueda
//$("#inBusqueda").keypress(function (e) {
//    var keycode = (e.keyCode ? e.keyCode : e.which);
//    if (keycode == "13") {
//        e.preventDefault();
//        $("#btnBuscarRiesgo").click();
//        return false;
//    }
//});

//$("#selOpcionBusqueda").change(function () {
//    var opcion = $(this).val();
//    var inBusqueda = $("#inBusqueda");
//    inBusqueda.val("");
//    switch (eval(opcion)) {
//        case 1:
//            inBusqueda.attr("placeholder", "Buscar por c\u00F3digo");
//            inBusqueda.attr("maxlength", "9");
//            break;
//        case 2:
//            inBusqueda.attr("placeholder", "Buscar por descripci\u00F3n");
//            inBusqueda.attr("maxlength", "250");
//            break;
//        case 3:
//            inBusqueda.attr("placeholder", "Buscar por usuario");
//            inBusqueda.attr("maxlength", "4");
//            break;
//        //case 4:
//        //    CrearDOMEstado();
//        //    break;
//        case 5:
//            $("#btnBuscarRiesgo").click();
//            break;
//        default:
//            inBusqueda.attr("placeholder", "Buscar ...");
//            break;

//    }
//});


//function CrearDOMEstado() {
//    var contenedor = $("#div-busqueda");
//    var dom = document.createElement("SELECT");
//    dom.setAttribute("id", "selEstadoRiesgo");
//    dom.setAttribute("class", "select2");
//    dom.append(contenedor.find(".input-group"));
//    //dom.appendChild(contenedor.find(".input-group"));
//    //contenedor.appendChild(x);

//    //document.body.appendChild(x);

//    $.fn.Conexion({
//        direccion: '/General/ListarConstante',
//        datos: { pnConsCod: 1002 },
//        terminado: function (data) {
//            datos = JSON.parse(data);
//            for (var i in datos.oLstConstante) {
//                var z = document.createElement("option");
//                z.setAttribute("value", datos.oLstConstante[i].nConsValor);
//                var t = document.createTextNode(datos.oLstConstante[i].cConsDescripcion);
//                z.appendChild(t);
//                document.getElementById("selEstadoRiesgo").appendChild(z);

//            }
//        },
//    });

//}

//$("#btnBuscarRiesgo").click(function (e) {
//    e.preventDefault();
//    //var inBusqueda = $("#inBusqueda");
//    //var inTpoBusqueda = $("#selOpcionBusqueda");

//    $.fn.Conexion({
//        direccion: '/RiesgoOperacional/MostrarRiesgosOperacionales',
//        //datos: { psBuscar: inBusqueda.val().trim(), pnTpoBusqueda: inTpoBusqueda.val() },
//        bloqueo: true,
//        terminado: function (data) {
//            datos = JSON.parse(data);
//            var $opciones = [
//                {
//                    Columna: "Tipo1", id: "id-detalle-riesgo", claseIcono: "entypo-info-circled", clase: "DetalleRiesgo", titulo: "Detalles del riesgo", function: function (e) {
//                        var ObjParam = { riesgo: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
//                        $.fn.Conexion({
//                            direccion: '/RiesgoOperacional/DevolverVistaParam',
//                            datos: { Vista: "DetalleRiesgo", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
//                            bloqueo: true,
//                            terminado: function (data) {
//                                window.location = data.url;
//                            },
//                        });
//                    }
//                },
//                {
//                    Columna: "Tipo2", id: "id-gestion-riesgo", claseIcono: "entypo-cog", clase: "GestionarRiesgo", titulo: "Gestionar riesgo", function: function (e) {
//                        var lnEstadoRiesgo = $(e).parents("tr").find("td").eq(9).html();
//                        if (eval(lnEstadoRiesgo) == 6) {
//                            $.fn.MensajeProcesos({
//                                posicion: 'A',
//                                mensaje: 'La acción no está permita dado a que el riesgo actualmente se encuenta en estado de modificación',
//                                titulo: 'Riesgo Operacional',
//                                tipo_mensaje: 'informacion'
//                            });
//                        } else {
//                            var ObjParam = { psNroRiesgo: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
//                            $.fn.Conexion({
//                                direccion: '/RiesgoOperacional/DevolverVistaParam',
//                                datos: { Vista: "GestionRiesgo", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
//                                bloqueo: true,
//                                terminado: function (data) {
//                                    window.location = data.url;
//                                },
//                            });
//                        }
//                    }
//                },
//                {
//                    Columna: "Tipo3", id: "id-modificacion", claseIcono: "entypo-pencil", clase: "Modificacion", titulo: "Modificar", function: function (e) {
//                        var lnEstadoRiesgo = $(e).parents("tr").find("td").eq(9).html();
//                        if (eval(lnEstadoRiesgo) != 6) {
//                            $.fn.MensajeProcesos({
//                                posicion: 'A',
//                                mensaje: 'La acción no está permita dado a que el riesgo actualmente se encuenta en proceso de gestión',
//                                titulo: 'Riesgo Operacional',
//                                tipo_mensaje: 'informacion'
//                            });
//                        } else {
//                            var ObjParam = { riesgo: base64_encode($(e).parents("tr").find("td").eq(1).html()) };
//                            $.fn.Conexion({
//                                direccion: '/RiesgoOperacional/DevolverVistaParam',
//                                datos: { Vista: "Modificar", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
//                                terminado: function (data) {
//                                    window.location = data.url;
//                                },
//                            });
//                        }
//                    }
//                }
//            ]

//            if (gUsuario.cRHCargoCod == "005011") {
//                $opciones.splice(2, 1);
//            } else if (gUsuario.cRHCargoCod != "005011") {
//                $opciones.splice(0, 2);
//            }

//            $("#content-tbl-riesgo-operacional-gestion").Tabla({
//                tblId: "tbl-riesgo-operacional-gestion",
//                cabecera: "Nro Riesgo,<strong>Código</strong>,<strong>Tipo</strong>,<strong>Riesgo Identificado</strong>,<strong>Usuario</strong>,<strong>Agencia Afectada</strong>,<strong>Área Afectada</strong>,<strong>Proceso Actual</strong>,<strong>Estado Riesgo</strong>,<strong>Fecha Detección</strong>",
//                campos: "nNroRiesgo,cCodRiesgo,cTpoRiesgo,cRiesgoIdentiticado,oUsuarios.cUser,oAgencias.cAgeDescripcion,oAreas.cAreaDescripcion,cProcRiesgo,nEstadoRiesgo,dFechaDeteccion",
//                datos: datos.oLstRiesgoOperacional,
//                classtbl: "table table-hover responsive",
//                //cantRegVertical: 10,
//                //scrollVertical: "Si",
//                alineado: "C,C,C,L,C,C,C,C,C,C",
//                formato: "0,0,0,0,0,0,0,0,0,3",
//                controles: "0,0,0,4,0,0,0,0,0,0",
//                visible: "0,1,0,1,1,1,1,1,0,1",
//                anchocolumna: "0,10,0,30,5,10,10,10,0,10",
//                sindata: "No se encontró información para la gestión de los riesgos operacionales",
//                numerado: "Si",
//                paginacion: "Si",
//                ajustar: 'No',
//                paginacion: { filas: 5, pagina: 1, nombre: 'tabla-riesgo-operacional' },
//                opciones: $opciones,
//            });
//        },
//    });
//});


//#endregion



//$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
//    debugger;
//    var target = $(e.target).attr("href");
//    if (target == "#buscaCodigo") {
//        $("#inCodRiesgo").val("");
//        gTpoBusqueda = 1;
//    } else if (target == "#buscaDescripcion") {
//        $("#inDescriopcion").val("");
//        gTpoBusqueda = 2;
//    } else if (target == "#buscaUsuario") {
//        $("#selArea").select2("val", "");
//        $("#selUsuario").select2("val", "");
//        ObtenerAgencias("#selAgencia");
//        gTpoBusqueda = 3;
//    } else if (target == "#buscaArea") {
//        $("#selAreaAfectada").select2("val", "");
//        ObtenerAgencias("#selAgenciaAfectada");
//        gTpoBusqueda = 4;
//    } else if (target == "#buscaEstado") {
//        ObtenerEstadoRiesgo();
//        gTpoBusqueda = 5;
//    }


//});


//function ObtenerAgencias(idSelect) {
//    var ListaAgencias = new Array();
//    $.fn.Conexion({
//        direccion: '/General/ListaAgencias',
//        terminado: function (data) {
//            datos = JSON.parse(data);
//            debugger;
//            CrearLista(idSelect, datos.oLstAgencia, 'cAgeCod', 'cAgeDescripcion', 'Agencias');
//        },

//    });
//}

//function ObtenerAreas(idSelect, sAgeCod) {
//    var ListaAreas = new Array();
//    //var Areas = ""
//    $.fn.Conexion({
//        direccion: '/General/ListarAreasAgencia',
//        datos: { psAgeCod: sAgeCod },
//        terminado: function (data) {
//            ListaAreas = JSON.parse(data);
//            debugger;
//            CrearLista(idSelect, ListaAreas.oLstAreas, 'cAreaCod', 'cAreaDescripcion', 'Áreas');
//        },
//        //bloqueo: false
//    });
//}

//function ObtenerEstadoRiesgo() {
//    var ListaEstados = new Array();
//    //var Areas = ""
//    $.fn.Conexion({
//        direccion: '/General/ListarConstante',
//        datos: { pnConsCod: 1002 },
//        terminado: function (data) {
//            ListaEstados = JSON.parse(data);
//            debugger;
//            CrearLista('#selEstadoRiesgo', ListaEstados.oLstConstante, 'nConsValor', 'cConsDescripcion', 'Proceso actual del riesgo');
//        },
//        //bloqueo: false
//    });
//}


//function ObtenerUsuariosAreasAgencias(sAgeCod, sAreaCod) {
//    var ListaUsuario = new Array();
//    //var Areas = ""
//    $.fn.Conexion({
//        direccion: '/General/ListarUsuarioAreaAgencia',
//        datos: { psAgeCod: sAgeCod, psAreaCod: sAreaCod },
//        terminado: function (data) {
//            ListaUsuario = JSON.parse(data);
//            debugger;
//            CrearLista('#selUsuario', ListaUsuario.oLstUsuarios, 'cUser', 'cUsuario', 'Usuarios');
//        },
//        //bloqueo: false
//    });
//}

//$("#selAgencia").change(function () {
//    if ($("#selAgencia").val() != "") {
//        ObtenerAreas("#selArea", $("#selAgencia").val());
//    }
//});

//$("#selArea").change(function () {
//    if ($("#selAgencia").val() != "" && $("#selArea").val() != "") {
//        ObtenerUsuariosAreasAgencias($("#selAgencia").val(), $("#selArea").val());
//    }
//});

//$("#selAgenciaAfectada").change(function () {
//    if ($("#selAgenciaAfectada").val() != "") {
//        ObtenerAreas("#selAreaAfectada", $("#selAgencia").val());
//    }
//});


//function MostrarRiesgoOperacionales(sAgeCod, sAreaCod, sUser, sCodRiesgo, sRiesgoIdentificado, nProcRiesgo, nTpoBusqueda) {
//    var sAgeCod = sAgeCod || "";
//    var sAreaCod = sAreaCod || "";
//    var sUser = sUser || "";
//    var sCodRiesgo = sCodRiesgo || "";
//    var sRiesgoIdentificado = sRiesgoIdentificado || "";
//    var nProcRiesgo = nProcRiesgo || 0;
//    var nTpoBusqueda = nTpoBusqueda || 0;

//    $.fn.Conexion({
//        direccion: '/RiesgoOperacional/MostrarRiesgosOperacionales',
//        datos: { psAgeCod: sAgeCod, psAreaCod: sAreaCod, psUser: sUser, psCodRiesgo: sCodRiesgo, psRiesgoIdentificado: sRiesgoIdentificado, pnProcRiesgo: nProcRiesgo, pnTpoBusqueda: nTpoBusqueda },
//        //bloqueo: true,
//        terminado: function (data) {
//            datos = JSON.parse(data);
//            if (datos.oLstRiesgoOperacional != null) {
//                var $opciones = null;
//                if (gUsuario.cRHCargoCod == "005011") {
//                    $opciones = [{
//                        Columna: "Tipo1", id: "id-detalle-riesgo", claseIcono: "entypo-info-circled", clase: "DetalleRiesgo", titulo: "Detalles del riesgo", function: function (e) {
//                            var ObjParam = {
//                                psNroRiesgo: base64_encode($(e).parents("tr").find("td").eq(1).html())
//                            };
//                            $.fn.Conexion({
//                                direccion: '/RiesgoOperacional/DevolverVistaParam',
//                                datos: { Vista: "DetalleRiesgo", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
//                                terminado: function (data) {
//                                    window.location = data.url;
//                                },
//                            });

//                        }
//                    },
//                    {
//                        Columna: "Tipo1", id: "id-gestion-riesgo", claseIcono: "entypo-cog", clase: "GestionarRiesgo", titulo: "Gestionar riesgo", function: function (e) {
//                            var ObjParam = {
//                                psNroRiesgo: base64_encode($(e).parents("tr").find("td").eq(1).html())
//                            };
//                            $.fn.Conexion({
//                                direccion: '/RiesgoOperacional/DevolverVistaParam',
//                                datos: { Vista: "GestionRiesgo", Controlador: "RiesgoOperacional", Parametros: JSON.stringify(ObjParam) },
//                                terminado: function (data) {
//                                    window.location = data.url;
//                                },
//                            });
//                        }
//                    }]
//                }
//                $("#tbl-riesgo-operacional-gestion").Tabla({
//                    tblId: "tbl-riesgo-operacional-gestion",
//                    cabecera: "Nro Riesgo,<strong>Código</strong>,<strong>Tipo</strong>,<strong>Riesgo Identificado</strong>,<strong>Usuario</strong>,<strong>Agencia Afectada</strong>,<strong>Área Afectada</strong>,<strong>Proceso Actual</strong>,<strong>Estado Riesgo</strong>,<strong>Fecha Detección</strong>",
//                    campos: "nNroRiesgo,cCodRiesgo,cTpoRiesgo,cRiesgoIdentiticado,oUsuarios.cUser,oAgencias.cAgeDescripcion,oAreas.cAreaDescripcion,cProcRiesgo,cEstadoRiesgo,dFechaDeteccion",
//                    datos: datos.oLstRiesgoOperacional,
//                    classtbl: "table table-responsive responsive",
//                    cantRegVertical: 10,
//                    //scrollVertical: "Si",
//                    alineado: "C,C,C,L,C,C,C,C,C,C",
//                    formato: "0,0,0,0,0,0,0,0,0,3",
//                    controles: "0,0,0,4,0,0,0,0,0,0",
//                    visible: "0,1,0,1,1,1,1,1,0,1",
//                    anchocolumna: "0,10,0,30,5,10,10,10,0,10",
//                    sindata: "No se encontró información para la gestión de los riesgos operacionales",
//                    numerado: "Si",
//                    paginacion: "Si",
//                    ajustar: 'No',
//                    paginacion: { filas: 5, pagina: 1, nombre: 'tabla-riesgo-operacional' },
//                    opciones: $opciones,
//                });
//            }

//        },
//    });
//}



//$("#btnBuscarCodigo").click(function (e) {
//    e.preventDefault();
//    debugger;
//    if (!$.fn.ValidarInput({ html: "#inCodRiesgo" })) {
//        $("#inCodRiesgo").addClass("input-validator");
//        //validacion = false;
//    } else {
//        $("#inCodRiesgo").removeClass("input-validator");
//    }
//    MostrarRiesgoOperacionales('', '', '', $("#inCodRiesgo").val(), '', 0, 1);
//});

//$("#btnBuscarDescripcion").click(function (e) {
//    e.preventDefault();
//    if (!$.fn.ValidarInput({ html: "#inDescripcion" })) {
//        $("#inDescripcion").addClass("input-validator");
//        validacion = false;
//    } else {
//        $("#inDescripcion").removeClass("input-validator");
//    }
//    MostrarRiesgoOperacionales("", "", "", "", $("#inDescripcion").val(), 0, 2);
//});

//$("#selUsuario").change(function () {
//    if (!$.fn.ValidarInput({ html: "#selAgencia", idSelect: true })) {
//        $("#selAgencia").addClass("input-validator");
//        validacion = false;
//    } else {
//        $("#selAgencia").removeClass("input-validator");
//    }

//    if (!$.fn.ValidarInput({ html: "#selArea", idSelect: true })) {
//        $("#selArea").addClass("input-validator");
//        validacion = false;
//    } else {
//        $("#selArea").removeClass("input-validator");
//    }

//    if (!$.fn.ValidarInput({ html: "#selUsuario", idSelect: true })) {
//        $("#selUsuario").addClass("input-validator");
//        validacion = false;
//    } else {
//        $("#selUsuario").removeClass("input-validator");
//    }
//    MostrarRiesgoOperacionales(("#selAgencia").val(), $("#selArea").val(), $("#selUsuario").val(), '', '', 0, 3);
//});

//$("#selAreaAfectada").change(function () {
//    if (!$.fn.ValidarInput({ html: "#selAgenciaAfectada", idSelect: true })) {
//        $("#selAgenciaAfectada").addClass("input-validator");
//        validacion = false;
//    } else {
//        $("#selAgenciaAfectada").removeClass("input-validator");
//    }

//    if (!$.fn.ValidarInput({ html: "#selAreaAfectada", idSelect: true })) {
//        $("#selAreaAfectada").addClass("input-validator");
//        validacion = false;
//    } else {
//        $("#selAreaAfectada").removeClass("input-validator");
//    }
//    MostrarRiesgoOperacionales(("#selAgenciaAfectada").val(), $("#selAreaAfectada").val(), '', '', '', 0, 4);
//});


//$("#selEstadoRiesgo").change(function () {
//    if (!$.fn.ValidarInput({ html: "#selEstadoRiesgo", idSelect: true })) {
//        $("#selEstadoRiesgo").addClass("input-validator");
//        validacion = false;
//    } else {
//        $("#selEstadoRiesgo").removeClass("input-validator");
//    }
//    MostrarRiesgoOperacionales('', '', '', '', '', $("#selEstadoRiesgo").val(), 5);

//});

$("#btnGenerarReporte").click(function (e) {
    e.preventDefault();
    $.fn.Conexion({
        direccion: '/RiesgoOperacional/DevolverVista',
        datos: { Vista: "GenerarExcelRiesgoOperacional", Controlador: "RiesgoOperacional" },
        //bloqueo: true,
        terminado: function (url) {
            window.location.href = url;
        },
    });
});



