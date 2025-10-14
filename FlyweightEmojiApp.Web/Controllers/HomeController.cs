using FlyweightEmojiApp.Models;
using FlyweightPattern.Factories;
using FlyweightPattern.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FlyweightEmojiApp.Controllers
{
    // DTO simple solo para la sesión
    public class EmojiUsoDTO
    {
        public string Simbolo { get; set; }
        public int PosicionX { get; set; }
        public int PosicionY { get; set; }
        public string Contexto { get; set; }
    }

    public class HomeController : Controller
    {
        private static readonly EmojiFactory _emojiFactory = new EmojiFactory();

        // Helper para obtener la lista de emojis usados desde Session utilizando DTO
        private List<EmojiUso> ObtenerEmojisUsados()
        {
            var json = HttpContext.Session.GetString("EmojisUsados");
            if (string.IsNullOrEmpty(json))
                return new List<EmojiUso>();

            var listaDto = JsonConvert.DeserializeObject<List<EmojiUsoDTO>>(json) ?? new List<EmojiUsoDTO>();

            // Transforma DTO a EmojiUso real usando el factory
            return listaDto
                .Select(x => new EmojiUso
                {
                    Emoji = _emojiFactory.ObtenerEmoji(x.Simbolo),
                    PosicionX = x.PosicionX,
                    PosicionY = x.PosicionY,
                    Contexto = x.Contexto
                }).ToList();
        }

        // Helper para guardar la lista serializada en Session usando DTO
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
