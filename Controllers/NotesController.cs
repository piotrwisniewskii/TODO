using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel.DataAnnotations;
using TODO.Data.DataBase;
using TODO.Data.Models;
using TODO.Data.Models.ViewModels;
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
            var modelNoteList = await _service.GetAllAsync(n => n.TODOUser);
            var model = new List<NoteVM>();
            foreach (var item in modelNoteList)
            {
                var mappedNote = _mapper.Map<NoteVM>(item);
                model.Add(mappedNote);
            }

            return View(model);
        }

        public async Task <IActionResult> Create()
        {
            var usersDictionary = new Dictionary<int, string>();
            var usersList = await _userService.GetAllAsync();
            foreach (var user in usersList)
            {
                usersDictionary.Add(user.Id, user.Name);
            }
            var model = new NoteVM();
            model.TODOUsersDictionary = usersDictionary;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("NoteName", "NoteMessage", "NoteDate", "Priority", "IsDone", "TODOUserId")]Note note)
        {
            if(!ModelState.IsValid)
            {
                return View(note);
            }
            _userService.GetAllAsync();
            await _service.AddAsync(_mapper.Map<Note>(note));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var modelDetails =_mapper.Map<NoteVM>(await _service.GetByIdAsync(id));

            if (modelDetails == null) return View("NotFound");
            return View(modelDetails); 
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<NoteVM>(await _service.GetByIdAsync(id));

            var usersDictionary = new Dictionary<int, string>();
            var usersList = await _userService.GetAllAsync();
            foreach (var user in usersList)
            {
                usersDictionary.Add(user.Id, user.Name);
            }
            model.TODOUsersDictionary = usersDictionary;
            return View(model);

       
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,NoteName", "NoteMessage", "NoteDate", "Priority","IsDone", "TODOUserId")] Note note)
        {
            if (!ModelState.IsValid) return View(note);

            if (id == note.Id)
            {
                await _service.UpdateAsync(id,_mapper.Map<Note>(note));
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = _mapper.Map<NoteVM>(await _service.GetByIdAsync(id));
            if (model == null) return View("NotFound");
            return View(model);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = _mapper.Map<NoteVM>(await _service.GetByIdAsync(id));
            if (model == null) return View("NotFound");

            await _service.DeleteAsync(id);
         
            return RedirectToAction(nameof(Index));
        }


    }
}
