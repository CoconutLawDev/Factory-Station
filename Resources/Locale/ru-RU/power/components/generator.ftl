generator-clogged = {CAPITALIZE($generator)} резко выключается!

portable-generator-verb-start = Запустить генератор
portable-generator-verb-start-msg-unreliable = Запустите генератор. Это может занять несколько попыток.
portable-generator-verb-start-msg-reliable = Запустите генератор.
portable-generator-verb-start-msg-unanchored = Генератор должен быть сначала закреплён!
portable-generator-verb-stop = Остановить генератор
portable-generator-start-fail = Вы дёргаете шнур, но он не заводится.
portable-generator-start-success = Вы дёргаете шнур, и он оживает.

portable-generator-ui-title = Переносной генератор
portable-generator-ui-status-stopped = Остановлен:
portable-generator-ui-status-starting = Запускается:
portable-generator-ui-status-running = Работает:
portable-generator-ui-start = Запустить
portable-generator-ui-stop = Остановить
portable-generator-ui-target-power-label = Целевая мощность (кВт):
portable-generator-ui-efficiency-label = Эффективность:
portable-generator-ui-fuel-use-label = Расход топлива:
portable-generator-ui-fuel-left-label = Осталось топлива:
portable-generator-ui-clogged = Обнаружены загрязнения в топливном баке!
portable-generator-ui-eject = Извлечь
portable-generator-ui-eta = (~{ $minutes } мин)
portable-generator-ui-unanchored = Не закреплён
portable-generator-ui-current-output = Текущая выходная мощность: {$voltage}
portable-generator-ui-network-stats = Сеть:
portable-generator-ui-network-stats-value = { POWERWATTS($supply) } / { POWERWATTS($load) }
portable-generator-ui-network-stats-not-connected = Не подключено

power-switchable-generator-examine = Выходная мощность установлена на {$voltage}.
power-switchable-generator-switched = Выходная мощность переключена на {$voltage}!

power-switchable-voltage = { $voltage ->
    [HV] [color=orange]ВН[/color]
    [MV] [color=yellow]СН[/color]
    *[LV] [color=green]НН[/color]
}
power-switchable-switch-voltage = Переключить на {$voltage}

fuel-generator-verb-disable-on = Сначала выключите генератор!
