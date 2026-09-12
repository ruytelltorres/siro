var tree = new Object();

window.onload = function () {
    $.fn.Conexion({
        direccion: '/Menu/MenuSistema',
        mensaje: 'Cargando opciones de menú del sistema',
        bloqueo: true,
        terminado: function (data) {
            $('#MenuSistema').treeview({
                data: data,
                expandIcon: 'entypo-down-dir',
                collapseIcon: 'entypo-right-dir',
                emptyIcon: '',
                nodeIcon: '',
                selectedIcon: '',
                checkedIcon: 'fa fa-check-square',//'entypo-check',
                uncheckedIcon: 'fa fa-square-o', //'entypo-cancel',

                color: undefined, // '#000000',
                backColor: undefined, // '#FFFFFF',
                borderColor: undefined, // '#dddddd',
                onhoverColor: '#F5F5F5',
                selectedColor: '#4F4E4E',
                selectedBackColor: '#F5F5F5', //'#428bca',
                searchResultColor: '#D9534F',
                searchResultBackColor: undefined, //'#FFFFFF',


                enableLinks: false,
                highlightSelected: true,
                highlightSearchResults: true,
                showBorder: true,
                showIcon: false,
                showCheckbox: true,
                showTags: false,
                multiSelect: false,


                onNodeChecked: function (event, data) {
                    if (!$.fn.ValidarInput({ html: "#selGrupos", idSelect: true })) {
                        $.fn.MensajeProcesos({
                            //clase: 'red',
                            posicion: 'A',
                            mensaje: 'Falta seleccionar al usuario para activar el menu',
                            titulo: 'Mensaje del Sistema',
                            tipo_mensaje: 'informacion'

                        });

                        return;
                    }

                    var lsUsuario = $("#selGrupos").val();
                    var lsMenuId = data.id;

                    ActivarOpcionSistema(lsUsuario, lsMenuId, 1)
                    //$('#MenuSistema').treeview('checkNode', [data.nodeId, { silent: true }]);
                    //debugger;
                },
                //onNodeCollapsed: function (event, node) {
                //    debugger;
                //    //nodeIdList.push(node.nodeId);
                //    $.each(node.nodes, function () {
                //        if (this.state.selected) {
                //            $('#MenuSistema').treeview('selectNode', [nodeIdList[0], { silent: true }]);
                //            hasSelect = true;
                //        }
                //    });
                //},
                //onNodeDisabled: undefined,
                //onNodeEnabled: undefined,

                //onNodeExpanded: function (event, node) {
                //    debugger;
                //    $.each(node.nodes, function () {
                //        if ((this.nodeId == selectedNodeId || nodeIdList.indexOf(this.nodeId) > -1)
                //            && hasSelect) {
                //            $('#MenuSistema').treeview('selectNode',[this.nodeId, { silent: true }]);
                //        }
                //    });
                //}, 
                onNodeSelected: function (event, data) {
                    //$(this).treeview('unselectNode', [data.nodeId, { silent: false }]);

                    //var lsUsuario = $("#selGrupos").val() == null ? '' : $("#selGrupos").val();

                    //ObtenerCargosPermitidosMenu(data.id, lsUsuario, 1)


                },
                onNodeUnchecked: function (event, data) {
                    if (!$.fn.ValidarInput({ html: "#selGrupos", idSelect: true })) {
                        $.fn.MensajeProcesos({
                            //clase: 'red',
                            posicion: 'A',
                            mensaje: 'Falta seleccionar al usuario para desactivar el menu',
                            titulo: 'Mensaje del Sistema',
                            tipo_mensaje: 'informacion'

                        });

                        return;
                    }

                    var lsUsuario = $("#selGrupos").val();
                    var lsMenuId = data.id;

                    ActivarOpcionSistema(lsUsuario, lsMenuId, 2)
                },
                //onNodeUnselected: function (event, node) {
                //    debugger;
                //    $(this).treeview('selectNode', [node.nodeId, { silent: true }]);
                //}

                //onSearchComplete: undefined,
                //onSearchCleared: undefined

            });
            //$('#MenuSistema').treeview('collapseAll', { silent: true }); //Contraer todos los nodos
            //$('#MenuSistema').treeview('checkAll', { silent: true }); //Hacer check en todas las opciones
        },
        //bloqueo: false
    });

    ObtenerGrupos();
}





//#region Opcion: Usuarios por areas
function ObtenerGrupos() {
    $.fn.Conexion({
        direccion: '/Menu/Grupos',
        bloqueo: true,
        mensaje: 'Obteniendo los grupos configurados. Por favor espere...',
        terminado: function (data) {
            datos = JSON.parse(data);
            $("#selGrupos").selselect2({ dataShow: "cConsDescripcion", dataValue: "cConsDescripcion", datalist: datos });
        },
        //bloqueo: false
    });
}
//#endregion

//#region Carga TreeView
//function MenuSistema() {
//    $.fn.Conexion({
//        direccion: '/Menu/NodosMenuSistema',
//        mensaje: 'Cargando opciones de menú del sistema',
//        bloqueo: true,
//        terminado: function (data) {
//            //ObtenerAgencias();
//            ObtenerAreas();
//            $('#MenuSistema').treeview({
//                data: data,
//                expandIcon: 'entypo-down-dir',
//                collapseIcon: 'entypo-right-dir',
//                emptyIcon: '',
//                nodeIcon: '',
//                selectedIcon: '',
//                checkedIcon: 'fa fa-check-square',//'entypo-check',
//                uncheckedIcon: 'fa fa-square-o', //'entypo-cancel',

//                color: undefined, // '#000000',
//                backColor: undefined, // '#FFFFFF',
//                borderColor: undefined, // '#dddddd',
//                onhoverColor: '#F5F5F5',
//                selectedColor: '#4F4E4E',
//                selectedBackColor: '#F5F5F5', //'#428bca',
//                searchResultColor: '#D9534F',
//                searchResultBackColor: undefined, //'#FFFFFF',


//                enableLinks: false,
//                highlightSelected: true,
//                highlightSearchResults: true,
//                showBorder: true,
//                showIcon: false,
//                showCheckbox: true,
//                showTags: false,
//                multiSelect: false,


//                onNodeChecked: function (event, data) {
//                    if (!$.fn.ValidarInput({ html: "#selUsuarios", idSelect: true })) {
//                        $.fn.MensajeProcesos({
//                            //clase: 'red',
//                            posicion: 'A',
//                            mensaje: 'Falta seleccionar al usuario para activar el menu',
//                            titulo: 'Mensaje del Sistema',
//                            tipo_mensaje: 'informacion'

//                        });

//                        return;
//                    }

//                    var lsUsuario = $("#selUsuarios").val();
//                    var lsMenuId = data.id;

//                    ActivarOpcionSistema(lsUsuario, lsMenuId, 1)
//                    //$('#MenuSistema').treeview('checkNode', [data.nodeId, { silent: true }]);
//                    //debugger;
//                },
//                //onNodeCollapsed: function (event, node) {
//                //    debugger;
//                //    //nodeIdList.push(node.nodeId);
//                //    $.each(node.nodes, function () {
//                //        if (this.state.selected) {
//                //            $('#MenuSistema').treeview('selectNode', [nodeIdList[0], { silent: true }]);
//                //            hasSelect = true;
//                //        }
//                //    });
//                //},
//                //onNodeDisabled: undefined,
//                //onNodeEnabled: undefined,

//                //onNodeExpanded: function (event, node) {
//                //    debugger;
//                //    $.each(node.nodes, function () {
//                //        if ((this.nodeId == selectedNodeId || nodeIdList.indexOf(this.nodeId) > -1)
//                //            && hasSelect) {
//                //            $('#MenuSistema').treeview('selectNode',[this.nodeId, { silent: true }]);
//                //        }
//                //    });
//                //}, 
//                onNodeSelected: function (event, data) {
//                    //$(this).treeview('unselectNode', [data.nodeId, { silent: false }]);

//                    var lsUsuario = $("#selUsuarios").val() == null ? '' : $("#selUsuarios").val();

//                    ObtenerCargosPermitidosMenu(data.id, lsUsuario, 1)


//                },
//                onNodeUnchecked: function (event, data) {
//                    if (!$.fn.ValidarInput({ html: "#selUsuarios", idSelect: true })) {
//                        $.fn.MensajeProcesos({
//                            //clase: 'red',
//                            posicion: 'A',
//                            mensaje: 'Falta seleccionar al usuario para desactivar el menu',
//                            titulo: 'Mensaje del Sistema',
//                            tipo_mensaje: 'informacion'

//                        });

//                        return;
//                    }

//                    var lsUsuario = $("#selUsuarios").val();
//                    var lsMenuId = data.id;

//                    ActivarOpcionSistema(lsUsuario, lsMenuId, 2)
//                },
//                //onNodeUnselected: function (event, node) {
//                //    debugger;
//                //    $(this).treeview('selectNode', [node.nodeId, { silent: true }]);
//                //}

//                //onSearchComplete: undefined,
//                //onSearchCleared: undefined

//            });
//            //$('#MenuSistema').treeview('collapseAll', { silent: true }); //Contraer todos los nodos
//            //$('#MenuSistema').treeview('checkAll', { silent: true }); //Hacer check en todas las opciones
//        },
//        //bloqueo: false
//    });
//}
//#endregion

function ContraerExpandirMenu(nAccionar) {
    if (nAccionar == 1) {
        $('#MenuSistema').treeview('expandAll', { silent: true }); //Expandir nodos
    } else if (nAccionar == 2) {
        $('#MenuSistema').treeview('collapseAll', { silent: true }); //Contraer nodos
    }
}

function ActivarOpcionSistema(sGrupoUsuario, sMenuId, nOpcion) {
    $.fn.Conexion({
        direccion: '/Menu/ActivarMenuSistema',
        datos: { psGrupoUsu: sGrupoUsuario, psMenuId: sMenuId, Opcion: nOpcion },
        bloqueo: true,
        terminado: function (datos) {
            if (datos.Mensaje == '') {
                //if (nOpcion == 1) {
                //    $.fn.MensajeProcesos({
                //        posicion: 'A',
                //        mensaje: 'Se activo la opción al usuario',
                //        titulo: 'Mensaje del Sistema',
                //        tipo_mensaje: 'exito'

                //    });
                //} else if (nOpcion == 2) {
                //    $.fn.MensajeProcesos({
                //        posicion: 'A',
                //        mensaje: 'Se desactivo la opción al usuario',
                //        titulo: 'Configuración - Opciones del menú',
                //        tipo_mensaje: 'exito'
                //    });
                //}
            } else {
                $.fn.MensajeProcesos({
                    posicion: 'A',
                    mensaje: datos.Mensaje,
                    titulo: 'Configuración - Opciones del menú',
                    tipo_mensaje: 'informacion'
                });
                //SeleccionarOpcionesUsuario(sGrupoUsuario);
            }
        },
        //bloqueo: false
    });
}

//function ObtenerCargosPermitidosMenu(idMenu, Usuario, Proceso) {
//    $.fn.Conexion({
//        direccion: '/Menu/ObtenerInfPermisoMenu',
//        datos: { cMenuId: idMenu, cUsuario: Usuario, Proceso: Proceso },
//        bloqueo: true,
//        terminado: function (data) {
//            datos = JSON.parse(data);
//            $('.todo-list').empty();
//            var $ListaCargos = $("#ListaCargos");
//            if (datos.length > 0) {
//                var cargo = new Object();
//                for (i = 0; i < datos.length; i++) {
//                    cargo = $('<li><label>' + datos[i].oUsuario.cRHCargoCod + " " + datos[i].oUsuario.cRHCargoDescripcion + '</label><a href="#" class="bg" onclick="EliminarCargoOpcionMenu(' + "'" + idMenu + "'" + ', ' + "'" + datos[i].oUsuario.cRHCargoCod + "'" + ')"> <i class="entypo-trash"></a></li>');
//                    cargo.appendTo($ListaCargos.find('.todo-list'));

//                }
//                $('#ListaCargos').val('');

//                cargo.hide().slideDown('fast');
//                replaceCheckboxes();

//            } else {

//                var $todo_entry = $('<li><label>Todos los cargos</label></li>');
//                $('#ListaCargos').val('');

//                $todo_entry.appendTo($ListaCargos.find('.todo-list'));
//                $todo_entry.hide().slideDown('fast');
//                replaceCheckboxes();
//            }
//        },
//        bloqueo: false
//    });
//}

function EliminarCargoOpcionMenu(idMenu, CodCargo) {
    alert("idMenu: " + idMenu + ",  CodCargo " + CodCargo);
}


$("#selGrupos").change(function () {
    var lsUsuario = $("#selGrupos").val();
    SeleccionarOpcionesUsuario(lsUsuario);
    //$.fn.Conexion({
    //    direccion: '/Menu/ObtenerOpcionesMenuUsuario',
    //    datos: { psUsuario: lsUsuario },
    //    bloqueo: true,
    //    mensaje: 'Obteniendo opciones de usuario',
    //    terminado: function (data) {
    //        datos = JSON.parse(data);
    //        if (datos.length > 0) {
    //            for (i in datos) {
    //                var nodo = $('#MenuSistema').treeview('search', [datos[i].cTitulo, { ignoreCase: true, exactMatch: true }]);
    //                $('#MenuSistema').treeview('checkNode', [nodo, { silent: true }]);
    //            }
    //        } else {
    //            $('#MenuSistema').treeview('uncheckAll', { silent: true });
    //            $.fn.MensajeProcesos({
    //                posicion: 'A',
    //                mensaje: 'No se encontro opciones del menu activos para el usuario ' + lsUsuario,
    //                titulo: 'Mensaje del Sistema',
    //                tipo_mensaje: 'informacion'
    //            });
    //        }
    //    },
    //    //bloqueo: false
    //});
});
function SeleccionarOpcionesUsuario(usuario) {
    $.fn.Conexion({
        direccion: '/Menu/ObtenerOpcionesMenuUsuario',
        datos: { psUsuario: usuario },
        bloqueo: true,
        mensaje: 'Obteniendo opciones de usuario',
        terminado: function (data) {
            datos = JSON.parse(data);
            if (datos.length > 0) {
                for (i in datos) {
                    var nodo = $('#MenuSistema').treeview('search', [datos[i].cTitulo, { ignoreCase: true, exactMatch: true }]);
                    $('#MenuSistema').treeview('checkNode', [nodo, { silent: true }]);
                }
            } else {
                $('#MenuSistema').treeview('uncheckAll', { silent: true });
                $.fn.MensajeProcesos({
                    posicion: 'A',
                    mensaje: 'No se encontro opciones del menu activos para el usuario ' + usuario,
                    titulo: 'Mensaje del Sistema',
                    tipo_mensaje: 'informacion'
                });
            }
        },
    });
}


//$("#btnGuardar").click(function (e) {
//    e.preventDefault();
//    //$.fn.Conexion({
//    //    direccion: '/RiesgoOperacional/RegistrarRiesgoOperacional',
//    //    //datos: {},
//    //    bloqueo: true,
//    //    terminado: function (datos) {

//    //    },
//    //    bloqueo: false
//    //});
//});

