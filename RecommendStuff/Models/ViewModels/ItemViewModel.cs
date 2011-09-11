using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecommendStuff.Models;
using LinqToSqlShared;

namespace RecommendStuff.ViewModels
{
    public class ItemViewModel
    {
        public ItemViewModel
        (IList<Item> songs, 
         IList<Item> films,
         IList<Item> books,
         IList<Item> games,
         IList<Item> apps,
         IList<Item> mostRecentItems,
         string username,
         string state,
         string yob,
         string stereotype,
         bool male,
         string recentItem,
         string guid,
         string recentItemId,
            string location
            )
        {
            this.songs = songs;
            this.films = films;
            this.books = books;
            this.games = games;
            this.apps = apps;
            this.username = username;
            this.mostRecentItems = mostRecentItems;
            this.state = state;
            this.yob = yob;
            this.stereotype = stereotype;
            this.male = male;
            this.recentItem = recentItem;
            this.guid = guid;
            this.recentItemId = recentItemId;
            this.location = location;
        }
 
        public IList<Item> songs { get; private set; }
        public IList<Item> films { get; private set; }
        public IList<Item> books { get; private set; }
        public IList<Item> games { get; private set; }
        public IList<Item> apps { get; private set; }
        public IList<Item> mostRecentItems { get; private set; }
        public string username { get; private set; }
        public string state { get; private set; }
        public string yob { get; private set; }
        public string stereotype { get; private set; }
        public bool male { get; private set; }
        public string recentItem { get; private set; }
        public string recentItemId { get; private set; }
        public string guid { get; private set; }
        public string location { get; private set; }
    }
}