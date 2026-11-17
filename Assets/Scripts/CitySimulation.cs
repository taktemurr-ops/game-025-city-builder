using System;
using System.Collections.Generic;

namespace GameStudio.CityBuilder
{
    [Serializable]
    public sealed class CitySnapshot
    {
        public int Turn;
        public int Population;
        public int Food;
        public int Coins;
        public int Farms;
        public int Houses;
        public bool GameOver;
    }

    /// <summary>Pure, deterministic rules. UI, storage, advertising and purchases are external adapters.</summary>
    public sealed class CitySimulation
    {
        private CitySnapshot state;
        public CitySnapshot State { get { return Copy(state); } }

        public CitySimulation() : this(new CitySnapshot { Turn = 0, Population = 4, Food = 12, Coins = 30, Farms = 0, Houses = 1 }) { }

        public CitySimulation(CitySnapshot saved)
        {
            if (saved == null || saved.Turn < 0 || saved.Population < 0 || saved.Food < 0 ||
                saved.Coins < 0 || saved.Farms < 0 || saved.Houses < 0 ||
                saved.Population > checked(saved.Houses * 5))
                throw new ArgumentException("Invalid city snapshot", nameof(saved));
            state = Copy(saved);
        }

        public bool BuildFarm()
        {
            if (state.GameOver || state.Coins < 12) return false;
            state.Coins -= 12;
            state.Farms++;
            return true;
        }

        public bool BuildHouse()
        {
            if (state.GameOver || state.Coins < 18) return false;
            state.Coins -= 18;
            state.Houses++;
            return true;
        }

        public bool WelcomeResident()
        {
            if (state.GameOver || state.Population >= state.Houses * 5 || state.Food < 2) return false;
            state.Food -= 2;
            state.Population++;
            return true;
        }

        public void AdvanceTurn()
        {
            if (state.GameOver) return;
            state.Turn++;
            state.Food += state.Farms * 5;
            int meals = Math.Min(state.Food, state.Population);
            state.Food -= meals;
            int hungry = state.Population - meals;
            state.Population -= hungry;
            state.Coins += state.Population * 2;
            if (state.Population == 0) state.GameOver = true;
        }

        private static CitySnapshot Copy(CitySnapshot x)
        {
            return new CitySnapshot { Turn=x.Turn, Population=x.Population, Food=x.Food,
                Coins=x.Coins, Farms=x.Farms, Houses=x.Houses, GameOver=x.GameOver };
        }
    }
}
