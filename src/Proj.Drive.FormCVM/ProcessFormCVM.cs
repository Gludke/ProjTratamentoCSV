using HtmlAgilityPack;
using Proj.Shared;

namespace Proj.Drive.FormCVM
{
    public class ProcessFormCVM
    {
        public async Task<string> BuscarImgCodCaptchaCvm()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://sistemas.cvm.gov.br/asp/cvmwww/cadastro/CadListPartic.asp");

            if (response.IsSuccessStatusCode)
            {
                // Carrega o conteúdo da página em um objeto HtmlDocument
                var content = await response.Content.ReadAsStringAsync();
                var doc = new HtmlDocument();
                doc.LoadHtml(content);

                // Selecionar o elemento img que contém o código de 4 dígitos
                var img = doc.DocumentNode.SelectSingleNode("//img[@src='/asp/cvmwww/captcha/aspcaptcha.asp']");

                if (img != null)
                {
                    // Obter o valor do atributo src
                    var src = $"https://sistemas.cvm.gov.br{img.Attributes["src"].Value}";
                    var fileName = "codigo.jpg";

                    // Salvar a imagem em um arquivo
                    Utils.CopyFile(await httpClient.GetByteArrayAsync(src), fileName);

                    //Caminho da imagem salva
                    return $"{Utils.GetRoot()}\\{fileName}";
                }
                else
                {
                    Console.WriteLine($"Imagem não encontrada");
                    return $"";
                }
            }
            else
            {
                Console.WriteLine($"Erro ao enviar a solicitação: {response.StatusCode}");
                return $"";
            }
        }



    }
}