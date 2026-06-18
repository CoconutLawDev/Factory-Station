anomaly-component-contact-damage = Аномалия сжигает вашу кожу!

anomaly-vessel-component-anomaly-assigned = Аномалия привязана к контейнеру.
anomaly-vessel-component-not-assigned = Этот контейнер не привязан ни к какой аномалии. Попробуйте использовать на нём сканер.
anomaly-vessel-component-assigned = Этот контейнер в данный момент привязан к аномалии.

anomaly-particles-delta = Дельта-частицы
anomaly-particles-epsilon = Эпсилон-частицы
anomaly-particles-zeta = Дзета-частицы
anomaly-particles-omega = Омега-частицы
anomaly-particles-sigma = Сигма-частицы

anomaly-scanner-component-scan-complete = Сканирование завершено!

anomaly-scanner-ui-title = Сканер аномалий
anomaly-scanner-no-anomaly = В данный момент аномалия не отсканирована.
anomaly-scanner-severity-percentage = Текущая напряжённость: [color=gray]{$percent}[/color]
anomaly-scanner-severity-percentage-unknown = Текущая напряжённость: [color=red]ОШИБКА[/color]
anomaly-scanner-stability-low = Текущее состояние аномалии: [color=gold]Распад[/color]
anomaly-scanner-stability-medium = Текущее состояние аномалии: [color=forestgreen]Стабильно[/color]
anomaly-scanner-stability-high = Текущее состояние аномалии: [color=crimson]Рост[/color]
anomaly-scanner-stability-unknown = Текущее состояние аномалии: [color=red]ОШИБКА[/color]
anomaly-scanner-point-output = Выход точек: [color=gray]{$point}[/color]
anomaly-scanner-point-output-unknown = Выход точек: [color=red]ОШИБКА[/color]
anomaly-scanner-particle-readout = Анализ реакции частиц:
anomaly-scanner-particle-danger = - [color=crimson]Тип опасности:[/color] {$type}
anomaly-scanner-particle-unstable = - [color=plum]Нестабильный тип:[/color] {$type}
anomaly-scanner-particle-containment = - [color=goldenrod]Тип сдерживания:[/color] {$type}
anomaly-scanner-particle-transformation = - [color=#6b75fa]Тип трансформации:[/color] {$type}
anomaly-scanner-particle-danger-unknown = - [color=crimson]Тип опасности:[/color] [color=red]ОШИБКА[/color]
anomaly-scanner-particle-unstable-unknown = - [color=plum]Нестабильный тип:[/color] [color=red]ОШИБКА[/color]
anomaly-scanner-particle-containment-unknown = - [color=goldenrod]Тип сдерживания:[/color] [color=red]ОШИБКА[/color]
anomaly-scanner-particle-transformation-unknown = - [color=#6b75fa]Тип трансформации:[/color] [color=red]ОШИБКА[/color]
anomaly-scanner-pulse-timer = Время до следующего импульса: [color=gray]{$time}[/color]

anomaly-gorilla-core-slot-name = Ядро аномалии
anomaly-gorilla-charge-none = Внутри нет [bold]ядра аномалии[/bold].
anomaly-gorilla-charge-limit = Осталось [color={$count ->
    [3]green
    [2]yellow
    [1]orange
    [0]red
    *[other]purple
}]{$count} {$count ->
    [one]заряд
    [few]заряда
    *[other]зарядов
}[/color].
anomaly-gorilla-charge-infinite = У неё [color=gold]бесконечные заряды[/color]. [italic]Пока что...[/italic]

anomaly-sync-connected = Аномалия успешно подключена
anomaly-sync-disconnected = Соединение с аномалией потеряно!
anomaly-sync-no-anomaly = Нет аномалий в радиусе действия.
anomaly-sync-examine-connected = [color=darkgreen]Подключено[/color] к аномалии.
anomaly-sync-examine-not-connected = [color=darkred]Не подключено[/color] к аномалии.
anomaly-sync-connect-verb-text = Подключить аномалию
anomaly-sync-connect-verb-message = Подключить ближайшую аномалию к {THE($machine)}.
anomaly-sync-disconnect-verb-text = Отключить аномалию
anomaly-sync-disconnect-verb-message = Отключить подключённую аномалию от {THE($machine)}.

anomaly-generator-ui-title = Генератор аномалий
anomaly-generator-fuel-display = Топливо:
anomaly-generator-cooldown = Перезарядка: [color=gray]{$time}[/color]
anomaly-generator-no-cooldown = Перезарядка: [color=gray]Завершена[/color]
anomaly-generator-yes-fire = Статус: [color=forestgreen]Готов[/color]
anomaly-generator-no-fire = Статус: [color=crimson]Не готов[/color]
anomaly-generator-generate = Сгенерировать аномалию
anomaly-generator-charges = {$charges} {$charges ->
    [one] заряд
    [few] заряда
    *[other] зарядов
}
anomaly-generator-announcement = Аномалия сгенерирована!

anomaly-command-pulse = Посылает импульс к целевой аномалии
anomaly-command-supercritical = Переводит целевую аномалию в сверхкритическое состояние

# Flavor text on the footer
anomaly-generator-flavor-left = Аномалия может появиться внутри оператора.
anomaly-generator-flavor-right = v1.1

anomaly-behavior-unknown = [color=red]ОШИБКА. Невозможно прочитать.[/color]

anomaly-behavior-title = Анализ отклонений поведения:
anomaly-behavior-point = [color=gold]Аномалия производит {$mod}% от стандартного количества очков[/color]

anomaly-behavior-safe = [color=forestgreen]Аномалия чрезвычайно стабильна. Крайне редкие пульсации.[/color]
anomaly-behavior-slow = [color=forestgreen]Частота пульсаций значительно реже.[/color]
anomaly-behavior-light = [color=forestgreen]Мощность пульсаций значительно снижена.[/color]
anomaly-behavior-balanced = Отклонений в поведении не обнаружено.
anomaly-behavior-delayed-force = Частота пульсаций сильно снижена, но их мощность увеличена.
anomaly-behavior-rapid = Частота пульсаций значительно выше, но их сила ослаблена.
anomaly-behavior-reflect = Обнаружено защитное покрытие.
anomaly-behavior-nonsensivity = Обнаружена слабая реакция на частицы.
anomaly-behavior-sensivity = Обнаружена усиленная реакция на частицы.
anomaly-behavior-invisibility = Обнаружено искажение световых волн.
anomaly-behavior-secret = Обнаружены помехи. Некоторые данные невозможно прочитать.
anomaly-behavior-inconstancy = [color=crimson]Обнаружена непостоянство. Типы частиц могут меняться со временем.[/color]
anomaly-behavior-fast = [color=crimson]Частота пульсаций сильно увеличена.[/color]
anomaly-behavior-strenght = [color=crimson]Мощность пульсаций значительно увеличена.[/color]
anomaly-behavior-moving = [color=crimson]Обнаружена нестабильность координат.[/color]
anomaly-secret-admin = [color=red](ОШИБКА)[/color]
