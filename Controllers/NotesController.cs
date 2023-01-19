using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TODO.Data.DataBase;
using TODO.Data.Models;
using TODO.Services.NotesServices;
using TODO.Services.TODOUsersServices;

namespace TODO.Controllers
{
    public class NotesController : Controller
    {
       private readonly INotesService _service;
        private readonly ITODOUsersService _userService;
        private readonly IMapper _mapper;
       public NotesController(INotesService service, ITODOUsersService userService, IMapper mapper)
        {
            _service = service;
            _userService = userService;
            _mapper = mapper;
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
            if (!ModelState.IsValid) return View(note);

            if (id == note.Id)
            {
                await _service.UpdateAsync(id, note);
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var modelDetails = await _service.GetByIdAsync(id);
            if (modelDetails == null) return View("NotFound");
            return View(modelDetails);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, [Bind("Id,NoteName", "NoteMessage", "NoteDate", "Priority", "IsDone")] Note note)
        {
            var modelDetails = await _service.GetByIdAsync(id);
            if (modelDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
         
            return RedirectToAction(nameof(Index));
        }


    }
}
