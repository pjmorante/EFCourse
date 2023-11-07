using CursoEFCore.Data;
using CursoEFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public UsuariosController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            List<Usuario> listaUsuarios = _contexto.Usuario.AsNoTracking().ToList();
            return View(listaUsuarios);
        }
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _contexto.Usuario.Add(usuario);
                _contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Editar(int? id)
        {
            if(id is null)
                return View();

            var usuario = _contexto.Usuario.FirstOrDefault(c => c.Id == id);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _contexto.Usuario.Update(usuario);
                _contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }
        public IActionResult Borrar(int? id)
        {
            var usuario = _contexto.Usuario.FirstOrDefault(c => c.Id == id);
            _contexto.Usuario.Remove(usuario);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
