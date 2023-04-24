using Proj.Shared;

namespace Proj.Drive.CVM
{
    public class ProcessCsv
    {
        public readonly string _urlCsv;
        public readonly string _pathDocCsv;

        public ProcessCsv()
        {
            _urlCsv = "https://dados.cvm.gov.br/dados/EMISSOR_CEPAC/CAD/DADOS/cad_emissor_cepac.csv";
            _pathDocCsv = $"{Utils.GetRoot()}\\{Path.GetFileName(_urlCsv)}";
        }

        /// <summary>
        /// Baixa e salva o arquivo CSV da url informada
        /// </summary>
        public async Task CopyCsvFromUrl()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(_urlCsv);

            if (response.IsSuccessStatusCode)
            {
                var streamNewDoc = await response.Content.ReadAsStreamAsync();

                if (File.Exists(_pathDocCsv))
                {
                    var streamToTest = await response.Content.ReadAsStreamAsync();
                    //Compara o conteúdo dos dois arquivos
                    if (!Utils.CompararArquivos(streamToTest, _pathDocCsv))
                    {
                        //substitui o arquivo pelo novo quando são diferentes
                        Utils.CopyFile(streamNewDoc, $"{Path.GetFileName(_urlCsv)}");
                    }
                }
                else
                {
                    //Salva o arquivo recebido
                    Utils.CopyFile(streamNewDoc, $"{Path.GetFileName(_urlCsv)}");
                }
            }
        }

        /// <summary>
        /// Abre e lê o arquivo .csv salvo
        /// </summary>
        public bool FindInCsv(string busca)
        {
            return Utils.FindInDoc(_pathDocCsv, busca);
        }

    }
}
