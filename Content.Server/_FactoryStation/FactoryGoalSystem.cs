using System.Collections.Generic;
using System.Linq;
using Content.Server.Access.Systems;
using Content.Server.Chat.Systems;
using Content.Server.Popups;
using Content.Shared.Access;
using Content.Shared.Access.Components;
using Content.Shared.Access.Systems;
using Content.Shared.FactoryStation;
using Content.Shared.FactoryStation.Components;
using Content.Shared.FactoryStation.Messages;
using Content.Shared.FactoryStation.Prototypes;
using Content.Shared.FactoryStation.States;
using Content.Shared.Interaction;
using Content.Shared.Stacks;
using Content.Shared.UserInterface;
using Robust.Server.GameObjects;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Server.FactoryStation;

public sealed partial class FactoryGoalSystem : EntitySystem
{
    [Dependency] private IPrototypeManager _protoMan = default!;
    [Dependency] private IRobustRandom _random = default!;
    [Dependency] private AccessReaderSystem _accessReader = default!;
    [Dependency] private ChatSystem _chat = default!;
    [Dependency] private PopupSystem _popup = default!;
    [Dependency] private UserInterfaceSystem _ui = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<FactoryGoalConsoleComponent, ComponentStartup>(
            OnConsoleStartup);

        SubscribeLocalEvent<FactoryGoalConsoleComponent, FactoryGoalSelectMessage>(
            OnGoalSelected);

        SubscribeLocalEvent<FactoryGoalPadComponent, InteractUsingEvent>(
            OnPadInteract);
    }

    private void OnConsoleStartup(
        EntityUid uid,
        FactoryGoalConsoleComponent component,
        ComponentStartup args)
    {
        var goalComp = EnsureComp<FactoryGoalComponent>(uid);

        // Генерируем цели только один раз.
        if (goalComp.AvailableGoals.Count == 0)
            GenerateGoals(goalComp);

        UpdateAllUis(goalComp);
    }

    /// <summary>
    /// Выбирает случайную цель без повторений.
    /// Когда все цели использованы — пул сбрасывается.
    /// </summary>
    private ProtoId<FactoryGoalPrototype>? PickRandomGoal(
        List<FactoryGoalPrototype> goals,
        FactoryGoalComponent comp)
    {
        var available = goals
            .Where(g => !comp.UsedGoals.Contains(g.ID))
            .ToList();

        // Если все цели уже использовались — очищаем пул.
        if (available.Count == 0)
        {
            comp.UsedGoals.Clear();
            available = goals.ToList();
        }

        if (available.Count == 0)
            return null;

        var selected = available[_random.Next(available.Count)];

        comp.UsedGoals.Add(selected.ID);

        return selected.ID;
    }

    private void GenerateGoals(FactoryGoalComponent comp)
    {
        var allGoals = _protoMan
            .EnumeratePrototypes<FactoryGoalPrototype>()
            .ToList();

        var lightGoals = allGoals
            .Where(g => g.Difficulty == "Light")
            .ToList();

        var mediumGoals = allGoals
            .Where(g => g.Difficulty == "Medium")
            .ToList();

        var hardGoals = allGoals
            .Where(g => g.Difficulty == "Hard")
            .ToList();

        comp.AvailableGoals.Clear();

        var light = PickRandomGoal(lightGoals, comp);
        var medium = PickRandomGoal(mediumGoals, comp);
        var hard = PickRandomGoal(hardGoals, comp);

        if (light != null)
            comp.AvailableGoals.Add(light.Value);

        if (medium != null)
            comp.AvailableGoals.Add(medium.Value);

        if (hard != null)
            comp.AvailableGoals.Add(hard.Value);
    }

    private string GetDifficultyColor(string? difficulty)
    {
        return difficulty switch
        {
            "Light" => "#5EFF7A",
            "Medium" => "#FFD95E",
            "Hard" => "#FF6B6B",
            _ => "#FFFFFF"
        };
    }

    private string GetDifficultyName(string? difficulty)
    {
        return difficulty switch
        {
            "Light" => "ЛЁГКИЙ",
            "Medium" => "СРЕДНИЙ",
            "Hard" => "КРИТИЧЕСКИЙ",
            _ => "НЕИЗВЕСТНЫЙ"
        };
    }

    private void OnGoalSelected(
        EntityUid uid,
        FactoryGoalConsoleComponent component,
        FactoryGoalSelectMessage args)
    {
        if (args.GoalId == null)
            return;

        if (!TryComp<FactoryGoalComponent>(uid, out var goalComp))
            return;

        var user = args.Actor;

        if (TryComp<AccessReaderComponent>(uid, out var access))
        {
            if (!_accessReader.IsAllowed(user, uid, access))
            {
                _popup.PopupEntity(
                    "У вас нет доступа для выбора промышленного контракта.",
                    uid,
                    user);

                return;
            }
        }

        if (!goalComp.AvailableGoals.Contains(args.GoalId))
            return;

        goalComp.CurrentGoal = args.GoalId;
        goalComp.CurrentProgress = 0;

        string? goalName = null;
        string? difficulty = null;
        var requiredAmount = 0;

        if (_protoMan.TryIndex<FactoryGoalPrototype>(
                args.GoalId,
                out var proto))
        {
            goalName = proto.Name;
            difficulty = proto.Difficulty;
            requiredAmount = proto.RequiredAmount;
        }

        var color = GetDifficultyColor(difficulty);
        var difficultyName = GetDifficultyName(difficulty);

        var announcement =
             $"ВНИМАНИЕ: Центральное Командование санкционировало новый промышленный контракт.\n\n" +

             $"Уровень приоритета: {difficultyName}\n" +
             $"Контракт: {goalName ?? args.GoalId}\n" +
             $"Требуемый объём поставки: {requiredAmount}\n\n" +

             $"Станции предписывается немедленно приступить к производству и доставке указанных ресурсов.\n" +

             $"Невыполнение квоты может негативно сказаться на рейтинге станции.";


        _chat.DispatchStationAnnouncement(
            source: uid,
            message: announcement,
            sender: "Центральное Командование",
            playDefaultSound: true);

        UpdateAllUis(goalComp);
    }

    private void OnPadInteract(
        EntityUid uid,
        FactoryGoalPadComponent component,
        InteractUsingEvent args)
    {
        if (args.Handled)
            return;

        var query = EntityQueryEnumerator<
            FactoryGoalConsoleComponent,
            FactoryGoalComponent>();

        if (!query.MoveNext(out var consoleUid, out _, out var goalComp))
            return;

        if (goalComp.CurrentGoal == null)
            return;

        if (!_protoMan.TryIndex(
                goalComp.CurrentGoal,
                out FactoryGoalPrototype? goalProto))
            return;

        var itemProto = MetaData(args.Used).EntityPrototype?.ID;

        if (itemProto == null)
            return;

        // Предмет не подходит под текущий контракт.
        if (itemProto != goalProto.RequiredItem)
            return;

        var amount = 1;

        // Поддержка стаковых предметов.
        if (TryComp<StackComponent>(args.Used, out var stack))
            amount = stack.Count;

        Del(args.Used);

        goalComp.CurrentProgress += amount;

        // Контракт выполнен.
        if (goalComp.CurrentProgress >= goalProto.RequiredAmount)
        {
            var completeColor = GetDifficultyColor(goalProto.Difficulty);
            var completeDifficulty = GetDifficultyName(goalProto.Difficulty);

            var completionAnnouncement =
                $"Центральное Командование подтверждает успешное выполнение промышленного контракта.\n\n" +

                $"Контракт: {goalProto.Name}\n" +
                $"Уровень приоритета: {completeDifficulty}\n\n" +

                $"Поставка зарегистрирована и внесена в централизованный производственный реестр NanoTrasen.\n" +

                $"{goalProto.RewardMessage}\n\n" +

                $"Рейтинг станции повышен.";

            _chat.DispatchStationAnnouncement(
                source: uid,
                message: completionAnnouncement,
                sender: "Центральное Командование",
                playDefaultSound: true);

            // Удаляем выполненную цель.
            goalComp.AvailableGoals.Remove(goalComp.CurrentGoal.Value);

            // Ищем новую цель такой же сложности.
            var sameDifficultyGoals = _protoMan
                .EnumeratePrototypes<FactoryGoalPrototype>()
                .Where(g => g.Difficulty == goalProto.Difficulty)
                .ToList();

            var replacement = PickRandomGoal(
                sameDifficultyGoals,
                goalComp);

            if (replacement != null)
                goalComp.AvailableGoals.Add(replacement.Value);

            goalComp.CurrentGoal = null;
            goalComp.CurrentProgress = 0;
        }

        UpdateAllUis(goalComp);

        args.Handled = true;
    }

    private void UpdateUi(
        EntityUid uid,
        FactoryGoalComponent comp)
    {
        string? goalName = null;
        string? difficulty = null;
        var requiredAmount = 0;

        if (comp.CurrentGoal != null &&
            _protoMan.TryIndex(comp.CurrentGoal, out var proto))
        {
            goalName = proto.Name;
            difficulty = proto.Difficulty;
            requiredAmount = proto.RequiredAmount;
        }

        var state = new FactoryGoalUpdateState(
            comp.CurrentGoal?.Id,
            comp.CurrentProgress,
            comp.AvailableGoals.Select(x => x.Id).ToList(),
            goalName,
            difficulty,
            requiredAmount);

        _ui.SetUiState(
            uid,
            FactoryGoalConsoleUiKey.Key,
            state);
    }

    private void UpdateAllUis(FactoryGoalComponent comp)
    {
        string? goalName = null;
        string? difficulty = null;
        var requiredAmount = 0;

        if (comp.CurrentGoal != null &&
            _protoMan.TryIndex(comp.CurrentGoal, out var proto))
        {
            goalName = proto.Name;
            difficulty = proto.Difficulty;
            requiredAmount = proto.RequiredAmount;
        }

        var state = new FactoryGoalUpdateState(
            comp.CurrentGoal?.Id,
            comp.CurrentProgress,
            comp.AvailableGoals.Select(x => x.Id).ToList(),
            goalName,
            difficulty,
            requiredAmount);

        // Обновляем все консоли.
        var consoleQuery = EntityQueryEnumerator<
            FactoryGoalConsoleComponent,
            UserInterfaceComponent>();

        while (consoleQuery.MoveNext(out var uid, out _, out _))
        {
            _ui.SetUiState(
                uid,
                FactoryGoalConsoleUiKey.Key,
                state);
        }

        // Обновляем все дисплеи.
        var displayQuery = EntityQueryEnumerator<
            FactoryGoalDisplayComponent,
            UserInterfaceComponent>();

        while (displayQuery.MoveNext(out var uid, out _, out _))
        {
            _ui.SetUiState(
                uid,
                FactoryGoalDisplayUiKey.Key,
                state);
        }
    }
}
