using Content.Server.Atmos.EntitySystems;
using Content.Server.FactoryStation.Components;
using Content.Shared.Atmos;
using Content.Shared.Lathe;

namespace Content.Server.FactoryStation.Systems;

public sealed partial class FactoryAtmosHeatSystem : EntitySystem
{
    [Dependency] private AtmosphereSystem _atmosphere = default!;

    private float _accumulator;

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        _accumulator += frameTime;

        // Обновление раз в секунду
        if (_accumulator < 1f)
            return;

        _accumulator = 0f;

        var query = EntityQueryEnumerator<
            FactoryIndustrialHeatComponent,
            LatheComponent>();

        while (query.MoveNext(out var uid, out var heat, out var lathe))
        {
            // Станок не работает
            if (lathe.CurrentRecipe == null)
                continue;

            // Получаем атмосферу тайла, где стоит станок
            var mixture = _atmosphere.GetContainingMixture(uid, true);

            if (mixture == null)
                continue;

            // Нагреваем атмосферу
            mixture.Temperature += heat.AtmosHeatPerSecond;
        }
    }
}
