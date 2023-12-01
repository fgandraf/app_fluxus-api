namespace FluxusApi
{
    public class Authentication
    {
        private const string TOKEN = "xz8wM6zr2RfF18GBM0B5yrkoo";
        private IHttpContextAccessor _context;
        public string ConnectionString { get; private set; }


        public Authentication(IHttpContextAccessor context)
        {
            _context = context;
        }


        public void Authenticate()
        {
            try
            {
                string TokenRecebido = _context.HttpContext.Request.Headers["Token"].ToString();

                if (String.Equals(TOKEN, TokenRecebido) == false)
                    throw new Exception("Invalid token");

                IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
                ConnectionString = configuration.GetConnectionString("Default");
            }
            catch
            {
                throw new Exception("Impossible to establish the connection!");
            }

        }


    }
}
