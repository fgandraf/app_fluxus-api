
namespace FluxusApi.Repositories

{
    public static class ConnectionString
    {
        public const string CONNECTION_STRING = "SERVER=localhost; PORT=3306; DATABASE=fluxus; UID=root; PWD=1q2w3e4r@#$; charset=utf8;";

        public static string Get()
        {
            return "SERVER=localhost; PORT=3306; DATABASE=fluxus; UID=root; PWD=1q2w3e4r@#$; charset=utf8;";
        }
        
    }
}
