using CursoEFCore.Data;
using CursoEFCore.Models;
using CursoEFCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public ArticulosController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            //Explicit loading, trae el nombre de las Categorias
            //List<Articulo> listaArticulos = _contexto.Articulo.ToList();

            //foreach (var articulo in listaArticulos)
            //{                
            //    _contexto.Entry(articulo).Reference(c => c.Categoria).Load();
            //}
            //Eager loading
            List<Articulo> listaArticulos = _contexto.Articulo.Include(c => c.Categoria).ToList();
            return View(listaArticulos);
        }
        public IActionResult Crear()
        {
            ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();
            articuloCategorias.ListaCategorias = _contexto.Categoria.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = i.Nombre,
                Value = i.Categoria_Id.ToString()
            });
            return View(articuloCategorias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _contexto.Articulo.Add(articulo);
                _contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();
            articuloCategorias.ListaCategorias = _contexto.Categoria.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = i.Nombre,
                Value = i.Categoria_Id.ToString()
            });
            return View(articuloCategorias);
        }
        public IActionResult Editar(int? id)
        {
            if(id is null)
                return View();

            ArticuloCategoriaVM articuloCategorias = new ArticuloCategoriaVM();
            articuloCategorias.ListaCategorias = _contexto.Categoria.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = i.Nombre,
                Value = i.Categoria_Id.ToString()
            });

            articuloCategorias.Articulo = _contexto.Articulo.FirstOrDefault(c => c.Articulo_Id == id);
            if(articuloCategorias is null)
                return NotFound();

            return View(articuloCategorias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(ArticuloCategoriaVM articuloVM)
        {
            if (articuloVM.Articulo.Articulo_Id == 0)
            {
                return View(articuloVM);
            }
            _contexto.Articulo.Update(articuloVM.Articulo);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }
        public IActionResult Borrar(int? id)
        {
            var articulo = _contexto.Articulo.FirstOrDefault(c => c.Articulo_Id == id);
            _contexto.Articulo.Remove(articulo);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult AdministrarEtiquetas(int id)
        {
            ArticuloEtiquetaVM articuloEtiquetas = new ArticuloEtiquetaVM
            {
                ListaArticuloEtiquetas = _contexto.ArticuloEtiqueta.Include(e => e.Etiqueta).Include(a => a.Articulo)
                .Where(a => a.Articulo_Id == id),

                ArticuloEtiqueta = new ArticuloEtiqueta()
                {
                    Articulo_Id = id
                },
                Articulo = _contexto.Articulo.FirstOrDefault(a => a.Articulo_Id == id),
            };

            List<int> listaTemporalEtiquetasArticulo = articuloEtiquetas.ListaArticuloEtiquetas.Select(e => e.Etiqueta_Id).ToList();
            var listaTempora = _contexto.Etiqueta.Where(e => !listaTemporalEtiquetasArticulo.Contains(e.Etiqueta_Id)).ToList();

            articuloEtiquetas.ListaEtiquetas = listaTempora.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = i.Titulo,
                Value = i.Etiqueta_Id.ToString()
            });

            return View(articuloEtiquetas);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdministrarEtiquetas(ArticuloEtiquetaVM articuloEtiquetas)
        {
            if(articuloEtiquetas.ArticuloEtiqueta.Articulo_Id != 0 && articuloEtiquetas.ArticuloEtiqueta.Etiqueta_Id != 0)
            {
                _contexto.ArticuloEtiqueta.Add(articuloEtiquetas.ArticuloEtiqueta);
                _contexto.SaveChanges();
            }
            return RedirectToAction(nameof(AdministrarEtiquetas), new
            {
                @id = articuloEtiquetas.ArticuloEtiqueta.Articulo_Id
            });
        }
        [HttpPost]
        public IActionResult EliminarEtiquetas(int idEtiqueta, ArticuloEtiquetaVM articuloEtiquetas)
        {
            int idArticulo = articuloEtiquetas.Articulo.Articulo_Id;
            ArticuloEtiqueta articuloEtiqueta = _contexto.ArticuloEtiqueta.FirstOrDefault(
                u => u.Etiqueta_Id == idEtiqueta && u.Articulo_Id == idArticulo
                );

            _contexto.ArticuloEtiqueta.Remove(articuloEtiqueta);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(AdministrarEtiquetas), new
            {
                @id = idArticulo
            });
        }
    }
}
