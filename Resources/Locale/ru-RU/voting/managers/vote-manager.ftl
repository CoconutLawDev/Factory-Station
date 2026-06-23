# Displayed as initiator of vote when no user creates the vote
ui-vote-initiator-server = Сервер

## Default.Votes

ui-vote-restart-title = Перезапустить раунд
ui-vote-restart-succeeded = Голосование за перезапуск успешно.
ui-vote-restart-failed = Голосование за перезапуск не удалось (требуется { TOSTRING($ratio, "P0") }).
ui-vote-restart-fail-not-enough-ghost-players = Голосование за перезапуск не удалось: для его инициации требуется минимум { $ghostPlayerRequirement }% игроков-призраков. В настоящее время их недостаточно.
ui-vote-restart-yes = Да
ui-vote-restart-no = Нет
ui-vote-restart-abstain = Воздержаться

ui-vote-gamemode-title = Следующий режим игры
ui-vote-gamemode-tie = Ничья в голосовании за режим! Выбран... { $picked }
ui-vote-gamemode-win = { $winner } победил(а) в голосовании за режим!

ui-vote-map-title = Следующая карта
ui-vote-map-tie = Ничья в голосовании за карту! Выбрана... { $picked }
ui-vote-map-win = { $winner } победила в голосовании за карту!
ui-vote-map-notlobby = Голосование за карты доступно только в лобби перед раундом!
ui-vote-map-notlobby-time = Голосование за карты доступно только в лобби перед раундом, когда осталось { $time }!
ui-vote-map-invalid = { $winner } стала недоступной после голосования за карту! Она не будет выбрана.

# Votekick votes
ui-vote-votekick-unknown-initiator = Игрок
ui-vote-votekick-unknown-target = Неизвестный игрок
ui-vote-votekick-title = { $initiator } начал(а) голосование за кик игрока: { $targetEntity }. Причина: { $reason }
ui-vote-votekick-yes = Да
ui-vote-votekick-no = Нет
ui-vote-votekick-abstain = Воздержаться
ui-vote-votekick-success = Кик { $target } по голосованию успешен. Причина: { $reason }
ui-vote-votekick-failure = Кик { $target } по голосованию не удался. Причина: { $reason }
ui-vote-votekick-not-enough-eligible = Недостаточно голосующих онлайн для начала голосования за кик: { $voters }/{ $requirement }
ui-vote-votekick-server-cancelled = Голосование за кик { $target } отменено сервером.
