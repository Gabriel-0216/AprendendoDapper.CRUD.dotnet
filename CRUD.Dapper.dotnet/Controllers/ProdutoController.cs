using CRUD.Dapper.dotnet.Entities;
using CRUD.Dapper.dotnet.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Dapper.dotnet.Controllers
{
    public class ProdutoController : Controller
    {
        
        private readonly IConfiguration _configuration;
       public ProdutoController(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                ProdutoRepository produtoRepository = new ProdutoRepository(_configuration);
                produtoRepository.Add(produto);
            }

            return RedirectToAction(nameof(Listar));
        }


        [HttpGet]
        public IActionResult Editar(int id)
        {
            ProdutoRepository produtoRepository = new ProdutoRepository(_configuration);
            var produto = produtoRepository.Get(id);

            return View(produto);

        }

        public IActionResult Listar()
        {
            ProdutoRepository produtoRepository = new ProdutoRepository(_configuration);
            var listaProdutos = new List<Produto>();

            listaProdutos = produtoRepository.GetProdutos();
            return View(listaProdutos);

        }

        [HttpPost]
        public IActionResult Editar(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                ProdutoRepository produtoRepository = new ProdutoRepository(_configuration);
                produtoRepository.Edit(produto);
                return RedirectToAction(nameof(Listar));
            }
            else
            {
                return NotFound();
            }
           

        }



        [HttpGet]
        public IActionResult Detalhes(int id)
        {
                ProdutoRepository produtoRepository = new ProdutoRepository(_configuration);
                var atividade = produtoRepository.Get(id);

            return View(atividade);
           
        }

        [HttpGet]
        public async Task<IActionResult> Delete (int id)
        {
            ProdutoRepository produtoRepository = new ProdutoRepository(_configuration);
            var atividade = produtoRepository.Get(id);
            if(atividade == null)
            {
                return NotFound();
            }

            return View(atividade);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            ProdutoRepository produtoRepository = new ProdutoRepository(_configuration);
            produtoRepository.Delete(id);

            return RedirectToAction(nameof(Listar));

        }

        //private bool ProdutoExists(int id)
        //{
            
        //}
       
        
    }
}
