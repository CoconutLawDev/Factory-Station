### UI

chat-manager-max-message-length = Ваше сообщение превышает лимит в {$maxMessageLength} символов
chat-manager-ooc-chat-enabled-message = OOC-чат включён.
chat-manager-ooc-chat-disabled-message = OOC-чат отключён.
chat-manager-looc-chat-enabled-message = LOOC-чат включён.
chat-manager-looc-chat-disabled-message = LOOC-чат отключён.
chat-manager-dead-looc-chat-enabled-message = Мёртвые игроки теперь могут использовать LOOC.
chat-manager-dead-looc-chat-disabled-message = Мёртвые игроки больше не могут использовать LOOC.
chat-manager-crit-looc-chat-enabled-message = Игроки в критическом состоянии теперь могут использовать LOOC.
chat-manager-crit-looc-chat-disabled-message = Игроки в критическом состоянии больше не могут использовать LOOC.
chat-manager-admin-ooc-chat-enabled-message = Админский OOC-чат включён.
chat-manager-admin-ooc-chat-disabled-message = Админский OOC-чат отключён.

chat-manager-max-message-length-exceeded-message = Ваше сообщение превысило лимит в {$limit} символов
chat-manager-no-headset-on-message = На вас нет гарнитуры!
chat-manager-no-radio-key = Не указан ключ радиостанции!
chat-manager-no-such-channel = Нет канала с ключом «{$key}»!
chat-manager-whisper-headset-on-message = Вы не можете шептать в радиоэфир!

chat-manager-server-wrap-message = [bold]{$message}[/bold]
chat-manager-sender-announcement = Центральное Командование
chat-manager-sender-announcement-wrap-message = [font size=14][bold]Объявление от {$sender}:[/font][font size=12]
                                                {$message}[/bold][/font]
chat-manager-entity-say-wrap-message = [BubbleHeader][bold][Name]{$entityName}[/Name][/bold][/BubbleHeader] {$verb}, [font={$fontType} size={$fontSize}]“[BubbleContent]{$message}[/BubbleContent]”[/font]
chat-manager-entity-say-bold-wrap-message = [BubbleHeader][bold][Name]{$entityName}[/Name][/bold][/BubbleHeader] {$verb}, [font={$fontType} size={$fontSize}]“[BubbleContent][bold]{$message}[/bold][/BubbleContent]”[/font]

chat-manager-entity-whisper-wrap-message = [font size=11][italic][BubbleHeader][Name]{$entityName}[/Name][/BubbleHeader] шепчет: «[BubbleContent]{$message}[/BubbleContent]»[/italic][/font]
chat-manager-entity-whisper-unknown-wrap-message = [font size=11][italic][BubbleHeader]Кто-то[/BubbleHeader] шепчет: «[BubbleContent]{$message}[/BubbleContent]»[/italic][/font]

# THE() is not used here because the entity and its name can technically be disconnected if a nameOverride is passed...
chat-manager-entity-me-wrap-message = [italic]{ PROPER($entity) ->
    *[false] {$entityName} {$message}[/italic]
     [true] {CAPITALIZE($entityName)} {$message}[/italic]
    }

chat-manager-entity-looc-wrap-message = LOOC: [bold]{$entityName}:[/bold] {$message}
chat-manager-send-ooc-wrap-message = OOC: [bold]{$playerName}:[/bold] {$message}
chat-manager-send-ooc-patron-wrap-message = OOC: [bold][color={$patronColor}]{$playerName}[/color]:[/bold] {$message}

chat-manager-send-dead-chat-wrap-message = {$deadChannelName}: [bold][BubbleHeader]{$playerName}[/BubbleHeader]:[/bold] [BubbleContent]{$message}[/BubbleContent]
chat-manager-send-admin-dead-chat-wrap-message = {$adminChannelName}: [bold]([BubbleHeader]{$userName}[/BubbleHeader]):[/bold] [BubbleContent]{$message}[/BubbleContent]
chat-manager-send-admin-chat-wrap-message = {$adminChannelName}: [bold]{$playerName}:[/bold] {$message}
chat-manager-send-admin-announcement-wrap-message = [bold]{$adminChannelName}: {$message}[/bold]

chat-manager-send-hook-ooc-wrap-message = OOC: [bold](D){$senderName}:[/bold] {$message}
chat-manager-send-hook-admin-wrap-message = ADMIN: [bold](D){$senderName}:[/bold] {$message}

chat-manager-dead-channel-name = МЁРТВЫЕ
chat-manager-admin-channel-name = АДМИН

chat-manager-rate-limited = You are sending messages too quickly!
chat-manager-rate-limit-admin-announcement = Rate limit warning: { $player }

## Speech verbs for chat

chat-speech-verb-suffix-exclamation = !
chat-speech-verb-suffix-exclamation-strong = !!
chat-speech-verb-suffix-question = ?
chat-speech-verb-suffix-stutter = -
chat-speech-verb-suffix-mumble = ..

chat-speech-verb-name-none = Нет
chat-speech-verb-name-default = Стандартный
chat-speech-verb-default = говорит
chat-speech-verb-name-exclamation = Восклицание
chat-speech-verb-exclamation = восклицает
chat-speech-verb-name-exclamation-strong = Крик
chat-speech-verb-exclamation-strong = кричит
chat-speech-verb-name-question = Вопрос
chat-speech-verb-question = спрашивает
chat-speech-verb-name-stutter = Заикание
chat-speech-verb-stutter = заикается
chat-speech-verb-name-mumble = Бормотание
chat-speech-verb-mumble = бормочет

chat-speech-verb-name-arachnid = Паукообразный
chat-speech-verb-insect-1 = стрекочет
chat-speech-verb-insect-2 = чирикает
chat-speech-verb-insect-3 = щёлкает

chat-speech-verb-name-moth = Мотылёк
chat-speech-verb-winged-1 = трепещет
chat-speech-verb-winged-2 = хлопает
chat-speech-verb-winged-3 = жужжит

chat-speech-verb-name-slime = Слайм
chat-speech-verb-slime-1 = плескается
chat-speech-verb-slime-2 = булькает
chat-speech-verb-slime-3 = сочится

chat-speech-verb-name-plant = Диона
chat-speech-verb-plant-1 = шелестит
chat-speech-verb-plant-2 = колышется
chat-speech-verb-plant-3 = скрипит

chat-speech-verb-name-robotic = Роботизированный
chat-speech-verb-robotic-1 = констатирует
chat-speech-verb-robotic-2 = пищит
chat-speech-verb-robotic-3 = гудит

chat-speech-verb-name-reptilian = Рептилия
chat-speech-verb-reptilian-1 = шипит
chat-speech-verb-reptilian-2 = фыркает
chat-speech-verb-reptilian-3 = пыхтит

chat-speech-verb-name-skeleton = Скелет
chat-speech-verb-skeleton-1 = гремит
chat-speech-verb-skeleton-2 = стучит
chat-speech-verb-skeleton-3 = скрежещет

chat-speech-verb-name-vox = Вокс
chat-speech-verb-vox-1 = визжит
chat-speech-verb-vox-2 = пронзительно кричит
chat-speech-verb-vox-3 = квакает

chat-speech-verb-name-canine = Пёс
chat-speech-verb-canine-1 = лает
chat-speech-verb-canine-2 = гавкает
chat-speech-verb-canine-3 = воет

chat-speech-verb-name-goat = Goat
chat-speech-verb-goat-1 = bleats
chat-speech-verb-goat-2 = grunts
chat-speech-verb-goat-3 = cries

chat-speech-verb-name-small-mob = Мышь
chat-speech-verb-small-mob-1 = пищит
chat-speech-verb-small-mob-2 = попискивает

chat-speech-verb-name-large-mob = Карп
chat-speech-verb-large-mob-1 = рычит
chat-speech-verb-large-mob-2 = ворчит

chat-speech-verb-name-monkey = Обезьяна
chat-speech-verb-monkey-1 = улюлюкает
chat-speech-verb-monkey-2 = визжит

chat-speech-verb-name-cluwne = Клювн

chat-speech-verb-name-parrot = Попугай
chat-speech-verb-parrot-1 = скрежещет
chat-speech-verb-parrot-2 = чирикает
chat-speech-verb-parrot-3 = щебечет

chat-speech-verb-cluwne-1 = хихикает
chat-speech-verb-cluwne-2 = гогочет
chat-speech-verb-cluwne-3 = смеётся

chat-speech-verb-name-ghost = Призрак
chat-speech-verb-ghost-1 = жалуется
chat-speech-verb-ghost-2 = дышит
chat-speech-verb-ghost-3 = гудит
chat-speech-verb-ghost-4 = бормочет

chat-speech-verb-name-electricity = Электричество
chat-speech-verb-electricity-1 = потрескивает
chat-speech-verb-electricity-2 = жужжит
chat-speech-verb-electricity-3 = визжит

chat-speech-verb-vulpkanin-1 = рычит
chat-speech-verb-vulpkanin-2 = лает
chat-speech-verb-vulpkanin-3 = урчит
chat-speech-verb-vulpkanin-4 = тявкает
chat-speech-verb-vulpkanin = Вульпканин

chat-speech-verb-name-wawa = Wawa
chat-speech-verb-wawa-1 = вещает
chat-speech-verb-wawa-2 = изрекает
chat-speech-verb-wawa-3 = заявляет
chat-speech-verb-wawa-4 = размышляет
