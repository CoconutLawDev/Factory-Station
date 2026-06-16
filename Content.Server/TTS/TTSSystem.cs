using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Robust.Shared.Configuration;
using Robust.Shared.IoC;
using Robust.Shared.Log;

namespace Content.Server.TTS
{
    [Virtual]
    public class TTSSystem : EntitySystem
    {
        [Dependency] private ILogManager _logManager = default!;
        [Dependency] private IConfigurationManager _configurationManager = default!;

        private TTSClient _ttsClient = default!;
        private ISawmill _sawmill = default!;
        private bool _isEnabled = false;
        private string[] _availableSpeakers = Array.Empty<string>();

        public override void Initialize()
        {
            base.Initialize();
            _sawmill = _logManager.GetSawmill("tts");

            // Получаем API-ключ из защищённых источников
            var apiKey = GetApiKey();

            if (string.IsNullOrEmpty(apiKey))
            {
                _sawmill.Warning("TTS API key not configured. TTS will be disabled.");
                _sawmill.Warning("Set TTS_API_KEY environment variable or configure in appsettings.yml");
                _isEnabled = false;
                return;
            }

            try
            {
                _ttsClient = new TTSClient(apiKey);
                _isEnabled = true;
                _sawmill.Info("TTS System initialized successfully.");

                // Проверяем подключение и загружаем список голосов
                Task.Run(async () => await InitializeAsync());
            }
            catch (Exception ex)
            {
                _sawmill.Error($"Failed to initialize TTS Client: {ex.Message}");
                _isEnabled = false;
            }
        }

        private async Task InitializeAsync()
        {
            try
            {
                var voicesJson = await _ttsClient.GetVoicesAsync();
                _sawmill.Info($"TTS connection successful.");

                // Парсим список голосов
                using var doc = JsonDocument.Parse(voicesJson);
                if (doc.RootElement.TryGetProperty("voices", out var voicesArray))
                {
                    var speakers = new System.Collections.Generic.List<string>();
                    foreach (var voice in voicesArray.EnumerateArray())
                    {
                        if (voice.TryGetProperty("speakers", out var speakersArray))
                        {
                            foreach (var speaker in speakersArray.EnumerateArray())
                            {
                                speakers.Add(speaker.GetString() ?? "unknown");
                            }
                        }
                    }
                    _availableSpeakers = speakers.ToArray();
                    _sawmill.Info($"Loaded {_availableSpeakers.Length} available voices.");
                }
            }
            catch (Exception ex)
            {
                _sawmill.Error($"TTS initialization failed: {ex.Message}");
                _isEnabled = false;
            }
        }

        private string? GetApiKey()
        {
            // 1. Пробуем из переменной окружения (самый безопасный способ)
            var apiKey = Environment.GetEnvironmentVariable("TTS_API_KEY");
            if (!string.IsNullOrEmpty(apiKey))
            {
                _sawmill.Info("TTS API key loaded from environment variable.");
                return apiKey;
            }

            // 2. Пробуем из защищённого файла secrets.yml (игнорируется в git)
            var secretsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "secrets.yml");
            if (File.Exists(secretsPath))
            {
                try
                {
                    var lines = File.ReadAllLines(secretsPath);
                    foreach (var line in lines)
                    {
                        var trimmed = line.Trim();
                        if (trimmed.StartsWith("api_key:"))
                        {
                            var parts = trimmed.Split(':', 2);
                            if (parts.Length == 2)
                            {
                                apiKey = parts[1].Trim().Trim('"', '\'');
                                if (!string.IsNullOrEmpty(apiKey))
                                {
                                    _sawmill.Info("TTS API key loaded from secrets.yml.");
                                    return apiKey;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _sawmill.Error($"Failed to read secrets file: {ex.Message}");
                }
            }

            // 3. Fallback на appsettings.yml (не рекомендуется, но как запасной вариант)
            try
            {
                apiKey = _configurationManager.GetCVar<string>("tts.api_key");
                if (!string.IsNullOrEmpty(apiKey) && apiKey != "YOUR_API_KEY_HERE" && apiKey != "ВАШ_API_КЛЮЧ")
                {
                    _sawmill.Warning("TTS API key loaded from appsettings.yml. Consider using environment variable for security.");
                    return apiKey;
                }
            }
            catch
            {
                // Игнорируем ошибки чтения конфига
            }

            return null;
        }

        /// <summary>
        /// Проверить, включена ли TTS система
        /// </summary>
        public bool IsEnabled => _isEnabled;

        /// <summary>
        /// Получить список доступных голосов
        /// </summary>
        public string[] GetAvailableSpeakers() => _availableSpeakers;

        /// <summary>
        /// Синтезировать речь и сохранить в файл
        /// </summary>
        public async Task<bool> TrySynthesizeSpeechAsync(string speaker, string text, string outputPath, string ext = "wav", string? effect = null)
        {
            if (!_isEnabled)
            {
                _sawmill.Warning("TTS is disabled. Check API key configuration.");
                return false;
            }

            if (string.IsNullOrEmpty(text))
            {
                _sawmill.Warning("Cannot synthesize empty text.");
                return false;
            }

            if (!IsSpeakerValid(speaker))
            {
                _sawmill.Warning($"Speaker '{speaker}' is not in the list of available voices.");
                return false;
            }

            try
            {
                _sawmill.Debug($"Synthesizing speech: speaker={speaker}, text='{text}', effect={effect ?? "none"}");
                var audioData = await _ttsClient.SynthesizeSpeechAsync(speaker, text, ext, effect);

                // Создаём директорию, если её нет
                var directory = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(directory))
                    Directory.CreateDirectory(directory);

                await File.WriteAllBytesAsync(outputPath, audioData);
                _sawmill.Info($"Speech saved to {outputPath} ({audioData.Length} bytes)");
                return true;
            }
            catch (Exception ex)
            {
                _sawmill.Error($"Speech synthesis failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Синтезировать речь и получить массив байт
        /// </summary>
        public async Task<byte[]?> TrySynthesizeSpeechBytesAsync(string speaker, string text, string ext = "wav", string? effect = null)
        {
            if (!_isEnabled)
            {
                _sawmill.Warning("TTS is disabled. Check API key configuration.");
                return null;
            }

            if (string.IsNullOrEmpty(text))
            {
                _sawmill.Warning("Cannot synthesize empty text.");
                return null;
            }

            if (!IsSpeakerValid(speaker))
            {
                _sawmill.Warning($"Speaker '{speaker}' is not in the list of available voices.");
                return null;
            }

            try
            {
                _sawmill.Debug($"Synthesizing speech: speaker={speaker}, text='{text}', effect={effect ?? "none"}");
                return await _ttsClient.SynthesizeSpeechAsync(speaker, text, ext, effect);
            }
            catch (Exception ex)
            {
                _sawmill.Error($"Speech synthesis failed: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Получить список всех голосов в формате JSON
        /// </summary>
        public async Task<string?> GetVoicesJsonAsync()
        {
            if (!_isEnabled)
            {
                _sawmill.Warning("TTS is disabled.");
                return null;
            }

            try
            {
                return await _ttsClient.GetVoicesAsync();
            }
            catch (Exception ex)
            {
                _sawmill.Error($"Failed to get voices: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Создать кастомный голос
        /// </summary>
        public async Task<bool> TryCreateCustomVoiceAsync(string speakerName, byte[] audioData)
        {
            if (!_isEnabled)
            {
                _sawmill.Warning("TTS is disabled.");
                return false;
            }

            try
            {
                var result = await _ttsClient.CreateCustomVoiceAsync(speakerName, audioData);
                _sawmill.Info($"Custom voice created: {speakerName}");

                // Обновляем список голосов
                await InitializeAsync();
                return true;
            }
            catch (Exception ex)
            {
                _sawmill.Error($"Failed to create custom voice: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Удалить кастомный голос
        /// </summary>
        public async Task<bool> TryDeleteCustomVoiceAsync(string speakerName)
        {
            if (!_isEnabled)
            {
                _sawmill.Warning("TTS is disabled.");
                return false;
            }

            try
            {
                var result = await _ttsClient.DeleteCustomVoiceAsync(speakerName);
                _sawmill.Info($"Custom voice deleted: {speakerName}");

                // Обновляем список голосов
                await InitializeAsync();
                return true;
            }
            catch (Exception ex)
            {
                _sawmill.Error($"Failed to delete custom voice: {ex.Message}");
                return false;
            }
        }

        private bool IsSpeakerValid(string speaker)
        {
            if (_availableSpeakers.Length == 0)
                return true; // Если список ещё не загружен, пропускаем проверку

            return Array.Exists(_availableSpeakers, s => s.Equals(speaker, StringComparison.OrdinalIgnoreCase));
        }

        public override void Shutdown()
        {
            base.Shutdown();
            _ttsClient?.Dispose();
        }
    }
}
