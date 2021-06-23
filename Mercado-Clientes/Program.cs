using System;
using Infraestrutura.Repositories;

namespace Mercado_Clientes
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new ProdutoListRepository();
            
            Console.WriteLine("Hello World!");
        }
    }
}
