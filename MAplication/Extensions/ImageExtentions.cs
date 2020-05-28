using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace UI.Extentions
{
    public class ImageExtentions
    {
        IHostingEnvironment _hostingEnvironment;
        public ImageExtentions(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> ImageUploader(IFormFile pic,string path)
        {
            string result = string.Empty;
            if (pic != null && pic.Length > 0)
            {
                var file = pic;
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, path);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse
                        (file.ContentDisposition).FileName.Trim('"');

                    System.Console.WriteLine(fileName);
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        result = file.FileName;
                    }
                }
            }
            return result;
        }
    }
}
