### Voting system related console commands

## 'createvote' command

cmd-createvote-desc = Создаёт голосование
cmd-createvote-help = Использование: createvote <'restart'|'preset'|'map'>
cmd-createvote-cannot-call-vote-now = Вы не можете начать голосование прямо сейчас!
cmd-createvote-invalid-vote-type = Неверный тип голосования
cmd-createvote-arg-vote-type = <тип голосования>

## 'customvote' command

cmd-customvote-desc = Создаёт пользовательское голосование
cmd-customvote-help = Использование: customvote <заголовок> <вариант1> <вариант2> [вариант3...]
cmd-customvote-on-finished-tie = Голосование «{$title}» завершено: ничья между {$ties}!
cmd-customvote-on-finished-win = Голосование «{$title}» завершено: победил {$winner}!
cmd-customvote-arg-title = <title>
cmd-customvote-arg-option-n = <option{ $n }>

## 'vote' command

cmd-vote-desc = Голосует в активном голосовании
cmd-vote-help = vote <voteId> <option>
cmd-vote-cannot-call-vote-now = Вы не можете начать голосование прямо сейчас!
cmd-vote-on-execute-error-must-be-player = Вы должны быть игроком
cmd-vote-on-execute-error-invalid-vote-id = Неверный ID голосования
cmd-vote-on-execute-error-invalid-vote-options = Неверные варианты голосования
cmd-vote-on-execute-error-invalid-vote = Неверное голосование
cmd-vote-on-execute-error-invalid-option = Неверный вариант

## 'listvotes' command

cmd-listvotes-desc = Показывает список текущих активных голосований
cmd-listvotes-help = Использование: listvotes

## 'cancelvote' command

cmd-cancelvote-desc = Отменяет активное голосование
cmd-cancelvote-help = Использование: cancelvote <id>
                      ID можно получить с помощью команды listvotes.
cmd-cancelvote-error-invalid-vote-id = Неверный ID голосования
cmd-cancelvote-error-missing-vote-id = Отсутствует ID
cmd-cancelvote-arg-id = <id>
