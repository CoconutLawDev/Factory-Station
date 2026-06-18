# ban
cmd-ban-desc = Банит кого-либо
cmd-ban-help = Использование: ban <name or user ID> <reason> [duration in minutes, leave out or 0 for permanent ban]
cmd-ban-player = Не удаётся найти игрока с таким именем.
cmd-ban-invalid-minutes = {$minutes} — неверное количество минут!
cmd-ban-invalid-severity = {$severity} — неверная серьёзность!
cmd-ban-invalid-arguments = Неверное количество аргументов
cmd-ban-hint = <name/user ID>
cmd-ban-hint-reason = <reason>
cmd-ban-hint-duration = [duration]
cmd-ban-hint-severity = [severity]

cmd-ban-hint-duration-1 = Навсегда
cmd-ban-hint-duration-2 = 1 день
cmd-ban-hint-duration-3 = 3 дня
cmd-ban-hint-duration-4 = 1 неделя
cmd-ban-hint-duration-5 = 2 недели
cmd-ban-hint-duration-6 = 1 месяц

# ban panel
cmd-banpanel-desc = Открывает панель банов
cmd-banpanel-help = Использование: banpanel [name or user guid]
cmd-banpanel-server = Эту команду нельзя использовать из консоли сервера
cmd-banpanel-player-err = Указанный игрок не найден

# listbans
cmd-banlist-desc = Показывает активные баны пользователя.
cmd-banlist-help = Использование: banlist <name or user ID>
cmd-banlist-empty = Активных банов для {$user} не найдено
cmd-banlist-hint = <name/user ID>

cmd-ban_exemption_update-desc = Устанавливает освобождение от определённого типа бана для игрока.
cmd-ban_exemption_update-help = Использование: ban_exemption_update <player> <flag> [<flag> [...]]
    Укажите несколько флагов, чтобы дать игроку несколько флагов освобождения от бана.
    Чтобы удалить все освобождения, выполните эту команду и укажите "None" в качестве единственного флага.

cmd-ban_exemption_update-nargs = Ожидается как минимум 2 аргумента
cmd-ban_exemption_update-locate = Не удаётся найти игрока '{$player}'.
cmd-ban_exemption_update-invalid-flag = Неверный флаг '{$flag}'.
cmd-ban_exemption_update-success = Флаги освобождения от бана обновлены для '{$player}' ({$uid}).
cmd-ban_exemption_update-arg-player = <player>
cmd-ban_exemption_update-arg-flag = <flag>

cmd-ban_exemption_get-desc = Показывает освобождения от бана для определённого игрока.
cmd-ban_exemption_get-help = Использование: ban_exemption_get <player>

cmd-ban_exemption_get-nargs = Ожидается ровно 1 аргумент
cmd-ban_exemption_get-none = Пользователь не освобождён ни от каких банов.
cmd-ban_exemption_get-show = Пользователь освобождён от следующих флагов бана: {$flags}.
cmd-ban_exemption_get-arg-player = <player>

# Ban panel
ban-panel-title = Панель банов
ban-panel-player = Игрок
ban-panel-ip = IP
ban-panel-hwid = HWID
ban-panel-reason = Причина
ban-panel-last-conn = Использовать IP и HWID из последнего подключения?
ban-panel-submit = Забанить
ban-panel-confirm = Вы уверены?
ban-panel-tabs-basic = Основная информация
ban-panel-tabs-reason = Причина
ban-panel-tabs-players = Список игроков
ban-panel-tabs-role = Информация о бане роли
ban-panel-no-data = Вы должны указать либо пользователя, IP, либо HWID для бана
ban-panel-invalid-ip = Не удалось распознать IP-адрес. Пожалуйста, попробуйте снова
ban-panel-select = Выберите тип
ban-panel-server = Бан сервера
ban-panel-role = Бан роли
ban-panel-minutes = Минуты
ban-panel-hours = Часы
ban-panel-days = Дни
ban-panel-weeks = Недели
ban-panel-months = Месяцы
ban-panel-years = Годы
ban-panel-permanent = Навсегда
ban-panel-ip-hwid-tooltip = Оставьте пустым и отметьте флажок ниже, чтобы использовать данные последнего подключения
ban-panel-severity = Серьёзность:
ban-panel-erase = Стереть сообщения чата и игрока из раунда
ban-panel-expiry-error = ошибка

# Ban string
server-ban-string = {$admin} создал {$severity} бан сервера, истекающий {$expires}, для [{$name}, {$ip}, {$hwid}], причина: {$reason}
server-ban-string-no-pii = {$admin} создал {$severity} бан сервера, истекающий {$expires}, для {$name}, причина: {$reason}
server-ban-string-never = никогда

# Kick on ban
ban-kick-reason = Вы были забанены
