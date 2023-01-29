using Microsoft.AspNetCore.Http;
using System;

namespace FluxusApi
{
    public class Autentication
    {
        public const string TOKEN = "xz8wM6zr2RfF18GBM0B5yrkoo";
        IHttpContextAccessor _context;

        public Autentication(IHttpContextAccessor context)
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
            }
            catch
            {
                throw new Exception("Impossible to establish the connection!");
            }

        }


    }
}
