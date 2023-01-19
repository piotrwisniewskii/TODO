using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TODO.Data.DataBase;
using TODO.Data.Models;
using TODO.Data.Models.ViewModels;
using TODO.Services.TODOUsersServices;

namespace TODO.Controllers
{
    public class TODOUsersController : Controller
    {
        private readonly ITODOUsersService _service;
        private readonly IMapper _mapper;

        public TODOUsersController(ITODOUsersService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var usersList = await _service.GetAllAsync(n=>n.Notes);
            var model = new List<TODOUserVM>();
            foreach (var item in usersList)
            {
                var mappedUser = _mapper.Map<TODOUserVM>(item);
                model.Add(mappedUser);
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = _mapper.Map<TODOUserVM>(await _service.GetByIdAsync(id)); ;

            if (model == null) return View("NotFound");
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name","Surname", "ProfilePictureURL")]TODOUser user)
        {
            if (!ModelState.IsValid) return View(user);
            await _service.AddAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task <IActionResult> Edit(int id)
        {
            var model = _mapper.Map<TODOUserVM>(await _service.GetByIdAsync(id));

            if (model == null) return View("NotFound");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,TODOUserVM user)
        {
            if (!ModelState.IsValid) return View(user);

            if (id == user.Id)
            {
            await _service.UpdateAsync(id, _mapper.Map<TODOUser>(user));
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var model = _mapper.Map<TODOUserVM>(await _service.GetByIdAsync(id));
            if (model == null) return View("NotFound");
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = _mapper.Map<TODOUserVM>(await _service.GetByIdAsync(id));
            if (model == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
