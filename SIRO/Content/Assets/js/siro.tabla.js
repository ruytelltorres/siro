(function ($) {
    $.fn.Tabla = function (m) {
        /*	var matrix = {
                    contenedor: "#div",
                    Max:	5 (máximoas fila)
                    alineacion: "center";
                    espacioceldas: "5px";
                    claseFilas: clase1;
                    claseFilasSobre:clase2;
                    funcion_click: function(){}
                    numerado: "si",
                    Datos: {array con los datos}
                    ConGlobo:"texto" //"genera un globo en la fila, si no se le pone o se le pone No no se muestra
                    FilaEnlace: "Si" //por defecto no, define si la fila tiene el cursor de enlace
                    idtabla: "idtabla"
                    paginacion:{filas:10, pagina:1};
                    conexion: {tipo: "EjecutarProcedimiento", procedimiento: "sp_servicioahabitacion_devolver", Datos: "Hotel Sol del Oriente"},
                    opciones: [	{Columna:"Tipo1", clase:"necesario", imagen: "Interface/imagenes_sistemas/editar.png", titulo:titulo1, funcion: function(){}},
                                {Columna:"Tipo2", clase:"necesario", imagen: "Interface/imagenes_sistemas/editar.png", titulo:titulo2, funcion: function(){}},
                                {Columna:"Tipo3", clase:"necesario", imagen: "Interface/imagenes_sistemas/editar.png", titulo:titulo3, funcion: function(){}}
                            ],			
                   alineado:"C,D,I" => C:CENTRAR, D:DERECHA, I:IZQUIERDA,
                   formato:"0,1,2,3,4" =>0:General, 1:Entero, 2:(Decimal-2 decimales), 3:Fecha(tiene que tener el formato YYYY-MM-DD),4:(Porcentaje-2 decimales),
                   controles:"0,1,2,3"=>0:General,1:TextBox,2:TextBox(Decimal),3:Oculto(Hidden)
            }
            */
        var w = m.datos;
        m.tblId = m.tblId || "";
        m.numerado = m.numerado || "No";
        m.contenedor = m.contenedor || this;
        m.cabecera = m.cabecera || "";
        m.scrollVertical = m.scrollVertical || "No";
        m.cantRegVertical = m.cantRegVertical || 4;
        m.campos = m.campos || "";
        m.visible = m.visible || "";
        m.subLista = m.subLista || "Si";
        m.cargando = m.cargando || false;
        m.classtbl = m.classtbl || "table table-hover responsive";
        m.alineado = m.alineado || "";
        m.formato = m.formato || "";
        m.controles = m.controles || "";
        m.anchocolumna = m.anchocolumna || "";
        m.sindata = m.sindata || "No se encontro información";
        m.paginacion = m.paginacion || "No";
        m.ajustar = m.ajustar || "Si";

        var col = m.cabecera.split(",");
        if (m.campos != "") {
            camp = m.campos.split(",");
        } else { camp = [] }

        var alinear;
        if (m.alineado != "") {
            alinear = m.alineado.split(",");
        } else { alinear = [] }

        var tipo;
        if (m.formato != "") {
            tipo = m.formato.split(",");
        } else { tipo = [] }

        var control;
        if (m.controles != "") {
            control = m.controles.split(",");
        } else { control = [] }
        
        var visible;
        if (m.visible != "") {
            visible = m.visible.split(",");
        } else { visible = [] }

        var anchocol;
        if (m.anchocolumna != null) {
            anchocol = m.anchocolumna.split(",");
        } else { anchocol = [] }

        var StyleVisibledCol = "";

        var html = '';
        html = '<table width="100%" data-edit="false" id="' + m.tblId + '" class="' + m.classtbl + ' tbl-siro tblcab">';
        //html = '<div class="' + cssTbl + '"><table width="100%" data-edit="false" id="' + m.tblId + '" class="' + m.classtbl + ' tbl-siro tblcab">';
        var EmtyData = "<tr style='height: 200px;'><td class='no-data' style='text-align: center; vertical-align: middle' colspan=" + (col.length + control.length) + "><strong>" + m.sindata + "</strong></td></tr>";

        if (m.cabecera != "" || m.numerado == "Si") {
            html += '<thead><tr>';
            if (m.numerado == "Si") { html += '<th width="5%" style="text-align: center; vertical-align: middle;"><strong>N\u00B0</strong></th>'; }
            for (i in col) {
                StyleVisibledCol = "";
                if (visible.length > 0) {
                    if (visible[i] == "0") {
                        StyleVisibledCol = ' style="text-align: center; vertical-align: middle; display: none;" '
                    } else {
                        StyleVisibledCol = ' style="text-align: center; vertical-align: middle;" ';
                    }
                } else {
                    StyleVisibledCol = ' style="text-align: center; vertical-align: middle;" ';
                }
                
                if (anchocol.length > 0) {
                    if (eval(anchocol[i]) > 0) {
                        html += '<th width="' + anchocol[i] + '%" ' + StyleVisibledCol + '>' + col[i] + '</th>';
                    }
                }
                
            }
            
            if (m["seleccion"]) { html = html + "<th width='15%' style='text-align:center; vertical-align: middle;'><strong>Seleccionar</strong></th>"; }
            
            if (m["opciones"]) { html = html + "<th width='18%' style='text-align:center; vertical-align: middle;'><strong>Acciones</strong></th>"; }
            
            html += '</tr></thead>';
        }

        html += '<tbody>';
        n = 0;

        for (i in m.datos) {
            if (m["paginacion"]) {
                if (n + 1 > m["paginacion"]["filas"]) {
                    html += "<tr class='gFila-" + m.tblId + "' style='display:none'>";
                } else {
                    html += "<tr  class='gFila-" + m.tblId + "'>";
                }
            } else {
                html += "<tr>";
            }

            if (m.numerado == "Si")
                html += '<td class="text-center">' + (n + 1) + '</td>';

            var alineacion = "";
            var dat;
            if (camp.length > 0) {

                for (k in camp) {
                    if (alinear.length > 0) {
                        alineacion = (alinear[k] == "C" ? "text-center" : (alinear[k] == "D" ? "text-right" : "text-left"));
                    }

                    dat = eval("m.datos[i]." + camp[k]);
                    dat = (dat == null ? "" : dat);

                    if (tipo.length > 0) {
                        switch (tipo[k]) {
                            case "1": { break; }
                            case "2": { dat = number_format(dat, 2); break; }
                            case "3": { dat = moment(dat).format("DD/MM/YYYY") != '01/01/0001' ? moment(dat).format("DD/MM/YYYY"):''; break; }
                            case "4": { dat = number_format(dat * 100.00, 2); break; }
                        }
                    }

                    if (control.length > 0) {
                        switch (control[k]) {
                            case "1": { dat = '<input type="text" class="form-control ' + k + 'text" value="' + dat + '"/>'; break; }
                            case "2": { dat = '<input type="text" class="form-control text-right ' + k + 'text" onkeypress="return val_09DCN(event,this);" value="' + dat + '" maxlength="7">'; break; }
                            case "3": { dat = '<input type="hidden" class="' + k + 'text"  value="' + dat + '">'; break; }
                            case "4": { dat = '<div class="scrollable"><p class="text-primary text-justify">' + dat + '</p></div>'; break; }
                            case "5": { dat = '<p class="text-primary">' + dat + '</p>'; break; }
                            case "6": { dat = '<div class="input-spinner"><button type="button" class="btn btn-default">-</button><input type="text" class="form-control size-1" value="' + dat + '"/><button type="button" class="btn btn-default">+</button></div>'; break; }
                        }
                    }

                    StyleVisibledCol = "";
                    if (visible.length > 0) {
                        if (visible[k] == "0") {
                            StyleVisibledCol = ' style="display:none" ';
                        } else {
                            StyleVisibledCol = "";
                        }
                    } else {
                        StyleVisibledCol = "";
                    }

                    html += '<td  ' + StyleVisibledCol + ' class="' + alineacion + '">' + dat + '</td>';
                }
            }
            else {
                if (m.subLista == "No") {
                    html += '<td>' + m.datos[i] + '</td>';
                } else {
                    for (j in m.datos[i]) {
                        if (alinear.length > 0) {
                            alineacion = (alinear[k] == "C" ? "text-center" : (alinear[k] == "D" ? "text-right" : "text-left"));
                        }
                        dat = m.datos[i][j];
                        dat = (dat == null ? "" : dat);

                        if (tipo.length > 0) {
                            switch (tipo[k]) {
                                case "1": { break; }
                                case "2": { dat = number_format(dat, 2); break; }
                                case "3": { dat = moment(dat).format("DD/MM/YYYY a las hh:mm:ss"); break; }
                                case "4": { dat = number_format(dat * 100.00, 2); break; }
                            }
                        }

                        if (control.length > 0) {
                            switch (control[k]) {
                                case "1": { dat = '<input type="text" class="form-control ' + k + 'text" value="' + dat + '"/>'; break; }
                                case "2": { dat = '<input  type="text" class="form-control text-right ' + k + 'text" onkeypress="return val_09DCN(event,this);" value="' + dat + '" maxlength="7">'; break; }
                                case "3": { dat = '<input type="hidden" class="' + k + 'text" value="' + dat + '">'; break; }
                                case "4": { dat = '<div class="scrollable"><p class="text-primary">' + dat + '</p></div>'; break; }
                                case "5": { dat = '<p class="text-primary">' + dat + '</p>'; break; }
                                case "6": { dat = '<div class="input-spinner"><button type="button" class="btn btn-default">-</button><input type="text" class="form-control size-1" value="' + dat + '"/><button type="button" class="btn btn-default">+</button></div>'; break; }
                            }
                        }

                        StyleVisibledCol = "";
                        if (visible.length > 0) {
                            if (visible[j] == "0") {
                                StyleVisibledCol = ' style="display:none" ';
                            } else {
                                StyleVisibledCol = "";
                            }
                        } else {
                            StyleVisibledCol = "";
                        }

                        html += '<td  ' + StyleVisibledCol + ' class="' + alineacion + '">' + dat + '</td>';
                    }
                }
            }
            if (m["seleccion"]) {
                $.each(m["seleccion"], function (k, v) {
                    //html += "<td style='text-align:center'><div class='checkbox checkbox-replace'><input type='checkbox' id='chkSel'></div></td>";
                    html += "<td style='text-align:center'><input type='checkbox' id='chkSel'></td>";
                    //html += "<td style='text-align:center'><div class='checkbox checkbox-replace neon-cb-replacement'><label class='cb-wrapper'></label><input type='checkbox' id='chkSel'></div></td>";

                });
            };

            if (m["opciones"]) {
                html += "<td style='text-align:center'>";
                html += "<div class='btn-group'>";

                // Las primeras 2 opciones siempre se muestran como botones individuales.
                // A partir de la 3ra opcion (si existen mas de 2), estas se agrupan en el dropdown.
                var opcionesVisibles = m["opciones"].slice(0, 2);
                var opcionesAgrupadas = m["opciones"].length > 2 ? m["opciones"].slice(2) : [];

                $.each(opcionesVisibles, function (k, v) {
                    html += "<button type='button' id='" + v.id + "' class='btn btn-primary " + v.clase + "' title='" + v.titulo + "' style='margin-right:4px'>";
                    html += "<i class=" + v.claseIcono + "></i>";
                    html += "</button>";
                });

                if (opcionesAgrupadas.length > 0) {
                    html += "<button type='button' class='btn btn-default btn-sm dropdown-toggle' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>"
                    //html += "<span class='caret'></span>"
                    html += "<i class='entypo-dot-3'></i>"
                    html += "<span class='sr-only'>Toggle Dropdown</span>"
                    html += "</button>"
                    html += "<ul class='dropdown-menu dropdown-default pull-right'>"
                    $.each(opcionesAgrupadas, function (k, v) {
                        html += "<li id='" + v.id + "'><a href='#' class='" + v.clase + "'><i class=" + v.claseIcono + "></i> " + v.titulo + "</a></li>"
                    });
                    html += "</ul>"
                }

                html += "</div>"
                html += "</td>";

            };
            html += '</tr>';
            n++;
        }
        if (m.datos == null || m.datos.length == 0) { html += EmtyData; }

        html += '</tbody>';
        if (m["paginacion"] != "No" && m.datos != null && m.datos.length > 0) {
            html += '<tfoot><tr><td colspan=' + (col.length + control.length) + '>';
            html += '<div class="pull-right"><div class="btn-group">';
            html += '<input type="button" class="btn btn-white gPagPrimero-' + m.tblId + '" id="gPagPrimero-' + m.tblId + '" value="Inicio">';
            html += '<input type="button" class="btn btn-white gPagAnterior-' + m.tblId + '" id="gPagAnterior-' + m.tblId + '" value="Anterior">';
            html += '<input type="button" class="btn btn-white gPagNumero-' + m.tblId + '" id="gPagNumero-' + m.tblId + '" value="1"></label>';
            html += '<input type="button" class="btn btn-white gPagSiguiente-' + m.tblId + '"id="gPagSiguiente-' + m.tblId + '" value="Siguiente">';
            html += '<input type="button" class="btn btn-white gPagUltimo-' + m.tblId + '" id="gPagUltimo-' + m.tblId + '" value="Ultimo">';
            html += '</div></div>';
            html += '</td></tr></tfoot>';
        }
        html += '</table>';

        $(m.contenedor).html(html);
        if (m["opciones"]) {
            $.each(m["opciones"], function (k, v) {
                $("." + v.clase).bind("click", function () {
                    v.function(this);
                });
            });
        };

        $("#" + m.tblId + " tbody tr").bind("click", function () {
            $("#" + m.tblId + " tbody tr").removeClass("seleccionado");
            $(this).addClass("seleccionado");

        })

        if (m["paginacion"] != "No") {
            function totalpaginas() {
                var pint = parseInt($(".gFila-" + m.tblId + "").length / m["paginacion"]["filas"]);
                var pdecimal = $(".gFila-" + m.tblId + "").length / m["paginacion"]["filas"];
                if (pint == pdecimal) {
                    paginas = pint;
                } else {
                    paginas = pint + 1;
                }
                return paginas;
            }

            function paginar(s) {
                x = 0;
                pagina = m["paginacion"]["pagina"];
                filas = m["paginacion"]["filas"];
                desde = (pagina * filas) - filas + 1;
                hasta = pagina * filas;
                $("." + s).each(function () {
                    x = x + 1;
                    if (x >= desde && x <= hasta) { $(this).show(); } else { $(this).hide(); }
                });

                $(m["contenedor"]).find("input").prop("disabled", false);
                if (pagina == 1) {
                    $("#gPagAnterior-" + m.tblId + ",#gPagPrimero-" + m.tblId + "").prop("disabled", true);
                }
                if (pagina == totalpaginas()) {
                    $("#gPagSiguiente-" + m.tblId + ",#gPagUltimo-" + m.tblId + "").prop("disabled", true);
                }
                //else {
                //    $("#gPagSiguiente-" + m.tblId + ",#gPagUltimo-" + m.tblId + "").prop("disabled", true);
                //}
                $("#gPagNumero-" + m.tblId + "").prop({ value: "Pag. " + pagina + " de " + totalpaginas(), disabled: true });
            }

            paginar(1);

            $("#gPagSiguiente-" + m.tblId + "").bind("click", function (e) { e.preventDefault(); m["paginacion"]["pagina"] = m["paginacion"]["pagina"] + 1; paginar("gFila-" + m.tblId + ""); });
            $("#gPagAnterior-" + m.tblId + "").bind("click", function (e) { e.preventDefault(); m["paginacion"]["pagina"] = m["paginacion"]["pagina"] - 1; paginar("gFila-" + m.tblId + ""); });
            $("#gPagPrimero-" + m.tblId + "").bind("click", function (e) { e.preventDefault(); m["paginacion"]["pagina"] = 1; paginar("gFila-" + m.tblId + ""); });
            $("#gPagUltimo-" + m.tblId + "").bind("click", function (e) { e.preventDefault(); m["paginacion"]["pagina"] = totalpaginas(); paginar("gFila-" + m.tblId + ""); });
            //$(document).bind("keydown", function (e) {
            //    if (e.which == 37 && typeof ($("#gPagAnterior-" + m.tblId + "").attr("disabled")) == "undefined") { $("#gPagAnterior-" + m.tblId + "").click(); };
            //    if (e.which == 39 && typeof ($("#gPagSiguiente-" + m.tblId + "").attr("disabled")) == "undefined") { $("#gPagSiguiente-" + m.tblId + "").click(); };
            //});
        }
    }


})(jQuery);

(function ($) {
    $.uiTableFilter = function (jq, phrase, column, ifHidden) {
        var new_hidden = false;
        if (this.last_phrase === phrase) return false;

        var phrase_length = phrase.length;
        var words = phrase.toLowerCase().split(" ");

        // these function pointers may change
        var matches = function (elem) { elem.show() }
        var noMatch = function (elem) { elem.hide(); new_hidden = true }
        var getText = function (elem) { return elem.text() }

        if (column) {
            var index = null;
            $(jq).find("thead > tr:last > th").each(function (i) {
                if ($.trim($(this).text()) == column) {
                    index = i; return false;
                }
            });
            if (index == null) throw ("given column: " + column + " not found")

            getText = function (elem) {
                return $(elem.find(
                    ("td:eq(" + index + ")"))).text()
            }
        }

        // if added one letter to last time,
        // just check newest word and only need to hide
        if ((words.size > 1) && (phrase.substr(0, phrase_length - 1) ===
            this.last_phrase)) {

            if (phrase[-1] === " ") { this.last_phrase = phrase; return false; }

            var words = words[-1]; // just search for the newest word

            // only hide visible rows
            matches = function (elem) { ; }
            var elems = $(jq).find("tbody:first > tr:visible")
        }
        else {
            new_hidden = true;
            var elems = $(jq).find("tbody:first > tr")
        }

        elems.each(function () {
            var elem = $(this);
            $.uiTableFilter.has_words(getText(elem), words, false) ?
                matches(elem) : noMatch(elem);
        });

        last_phrase = phrase;
        if (ifHidden && new_hidden) ifHidden();
        return jq;
    };

    $.uiTableFilter.last_phrase = ""

    $.uiTableFilter.has_words = function (str, words, caseSensitive) {
        var text = caseSensitive ? str : str.toLowerCase();
        for (var i = 0; i < words.length; i++) {
            if (text.indexOf(words[i]) === -1) return false;
        }
        return true;
    }
})(jQuery);