using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbContext _db;
        public MovieController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var movies = _db.Movies;
            return View(movies);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie obj)
        {
            if (ModelState.IsValid)
            {
                _db.Movies.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Movie created successifully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public IActionResult Edit(int id)
        {
            var movie = _db.Movies.Find(id);
            return View(movie);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie obj)
        {
            if (ModelState.IsValid)
            {
                _db.Movies.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Movie updated successifully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET
        public IActionResult Details(int id)
        {
            var movie = _db.Movies.Find(id);
            if (movie == null) return NotFound();

            return View(movie);
        }

        // GET
        public IActionResult Delete(int id)
        {
            var movie = _db.Movies.Find(id);
            if(movie == null) return NotFound();
            return View(movie);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var movie = _db.Movies.Find(id);
            if(movie == null) return NotFound();

            _db.Movies.Remove(movie);
            _db.SaveChanges();
            TempData["success"] = "Movie deleted successifully";
            return RedirectToAction("Index");
        }
    }
}
