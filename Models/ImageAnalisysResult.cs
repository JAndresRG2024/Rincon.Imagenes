using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rincon.Imagenes.Models
{
    public class ImageAnalisysResult
    {
        public CaptionResult captionResult { get; set; }
        public ReadResult readResult { get; set; }

        public override string ToString()
        {
            if (captionResult != null && !string.IsNullOrWhiteSpace(captionResult.text))
            {
                return captionResult.text;
            }
            else if (readResult != null && readResult.blocks != null)
            {
                var s = "";
                foreach (var block in readResult.blocks)
                {
                    foreach (var line in block.lines)
                    {
                        s += line.text + " ";
                    }
                }
                return s.Trim();
            }
            return "No se encontró descripción ni texto en la imagen.";
        }
    }
}
