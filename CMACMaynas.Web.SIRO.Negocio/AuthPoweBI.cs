using Newtonsoft.Json;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class AuthPoweBI
    {
        [JsonProperty(PropertyName = "UserName", Order = 0)]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "AccessToken", Order = 1)]
        public string AccessToken { get; set; }

        //    UserName = "Pruebas Power BI",
        //    AccessToken = accessToken
    }
}
