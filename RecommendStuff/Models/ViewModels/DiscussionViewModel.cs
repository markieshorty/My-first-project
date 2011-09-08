using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RecommendStuff.Models.ViewModels
{
    public class DiscussionViewModel
    {
        public DiscussionViewModel
        (int itemId,
            IList<ItemComment> comments,
            IList<Link> links,
            string username,
            string state,
            Item item,
            string link,
            string comment,
            string topLink,
            string youtubeLink
            )
        {
            this.links = links;
            this.comments = comments;
            this.itemId = itemId;
            this.username = username;
            this.state = state;
            this.item = item;
            this.link = link;
            this.comment = comment;
            this.topLink = topLink;
            this.youtubeLink = youtubeLink;
        }
        public Item item { get; private set; }
        public IList<Link> links { get; private set; }
        public IList<ItemComment> comments { get; private set; }
        public int itemId { get; private set; }
        public string username { get; private set; }
        public string state { get; private set; }
        [Required]
        [RegularExpression(@"((https?|ftp|gopher|telnet|file|notes|ms-help):((//)|(\\\\))+[\w\d:#@%/;$()~_?\+-=\\\.&]*)", ErrorMessage = "A valid URL is required.")]
        public string link { get; private set; }
        public string topLink { get; private set; }
        [Required]
        public string comment { get; private set; }
        public string youtubeLink { get; private set; }
    }
}
