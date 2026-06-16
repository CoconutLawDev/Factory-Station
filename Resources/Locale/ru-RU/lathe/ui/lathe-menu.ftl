lathe-menu-title = Меню станка
lathe-menu-queue = Очередь
lathe-menu-server-list = Список серверов
lathe-menu-sync = Синхронизация
lathe-menu-search-designs = Поиск чертежей
lathe-menu-category-all = Всё
lathe-menu-search-filter = Фильтр:
lathe-menu-amount = Количество:
lathe-menu-recipe-count = { $count ->
    [one] {$count} рецепт
    [few] {$count} рецепта
   *[many] {$count} рецептов
}
lathe-menu-reagent-slot-examine = Сбоку есть отверстие для стакана.
lathe-reagent-dispense-no-container = Жидкость выливается из {THE($name)} на пол!
lathe-menu-result-reagent-display = {$reagent} ({$amount} ед)
lathe-menu-material-display = {$material} ({$amount})
lathe-menu-tooltip-display = {$amount} из {$material}
lathe-menu-description-display = [italic]{$description}[/italic]
lathe-menu-material-amount = { $amount ->
    [1] {NATURALFIXED($amount, 2)} {$unit}
    *[other] {NATURALFIXED($amount, 2)} {$unit}
}
lathe-menu-material-amount-missing = { $amount ->
    [1] {NATURALFIXED($amount, 2)} {$unit} из {$material} ([color=red]не хватает {NATURALFIXED($missingAmount, 2)} {$unit}[/color])
    *[other] {NATURALFIXED($amount, 2)} {$unit} из {$material} ([color=red]не хватает {NATURALFIXED($missingAmount, 2)} {$unit}[/color])
}
lathe-menu-no-materials-message = Материалы не загружены.
lathe-menu-silo-linked-message = Силомат подключён
lathe-menu-fabricating-message = Производство...
lathe-menu-materials-title = Материалы
lathe-menu-queue-title = Очередь производства
lathe-menu-delete-fabricating-tooltip = Отменить печать текущего предмета.
lathe-menu-delete-item-tooltip = Отменить печать этой партии.
lathe-menu-move-up-tooltip = Переместить эту партию вперёд в очереди.
lathe-menu-move-down-tooltip = Переместить эту партию назад в очереди.
lathe-menu-item-single = {$index}. {$name}
lathe-menu-item-batch = {$index}. {$name} ({$printed}/{$total})
