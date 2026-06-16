# Examine text after when they're holding something (in-hand)
comp-hands-examine = { CAPITALIZE($user) } { CONJUGATE-BE($user) } держит { $items }.
comp-hands-examine-empty = { CAPITALIZE($user) } { CONJUGATE-BE($user) } ничего не держит.
comp-hands-examine-wrapper = { INDEFINITE($item) } [color=paleturquoise]{$item}[/color]

hands-system-blocked-by = Заблокировано
