using System.Collections.Generic;
using Content.Server.FactoryStation.Components;
using Content.Shared.FactoryStation;
using Content.Shared.Lathe;
using Content.Shared.Materials;
using Content.Shared.Damage.Components;
using Content.Shared.Power.Components;
using Content.Shared.Damage.Systems;
using Robust.Shared.GameObjects;
using Robust.Shared.Map;
using Robust.Shared.Timing;

namespace Content.Server.FactoryStation.Systems;

public sealed partial class EnterpriseConsoleSystem : EntitySystem
{
    [Dependency] private IMapManager _mapManager = default!;
    [Dependency] private IGameTiming _gameTiming = default!;
    [Dependency] private SharedTransformSystem _transformSystem = default!;
    [Dependency] private DamageableSystem _damageableSystem = default!;

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<EnterpriseConsoleComponent, EnterpriseConsoleDataComponent, TransformComponent>();
        while (query.MoveNext(out var uid, out var console, out var data, out var consoleTransform))
        {
            console.UpdateAccumulator += frameTime;
            if (console.UpdateAccumulator < console.UpdateInterval)
                continue;
            console.UpdateAccumulator -= console.UpdateInterval;

            List<MachineInfo> machines = new();
            var mapId = consoleTransform.MapID;
            var consoleWorldPos = _transformSystem.GetWorldPosition(consoleTransform);

            var latheQuery = AllEntityQuery<LatheComponent, TransformComponent>();
            while (latheQuery.MoveNext(out var latheUid, out var lathe, out var latheTransform))
            {
                if (latheTransform.MapID != mapId)
                    continue;

                if (console.Range > 0)
                {
                    var machineWorldPos = _transformSystem.GetWorldPosition(latheTransform);
                    if ((consoleWorldPos - machineWorldPos).Length() > console.Range)
                        continue;
                }

                string name = "Machine";
                if (TryComp<FactoryMachineNameComponent>(latheUid, out var nameComp))
                    name = nameComp.Name;
                else if (TryComp(latheUid, out MetaDataComponent? meta) && !string.IsNullOrEmpty(meta.EntityName))
                    name = meta.EntityName;

                string? activeRecipe = lathe.CurrentRecipe == default ? null : lathe.CurrentRecipe;

                Dictionary<string, int> materials = new();
                if (TryComp<MaterialStorageComponent>(latheUid, out var matStorage))
                {
                    foreach (var (matId, amount) in matStorage.Storage)
                        materials[matId] = amount;
                }

                float? temperature = null;
                if (TryComp<FactoryIndustrialHeatComponent>(latheUid, out var heat))
                    temperature = heat.CurrentHeat;

                int damage = 0;
                if (TryComp<DamageableComponent>(latheUid, out var damageable))
                    damage = _damageableSystem.GetTotalDamage(latheUid).Int();

                bool powered = true;
                if (TryComp<SharedApcPowerReceiverComponent>(latheUid, out var powerReceiver))
                    powered = powerReceiver.Powered;

                MachineStatus status;
                if (!powered)
                {
                    status = MachineStatus.Offline;
                }
                else
                {
                    float dangerThreshold = 700f;
                    float criticalThreshold = 1100f;
                    if (TryComp<FactoryIndustrialHeatComponent>(latheUid, out var heatComp))
                    {
                        dangerThreshold = heatComp.DangerThreshold;
                        criticalThreshold = heatComp.CriticalThreshold;
                    }

                    if ((temperature >= criticalThreshold && heatComp != null) || damage >= 100)
                        status = MachineStatus.Critical;
                    else if ((temperature >= dangerThreshold && heatComp != null) || damage >= 50)
                        status = MachineStatus.Warning;
                    else
                        status = MachineStatus.Normal;
                }

                machines.Add(new MachineInfo(
                    GetNetEntity(latheUid),
                    name,
                    activeRecipe,
                    materials,
                    temperature,
                    damage,
                    powered,
                    status
                ));
            }

            data.Machines = machines;

            // Отправляем состояние BUI через UserInterfaceComponent
            if (TryComp<UserInterfaceComponent>(uid, out var uiComp))
            {
  //              uiComp.SetState(EnterpriseConsoleUiKey.Key, new EnterpriseConsoleBuiState(machines));
            }

            // Помечаем компонент как грязный, чтобы клиент получил обновление
            Dirty(uid, data);
        }
    }
}
