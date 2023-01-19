using Microsoft.AspNetCore.Mvc;
using TODO.Data.DataBase;
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
    }
}
