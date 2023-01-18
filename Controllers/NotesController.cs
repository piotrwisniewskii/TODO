using Microsoft.AspNetCore.Mvc;
using TODO.Data.DataBase;
using TODO.Data.Models;
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

        [HttpPost]
        public async Task<IActionResult> Create([Bind("NoteName", "NoteMessage", "NoteDate", "Priority", "IsDone")]Note note)
        {
            if(!ModelState.IsValid)
            {
                return View(note);
            }
            await _service.AddAsync(note);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var modelDetails = await _service.GetByIdAsync(id);

            if (modelDetails == null) return View("NotFound");
            return View(modelDetails); 
        }

        public async Task<IActionResult> Edit(int id)
        {
            var modelDetails = await _service.GetByIdAsync(id);
            if (modelDetails == null) return View("NotFound");
            return View(modelDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,NoteName", "NoteMessage", "NoteDate", "Priority","IsDone")] Note note)
        {
            if (!ModelState.IsValid)
            {
                return View(note);
            }
            await _service.UpdateAsync(id,note);
            return RedirectToAction(nameof(Index));
        }


    }
}
