using CursoEFCore.Data;
using CursoEFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Controllers
{
    public class EtiquetasController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public EtiquetasController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            List<Etiqueta> listaEtiquetas = _contexto.Etiqueta.ToList();
            return View(listaEtiquetas);
        }
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Etiqueta etiqueta)
        {
            if (ModelState.IsValid)
            {
                _contexto.Etiqueta.Add(etiqueta);
                _contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Editar(int? id)
        {
            if(id is null)
                return View();

            var etiqueta = _contexto.Etiqueta.FirstOrDefault(c => c.Etiqueta_Id == id);
            return View(etiqueta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Etiqueta etiqueta)
        {
            if (ModelState.IsValid)
            {
                _contexto.Etiqueta.Update(etiqueta);
                _contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(etiqueta);
        }
        public IActionResult Borrar(int? id)
        {
            var etiqueta = _contexto.Etiqueta.FirstOrDefault(c => c.Etiqueta_Id == id);
            _contexto.Etiqueta.Remove(etiqueta);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
