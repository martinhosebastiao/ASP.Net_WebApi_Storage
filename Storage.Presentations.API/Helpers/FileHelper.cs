using System;
namespace Storage.Presentations.API.Helpers
{
    public abstract class FileHelper
    {
        /// <summary>
        /// Obter um possivel nome unico para um ficheiro, usando o nome completo
        /// </summary>
        /// <param name="fileName">Caminho completo do ficheiro</param>
        /// <returns>Retorna um possivel nome unico</returns>
        public static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);

            var fileType = string.Concat(".", GetFileType(fileName));
            var fileNewName = string.Concat(Guid.NewGuid().ToString().AsSpan(0, 8), DateTime.UtcNow.Ticks.ToString(), Guid.NewGuid().ToString().AsSpan(0, 8), fileType);

            return fileNewName.ToLower();
        }

        /// <summary>
        /// Obter a extensão de um ficheiro atravez do nome completo
        /// Exemplo: C:\app\curriculo.pdf
        /// </summary>
        /// <param name="path">Caminho completo do ficheiro</param>
        /// <returns>Retorna a extensão ou tipo do ficheiro. Exemplo: pdf</returns>
        public static string GetFileType(string path)
        {
            var fileType = Path.GetExtension(path).Replace(".", "").ToLower();

            return fileType;
        }
    }
}

