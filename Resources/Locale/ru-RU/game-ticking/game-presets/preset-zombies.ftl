zombie-title = Зомби
zombie-description = Нежить выпущена на станцию! Работайте вместе с экипажем, чтобы пережить вспышку и обезопасить станцию.

zombieteors-title = Зомби-метеоры
zombieteors-description = Нежить выпущена на станцию во время катаклизмического метеоритного дождя! Работайте вместе с остальным экипажем и делайте всё возможное, чтобы выжить!

zombie-not-enough-ready-players = Недостаточно игроков готово к игре! Было готово {$readyPlayersCount} из {$minimumPlayers} необходимых. Не удаётся запустить режим «Зомби».
zombie-no-one-ready = Нет готовых игроков! Не удаётся запустить режим «Зомби».

zombie-patientzero-role-greeting = Вы — первый заражённый. Добудьте припасы и подготовьтесь к своему неизбежному превращению. Ваша цель — захватить станцию, заразив как можно больше людей.
zombie-healing = Вы чувствуете шевеление в вашей плоти
zombie-infection-warning = Вы чувствуете, как вирус зомби захватывает вас
zombie-infection-underway = Ваша кровь начинает густеть

zombie-alone = Вы чувствуете себя совершенно одиноким.

zombie-shuttle-call = Мы зафиксировали, что нежить захватила станцию. Отправляем аварийный шаттл для эвакуации оставшегося персонала.

zombie-round-end-initial-count = { $initialCount ->
    [one] Был один первый заражённый:
    [few] Было {$initialCount} первых заражённых:
   *[many] Было {$initialCount} первых заражённых:
}
zombie-round-end-user-was-initial = - [color=plum]{$name}[/color] ([color=gray]{$username}[/color]) был одним из первых заражённых.

zombie-round-end-amount-none = [color=green]Все зомби были уничтожены![/color]
zombie-round-end-amount-low = [color=green]Почти все зомби были истреблены.[/color]
zombie-round-end-amount-medium = [color=yellow]{$percent}% экипажа превратились в зомби.[/color]
zombie-round-end-amount-high = [color=crimson]{$percent}% экипажа превратились в зомби.[/color]
zombie-round-end-amount-all = [color=darkred]Весь экипаж стал зомби![/color]

zombie-round-end-survivor-count = { $count ->
    [one] Остался всего один выживший:
    [few] Осталось всего {$count} выживших:
   *[many] Осталось всего {$count} выживших:
}
zombie-round-end-user-was-survivor = - [color=White]{$name}[/color] ([color=gray]{$username}[/color]) пережил(а) вспышку.
