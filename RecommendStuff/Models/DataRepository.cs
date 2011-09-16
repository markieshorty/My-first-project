using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecommendStuff.Models;
using RecommendStuff.Models.ViewModels;

namespace RecommendStuff.Models
{
    public class DataRepository
    {
        LinqDataContext db;

        public DataRepository()
        {
            db = new LinqDataContext();
        }

        public IList<County>  getCounties()
        {
            return db.Counties.OrderBy(x => x.County1).ToList();
        }

        public void DeleteItem(int ItemId)
        {
            IList<Link> links = db.Links.Where(x => x.ItemId == ItemId).ToList();
            db.Links.DeleteAllOnSubmit(links);

            IList<ItemComment> comments = db.ItemComments.Where(x => x.ItemId == ItemId).ToList();
            db.ItemComments.DeleteAllOnSubmit(comments);

            Item item = db.Items.FirstOrDefault(x => x.ItemId == ItemId);

            db.Items.DeleteOnSubmit(item);
            db.SubmitChanges();
        }

        public void deleteConnectionById(int Id)
        {
            FollowConnection fC = db.FollowConnections.FirstOrDefault(x => x.Id == Id);
            db.FollowConnections.DeleteOnSubmit(fC);
            db.SubmitChanges();
        }

        public string getUsernameFromItemId(int id)
        {
            Item i = db.Items.FirstOrDefault(x => x.ItemId == id);
            return i.Username.ToString();
        }

        public IList<FollowConnection> getFollowConnections(string username)
        {
            return db.FollowConnections.Where(x => x.Username == username).ToList();
        }

        public string getYOB(string Id)
        {
           User user = db.Users.FirstOrDefault(x => x.Username == Id);
           return user.YearOfBirth.ToString();
        }

        public string getStereotype(string Id)
        {
            User user = db.Users.FirstOrDefault(x => x.Username == Id);
            return user.Stereotype.ToString();
        }

        public bool isMale(string Id)
        {
            User user = db.Users.FirstOrDefault(x => x.Username == Id);
            return (bool)user.Male;
        }

        public string getMostRecentItem(string Id)
        {
            try
            {
                return db.Items.Where(x => x.Username == Id).OrderByDescending(x => x.Date).FirstOrDefault().Name;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public Item getItemById(string Id)
        {
            return db.Items.FirstOrDefault(x => x.ItemId == Convert.ToInt32(Id));
        }

        public bool linkExists(string link, int itemId)
        {
            int num = db.Links.Count(x => x.Url == link && x.ItemId == itemId);
            if (num > 0)
                return true;
            else
                return false;
        }

        public string getMostRecentItemId(string Id)
        {
            try
            {
                return db.Items.Where(x => x.Username == Id).OrderByDescending(x => x.Date).FirstOrDefault().ItemId.ToString();
            }
            catch (Exception)
            {
                return "Nothing!";
            }
        }
        
        //Home Screen

        public List<Item> getSpecificItems(int catId,int takeValue)
        {
            var items = from i in db.Items

                        where i.CategoryId == catId

                        group i by i.Name into g

                        select g.OrderByDescending(x=>x.ThumbsUp).First();

            return items.OrderByDescending(x => x.ThumbsUp).ThenByDescending(x => x.Date).Take(takeValue).ToList();
        }

        public IList<Item> getSpecificItemsSearch(int catId,string search,int takeValue)
        {
            var items = from i in db.Items

                        where i.CategoryId == catId && i.Name.Contains(search)

                        select i;

            return items.OrderByDescending(x => x.ThumbsUp).ThenByDescending(x => x.Date).Take(takeValue).ToList();
        }

        public List<Item> getSpecificItemsInNetwork(string username,int catId,int takeValue)
        {
            IList<FollowConnection> connections = db.FollowConnections.Where(x => x.Username == username).ToList();
            List<Item> itemsFiltered = new List<Item>();

            for (int j = 0; j < connections.Count(); j++)
            {
               IList<Item> tempList = db.Items.Where(x => x.Username == connections[j].FollowingName && x.CategoryId == catId).ToList();
                itemsFiltered.AddRange(tempList);
            }
            var items = from i in itemsFiltered
                           group i by i.Name into g
                           select g.OrderByDescending(x => x.ThumbsUp).First();

            return items.OrderByDescending(x => x.ThumbsUp).ThenByDescending(x => x.Date).Take(takeValue).ToList();
        }

        public List<Item> getSpecificItemsInNetworkSearch(string username,int catId,string search,int takeValue)
        {
            IList<FollowConnection> connections = db.FollowConnections.Where(x => x.Username == username).ToList();
            List<Item> itemsFiltered = new List<Item>();

            for (int j = 0; j < connections.Count(); j++)
            {
                IList<Item> tempList = db.Items.Where(x => x.Username == connections[j].FollowingName && x.CategoryId == catId && x.Name.Contains(search)).ToList();
                itemsFiltered.AddRange(tempList);
            }
            
            return itemsFiltered.OrderByDescending(x => x.ThumbsUp).ThenByDescending(x => x.Date).Take(takeValue).ToList();
        }
        
        public IList<Item> getMostRecentItems(int takeValue)
        {
            var items = from i in db.Items

                        group i by i.Name into g

                        select g.OrderByDescending(x => x.ThumbsUp).First();

            return items.OrderByDescending(x => x.Date).ThenByDescending(x => x.ThumbsUp).Take(takeValue).ToList();
        }

          public IList<Item> getMostRecentItemsSearch(int takeValue,string search)
        {
            return db.Items.Where(x=>x.Name.Contains(search)).OrderByDescending(x => x.Date).Take(takeValue).ToList();
        }

    
        public IList<Item> getMostRecentSpecificItems(int catId,int takeValue)
        {
            var items = from i in db.Items

                        where i.CategoryId == catId

                        group i by i.Name into g

                        select g.OrderByDescending(x => x.ThumbsUp).First();
            
            return items.OrderByDescending(x => x.Date).Take(takeValue).ToList();
        }

        public List<Item> getMostRecentSpecificItemsInNetwork(string username,int catId,int takeValue)
        {
            IList<FollowConnection> connections = db.FollowConnections.Where(x => x.Username == username).ToList();
            List<Item> itemsFiltered = new List<Item>();

            for (int j = 0; j < connections.Count(); j++)
            {
                IList<Item> tempList = db.Items.Where(x => x.Username == connections[j].FollowingName && x.CategoryId == catId).ToList();
                itemsFiltered.AddRange(tempList);
            }
            var items = from i in itemsFiltered
                        group i by i.Name into g
                        select g.OrderByDescending(x => x.ThumbsUp).First();

            return items.OrderByDescending(x => x.Date).Take(takeValue).ToList();
        }
      
        public List<Item> getMostRecentItemsInNetwork(string username,int takeValue)
        {
            IList<FollowConnection> connections = db.FollowConnections.Where(x => x.Username == username).ToList();
            List<Item> itemsFiltered = new List<Item>();

            for (int j = 0; j < connections.Count(); j++)
            {
                IList<Item> tempList = db.Items.Where(x => x.Username == connections[j].FollowingName).ToList();
                itemsFiltered.AddRange(tempList);
            }
            var items = from i in itemsFiltered
                        group i by i.Name into g
                        select g.OrderByDescending(x => x.ThumbsUp).First();

            return items.OrderByDescending(x => x.Date).Take(takeValue).ToList();
        }

         public List<Item> getMostRecentItemsInNetworkSearch(string username,string search,int takeValue)
        {
            IList<FollowConnection> connections = db.FollowConnections.Where(x => x.Username == username).ToList();
            List<Item> itemsFiltered = new List<Item>();

            for (int j = 0; j < connections.Count(); j++)
            {
                IList<Item> tempList = db.Items.Where(x => x.Username == connections[j].FollowingName && x.Name.Contains(search)).ToList();
                itemsFiltered.AddRange(tempList);
            }
           
            return itemsFiltered.OrderByDescending(x => x.Date).Take(takeValue).ToList();
        }

        public void FollowSelf(string username)
        {
            FollowConnection conn = new FollowConnection();
            conn.FollowingName = username;
            conn.Username = username;
            db.FollowConnections.InsertOnSubmit(conn);
            db.SubmitChanges();
        }

        public IList<Item> getItemsByUser(string username)
        {
            IList<Item> items = db.Items.Where(x => x.Username == username).OrderByDescending(x=>x.Date).ToList();
            return items;
        }

        public bool AlreadyFollowingUser(string loggedInUser, string possUser)
        {
            if (db.FollowConnections.Where(x => x.Username == loggedInUser).Count(x => x.FollowingName == possUser) > 0)
                return true;
            else 
                return false;

        }

        public void AddFollowConnection(string loggedInUser,string username)
        {
            FollowConnection conn = new FollowConnection();

            conn.FollowingName = username;
            conn.Username = loggedInUser;
            db.FollowConnections.InsertOnSubmit(conn);
            db.SubmitChanges();
        }

        public void deleteConnection(FollowConnection deleteMe)
        {
            FollowConnection delete = db.FollowConnections.FirstOrDefault(x=>x.Id == deleteMe.Id);

            db.FollowConnections.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }

        public FollowConnection getFollowConnectionFromUser(string loggedInUser,string username)
        {
            return db.FollowConnections.FirstOrDefault(x => x.Username == loggedInUser && x.FollowingName == username);
        }

        public string getFollowingCount(string username)
        {
            int count = (db.FollowConnections.Count(x => x.Username == username) - 1);

            return count.ToString();
        }

        public string getLocation(string Id)
        {
            User user = db.Users.FirstOrDefault(x => x.Username == Id);
            return user.Location;

        }

        public string getFollowedByCount(string username)
        {
            int count = (db.FollowConnections.Count(x => x.FollowingName == username) - 1);
            return count.ToString();
        }

        //Login Screen
        public bool UserCanLogIn(LoginViewModel viewModel)
        {
            int count = db.Users.Count(x => x.Username == viewModel.username && x.Password == viewModel.password);
            if (count == 1)
                return true;
            else
                return false;
        
        }

        public User GetUser(LoginViewModel viewModel)
        {
            return db.Users.First(x => x.Username == viewModel.username && x.Password == viewModel.password);
        }

        public User AddUser(RegisterViewModel viewModel)
        {
            User newUser = new User();

            newUser.DateAdded = DateTime.Now;
            newUser.YearOfBirth = viewModel.dobYear;
            newUser.Location = viewModel.county;
            newUser.LastLoginDate = DateTime.Now;
            newUser.UserRating = 0;
            newUser.Username = viewModel.username;
            newUser.Password = viewModel.password;
            newUser.Alias = viewModel.username;
            newUser.Email = viewModel.email;
            newUser.Stereotype = "NULL!";
  
            if (viewModel.gender == "1")
                newUser.Male = true;
            else if (viewModel.gender == "0")
                newUser.Male = false;

            db.Users.InsertOnSubmit(newUser);
            db.SubmitChanges();
            return newUser;
        }

        public bool usernameExists(string requestedUsername)
        {
            if (db.Users.Count(x => x.Username == requestedUsername) > 0)
            {
                return true;
            }
            else
                return false;
        }

        public void LikeItem(string ItemId)
        {
            Item item2 = db.Items.FirstOrDefault(x => x.ItemId == Convert.ToInt32(ItemId));
            item2.ThumbsUp++;

            item2.User.UserRating++;

            db.SubmitChanges();
        }

        public void LikeLink(int Id)
        {
            Link link = db.Links.FirstOrDefault(x => x.LinkId == Id);

            link.ThumbsUp++;

            db.SubmitChanges();
        }

        public void ReportAsDead(int Id)
        {
            Link link = db.Links.FirstOrDefault(x => x.LinkId == Id);

            link.ThumbsDown++;

            db.SubmitChanges();
        }

        public void DereportAsDead(int Id)
        {
            Link link = db.Links.FirstOrDefault(x => x.LinkId == Id);

            link.ThumbsDown--;

            db.SubmitChanges();
        }

        public void LikeLinkInstead(string url, int itemId)
        {
            Link link = db.Links.FirstOrDefault(x => x.Url == url && x.ItemId == itemId);
            link.ThumbsUp++;
            db.SubmitChanges();
        }

        public void DelikeLink(int Id)
        {
            Link link = db.Links.FirstOrDefault(x => x.LinkId == Id);

            link.ThumbsUp--;

            db.SubmitChanges();
        }

        public void DontLikeItem(string ItemId)
        {
            Item item2 = db.Items.FirstOrDefault(x => x.ItemId == Convert.ToInt32(ItemId));
            item2.ThumbsUp--;

            item2.User.UserRating--;

            db.SubmitChanges();
        }

        public int AddItem(string name, string username, int categoryId)
        {
            Item item = new Item();

            item.CategoryId = categoryId;
            item.Date = DateTime.Now;
            item.Name = name;
            item.Username = username;
            item.ThumbsUp = 1;

            db.Items.InsertOnSubmit(item);
            db.SubmitChanges();

            return item.ItemId;
        }

        public void AddLink(string url, int itemId,string username)
        {
            Link link = new Link();

            link.DeadFlags = 0;
            link.ItemId = itemId;
            link.ThumbsUp = 1;
            link.Url = url;
            link.Date = DateTime.Now;
            link.Username = username;

            db.Links.InsertOnSubmit(link);
            db.SubmitChanges();
        }

        public void AddComment(string username, int itemId, string comment)
        {
            ItemComment itemComment = new ItemComment();

            itemComment.Username = username;
            itemComment.ItemId = itemId;
            itemComment.Comment = comment;
            itemComment.Date = DateTime.Now;
            itemComment.ThumbsUp = 1;

            db.ItemComments.InsertOnSubmit(itemComment);
            db.SubmitChanges();
        }

        public Item getItem(int itemId)
        {
            return db.Items.FirstOrDefault(x => x.ItemId == itemId);
        }

        public IList<ItemComment> getComments(int itemId)
        {
            return db.ItemComments.Where(x => x.ItemId == itemId).OrderByDescending(x=>x.Date).Take(20).ToList();
        }

        public IList<Link> getLinks(int itemId)
        {
            return db.Links.Where(x => x.ItemId == itemId && x.ThumbsDown < 5).OrderByDescending(x=>x.ThumbsUp).ToList();
        }

        public void DeleteComment(string Id)
        {
            int id = Convert.ToInt32(Id);

            ItemComment comment = db.ItemComments.FirstOrDefault(x => x.Id == id);
            db.ItemComments.DeleteOnSubmit(comment);
            db.SubmitChanges();
        }

        public void DeleteLink(int Id)
        {
            Link link = db.Links.FirstOrDefault(x => x.LinkId == Id);
            db.Links.DeleteOnSubmit(link);
            db.SubmitChanges();
        }

        public void LikeComment(string Id)
        {
            int id = Convert.ToInt32(Id);

            ItemComment comment = db.ItemComments.FirstOrDefault(x => x.Id == id);
            comment.ThumbsUp++;
            db.SubmitChanges();
        }

        public void DelikeComment(string Id)
        {
            int id = Convert.ToInt32(Id);

            ItemComment comment = db.ItemComments.FirstOrDefault(x => x.Id == id);
            comment.ThumbsUp--;
            db.SubmitChanges();
        }

        public string getTopLink(int itemId)
        {
            try
            {
            Link link = db.Links.Where(x => x.ItemId == itemId).OrderByDescending(x => x.ThumbsUp).FirstOrDefault();
            return link.Url;
            }
            catch (Exception)
            {

                return "";
            }
           
        }

        public string getTopYoutubeLink(int itemId)
        {
            try
            {
             Link link = db.Links.Where(x => x.ItemId == itemId && x.Url.Contains("www.youtube")).ToList().OrderByDescending(x=>x.ThumbsUp).FirstOrDefault();
            return link.Url;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string[] getItemTopLinks(IList<Item> items)
        {
            int count = items.Count();
            string[] topLinks = new string[count];

            for (int i = 0; i < topLinks.Length; i++)
            {
                topLinks[i] = getTopLink(items[i].ItemId);
            }
            return topLinks;
        }

        public string[] getItemYouTubes(IList<Item> items)
        {
            int count = items.Count();
            string[] youtubes = new string[count];

            for (int i = 0; i < youtubes.Length; i++)
            {
                youtubes[i] = getTopYoutubeLink(items[i].ItemId);
            }
            return youtubes;
        }





    }
}