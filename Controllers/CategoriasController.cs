using CursoEFCore.Data;
using CursoEFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public CategoriasController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            //Trae todas las categorias
            List<Categoria> listaCategorias = _contexto.Categoria.ToList();

            //Pagination
            //List<Categoria> listaCategorias = _contexto.Categoria.Skip(2).Take(3).ToList();

            //consulta filtrando por fecha
            //DateTime fechaComparacion = new DateTime(2023, 11, 08);
            //List<Categoria> listaCategorias = _contexto.Categoria.Where(f => f.FechaCreacion >= fechaComparacion).ToList();
            return View(listaCategorias);

            //Seleccionar columnas especificas
            //FromSqlRaw
            // var categorias = _contexto.Categoria.FromSqlRaw("SELECT * FROM categoria WHERE nombre LIKE 'categoria%' AND activo = 1").ToList();

            //Interpolacion de string (string interpolation)
            //var id = 31;
            //var categorias = _contexto.Categoria.FromSqlRaw($"SELECT * FROM categoria WHERE categoria_id = {id}").ToList();

        }

        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contexto.Categoria.Add(categoria);
                _contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult CrearMultipleOpcion2()
        {
            List<Categoria> categorias = new List<Categoria>();
            for (int i = 0; i < 2; i++)
            {
                categorias.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
                //_contexto.Categoria.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
            }
            _contexto.Categoria.AddRange(categorias);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult CrearMultipleOpcion5()
        {
            List<Categoria> categorias = new List<Categoria>();
            for (int i = 0; i < 5; i++)
            {
                categorias.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
                //_contexto.Categoria.Add(new Categoria { Nombre = Guid.NewGuid().ToString() });
            }
            _contexto.Categoria.AddRange(categorias);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult VistaCrearMultipleOpcionFormulario()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearMultipleOpcionFormulario()
        {
            string categoriasForm = Request.Form["Nombre"];
            var listaCategorias = from val in categoriasForm.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries) select (val);
            List<Categoria> categorias = new List<Categoria>();

            foreach (var categoria in listaCategorias)
            {
                categorias.Add(new Categoria
                {
                    Nombre = categoria
                });
            }
            _contexto.Categoria.AddRange(categorias);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Editar(int? id)
        {
            if(id is null)
                return View();

            var categoria = _contexto.Categoria.FirstOrDefault(c => c.Categoria_Id == id);
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contexto.Categoria.Update(categoria);
                _contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
        public IActionResult Borrar(int? id)
        {
            var categoria = _contexto.Categoria.FirstOrDefault(c => c.Categoria_Id == id);
            _contexto.Categoria.Remove(categoria);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult BorrarMultiple2()
        {
            IEnumerable<Categoria> categorias = _contexto.Categoria.OrderByDescending(c => c.Categoria_Id).Take(2);
            _contexto.Categoria.RemoveRange(categorias);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult BorrarMultiple5()
        {
            IEnumerable<Categoria> categorias = _contexto.Categoria.OrderByDescending(c => c.Categoria_Id).Take(5);
            _contexto.Categoria.RemoveRange(categorias);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
