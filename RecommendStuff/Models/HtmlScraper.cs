using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Cache;
using System.Text.RegularExpressions;

namespace RecommendStuff.Models
{
    public class HtmlScraper
    {
        private string _Url;
        private string _Html;
        private string _HtmlModified;
        private string _Body;

        //public HtmlScraper()
        //{
        //    _Url = Url.Replace(" ", "");

        //    AddHttp();

        //    ScrapeUrl();

        //    // sanitise the html so it's ready to work with. TODO: make this optional?
        //    RemoveWhitespaceFromHtml();
        //    RemoveWeirdASymbol();
        //    RemoveCommas();

        //    SetBody();
        //}

        private void SetBody()
        {
            int OpeningPosition = 0;
            int ClosingPosition = 0;

            OpeningPosition = _HtmlModified.IndexOf("<body", StringComparison.OrdinalIgnoreCase);
            ClosingPosition = _HtmlModified.IndexOf("</body>", StringComparison.OrdinalIgnoreCase);

            _Body = _HtmlModified.Substring(OpeningPosition, ClosingPosition - OpeningPosition + 7);
        }

        public string GetBody()
        {
            return _Body;
        }

        private void AddHttp()
        {
            if (!_Url.StartsWith("http://")) _Url = "http://" + _Url;
        }

        public void ScrapeUrl(string url)
        {
            _Url = url.Replace(" ", "");

            AddHttp();

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(_Url);

            webRequest.Method = "GET";
            webRequest.Timeout = 5000;
            webRequest.AllowAutoRedirect = true;
            webRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:2.0b11) Gecko/20100101 Firefox/4.0b11";
            webRequest.Referer = "http://www.google.co.uk/";
            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            webRequest.KeepAlive = true;
            
            WebResponse webResponse = webRequest.GetResponse();

            StreamReader streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.Default);

            _Html = streamReader.ReadToEnd();
            _HtmlModified = _Html;
            SetBody();

            RemoveWhitespaceFromHtml();
            RemoveWeirdASymbol();
            RemoveCommas();
        }

        public void RemoveWhitespaceFromHtml()
        {
            _HtmlModified = Regex.Replace(_HtmlModified, @"\s+", " ");
            _HtmlModified = _HtmlModified.Replace(Environment.NewLine, "");
            _HtmlModified = _HtmlModified.Replace("\t", "");
        }

        public void RemoveWeirdASymbol()
        {
            _HtmlModified = _HtmlModified.Replace("Â", "");
        }

        public void RemoveCommas()
        {
            _HtmlModified = _HtmlModified.Replace(",", "");
        }

        public void SetHtml(string html)
        {
            _Html = html;
            _HtmlModified = html;
            RemoveWhitespaceFromHtml();
            RemoveWeirdASymbol();
            RemoveCommas();
            SetBody();
        }

        // ***

        public string GetHtmlBeforeString(string str, int length)
        {
            int location = _HtmlModified.IndexOf(str);

            if (location == -1) return "-1";

            return _HtmlModified.Substring(location - length, length);
        }
        
        public string GetHtmlAfterString(string str, int length)
        {
            int location = _HtmlModified.IndexOf(str);

            // if the location returns -1 (not found), return error

            if (location == -1) return "-1";

            return _HtmlModified.Substring(location + str.Length, length);
        }

        // ***

        public string GetContentsRightOfTagToChevron(string tag, int occurance)
        {
            if (occurance < 0) return "-1"; // if the occurence is less than zero it's because it couldn't build the rule. Return error immediately.
            int CurrentPosition = 0;
            int Length = 0;
            string Contents;

            for (int i = 0; i != occurance; i++)
			{
                CurrentPosition = _HtmlModified.IndexOf(tag, CurrentPosition);
                if (CurrentPosition == -1) return "-1"; // if it was -1 then the tag wasn't even found
                CurrentPosition++;
                // ++ so that it doesn't find the same index again.
			}

            // then -- to set it back to the whole tag
            CurrentPosition--;
            // add the length of the tag on so we're now just before the price.
            CurrentPosition += tag.Length;
            // now chop off everything before the price
            Contents = _HtmlModified.Substring(CurrentPosition);

            // find the position of the next opening chevron
            for (int j = 0; Contents[j] != '<'; j++)
            {
                Length++;
            }
            // the string before the opening chevron should be the price.
            return Contents.Substring(0, Length);
        }

        public string GetContentsRightOfTagToChevronFromBottom(string tag, int occurance)
        {
            if (occurance < 0) return "-1"; // if the occurence is less than zero it's because it could build the rule. Return immediately.

            int CurrentPosition = _Body.Length;
            int Length = 0;
            string Contents;

            for (int i = 0; i != occurance; i++)
            {
                CurrentPosition = _HtmlModified.LastIndexOf(tag, CurrentPosition);
                if (CurrentPosition == -1) return "-1"; // if it was -1 then the tag wasn't even found
                
            }

            // add the length of the tag on so we're now just before the price.
            CurrentPosition += tag.Length;
            // now chop off everything before the price
            Contents = _HtmlModified.Substring(CurrentPosition);

            // find the position of the next opening chevron
            for (int j = 0; Contents[j] != '<'; j++)
            {
                Length++;
            }
            // the string before the opening chevron should be the price.
            return Contents.Substring(0, Length);
        }

        public string GetTagToLeftOfString(string str)
        {
            int CurrentPosition;
            int OpeningPosition;
            int ClosingPosition;
            string Contents;
            int Length = 0;

            CurrentPosition = _Body.IndexOf(str);
            // if current position is -1 then it couldn't find the value
            if (CurrentPosition == -1) return "-1";
            // chop off the price and everything after it
            Contents = _Body.Substring(0, CurrentPosition);
            // find the location of the first closing chevron
            for (int i = Contents.Length-1; Contents[i] != '>'; i--)
            {
                Length++;
            }

            ClosingPosition = Contents.Length - Length;
            Length = 0; // reset length
            // now find the location of of the first opening chevron
            for (int j = ClosingPosition-1; Contents[j] != '<'; j--)
            {
                Length++;
            }
            // the chevrons and everything inside them should be the tag
            OpeningPosition = ClosingPosition - (Length+1);

            return Contents.Substring(OpeningPosition, ClosingPosition-OpeningPosition);
        }

        public int GetOccuranceOfStringTag(string tag, string str)
        {
            int CurrentPosition = 0;
            int OpeningPosition;
            int Occurance = 1;
            string Contents;

            for (int i = 0; i < 200; i++) // we're assuming the maximum amount of occurances isn't going to exceed 100. This is in place to prevent an infinity loop.
            {
                CurrentPosition = _Body.IndexOf(tag, CurrentPosition);

                if (CurrentPosition == -1) return -1;

                CurrentPosition += tag.Length; // add the tag so it doesn't get counted in the lookup

                // check if the tag contains our price
                int Length = 0;
                // first find the next opening chevron
                for (int j = CurrentPosition; _Body[j] != '<'; j++)
                {
                    Length++;
                }
                Contents = _Body.Substring(CurrentPosition, Length); // contents = the tag to the next closing tag

                if (Contents.IndexOf(str) != -1) break; // if the price can be found within here then we've got our occurence

                Occurance++;
                CurrentPosition++; // move the position forward so it doesnt find the same tag again.
            }

            return Occurance;
        }

        public int GetOccuranceOfStringTagFromBottom(string tag, string str)
        {
            // TODO: lots of repetition here: look at improvement.
            int CurrentPosition = _Body.Length;
            int Occurance = 1;
            string Contents;

            for (int i = 0; i < 200; i++) 
            {

                CurrentPosition = _Body.LastIndexOf(tag, CurrentPosition);

                if (CurrentPosition == -1) return -1;

                CurrentPosition += tag.Length; // add the tag so it doesn't get counted in the lookup

                // check if the tag contains our price
                int Length = 0;
                // first find the next opening chevron
                for (int j = CurrentPosition; _Body[j] != '<'; j++)
                {
                    Length++;
                }
                Contents = _Body.Substring(CurrentPosition, Length);

                if (Contents.IndexOf(str) != -1) break;

                Occurance++;
                CurrentPosition-=tag.Length; // move the position backward so it doesnt find the same tag again.
            }

            return Occurance;
        }
    }
}