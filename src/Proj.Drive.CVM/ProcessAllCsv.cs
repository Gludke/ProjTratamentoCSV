using Proj.Shared;
using System.IO.Compression;

namespace Proj.Drive.CVM;

public class ProcessAllCsv
{
    private readonly Dictionary<string, string> _factory;

    public ProcessAllCsv()
    {
        _factory = new Dictionary<string, string>
        {
            { "https://dados.cvm.gov.br/dados/ADM_CART/CAD/DADOS/cad_adm_cart.zip", "cad_adm_cart_pj.csv" },
            { "https://dados.cvm.gov.br/dados/INTERMED/CAD/DADOS/cad_intermed.zip", "cad_intermed.csv" },
            { "https://dados.cvm.gov.br/dados/CONSULTOR_VLMOB/CAD/DADOS/cad_consultor_vlmob.zip", "cad_consultor_vlmob_pj.csv" },
            { "https://dados.cvm.gov.br/dados/AUDITOR/CAD/DADOS/cad_auditor.zip", "cad_auditor_pj.csv" }
        };

        //ValidatePathDirectory();
    }


    public async Task<string> Process()
    {
        try
        {
            var httpClient = new HttpClient();
            var responses = new List<HttpResponseMessage>();

            foreach (var url in _factory.Keys)
            {
                responses.Add(await httpClient.GetAsync(url));
            }

            //Percorre cada .zip baixado
            responses.Where(r => r.IsSuccessStatusCode).ToList().ForEach(async r => {
                using var zipStream = await r.Content.ReadAsStreamAsync();

                using var zip = new ZipArchive(zipStream, ZipArchiveMode.Read);

                //URL de download do zip
                var urlZip = r.RequestMessage!.RequestUri!.ToString();
                var nameDoc = _factory[urlZip];

                //Pega o arquivo csv especificado pela 'key'
                var csv = zip.GetEntry(nameDoc);

                if (csv != null)
                {
                    //Sobrescreve os arquivos antigos
                    Utils.CopyFile(csv.Open(), $"{nameDoc}");
                }

                zip.Dispose();
                zipStream.Dispose();
            });
        }
        catch (Exception ex)
        {
            throw;
        }

        return "";
    }


    //private void ValidatePathDirectory()
    //{
    //    if (!Directory.Exists(PathDirectory))
    //    {
    //        Directory.CreateDirectory(PathDirectory);
    //    }
    //}



}
