discord-watchlist-connection-header =
    { $players ->
        [one] Один игрок из списка наблюдения подключился к {$serverName}
        [few] {$players} игрока из списка наблюдения подключились к {$serverName}
       *[many] {$players} игроков из списка наблюдения подключились к {$serverName}
    }

discord-watchlist-connection-entry = - {$playerName} с сообщением «{$message}»{ $expiry ->
        [0] {""}
       *[other] {" "}(истекает <t:{$expiry}:R>)
    }{ $otherWatchlists ->
        [0] {""}
        [one] {" "}и ещё {$otherWatchlists} список наблюдения
        [few] {" "}и ещё {$otherWatchlists} списка наблюдения
       *[many] {" "}и ещё {$otherWatchlists} списков наблюдения
    }
