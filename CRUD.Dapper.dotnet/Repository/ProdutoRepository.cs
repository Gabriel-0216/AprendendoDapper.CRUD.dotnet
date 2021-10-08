using CRUD.Dapper.dotnet.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Dapper.dotnet.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        IConfiguration _configuration;

        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("ProdutoConnection").Value;
            return connection;
        }

        public int Add(Produto produto)
        {
            var connectionString = this.GetConnection();
            int rows = 0;

            var query = $@"insert into Produtos(ProdutoId, NomeProduto, Estoque, Preco) VALUES (@Id, @Nome, @Estoque, @Preco)";
            DynamicParameters param = new DynamicParameters();
            param.Add("Id", produto.ProdutoId);
            param.Add("Nome", produto.NomeProduto);
            param.Add("Estoque", produto.Estoque);
            param.Add("Preco", produto.Preco);

            using(var connection = new SqlConnection(connectionString))
            {
                rows = connection.Execute(query, param: param, commandType: System.Data.CommandType.Text);
            }

                return rows;
    
        }

        public int Delete(int id)
        {
            int rows = 0;
            var connectionString = this.GetConnection();
            DynamicParameters param = new DynamicParameters();
            var query = $@"delete from Produtos where ProdutoId = @Id";
            param.Add("Id", id);

            using (var connection = new SqlConnection(connectionString))
            {
                rows = connection.Execute(query, param: param, commandType: System.Data.CommandType.Text);
            }

            return rows;
            
        }

        public int Edit(Produto produto)
        {
            int rows = 0;
            var connectionString = this.GetConnection();
            DynamicParameters param = new DynamicParameters();

            var query = $@"update from Produtos SET NomeProduto = @Nome, Estoque = @Estoque, Preco = @Preco WHERE ProdutoId = @ProdutoId";
            param.Add("ProdutoId", produto.ProdutoId);
            param.Add("@Nome", produto.NomeProduto);
            param.Add("@Preco", produto.Preco);
            param.Add("@Estoque", produto.Estoque);

            using(var connection = new SqlConnection(connectionString))
            {
                rows = connection.Execute(query, param: param, commandType: System.Data.CommandType.Text);
            }

            return rows;
        }

        public Produto Get(int id)
        {
            var connectionString = this.GetConnection();
            Produto produto = new Produto();
            DynamicParameters param = new DynamicParameters();
            var query = $@"select ProdutoId, Estoque, NomeProduto, Preco from PRODUTOS where ProdutoId = @id";
            param.Add("id", id);

            using(var connection = new SqlConnection(connectionString))
            {
                produto = connection.Query<Produto>(query, param: param, commandType: System.Data.CommandType.Text).FirstOrDefault();
            }
            return produto;
        }



        public List<Produto> GetProdutos()
        {
            var listaProdutos = new List<Produto>();
            var connectionString = this.GetConnection();
            var query = $@"SELECT ProdutoId, Estoque, NomeProduto, Preco FROM PRODUTOS";
            
            using(var connection = new SqlConnection(connectionString))
            {
                listaProdutos = connection.Query<Produto>(query).ToList();
            }

            return listaProdutos;
        }
    }
}
