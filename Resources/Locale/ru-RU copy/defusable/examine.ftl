defusable-examine-defused = {CAPITALIZE($name)} [color=lime]обезврежено[/color].
defusable-examine-live = {CAPITALIZE($name)} [color=red]тикает[/color], осталось [color=red]{$time}[/color] секунд.
defusable-examine-live-display-off = {CAPITALIZE($name)} [color=red]тикает[/color], но таймер, похоже, выключен.
defusable-examine-inactive = {CAPITALIZE($name)} [color=lime]неактивно[/color], но всё ещё может быть активировано.
defusable-examine-bolts = Фиксаторы {$down ->
[true] [color=red]опущены[/color]
*[false] [color=green]подняты[/color]
}.
