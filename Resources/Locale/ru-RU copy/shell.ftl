### for technical and/or system messages

## General

shell-command-success = Команда выполнена успешно
shell-invalid-command = Неверная команда.
shell-invalid-command-specific = Неверная команда {$commandName}.
shell-can-only-run-from-pre-round-lobby = Вы можете выполнить эту команду только в лобби перед раундом.
shell-can-only-run-while-round-is-active = Вы можете выполнить эту команду только во время раунда.
shell-cannot-run-command-from-server = Вы не можете выполнить эту команду с сервера.
shell-only-players-can-run-this-command = Только игроки могут выполнить эту команду.
shell-must-be-attached-to-entity = Вы должны быть привязаны к сущности, чтобы выполнить эту команду.
shell-must-have-body = У вас должно быть тело, чтобы выполнить эту команду.

shell-unknown-error = Произошла неизвестная ошибка.

## Arguments

shell-need-exactly-one-argument = Требуется ровно один аргумент.
shell-wrong-arguments-number-need-specific = Требуется {$properAmount} аргументов, получено {$currentAmount}.
shell-argument-must-be-number = Аргумент должен быть числом.
shell-argument-must-be-boolean = Аргумент должен быть логическим значением (true/false).
shell-wrong-arguments-number = Неверное количество аргументов.
shell-need-between-arguments = Требуется от {$lower} до {$upper} аргументов!
shell-need-minimum-arguments = Требуется как минимум {$minimum} аргументов!
shell-need-minimum-one-argument = Требуется хотя бы один аргумент!
shell-need-exactly-zero-arguments = Эта команда не принимает аргументов.

shell-argument-uid = EntityUid

## Guards

shell-missing-required-permission = Вам нужно разрешение {$perm} для этой команды!
shell-entity-is-not-mob = Целевая сущность не является мобом!
shell-invalid-entity-id = Неверный ID сущности.
shell-invalid-grid-id = Неверный ID сетки.
shell-invalid-map-id = Неверный ID карты.
shell-invalid-entity-uid = {$uid} не является допустимым uid сущности
shell-invalid-bool = Неверное логическое значение.
shell-entity-uid-must-be-number = EntityUid должен быть числом.
shell-could-not-find-entity = Не удаётся найти сущность {$entity}
shell-could-not-find-entity-with-uid = Не удаётся найти сущность с uid {$uid}
shell-entity-with-uid-lacks-component = Сущность с uid {$uid} не имеет компонента {$componentName}
shell-entity-target-lacks-component = Целевая сущность не имеет компонента {$componentName}
shell-invalid-color-hex = Неверный шестнадцатеричный код цвета!
shell-target-player-does-not-exist = Целевой игрок не существует!
shell-target-entity-does-not-have-message = Целевая сущность не имеет {$missing}!
shell-timespan-minutes-must-be-correct = {$span} не является допустимым количеством минут.
shell-argument-must-be-prototype = Аргумент {$index} должен быть {LOC($prototypeName)}!
shell-argument-number-must-be-between = Аргумент {$index} должен быть числом от {$lower} до {$upper}!
shell-argument-station-id-invalid = Аргумент {$index} должен быть допустимым id станции!
shell-argument-map-id-invalid = Аргумент {$index} должен быть допустимым id карты!
shell-argument-number-invalid = Аргумент {$index} должен быть допустимым числом!

# Hints
shell-argument-username-hint = <username>
shell-argument-username-optional-hint = [username]
