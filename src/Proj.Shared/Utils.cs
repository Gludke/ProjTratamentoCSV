namespace Proj.Shared
{
    public static class Utils
    {
        public static readonly string _rootDirectory = "C:\\Users\\Guilherme\\Desktop\\Dev Projects\\ProjTratamentoCSV\\ProjTratamentoCSV\\docs";

        public static string GetRoot()
        {
            return _rootDirectory;
        }

        public static string ConvertToBase64(Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            return Convert.ToBase64String(bytes);
            //return new MemoryStream(Encoding.UTF8.GetBytes(base64));
        }

        public static byte[] ConvertToBytes(Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            return bytes;
        }

        public static string CopyFile(Stream fileStream, string fileName)
        {
            byte[] fileBytes = ConvertToBytes(fileStream);

            var pathFile = $"{GetRoot()}\\{fileName}";

            File.WriteAllBytes(pathFile, fileBytes);

            return pathFile;
        }

        public static string CopyFile(byte[] fileBytes, string fileName)
        {
            var pathFile = $"{GetRoot()}\\{fileName}";

            File.WriteAllBytes(pathFile, fileBytes);

            return pathFile;
        }


        public static bool FindInDoc(string pathDoc, string busca)
        {
            var find = false;
            using (var reader = new StreamReader(pathDoc))
            {
                while (!reader.EndOfStream)
                {
                    var linha = reader.ReadLine();
                    if (linha?.ToLower().Contains(busca.ToLower()) ?? false)
                        find = true;
                }
            }

            return find;
        }

        /// <summary>
        /// Retorna 'true' se forem iguais; 'false' se não forem
        /// </summary>
        public static bool CompararArquivos(string caminhoArquivo1, string caminhoArquivo2)
        {
            var linhasArquivo1 = LerLinhas(caminhoArquivo1);
            var linhasArquivo2 = LerLinhas(caminhoArquivo2);

            if (linhasArquivo1.Count != linhasArquivo2.Count)
            {
                return false;
            }

            for (int i = 0; i < linhasArquivo1.Count; i++)
            {
                if (linhasArquivo1[i] != linhasArquivo2[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Retorna 'true' se forem iguais; 'false' se não forem
        /// </summary>
        public static bool CompararArquivos(Stream streamArquivo1, string caminhoArquivo2)
        {
            var linhasArquivo1 = LerLinhas(streamArquivo1);
            var linhasArquivo2 = LerLinhas(caminhoArquivo2);

            if (linhasArquivo1.Count != linhasArquivo2.Count)
            {
                return false;
            }

            for (int i = 0; i < linhasArquivo1.Count; i++)
            {
                if (linhasArquivo1[i] != linhasArquivo2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static List<string> LerLinhas(Stream stream)
        {
            var linhas = new List<string>();

            var reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                var linha = reader.ReadLine();
                linhas.Add(linha);
            }

            return linhas;
        }

        public static List<string> LerLinhas(string pathDoc)
        {
            var linhas = new List<string>();

            using (var reader = new StreamReader(pathDoc))
            {
                while (!reader.EndOfStream)
                {
                    var linha = reader.ReadLine();
                    linhas.Add(linha);
                }
            }

            return linhas;
        }


    }
}
