using SIRO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SIRO.Utils.Error
{
    public class GetError
    {
        public static ErrorModel GetErrorModel(string errorTitulo, HttpStatusCode errorCod, string errorMensaje)
        {
            string errorIcono = "entypo-attention";
            switch (errorCod)
            {
                case HttpStatusCode.NotFound:
                    errorIcono = "entypo-attention";
                    break;
                case HttpStatusCode.Unauthorized:
                    errorIcono = "entypo-user";
                    break;
                case HttpStatusCode.InternalServerError:
                    errorIcono = "entypo-attention";
                    break;
                default:
                    errorIcono = "entypo-attention";
                    break;
            }
            ErrorModel errorModel = new ErrorModel
            {
                Titulo = errorTitulo,
                CodigoError = errorCod,
                MensajeError = errorMensaje == String.Empty ? "Lo sentimos, tuvimos problemas al procesar su solicitud, Por favor cuminicarse con el Departamento de Tecnología" : errorMensaje,
                IconoError = errorIcono,
            };
            return errorModel;
        }
    }
}