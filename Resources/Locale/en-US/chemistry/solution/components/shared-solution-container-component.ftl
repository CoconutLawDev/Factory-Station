shared-solution-container-component-on-examine-main-text = Содержит {INDEFINITE($desc)} [color={$color}]{$desc}[/color] { $chemCount ->
    [1] химикат.
    [few] химиката.
   *[many] химикатов.
    }

examinable-solution-has-recognizable-chemicals = Вы можете распознать в растворе {$recognizedString}.
examinable-solution-recognized = [color={$color}]{$chemical}[/color]

examinable-solution-on-examine-volume = Содержимое раствора { $fillLevel ->
    [exact] составляет [color=white]{$current}/{$max} ед[/color].
   *[other] [bold]{ -solution-vague-fill-level(fillLevel: $fillLevel) }[/bold].
}

examinable-solution-on-examine-volume-no-max = Содержимое раствора { $fillLevel ->
    [exact] составляет [color=white]{$current} ед[/color].
   *[other] [bold]{ -solution-vague-fill-level(fillLevel: $fillLevel) }[/bold].
}

examinable-solution-on-examine-volume-puddle = Лужа { $fillLevel ->
    [exact] составляет [color=white]{$current} ед[/color].
    [full] огромна и переполнена!
    [mostlyfull] огромна и переполнена!
    [halffull] глубока и течёт.
    [halfempty] очень глубока.
   *[mostlyempty] собирается в лужи.
    [empty] образует несколько маленьких лужиц.
}

-solution-vague-fill-level =
    { $fillLevel ->
        [full] [color=white]Полна[/color]
        [mostlyfull] [color=#DFDFDF]Почти полна[/color]
        [halffull] [color=#C8C8C8]Наполовину полна[/color]
        [halfempty] [color=#C8C8C8]Наполовину пуста[/color]
        [mostlyempty] [color=#A4A4A4]Почти пуста[/color]
       *[empty] [color=gray]Пуста[/color]
    }
