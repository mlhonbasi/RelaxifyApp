using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class UploadController : ControllerBase
    {
        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { error = "Dosya boş" });

            // 📁 Yükleme klasörünü oluştur
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // 📄 Benzersiz dosya ismi oluştur
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            // 💾 Dosyayı kaydet
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 🌐 Geriye erişilebilir path gönder (Angular burayı kullanacak)
            var relativePath = $"/uploads/{fileName}";

            return Ok(new { filePath = relativePath });
        }

        [HttpPost("audio")]
        public async Task<IActionResult> UploadAudio(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { error = "Dosya boş" });

            // 🎵 Müzik dosyaları için /audio klasörü
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "audio");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Geriye /uploads/audio/... olarak döndür
            var relativePath = $"/audio/{fileName}";

            return Ok(new { filePath = relativePath });
        }

    }
}
