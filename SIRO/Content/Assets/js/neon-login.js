var neonLogin = neonLogin || {};

; (function ($, window, undefined) {
    "use strict";

    $(document).ready(function () {
        neonLogin.$container = $("#form_login");
        // Login Form & Validation
        neonLogin.$container.validate({
            rules: {
                inUsuario: {
                    required: true
                },
                inClave: {
                    required: true
                },
            },
            messages: {
                inUsuario: {
                    required: "Ingrese su usuario"
                },
                inClave: {
                    required: "Ingrese su contrase&ntilde;a"
                },
            },
            highlight: function (element) {
                $(element).closest('.input-group').addClass('validate-has-error');
            },
            unhighlight: function (element) {
                $(element).closest('.input-group').removeClass('validate-has-error');
            },
            submitHandler: function (ev) {
                $(".login-page").addClass('logging-in');
                $(".form-login-error").slideUp('fast');
                setTimeout(function () {
                    var random_pct = 25 + Math.round(Math.random() * 30);
                    neonLogin.setPercentage(20 + random_pct);

                    $.fn.Conexion({
                        direccion: '/Login/ValidarUsuario',
                        datos: { Usuario: $('#inUsuario').val(), Clave: $('#inClave').val() },
                        terminado: function (data, textStatus, jqXHR) {
                            var login_status = data.Estado;
                            setTimeout(function () {
                                if (login_status === 0) {
                                    $(".login-page").removeClass('logging-in');
                                    $(".form-login-error h3").html(data.Mensaje);
                                    neonLogin.resetProgressBar(true);
                                } else if (login_status === 1) {
                                    $(".login-progressbar-indicator span").html("Cargando opciones del usuario");
                                    neonLogin.setPercentage(80);
                                    setTimeout(function () {
                                        neonLogin.setPercentage(100);
                                        window.location = data.URL;
                                        window.location.href = "#";
                                    }, 1000 /*400*/);
                                }
                            }, 1000);
                        }
                    });
                }, 650);
            }
        });
        
        // Lockscreen & Validation
        var is_lockscreen = $(".login-page").hasClass('is-lockscreen');

        if (is_lockscreen) {
            neonLogin.$container = $("#form_lockscreen");
            neonLogin.$ls_thumb = neonLogin.$container.find('.lockscreen-thumb');

            neonLogin.$container.validate({
                rules: {

                    inClave: {
                        required: true
                    },

                },

                highlight: function (element) {
                    $(element).closest('.input-group').addClass('validate-has-error');
                },


                unhighlight: function (element) {
                    $(element).closest('.input-group').removeClass('validate-has-error');
                },

                submitHandler: function (ev) {
					/* 
						Demo Purpose Only 
						
						Here you can handle the page login, currently it does not process anything, just fills the loader.
					*/

                    $(".login-page").addClass('logging-in-lockscreen'); // This will hide the login form and init the progress bar

                    // We will wait till the transition ends				
                    setTimeout(function () {
                        var random_pct = 25 + Math.round(Math.random() * 30);

                        neonLogin.setPercentage(random_pct, function () {
                            // Just an example, this is phase 1
                            // Do some stuff...

                            // After 0.77s second we will execute the next phase
                            setTimeout(function () {
                                neonLogin.setPercentage(100, function () {
                                    // Just an example, this is phase 2
                                    // Do some other stuff...

                                    // Redirect to the page
                                    setTimeout("window.location.href = '../../'", 600);
                                }, 2);

                            }, 820);
                        });

                    }, 650);
                }
            });
        }
        
        // Login Form Setup
        neonLogin.$body = $(".login-page");
        neonLogin.$login_progressbar_indicator = $(".login-progressbar-indicator h3");
        neonLogin.$login_progressbar = neonLogin.$body.find(".login-progressbar div");

        neonLogin.$login_progressbar_indicator.html('0%');

        if (neonLogin.$body.hasClass('login-form-fall')) {
            var focus_set = false;

            setTimeout(function () {
                neonLogin.$body.addClass('login-form-fall-init')

                setTimeout(function () {
                    if (!focus_set) {
                        neonLogin.$container.find('input:first').focus();
                        focus_set = true;
                    }

                }, 550);

            }, 0);
        }
        else {
            neonLogin.$container.find('input:first').focus();
        }

        // Focus Class
        neonLogin.$container.find('.form-control').each(function (i, el) {
            var $this = $(el),
                $group = $this.closest('.input-group');

            $this.prev('.input-group-addon').click(function () {
                $this.focus();
            });

            $this.on({
                focus: function () {
                    $group.addClass('focused');
                },

                blur: function () {
                    $group.removeClass('focused');
                }
            });
        });

        // Functions
        $.extend(neonLogin, {
            setPercentage: function (pct, callback) {
                pct = parseInt(pct / 100 * 100, 10) + '%';
                
                // Normal Login
                neonLogin.$login_progressbar_indicator.html(pct);
                neonLogin.$login_progressbar.width(pct);

                var o = {
                    pct: parseInt(neonLogin.$login_progressbar.width() / neonLogin.$login_progressbar.parent().width() * 100, 10)
                };

                TweenMax.to(o, .7, {
                    pct: parseInt(pct, 10),
                    roundProps: ["pct"],
                    ease: Sine.easeOut,
                    onUpdate: function () {
                        neonLogin.$login_progressbar_indicator.html(o.pct + '%');
                    },
                    onComplete: callback
                });
            },

            resetProgressBar: function (display_errors) {
                TweenMax.set(neonLogin.$container, { css: { opacity: 0 } });

                setTimeout(function () {
                    TweenMax.to(neonLogin.$container, .6, {
                        css: { opacity: 1 }, onComplete: function () {
                            neonLogin.$container.attr('style', '');
                        }
                    });

                    neonLogin.$login_progressbar_indicator.html('0%');
                    neonLogin.$login_progressbar.width(0);

                    if (display_errors) {
                        var $errors_container = $(".form-login-error");

                        $errors_container.show();
                        var height = $errors_container.outerHeight();

                        $errors_container.css({
                            height: 0
                        });

                        TweenMax.to($errors_container, .45, {
                            css: { height: height }, onComplete: function () {
                                $errors_container.css({ height: 'auto' });
                            }
                        });

                        // Reset password fields
                        neonLogin.$container.find('input[type="password"]').val('');
                    }

                }, 800);
            }
        });
    });

})(jQuery, window);