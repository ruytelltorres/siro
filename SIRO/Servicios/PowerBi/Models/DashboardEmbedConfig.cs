using Microsoft.PowerBI.Api.Models;
using System;

namespace SIRO.Servicios.PowerBi.Models
{
    public class DashboardEmbedConfig
    {
        public Guid DashboardId { get; set; }

        public string EmbedUrl { get; set; }

        public EmbedToken EmbedToken { get; set; }
    }
}