using FlyweightEmojiApp.Models;
using FlyweightPattern.Factories;
using FlyweightPattern.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FlyweightEmojiApp.Controllers
{
    public class HomeController : Controller
    {
        private static readonly EmojiFactory _emojiFactory = new EmojiFactory();

        // Helper para obtener la lista de emojis usados desde Session (serializada en JSON)
        private List<EmojiUso> ObtenerEmojisUsados()
        {
            var json = HttpContext.Session.GetString("EmojisUsados");
            if (string.IsNullOrEmpty(json))
                return new List<EmojiUso>();
            return JsonConvert.DeserializeObject<List<EmojiUso>>(json) ?? new List<EmojiUso>();
        }

        // Helper para guardar la lista serializada en Session
        private void GuardarEmojisUsados(List<EmojiUso> lista)
        {
            HttpContext.Session.SetString("EmojisUsados", JsonConvert.SerializeObject(lista));
        }

        // Vista principal
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

        // [HttpPost] para simular agregar un emoji desde la vista
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
