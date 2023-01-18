using Microsoft.AspNetCore.Mvc;
using TODO.Data.DataBase;

namespace TODO.Controllers
{
    public class TODOUsersController : Controller
    {
        private readonly TODODbContext _context;

        public TODOUsersController(TODODbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.TODOUsers.ToList();
            return View(data);
        }
    }
}
