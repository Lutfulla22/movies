using Microsoft.AspNetCore.Http;

namespace movies.Models
{
    public class FileUpload
    {
        public IFormFile files { get; set; }

    }
}