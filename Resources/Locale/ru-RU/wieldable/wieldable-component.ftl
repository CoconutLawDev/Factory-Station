### Locale for wielding items; i.e. two-handing them

wieldable-verb-text-wield = Взять в две руки
wieldable-verb-text-unwield = Взять в одну руку

wieldable-component-successful-wield = Вы берёте { THE($item) } в две руки.
wieldable-component-failed-wield = Вы берёте { THE($item) } в одну руку.
wieldable-component-successful-wield-other = { CAPITALIZE($user) } берёт { THE($item) } в две руки.
wieldable-component-failed-wield-other = { CAPITALIZE($user) } берёт { THE($item) } в одну руку.
wieldable-component-blocked-wield = { CAPITALIZE($blocker) } мешает вам взять { THE($item) } в две руки.

wieldable-component-no-hands = У вас недостаточно рук!
wieldable-component-not-enough-free-hands = {$number ->
    [one] Вам нужна свободная рука, чтобы взять { THE($item) } в две руки.
    [few] Вам нужно { $number } свободные руки, чтобы взять { THE($item) } в две руки.
   *[many] Вам нужно { $number } свободных рук, чтобы взять { THE($item) } в две руки.
}
wieldable-component-not-in-hands = { CAPITALIZE($item) } не в ваших руках!

wieldable-component-requires = { CAPITALIZE($item) } нужно держать в двух руках!

gunwieldbonus-component-examine = Это оружие точнее при использовании в двух руках.

gunrequireswield-component-examine = Из этого оружия можно стрелять только держа его в двух руках.
