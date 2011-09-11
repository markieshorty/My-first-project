using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecommendStuff.Models;
using RecommendStuff.ViewModels;
using RecommendStuff.Models.ViewModels;

namespace RecommendStuff.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index(string Id, string state)
        {
            int ten = 10;
            if (state != "Global" && state != null)
            {
                DataRepository helper = new DataRepository();
                
                IList<Item> songs = helper.getSpecificItemsInNetwork(Id,1,ten);
                IList<Item> films = helper.getSpecificItemsInNetwork(Id,6,ten);
                IList<Item> books = helper.getSpecificItemsInNetwork(Id,3,ten);
                IList<Item> games = helper.getSpecificItemsInNetwork(Id,4,ten);
                IList<Item> apps = helper.getSpecificItemsInNetwork(Id,5,ten);
                IList<Item> mostRecentItems = helper.getMostRecentItemsInNetwork(Id,ten);
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);

                ViewData["songsTopLinks"] = helper.getItemTopLinks(songs);
                ViewData["songsYouTubes"] = helper.getItemYouTubes(songs);

                ViewData["filmsTopLinks"] = helper.getItemTopLinks(films);
                ViewData["filmsYouTubes"] = helper.getItemYouTubes(films);

                ViewData["booksTopLinks"] = helper.getItemTopLinks(books);
                ViewData["booksYouTubes"] = helper.getItemYouTubes(books);

                ViewData["gamesTopLinks"] = helper.getItemTopLinks(games);
                ViewData["gamesYouTubes"] = helper.getItemYouTubes(games);

                ViewData["appsTopLinks"] = helper.getItemTopLinks(apps);
                ViewData["appsYouTubes"] = helper.getItemYouTubes(apps);

                ViewData["recentTopLinks"] = helper.getItemTopLinks(mostRecentItems);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(mostRecentItems);

                ViewData["Following"] = helper.getFollowingCount(Id);
                ViewData["FollowedBy"] = helper.getFollowedByCount(Id);

                return View(new ItemViewModel(songs, films, books, games, apps,mostRecentItems,Id,"User",yob,stereotype,male,recentItem,"",recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> songs = helper.getSpecificItems(1,ten);
                IList<Item> films = helper.getSpecificItems(6, ten);
                IList<Item> books = helper.getSpecificItems(3, ten);
                IList<Item> games = helper.getSpecificItems(4, ten);
                IList<Item> apps = helper.getSpecificItems(5, ten);
                IList<Item> mostRecentItems = helper.getMostRecentItems(ten);

                ViewData["songsTopLinks"] = helper.getItemTopLinks(songs);
                ViewData["songsYouTubes"] = helper.getItemYouTubes(songs);

                ViewData["filmsTopLinks"] = helper.getItemTopLinks(films);
                ViewData["filmsYouTubes"] = helper.getItemYouTubes(films);

                ViewData["booksTopLinks"] = helper.getItemTopLinks(books);
                ViewData["booksYouTubes"] = helper.getItemYouTubes(books);

                ViewData["gamesTopLinks"] = helper.getItemTopLinks(games);
                ViewData["gamesYouTubes"] = helper.getItemYouTubes(games);

                ViewData["appsTopLinks"] = helper.getItemTopLinks(apps);
                ViewData["appsYouTubes"] = helper.getItemYouTubes(apps);

                ViewData["recentTopLinks"] = helper.getItemTopLinks(mostRecentItems);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(mostRecentItems);

                return View(new ItemViewModel(songs, films, books, games, apps, mostRecentItems, null, "Global","","",false,"","","",""));
            }
        }

        [HttpPost]
        public ActionResult SearchRecents(string Id, string state)
        {
            string search = Request.Form["search"];

            if (state != "Global" && state != null)
            {
                DataRepository helper = new DataRepository();

                IList<Item> recents = helper.getMostRecentItemsInNetworkSearch(Id,search,10);
                if (recents.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search +"'";
                }
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);

                ViewData["recentTopLinks"] = helper.getItemTopLinks(recents);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(recents);

                return PartialView(new ItemViewModel(null, null, null, null, null, recents, Id, "User", yob, stereotype, male, recentItem, "", recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> recents = helper.getMostRecentItemsSearch(10,search);
                if (recents.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                ViewData["recentTopLinks"] = helper.getItemTopLinks(recents);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(recents);

                return PartialView(new ItemViewModel(null, null, null, null, null, recents, null, "Global", "", "", false, "", "", "",""));
            }
        }

       

        [HttpPost]
        public ActionResult SearchSongs(string Id, string state)
        {
            string search = Request.Form["search"];

            if (state != "Global" && state != null)
            {
                DataRepository helper = new DataRepository();

                IList<Item> songs = helper.getSpecificItemsInNetworkSearch(Id, 1,search,10);
                if (songs.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search +"'";
                }
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);

                ViewData["songsTopLinks"] = helper.getItemTopLinks(songs);
                ViewData["songsYouTubes"] = helper.getItemYouTubes(songs);

                return PartialView(new ItemViewModel(songs, null, null, null, null, null, Id, "User", yob, stereotype, male, recentItem, "", recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> songs = helper.getSpecificItemsSearch(1,search,10);
                if (songs.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                ViewData["songsTopLinks"] = helper.getItemTopLinks(songs);
                ViewData["songsYouTubes"] = helper.getItemYouTubes(songs);

                return PartialView(new ItemViewModel(songs, null, null, null, null, null, null, "Global", "", "", false, "", "", "",""));
            }
        }

        [HttpPost]
        public ActionResult SearchFilms(string Id, string state)
        {
            string search = Request.Form["search"];

            if (state != "Global" && state != null)
            {
                DataRepository helper = new DataRepository();

                IList<Item> films = helper.getSpecificItemsInNetworkSearch(Id, 6, search, 10);
                if (films.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);

                ViewData["filmsTopLinks"] = helper.getItemTopLinks(films);
                ViewData["filmsYouTubes"] = helper.getItemYouTubes(films);

                return PartialView(new ItemViewModel(null, films, null, null, null, null, Id, "User", yob, stereotype, male, recentItem, "", recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> films = helper.getSpecificItemsSearch(6, search, 10);
                if (films.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                ViewData["filmsTopLinks"] = helper.getItemTopLinks(films);
                ViewData["filmsYouTubes"] = helper.getItemYouTubes(films);

                return PartialView(new ItemViewModel(null, films, null, null, null, null, null, "Global", "", "", false, "", "", "",""));
            }
        }

        [HttpPost]
        public ActionResult SearchBooks(string Id, string state)
        {
            string search = Request.Form["search"];

            if (state != "Global" && state != null)
            {
                DataRepository helper = new DataRepository();

                IList<Item> books = helper.getSpecificItemsInNetworkSearch(Id, 3, search, 10);
                if (books.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);

                ViewData["filmsTopLinks"] = helper.getItemTopLinks(books);
                ViewData["filmsYouTubes"] = helper.getItemYouTubes(books);

                return PartialView(new ItemViewModel(null,null,books, null, null, null, Id, "User", yob, stereotype, male, recentItem, "", recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> books = helper.getSpecificItemsSearch(3, search, 10);
                if (books.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                ViewData["booksTopLinks"] = helper.getItemTopLinks(books);
                ViewData["booksYouTubes"] = helper.getItemYouTubes(books);

                return PartialView(new ItemViewModel(null, null, books, null, null, null, null, "Global", "", "", false, "", "", "",""));
            }
        }

        [HttpPost]
        public ActionResult SearchGames(string Id, string state)
        {
            string search = Request.Form["search"];

            if (state != "Global" && state != null)
            {
                DataRepository helper = new DataRepository();

                IList<Item> games = helper.getSpecificItemsInNetworkSearch(Id, 4, search, 10);
                if (games.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);

                ViewData["gamesTopLinks"] = helper.getItemTopLinks(games);
                ViewData["gamesYouTubes"] = helper.getItemYouTubes(games);

                return PartialView(new ItemViewModel(null, null, null, games, null, null, Id, "User", yob, stereotype, male, recentItem, "", recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> games = helper.getSpecificItemsSearch(4, search, 10);
                if (games.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                ViewData["gamesTopLinks"] = helper.getItemTopLinks(games);
                ViewData["gamesYouTubes"] = helper.getItemYouTubes(games);

                return PartialView(new ItemViewModel(null, null, null, games, null, null, null, "Global", "", "", false, "", "", "",""));
            }
        }

        [HttpPost]
        public ActionResult SearchApps(string Id, string state)
        {
            string search = Request.Form["search"];

            if (state != "Global" && state != null)
            {
                DataRepository helper = new DataRepository();

                IList<Item> apps = helper.getSpecificItemsInNetworkSearch(Id, 5, search, 10);
                if (apps.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);

                ViewData["appsTopLinks"] = helper.getItemTopLinks(apps);
                ViewData["appsYouTubes"] = helper.getItemYouTubes(apps);

                return PartialView(new ItemViewModel(null, null, null, null, apps, null, Id, "User", yob, stereotype, male, recentItem, "", recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> apps = helper.getSpecificItemsSearch(5, search, 10);
                if (apps.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                ViewData["appsTopLinks"] = helper.getItemTopLinks(apps);
                ViewData["appsYouTubes"] = helper.getItemYouTubes(apps);

                return PartialView(new ItemViewModel(null, null, null, null, apps, null, null, "Global", "", "", false, "", "", "",""));
            }
        }

        [HttpPost]
        public ActionResult SearchRecentsMain(string Id, string state)
        {
            string search = Request.Form["search"];

            if (state != "Global" && state != null)
            {
                DataRepository helper = new DataRepository();

                IList<Item> recents = helper.getMostRecentItemsInNetworkSearch(Id, search, 100);
                if (recents.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);

                ViewData["recentTopLinks"] = helper.getItemTopLinks(recents);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(recents);

                return PartialView(new ItemViewModel(null, null, null, null, null, recents, Id, "User", yob, stereotype, male, recentItem, "", recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> recents = helper.getMostRecentItemsSearch(100, search);
                if (recents.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                ViewData["recentTopLinks"] = helper.getItemTopLinks(recents);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(recents);

                return PartialView(new ItemViewModel(null, null, null, null, null, recents, null, "Global", "", "", false, "", "", "",""));
            }
        }

        [HttpPost]
        public ActionResult SearchSongsMain(string Id, string state)
        {
            string search = Request.Form["search"];

            if (state != "Global" && state != null)
            {
                DataRepository helper = new DataRepository();

                IList<Item> songs = helper.getSpecificItemsInNetworkSearch(Id, 1, search, 100);
                if (songs.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);

                ViewData["songsTopLinks"] = helper.getItemTopLinks(songs);
                ViewData["songsYouTubes"] = helper.getItemYouTubes(songs);

                return PartialView(new ItemViewModel(songs, null, null, null, null, null, Id, "User", yob, stereotype, male, recentItem, "", recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> songs = helper.getSpecificItemsSearch(1, search, 100);
                if (songs.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                ViewData["songsTopLinks"] = helper.getItemTopLinks(songs);
                ViewData["songsYouTubes"] = helper.getItemYouTubes(songs);

                return PartialView(new ItemViewModel(songs, null, null, null, null, null, null, "Global", "", "", false, "", "", "",""));
            }
        }

        [HttpPost]
        public ActionResult SearchFilmsMain(string Id, string state)
        {
            string search = Request.Form["search"];

            if (state != "Global" && state != null)
            {
                DataRepository helper = new DataRepository();

                IList<Item> films = helper.getSpecificItemsInNetworkSearch(Id, 6, search, 100);
                if (films.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);

                ViewData["filmsTopLinks"] = helper.getItemTopLinks(films);
                ViewData["filmsYouTubes"] = helper.getItemYouTubes(films);

                return PartialView(new ItemViewModel(null, films, null, null, null, null, Id, "User", yob, stereotype, male, recentItem, "", recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> films = helper.getSpecificItemsSearch(6, search, 100);
                if (films.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                ViewData["filmsTopLinks"] = helper.getItemTopLinks(films);
                ViewData["filmsYouTubes"] = helper.getItemYouTubes(films);

                return PartialView(new ItemViewModel(null, films, null, null, null, null, null, "Global", "", "", false, "", "", "",""));
            }
        }

        [HttpPost]
        public ActionResult SearchBooksMain(string Id, string state)
        {
            string search = Request.Form["search"];

            if (state != "Global" && state != null)
            {
                DataRepository helper = new DataRepository();

                IList<Item> books = helper.getSpecificItemsInNetworkSearch(Id, 3, search, 100);
                if (books.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);

                ViewData["filmsTopLinks"] = helper.getItemTopLinks(books);
                ViewData["filmsYouTubes"] = helper.getItemYouTubes(books);

                return PartialView(new ItemViewModel(null, null, books, null, null, null, Id, "User", yob, stereotype, male, recentItem, "", recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> books = helper.getSpecificItemsSearch(3, search, 100);
                if (books.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                ViewData["booksTopLinks"] = helper.getItemTopLinks(books);
                ViewData["booksYouTubes"] = helper.getItemYouTubes(books);

                return PartialView(new ItemViewModel(null, null, books, null, null, null, null, "Global", "", "", false, "", "", "",""));
            }
        }

        [HttpPost]
        public ActionResult SearchGamesMain(string Id, string state)
        {
            string search = Request.Form["search"];

            if (state != "Global" && state != null)
            {
                DataRepository helper = new DataRepository();

                IList<Item> games = helper.getSpecificItemsInNetworkSearch(Id, 4, search, 100);
                if (games.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);

                ViewData["gamesTopLinks"] = helper.getItemTopLinks(games);
                ViewData["gamesYouTubes"] = helper.getItemYouTubes(games);

                return PartialView(new ItemViewModel(null, null, null, games, null, null, Id, "User", yob, stereotype, male, recentItem, "", recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> games = helper.getSpecificItemsSearch(4, search, 100);
                if (games.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                ViewData["gamesTopLinks"] = helper.getItemTopLinks(games);
                ViewData["gamesYouTubes"] = helper.getItemYouTubes(games);

                return PartialView(new ItemViewModel(null, null, null, games, null, null, null, "Global", "", "", false, "", "", "",""));
            }
        }

        [HttpPost]
        public ActionResult SearchAppsMain(string Id, string state)
        {
            string search = Request.Form["search"];

            if (state != "Global" && state != null)
            {
                DataRepository helper = new DataRepository();

                IList<Item> apps = helper.getSpecificItemsInNetworkSearch(Id, 5, search, 100);
                if (apps.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);

                ViewData["appsTopLinks"] = helper.getItemTopLinks(apps);
                ViewData["appsYouTubes"] = helper.getItemYouTubes(apps);

                return PartialView(new ItemViewModel(null, null, null, null, apps, null, Id, "User", yob, stereotype, male, recentItem, "", recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> apps = helper.getSpecificItemsSearch(5, search, 100);
                if (apps.Count() < 1)
                {
                    ViewData["NoResults"] = "Sorry, there are no results for '" + search + "'";
                }
                ViewData["appsTopLinks"] = helper.getItemTopLinks(apps);
                ViewData["appsYouTubes"] = helper.getItemYouTubes(apps);

                return PartialView(new ItemViewModel(null, null, null, null, apps, null, null, "Global", "", "", false, "", "", "",""));
            }
        }


        public ActionResult Songs(string Id, string state)
        {
            if (state != "Global")
            {
                DataRepository helper = new DataRepository();
                IList<Item> songs = helper.getSpecificItemsInNetwork(Id,1,100);
                IList<Item> mostRecentItems = helper.getMostRecentSpecificItemsInNetwork(Id,1,100);
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);
                ViewData["songsTopLinks"] = helper.getItemTopLinks(songs);
                ViewData["songsYouTubes"] = helper.getItemYouTubes(songs);
                ViewData["recentTopLinks"] = helper.getItemTopLinks(mostRecentItems);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(mostRecentItems);
                ViewData["Following"] = helper.getFollowingCount(Id);
                ViewData["FollowedBy"] = helper.getFollowedByCount(Id);
                return View(new ItemViewModel(songs, null, null, null, null, mostRecentItems, Id, "Network",yob,stereotype,male,recentItem,"",recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> songs = helper.getSpecificItems(1,100);
                IList<Item> mostRecentItems = helper.getMostRecentSpecificItems(1,100);
                ViewData["songsTopLinks"] = helper.getItemTopLinks(songs);
                ViewData["songsYouTubes"] = helper.getItemYouTubes(songs);
                ViewData["recentTopLinks"] = helper.getItemTopLinks(mostRecentItems);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(mostRecentItems);
                return View(new ItemViewModel(songs, null, null, null, null, mostRecentItems, null, "Global", "", "", false, "", "", "",""));
            }
        }

        public ActionResult Films(string Id, string state)
        {
            if (state != "Global")
            {
                DataRepository helper = new DataRepository();
                IList<Item> films = helper.getSpecificItemsInNetwork(Id, 6,100);
                IList<Item> mostRecentItems = helper.getMostRecentSpecificItemsInNetwork(Id, 6,100);
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);
                ViewData["filmsTopLinks"] = helper.getItemTopLinks(films);
                ViewData["filmsYouTubes"] = helper.getItemYouTubes(films);
                ViewData["recentTopLinks"] = helper.getItemTopLinks(mostRecentItems);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(mostRecentItems);
                ViewData["Following"] = helper.getFollowingCount(Id);
                ViewData["FollowedBy"] = helper.getFollowedByCount(Id);

                return View(new ItemViewModel(null, films, null, null, null, mostRecentItems, Id, "Network", yob, stereotype, male, recentItem, "",recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> films = helper.getSpecificItems(6,100);
                IList<Item> mostRecentItems = helper.getMostRecentSpecificItems(6,100);
                ViewData["filmsTopLinks"] = helper.getItemTopLinks(films);
                ViewData["filmsYouTubes"] = helper.getItemYouTubes(films);
                ViewData["recentTopLinks"] = helper.getItemTopLinks(mostRecentItems);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(mostRecentItems);
                return View(new ItemViewModel(null, films, null, null, null, mostRecentItems, null, "Global", "", "", false, "", "","",""));
            }
        }

        public ActionResult Books(string Id, string state)
        {
            if (state != "Global")
            {
                DataRepository helper = new DataRepository();
                IList<Item> books = helper.getSpecificItemsInNetwork(Id, 3,100);
                IList<Item> mostRecentItems = helper.getMostRecentSpecificItemsInNetwork(Id, 3,100);
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);
                ViewData["booksTopLinks"] = helper.getItemTopLinks(books);
                ViewData["booksYouTubes"] = helper.getItemYouTubes(books);
                ViewData["recentTopLinks"] = helper.getItemTopLinks(mostRecentItems);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(mostRecentItems);
                ViewData["Following"] = helper.getFollowingCount(Id);
                ViewData["FollowedBy"] = helper.getFollowedByCount(Id);
                return View(new ItemViewModel(null, null, books, null, null, mostRecentItems, Id, "Network", yob, stereotype, male, recentItem, "",recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> books = helper.getSpecificItems(3,100);
                IList<Item> mostRecentItems = helper.getMostRecentSpecificItems(3,100);
                ViewData["booksTopLinks"] = helper.getItemTopLinks(books);
                ViewData["booksYouTubes"] = helper.getItemYouTubes(books);
                ViewData["recentTopLinks"] = helper.getItemTopLinks(mostRecentItems);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(mostRecentItems);
                return View(new ItemViewModel(null, null, books, null, null, mostRecentItems, null, "Global", "", "", false, "", "","",""));
            }
        }

        public ActionResult Games(string Id, string state)
        {
            if (state != "Global")
            {
                DataRepository helper = new DataRepository();
                IList<Item> games = helper.getSpecificItemsInNetwork(Id, 4,100);
                IList<Item> mostRecentItems = helper.getMostRecentSpecificItemsInNetwork(Id, 4,100);
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);
                ViewData["gamesTopLinks"] = helper.getItemTopLinks(games);
                ViewData["gamesYouTubes"] = helper.getItemYouTubes(games);
                ViewData["recentTopLinks"] = helper.getItemTopLinks(mostRecentItems);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(mostRecentItems);
                ViewData["Following"] = helper.getFollowingCount(Id);
                ViewData["FollowedBy"] = helper.getFollowedByCount(Id);
                return View(new ItemViewModel(null, null, null, games, null, mostRecentItems, Id, "Network", yob, stereotype, male, recentItem, "",recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> games = helper.getSpecificItems(4,100);
                IList<Item> mostRecentItems = helper.getMostRecentSpecificItems(4,100);
                ViewData["gamesTopLinks"] = helper.getItemTopLinks(games);
                ViewData["gamesYouTubes"] = helper.getItemYouTubes(games);
                ViewData["recentTopLinks"] = helper.getItemTopLinks(mostRecentItems);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(mostRecentItems);
                return View(new ItemViewModel(null, null, null, games, null, mostRecentItems, null, "Global", "", "", false, "", "","",""));
            }
        }

        public ActionResult Apps(string Id, string state)
        {
            if (state != "Global")
            {
                DataRepository helper = new DataRepository();
                IList<Item> apps = helper.getSpecificItemsInNetwork(Id, 5,100);
                IList<Item> mostRecentItems = helper.getMostRecentSpecificItemsInNetwork(Id, 5,100);
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);
                ViewData["appsTopLinks"] = helper.getItemTopLinks(apps);
                ViewData["appsYouTubes"] = helper.getItemYouTubes(apps);
                ViewData["recentTopLinks"] = helper.getItemTopLinks(mostRecentItems);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(mostRecentItems);
                ViewData["Following"] = helper.getFollowingCount(Id);
                ViewData["FollowedBy"] = helper.getFollowedByCount(Id);
                return View(new ItemViewModel(null, null, null, null, apps, mostRecentItems, Id, "Network", yob, stereotype, male, recentItem, "",recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> apps = helper.getSpecificItems(5,100);
                IList<Item> mostRecentItems = helper.getMostRecentSpecificItems(5,100);
                ViewData["appsTopLinks"] = helper.getItemTopLinks(apps);
                ViewData["appsYouTubes"] = helper.getItemYouTubes(apps);
                ViewData["recentTopLinks"] = helper.getItemTopLinks(mostRecentItems);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(mostRecentItems);
                return View(new ItemViewModel(null, null, null, null,apps, mostRecentItems, null, "Global", "", "", false, "", "","",""));
            }
        }

        public ActionResult Recently(string Id, string state)
        {
            if (state != "Global")
            {
                DataRepository helper = new DataRepository();
                IList<Item> mostRecentItems = helper.getMostRecentItemsInNetwork(Id,100);
                string yob = helper.getYOB(Id);
                string stereotype = helper.getStereotype(Id);
                bool male = helper.isMale(Id);
                string recentItem = helper.getMostRecentItem(Id);
                string recentItemId = helper.getMostRecentItemId(Id);
                string location = helper.getLocation(Id);
                ViewData["recentTopLinks"] = helper.getItemTopLinks(mostRecentItems);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(mostRecentItems);
                ViewData["Following"] = helper.getFollowingCount(Id);
                ViewData["FollowedBy"] = helper.getFollowedByCount(Id);
                return View(new ItemViewModel(null, null, null, null, null, mostRecentItems, Id, "Network", yob, stereotype, male, recentItem, "",recentItemId,location));
            }
            else//Global State
            {
                DataRepository helper = new DataRepository();
                IList<Item> mostRecentItems = helper.getMostRecentItems(100);
                ViewData["recentTopLinks"] = helper.getItemTopLinks(mostRecentItems);
                ViewData["recentYouTubes"] = helper.getItemYouTubes(mostRecentItems);
                return View(new ItemViewModel(null, null, null, null, null, mostRecentItems, null, "Global", "", "", false, "", "","",""));
            }
        }
        
        public ActionResult Follow(string Id)
        {
            string id = Id;
            DataRepository helper = new DataRepository();
            if (!helper.AlreadyFollowingUser(Session["Username"].ToString(), Id))
                helper.AddFollowConnection(Session["Username"].ToString(), Id);

            ViewData["Username"] = id;
            ViewData["Following"] = helper.getFollowingCount(Id);
            ViewData["FollowedBy"] = helper.getFollowedByCount(Id);

            return PartialView();
        }

        public ActionResult Unfollow(string Id)
        {
            string id = Id;
            DataRepository helper = new DataRepository();

            FollowConnection deleteMe2 = helper.getFollowConnectionFromUser(Session["Username"].ToString(),Id);

            helper.deleteConnection(deleteMe2);

            ViewData["Username"] = id;
            ViewData["Following"] = helper.getFollowingCount(Id);
            ViewData["FollowedBy"] = helper.getFollowedByCount(Id);

            return PartialView();
        }

        public ActionResult Like(string ItemId,string Id,string state,string guid)
        {
           
            DataRepository helper = new DataRepository();

            ViewData["ItemId"] = ItemId;
            ViewData["Id"] = Id;
            ViewData["State"] = state;
            ViewData["Guid"] = guid;

            helper.LikeItem(ItemId);

            return PartialView();
        }

        public ActionResult DontLike(string ItemId,string Id,string state,string guid)
        {
            DataRepository helper = new DataRepository();
          
            ViewData["ItemId"] = ItemId;
            ViewData["Id"] = Id;
            ViewData["State"] = state;
            ViewData["Guid"] = guid;

            helper.DontLikeItem(ItemId);

            return PartialView();
        }

        public ActionResult RecSong()
        {
            if (Session["Username"] != null)
            {
                return PartialView();
            }
            else
                return PartialView("Message");
        }
        public ActionResult RecFilm()
        {
            if (Session["Username"] != null)
            {
                return PartialView();
            }
            else
                return PartialView("Message");
        }
        public ActionResult RecBook()
        {
            if (Session["Username"] != null)
            {
                return PartialView();
            }
            else
                return PartialView("Message");
        }
        public ActionResult RecGame()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "PS3",
                Value = "PS3"
            });
            items.Add(new SelectListItem
            {
                Text = "PC",
                Value = "PC"
            });
            items.Add(new SelectListItem
            {
                Text = "XBOX",
                Value = "XBOX",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "Wii",
                Value = "Wii"
            });
            items.Add(new SelectListItem
            {
                Text = "PSP",
                Value = "PSP"
            });
            items.Add(new SelectListItem
            {
                Text = "DS",
                Value = "DS"
            });

            GameViewModel model = new GameViewModel();
            model.consoles = items;
            if (Session["Username"] != null)
            {
                return PartialView(model);
            }
            else
                return PartialView("Message");
        }

        public ActionResult RecApp()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "iPhone",
                Value = "iPhone"
            });
            items.Add(new SelectListItem
            {
                Text = "iPad",
                Value = "iPad"
            });
            items.Add(new SelectListItem
            {
                Text = "Android",
                Value = "Android",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "Blackberry",
                Value = "Blackberry"
            });
            items.Add(new SelectListItem
            {
                Text = "Windows Phone",
                Value = "Windows Phone"
            });

            AppViewModel model = new AppViewModel();
            model.platforms = items;

            if (Session["Username"] != null)
            {
                return PartialView(model);
            }
            else
                return PartialView("Message");
        }

        public ActionResult RecSongRequest(string songName,string artistName, string url, string comment)
        {
            //store new item, link and new comment(if not null)
            //HtmlScraper scraper = new HtmlScraper();
            //scraper.ScrapeUrl(url);
            //string body = scraper.GetBody();

            //if (body.Contains(songName) && body.Contains(artistName))
            //{
                DataRepository helper = new DataRepository();
                string name = songName + " by " + artistName;
                string username = Session["Username"].ToString();
                int itemId = helper.AddItem(name, username, 1);

                helper.AddLink(url, itemId, username);

                if (comment != "")
                {
                    helper.AddComment(username, itemId, comment);
                }
                
            return PartialView("Index", new { Id = Session["Username"].ToString(), state = "Network" });
        }

        public ActionResult RecFilmRequest(string filmName, string url, string comment)
        {
            //store new item, link and new comment(if not null)
            DataRepository helper = new DataRepository();
            string name = filmName;
            string username = Session["Username"].ToString();
            int itemId = helper.AddItem(name, username, 6);

            helper.AddLink(url, itemId, username);

            if (comment != "")
            {
                helper.AddComment(username, itemId, comment);
            }

            return RedirectToAction("Index", new { Id = Session["Username"].ToString(), state = "Network" });
        }

        public ActionResult RecBookRequest(string bookName, string authorName, string url, string comment)
        {
            //store new item, link and new comment(if not null)
            DataRepository helper = new DataRepository();
            string name = bookName + " by " + authorName;
            string username = Session["Username"].ToString();
            int itemId = helper.AddItem(name, username, 3);

            helper.AddLink(url, itemId,username);

            if (comment != "")
            {
                helper.AddComment(username, itemId, comment);
            }

            return RedirectToAction("Index", new { Id = Session["Username"].ToString(), state = "Network" });
        }

        public ActionResult RecGameRequest(string gameName, string consoleName, string url, string comment)
        {
            //store new item, link and new comment(if not null)
            DataRepository helper = new DataRepository();
            string name = gameName + " on " + consoleName;
            string username = Session["Username"].ToString();
            int itemId = helper.AddItem(name, username, 4);

            helper.AddLink(url, itemId, username);

            if (comment != "")
            {
                helper.AddComment(username, itemId, comment);
            }

            return RedirectToAction("Index", new { Id = Session["Username"].ToString(), state = "Network" });
        }

        public ActionResult RecAppRequest(string appName, string platform, string url, string comment)
        {
            //store new item, link and new comment(if not null)
            DataRepository helper = new DataRepository();
            string name = appName + " on " + platform;
            string username = Session["Username"].ToString();
            int itemId = helper.AddItem(name, username, 5);

            helper.AddLink(url, itemId,username);

            if (comment != "")
            {
                helper.AddComment(username, itemId, comment);
            }

            return RedirectToAction("Index", new { Id = Session["Username"].ToString(), state = "Network" });
        }


        public ActionResult Discussion(string Id, string state, int itemId)
        {
            DataRepository helper = new DataRepository();

            Item item = helper.getItem(itemId);
            IList<ItemComment> comments = helper.getComments(itemId);
            IList<Link> links = helper.getLinks(itemId);
            string topLink = helper.getTopLink(itemId);
            string youtubeLink = helper.getTopYoutubeLink(itemId);

            DiscussionViewModel model = new DiscussionViewModel(itemId, comments, links, Id, state, item,"","",topLink,youtubeLink);

            return View(model);
        }

        public ActionResult AddLink(string Id,int ItemId)
        {
            if (Session["Username"] != null)
            {
                DataRepository helper = new DataRepository();
                Item item = helper.getItem(ItemId);
                DiscussionViewModel model = new DiscussionViewModel(ItemId, null, null, "", "", item, "", "", "", "");
                return PartialView(model);
            }
            else
            {
                return PartialView("LoginAlert");
            }
        }

        public ActionResult AllLinks(int ItemId)
        {
            DataRepository helper = new DataRepository();
            IList<Link> links = helper.getLinks(ItemId);
            Item item = helper.getItem(ItemId);
            DiscussionViewModel model = new DiscussionViewModel(ItemId,null, links, "", "", item, "", "", "", "");
            return PartialView(model);
        }

        public ActionResult AddComment(string Id, int ItemId,string comment)
        { 
            DataRepository helper = new DataRepository();
            
            string commentLeaver = "";
            if (Session["Username"] == null)
            {
                ViewData["Alert"] = "Please sign in to leave a comment";
                Item itemB = helper.getItem(ItemId);
                IList<ItemComment> commentsB = helper.getComments(ItemId);
                IList<Link> linksB = helper.getLinks(ItemId);

                DiscussionViewModel modelB = new DiscussionViewModel(ItemId, commentsB, linksB,"", "", itemB, "", "","","");
                return PartialView(modelB);
            }
            else
                commentLeaver = Session["Username"].ToString();

            helper.AddComment(commentLeaver, ItemId, comment);

            Item item = helper.getItem(ItemId);
            IList<ItemComment> comments = helper.getComments(ItemId);
            IList<Link> links = helper.getLinks(ItemId);

            DiscussionViewModel model = new DiscussionViewModel(ItemId, comments, links,"", "", item, "", "","","");

            return PartialView(model);
        }

        public ActionResult DeleteComment(string Id, int ItemId)
        {
            DataRepository helper = new DataRepository();

            helper.DeleteComment(Id);

            Item item = helper.getItem(ItemId);
            IList<ItemComment> comments = helper.getComments(ItemId);
            IList<Link> links = helper.getLinks(ItemId);

            DiscussionViewModel model = new DiscussionViewModel(ItemId, comments, links, "", "", item, "", "","","");

            ViewData["Id"] = Id;

            return PartialView(model);
        }

              

        public ActionResult LikeComment(string Id, int ItemId)
        {
            DataRepository helper = new DataRepository();

            helper.LikeComment(Id);

            Item item = helper.getItem(ItemId);
            IList<ItemComment> comments = helper.getComments(ItemId);
            IList<Link> links = helper.getLinks(ItemId);

            DiscussionViewModel model = new DiscussionViewModel(ItemId, comments, links, "", "", item, "", "","","");

            ViewData["Id"] = Id;

            return PartialView(model);
        }

        public ActionResult LikeLink(string LinkId)
        {
            DataRepository helper = new DataRepository();

            helper.LikeLink(Convert.ToInt32(LinkId));

            ViewData["Id"] = LinkId;

            return PartialView();
        }

        public ActionResult DeadLink(string LinkId)
        {
            DataRepository helper = new DataRepository();

            helper.ReportAsDead(Convert.ToInt32(LinkId));

            ViewData["Id"] = LinkId;

            return PartialView();
        }

        public ActionResult DeReportLink(string LinkId)
        {
            DataRepository helper = new DataRepository();

            helper.DereportAsDead(Convert.ToInt32(LinkId));

            ViewData["Id"] = LinkId;

            return PartialView();
        }

        public ActionResult DelikeLink(string LinkId)
        {
            DataRepository helper = new DataRepository();

            helper.DelikeLink(Convert.ToInt32(LinkId));

            ViewData["Id"] = LinkId;

            return PartialView();
        }

        public ActionResult DeleteLink(string Id, int ItemId)
        {
            DataRepository helper = new DataRepository();

            helper.DeleteLink(Convert.ToInt32(Id));

            IList<Link> links = helper.getLinks(ItemId);

            DiscussionViewModel model = new DiscussionViewModel(-1, null, links, "", "", null, "", "", "", "");

            ViewData["Id"] = Id;

            return PartialView(model);  
        }

        public ActionResult DelikeComment(string Id, int ItemId)
        {
            DataRepository helper = new DataRepository();

            helper.DelikeComment(Id);

            Item item = helper.getItem(ItemId);
            IList<ItemComment> comments = helper.getComments(ItemId);
            IList<Link> links = helper.getLinks(ItemId);

            DiscussionViewModel model = new DiscussionViewModel(ItemId, comments, links, "", "", item, "", "", "", "");

            ViewData["Id"] = Id;

            return PartialView(model);
        }

        public ActionResult AddLinkRequest(int itemId, string link)
        {
            //store new item, link and new comment(if not null)
            DataRepository helper = new DataRepository();

            if (!helper.linkExists(link, itemId))
                helper.AddLink(link, itemId, Session["Username"].ToString());
            else
            {
                helper.LikeLinkInstead(link, itemId);
                TempData["LinkExists"] = "That link has already been added against this recommendation. We have liked this link instead.";
            }

            Item item = helper.getItemById(itemId.ToString());

            return RedirectToAction("Discussion", new {Id = item.Username, state = "Network", itemId = itemId });
        }

        [HttpPost]
        public ActionResult UserSearch()
        {
            string search = Request.Form["SearchMate"];

            DataRepository helper = new DataRepository();
            if (helper.usernameExists(search))
            {
                return RedirectToAction("Index", new { Id = search,state="User" });
            }
            else
            {
                TempData["UserDoesNotExist"] = "That user does not exist.";
                return RedirectToAction("Index");
            }
        }
    }
}
