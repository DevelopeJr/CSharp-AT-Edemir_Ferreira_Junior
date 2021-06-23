using Domain.Interfaces;
using Domain.Entities;
using Infraestrutura.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerServiceManagerProduto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
                return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IProdutoRepository>(x => new ProdutoDictionaryRepository());
                    services.AddHostedService<Worker>();
                });
        }

        private static IProdutoRepository DefinirRepositorioInstancia(string configRepositorio)
        {
            if (configRepositorio == "ProdutoDictionaryRepository")
                return new ProdutoDictionaryRepository();
            else if (configRepositorio == "ProdutoListRepository")
                return new ProdutoListRepository();
            else
                throw new NotImplementedException("Não existe implementa��o de reposit�rio para configura��o existente.");
        }

    }
}
