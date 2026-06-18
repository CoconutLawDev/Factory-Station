ore-silo-ui-title = Материальный силомат
ore-silo-ui-label-clients = Механизмы
ore-silo-ui-label-mats = Материалы
ore-silo-ui-itemlist-entry = {$linked ->
    [true] {"[Подключён] "}
    *[False] {""}
} {$name} ({$beacon}) {$inRange ->
    [true] {""}
    *[false] (Вне зоны действия)
}
