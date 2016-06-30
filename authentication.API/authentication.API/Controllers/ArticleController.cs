using authentication.API.Models;
using authentication.API.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        [AllowAnonymous]
        public ResponseModel GetByAlias(string alias)
        {
            return new ResponseModel { data = _repo.getArticleByAlias(alias), code = HttpStatusCode.OK };
        }

        [Authorize]
        [HttpPost]
        public ResponseModel Create(Article articleModel)
        {
            try
            {
                if (string.IsNullOrEmpty(articleModel.Title))
                {
                    return new ResponseModel { code = HttpStatusCode.BadRequest, message = "Title is required" };
                }
                if (string.IsNullOrEmpty(articleModel._Content))
                {
                    return new ResponseModel { code = HttpStatusCode.BadRequest, message = "Content is required" };
                }
                articleModel.UserId = 1;
                articleModel.Created = DateTime.UtcNow;
                articleModel.Alias = RemoveUnicode(articleModel.Title).ToLower() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                if (_repo.insert(articleModel) != -1)
                {
                    return new ResponseModel { code = HttpStatusCode.Created, message = "OK" };
                }
                return new ResponseModel { code = HttpStatusCode.ExpectationFailed, message = "Error" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { code = HttpStatusCode.ExpectationFailed, message = "Error" };
            }
        }
        private string RemoveUnicode(string inputText)
        {
            string stFormD = inputText.Normalize(System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string str = "";
            for (int i = 0; i <= stFormD.Length - 1; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[i]);
                if (uc == UnicodeCategory.NonSpacingMark == false)
                {
                    if (stFormD[i] == 'đ')
                        str = "d";
                    else if (stFormD[i] == '\r' | stFormD[i] == '\n')
                        str = "";
                    else
                        str = stFormD[i].ToString();
                    sb.Append(str);
                }
            }

            sb = sb.Replace(" ", "-");
            sb = sb.Replace(":", "");
            sb = sb.Replace("(", "");
            sb = sb.Replace(")", "");
            sb = sb.Replace("!", "");
            sb = sb.Replace("?", "");
            sb = sb.Replace("--", "-");
            sb = sb.Replace("/", "-");
            sb = sb.Replace(",", "-");

            return sb.ToString().ToLower();
        }
    }
    

}
