using Microsoft.AspNetCore.Hosting;
using Proj.Drive.CVM;
using Proj.Drive.FormCVM;

await ExecutarPrograma();













static async Task ExecutarPrograma()
{
    var fim = false;
    while (!fim)
    {
        Console.WriteLine(
            "\n\n-------------------------MENU INICIAL-------------------------\n" +
            "\n(1) Atualizar base de dados" +
            "\n(2) Buscar valor na base de dados" +
            "\n(0) Fechar programa" +
            "\n\n--------------------------------------------------------------" +
            "\nDigite uma opção do menu:"
        );
        var opcao = Console.ReadLine();

        switch (opcao)
        {
            case "1":
                Console.WriteLine($"\nATUALIZANDO BASE...");

                await new ProcessAllCsv().Process();

                Console.WriteLine($"\nBASE ATUALIZADA COM SUCESSO");

                break;

            case "2":
                var driveCsv2 = new ProcessCsv();

                Console.WriteLine($"\nDigite o CNPJ que deseja buscar no arquivo: ");
                var cnpj = Console.ReadLine();
                Console.WriteLine($"\nInforme qual o 'Tipo de Prestador' que deseja buscar para esse CNPJ: ");
                var tpPrest = Console.ReadLine();

                while (string.IsNullOrEmpty(cnpj) || string.IsNullOrEmpty(tpPrest))
                {
                    Console.WriteLine($"\nOPÇÃO INVÁLIDA");
                    Console.WriteLine($"\nDigite o CNPJ que deseja buscar no arquivo: ");
                    cnpj = Console.ReadLine();
                    Console.WriteLine($"\nInforme qual o 'Tipo de Prestador' que deseja buscar para esse CNPJ: ");
                    tpPrest = Console.ReadLine();
                }

                Console.WriteLine($"\nBUSCANDO...");

                if (driveCsv2.FindInCsv(cnpj, tpPrest))
                    Console.WriteLine($"\nSUCESS: '{cnpj}' do tipo '{tpPrest}' foi encontrado no arquivo");
                else
                    Console.WriteLine($"\nFAIL: '{cnpj}' do tipo '{tpPrest}' não foi encontrado no arquivo");

                break;

            case "0":
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
