## Survivor

roles-antag-survivor-name = Выживший
# It's a Halo reference
roles-antag-survivor-objective = Текущая цель: Выжить

survivor-role-greeting =
    Вы — Выживший. Прежде всего вам нужно вернуться в Центральное Командование живым.
    Соберите как можно больше огневой мощи, чтобы гарантировать своё выживание.
    Не доверяйте никому.

survivor-round-end-dead-count =
{
    $deadCount ->
        [one] [color=red]{$deadCount}[/color] выживший погиб.
        [few] [color=red]{$deadCount}[/color] выживших погибло.
       *[many] [color=red]{$deadCount}[/color] выживших погибло.
}

survivor-round-end-alive-count =
{
    $aliveCount ->
        [one] [color=yellow]{$aliveCount}[/color] выживший остался на станции.
        [few] [color=yellow]{$aliveCount}[/color] выживших осталось на станции.
       *[many] [color=yellow]{$aliveCount}[/color] выживших осталось на станции.
}

survivor-round-end-alive-on-shuttle-count =
{
    $aliveCount ->
        [one] [color=green]{$aliveCount}[/color] выживший выбрался живым.
        [few] [color=green]{$aliveCount}[/color] выживших выбралось живыми.
       *[many] [color=green]{$aliveCount}[/color] выживших выбралось живыми.
}

## Wizard

objective-issuer-swf = [color=turquoise]Федерация космических волшебников[/color]

wizard-title = Волшебник
wizard-description = На станции волшебник! Никогда не знаешь, что они могут сделать.

roles-antag-wizard-name = Волшебник
roles-antag-wizard-objective = Преподать им урок, который они никогда не забудут.

wizard-role-greeting =
    Время волшебника, огненный шар!
    Между Федерацией космических волшебников и NanoTrasen возникла напряжённость. Вы были выбраны Федерацией космических волшебников, чтобы навестить станцию и «напомнить им», почему с заклинателями не стоит шутить.
    Устройте хаос и разрушения! Что именно вы будете делать — решать вам, но помните, что Федерация волшебников хочет, чтобы вы выбрались живым.

wizard-round-end-name = волшебник

## TODO: Wizard Apprentice (Coming sometime post-wizard release)
