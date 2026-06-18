# UI
admin-notes-title = Заметки для {$player}
admin-notes-new-note = Новая заметка
admin-notes-show-more = Показать ещё
admin-notes-for = Заметка для: {$player}
admin-notes-id = Id: {$id}
admin-notes-type = Тип: {$type}
admin-notes-severity = Серьёзность: {$severity}
admin-notes-secret = Секретно
admin-notes-notsecret = Не секретно
admin-notes-expires = Истекает: {$expires}
admin-notes-expires-never = Не истекает
admin-notes-edited-never = Никогда
admin-notes-round-id = Id раунда: {$id}
admin-notes-round-id-unknown = Id раунда: Неизвестен
admin-notes-created-by = Создано: {$author}
admin-notes-created-at = Создано: {$date}
admin-notes-last-edited-by = Последнее редактирование: {$author}
admin-notes-last-edited-at = Последнее редактирование: {$date}
admin-notes-edit = Редактировать
admin-notes-delete = Удалить
admin-notes-hide = Скрыть
admin-notes-delete-confirm = Подтвердить удаление
admin-notes-edited = Последнее редактирование от {$author} {$date}
admin-notes-unbanned = Разбанен {$admin} {$date}
admin-notes-message-desc = [color=white]Вы получили { $count ->
    [one] административное сообщение
    [few] административных сообщения
   *[many] административных сообщений
} с момента вашего последнего захода на этот сервер.[/color]
admin-notes-message-admin = От [bold]{ $admin }[/bold], написано { TOSTRING($date, "f") }:
admin-notes-message-wait = Кнопка подтверждения станет доступна через {$time} секунд.
admin-notes-message-accept = Отклонить навсегда
admin-notes-message-dismiss = Отклонить сейчас
admin-notes-message-seen = Просмотрено
admin-notes-banned-from = Забанен с
admin-notes-the-server = сервера
admin-notes-permanently = навсегда
admin-notes-days = {$days} дн.
admin-notes-hours = {$hours} ч.
admin-notes-minutes = {$minutes} мин.

# Note editor UI
admin-note-editor-title-new = Создание новой заметки для {$player}
admin-note-editor-title-existing = Редактирование заметки {$id} для {$player} от {$author}
admin-note-editor-pop-out = Отделить окно
admin-note-editor-secret = Секретно?
admin-note-editor-secret-tooltip = Если отмечено, игрок не увидит эту заметку
admin-note-editor-type-note = Заметка
admin-note-editor-type-message = Сообщение
admin-note-editor-type-watchlist = Список наблюдения
admin-note-editor-type-server-ban = Бан на сервере
admin-note-editor-type-role-ban = Бан на роль
admin-note-editor-severity-select = Выбрать
admin-note-editor-severity-none = Нет
admin-note-editor-severity-low = Низкая
admin-note-editor-severity-medium = Средняя
admin-note-editor-severity-high = Высокая
admin-note-editor-expiry-checkbox = Постоянная?
admin-note-editor-expiry-checkbox-tooltip = Отметьте, чтобы установить срок действия
admin-note-editor-expiry-label = Истекает через:
admin-note-editor-expiry-label-params = Истекает: {$date} (через {$expiresIn})
admin-note-editor-expiry-label-expired = Истекла
admin-note-editor-expiry-placeholder = Введите время истечения (целое число).
admin-note-editor-submit = Отправить
admin-note-editor-submit-confirm = Вы уверены?

# Time
admin-note-button-minutes = Минуты
admin-note-button-hours = Часы
admin-note-button-days = Дни
admin-note-button-weeks = Недели
admin-note-button-months = Месяцы
admin-note-button-years = Годы
admin-note-button-centuries = Века


# Verb
admin-notes-verb-text = Открыть заметки администратора

# Watchlist and message login
admin-notes-watchlist = Список наблюдения для {$player}: {$message}
admin-notes-new-message = Вы получили сообщение от администратора {$admin}: {$message}
admin-notes-fallback-admin-name = [Система]

# Admin remarks
admin-remarks-command-description = Открывает страницу заметок администратора
admin-remarks-command-error = Заметки администратора отключены
admin-remarks-title = Заметки администратора

# Misc
system-user = [Система]
