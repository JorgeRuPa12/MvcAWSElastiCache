using Microsoft.AspNetCore.Mvc;
using MvcAWSElastiCache.Models;
using MvcAWSElastiCache.Repositories;
using MvcAWSElastiCache.Services;

namespace MvcAWSElastiCache.Controllers
{
    public class CochesController : Controller
    {
        private RepositoryCoches repo;
        private ServiceCacheRedis service;
        public CochesController(RepositoryCoches repo, ServiceCacheRedis service)
        {
            this.repo = repo;
            this.service = service;
        }

        public IActionResult Index()
        {
            List<Coche> coches = this.repo.GetCoches();
            return View(coches);
        }

        public IActionResult Details(int id)
        {
            Coche coche = this.repo.FindCoche(id);
            return View(coche);
        }

        public async Task<IActionResult> Favoritos()
        {
            List<Coche> coches = await this.service.GetCochesFavoritosAsync();
            return View(coches);
        }
        public async Task<IActionResult> SeleccionarFavoritos(int idcoche)
        {
            Coche car = this.repo.FindCoche(idcoche);
            await this.service.AddFavoritoAsync(car);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteFavorito(int idcoche)
        {
            await this.service.DeleteFavoritoAsync(idcoche);
            return RedirectToAction("Index");
        }
    }
}
