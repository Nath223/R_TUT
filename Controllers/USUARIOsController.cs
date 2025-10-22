using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using R_TUT.Data;
using R_TUT.Models;

namespace R_TUT.Controllers
{
    public class USUARIOsController : Controller
    {
        private readonly AppDbContext _context;

        public USUARIOsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: USUARIOs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: USUARIOs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uSUARIO = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id_USUARIO == id);
            if (uSUARIO == null)
            {
                return NotFound();
            }

            return View(uSUARIO);
        }

        // GET: USUARIOs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: USUARIOs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_USUARIO,Nombre,Cuatrimestre,Matricula,N_Usuario,Contrasena")] USUARIO uSUARIO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uSUARIO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uSUARIO);
        }

        // GET: USUARIOs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uSUARIO = await _context.Usuarios.FindAsync(id);
            if (uSUARIO == null)
            {
                return NotFound();
            }
            return View(uSUARIO);
        }

        // POST: USUARIOs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_USUARIO,Nombre,Cuatrimestre,Matricula,N_Usuario,Contrasena")] USUARIO uSUARIO)
        {
            if (id != uSUARIO.Id_USUARIO)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uSUARIO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!USUARIOExists(uSUARIO.Id_USUARIO))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(uSUARIO);
        }

        // GET: USUARIOs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uSUARIO = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id_USUARIO == id);
            if (uSUARIO == null)
            {
                return NotFound();
            }

            return View(uSUARIO);
        }

        // POST: USUARIOs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uSUARIO = await _context.Usuarios.FindAsync(id);
            if (uSUARIO != null)
            {
                _context.Usuarios.Remove(uSUARIO);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool USUARIOExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id_USUARIO == id);
        }
    }
}
