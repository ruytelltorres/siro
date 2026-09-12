(function ($) {
    $.fn.MensajeProcesos = function (m) {
        m = m || {};
        m.clase = m.clase || "green";
        m.duracion = m.duracion || 100;
        m.ocultar = m.ocultar || 500;
        m.tiempo_limite = m.tiempo_limite || 3000;
        m.tiempo_extendido = m.tiempo_extendido || 1000;
        m.titulo = m.titulo || "Titulo";
        m.mensaje = m.mensaje || "Mensaje del Sistema";
        m.tipo_mensaje = m.tipo_mensaje || 'exito';
        m.posicion = m.posicion || 'B';
        m.onhidden = m.onhidden || function () { };
        m.onclick = m.onclick || function () { };

        if (m.posicion === 'A') { m.posicion = 'toast-top-right'; }
        else if (m.posicion === 'B') { m.posicion = 'toast-bottom-right'; }
        else if (m.posicion === 'FA') { m.posicion = 'toast-top-full-width'; }
        else if (m.posicion === 'FB') { m.posicion = 'toast-bottom-full-width'; }
        //setTimeout(function () {
        var opts = {
            "closeButton": false,
            "debug": false,
            "positionClass": m.posicion, //"toast-bottom-right",//rtl() || public_vars.$pageContainer.hasClass('right-sidebar') ? "toast-top-left" : "toast-top-right",
            "toastClass": m.clase,//"black",
            "onclick": m.onclick,
            "onHidden": m.onhidden,
            "showDuration": m.duracion, //"300",
            "hideDuration": m.ocultar, //"1000",
            "timeOut": m.tiempo_limite,  //"5000",
            "extendedTimeOut": m.tiempo_extendido, //"1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut",
            "preventDuplicates": true, //Add TORE evitar la pila de mensajes iguales.
            //"iconClasses": {
            //    error: 'fa fa-close',
            //    info: 'fa fa-info',
            //    success: 'fa fa-close',
            //    warning: 'fa fa-close'
            //},
        };

        if (m.tipo_mensaje == 'informacion') {
            toastr.info(m.mensaje, m.titulo, opts);
        } else if (m.tipo_mensaje == 'exito') {
            toastr.success(m.mensaje, m.titulo, opts);
        } else if (m.tipo_mensaje == 'error') {
            toastr.error(m.mensaje, m.titulo, opts);
        } else if (m.tipo_mensaje == 'advertencia') {
            toastr.warning(m.mensaje, m.titulo, opts);
        }
        //}, 100);
    }
})(jQuery);
