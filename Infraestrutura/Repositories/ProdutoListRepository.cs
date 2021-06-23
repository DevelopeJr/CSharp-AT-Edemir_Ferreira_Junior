using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using System.Linq;

namespace Infraestrutura.Repositories
{
    public sealed class ProdutoListRepository : IProdutoRepository
    {
        private static List<ProdutoModel> _ProdutoList = new List<ProdutoModel>();
        public void Adicionar(ProdutoModel produtoModel)
        {
            _ProdutoList.Add(produtoModel);
        }
        public void Editar(Guid idProduto, string nomeProduto, string caracteristicasProduto, int quantidade, DateTime dataFabricacao)
        {
            var index = _ProdutoList.FindIndex(produto => produto._idProduto.Equals(idProduto));
            var editarProduto = _ProdutoList[index];

            editarProduto.AlterarDados(nomeProduto, caracteristicasProduto, quantidade, dataFabricacao);
        }
        public void Deletar(ProdutoModel produtoModel)
        {
            _ProdutoList.Remove(produtoModel);
        }
        public IList<ProdutoModel> Pesquisar(string busca)
        {
            IList<ProdutoModel> listaProduto = new List<ProdutoModel>();

            listaProduto = _ProdutoList.Where(produtos => produtos._nomeProduto.ToLower().Contains(busca.ToLower())).ToList();

            return listaProduto;
        }
    }
}
       