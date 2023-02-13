using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;
using System.Collections;
using MySql.Data.MySqlClient;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class ProfileController : ControllerBase
    {
        Autentication Authenticator;

        public ProfileController(IHttpContextAccessor context)
            => Authenticator = new Autentication(context);


        [HttpGet] // GET:api/Profile
        public IActionResult GetAll()
        {
            Profile result;

            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ProfileRepository(connection).Get(1);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
        }


        [HttpGet("Logo")] // GET:api/Profile/Logo
        public IActionResult GetLogo()
        {
            byte[] result;

            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ProfileRepository(connection).GetLogo();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
        }


        [HttpGet("ToPrint")] // GET:api/Profile/ToPrint
        public IActionResult GetToPrint()
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ProfileRepository(connection).GetToPrint();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
        }


        [HttpGet("TradingName")] // GET:api/Profile/TradingName
        public IActionResult GetTradingName()
        {
            string result;

            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ProfileRepository(connection).GetTradingName();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
        }


        [HttpPost] // POST:api/Profile
        public IActionResult Post([FromBody] Profile profile)
        {
            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ProfileRepository(connection).Insert(profile);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return Ok();
        }


        [HttpPut] // PUT:api/Profile
        public IActionResult Put([FromBody] Profile profile)
        {
            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ProfileRepository(connection).Update(profile);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return Ok();
        }
    }
}
