using authentication.API.Models;
using authentication.API.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace authentication.API.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private UserRepository _repo = null;

        public AccountController()
        {
            _repo = new UserRepository();
        }
        [Authorize]
        [Route("Users")]
        public ResponseModel Get(int page = 1, int limit = 10)
        {
            page--;
            var list = _repo.getAll(page * limit, limit);
            return new ResponseModel { code = HttpStatusCode.OK, data = list };
        }
        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public ResponseModel Register(User userModel)
        {
            if (!string.IsNullOrEmpty(userModel.UserName))
            {
                return new ResponseModel { code = HttpStatusCode.BadRequest, message = "User name is required" };
            }
            if (!string.IsNullOrEmpty(userModel.FirstName))
            {
                return new ResponseModel{code = HttpStatusCode.BadRequest, message = "First name is required" };
            }
            if (!string.IsNullOrEmpty(userModel.LastName))
            {
                return new ResponseModel { code = HttpStatusCode.BadRequest, message = "Last name is required" };
            }
            if (!string.IsNullOrEmpty(userModel.Password))
            {
                return new ResponseModel { code = HttpStatusCode.BadRequest, message = "Password is required" };
            }
            if (_repo.checkExist(userModel.UserName))
            {
                return new ResponseModel { code = HttpStatusCode.Conflict, message = "This email already exists" };
            }
            userModel.Role = 1;
            if (_repo.insert(userModel) != -1)
            {
                return new ResponseModel { code = HttpStatusCode.Created, message = "OK" };
            }
            return new ResponseModel { code = HttpStatusCode.ExpectationFailed, message = "Error" };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
