using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UploadImagem.Context;
using UploadImagem.Models;
using UploadImagem.ViewModels;

namespace UploadImagem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PostGreeContext _context;

        public HomeController(ILogger<HomeController> logger, PostGreeContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var lojas = _context.Lojas
                .Include("ArquivoDigitalizacao")
                .ToList();

            var viewmodel = new LojasViewModel
            {
                Lojas = lojas
            };

            return View(viewmodel);
        }

        public void Deletar()
        {
            var loja3 = _context.Lojas.Where(x => x.Id == 3 || x.Id == 4).ToList();

            _context.Remove(loja3.Where(x => x.Id == 3).FirstOrDefault());
            _context.Remove(loja3.Where(x => x.Id == 4).FirstOrDefault());
            _context.SaveChanges();
        }

        public IActionResult CadastrarLoja()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(CadastrarLojaViewModel viewmodel)
        {
            var loja = new Loja
            {
                Nome = viewmodel.Nome
            };

            var arquivo = new ArquivoDigitalizacao();

            using (var ms = new MemoryStream())
            {
                viewmodel.Arquivo.CopyTo(ms);

                arquivo.Nome = viewmodel.Arquivo.Name;
                arquivo.Formato = viewmodel.Arquivo.FileName;
                arquivo.Arquivo = ms.ToArray();
            }

            loja.ArquivoDigitalizacao = arquivo;

            _context.Lojas.Add(loja);
            _context.SaveChanges();

            return View("Index");
        }
    }
}