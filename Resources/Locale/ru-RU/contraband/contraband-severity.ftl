contraband-examine-text-Minor =
    { $type ->
        *[item] [color={$color}]Этот предмет считается незначительной контрабандой.[/color]
        [reagent] [color={$color}]Этот реагент считается незначительной контрабандой.[/color]
    }

contraband-examine-text-Restricted =
    { $type ->
        *[item] [color={$color}]Этот предмет имеет ведомственное ограничение.[/color]
        [reagent] [color={$color}]Этот реагент имеет ведомственное ограничение.[/color]
    }

contraband-examine-text-Restricted-department =
    { $type ->
        *[item] [color={$color}]Этот предмет ограничен для {$departments} и может считаться контрабандой.[/color]
        [reagent] [color={$color}]Этот реагент ограничен для {$departments} и может считаться контрабандой.[/color]
    }

contraband-examine-text-Major =
    { $type ->
        *[item] [color={$color}]Этот предмет считается крупной контрабандой.[/color]
        [reagent] [color={$color}]Этот реагент считается крупной контрабандой.[/color]
    }

contraband-examine-text-GrandTheft =
    { $type ->
        *[item] [color={$color}]Этот предмет является ценной целью для агентов Синдиката![/color]
        [reagent] [color={$color}]Этот реагент является ценной целью для агентов Синдиката![/color]
    }

contraband-examine-text-Highly-Illegal =
    { $type ->
        *[item] [color={$color}]Этот предмет является особо опасной контрабандой![/color]
        [reagent] [color={$color}]Этот реагент является особо опасной контрабандой![/color]
    }

contraband-examine-text-Syndicate =
    { $type ->
        *[item] [color={$color}]Этот предмет является особо опасной контрабандой Синдиката![/color]
        [reagent] [color={$color}]Этот реагент является особо опасной контрабандой Синдиката![/color]
    }

contraband-examine-text-Magical =
    { $type ->
        *[item] [color={$color}]Этот предмет является особо опасной магической контрабандой![/color]
        [reagent] [color={$color}]Этот реагент является особо опасной магической контрабандой![/color]
    }

contraband-examine-text-avoid-carrying-around = [color=red][italic]Возможно, вам стоит избегать открытого ношения этого предмета без уважительной причины.[/italic][/color]
contraband-examine-text-in-the-clear = [color=green][italic]Вы можете открыто носить этот предмет.[/italic][/color]

contraband-examinable-verb-text = Законность
contraband-examinable-verb-message = Проверить законность этого предмета.

contraband-department-plural = {$department}
contraband-job-plural = {$job}
