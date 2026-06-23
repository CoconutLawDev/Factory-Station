## UI

cargo-console-menu-title = Консоль заказов грузового отдела
cargo-console-menu-flavor-left = Закажите ещё больше коробок с пиццей, чем обычно!
cargo-console-menu-flavor-right = v2.1
cargo-console-menu-account-name-label = Счёт:{" "}
cargo-console-menu-account-name-none-text = Отсутствует
cargo-console-menu-account-name-format = [bold][color={$color}]{$name}[/color][/bold] [font="Monospace"]\[{$code}\][/font]
cargo-console-menu-shuttle-name-label = Shuttle name:{" "}
cargo-console-menu-shuttle-name-none-text = None
cargo-console-menu-points-label = Balance:{" "}
cargo-console-menu-points-amount = ${$amount}
cargo-console-menu-shuttle-status-label = Shuttle status:{" "}
cargo-console-menu-shuttle-status-away-text = Away
cargo-console-menu-order-capacity-label = Order capacity:{" "}
cargo-console-menu-call-shuttle-button = Activate telepad
cargo-console-menu-permissions-button = Permissions
cargo-console-menu-categories-label = Categories:{" "}
cargo-console-menu-search-bar-placeholder = Search
cargo-console-menu-requests-label = Requests
cargo-console-menu-orders-label = Orders
cargo-console-menu-populate-categories-all-text = All
cargo-console-menu-order-row-title = {$productName} (x{$orderAmount} for {$orderPrice}$)
cargo-console-menu-populate-orders-cargo-order-row-product-name-text = Requested by: {$orderRequester} from [color={$accountColor}]{$account}[/color]
cargo-console-menu-order-row-product-description = Reason: {$orderReason}
cargo-console-menu-order-row-button-approve = Approve
cargo-console-menu-order-row-button-cancel = Cancel
cargo-console-menu-order-row-alerts-reason-absent = The reason is not specified
cargo-console-menu-order-row-alerts-requester-unknown = Unknown
cargo-console-menu-tab-title-orders = Orders
cargo-console-menu-tab-title-funds = Transfers
cargo-console-menu-account-action-transfer-limit = [bold]Transfer Limit:[/bold] ${$limit}
cargo-console-menu-account-action-transfer-limit-unlimited-notifier = [color=gold](Unlimited)[/color]
cargo-console-menu-account-action-select = [bold]Account Action:[/bold]
cargo-console-menu-account-action-amount = [bold]Amount:[/bold] $
cargo-console-menu-account-action-button = Transfer
cargo-console-menu-toggle-account-lock-button = Toggle Transfer Limit
cargo-console-menu-account-action-option-withdraw = Withdraw Cash
cargo-console-menu-account-action-option-transfer = Transfer Funds to {$code}

# Orders
cargo-console-order-not-allowed = Доступ запрещён
cargo-console-station-not-found = Станция не найдена
cargo-console-invalid-product = Неверный ID продукта
cargo-console-too-many = Слишком много одобренных заказов
cargo-console-snip-snip = Заказ урезан до лимита
cargo-console-insufficient-funds = Недостаточно средств (требуется {$cost})
cargo-console-unfulfilled = Нет места для выполнения заказа
cargo-console-trade-station = Отправлено на {$destination}
cargo-console-unlock-approved-order-broadcast = [bold]{$productName} x{$orderAmount}[/bold] стоимостью [bold]{$cost}[/bold] одобрен [bold]{$approver}[/bold]
cargo-console-fund-withdraw-broadcast = [bold]{$name} снял {$amount} специан со счёта {$name1} \[{$code1}\][/bold]
cargo-console-fund-transfer-broadcast = [bold]{$name} перевёл {$amount} специан со счёта {$name1} \[{$code1}\] на счёт {$name2} \[{$code2}\][/bold]
cargo-console-fund-transfer-user-unknown = Неизвестен

cargo-console-paper-reason-default = None
cargo-console-paper-approver-default = Self
cargo-console-paper-print-name = Order #{$orderNumber}
cargo-console-paper-print-text = [head=2]Order #{$orderNumber}[/head]
    {"[bold]Item:[/bold]"} {$itemName} (x{$orderQuantity})
    {"[bold]Requested by:[/bold]"} {$requester}

    {"[head=3]Информация о заказе[/head]"}
    {"[bold]Плательщик:[/bold]"} {$account} [font="Monospace"]\[{$accountcode}\][/font]
    {"[bold]Одобрил:[/bold]"} {$approver}
    {"[bold]Причина:[/bold]"} {$reason}

# Cargo shuttle console
cargo-shuttle-console-menu-title = Консоль грузового шаттла
cargo-shuttle-console-station-unknown = Неизвестна
cargo-shuttle-console-shuttle-not-found = Не найден
cargo-shuttle-console-organics = На шаттле обнаружена органическая жизнь
cargo-no-shuttle = Грузовой шаттл не найден!

# Funding allocation console
cargo-funding-alloc-console-menu-title = Консоль распределения финансирования
cargo-funding-alloc-console-label-account = [bold]Счёт[/bold]
cargo-funding-alloc-console-label-code = [bold] Код [/bold]
cargo-funding-alloc-console-label-balance = [bold] Баланс [/bold]
cargo-funding-alloc-console-label-cut = [bold] Доля от доходов (%) [/bold]

cargo-funding-alloc-console-label-primary-cut = Доля грузового отдела из средств из не сейфов:
cargo-funding-alloc-console-label-lockbox-cut = Доля грузового отдела из средств от продажи из сейфов:

cargo-funding-alloc-console-label-help-non-adjustible = Грузовой отдел получает {$percent}% от прибыли от продажи не из сейфов. Остальное распределяется, как указано ниже:
cargo-funding-alloc-console-label-help-adjustible = Оставшиеся средства из источников не из сейфов распределяются, как указано ниже:
cargo-funding-alloc-console-button-save = Сохранить изменения
cargo-funding-alloc-console-label-save-fail = [bold]Распределение доходов некорректно![/color] [color=red]({$pos ->
    [1] +
    *[-1] -
}{$val}%)[/color]

# Slip template
cargo-acquisition-slip-body = [head=3]Детали актива[/head]
    {"[bold]Продукт:[/bold]"} {$product}
    {"[bold]Описание:[/bold]"} {$description}
    {"[bold]Цена за ед.:[/bold]"} {$unit}$
    {"[bold]Количество:[/bold]"} {$amount}
    {"[bold]Стоимость:[/bold]"} {$cost}$

    {"[head=3]Детали покупки[/head]"}
    {"[bold]Заказчик:[/bold]"} {$orderer}
    {"[bold]Причина:[/bold]"} {$reason}
