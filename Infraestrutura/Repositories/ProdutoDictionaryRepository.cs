using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using System.IO;
using System.Linq;

namespace Infraestrutura.Repositories
{
    public class ProdutoDictionaryRepository : IProdutoRepository
    {
        private static List<ProdutoModel> _ProdutoList = new List<ProdutoModel>();

        private static string path = "../Infraestrutura/DataBase/DadosProduto.txt";

        public IList<ProdutoModel> Pesquisar(string busca)
        {
            IList<ProdutoModel> listaProduto = new List<ProdutoModel>();

            listaProduto = _ProdutoList.Where(produto => produto._nomeProduto.ToLower().Contains(busca.ToLower())).ToList();

            return listaProduto;
        }

        public void Adicionar(ProdutoModel produtoModel)
        {
            _ProdutoList.Add(produtoModel);

            List<string> produtoStr = new List<string>();

            foreach (var produto in _ProdutoList)
            {
                produtoStr.Add(produto.ToString());
            }

            File.WriteAllLines(path, produtoStr);
        }

        public void Editar(Guid idProduto, string nomeProduto, string caracteristicaProduto, int quantidade, DateTime dataFabricacao)
        {
            var index = _ProdutoList.FindIndex(produto => produto._idProduto.Equals(idProduto));

            var editarProduto = _ProdutoList[index];

            editarProduto.AlterarDados(nomeProduto, caracteristicaProduto, quantidade, dataFabricacao);

            List<string> produtoStr = new List<string>();

            foreach (var produto in _ProdutoList)
            {
                produtoStr.Add(produto.ToString());
            }

            File.WriteAllLines(path, produtoStr);
        }

        public void Deletar(ProdutoModel produtoModel)
        {
            _ProdutoList.Remove(produtoModel);

            List<string> produtoStr = new List<string>();

            foreach (var produto in _ProdutoList)
            {
                produtoStr.Add(produto.ToString());
            }

            File.WriteAllLines(path, produtoStr);
        }

        public ProdutoDictionaryRepository()
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    string str;
                    string[] strArray;
                    str = sr.ReadLine();
                    strArray = str.Split(',');

                    Guid idProduto = new Guid(strArray[0]);
                    string nomeProduto = strArray[1];
                    string caracteristicasProduto = strArray[2];
                    int quantidade = Convert.ToInt32(strArray[3]);
                    DateTime dataFabricacao = Convert.ToDateTime(strArray[4]);

                    var produto = new ProdutoModel(nomeProduto, caracteristicasProduto, quantidade, dataFabricacao);
                    produto.AlterarId(idProduto);

                    _ProdutoList.Add(produto);
                }
            }
        }
    }


}
