using authentication.API.Context;
using authentication.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace authentication.API.Repository
{
    public class UserRepository : IDisposable
    {
        private BlogContext blg;

        public int insert(User user)
        {
            try
            {
                using (blg = new BlogContext())
                {
                    blg.Users.Add(user);
                    blg.SaveChanges();
                    return user.Id;
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
                    var user = blg.Users.Find(id);
                    if (user != null)
                    {
                        blg.Users.Remove(user);
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

        public bool update(User user)
        {
            try
            {
                using (blg = new BlogContext())
                {
                    var currUser = blg.Users.Find(user.Id);
                    if (currUser != null)
                    {
                        blg.Entry(currUser).CurrentValues.SetValues(user);
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

        public bool checkExist(string userName)
        {
            try
            {
                using (blg = new BlogContext())
                {
                    var user = blg.Users.FirstOrDefault(x => x.UserName == userName);
                    if (user != null)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public UserExt getAll(int page, int limit)
        {
            List<User> list = new List<User>();
            using (blg = new BlogContext())
            {
                list = blg.Users.OrderBy(x=>x.UserName).Skip(page).Take(limit).ToList();
                return new UserExt { users = list, totalRecords = blg.Users.Count()};
            }
            
        }
        public void Dispose()
        {
            blg.Dispose();

        }
    }
}