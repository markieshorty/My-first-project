using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecommendStuff.Models.ViewModels;
using RecommendStuff.Models;
using RecommendStuff.ViewModels;

namespace RecommendStuff.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            DataRepository helper = new DataRepository();

            string username = Session["Username"].ToString();

            IList<Item> mostRecentItems = helper.getItemsByUser(username);
            //initialise item view model but filter by single user logged in
            IList<FollowConnection> friends = helper.getFollowConnections(username);

            ViewData["Friends"] = friends;
            return View(new ItemViewModel(null, null, null, null, null, mostRecentItems, username, "User","" , "", false, "", "", ""));
        }

        public ActionResult DeleteItem(int ItemId)
        {
            DataRepository helper = new DataRepository();
            
            string username = Session["Username"].ToString();
            helper.DeleteItem(ItemId);
            IList<Item> mostRecentItems = helper.getItemsByUser(username);
            //initialise item view model but filter by single user logged in
            return PartialView(new ItemViewModel(null, null, null, null, null, mostRecentItems, username, "User", "", "", false, "", "", ""));
        }

        [HttpPost]
        public ActionResult Unfollow(int Id)
        {
            DataRepository helper = new DataRepository();

            helper.deleteConnectionById(Convert.ToInt32(Id));

            string username = Session["Username"].ToString();
            IList<FollowConnection> friends = helper.getFollowConnections(username);
            ViewData["Friends"] = friends;

            return PartialView();
        }

        public ActionResult Login(LoginViewModel viewModel)
        {
            return View(viewModel);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoginRequest(LoginViewModel viewModel)
        {
            DataRepository helper = new DataRepository();

            if (helper.UserCanLogIn(viewModel))
            {
                viewModel.user = helper.GetUser(viewModel);
                Session["LoggedIn"] = "true";
                Session["Username"] = viewModel.user.Username.ToString();

                ViewData["ReloadPage"] = "true";

                return PartialView();
            }

            TempData["ErrorMessage"] = "Login Failed";
            return PartialView();
        }

        public ActionResult Register(RegisterViewModel viewModel)
        {

            List<SelectListItem> items = new List<SelectListItem>();
            
            items.Add(new SelectListItem
             {
            Text = "Female",
            Value = "0"
            });
            items.Add(new SelectListItem
            {
                Text = "Male",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "Prefer not to say",
                Value = "2"
            });

            viewModel.GenderOptions = items;

            List<SelectListItem> dobItems = new List<SelectListItem>();
            int j = 0;
            for (int i = (int)DateTime.Now.Year; i > Convert.ToInt32(DateTime.Now.Year) - 122; i--)
            {
                dobItems.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
                j++;
            }

            dobItems.Add(new SelectListItem
            {
                Text = "Prefer not to say",
                Value = j.ToString()
            });
           
            viewModel.dobYearOptions = dobItems;

            List<SelectListItem> stereotypes = new List<SelectListItem>();
          
            stereotypes.Add(new SelectListItem
            {
                Text = "Songs",
                Value = "songs"
            });
            stereotypes.Add(new SelectListItem
            {
                Text = "Films",
                Value = "films"
            });
            stereotypes.Add(new SelectListItem
            {
                Text = "Books",
                Value = "books"
            });
            stereotypes.Add(new SelectListItem
            {
                Text = "Games",
                Value = "games"
            });
            stereotypes.Add(new SelectListItem
            {
                Text = "Apps",
                Value = "apps"
            });

            viewModel.stereotypeOptions = stereotypes;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult RegisterRequest(RegisterViewModel viewModel)
        {
            DataRepository helper = new DataRepository();

            if (viewModel.username == null)
            {
                viewModel.errorMessage = "Please type in a username";
                return RedirectToAction("Register",viewModel);
            }
            else if (viewModel.password != viewModel.retypePassword)
            {
                viewModel.errorMessage = "Make sure both passwords are the same";
                return RedirectToAction("Register",viewModel);
            }
            else if (helper.usernameExists(viewModel.username))
            {
                viewModel.errorMessage = "Sorry, that alias has been taken";
                return RedirectToAction("Register",viewModel);
            }
            else
            {
                helper.AddUser(viewModel);
                helper.FollowSelf(viewModel.username.ToString());
                Session["LoggedIn"] = "true";
                Session["Username"] = viewModel.username.ToString();
                return RedirectToAction("Index", "Home", new { Id = Session["Username"].ToString(), State = "Network" });
            }
        }

        public ActionResult Unfollow(FollowConnection deleteMe)
        {
            DataRepository helper = new DataRepository();
            helper.deleteConnection(deleteMe);

            return RedirectToAction("Index");
        }

    }
}
