using Content.Server.FactoryStation.Components;
using Robust.Server.GameObjects;
using Robust.Shared.Random;
using Robust.Shared.Maths;

namespace Content.Server.FactoryStation.Systems;

public sealed partial class FactoryFlickerSystem : EntitySystem
{
    [Dependency] private IRobustRandom _random = default!;
    [Dependency] private PointLightSystem _pointLight = default!;
    [Dependency] private SharedTransformSystem _transform = default!;

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<
            FactoryFlickerComponent,
            PointLightComponent>();

        while (query.MoveNext(out var uid, out var flicker, out var pointLight))
        {
            // Инициализация
            if (!flicker.FlickerInitialized)
            {
                flicker.BaseEnergy = pointLight.Energy;
                flicker.BaseRadius = pointLight.Radius;
                flicker.BaseColor = pointLight.Color;

                flicker.FlickerInitialized = true;
            }

            // Если лампа выключена
            if (!pointLight.Enabled)
                continue;

            flicker.NextFlicker -= frameTime;

            // Ждём следующего тика мерцания
            if (flicker.NextFlicker > 0f)
                continue;

            // Следующий тик
            flicker.NextFlicker = _random.NextFloat(
                flicker.MinFlickerDelay,
                flicker.MaxFlickerDelay);

            // Базовые множители
            var energyMul = _random.NextFloat(
                flicker.MinEnergyMultiplier,
                flicker.MaxEnergyMultiplier);

            var radiusMul = _random.NextFloat(
                flicker.MinRadiusMultiplier,
                flicker.MaxRadiusMultiplier);

            // Редкая почти-потеря питания
            if (_random.Prob(flicker.BlackoutChance))
            {
                energyMul *= 0.08f;
                radiusMul *= 0.5f;
            }

            // Применяем энергию
            _pointLight.SetEnergy(
                uid,
                flicker.BaseEnergy * energyMul,
                pointLight);

            // Применяем радиус
            _pointLight.SetRadius(
                uid,
                flicker.BaseRadius * radiusMul,
                pointLight);

            // Лёгкий industrial tint
            var greenShift = _random.NextFloat(0.9f, 0.95f);
            var blueShift = _random.NextFloat(0.72f, 0.8f);
            var redShift = 1f;

            var color = new Color(
                redShift,
                greenShift,
                blueShift);

            _pointLight.SetColor(uid, color, pointLight);
        }
    }
}
