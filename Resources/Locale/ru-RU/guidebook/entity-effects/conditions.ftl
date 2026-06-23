entity-condition-guidebook-total-damage =
    { $max ->
        [2147483648] у него как минимум {NATURALFIXED($min, 2)} единиц общего урона
        *[other] { $min ->
                    [0] у него не более {NATURALFIXED($max, 2)} единиц общего урона
                    *[other] у него от {NATURALFIXED($min, 2)} до {NATURALFIXED($max, 2)} единиц общего урона
                 }
    }

entity-condition-guidebook-type-damage =
    { $max ->
        [2147483648] у него как минимум {NATURALFIXED($min, 2)} единиц урона типа {$type}
        *[other] { $min ->
                    [0] у него не более {NATURALFIXED($max, 2)} единиц урона типа {$type}
                    *[other] у него от {NATURALFIXED($min, 2)} до {NATURALFIXED($max, 2)} единиц урона типа {$type}
                 }
    }

entity-condition-guidebook-group-damage =
    { $max ->
        [2147483648] у него как минимум {NATURALFIXED($min, 2)} единиц урона типа {$type}
        *[other] { $min ->
                    [0] у него не более {NATURALFIXED($max, 2)} единиц урона типа {$type}
                    *[other] у него от {NATURALFIXED($min, 2)} до {NATURALFIXED($max, 2)} единиц урона типа {$type}
                 }
    }

entity-condition-guidebook-total-hunger =
    { $max ->
        [2147483648] у цели как минимум {NATURALFIXED($min, 2)} единиц общего голода
        *[other] { $min ->
                    [0] у цели не более {NATURALFIXED($max, 2)} единиц общего голода
                    *[other] у цели от {NATURALFIXED($min, 2)} до {NATURALFIXED($max, 2)} единиц общего голода
                 }
    }

entity-condition-guidebook-reagent-threshold =
    { $max ->
        [2147483648] там как минимум {NATURALFIXED($min, 2)} ед {$reagent}
        *[other] { $min ->
                    [0] там не более {NATURALFIXED($max, 2)} ед {$reagent}
                    *[other] там от {NATURALFIXED($min, 2)} ед до {NATURALFIXED($max, 2)} ед {$reagent}
                 }
    }

entity-condition-guidebook-mob-state-condition =
    моб находится в состоянии { $state }

entity-condition-guidebook-job-condition =
    должность цели — { $job }

entity-condition-guidebook-solution-temperature =
    температура раствора { $max ->
            [2147483648] составляет как минимум {NATURALFIXED($min, 2)}K
            *[other] { $min ->
                        [0] составляет не более {NATURALFIXED($max, 2)}K
                        *[other] составляет от {NATURALFIXED($min, 2)}K до {NATURALFIXED($max, 2)}K
                     }
    }

entity-condition-guidebook-body-temperature =
    температура тела { $max ->
            [2147483648] составляет как минимум {NATURALFIXED($min, 2)}K
            *[other] { $min ->
                        [0] составляет не более {NATURALFIXED($max, 2)}K
                        *[other] составляет от {NATURALFIXED($min, 2)}K до {NATURALFIXED($max, 2)}K
                     }
    }

entity-condition-guidebook-organ-type =
    метаболизирующий орган { $shouldhave ->
                                [true] является
                                *[false] не является
                           } органом {$name}

entity-condition-guidebook-has-tag =
    цель { $invert ->
                 [true] не имеет
                 *[false] имеет
                } тег {$tag}

entity-condition-guidebook-this-reagent = этот реагент

entity-condition-guidebook-breathing =
    метаболизатор { $isBreathing ->
                [true] дышит нормально
                *[false] задыхается
               }

entity-condition-guidebook-internals =
    метаболизатор { $usingInternals ->
                [true] использует дыхательное оборудование
                *[false] дышит атмосферным воздухом
               }
