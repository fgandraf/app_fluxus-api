namespace FluxusApi
{
    public class Authenticator
    {
        private const string TOKEN = "xz8wM6zr2RfF18GBM0B5yrkoo";
        private readonly IHttpContextAccessor _context;
        
        public Authenticator(IHttpContextAccessor context)
            => _context = context;
        
        public bool Authenticate()
        {
            try
            {
                if (_context.HttpContext == null)
                    throw new Exception("Null context");
                
                var tokenContext = _context.HttpContext.Request.Headers["Token"].ToString();
                if (String.Equals(TOKEN, tokenContext) == false)
                    throw new Exception("Invalid token");

                return true;
            }
            catch
            {
                throw new Exception("Impossible to establish the connection!");
            }
        }
        
        
    }
}
