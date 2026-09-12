(function ($) {
    $.fn.Browser = function () {
        var isOpera = (!!window.opr && !!opr.addons) || !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
        var isFirefox = typeof InstallTrigger !== 'undefined';
        var isIE = /*@cc_on!@*/false || !!document.documentMode;
        var isEdge = !isIE && !!window.StyleMedia;
        var isChrome = !!window.chrome && (!!window.chrome.webstore || !!window.chrome.runtime);
        var isSafari = /constructor/i.test(window.HTMLElement) || (function (p) {
            return p.toString() === "[object SafariRemoteNotification]";
        })(!window['safari'] || (typeof safari !== 'undefined' && safari.pushNotification));
        var isBlink = (isChrome || isOpera) && !!window.CSS;

        if (isChrome) { return "CHROME"; }
        else if (isBlink) { return "BLINK"; }
        else if (isSafari) { return "SAFARI"; }
        else if (isIE) { return "IE"; }
        else if (isEdge) { return "EDGE"; }
        else if (isOpera) { return "OPERA"; }
        else if (isFirefox) { return "FIREFOX"; }
        else { return ""; }
    }

    $.fn.ValidateNewFile = function (file) {
        var Formats = ["xls", "xlsx", "doc", "docx", "pdf", "ppt", "pptx", "png", "jpg", "jpeg", "bmp", "txt"];
        var name = file.name.split(".");
        var contenido = file.size;
        name = name[name.length - 1];

        if (contenido > 2000000) {
            // $.fn.Msn("El peso del archivo debe ser menor a 2 MB");
            $.fn.MensajeProcesos({
                //clase: 'red',
                posicion: 'A',
                mensaje: 'El peso del archivo debe ser menor a 2 MB',
                titulo: 'Mensaje del Sistema',
                tipo_mensaje: 'informacion'
            });

            //$.fn.Mensaje({
            //    mensaje: "El peso del archivo debe ser menor a 2 MB",
            //    tipo: "Aceptar",
            //    tamano: "sm",
            //});
            return false;
        }
        if (!Formats.includes(name.toLowerCase())) {
            //$.fn.Msn("Solo puede subir archivos con los formatos: xls, xlsx, doc, docx, ppt, pptx, pdf, png, jpg, jpeg, bmp, txt");
            //$.fn.Mensaje({
            //    mensaje: "Solo puede subir archivos con los formatos: xls, xlsx, doc, docx, ppt, pptx, pdf, png, jpg, jpeg, bmp, txt",
            //    tipo: "Aceptar",
            //    tamano: "sm",
            //});

            $.fn.MensajeProcesos({
                clase: 'red',
                posicion: 'A',
                mensaje: 'Solo puede subir archivos con los formatos: xls, xlsx, doc, docx, ppt, pptx, pdf, png, jpg, jpeg, bmp, txt',
                titulo: 'Mensaje del Sistema',
                tipo_mensaje: 'informacion'

            });

            return false;
        }
        return true;
    }

    $.fn.CargarArchivos = function (m) {
        m.cDom = m.cDom || "";
        m.btn = m.btn || "";
        m.nCantidad = m.nCantidad || 1;
        m.cClase = m.cClase || 'table table-bordered';

        var nIndex = 0;
        var idTabla = "tblFilesTmp1234";
        var idForm = "formFilesTmp1234";
        var _oItem = {}; var lFiles = [];
        var cEmpty = "<tr class='text-center'><td colspan='3' style='color: black'><p>No se encontr\u00F3 archivos adjuntos</p></td></tr>";

        function TmpEventsTblFiles() {
            $(".tdTmpOpe .btn-cmacm").unbind("click").click(function (e) {
                e.preventDefault();
                var item = $(this).parent().parent();
                item.remove();
                if ($("#" + idTabla + " tbody tr .tdTmpFile").length == 0) {
                    $("#" + idTabla + " tbody").html(cEmpty);
                }
                Indexar(idTabla);
            });
        }

        function Indexar(idTabla) {
            var Filas = $("#" + idTabla + " tbody tr");
            for (var i = 0; i < Filas.length; i++) {
                var Dom = Filas.eq(i).find(".tdTmpIndex");
                if (Dom != null && Dom != undefined) {
                    Dom.html((i + 1).toString());
                }
            }
        }

        var cCadena = "<form id='" + idForm + "' method='post' enctype=\"multipart/form-data\">" +
            "<table id='" + idTabla + "' class='" + m.cClase + "'>" +
            "<thead><tr ><th class='text-center' width='7%' data-key='Archivo(s)'>N\u00B0</th><th class='text-center' width='60%' data-key='Adjuntado(s)'>Adjuntado(s)</th><th class='text-center' width='10%' data-key='Acción'>Acción</th></tr>" +
            "</thead><tbody>" + cEmpty + "</tbody></table></form><input type='file' id='tmpFirstFile' style='display: none'/>";
        $(m.cDom).html(cCadena);

        document.getElementById("tmpFirstFile").onchange = function () {
            nIndex++;
            if ($("#" + idTabla + " tbody tr .tdTmpFile").length >= m.nCantidad) {

                $.fn.MensajeProcesos({
                    clase: 'red',
                    posicion: 'A',
                    mensaje: 'Solo se pueden adjuntar máximo ' + m.nCantidad + ' archivo(s)',
                    titulo: 'Mensaje del Sistema',
                    tipo_mensaje: 'informacion'

                });
                return;
            }
            var nItem = { nNro: nIndex, cId: "inTmpTblFiles" + nIndex.toString(), cName: "tmpfile" + nIndex.toString() };
            var ccCadena = "<tr class='tmpClassFile'><td style='padding-top: 15px' class='tdTmpIndex'>1</td>" +
                "<td style='padding-top: 15px' class='tdTmpFile'>" +
                "<input type='file' name='" + nItem.cName + "' id='" + nItem.cId + "' class='custom-file-input' style='display: none'/>" +
                "<div class='divTmpFile'</div>" + nItem.cName + "</td>" +
                "<td class='tdTmpOpe text-center'><button class='btn btn-danger'><i class=\"entypo-cancel\"></i></button></td></tr>";
            if ($("#" + idTabla + " tbody tr .tdTmpFile").length == 0) { $("#" + idTabla + " tbody").html(""); }

            $(".tmpClassFile").removeClass("tmpClassFile");
            $("#" + idTabla + " tbody").append(ccCadena);
            TmpEventsTblFiles();

            document.getElementById(nItem.cId).onchange = function (event) {
                if ($("#" + nItem.cId)[0].files.length == 0 || !$.fn.ValidateNewFile($("#" + nItem.cId)[0].files[0])) {
                    $(this).parent().parent().remove();
                    if ($("#" + idTabla + " tbody tr .tdTmpFile").length == 0) {
                        $("#" + idTabla + " tbody").html(cEmpty);
                    }
                } else {
                    var cNombreArchivo = $("#" + nItem.cId)[0].files[0].name;
                    var Filas = $("#" + idTabla + " tbody tr");
                    var nExiste = false;
                    for (var i = 0; i < Filas.length; i++) {
                        var tmpNombre = Filas.eq(i).find(".tdTmpFile").find(".divTmpFile").html();
                        if (tmpNombre == cNombreArchivo) {
                            nExiste = true;
                        }
                    }
                    if (!nExiste) {
                        $(this).parent().find(".divTmpFile").html(cNombreArchivo);
                    } else {
                        $(this).parent().parent().remove();
                        $.fn.MensajeProcesos({
                            //clase: 'red',
                            posicion: 'A',
                            mensaje: 'El archivo se encuentra adjunto',
                            titulo: 'Mensaje del Sistema',
                            tipo_mensaje: 'informacion'

                        });
      
                    }
                }
            }
            $("#" + nItem.cId)[0].files = $("#tmpFirstFile")[0].files;

            document.getElementById(nItem.cId).onchange();
            //if ($.fn.Browser() == "FIREFOX" || $.fn.Browser() == "IE") {                
            //} else {
            //alert("No funciona");
            //
            Indexar(idTabla);
        }

        $(m.btn).unbind("click").click(function (e) {
            e.preventDefault();
            document.getElementById("tmpFirstFile").click();
        });

        _oItem.getData = function () {
            var nIdForm = '#' + idForm;
            //var nForm = new FormData($(nIdForm));
            //var nForm2 = new FormData($(nIdForm)[0]);
            var form = document.getElementById(idForm);
            var nForm = new FormData(form);
            return nForm;
        }

        _oItem.Validate = function () {
            return $("#" + idTabla + " tbody tr .tdTmpFile").length > 0;
        }

        return _oItem;
    }
})(jQuery);