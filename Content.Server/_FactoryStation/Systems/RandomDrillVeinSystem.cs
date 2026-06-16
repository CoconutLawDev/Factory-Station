using Content.Server.FactoryStation.Components;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Random;

namespace Content.Server.FactoryStation.Systems;

public sealed partial class RandomDrillVeinSystem : EntitySystem
{
    [Dependency] private IRobustRandom _random = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<RandomDrillVeinComponent, MapInitEvent>(OnMapInit);
    }

    private void OnMapInit(EntityUid uid, RandomDrillVeinComponent comp, MapInitEvent args)
    {
        if (!_random.Prob(comp.Chance) || comp.VeinPrototypes.Count == 0)
            return;

        // Выбираем случайный прототип из списка
        var proto = _random.Pick(comp.VeinPrototypes);

        // Сохраняем координаты текущей стены
        var coords = Transform(uid).Coordinates;

        // Удаляем старую стену
        Del(uid);

        // Создаём на её месте новую клетку
        Spawn(proto, coords);
    }
}
