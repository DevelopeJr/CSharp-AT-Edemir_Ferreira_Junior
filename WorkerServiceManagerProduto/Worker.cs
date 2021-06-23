using Domain.Interfaces;
using System.Globalization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Domain.Entities;

namespace WorkerServiceManagerProduto
{
    public class Worker : BackgroundService
    {
        private IProdutoRepository _repository;

        const string _pressioneQualquerTecla = "Pressione qualquer tecla para exibir o menu principal ...";

        public Worker(IProdutoRepository repository)
        {
            _repository = repository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            Padroes();

            string opcao;

            do
            {
                MenuPrincipal();

                opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    CadastroProduto();
                }
                else if (opcao == "2")
                {
                    AlteracaoProduto();
                }
                else if (opcao == "3")
                {
                    ApagarProduto();
                }
                else if (opcao == "4")
                {
                    Procurar();
                }
                else if (opcao == "5")
                {
                    Listar();
                }
                else if (opcao == "6")
                {
                    Rodape();
                    Environment.Exit(0);
                }

            } while (opcao != "6");
        }
        private void Padroes()
        {
            Console.Title = "ML - Gerenciador de produtos";

        }
        private void MenuPrincipal()
        {
            Console.WriteLine("ML - Gerenciador de produtos");
            Console.WriteLine("1 - Adicionar Produto:");
            Console.WriteLine("2 - Alterar dados dos Produtos:");
            Console.WriteLine("3 - Apagar um Produto:");
            Console.WriteLine("4 - Pesquisar Produto:");
            Console.WriteLine("5 - Listar os �ltimos 5 Produtos cadastrados:");
            Console.WriteLine("6 - Sair do ML");
            Console.WriteLine("\nEscolha uma das op��es acima: ");
        }
        private void Rodape()
        {
            Console.WriteLine("Finalizando ML");

        }
        private void OpcaoInvalida()
        {
            Console.WriteLine($"Opção inválida! Escolha uma opção válida. {_pressioneQualquerTecla}");
            Console.ReadKey();
        }
        private void Procurar()
        {
            Console.Clear();

            Console.WriteLine("Informe nome do produto que deseja pesquisar:");
            var busca = Console.ReadLine();

            var produtoEncontrados = _repository.Pesquisar(busca);

            if (produtoEncontrados.Count > 0)
            {
                Console.WriteLine("Selecione uma das op��es abaixo para visualizar os dados dos produtos encontrados:");
                for (var index = 0; index < produtoEncontrados.Count; index++)
                    Console.WriteLine($"{index} - {produtoEncontrados[index]._nomeProduto}");

                if (!ushort.TryParse(Console.ReadLine(), out var indexAExibir) || indexAExibir >= produtoEncontrados.Count)
                {
                    Console.WriteLine($"Op��o inv�lida! {_pressioneQualquerTecla}");
                    Console.ReadKey();
                }

                if (indexAExibir < produtoEncontrados.Count)
                {
                    var produto = produtoEncontrados[indexAExibir];

                    Console.Clear();

                    Console.WriteLine("Dados do produto qu�mico:");

                    Console.WriteLine("");

                    Console.WriteLine($"Nome do produto: {produto._nomeProduto}");

                    Console.WriteLine($"Nome carateristicas do produto: {produto._caracteristicasProduto}");
                    
                    Console.WriteLine($"Quantidade produzida do produto: {produto._quantidade}");

                    Console.WriteLine($"Data de fabrição: {produto._dataFabricacao:dd/MM/yyyy}");

                    produto.Validade(produto._dataFabricacao);

                    Console.WriteLine("");

                    Console.Write(_pressioneQualquerTecla);

                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();

                Console.WriteLine($"Não foi encontrado nenhuma produto químico! {_pressioneQualquerTecla}");

                Console.ReadKey();
            }
        }
        private void CadastroProduto()
        {
            Console.Clear();

            Console.WriteLine("Informe ID do produto:");
            var IdProduto = Console.ReadLine();
                        
            Console.WriteLine("Informe o nome do produto:");
            var NomeProduto = Console.ReadLine();

            Console.WriteLine("Informe caracteristicas do produto:");
            var CaracteristicasProduto = Console.ReadLine();

            Console.WriteLine("Informe a quantidade do produto:");
            if (!int.TryParse(Console.ReadLine(), out var quantidade))
            {
                Console.WriteLine($"Dado inválido para quantidade! Dados descartados! {_pressioneQualquerTecla}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Informe a data de fabrição (formato dd/MM/yyyy):");
                if (!DateTime.TryParse(Console.ReadLine(), out var dataFabricacao))
                {
                    Console.WriteLine($"Data inválida! Dados descartados! {_pressioneQualquerTecla}");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Os dados estão corretos?");
                    Console.WriteLine($"Nome do produto: {NomeProduto}");
                    Console.WriteLine($"Data de fabrição: {dataFabricacao:dd/MM/yyyy}");
                    Console.WriteLine("1 - Sim \n2 - N�o");

                    var opcaoParaAdicionar = Console.ReadLine();
                    if (opcaoParaAdicionar == "1")
                    {
                        _repository.Adicionar(new ProdutoModel( NomeProduto, CaracteristicasProduto, quantidade, dataFabricacao));

                        Console.WriteLine($"Dados adicionados com sucesso! {_pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                    else if (opcaoParaAdicionar == "2")
                    {
                        Console.WriteLine($"Dados descartados! {_pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"Õpção inválida! {_pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                }
            }
        }
        private void AlteracaoProduto()
        {
            Console.Clear();

            Console.WriteLine("Informe nome do produto que deseja editar:");
            var busca = Console.ReadLine();

            var produtoEncontrados = _repository.Pesquisar(busca);

            if (produtoEncontrados.Count > 0)
            {
                Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados dos produtos químicos encontrados:");
                for (var index = 0; index < produtoEncontrados.Count; index++)
                    Console.WriteLine($"{index} - {produtoEncontrados[index]._nomeProduto}");

                if (!ushort.TryParse(Console.ReadLine(), out var indexAExibir) || indexAExibir >= produtoEncontrados.Count)
                {
                    Console.WriteLine($"Opção inválida! {_pressioneQualquerTecla}");
                    Console.ReadKey();
                }

                if (indexAExibir < produtoEncontrados.Count)
                {
                    var produto = produtoEncontrados[indexAExibir];

                    Console.Clear();

                    Console.WriteLine("Dados do produto:");

                    Console.WriteLine("");

                    Console.WriteLine($"Nome do produto: {produto._nomeProduto}");

                    Console.WriteLine($"Características do produto: {produto._caracteristicasProduto}");

                    Console.WriteLine($"Quantidade produzida do produto: {produto._quantidade}");

                    Console.WriteLine($"Data de fabrição: {produto._dataFabricacao:dd/MM/yyyy}");

                    produto.Validade(produto._dataFabricacao);

                    Console.WriteLine("");

                    Console.WriteLine("Informe o novo nome do produto:");
                    var nomeProduto = Console.ReadLine();

                    Console.WriteLine("Informe a característica do produto:");
                    var caracteristicasProduto = Console.ReadLine();


                    Console.WriteLine("Informe a nova quantidade do produto:");
                    if (!int.TryParse(Console.ReadLine(), out var quantidade))
                    {
                        Console.WriteLine($"Dado inválido para quantidade! Dados descartados! {_pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Informe a data de fabricação (formato dd/MM/yyyy):");
                        if (!DateTime.TryParse(Console.ReadLine(), out var dataFabricacao))
                        {
                            Console.WriteLine($"Data inválida! Dados descartados! {_pressioneQualquerTecla}");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Os dados estão corretos?");
                            Console.WriteLine($"Novo produto: {nomeProduto}");
                            Console.WriteLine($"Data de fabrição: {dataFabricacao:dd/MM/yyyy}");
                            Console.WriteLine("1 - Sim \n2 - N�o");

                            var opcaoParaEditar = Console.ReadLine();
                            if (opcaoParaEditar == "1")
                            {
                                _repository.Editar(produto._idProduto, nomeProduto, caracteristicasProduto, quantidade, dataFabricacao);

                                Console.WriteLine($"Dados alterados com sucesso! {_pressioneQualquerTecla}");
                                Console.ReadKey();
                            }
                            else if (opcaoParaEditar == "2")
                            {
                                Console.WriteLine($"Dados descartados! {_pressioneQualquerTecla}");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine($"Opção inválida! {_pressioneQualquerTecla}");
                                Console.ReadKey();
                            }
                        }
                    }
                }
            }
        }
        private void ApagarProduto()
        {
            Console.Clear();

            Console.WriteLine("Informe nome do produto que deseja apagar:");
            var busca = Console.ReadLine();

            var produtoEncontrados = _repository.Pesquisar(busca);

            if (produtoEncontrados.Count > 0)
            {
                Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados dos produtos encontrados:");
                for (var index = 0; index < produtoEncontrados.Count; index++)
                    Console.WriteLine($"{index} - {produtoEncontrados[index]._nomeProduto}");

                if (!ushort.TryParse(Console.ReadLine(), out var indexAExibir) || indexAExibir >= produtoEncontrados.Count)
                {
                    Console.WriteLine($"Opção inválida! {_pressioneQualquerTecla}");
                    Console.ReadKey();
                }

                if (indexAExibir < produtoEncontrados.Count)
                {
                    var produto = produtoEncontrados[indexAExibir];

                    Console.Clear();

                    Console.WriteLine("Dados do produto:");

                    Console.WriteLine("");

                    Console.WriteLine($"Nome do produto: {produto._nomeProduto}");
                                        
                    Console.WriteLine($"Quantidade produzida do produto: {produto._quantidade}");

                    Console.WriteLine($"Data de fabricação: {produto._dataFabricacao:dd/MM/yyyy}");

                    produto.Validade(produto._dataFabricacao);

                    Console.WriteLine($"Deseja apagar do produto {produto._nomeProduto}?");
                    Console.WriteLine("1 - Sim \n2 - N�o");
                    var opcaoDeletar = Console.ReadLine();
                    if (opcaoDeletar == "1")
                    {
                        _repository.Deletar(produto);

                        Console.WriteLine($"Dados apagados com sucesso! {_pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                    else if (opcaoDeletar == "2")
                    {
                        Console.WriteLine($"Operação Cancelada! {_pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"OpOperação inválida! {_pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                }
            }
        }
        private void Listar()
        {
            Console.WriteLine("Ultimos Cinco Produtos Cadastrados");

            var produtos = _repository.Pesquisar("");

            var i = 1;

            foreach (var produto in produtos.TakeLast(5).ToList())
            {
                Console.WriteLine($"{i} - Nome: {produto._nomeProduto} ------- Data de Fabricação: {produto._dataFabricacao:dd/MM/yyyy}");
                i++;
            }
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}