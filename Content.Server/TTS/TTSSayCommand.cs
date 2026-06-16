using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Content.Server.Administration;
using Content.Server.TTS;
using Content.Shared.Administration;
using Robust.Shared.Console;
using Robust.Shared.IoC;

namespace Content.Server.Commands
{
    [AdminCommand(AdminFlags.Admin)]
    public sealed class TTSSayCommand : IConsoleCommand
    {
        public string Command => "ttssay";
        public string Description => "Синтезирует речь указанным голосом и сохраняет в файл.";
        public string Help => "ttssay <speaker> <text> [effect]";

        public async void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            if (args.Length < 2)
            {
                shell.WriteLine("Недостаточно аргументов.");
                shell.WriteLine($"Использование: {Command} {Help}");
                return;
            }

            var speaker = args[0];
            var effect = args.Length > 2 ? args[^1] : null;
            var textArgs = effect != null ? args.Skip(1).Take(args.Length - 2) : args.Skip(1);
            var text = string.Join(" ", textArgs);

            var ttsSystem = IoCManager.Resolve<IEntitySystemManager>().GetEntitySystem<TTSSystem>();

            if (!ttsSystem.IsEnabled)
            {
                shell.WriteLine("❌ TTS система отключена. Проверьте настройки API ключа.");
                return;
            }

            // Показываем доступные голоса
            var speakers = ttsSystem.GetAvailableSpeakers();
            if (speakers.Length > 0 && !speakers.Contains(speaker))
            {
                shell.WriteLine($"⚠️ Голос '{speaker}' не найден. Доступные голоса: {string.Join(", ", speakers)}");
                shell.WriteLine("Продолжаем с указанным голосом (может не сработать)...");
            }

            shell.WriteLine($"🎤 Синтезируем речь голосом '{speaker}': \"{text}\"");
            if (effect != null)
                shell.WriteLine($"🎵 С эффектом: {effect}");

            var fileName = $"tts_{speaker}_{DateTime.UtcNow.Ticks}.wav";
            var ttsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TTS");
            var filePath = Path.Combine(ttsDir, fileName);

            Directory.CreateDirectory(ttsDir);

            var result = await ttsSystem.TrySynthesizeSpeechAsync(speaker, text, filePath, "wav", effect);

            if (result)
            {
                shell.WriteLine($"✅ Речь успешно сохранена в: {filePath}");
            }
            else
            {
                shell.WriteLine("❌ Ошибка при синтезе речи. Проверьте логи сервера.");
            }
        }
    }
}
