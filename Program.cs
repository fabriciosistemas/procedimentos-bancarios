using System;
using System.Collections.Generic;

namespace DIO.Bank
{
	class Program
	{
		static List<Conta> listContas = new List<Conta>();
		static void Main(string[] args)
		{
			string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarContas();
						break;
					case "2":
						InserirConta();
						break;
					case "3":
						Transferir();
						break;
					case "4":
						Sacar();
						break;
					case "5":
						Depositar();
						break;
                    case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}
			
			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
		}

		private static void Depositar()
		{
			Console.Write("Digite o número da conta: ");
			int indiceConta = int.Parse(Console.ReadLine());

			Console.Write("Digite o valor a ser depositado: ");
			double valorDeposito = double.Parse(Console.ReadLine());

			//if (listContas[indiceConta])
			try 
			{
            	listContas[indiceConta].Depositar(valorDeposito);
			}
			catch (Exception ex)
			{
				if (ex.ToString().Contains("Index was out of range"))
					Console.WriteLine("Conta não encontrada. O depósito não pode ser realizado.");
				else
					Console.WriteLine("Erro: {0}", ex);
			}
		}

		private static void Sacar()
		{
			Console.Write("Digite o número da conta: ");
			int indiceConta = int.Parse(Console.ReadLine());

			Console.Write("Digite o valor a ser sacado: ");
			double valorSaque = double.Parse(Console.ReadLine());

			try 
			{
				listContas[indiceConta].Sacar(valorSaque);
			}
			catch (Exception ex)
			{
				if (ex.ToString().Contains("Index was out of range"))
					Console.WriteLine("Conta não encontrada. O saque não pode ser realizado.");
				else
					Console.WriteLine("Erro: {0}", ex);
			}
		}

		private static void Transferir()
		{
			Console.Write("Digite o número da conta de origem: ");
			int indiceContaOrigem = int.Parse(Console.ReadLine());

            Console.Write("Digite o número da conta de destino: ");
			int indiceContaDestino = int.Parse(Console.ReadLine());

			Console.Write("Digite o valor a ser transferido: ");
			double valorTransferencia = double.Parse(Console.ReadLine());

            listContas[indiceContaOrigem].Transferir(valorTransferencia, listContas[indiceContaDestino]);
		}

		private static void InserirConta()
		{
			Console.WriteLine("Inserir nova conta");
			bool validador = false;
			int entradaTipoConta = 0;
			double entradaCredito = 0;
			double entradaSaldo = 0;

			while (!validador)
			{
				Console.Write("Digite 1 para Conta Fisica ou 2 para Juridica: ");
				entradaTipoConta = int.Parse(Console.ReadLine());
				if (entradaTipoConta == 1 || entradaTipoConta == 2 )
					validador = true;
			}
			Console.Write("Digite o Nome do Cliente: ");
			string entradaNome = Console.ReadLine();

			try 
			{
				Console.Write("Digite o saldo inicial: ");
				entradaSaldo = double.Parse(Console.ReadLine());
			}
			catch (Exception ex)
			{
				Console.WriteLine("Insira um valor válido");
				return;
			}

			try
			{
				Console.Write("Digite o crédito: ");
				entradaCredito = double.Parse(Console.ReadLine());
			}
			catch (Exception ex)
			{
				Console.WriteLine("Insira um valor válido");
				return;
			}
			
			try
			{
				Conta novaConta = new Conta(tipoConta: (TipoConta)entradaTipoConta,
											saldo: entradaSaldo,
											credito: entradaCredito,
											nome: entradaNome);

				listContas.Add(novaConta);
			}
			catch (Exception ex)
			{
					Console.WriteLine("A conta não pode ser criada.");
					return;
			}
		}

		private static void ListarContas()
		{
			Console.WriteLine("Listar contas");

			if (listContas.Count == 0)
			{
				Console.WriteLine("Nenhuma conta cadastrada.");
				return;
			}

			for (int i = 0; i < listContas.Count; i++)
			{
				Conta conta = listContas[i];
				Console.Write("#{0} - ", i);
				Console.WriteLine(conta);
			}
		}

		private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Bank a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar contas");
			Console.WriteLine("2- Inserir nova conta");
			Console.WriteLine("3- Transferir");
			Console.WriteLine("4- Sacar");
			Console.WriteLine("5- Depositar");
            Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
	}
}
