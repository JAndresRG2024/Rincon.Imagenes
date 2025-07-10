using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rincon.Imagenes.Models;
using Rincon.Imagenes.Services;

namespace Rincon.Imagenes.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly VisionService _visionService;
        private readonly TranslationService _translationService; // Add this line  

        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
            _visionService = new VisionService();
            _translationService = new TranslationService(); // Initialize the service  
        }

        [BindProperty]
        public ImageInputModel Input { get; set; }

        public string Resultado { get; set; }

        public string ResultadoTraduccido { get; set; }
        public string ImagenMostrada { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {

            string imageUrl = "";
            
            if (!string.IsNullOrWhiteSpace(Input.UrlImagen))
            {
                imageUrl = Input.UrlImagen;
            }

            if (string.IsNullOrEmpty(imageUrl))
            {
                ModelState.AddModelError(string.Empty, "Debes proporcionar una URL.");
                return Page();
            }
            Resultado = await _visionService.AnalizarImagenAsync(imageUrl);
            ResultadoTraduccido = await _translationService.TraducirAsync(Resultado, "es");

            ImagenMostrada = imageUrl;

            return Page();
        }
    }
}