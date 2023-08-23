using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private DataContext _context;
        public BuggyController(DataContext context)
        {
            this._context= context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()  //401 error
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()  //replicating 404 error
        {
            var thing = _context.Users.Find(-1);
            if (thing == null) return NotFound();

            return thing;
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()  //replicating 500 error internal server error
        {
                var thing = _context.Users.Find(-1);

                var thingToReturn = thing.ToString();

                return thingToReturn;
                        
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest() //replicating 400 bad request error
        {
            return BadRequest("This was not a good request");
        }

      
    }
}
