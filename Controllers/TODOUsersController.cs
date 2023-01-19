using Microsoft.AspNetCore.Mvc;
using TODO.Data.DataBase;
using TODO.Data.Models;
using TODO.Services.TODOUsersServices;

namespace TODO.Controllers
{
    public class TODOUsersController : Controller
    {
        private readonly ITODOUsersService _service;

        public TODOUsersController(ITODOUsersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var modelDetails = await _service.GetByIdAsync(id);
            if (modelDetails == null) return View("NotFound");
            return View(modelDetails);
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
            var modelDetails = await _service.GetByIdAsync(id);
            if (modelDetails == null) return View("NotFound");
            return View(modelDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id", "Name", "Surname", "ProfilePictureURL")] TODOUser user)
        {
            if (!ModelState.IsValid) return View(user);

            if (id == user.Id)
            {
            await _service.UpdateAsync(id,user);
            return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var modelDetails = await _service.GetByIdAsync(id);
            if (modelDetails == null) return View("NotFound");
            return View(modelDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modelDetails = await _service.GetByIdAsync(id);
            if (modelDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
