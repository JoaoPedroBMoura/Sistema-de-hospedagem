using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DesafioProjetoHospedagem.Models
{
    public class Hotel
    {
        List<Reserva> reservas = new List<Reserva>();

        // Cria a suíte 
        Suite suitePremium = new Suite(tipoSuite: "Premium", capacidade: 2, valorDiaria: 100);
        Suite suiteBlack = new Suite("Black", 2, 70);
        Suite Familia = new Suite("Familia", 4, 50);
        Suite viajante = new Suite("Viajante", 2, 30);

        int idHospede = 1;
        int idReservaLocal = 1;

        private List<Pessoa> CadastrarHospede()
        {
            List<Pessoa> hospedes = new List<Pessoa>();
            
            bool ativo = true;

            while(ativo)
            {
                Pessoa hospede = new Pessoa();

                    Console.WriteLine("\r\nPor favor informe o primeiro nome do novo hóspede");
                    var nome = Console.ReadLine();

                    Console.WriteLine("Por favor informe o sobrenome do novo hóspede");
                    var sobrenome = Console.ReadLine();

                    hospede.Nome = nome;
                    hospede.Sobrenome = sobrenome;
                    hospede.NumeroDeHospede = idHospede;

                    hospedes.Add(hospede);
                    Console.WriteLine("\r\nHóspede cadastrado");

                    idHospede++;

                    Console.WriteLine("\r\nDeseja cadastrar um novo hóspede ? (1- Sim, 2- Não)");
    
                    if(Console.ReadLine() == "2"){
                        ativo = false;
                    }
                }
            
            return hospedes;
        }

        public void FazerReserva()
        {
            // Cria uma nova reserva, passando a suíte e os hóspedes
            int diasReservados;


            Reserva reserva = new Reserva();

            Console.Clear();
            Console.WriteLine("Qual suite você deseja ?");
            Console.WriteLine("Digite a sua opção:");
            Console.WriteLine("1 - Viajante | Diária 30 reais | 2 pessoas");
            Console.WriteLine("2 - Família  | Diária 60 reais | 4 pessoas");
            Console.WriteLine("3 - Black    | Diária 70 reais | 2 pessoas");
            Console.WriteLine("4 - Premium  | Diária 100 reais| 2 pessoas");

            switch (Console.ReadLine())
            {
                case "1":
                    reserva.CadastrarSuite(viajante);

                    break;

                case "2":
                    reserva.CadastrarSuite(Familia);
                    break;

                case "3":
                    reserva.CadastrarSuite(suiteBlack);
                    break;

                case "4":
                    reserva.CadastrarSuite(suitePremium);
                    break;

                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }

            Console.WriteLine("\r\nQuantos dias de hospedagem ?");
            diasReservados = Convert.ToInt32(Console.ReadLine());

            reserva.DiasReservados = diasReservados;

            

            Console.WriteLine("\r\nPor favor cadastre os hóspedes:\r\n");

            reserva.CadastrarHospedes(CadastrarHospede());

            reserva.IdReserva = idReservaLocal;

            idReservaLocal++;

            reservas.Add(reserva);
        }
        
        // Exibe a quantidade de hóspedes e o valor da diária
        public void FazerCheckout()
        {
            int idDaReserva, indiceDaReserva;
            decimal valorDiaria;
            bool controleMenu = true;

            if(reservas.Count != 0){
                foreach(var reservasLocais in reservas){
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine($"Reserva: {reservasLocais.IdReserva}| Hospedes: {reservasLocais.ObterQuantidadeHospedes()}");
                    reservasLocais.ApresentaHóspedes();
                }
            }

            Console.WriteLine("\r\nQual o id da sua reserva ?");
            idDaReserva = Convert.ToInt32(Console.ReadLine());

            indiceDaReserva = reservas.FindIndex(r => r.IdReserva == idDaReserva);

            if(indiceDaReserva != -1){
                Reserva reservaAtual = reservas[indiceDaReserva];

                valorDiaria = reservaAtual.CalcularValorDiaria();

                Console.WriteLine($"O valor total a se pagar é: R${valorDiaria}");

                do{
                    Console.WriteLine("Valor pago ?");
                    Console.WriteLine("1 - Sim");
                    Console.WriteLine("2 - Não");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            if (indiceDaReserva > -1)
                            {
                                reservas.RemoveAt(indiceDaReserva);
                                Console.WriteLine("Checkout feito com sucesso");
                                controleMenu = false;
                            }
                            break;
                        case "2":
                            Console.WriteLine("Chamando o Gerente...");
                            break;
                    }
                } while (controleMenu);
            }else{
                Console.WriteLine("Reserva não encontrada.");
                Console.ReadLine();
            }
            
           
        }
    
        public void ListarReservas()
        {
            if (reservas.Count != 0) {
                foreach (var reservasLocais in reservas) {
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine($"Reserva: {reservasLocais.IdReserva}| Hospedes: {reservasLocais.ObterQuantidadeHospedes()}");
                    Console.WriteLine($"Lista de hóspedes: \r\n");
                    reservasLocais.ApresentaHóspedes();
                }

                Console.WriteLine("\r\nAperte enter para seguir");
                Console.ReadLine();
            } else {
                Console.WriteLine("Problema com a lista de hóspedes");
            }
        }
    }
}