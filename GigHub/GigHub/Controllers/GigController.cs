using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigController : Controller
    {
        //get the list genre from the database
        private ApplicationDbContext _context;


        public GigController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Gig
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };  

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
            }
              
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue

            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            
            //redirect the action to home page for now
            return RedirectToAction("Index", "Home");

        }
    }
}