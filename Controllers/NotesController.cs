using Microsoft.AspNetCore.Mvc;
using TODO.Data.DataBase;
using TODO.Services.NotesServices;

namespace TODO.Controllers
{
    public class NotesController : Controller
    {
       private readonly INotesService _service;
       public NotesController(INotesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
