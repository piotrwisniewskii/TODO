using Microsoft.AspNetCore.Mvc;
using TODO.Data.DataBase;

namespace TODO.Controllers
{
    public class NotesController : Controller
    {
       private readonly TODODbContext _context;
       public NotesController(TODODbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Notes.ToList();
            return View(data);
        }
    }
}
