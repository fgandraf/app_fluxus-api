using Microsoft.AspNetCore.Http;
using System;

namespace Fluxus.Api
{
    public class Autentication
    {
        public const string TOKEN = "xz8wM6zr2RfF18GBM0B5yrkoo";
        IHttpContextAccessor _context;

        public Autentication(IHttpContextAccessor context)
        {
            _context = context;
         }


        public void Autenticar()
        {
            try
            {
                string TokenRecebido = _context.HttpContext.Request.Headers["Token"].ToString();
                if (String.Equals(TOKEN, TokenRecebido) == false)
                    throw new Exception("Token inválido");
            }
            catch
            {
                throw new Exception("Não foi possível estabelecer a conexão!");
            }

        }


    }
}
