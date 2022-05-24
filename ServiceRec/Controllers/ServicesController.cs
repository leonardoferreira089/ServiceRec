using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceRec.Business.Interfaces;
using ServiceRec.Data.Entities;
using System.Threading.Tasks;

namespace ServiceRec.Controllers
{

    public class ServicesController : Controller
    {        
        private readonly IServiceBusinessService _serviceBusinessService;
        public ServicesController(IServiceBusinessService serviceBusinessService)
        {
            _serviceBusinessService = serviceBusinessService;
        }
        [Route("services-list")]
        public async Task<IActionResult> Index()
        {
            return View(await _serviceBusinessService.GetAllServiceAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var service = await _serviceBusinessService.GetServiceAsync(id.Value);

            if(service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServiceName,Price,Type")] Service service)
        {
            if (ModelState.IsValid)
            {
                await _serviceBusinessService.CreateServiceAsync(service);
                return RedirectToAction(nameof(Index));
            }

            return View(service);
        }
   
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var service = await _serviceBusinessService.GetServiceAsync(id.Value);

            if(service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ServiceName,Price,Type")] Service service)
        {
            if(id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!await _serviceBusinessService.ServiceExistsAsync(service.Id))
                    {
                        return NotFound();
                    }
                }
                catch(DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var service = await _serviceBusinessService.GetServiceAsync(id.Value);

            if(service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _serviceBusinessService.RemoveServiceAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
