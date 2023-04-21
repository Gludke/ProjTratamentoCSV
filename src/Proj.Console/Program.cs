using Proj.Drive.CVM;

await ExecutarPrograma();













static async Task ExecutarPrograma()
{
    var fim = false;
    while (!fim)
    {
        Console.WriteLine(
            "\n\n-------------------------MENU INICIAL-------------------------\n" +
            "\n(1) Buscar valor no arquivo CSV" +
            "\n(2) Fechar programa" +
            "\n\n--------------------------------------------------------------" +
            "\nDigite uma opção do menu:"
        );
        var opcao = Console.ReadLine();

        switch (opcao)
        {
            case "1":
                ProcessCsv driveCsv = new();
                await driveCsv.CopyCsvFromUrl();

                Console.WriteLine($"\nDigite o valor que deseja buscar no arquivo: ");
                var buscar = Console.ReadLine();

                while (string.IsNullOrEmpty(buscar))
                {
                    Console.WriteLine($"\nOpção inválida. Digite o valor que deseja buscar no arquivo: ");
                    buscar = Console.ReadLine();
                }

                if (driveCsv.FindInCsv(buscar))
                    Console.WriteLine($"\n'{buscar}' foi encontrado no arquivo");
                else
                    Console.WriteLine($"\n'{buscar}' não foi encontrado no arquivo");

                break;

            case "2":
                Console.WriteLine($"\nEncerrando sistema...");
                fim = true;
                break;

            default:
                Console.WriteLine($"\nOpção inválida. Pressione 'Enter' para continuar");
                Console.ReadLine();
                break;
        }
    }
}
