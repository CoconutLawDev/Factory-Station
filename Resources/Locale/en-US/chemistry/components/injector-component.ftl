## UI

injector-volume-transfer-label = Объём: [color=white]{$currentVolume}/{$totalVolume} ед[/color]
    Режим: [color=white]{$modeString}[/color] ([color=white]{$transferVolume} ед[/color])
injector-volume-label = Объём: [color=white]{$currentVolume}/{$totalVolume} ед[/color]
    Режим: [color=white]{$modeString}[/color]
injector-toggle-verb-text = Переключить режим инжектора

## Entity

injector-component-inject-mode-name = впрыск
injector-component-draw-mode-name = забор
injector-component-dynamic-mode-name = динамический
injector-component-mode-changed-text = Теперь {$mode}
injector-component-transfer-success-message = Вы переливаете {$amount} ед в {THE($target)}.
injector-component-transfer-success-message-self = Вы переливаете {$amount} ед в себя.
injector-component-inject-success-message = Вы впрыскиваете {$amount} ед в {THE($target)}!
injector-component-inject-success-message-self = Вы впрыскиваете {$amount} ед в себя!
injector-component-draw-success-message = Вы забираете {$amount} ед из {THE($target)}.
injector-component-draw-success-message-self = Вы забираете {$amount} ед из себя.

## Fail Messages

injector-component-target-already-full-message = {CAPITALIZE($target)} уже заполнен!
injector-component-target-already-full-message-self = Вы уже заполнены!
injector-component-target-is-empty-message = {CAPITALIZE($target)} пуст!
injector-component-target-is-empty-message-self = Вы пусты!
injector-component-cannot-toggle-draw-message = Слишком много для забора!
injector-component-cannot-toggle-inject-message = Нечего впрыскивать!
injector-component-cannot-toggle-dynamic-message = Невозможно переключить динамический режим!
injector-component-empty-message = {CAPITALIZE($injector)} пуст!
injector-component-blocked-user = Защитное снаряжение блокирует впрыск!
injector-component-blocked-other = {CAPITALIZE(POSS-ADJ($target))} броня заблокировала впрыск {THE($user)}!
injector-component-cannot-transfer-message = Вы не можете перелить в {THE($target)}!
injector-component-cannot-transfer-message-self = Вы не можете перелить в себя!
injector-component-cannot-inject-message = Вы не можете впрыснуть в {THE($target)}!
injector-component-cannot-inject-message-self = Вы не можете впрыснуть в себя!
injector-component-cannot-draw-message = Вы не можете забрать из {THE($target)}!
injector-component-cannot-draw-message-self = Вы не можете забрать из себя!
injector-component-ignore-mobs = Этот инжектор может взаимодействовать только с контейнерами!

## mob-inject doafter messages

injector-component-needle-injecting-user = Вы начинаете вводить иглу.
injector-component-needle-injecting-target = {CAPITALIZE($user)} пытается ввести в вас иглу!
injector-component-needle-drawing-user = Вы начинаете вынимать иглу.
injector-component-needle-drawing-target = {CAPITALIZE($user)} пытается использовать иглу, чтобы забрать у вас!
injector-component-spray-injecting-user = Вы начинаете готовить распылительную насадку.
injector-component-spray-injecting-target = {CAPITALIZE($user)} пытается надеть на вас распылительную насадку!

## Target Popup Success messages
injector-component-feel-prick-message = Вы чувствуете маленький укол!
