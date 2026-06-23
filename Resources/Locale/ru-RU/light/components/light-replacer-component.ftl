### Interaction Messages

# Shown when player tries to replace light, but there is no lights left
comp-light-replacer-missing-light = В {THE($light-replacer)} не осталось ламп.

# Shown when player inserts light bulb inside light replacer
comp-light-replacer-insert-light = Вы вставляете {$bulb} в {THE($light-replacer)}.

# Shown when player tries to insert in light replacer brolen light bulb
comp-light-replacer-insert-broken-light = Вы не можете вставлять разбитые лампы!

# Shown when player refill light from light box
comp-light-replacer-refill-from-storage = Вы заправляете {THE($light-replacer)}.

### Examine

comp-light-replacer-no-lights = Он пуст.
comp-light-replacer-has-lights = Он содержит следующее:
comp-light-replacer-light-listing = {$amount ->
    [one] [color=yellow]{$amount}[/color] [color=gray]{$name}[/color]
    [few] [color=yellow]{$amount}[/color] [color=gray]{$name}[/color]
   *[many] [color=yellow]{$amount}[/color] [color=gray]{$name}[/color]
}
