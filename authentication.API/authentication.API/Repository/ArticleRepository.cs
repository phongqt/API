using authentication.API.Context;
using authentication.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace authentication.API.Repository
{
    public class ArticleRepository : IDisposable
    {
        private BlogContext blg;

        public int insert(Article article)
        {
            try
            {
                using (blg = new BlogContext())
                {
                    blg.Articles.Add(article);
                    blg.SaveChanges();
                    return article.Id;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public bool delete(int id)
        {
            try
            {
                using (blg = new BlogContext())
                {
                    var article = blg.Articles.Find(id);
                    if (article != null)
                    {
                        blg.Articles.Remove(article);
                        blg.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool update(Article article)
        {
            try
            {
                using (blg = new BlogContext())
                {
                    var currArticle = blg.Articles.Find(article.Id);
                    if (currArticle != null)
                    {
                        blg.Entry(currArticle).CurrentValues.SetValues(article);
                        blg.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public ArticleExt getAll(int page, int limit)
        {
            List<Article> list = new List<Article>();
            using (blg = new BlogContext())
            {
                list = blg.Articles.OrderBy(x => x.Title).Skip(page).Take(limit).ToList();
                return new ArticleExt { articles = list, totalRecords = blg.Articles.Count() };
            }

        }

        public void Dispose()
        {
            blg.Dispose();
        }
    }
}