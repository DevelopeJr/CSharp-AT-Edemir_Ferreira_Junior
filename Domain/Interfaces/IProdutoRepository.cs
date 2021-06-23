using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProdutoRepository

    {
        IList<ProdutoModel> Pesquisar(string busca);
        void Adicionar(ProdutoModel produtoModel);
        void Editar(Guid idProduto, string nomeProduto, string caracteristicaProduto, int quantidade, DateTime dataFabricacao);
        void Deletar(ProdutoModel produtoModel);
    }
}
