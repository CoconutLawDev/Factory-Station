# Loading Screen

replay-loading = Загрузка ({$cur}/{$total})
replay-loading-reading = Чтение файлов
replay-loading-processing = Обработка файлов
replay-loading-spawning = Создание сущностей
replay-loading-initializing = Инициализация сущностей
replay-loading-starting = Запуск сущностей
replay-loading-failed = Не удалось загрузить повтор. Ошибка:
                        {$reason}
replay-loading-retry = Попробовать загрузить с повышенной устойчивостью к ошибкам — МОЖЕТ ВЫЗВАТЬ ОШИБКИ!
replay-loading-cancel = Отмена

# Main Menu
replay-menu-subtext = Клиент повторов
replay-menu-load = Загрузить выбранный повтор
replay-menu-select = Выберите повтор
replay-menu-open = Открыть папку повторов
replay-menu-none = Повторы не найдены.

# Main Menu Info Box
replay-info-title = Информация о повторе
replay-info-none-selected = Повтор не выбран
replay-info-invalid = [color=red]Выбран неверный повтор[/color]
replay-info-info = {"["}color=gray]Выбран:[/color]  {$name} ({$file})
                   {"["}color=gray]Время:[/color]   {$time}
                   {"["}color=gray]ID раунда:[/color]   {$roundId}
                   {"["}color=gray]Длительность:[/color]   {$duration}
                   {"["}color=gray]ForkId:[/color]   {$forkId}
                   {"["}color=gray]Версия:[/color]   {$version}
                   {"["}color=gray]Движок:[/color]   {$engVersion}
                   {"["}color=gray]Хеш типа:[/color]   {$hash}
                   {"["}color=gray]Хеш сборки:[/color]   {$compHash}

# Replay selection window
replay-menu-select-title = Выбор повтора

# Replay related verbs
replay-verb-spectate = Наблюдать

# command
cmd-replay-spectate-help = replay_spectate [опциональная сущность]
cmd-replay-spectate-desc = Прикрепляет или открепляет локального игрока от указанной сущности по uid.
cmd-replay-spectate-hint = Опциональный EntityUid

cmd-replay-toggleui-desc = Переключает интерфейс управления повтором.
