# Relaxify API

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4.svg)
![Architecture](https://img.shields.io/badge/architecture-Clean%20Architecture-blue.svg)
![Database](https://img.shields.io/badge/database-PostgreSQL-336791.svg)
![Auth](https://img.shields.io/badge/auth-JWT-black.svg)

## Genel Bakış

Relaxify, kullanıcıların nefes egzersizi, meditasyon, rahatlatıcı müzik ve
basit oyunlar üzerinden günlük stresini yönetmesine yardımcı olan bir
uygulamanın **backend API**'sidir. Bu repo, uygulamanın sunucu tarafını —
kimlik doğrulama, içerik yönetimi, kullanıcı ilerlemesi ve öneri/analiz
altyapısını — kapsar; mobil/web istemci ve ML modeli ayrı repolarda yaşar.

Standart bir CRUD API'nin ötesinde, Relaxify üç farklı dış servisi bir araya
getirir: kullanıcının geçmiş davranışına (içerik kullanım logları, stres
testi sonuçları) dayanan bir **ML tabanlı öneri/stres tahmin motoru**
(otomatik model retrain dahil), sayfa bağlamına duyarlı bir **Gemini
chatbot**'u ve **ElevenLabs** ile gerçek zamanlı **metinden sese**
dönüşümü. API bu servislerin hepsiyle HTTP üzerinden, Clean Architecture
sınırlarını bozmadan konuşur.

## İçindekiler

- [AI/ML ve Dış Servis Entegrasyonları](#aiml-ve-dış-servis-entegrasyonları)
- [Özellikler](#özellikler)
- [Mimari](#mimari)
- [Teknoloji Yığını](#teknoloji-yığını)
- [Proje Yapısı](#proje-yapısı)
- [API Uç Noktaları](#api-uç-noktaları)
- [Gereksinimler](#gereksinimler)
- [Kurulum ve Çalıştırma](#kurulum-ve-çalıştırma)
- [Yol Haritası](#yol-haritası)

---

## AI/ML ve Dış Servis Entegrasyonları

Bu repo ses sentezi mantığını içermez, sadece ElevenLabs'i çağırır. Model
eğitimi/tahmini içinse hem **tüketici** hem **orkestratör** rolündedir:
tahmin istekleri harici bir Flask servisine HTTP ile gönderilir, ama model
**retrain işlemini bu backend'in kendisi tetikler** — bkz. aşağıdaki
"Otomatik model retrain" maddesi.

- **İçerik önerisi** (`RecommendationController`): kullanıcının son içerik
  kategorisi, kullanım süresi, saat/gün bilgisi ve son stres skoru bir araya
  getirilip harici bir Flask ML servisine gönderilir; dönen içerik id'sine
  göre öneri detayları döndürülür.
- **Stres tahmini** (`StressController`, `StressPredictionService`):
  benzer girdilerle aynı ML servisinden bir stres skoru tahmini alınır.
- **Otomatik model retrain** (`AiTrainingService`) — `AiController`'daki
  `retrain` / `retrain-stress` uç noktaları manuel, `AiModelRetrainService`
  adında saatte bir çalışan gerçek bir `BackgroundService` (`IHostedService`,
  her iterasyonda `IServiceScopeFactory` ile ayrı bir DI scope açıyor)
  otomatik olarak retrain'i tetikler. Retrain, ML servisine bir HTTP isteği
  değildir: backend, sırasıyla `pull_data.py` → `prepare_dataset.py` →
  `train_model.py` script'lerini yerel bir `python` process'i olarak
  çalıştırır (`Process.Start`), ardından Flask'a `/reload-model` veya
  `/reload-stress-model` çağrısı yaparak yeni modeli belleğe yükletir.
  > Script'lerin çalışma dizini `appsettings.Development.json`'daki
  > `AiPipeline:ScriptsRootPath` alanından okunur — kendi makinende
  > retrain'i çalıştırmak istersen bu değeri kendi
  > [`relaxify-ml`](https://github.com/mlhonbasi/relaxify-ml) klonunun
  > konumuna göre ayarlaman yeterli.
- **Chatbot** (`GeminiController`, `GeminiService`): Google Gemini API'si
  ile, kullanıcının bulunduğu sayfaya göre (anasayfa, stres raporu,
  meditasyon detayı vb.) farklı prompt şablonları üreten bir asistan.
- **Metinden sese** (`TtsController`): ElevenLabs API'siyle verilen metni
  gerçek zamanlı sese çevirip `audio/mpeg` olarak döndürür.

ML servisi kapalıysa öneri/tahmin uç noktaları hata fırlatmak yerine
boş/`null` sonuç döner; API'nin geri kalanı etkilenmez.

## Özellikler

- **Kimlik doğrulama** — JWT tabanlı login/register, şifreler BCrypt ile hashleniyor.
- **İçerik yönetimi** — nefes egzersizi, meditasyon, müzik ve oyun içerikleri;
  her biri için hem kullanıcı tarafı listeleme/detay/favori uç noktaları hem
  de ayrı **admin CRUD** controller'ları.
- **Stres testi** — soru/cevap akışı, sonuç kaydı, kullanıcı geçmişi ve özet
  istatistikleri.
- **Kullanıcı ilerlemesi** — favoriler, içerik kullanım logları, hedefler
  (goals) ve başarımlar (achievements).
- **Geri bildirim** — içerik bazlı kullanıcı geri bildirimi ve
  özet/istatistik uç noktaları.
- **Dosya yükleme** — görsel ve ses dosyaları için upload uç noktaları.

> AI/ML, chatbot ve metinden sese özellikleri için bkz. yukarıdaki
> [AI/ML ve Dış Servis Entegrasyonları](#aiml-ve-dış-servis-entegrasyonları).

## Mimari

Proje dört katmanlı bir **Clean Architecture** ile organize edilmiştir;
bağımlılıklar her zaman içeriye (Domain'e) doğru akar:

```
Api  ──depends on──►  Application  ──depends on──►  Domain
 │                          │
 └────────depends on────────┴──────────────────────►  Infrastructure ──► Domain
```

- **Domain** — entity'ler, enum'lar, repository arayüzleri, options
  sınıfları. Başka hiçbir katmana bağımlı değil.
- **Application** — iş kuralları ve servisler (auth, içerik, stres, AI,
  chatbot, achievement, goal, profile). Sadece `Domain`'e bağımlı; veritabanı
  veya HTTP detayı bilmez.
- **Infrastructure** — EF Core `DbContext`, repository implementasyonları,
  migration'lar. `Domain`'deki arayüzleri karşılar.
- **Api** — controller'lar, JWT/CORS/Swagger yapılandırması, `Program.cs`.
  Diğer üç katmanı bir araya getirir.

Bu ayrım sayesinde `Application` katmanındaki iş kuralları PostgreSQL yerine
başka bir veritabanına, ya da HTTP dışında başka bir sunum katmanına
geçilse bile değişmeden kalabilir; dış servis entegrasyonları (Gemini,
ElevenLabs, ML servisi) da birer `Application` servisi olarak aynı sınırın
içinde kalır.

## Teknoloji Yığını

| Katman | Teknoloji |
|---|---|
| Framework | ASP.NET Core (.NET 9) |
| Veritabanı | PostgreSQL + Entity Framework Core (Npgsql) |
| Kimlik doğrulama | JWT Bearer + BCrypt.Net |
| Dokümantasyon | Swagger / OpenAPI (Swashbuckle) |
| Dış servisler | Google Gemini API (chatbot), ElevenLabs API (metinden sese), harici Flask servisi (stres tahmini / içerik önerisi) |

## Proje Yapısı

```
relaxify-backend/
├── Api/                      # Controller'lar, Program.cs, appsettings, wwwroot (upload'lar)
│   └── Controllers/
├── Application/               # Servisler (Auth, Content, Stress, AI, Chatbot, Achievement, Goal, Profile...)
│   └── Services/
├── Domain/                    # Entity'ler, enum'lar, interface'ler, options
│   ├── Entities/
│   ├── Enums/
│   ├── Interfaces/
│   └── Options/
├── Infrastructure/            # DbContext, repository implementasyonları, migration'lar
│   ├── Context/
│   ├── Repositories/
│   └── Migrations/
└── Relaxify.sln
```

## API Uç Noktaları

Tüm uç noktalar `api/[controller]` altında toplanır. Gruplara göre özet:

| Alan | Controller(lar) | Örnek uç noktalar |
|---|---|---|
| Kimlik doğrulama | `Auth` | `POST /Register`, `POST /Login` |
| Profil | `Profile` | `PUT /ChangeName`, `PUT /ChangePassword` |
| İçerik (kullanıcı) | `Breathing/Meditation/Music/GameContent`, `Contents` | `GET`, `GET/{id}`, `POST toggle-favorite` |
| İçerik (admin) | `AdminBreathingContent`, `AdminContent`, `AdminGameContent`, `AdminMeditationContent`, `AdminMusicContent` | CRUD (`GET/POST/PUT/DELETE`) |
| Stres | `Stress`, `StressTestResult` | `GET questions`, `GET predict`, `POST submit`, `GET summary` |
| AI / ML | `Ai`, `Recommendation` | `POST retrain`, `GET recommendation-input` |
| Chatbot & TTS | `Gemini`, `Tts` | `POST ask`, `POST speech` |
| Kullanıcı ilerlemesi | `UserAchievement`, `UserGoal`, `UserContentLog` | `GET`, `POST mark-seen`, `POST log-steps` |
| Geri bildirim | `Feedback` | `POST`, `GET music-summary` |
| Dosya yükleme | `Upload` | `POST image`, `POST audio` |

Detaylı istek/yanıt şemaları için proje çalışırken `/swagger` adresine bakınız.

## Gereksinimler

- **.NET 9 SDK**
- **PostgreSQL** (erişilebilir bir instance)
- İsteğe bağlı: AI/öneri özellikleri için `localhost:5000`'de çalışan uyumlu
  bir Flask servisi, geçerli bir **Gemini API key**'i ve metinden sese için
  bir **ElevenLabs API key**'i.

## Kurulum ve Çalıştırma

1. Reponun kökünde `Api/appsettings.Development.json` dosyasını oluşturun
   (bu dosya `.gitignore`'dadır, gerçek bilgiler asla commit edilmez):

   ```json
   {
     "ConnectionStrings": {
       "db_relaxify": "Host=localhost;Port=5432;Database=db_relaxify;Username=postgres;Password=YOUR_PASSWORD"
     },
     "Jwt": {
       "Key": "en az 32 karakterlik gizli bir anahtar",
       "Issuer": "Relaxify",
       "Audience": "Relaxify"
     },
     "Gemini": {
       "ApiKey": "YOUR_GEMINI_API_KEY"
     },
     "ElevenLabs": {
       "ApiKey": "YOUR_ELEVENLABS_API_KEY",
       "VoiceId": "YOUR_VOICE_ID"
     }
   }
   ```

2. Veritabanını oluşturun:

   ```sh
   dotnet ef database update --project Infrastructure --startup-project Api
   ```

3. Çalıştırın:

   ```sh
   dotnet run --project Api
   ```

   API `http://localhost:5097` üzerinde ayağa kalkar; geliştirme ortamında
   Swagger UI `/swagger` altında açılır.

## Yol Haritası

- [ ] Test projesi eklenecek (şu an hiç test yok).
- [ ] CI pipeline'ı kurulacak (`.github/workflows` şu an boş).
- [ ] CORS politikası (`AllowAnyOrigin`) prod'a çıkmadan önce sıkılaştırılacak.
