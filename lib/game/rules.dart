import 'package:studio_core/core/arcade.dart';
import 'package:studio_core/core/missions.dart';
import 'package:studio_core/core/stats.dart';
import 'city_spec.dart';
import 'tycoon_rules.dart';

class CityRules extends TycoonRules {
  CityRules()
      : super(citySpec, 'game-025-city-builder', const [
          GameStat('population_peak', 'En yüksek nüfus', 'Peak population', scale: 80),
          GameStat('buildings_bought', 'İmar edilen bölge', 'Zones built', scale: 40),
          GameStat('money_earned', 'Vergi geliri', 'Tax income', scale: 5000),
        ], const [
          MissionTemplate('population_peak', 'population_peak', [26, 53, 80]),
          MissionTemplate('buildings_bought', 'buildings_bought', [13, 26, 40]),
        ]);
}

ArcadeRules createRules() => CityRules();
