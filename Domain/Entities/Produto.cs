using System;

namespace Domain.Entities
{
    public class ProdutoModel
    {
        public ProdutoModel(string nomeProduto, string caracteristicasProduto, int quantidade, DateTime dataFabricacao)
        {
            _idProduto = Guid.NewGuid();
            _nomeProduto = nomeProduto;
            _caracteristicasProduto = caracteristicasProduto;
            _quantidade = quantidade;
            _dataFabricacao = dataFabricacao;
        }

        public Guid _idProduto { get; private set; }
        public string _nomeProduto { get; private set; }
        public string _caracteristicasProduto { get; private set; }
        public int _quantidade { get; private set; }
        public DateTime _dataFabricacao { get; private set; }
        public void AlterarId(Guid id)
        {
            _idProduto = id;
        }
        public void AlterarDados(string nomeProduto, string caracteristicasProduto, int quantidade, DateTime dataFabricacao)
        {
            _nomeProduto = nomeProduto;
            _caracteristicasProduto = caracteristicasProduto;
            _quantidade = quantidade;
            _dataFabricacao = dataFabricacao;
            
            
        }
        public void Validade(DateTime dataFabricacao)
        {
            DateTime validade = new DateTime();

            validade = dataFabricacao;

            validade = validade.AddYears(2);

            var dataAtual = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            int comparaData = DateTime.Compare(dataAtual, validade);

            var diferencaTempo = validade.Subtract(DateTime.Now);

            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine($"Prazo de garantia: {validade:dd/MM/yyyy}");

            Console.WriteLine("");

            if (comparaData < 0)
            {
                Console.WriteLine($"O Produto está dentro do prazo de garantia!");
                Console.WriteLine($"Faltam {diferencaTempo.Days} para expirar garantia!");
            }
            else if (comparaData == 0)
            {
                Console.WriteLine($"Garantia do Produto está expirando hoje!");
            }
            else
            {
                Console.WriteLine($"Garantia expirada!");
                Console.WriteLine($"Garantia expirada a {System.Math.Abs(diferencaTempo.Days)} dias!");
            }
            Console.WriteLine("------------------------------------------------------");
        }
        public override string ToString()
        {
            return $"{_idProduto}, {_nomeProduto}, {_caracteristicasProduto}, {_quantidade}, {_dataFabricacao:dd/MM/yyyy}";
        }
    }
}
