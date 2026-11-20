import 'package:flutter/material.dart';

import '../app/arcade_stage.dart';
import '../app/game_api.dart';
import '../core/game_rules.dart';
import 'tycoon_engine.dart';
import 'rules.dart';
import 'tycoon_view.dart';

const _theme = TycoonTheme(Color(0xFF90E0EF), Color(0xFF6C757D), Color(0xFF00B4D8));



final gameConfig = GameConfig(
  gameId: 'game-025-city-builder',
  title: const ['Şehir Kurucu', 'City Builder', 'City Builder', 'City Builder', 'City Builder'],
  tagline: const ['Bölgele, elektrik ver, hayalindeki şehri büyüt!', 'Zone, power and grow your dream city!', 'Zone, power and grow your dream city!', 'Zone, power and grow your dream city!', 'Zone, power and grow your dream city!'],
  seedColor: const Color(0xFF00B4D8),
  icon: Icons.location_city,
  rules: CityRules(),
  buildPlay: (ctx) => ArcadeStage(
    ctx: ctx,
    create: () => TycoonSim((ctx.generated as GeneratedLevel<TycoonLevel>).puzzle),
    paint: (c, s, p, t) => paintTycoon(c, s, p, t, ctx.lang == 'tr', _theme),
    sound: tycoonSound,
    boosterIcon: Icons.bolt,
    boosterLabel: (l) => l == 'tr' ? 'Çılgın üretim' : 'Production frenzy',
    howTo: const [
      'Aşağıdaki kartlara dokunarak bina kur; nüfus ihtiyaçlarını karşıla ve hedef nüfusa ulaş.',
      'Tap cards to build; meet the needs and reach the population goal.',
      'Tippe auf Karten zum Bauen und erreiche das Bevölkerungsziel.',
      'Toca las cartas para construir y alcanza la población.',
      'Touchez les cartes pour construire et atteignez la population.',
    ],
  ),
  tutorial: const [
    TutorialStep(Icons.touch_app, [
      'Kartlara dokun: bina al. Sarı fiyat = alınabilir.',
      'Tap cards to buy. Yellow price = affordable.',
      'Karten antippen zum Kaufen.',
      'Toca las cartas para comprar.',
      'Touchez les cartes pour acheter.',
    ]),
    TutorialStep(Icons.pan_tool, [
      'Sahneye dokun: çalış, hasat et veya ilk müşteriye hemen servis yap.',
      'Tap the scene: work, harvest or serve the first customer.',
      'Szene antippen: arbeiten oder ernten.',
      'Toca la escena: trabajar o cosechar.',
      'Touchez la scène : travailler ou récolter.',
    ]),
    TutorialStep(Icons.bolt, [
      'Çılgın üretim güçlendirmesi 20 sn boyunca üretimi ikiye katlar.',
      'Production frenzy doubles output for 20 s.',
      'Produktionsrausch verdoppelt 20 s.',
      'Frenesí duplica la producción 20 s.',
      'Frénésie : production ×2 pendant 20 s.',
    ]),
  ],
);
