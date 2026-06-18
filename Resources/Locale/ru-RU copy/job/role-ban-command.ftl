### Localization for role ban command

cmd-roleban-desc = Забанить игрока на определённую роль
cmd-roleban-help = Использование: roleban <name or user ID> <job> <reason> [duration in minutes, leave out or 0 for permanent ban]

## Completion result hints
cmd-roleban-hint-1 = <name or user ID>
cmd-roleban-hint-2 = <job>
cmd-roleban-hint-3 = <reason>
cmd-roleban-hint-4 = [duration in minutes, leave out or 0 for permanent ban]
cmd-roleban-hint-5 = [severity]

cmd-roleban-hint-duration-1 = Навсегда
cmd-roleban-hint-duration-2 = 1 день
cmd-roleban-hint-duration-3 = 3 дня
cmd-roleban-hint-duration-4 = 1 неделя
cmd-roleban-hint-duration-5 = 2 недели
cmd-roleban-hint-duration-6 = 1 месяц


### Localization for role unban command

cmd-roleunban-desc = Снять с игрока бан на роль
cmd-roleunban-help = Использование: roleunban <role ban id>
cmd-roleunban-unable-to-parse-id = Не удаётся распознать {$id} как целочисленный id бана.
                                   {$help}

## Completion result hints
cmd-roleunban-hint-1 = <role ban id>


### Localization for roleban list command

cmd-rolebanlist-desc = Показать баны на роли пользователя
cmd-rolebanlist-help = Использование: <name or user ID> [include unbanned]

## Completion result hints
cmd-rolebanlist-hint-1 = <name or user ID>
cmd-rolebanlist-hint-2 = [include unbanned]


cmd-roleban-minutes-parse = {$time} — неверное количество минут.\n{$help}
cmd-roleban-severity-parse = {$severity} — неверная серьёзность.\n{$help}
cmd-roleban-arg-count = Неверное количество аргументов.
cmd-roleban-job-parse = Должность {$job} не существует.
cmd-roleban-name-parse = Не удаётся найти игрока с таким именем.
cmd-roleban-success = Игрок {$target} забанен на роль {$role} по причине: {$reason} {$length}.

cmd-roleban-inf = навсегда
cmd-roleban-until = до {$expires}

# Department bans
cmd-departmentban-desc = Забанить игрока на роли, входящие в отдел
cmd-departmentban-help = Использование: departmentban <name or user ID> <department> <reason> [duration in minutes, leave out or 0 for permanent ban]
