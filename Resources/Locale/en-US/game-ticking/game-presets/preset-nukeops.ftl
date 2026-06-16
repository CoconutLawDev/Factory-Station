nukeops-title = Ядерные оперативники
nukeops-description = Ядерные оперативники нацелились на станцию. Постарайтесь помешать им активировать ядерную бомбу, защищая диск!

nukeops-welcome =
    Вы — ядерный оперативник. Ваша цель — взорвать {$station}, превратив её в груду обломков. Ваши боссы из Синдиката предоставили вам всё необходимое для выполнения задачи.
    Операция {$name} началась! Смерть Нанотрейзен!
nukeops-briefing = Ваши задачи просты: доставить боезаряд и убраться до его взрыва. Приступить к миссии.

nukeops-opsmajor = [color=crimson]Полная победа Синдиката![/color]
nukeops-opsminor = [color=crimson]Незначительная победа Синдиката![/color]
nukeops-neutral = [color=yellow]Ничья![/color]
nukeops-crewminor = [color=green]Незначительная победа экипажа![/color]
nukeops-crewmajor = [color=green]Полная победа экипажа![/color]

nukeops-cond-nukeexplodedoncorrectstation = Ядерные оперативники смогли взорвать станцию.
nukeops-cond-nukeexplodedonnukieoutpost = Аванпост ядерных оперативников был уничтожен ядерным взрывом!
nukeops-cond-nukeexplodedonincorrectlocation = Ядерная бомба взорвалась за пределами станции.
nukeops-cond-nukeactiveinstation = Ядерная бомба была активирована на станции.
nukeops-cond-nukeactiveatcentcom = Ядерная бомба была активирована и доставлена в Центральное Командование!
nukeops-cond-nukediskoncentcom = Экипаж сбежал с диском аутентификации.
nukeops-cond-nukedisknotoncentcom = Экипаж оставил диск аутентификации.
nukeops-cond-nukiesabandoned = Ядерные оперативники были брошены.
nukeops-cond-allnukiesdead = Все ядерные оперативники погибли.
nukeops-cond-somenukiesalive = Некоторые ядерные оперативники погибли.
nukeops-cond-allnukiesalive = Ни один из ядерных оперативников не погиб.

nukeops-disk-location-title = Конечное местонахождение диска:
nukeops-disk-carried-by = {" "}несёт [color=White]{$name}[/color], [color=orange]{$job}[/color], {$location} { $user ->
    [unknown] { "" }
    *[other] ([color=gray]{$user}[/color])
}

storage-hierarchy-list = { $items-left ->
  [0] { $existing-text } { $item },
  *[other] { $existing-text } { $item }, в
}

nukeops-list-start = Ядерные оперативники были:
nukeops-list-name = - [color=White]{$name}[/color]
nukeops-list-name-user = - [color=White]{$name}[/color] ([color=gray]{$user}[/color])
nukeops-not-enough-ready-players = Недостаточно игроков готово к игре! Было готово {$readyPlayersCount} из {$minimumPlayers} необходимых. Не удаётся запустить Ядерных оперативников.
nukeops-no-one-ready = Нет готовых игроков! Не удаётся запустить Ядерных оперативников.

nukeops-role-commander = Командир
nukeops-role-agent = Санитар
nukeops-role-operator = Оперативник
