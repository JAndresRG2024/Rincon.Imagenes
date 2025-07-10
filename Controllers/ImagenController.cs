using Microsoft.AspNetCore.Mvc;
using Rincon.Imagenes.Models;
using Rincon.Imagenes.Services;

public class ImagenController : Controller
{
    private readonly VisionService _visionService;
    private readonly IWebHostEnvironment _env;

    public ImagenController(IWebHostEnvironment env)
    {
        _visionService = new VisionService();
        _env = env;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new ImageInputModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(ImageInputModel model)
    {
        string imageUrl = "";

        if (model.ImagenSubida != null && model.ImagenSubida.Length > 0)
        {
            var fileName = Path.GetFileName(model.ImagenSubida.FileName);
            var path = Path.Combine(_env.WebRootPath, "uploads", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.ImagenSubida.CopyToAsync(stream);
            }

            imageUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
        }
        else if (!string.IsNullOrWhiteSpace(model.UrlImagen))
        {
            imageUrl = model.UrlImagen;
        }

        if (string.IsNullOrEmpty(imageUrl))
        {
            ModelState.AddModelError("", "Debes subir una imagen o proporcionar una URL.");
            return View(model);
        }

        var resultado = await _visionService.AnalizarImagenAsync(imageUrl);
        ViewBag.Resultado = resultado;
        ViewBag.ImagenMostrada = imageUrl;

        return View(model);
    }
}
