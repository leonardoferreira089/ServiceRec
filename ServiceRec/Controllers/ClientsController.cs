using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceRec.Business.Interfaces;
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
        private readonly IClientBusinessService _clienteBusinessService;
        private readonly IServiceBusinessService _serviceBusinessService;
        public ClientsController(IClientBusinessService clientBusinessService, IServiceBusinessService serviceBusinessService)
        {
            _clienteBusinessService = clientBusinessService;
            _serviceBusinessService = serviceBusinessService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _clienteBusinessService.GetAllClientsAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var client = await _clienteBusinessService.GetClientAsync(id.Value);
            if (client == null)
            {
                return BadRequest();
            }

            return View(client);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["ServiceId"] = new SelectList(await _serviceBusinessService.GetAllServiceAsync(), "Id", "ServiceId");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientName,Email,CreatedAt,Status,ServiceId")] Client client)
        {
            if (ModelState.IsValid)
            {
                await _clienteBusinessService.CreateClientAsync(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var client = await _clienteBusinessService.GetClientAsync(id.Value);
            if(client == null)
            {
                return NotFound();
            }

            ViewData["ServiceId"] = new SelectList(await _serviceBusinessService.GetAllServiceAsync(), "Id", "ServiceName", client.ServiceId);
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientName,Email,CreatedAt,Status,ServiceId")] Client client)
        {
            if(id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!await _clienteBusinessService.ClientExistsAsync(client.Id))
                    {
                        return NotFound();
                    }
                    await _clienteBusinessService.UpdateClientAsync(client);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(await _serviceBusinessService.GetAllServiceAsync(), "Id", "ServiceName", client.ServiceId);
            return View(client);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var client = await _clienteBusinessService.GetClientAsync(id.Value);
            if(client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clienteBusinessService.RemoveClientAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
