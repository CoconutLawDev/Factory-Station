-create-3rd-person =
    { $chance ->
        [1] Создаёт
        *[other] создают
    }

-cause-3rd-person =
    { $chance ->
        [1] Вызывает
        *[other] вызывают
    }

-satiate-3rd-person =
    { $chance ->
        [1] Утоляет
        *[other] утоляют
    }

entity-effect-guidebook-spawn-entity =
    { $chance ->
        [1] Создаёт
        *[other] создают
    } { $amount ->
        [1] {INDEFINITE($entname)}
        *[other] {$amount} {MAKEPLURAL($entname)}
    }

entity-effect-guidebook-destroy =
    { $chance ->
        [1] Уничтожает
        *[other] уничтожают
    } объект

entity-effect-guidebook-break =
    { $chance ->
        [1] Ломает
        *[other] ломают
    } объект

entity-effect-guidebook-explosion =
    { $chance ->
        [1] Вызывает
        *[other] вызывают
    } взрыв

entity-effect-guidebook-emp =
    { $chance ->
        [1] Вызывает
        *[other] вызывают
    } электромагнитный импульс

entity-effect-guidebook-flash =
    { $chance ->
        [1] Вызывает
        *[other] вызывают
    } ослепляющую вспышку

entity-effect-guidebook-foam-area =
    { $chance ->
        [1] Создаёт
        *[other] создают
    } большое количество пены

entity-effect-guidebook-smoke-area =
    { $chance ->
        [1] Создаёт
        *[other] создают
    } большое количество дыма

entity-effect-guidebook-satiate-thirst =
    { $chance ->
        [1] Утоляет
        *[other] утоляют
    } { $relative ->
        [1] жажду в среднем темпе
        *[other] жажду в {NATURALFIXED($relative, 3)} раза быстрее среднего
    }

entity-effect-guidebook-satiate-hunger =
    { $chance ->
        [1] Утоляет
        *[other] утоляют
    } { $relative ->
        [1] голод в среднем темпе
        *[other] голод в {NATURALFIXED($relative, 3)} раза быстрее среднего
    }

entity-effect-guidebook-health-change =
    { $chance ->
        [1] { $healsordeals ->
                [heals] Лечит
                [deals] Наносит
                *[both] Изменяет здоровье на
             }
        *[other] { $healsordeals ->
                    [heals] лечат
                    [deals] наносят
                    *[both] изменяют здоровье на
                 }
    } { $changes }

entity-effect-guidebook-even-health-change =
    { $chance ->
        [1] { $healsordeals ->
            [heals] Равномерно лечит
            [deals] Равномерно наносит
            *[both] Равномерно изменяет здоровье на
        }
        *[other] { $healsordeals ->
            [heals] равномерно лечат
            [deals] равномерно наносят
            *[both] равномерно изменяют здоровье на
        }
    } { $changes }

entity-effect-guidebook-status-effect-old =
    { $type ->
        [update]{ $chance ->
                    [1] Вызывает
                     *[other] вызывают
                 } {LOC($key)} как минимум на {NATURALFIXED($time, 3)} {MANY("секунду", $time)} без накопления
        [add]   { $chance ->
                    [1] Вызывает
                    *[other] вызывают
                } {LOC($key)} как минимум на {NATURALFIXED($time, 3)} {MANY("секунду", $time)} с накоплением
        [set]  { $chance ->
                    [1] Вызывает
                    *[other] вызывают
                } {LOC($key)} на {NATURALFIXED($time, 3)} {MANY("секунду", $time)} без накопления
        *[remove]{ $chance ->
                    [1] Удаляет
                    *[other] удаляют
                } {NATURALFIXED($time, 3)} {MANY("секунду", $time)} из {LOC($key)}
    }

entity-effect-guidebook-status-effect =
    { $type ->
        [update]{ $chance ->
                    [1] Вызывает
                     *[other] вызывают
                 } {LOC($key)} как минимум на {NATURALFIXED($time, 3)} {MANY("секунду", $time)} без накопления
        [add]   { $chance ->
                    [1] Вызывает
                    *[other] вызывают
                } {LOC($key)} как минимум на {NATURALFIXED($time, 3)} {MANY("секунду", $time)} с накоплением
        [set]  { $chance ->
                    [1] Вызывает
                    *[other] вызывают
                } {LOC($key)} как минимум на {NATURALFIXED($time, 3)} {MANY("секунду", $time)} без накопления
        *[remove]{ $chance ->
                    [1] Удаляет
                    *[other] удаляют
                } {NATURALFIXED($time, 3)} {MANY("секунду", $time)} из {LOC($key)}
    } { $delay ->
        [0] немедленно
        *[other] через {NATURALFIXED($delay, 3)} секунд задержки
    }

entity-effect-guidebook-status-effect-indef =
    { $type ->
        [update]{ $chance ->
                    [1] Вызывает
                    *[other] вызывают
                 } постоянный {LOC($key)}
        [add]   { $chance ->
                    [1] Вызывает
                    *[other] вызывают
                } постоянный {LOC($key)}
        [set]  { $chance ->
                    [1] Вызывает
                    *[other] вызывают
                } постоянный {LOC($key)}
        *[remove]{ $chance ->
                    [1] Удаляет
                    *[other] удаляют
                } {LOC($key)}
    } { $delay ->
        [0] немедленно
        *[other] через {NATURALFIXED($delay, 3)} секунд задержки
    }

entity-effect-guidebook-knockdown =
    { $type ->
        [update]{ $chance ->
                    [1] Вызывает
                    *[other] вызывают
                    } сбивание с ног как минимум на {NATURALFIXED($time, 3)} {MANY("секунду", $time)} без накопления
        [add]   { $chance ->
                    [1] Вызывает
                    *[other] вызывают
                } сбивание с ног как минимум на {NATURALFIXED($time, 3)} {MANY("секунду", $time)} с накоплением
        *[set]  { $chance ->
                    [1] Вызывает
                    *[other] вызывают
                } сбивание с ног как минимум на {NATURALFIXED($time, 3)} {MANY("секунду", $time)} без накопления
        [remove]{ $chance ->
                    [1] Удаляет
                    *[other] удаляют
                } {NATURALFIXED($time, 3)} {MANY("секунду", $time)} сбивания с ног
    }

entity-effect-guidebook-set-solution-temperature-effect =
    { $chance ->
        [1] Устанавливает
        *[other] устанавливают
    } температуру раствора ровно на {NATURALFIXED($temperature, 2)}K

entity-effect-guidebook-adjust-solution-temperature-effect =
    { $chance ->
        [1] { $deltasign ->
                [1] Добавляет
                *[-1] Удаляет
            }
        *[other]
            { $deltasign ->
                [1] добавляют
                *[-1] удаляют
            }
    } тепло из раствора, пока он не достигнет { $deltasign ->
                [1] не более {NATURALFIXED($maxtemp, 2)}K
                *[-1] не менее {NATURALFIXED($mintemp, 2)}K
            }

entity-effect-guidebook-adjust-reagent-reagent =
    { $chance ->
        [1] { $deltasign ->
                [1] Добавляет
                *[-1] Удаляет
            }
        *[other]
            { $deltasign ->
                [1] добавляют
                *[-1] удаляют
            }
    } {NATURALFIXED($amount, 2)} ед {$reagent} { $deltasign ->
        [1] в
        *[-1] из
    } раствора

entity-effect-guidebook-adjust-reagent-group =
    { $chance ->
        [1] { $deltasign ->
                [1] Добавляет
                *[-1] Удаляет
            }
        *[other]
            { $deltasign ->
                [1] добавляют
                *[-1] удаляют
            }
    } {NATURALFIXED($amount, 2)} ед реагентов из группы {$group} { $deltasign ->
            [1] в
            *[-1] из
        } раствора

entity-effect-guidebook-adjust-temperature =
    { $chance ->
        [1] { $deltasign ->
                [1] Добавляет
                *[-1] Удаляет
            }
        *[other]
            { $deltasign ->
                [1] добавляют
                *[-1] удаляют
            }
    } {POWERJOULES($amount)} тепла { $deltasign ->
            [1] в
            *[-1] из
        } тела

entity-effect-guidebook-chem-cause-disease =
    { $chance ->
        [1] Вызывает
        *[other] вызывают
    } болезнь { $disease }

entity-effect-guidebook-chem-cause-random-disease =
    { $chance ->
        [1] Вызывает
        *[other] вызывают
    } болезни { $diseases }

entity-effect-guidebook-jittering =
    { $chance ->
        [1] Вызывает
        *[other] вызывают
    } дрожь

entity-effect-guidebook-clean-bloodstream =
    { $chance ->
        [1] Очищает
        *[other] очищают
    } кровоток от других химикатов

entity-effect-guidebook-cure-disease =
    { $chance ->
        [1] Лечит
        *[other] лечат
    } болезни

entity-effect-guidebook-eye-damage =
    { $chance ->
        [1] { $deltasign ->
                [1] Наносит
                *[-1] Лечит
            }
        *[other]
            { $deltasign ->
                [1] наносят
                *[-1] лечат
            }
    } урон глазам

entity-effect-guidebook-vomit =
    { $chance ->
        [1] Вызывает
        *[other] вызывают
    } рвоту

entity-effect-guidebook-create-gas =
    { $chance ->
        [1] Создаёт
        *[other] создают
    } { $moles } { $moles ->
        [1] моль
        *[other] молей
    } { $gas }

entity-effect-guidebook-drunk =
    { $chance ->
        [1] Вызывает
        *[other] вызывают
    } опьянение

entity-effect-guidebook-electrocute =
    { $chance ->
        [1] { $stuns ->
            [true] Бьёт током
            *[false] Шокирует
            }
        *[other] { $stuns ->
            [true] бьют током
            *[false] шокируют
            }
    } метаболизатор в течение {NATURALFIXED($time, 3)} {MANY("секунды", $time)}

entity-effect-guidebook-emote =
    { $chance ->
        [1] Заставит
        *[other] заставят
    } метаболизатор [bold][color=white]{$emote}[/color][/bold]

entity-effect-guidebook-extinguish-reaction =
    { $chance ->
        [1] Тушит
        *[other] тушат
    } огонь

entity-effect-guidebook-flammable-reaction =
    { $chance ->
        [1] Увеличивает
        *[other] увеличивают
    } возгораемость

entity-effect-guidebook-ignite =
    { $chance ->
        [1] Поджигает
        *[other] поджигают
    } метаболизатор

entity-effect-guidebook-make-sentient =
    { $chance ->
        [1] Делает
        *[other] делают
    } метаболизатор разумным

entity-effect-guidebook-make-polymorph =
    { $chance ->
        [1] Превращает
        *[other] превращают
    } метаболизатор в { $entityname }

entity-effect-guidebook-modify-bleed-amount =
    { $chance ->
        [1] { $deltasign ->
                [1] Вызывает
                *[-1] Уменьшает
            }
        *[other] { $deltasign ->
                    [1] вызывают
                    *[-1] уменьшают
                 }
    } кровотечение

entity-effect-guidebook-modify-blood-level =
    { $chance ->
        [1] { $deltasign ->
                [1] Увеличивает
                *[-1] Уменьшает
            }
        *[other] { $deltasign ->
                    [1] увеличивают
                    *[-1] уменьшают
                 }
    } уровень крови

entity-effect-guidebook-paralyze =
    { $chance ->
        [1] Парализует
        *[other] парализуют
    } метаболизатор как минимум на {NATURALFIXED($time, 3)} {MANY("секунду", $time)}

entity-effect-guidebook-movespeed-modifier =
    { $chance ->
        [1] Изменяет
        *[other] изменяют
    } скорость передвижения в {NATURALFIXED($sprintspeed, 3)}x как минимум на {NATURALFIXED($time, 3)} {MANY("секунду", $time)}

entity-effect-guidebook-reset-narcolepsy =
    { $chance ->
        [1] Временно отодвигает
        *[other] временно отодвигают
    } нарколепсию

entity-effect-guidebook-wash-cream-pie-reaction =
    { $chance ->
        [1] Смывает
        *[other] смывают
    } кремовый пирог с лица

entity-effect-guidebook-cure-zombie-infection =
    { $chance ->
        [1] Лечит
        *[other] лечат
    } текущую зомби-инфекцию

entity-effect-guidebook-cause-zombie-infection =
    { $chance ->
        [1] Передаёт
        *[other] передают
    } индивидууму зомби-инфекцию

entity-effect-guidebook-innoculate-zombie-infection =
    { $chance ->
        [1] Лечит
        *[other] лечат
    } текущую зомби-инфекцию и даёт иммунитет к будущим заражениям

entity-effect-guidebook-reduce-rotting =
    { $chance ->
        [1] Восстанавливает
        *[other] восстанавливают
    } {NATURALFIXED($time, 3)} {MANY("секунду", $time)} разложения

entity-effect-guidebook-area-reaction =
    { $chance ->
        [1] Вызывает
        *[other] вызывают
    } реакцию дыма или пены в течение {NATURALFIXED($duration, 3)} {MANY("секунды", $duration)}

entity-effect-guidebook-add-to-solution-reaction =
    { $chance ->
        [1] Вызывает
        *[other] вызывают
    } добавление {$reagent} во внутренний контейнер раствора

entity-effect-guidebook-artifact-unlock =
    { $chance ->
        [1] Помогает
        *[other] помогают
        } разблокировать инопланетный артефакт.

entity-effect-guidebook-artifact-durability-restore =
    Восстанавливает {$restored} прочности в активных узлах инопланетного артефакта.

entity-effect-guidebook-plant-attribute =
    { $chance ->
        [1] Изменяет
        *[other] изменяют
    } {$attribute} на {$positive ->
    [true] [color=red]{$amount}[/color]
    *[false] [color=green]{$amount}[/color]
    }

entity-effect-guidebook-plant-cryoxadone =
    { $chance ->
        [1] Омолаживает
        *[other] омолаживают
    } растение в зависимости от его возраста и времени роста

entity-effect-guidebook-plant-phalanximine =
    { $chance ->
        [1] Восстанавливает
        *[other] восстанавливают
    } жизнеспособность растения, утраченную из-за мутации

entity-effect-guidebook-plant-remove-kudzu =
    { $chance ->
        [1] Удаляет
        *[other] удаляют
    } рост сорняка кудзу с растения

entity-effect-guidebook-plant-diethylamine =
    { $chance ->
        [1] Увеличивает
        *[other] увеличивают
    } продолжительность жизни и/или базовое здоровье растения с 10% шансом для каждого

entity-effect-guidebook-plant-robust-harvest =
    { $chance ->
        [1] Увеличивает
        *[other] увеличивают
    } эффективность растения на {$increase} вплоть до максимума {$limit}. Заставляет растение терять семена при достижении эффективности {$seedlesstreshold}. Попытка добавить эффективность сверх {$limit} может снизить урожайность с 10% шансом

entity-effect-guidebook-plant-seeds-add =
    { $chance ->
        [1] Восстанавливает
        *[other] восстанавливают
    } семена растения

entity-effect-guidebook-plant-seeds-remove =
    { $chance ->
        [1] Удаляет
        *[other] удаляют
    } семена растения

entity-effect-guidebook-plant-mutate-chemicals =
    { $chance ->
        [1] Мутирует
        *[other] мутируют
    } растение для производства {$name}
