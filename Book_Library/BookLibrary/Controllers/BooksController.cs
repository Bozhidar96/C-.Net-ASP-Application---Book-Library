using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLC_Data.Context;
using BLC_Data.Entities;
using PagedList;
using PagedList.Mvc;

namespace BookLibary.Controllers
{
    public class BooksController : Controller
    {
        private BookLibraryCatalogDbContext db = new BookLibraryCatalogDbContext();

        // GET: Books
        public ActionResult Index(int? page, string titleSearch, string sortOrder, string authorSearch, int? genreSearch)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            IQueryable<Book> book = db.Books.AsQueryable();

            ViewBag.TitleSearch = titleSearch;
            ViewBag.AuthorSearch = authorSearch;
            
            ViewBag.GenreSearch = new SelectList(db.Genre,"Id","GenreName");
            
            if (!String.IsNullOrEmpty(titleSearch))
            {
                book = book.Where(x => x.Title.Contains(titleSearch));
            }
            if (!String.IsNullOrEmpty(authorSearch))
            {
                book = book.Where(x => x.Author.UserName.Contains(authorSearch));
            }
            if (genreSearch.HasValue)
            {
                book = book.Where(x => x.GenreId == genreSearch.Value);
            }


            ViewBag.CurrentSortParm = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSortParm = sortOrder == "author_asc" ? "author_desc" : "author_asc";
            ViewBag.GenreSortParm = sortOrder == "genre_asc" ? "genre_desc" : "genre_asc";
            ViewBag.ReleaseDataSortParm = sortOrder == "release_data_asc" ? "release_data_desc" : "release_data_asc";
            ViewBag.DescriptionSortParm = sortOrder == "description_asc" ? "description_data_desc" : "description_asc";

            switch (sortOrder)
            {
                case "title_desc":
                    book = book.OrderByDescending(x => x.Title);
                    break;
                case "author_asc":
                    book = book.OrderBy(x => x.Author.UserName);
                    break;
                case "author_desc":
                    book = book.OrderByDescending(x => x.Author.UserName);
                    break;
                case "genre_asc":
                    book = book.OrderBy(x => x.Genre.GenreName);
                    break;
                case "genre_desc":
                    book = book.OrderByDescending(x => x.Genre.GenreName);
                    break;
                case "release_data_asc":
                    book = book.OrderBy(x => x.ReleaseDate);
                    break;
                case "release_data_desc":
                    book = book.OrderByDescending(x => x.ReleaseDate);
                    break;
                case "description_asc":
                    book = book.OrderBy(x => x.Description);
                    break;
                case "description_desc":
                    book = book.OrderByDescending(x => x.Description);
                    break;
                
                default:
                    book = book.OrderBy(x => x.Title);
                    break;
            }
            return View(book.ToPagedList(pageNumber, pageSize));
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName");
            ViewBag.GenreId = new SelectList(db.Genre, "Id", "GenreName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ReleaseDate,AuthorId,GenreId,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", book.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genre, "Id", "GenreName", book.GenreId);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", book.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genre, "Id", "GenreName", book.GenreId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ReleaseDate,AuthorId,GenreId,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", book.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genre, "Id", "GenreName", book.GenreId);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
