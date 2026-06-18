game-ticker-restart-round = Перезапуск раунда...
game-ticker-start-round = Раунд начинается сейчас...
game-ticker-start-round-cannot-start-game-mode-fallback = Не удалось запустить режим {$failedGameMode}! Переключение на {$fallbackMode}...
game-ticker-start-round-cannot-start-game-mode-restart = Не удалось запустить режим {$failedGameMode}! Перезапуск раунда...
game-ticker-start-round-invalid-map = Выбранная карта {$map} не подходит для режима {$mode}. Режим может работать не так, как задумано...
game-ticker-unknown-role = Неизвестно
game-ticker-delay-start = Запуск раунда отложен на {$seconds} секунд.
game-ticker-pause-start = Запуск раунда приостановлен.
game-ticker-pause-start-resumed = Обратный отсчёт запуска раунда возобновлён.
game-ticker-player-join-game-message = Добро пожаловать на Space Station 14! Если вы играете в первый раз, обязательно ознакомьтесь с правилами игры и не стесняйтесь обращаться за помощью в LOOC (локальный OOC) или OOC (обычно доступен только между раундами).
game-ticker-get-info-text = Привет и добро пожаловать на [color=white]FactoryStation![/color]
                            Текущий раунд: [color=white]#{$roundId}[/color]
                            Количество игроков: [color=white]{$playerCount}[/color]
                            Текущая карта: [color=white]{$mapName}[/color]
                            Текущий игровой режим: [color=white]{$gmTitle}[/color]
                            >[color=yellow]{$desc}[/color]
game-ticker-get-info-preround-text = Привет и добро пожаловать на [color=white]Space Station 14![/color]
                            Текущий раунд: [color=white]#{$roundId}[/color]
                            Количество игроков: [color=white]{$playerCount}[/color] ([color=white]{$readyCount}[/color] {$readyCount ->
                                [one] готов
                                [few] готово
                               *[many] готовы
                            })
                            Текущая карта: [color=white]{$mapName}[/color]
                            Текущий игровой режим: [color=white]{$gmTitle}[/color]
                            >[color=yellow]{$desc}[/color]
game-ticker-no-map-selected = [color=yellow]Карта ещё не выбрана![/color]
game-ticker-player-no-jobs-available-when-joining = При попытке присоединиться к игре не было доступных должностей.

# Displayed in chat to admins when a player joins
player-join-message = Игрок {$name} присоединился.
player-first-join-message = Игрок {$name} присоединился впервые.

# Displayed in chat to admins when a player leaves
player-leave-message = Игрок {$name} покинул игру.

latejoin-arrival-announcement = {$character} ({$job}) прибыл на станцию!
latejoin-arrival-announcement-special = {$job} {$character} на борту!
latejoin-arrival-sender = Станция
latejoin-arrivals-direction = Шаттл, доставляющий вас на станцию, скоро прибудет.
latejoin-arrivals-direction-time = Шаттл, доставляющий вас на станцию, прибудет через {$time}.
latejoin-arrivals-dumped-from-shuttle = Таинственная сила не даёт вам покинуть прибывающий шаттл.
latejoin-arrivals-teleport-to-spawn = Таинственная сила телепортирует вас с прибывающего шаттла. Удачной смены!

preset-not-enough-ready-players = Не удаётся запустить {$presetName}. Требуется {$minimumPlayers} игроков, но готово {$readyPlayersCount}.
preset-no-one-ready = Не удаётся запустить {$presetName}. Никто не готов.

game-run-level-PreRoundLobby = Лобби перед раундом
game-run-level-InRound = В раунде
game-run-level-PostRound = После раунда
