using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Content.Server.TTS
{
    [Virtual]
    public class TTSClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string BaseUrl = "https://ntts.fdev.team/api/v1/tts";

        public TTSClient(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// Получить список всех доступных голосов
        /// </summary>
        public async Task<string> GetVoicesAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/speakers");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Создать кастомный голос по референсу
        /// </summary>
        public async Task<string> CreateCustomVoiceAsync(string speakerName, byte[] audioData)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(speakerName), "speaker_name");
            content.Add(new ByteArrayContent(audioData), "audio", "reference.wav");

            var response = await _httpClient.PostAsync($"{BaseUrl}/speakers", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Удалить кастомный голос
        /// </summary>
        public async Task<string> DeleteCustomVoiceAsync(string speakerName)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/speakers/{Uri.EscapeDataString(speakerName)}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Получить список доступных эффектов
        /// </summary>
        public async Task<string> GetEffectsAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/effects");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Синтезировать речь из текста
        /// </summary>
        /// <param name="speaker">Имя голоса (например, father_grigori)</param>
        /// <param name="text">Текст для синтеза</param>
        /// <param name="ext">Формат аудио: wav или ogg</param>
        /// <param name="effect">Эффект: echo, reverb, pitch_shift (опционально)</param>
        /// <returns>Массив байт с аудиоданными</returns>
        public async Task<byte[]> SynthesizeSpeechAsync(string speaker, string text, string ext = "wav", string? effect = null)
        {
            var url = $"{BaseUrl}?speaker={Uri.EscapeDataString(speaker)}&text={Uri.EscapeDataString(text)}&ext={ext}";
            if (!string.IsNullOrEmpty(effect))
                url += $"&effect={Uri.EscapeDataString(effect)}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
