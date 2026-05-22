using Content.Server.Administration;
using Content.Server.FactoryStation.Components;
using Content.Server.FactoryStation.Systems;
using Content.Shared.Administration;
using Robust.Shared.Console;
using Robust.Shared.Map.Components;

namespace Content.Server.FactoryStation.Commands;

[AdminCommand(AdminFlags.Mapping)]
public sealed class SpawnDebrisCommand : IConsoleCommand
{
    public string Command => "spawndebris";
    public string Description => "Создаёт один обломок астероида с рудой вокруг карты";
    public string Help => "spawndebris [mapUid]";

    public void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        var entMan = IoCManager.Resolve<IEntityManager>();
        var system = entMan.System<AsteroidFieldSystem>();

        EntityUid mapUid;

        if (args.Length > 0 && int.TryParse(args[0], out int mapId))
            mapUid = new EntityUid(mapId);
        else if (shell.Player?.AttachedEntity is { Valid: true } playerEntity)
        {
            var xform = entMan.GetComponent<TransformComponent>(playerEntity);
            mapUid = xform.MapUid ?? EntityUid.Invalid;
        }
        else
        {
            shell.WriteLine("Укажите UID карты или прикрепитесь к сущности на карте.");
            return;
        }

        if (!entMan.TryGetComponent<MapComponent>(mapUid, out _))
        {
            shell.WriteLine($"Сущность {mapUid} не является картой.");
            return;
        }

        if (!entMan.TryGetComponent<AsteroidFieldComponent>(mapUid, out var field))
        {
            shell.WriteLine("На карте нет компонента AsteroidFieldComponent. Сначала добавьте его через addcomp.");
            return;
        }

        system.GenerateDebris(mapUid, field);
        shell.WriteLine("Обломок создан.");
    }
}
