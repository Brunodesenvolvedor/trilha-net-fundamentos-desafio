using DesafioFundamentos.Models;
using Microsoft.Win32.SafeHandles;
using System;

namespace DesafioFundamentos
{

    class Program
    {
        static string[] placas = new string[20];

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            decimal precoInicial = 0;
            decimal precoPorHora = 0;

            Console.Clear();
            Console.WriteLine("Seja bem vindo ao sistema de estacionamento!\r\nDigite o preço inicial:");
             try
            {
            precoInicial = Convert.ToDecimal(Console.ReadLine());
            }
            catch (FormatException)
            {
            Console.WriteLine("Erro: o valor digitado não é um valor numérico válido para o preço inicial. Tente novamente.");
            return; // Encerra o programa se a entrada não puder ser convertida
            }
            
            Console.Clear();
            Console.WriteLine("Agora digite o preço por hora, por favor:");
            try
            {
            precoPorHora = Convert.ToDecimal(Console.ReadLine());
            }
            catch (FormatException)
            {
            Console.WriteLine("Erro: o valor digitado não é um valor numérico válido para o preço por hora. Tente novamente.");
            return; 
            }
            Console.Clear();


            // Instancia a classe Estacionamento, já com os valores obtidos anteriormente
            Estacionamento es = new Estacionamento(precoInicial, precoPorHora);

            string opcao = string.Empty;
            bool exibirMenu = true;

            // Realiza o loop do menu
            while (exibirMenu)
            {
                Console.Clear();
                Console.WriteLine("Digite o número da opção desejada:");
                Console.WriteLine("");
                Console.WriteLine("1 - Cadastrar veículo");
                Console.WriteLine("2 - Remover veículo");
                Console.WriteLine("3 - Listar veículos");
                Console.WriteLine("4 - Encerrar");

                switch (Console.ReadLine())
                {
                    case "1":
                        AdicionarVeiculo();
                        break;

                    case "2":
                        ListarVeiculos();
                        Console.WriteLine ("");
                        RemoverVeiculo(precoPorHora, precoInicial);
                        break;

                    case "3":
                        ListarVeiculos();
                        Console.WriteLine("Pressione enter para continuar:");
                        Console.ReadLine();
                        break;

                    case "4":
                        exibirMenu = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
                
            } 

        }

        static void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo a ser adicionada:");

            int posicaovazia = Array.IndexOf(placas, null); // Encontra a primeira posição vazia dentro do array e gera um índice. Se não encontrar, retorna -1.

            if (posicaovazia != -1) // Verifica se uma posição vazia foi encontrada
            {
                placas[posicaovazia] = Console.ReadLine();
                Console.WriteLine("Placa {0} adicionada.", placas[posicaovazia]);
            }
            else
            {
            Console.WriteLine("Impossível adicionar. Não existem posições vazias no momento.");
            }
            Console.WriteLine("Pressione enter para continuar:");
            Console.ReadLine();
        }

        static void RemoverVeiculo(decimal precoPorHora, decimal precoInicial)
        {
        
            Console.WriteLine ("Digite a placa a ser removida:");
            string placaAremover = Console.ReadLine();
            string apresentaPlaca = placaAremover;


            int verificaPosicao = Array.IndexOf(placas, placaAremover);
            int horasEstacionado;

            if (verificaPosicao != -1) 
            {
                placas[verificaPosicao] = null;
                Console.WriteLine ("Digite a quantidade de horas que o veículo permaneceu estacionado");
                while (!int.TryParse(Console.ReadLine(), out horasEstacionado))
                {
                    Console.WriteLine("Valor inválido. Por favor, digite um número inteiro para as horas estacionadas:");
                }
                decimal pagamento = horasEstacionado * precoPorHora + precoInicial;
                Console.WriteLine("Placa {0} removida.", apresentaPlaca);
                Console.WriteLine("O valor total a ser pago é de R${0}", pagamento);
                Console.WriteLine("Pressione enter para continuar:");
                Console.ReadLine();
            }
            else
            {
            Console.WriteLine("");
            Console.WriteLine("Placa não encontrada. Impossível efetuar a remoção.");
            Console.WriteLine("Pressione enter para continuar:");
            Console.ReadLine();
            }
        }

        static void ListarVeiculos()
        {
        bool nadacadastrado = true; // Inicializa como verdadeiro, pois ainda não encontramos nenhum veículo cadastrado

        Console.WriteLine("Os veículos cadastrados são: ");
        foreach(string leitor in placas)
        {
            if (!string.IsNullOrEmpty(leitor)) 
        {
            Console.WriteLine(leitor);
            nadacadastrado = false; // Define como falso se pelo menos um veículo for encontrado
        }
        }

        if (nadacadastrado)
        {
        Console.WriteLine("Nenhum");
        }
        }
    }
}



