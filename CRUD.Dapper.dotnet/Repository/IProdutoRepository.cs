using CRUD.Dapper.dotnet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Dapper.dotnet.Repository
{
    public interface IProdutoRepository
    {
        int Add(Produto produto);
        List<Produto> GetProdutos();
        Produto Get(int id);
        int Edit(Produto produto);
        int Delete(int id);
    }
}
