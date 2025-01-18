using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;

Hotel hotel = new Hotel();

var exibirMenu = true;
Console.WriteLine("Seja bem vindo ao Hotel BM !!");

while (exibirMenu)
{
    Console.WriteLine("Oque deseja fazer ?");
    Console.WriteLine("1 - Fazer checking");
    Console.WriteLine("2 - Fazer checkout");
    Console.WriteLine("3 - Listar reservar");
    Console.WriteLine("4 - Encerrar programa");
    switch (Console.ReadLine())
    {
        case "1":
            hotel.FazerReserva();
            break;

        case "2":
            hotel.FazerCheckout();
            break;
        case "3":
            hotel.ListarReservas();
            break;
        case "4":
            exibirMenu = false;
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    Console.Clear();
}