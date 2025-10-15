using FlyweightEmojiApp.Models;
using FlyweightEmojiApp.Web.Models;
using FlyweightPattern.Factories;
using FlyweightPattern.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

{
    public class HomeController : Controller
    {
        private static readonly EmojiFactory _emojiFactory = new EmojiFactory();

        private List<EmojiUso> ObtenerEmojisUsados()
        {
            var json = HttpContext.Session.GetString("EmojisUsados");
            if (string.IsNullOrEmpty(json))
                return new List<EmojiUso>();

            var listaDto = JsonConvert.DeserializeObject<List<EmojiUsoDTO>>(json) ?? new List<EmojiUsoDTO>();

            return listaDto
                .Select(x => new EmojiUso
                {
                    Emoji = _emojiFactory.ObtenerEmoji(x.Simbolo),
                    PosicionX = x.PosicionX,
                    PosicionY = x.PosicionY,
                    Contexto = x.Contexto
                }).ToList();
        }

        private void GuardarEmojisUsados(List<EmojiUso> lista)
        {
            var listaDto = lista.Select(x => new EmojiUsoDTO
            {
                Simbolo = x.Emoji.Simbolo,
                PosicionX = x.PosicionX,
                PosicionY = x.PosicionY,
                Contexto = x.Contexto
            }).ToList();

            HttpContext.Session.SetString("EmojisUsados", JsonConvert.SerializeObject(listaDto));
        }

        public IActionResult Index()
        {
            var viewModel = new ReporteViewModel
            {
                UsosDeEmojis = ObtenerEmojisUsados(),
                TotalFlyweightsEnMemoria = _emojiFactory.TotalEmojisEnMemoria,
                FlyweightsCacheados = _emojiFactory.ObtenerSimbolosCacheados(),
                EmojisDisponibles = _emojiFactory.ObtenerTodos()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AgregarEmoji(string simbolo, int? posicionX, int? posicionY, string contexto)
        {
            IEmoji emoji = _emojiFactory.ObtenerEmoji(simbolo);
            var emojisUsados = ObtenerEmojisUsados();

            emojisUsados.Add(new EmojiUso
            {
                Emoji = emoji,
                PosicionX = posicionX ?? 0,
                PosicionY = posicionY ?? 0,
                Contexto = contexto ?? "Mensaje"
            });

            GuardarEmojisUsados(emojisUsados);
            return RedirectToAction("Index");
        }
    }
}
