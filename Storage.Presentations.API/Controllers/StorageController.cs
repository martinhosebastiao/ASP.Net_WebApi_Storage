using Microsoft.AspNetCore.Mvc;
using Storage.Presentations.API.Helpers;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storage.Presentations.API.Controllers
{
    [Route("v1/[controller]")]
    public class StorageController : Controller
    {

        // GET api/values/5
        [HttpGet("download/{name}")]
        public IActionResult Download(string name)
        {
            return Ok("Em breve");

        }

        // POST api/values
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                //Obter um nome unico para o ficheiro
                var fileUniqueName = FileHelper.GetUniqueFileName(file.FileName);

                //Obter o caminho do directorio aonde a API esta a ser executado
                var currentPath = Environment.CurrentDirectory;

                //Obter a extensão do ficheiro
                var fileType = FileHelper.GetFileType(file.Name);

                //Obter o caminho aonde será armazenado o ficheiro carregado
                var uploadPath = Path.Combine(currentPath, "Storages", fileType);

                //Obter o caminho completo inclusive do ficheiro a ser armazedo
                var filePath = Path.Combine(uploadPath, fileUniqueName);

                var directory = Path.GetDirectoryName(filePath) ?? string.Empty;

                //Criar um directorio para armazenar o ficheiro caso não existir
                Directory.CreateDirectory(directory);

                //Montar um fluxo do ficheiro a ser criado com base no que foi carregado
                FileStream fileStream = new(filePath, FileMode.Create, FileAccess.ReadWrite);

                //Cria o nome ficheiro com base no que foi carregado
                await file.CopyToAsync(fileStream);

                //Verificar se o ficheiro foi criado
                bool isFileExist = System.IO.File.Exists(filePath);


                JsonResult result = new(fileUniqueName)
                {
                    //Se o upload foi bem sucedido 201 caso contrario 400
                    StatusCode = isFileExist ? 201 : 400
                };

                return result;
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}

