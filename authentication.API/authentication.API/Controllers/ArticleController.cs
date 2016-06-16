using authentication.API.Models;
using authentication.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace authentication.API.Controllers
{
    [RoutePrefix("api/article")]
    public class ArticleController : ApiController
    {
        private ArticleRepository _repo = null;
        public ArticleController()
        {
            _repo = new ArticleRepository();
        }
        [AllowAnonymous]
        public ResponseModel Get(int page = 1, int limit = 10)
        {
            page--;
            return new ResponseModel { data = _repo.getAll((page * limit), limit), code = HttpStatusCode.OK };
        }
    }
    

}
