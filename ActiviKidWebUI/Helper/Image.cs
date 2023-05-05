using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ActiviKidWebUI.Helper
{
    public class Image
    {

        [Obsolete]
        public static string Add(IFormFile file, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var l = file.Length;
            var filePath = "";
            var fileName = "";
            if (file != null && file.Length > 0)
            {
                var imagePath = @"\assets\img\";
                var uploadPath = env.WebRootPath + imagePath;





                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);

                }
                var gif = file.FileName.Substring(file.FileName.Length - 4, 4).ToString();

                var unicFileName = Guid.NewGuid().ToString();
           
                    fileName = Path.GetFileName(unicFileName + "." + file.FileName.Split(".")[1].ToLower());
      

                string fullPath = uploadPath + fileName;
                filePath = Path.Combine(uploadPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }


            }
            return fileName;
        }

        [Obsolete]
        public static void Delete(string files, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var imagePath = @"\assets\img\";
            var uploadPath = env.WebRootPath + imagePath + files;
            FileInfo file = new FileInfo(uploadPath);
            if (file.Exists)//check file exsit or not  
            {
                file.Delete();
            }
        }


    }
}
