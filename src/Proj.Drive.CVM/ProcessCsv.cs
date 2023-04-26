using Proj.Shared;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

namespace Proj.Drive.CVM
{
    public class ProcessCsv
    {
        public readonly string _urlZipAdmFid_GestRec;
        public readonly string _pathCsvAdmFid_GestRec;

        public ProcessCsv()
        {
            _urlZipAdmFid_GestRec = "https://dados.cvm.gov.br/dados/ADM_CART/CAD/DADOS/cad_adm_cart.zip";
            _pathCsvAdmFid_GestRec = $"{Utils.GetRoot()}\\{Path.GetFileNameWithoutExtension(_urlZipAdmFid_GestRec)}.csv";
        }

        public async Task CopyCsvFromUrlZip()
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(_urlZipAdmFid_GestRec);

                if (response.IsSuccessStatusCode)
                {
                    using var zipStream = await response.Content.ReadAsStreamAsync();

                    using var zip = new ZipArchive(zipStream, ZipArchiveMode.Read);

                    //Pega o arquivo csv especificado
                    var csv = zip.GetEntry("cad_adm_cart_pj.csv");

                    if (csv != null)
                    {
                        //Salva o csv recebido
                        Utils.CopyFile(csv.Open(), $"{Path.GetFileName(_pathCsvAdmFid_GestRec)}");
                    }

                    zip.Dispose();
                    zipStream.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Abre e lê o arquivo .csv salvo
        /// </summary>
        public bool FindInCsv(string cnpj, string tipoPrestador)
        {
            var find = false;
            cnpj = Regex.Replace(cnpj, @"[-./]", "");

            try
            {
                using var reader = new StreamReader(_pathCsvAdmFid_GestRec, Encoding.GetEncoding("iso-8859-1"));

                while (!reader.EndOfStream)
                {
                    var linha = reader.ReadLine();
                    if (linha == null)
                        continue;

                    //Pega o valor da coluna CNPJ
                    var columnCnpj = linha.Split(';')[0];
                    columnCnpj = Regex.Replace(columnCnpj, @"[-./]", "");

                    if (columnCnpj.Contains(cnpj))
                    {
                        linha = linha!.RemoveAccents().ToUpper();
                        if (linha.Contains(tipoPrestador!.RemoveAccents().ToUpper())
                            && linha.Contains("FUNCIONAMENTO NORMAL"))
                        {
                            find = true;
                        }
                    }
                }

                reader.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return find;
        }













        /// <summary>
        /// Baixa e salva o arquivo CSV da url informada
        /// </summary>
        public async Task CopyCsvFromUrlZipWithTeste()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(_urlZipAdmFid_GestRec);

            if (response.IsSuccessStatusCode)
            {
                using var zipStream = await response.Content.ReadAsStreamAsync();
                
                using var zip = new ZipArchive(zipStream, ZipArchiveMode.Read);

                //Pega o arquivo csv especificado
                var csv = zip.GetEntry("cad_adm_cart_pj.csv");

                if(csv != null)
                {
                    using var csvStream = csv.Open();
                    //var streamNewDoc = await response.Content.ReadAsStreamAsync();

                    if (File.Exists(_pathCsvAdmFid_GestRec))
                    {
                        //var streamToTest = await response.Content.ReadAsStreamAsync();
                        var csvStreamToTest = csv.Open();

                        //Compara o conteúdo dos dois arquivos
                        //if (!Utils.CompararArquivos(streamToTest, _pathCsvAdmFid_GestRec))
                        //{
                        //    //substitui o arquivo pelo novo quando são diferentes
                        //    Utils.CopyFile(streamNewDoc, $"{Path.GetFileName(_urlZipAdmFid_GestRec)}");
                        //}
                    }
                    else
                    {
                        //Salva o arquivo recebido
                        //Utils.CopyFile(streamNewDoc, $"{Path.GetFileName(_urlZipAdmFid_GestRec)}");
                    }
                    csvStream.Dispose();
                }

                zip.Dispose();
                zipStream.Dispose();
            }
        }

        

    }
}
