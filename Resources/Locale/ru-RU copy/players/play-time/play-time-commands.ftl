parse-minutes-fail = Не удаётся распознать «{$minutes}» как минуты
parse-session-fail = Сессия для «{$username}» не найдена

## Role Timer Commands

# - playtime_addoverall
cmd-playtime_addoverall-desc = Добавляет указанное количество минут к общему времени игры игрока
cmd-playtime_addoverall-help = Использование: {$command} <user name> <minutes>
cmd-playtime_addoverall-succeed = Общее время для {$username} увеличено до {TOSTRING($time, "dddd\\:hh\\:mm")}
cmd-playtime_addoverall-arg-user = <user name>
cmd-playtime_addoverall-arg-minutes = <minutes>
cmd-playtime_addoverall-error-args = Ожидается ровно два аргумента

# - playtime_addrole
cmd-playtime_addrole-desc = Добавляет указанное количество минут к времени игры игрока за определённую роль
cmd-playtime_addrole-help = Использование: {$command} <user name> <role> <minutes>
cmd-playtime_addrole-succeed = Время игры за роль для {$username} / '{$role}' увеличено до {TOSTRING($time, "dddd\\:hh\\:mm")}
cmd-playtime_addrole-arg-user = <user name>
cmd-playtime_addrole-arg-role = <role>
cmd-playtime_addrole-arg-minutes = <minutes>
cmd-playtime_addrole-error-args = Ожидается ровно три аргумента

# - playtime_getoverall
cmd-playtime_getoverall-desc = Получает общее время игры игрока
cmd-playtime_getoverall-help = Использование: {$command} <user name>
cmd-playtime_getoverall-success = Общее время для {$username}: {TOSTRING($time, "dddd\\:hh\\:mm")}.
cmd-playtime_getoverall-arg-user = <user name>
cmd-playtime_getoverall-error-args = Ожидается ровно один аргумент

# - GetRoleTimer
cmd-playtime_getrole-desc = Получает все или один таймер роли игрока
cmd-playtime_getrole-help = Использование: {$command} <user name> [role]
cmd-playtime_getrole-no = Таймеры ролей не найдены
cmd-playtime_getrole-role = Роль: {$role}, Время: {$time}
cmd-playtime_getrole-overall = Общее время: {$time}
cmd-playtime_getrole-succeed = Время игры для {$username}: {TOSTRING($time, "dddd\\:hh\\:mm")}.
cmd-playtime_getrole-arg-user = <user name>
cmd-playtime_getrole-arg-role = <role|'Overall'>
cmd-playtime_getrole-error-args = Ожидается один или два аргумента

# - playtime_save
cmd-playtime_save-desc = Сохраняет время игры игрока в базу данных
cmd-playtime_save-help = Использование: {$command} <user name>
cmd-playtime_save-succeed = Время игры для {$username} сохранено
cmd-playtime_save-arg-user = <user name>
cmd-playtime_save-error-args = Ожидается ровно один аргумент

## 'playtime_flush' command'

cmd-playtime_flush-desc = Сбрасывает активные трекеры в хранилище времени игры.
cmd-playtime_flush-help = Использование: {$command} [user name]
    Это вызывает сброс только во внутреннее хранилище, немедленного сброса в БД не происходит.
    Если указан пользователь, сбрасывается только его данные.

cmd-playtime_flush-error-args = Ожидается ноль или один аргумент
cmd-playtime_flush-arg-user = [user name]
