using System;
using NUnit.Framework;
using GameStudio.CityBuilder;

namespace GameStudio.CityBuilder.Tests
{
    public class CitySimulationTests
    {
        [Test] public void FarmCostsCoinsAndProducesFood()
        {
            var city = new CitySimulation();
            Assert.IsTrue(city.BuildFarm());
            Assert.AreEqual(18, city.State.Coins);
            city.AdvanceTurn();
            Assert.AreEqual(13, city.State.Food);
        }

        [Test] public void CannotBuildWithoutFunds()
        {
            var city = new CitySimulation(new CitySnapshot { Population=1, Houses=1, Food=2, Coins=0 });
            Assert.IsFalse(city.BuildFarm());
            Assert.IsFalse(city.BuildHouse());
            Assert.AreEqual(0, city.State.Farms);
        }

        [Test] public void HousingLimitsPopulation()
        {
            var city = new CitySimulation(new CitySnapshot { Population=5, Houses=1, Food=20 });
            Assert.IsFalse(city.WelcomeResident());
            Assert.AreEqual(5, city.State.Population);
        }

        [Test] public void StarvationEndsGameAndPreventsConstruction()
        {
            var city = new CitySimulation(new CitySnapshot { Population=2, Houses=1, Food=0, Coins=30 });
            city.AdvanceTurn();
            Assert.IsTrue(city.State.GameOver);
            Assert.AreEqual(0, city.State.Population);
            Assert.IsFalse(city.BuildFarm());
        }

        [Test] public void SnapshotIsDefensiveCopyAndCanRestore()
        {
            var city = new CitySimulation();
            var snapshot = city.State;
            snapshot.Coins = 999;
            Assert.AreEqual(30, city.State.Coins);
            var restored = new CitySimulation(city.State);
            Assert.AreEqual(city.State.Food, restored.State.Food);
        }

        [Test] public void RejectsInvalidSave()
        {
            Assert.Throws<ArgumentException>(() => new CitySimulation(new CitySnapshot { Population=6, Houses=1 }));
        }
    }
}
