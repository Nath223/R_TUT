using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using R_TUT.Data;
using R_TUT.Models;

namespace R_TUT.Controllers
{
    public class DOCUMENTOSController : Controller
    {
        private readonly AppDbContext _context;

        public DOCUMENTOSController(AppDbContext context)
        {
            _context = context;
        }

        // Lista de materias disponibles
        private List<SelectListItem> ObtenerMaterias()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "Programación Estructurada", Text = "Programación Estructurada" },
                new SelectListItem { Value = "Desarrollo y Programación Orientado a Objetos", Text = "Desarrollo y Programación Orientado a Objetos" },
                new SelectListItem { Value = "Diseño y Administración de Bases de Datos", Text = "Diseño y Administración de Bases de Datos" },
                new SelectListItem { Value = "Seguridad Informática", Text = "Seguridad Informática" },
                new SelectListItem { Value = "Gestión de Servidores", Text = "Gestión de Servidores" }
            };
        }

        // GET: DOCUMENTOS
        public async Task<IActionResult> Index()
        {
            return View(await _context.DOCUMENTOS.ToListAsync());
        }

        // GET: DOCUMENTOS/Create
        public IActionResult Create()
        {
            ViewBag.Materias = ObtenerMaterias(); // ✅ Enviar lista al formulario
            return View();
        }

        // POST: DOCUMENTOS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Titulo, string Autor, string Descripcion, string Bibliografia, string Materia, IFormFile Documento)
        {
            ViewBag.Materias = ObtenerMaterias(); // Reasigna la lista si hay error

            if (Documento == null || Documento.Length == 0)
            {
                ModelState.AddModelError("Documento", "Debes seleccionar un archivo PDF.");
                return View();
            }

            if (!Documento.ContentType.Equals("application/pdf"))
            {
                ModelState.AddModelError("Documento", "Solo se permiten archivos PDF.");
                return View();
            }

            using var memoryStream = new MemoryStream();
            await Documento.CopyToAsync(memoryStream);

            var nuevoDoc = new DOCUMENTOS
            {
                Titulo = Titulo,
                Autor = Autor,
                Descripcion = Descripcion,
                Bibliografia = Bibliografia,
                Materia = Materia, // ✅ Nuevo campo
                Documento = memoryStream.ToArray()
            };

            _context.Add(nuevoDoc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: DOCUMENTOS/Descargar/5
        public async Task<IActionResult> Descargar(int id)
        {
            var documento = await _context.DOCUMENTOS.FindAsync(id);
            if (documento == null)
                return NotFound();

            return File(documento.Documento, "application/pdf", $"{documento.Titulo}.pdf");
        }

        // GET: DOCUMENTOS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var documento = await _context.DOCUMENTOS.FindAsync(id);
            if (documento == null)
                return NotFound();

            ViewBag.Materias = ObtenerMaterias();
            return View(documento);
        }

        // POST: DOCUMENTOS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Autor,Descripcion,Bibliografia,Materia")] DOCUMENTOS dOCUMENTOS)
        {
            if (id != dOCUMENTOS.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var documentoExistente = await _context.DOCUMENTOS.FindAsync(id);
                    if (documentoExistente == null)
                        return NotFound();

                    documentoExistente.Titulo = dOCUMENTOS.Titulo;
                    documentoExistente.Autor = dOCUMENTOS.Autor;
                    documentoExistente.Descripcion = dOCUMENTOS.Descripcion;
                    documentoExistente.Bibliografia = dOCUMENTOS.Bibliografia;
                    documentoExistente.Materia = dOCUMENTOS.Materia; // ✅ Nuevo campo

                    _context.Update(documentoExistente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DOCUMENTOSExists(dOCUMENTOS.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Materias = ObtenerMaterias();
            return View(dOCUMENTOS);
        }

        // GET: DOCUMENTOS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var documento = await _context.DOCUMENTOS.FirstOrDefaultAsync(m => m.Id == id);
            if (documento == null)
                return NotFound();

            return View(documento);
        }

        // POST: DOCUMENTOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documento = await _context.DOCUMENTOS.FindAsync(id);
            if (documento != null)
            {
                _context.DOCUMENTOS.Remove(documento);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DOCUMENTOSExists(int id)
        {
            return _context.DOCUMENTOS.Any(e => e.Id == id);
        }
    }
}
