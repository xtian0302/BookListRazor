using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;
        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Book Book { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if(id == null)
            {
                //create
                return Page();
            }
            //update
            Book = await _db.Book.FirstOrDefaultAsync(u=> u.Id == id);
            if(Book== null)
            {
                return NotFound();
            }
            return Page(); 
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {

                if(Book.Id == 0)
                {
                    _db.Book.Add(Book);
                }
                else
                {
                    //Updates Every Property of the object
                    _db.Book.Update(Book);
                }
                //Updates only the changed properties of the object
                //var BookFromDb = await _db.Book.FindAsync(Book.Id);
                //BookFromDb.Name = Book.Name;
                //BookFromDb.ISBN = Book.ISBN;
                //BookFromDb.Author = Book.Author;
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
