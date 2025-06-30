using Application.Services.Chatbot.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services.Chatbot
{
    public class GeminiService(HttpClient httpClient, IConfiguration config) : IGeminiService
    {
        public async Task<string> AskGeminiAsync(GeminiRequestModel model)
        {
            var apiKey = config["Gemini:ApiKey"];
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

            // 🔁 Sayfaya özel prompt metni üret
            var promptText = model.Page switch
            {
                "Home" => $"""
            Kullanıcı şu anda uygulamanın **anasayfasında**.

            Bu sayfa aktif içerik göstermiyor, sadece genel bir giriş ekranı.

            Kullanıcının mesajı:
            {model.UserMessage}

            Lütfen kullanıcıya, aşağıdaki modüllerden hangisinin uygun olabileceğini öner:
            - Nefes egzersizi
            - Meditasyon
            - Rahatlatıcı müzik
            - Basit oyunlar

            Açıklayıcı, motive edici ve yönlendirici bir yanıt ver.
        """,

                "StressReport" => $"""
            Kullanıcı şu anda **stres raporu** sayfasında.

            Sayfa içeriği:
            {model.Context.ToString()}

            Kullanıcının mesajı:
            {model.UserMessage}

            Lütfen verileri analiz ederek açıklayıcı, destekleyici ve kişiselleştirilmiş bir cevap üret.
        """,
                "MusicList" => $"""
            Kullanıcı şu anda **rahatlatıcı sesler / müzikler** sayfasında.

            Sayfa verileri:
            {model.Context.ToString()}

            Kullanıcının mesajı:
            {model.UserMessage}

            Lütfen kullanıcıya:
            - Günlük/haftalık müzik kullanımına göre destekleyici analiz yap
            - En çok dinlenen müziğe referans ver (varsa)
            - Dilersen feedback (rahatlatıcı/nötr/rahatsız edici) dağılımına göre öneride bulun

            Açıklayıcı, yönlendirici ve motive edici bir yanıt üret.
        """,
                "MeditationList" => $"""
            Kullanıcı şu anda **Meditasyon Listesi** sayfasında.

            Kullanıcının meditasyon amacı (purpose): {model.Context.GetProperty("selectedPurpose").ToString()}

            Aşağıda listelenmiş meditasyon içerikleri yer alıyor:
            {model.Context.GetProperty("meditations").ToString()}

            Kullanıcının mesajı:
            {model.UserMessage}

            Lütfen bu sayfaya özel öneriler sun:
            - Hangi meditasyon amacına uygun içerikler varsa belirt
            - Süresi kısa içerikleri önerebilirsin
            - Açıklamaya göre yeni başlayanlara uygun olanları vurgula

            Motive edici, sade ve kullanıcı dostu bir yanıt ver.
        """,
                "MeditationDetail" => $"""
            Kullanıcı şu anda bir meditasyonun detay sayfasında.

            Başlık: {model.Context.GetProperty("title").ToString()}
            Açıklama: {model.Context.GetProperty("description").ToString()}

            Adımlar:
            {string.Join("\n", model.Context.GetProperty("steps").EnumerateArray().Select((s, i) => $"- {i + 1}. {s.GetProperty("title").ToString()}: {s.GetProperty("description").ToString()}"))}

            Kullanıcının sorusu:
            {model.UserMessage}

            Lütfen bu meditasyona özel, sade ve motive edici bir yanıt ver.
            """,
                "BreathingList" => $"""
                Kullanıcı şu anda nefes egzersizi listesi sayfasında.

                Aşağıda listelenen içerikler ve detayları:

                {string.Join("\n", model.Context.GetProperty("contents").EnumerateArray()
                      .Select(c =>
                        $"- {c.GetProperty("title").ToString()} ({c.GetProperty("stepCount")} adım, toplam ~{c.GetProperty("stepCount").GetInt32() * c.GetProperty("durationInSeconds").GetInt32()} saniye): {c.GetProperty("description").ToString()}")
                    )}

                Kullanıcının mesajı:
                {model.UserMessage}

                Lütfen içeriklere özel öneri üret:
                - Yeni başlayanlar için basit olanları öne çıkar
                - Uzun süre isteyenlere göre yönlendirme yap
                - Açıklamalardan anlam çıkararak rehberlik ver
            """,
                "BreathingDetail" => $"""
                Kullanıcı şu anda bir nefes egzersizi detay sayfasında.

                 Egzersiz Adı: {model.Context.GetProperty("title").GetString()}
                 Açıklama: {model.Context.GetProperty("description").GetString()}
                 Toplam Döngü Sayısı: {model.Context.GetProperty("stepCount").GetInt32()}

                 Adımlar:
                {string.Join("\n", model.Context.GetProperty("steps").EnumerateArray()
                      .Select((s, i) => $"- {s.GetProperty("title").GetString()} ({s.GetProperty("duration").GetInt32()} sn): {s.GetProperty("description").GetString()}"))}

                 Kullanıcı bu egzersizi tamamladıysa, adım adım süreler:
                {(model.Context.TryGetProperty("completedDurations", out var durations) ?
                      string.Join(", ", durations.EnumerateArray().Select((d, i) => $"Adım {i + 1}: {d.GetDouble():0.0} sn")) : "Henüz tamamlanmadı.")}

                Kullanıcının sorusu:
                {model.UserMessage}

                Lütfen bu egzersize özel açıklayıcı, sade ve motive edici bir yanıt üret.
                """,
                "GameList" => $"""
    Kullanıcı şu anda oyunlar sayfasında.

    Sayfada listelenen oyun içerikleri şunları içeriyor:
    {string.Join("\n", model.Context.GetProperty("games").EnumerateArray().Select(g => $"- {g.GetProperty("title").GetString()}: {g.GetProperty("description").GetString()} {(g.TryGetProperty("isFavorite", out var fav) && fav.GetBoolean() ? "[Favori]" : "")}"))}

    Kullanıcının filtrelediği içerik sayısı: {model.Context.GetProperty("games").GetArrayLength()}

    Kullanıcının sorusu:
    {model.UserMessage}

    Yukarıdaki oyunlara dayalı olarak bilgilendirici, motive edici ve kullanıcıyı yönlendiren bir yanıt ver.
    """,



                _ => $"""
            Kullanıcının bulunduğu sayfa: {model.Page}
            İçerik:
            {model.Context.ToString()}

            Kullanıcının mesajı:
            {model.UserMessage}

            Bağlama göre en uygun şekilde cevap ver.
        """
            };

            var body = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new { text = promptText }
                }
            }
        }
            };

            var response = await httpClient.PostAsJsonAsync(url, body);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<JsonElement>();
            return result.GetProperty("candidates")[0]
                         .GetProperty("content")
                         .GetProperty("parts")[0]
                         .GetProperty("text")
                         .GetString();
        }

    }

}
