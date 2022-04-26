using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceRec.Data.Context;
using ServiceRec.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRec.Controllers
{
    public class ClientsController : Controller
    {
        private readonly RecDbContext _context;
        public ClientsController(RecDbContext context)
        {
            _context = context;
        }

        //get : Clients
        public async Task<IActionResult> Index()
        {
            var clients = _context.Clients.Include(c => c.Service);
            return View(await clients.ToListAsync());
        }

        //get: clients/details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.Include(c => c.Service).FirstOrDefaultAsync(c => c.Id == id);

            return View(client);
        }

        //get create client
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList( _context.Clients, "Id", "ServiceName");
            return View();
        }

        //post create client
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientName,Email,CreatedAt,Status,ServiceId")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceNane", client.ServiceId);
            return View(client);           
        }

        //edit Get
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var client = await _context.Clients.FindAsync(id);
            if(client == null)
            {
                return BadRequest();
            }

            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName", client.ServiceId);
            return View(client);
        }

        //edit post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ClientName, Email, CreatedAt, Status, ServiceId")] Client client)
        {
            if(id != client.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        //delete get
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var client = _context.Clients.Include(c => c.Service).FirstOrDefault(c => c.Id == id);

            if(client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        //delete post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        private bool ClientExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }

    }
}
