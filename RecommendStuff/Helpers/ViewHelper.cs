using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecommendStuff.Models;

namespace RecommendStuff.Helpers
{
    public class ViewHelper
    {
        LinqDataContext db;
        public ViewHelper()
        {
            db = new LinqDataContext();
        }

        public bool AlreadyFollowing(string loggedInUser,string username)
        {
            int num = db.FollowConnections.Where(x => x.Username == loggedInUser).Count(x => x.FollowingName == username);
            if (num > 0)
                return true;
            else 
                return false;
        }
    }


}