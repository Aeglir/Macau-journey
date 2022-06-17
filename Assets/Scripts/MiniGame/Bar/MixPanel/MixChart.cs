using System.Collections.Generic;
using System.Linq;
using static MiniGame.Bar.Mix.MixPanel;

namespace MiniGame.Bar.Mix
{
    public struct MaterialChart
    {
        public int[] wines;
        public int[] drinks;
        public int[] flavorings;

        public MaterialChart(int[] wines, int[] drinks, int[] flavorings)
        {
            this.wines = wines;
            this.drinks = drinks;
            this.flavorings = flavorings;
        }
    }
    public class MixChart : MaterialAdder<MATERIALTYPE,int>
    {
        private List<int> wines;
        private List<int> drinks;
        private List<int> flavorings;
        internal MaterialChart result{
            get{
                wines.Sort();
                drinks.Sort();
                flavorings.Sort();
                return new MaterialChart(wines.ToArray(),drinks.ToArray(),flavorings.ToArray());
            }
        }
        int current;
        internal MixChart()
        {
            wines = new List<int>();
            drinks = new List<int>();
            flavorings = new List<int>();
        }
        internal bool isEmpty()
        {
            return wines.Count==0&&drinks.Count==0&&flavorings.Count==0;
        }
        internal void Clear()
        {
            wines.Clear();
            drinks.Clear();
            flavorings.Clear();
            current=0;
        }
        private void AddWine(int tag)
        {
            wines.Add(tag);
            current++;
        }
        private void AddDrink(int tag)
        {
            drinks.Add(tag);
            current++;
        }
        private void AddFlavoring(int tag)
        {
            flavorings.Add(tag);
            current++;
        }

        public void addMaterial(MATERIALTYPE type,int tag)
        {
            switch(type)
            {
                case MATERIALTYPE.WINE:
                AddWine(tag);
                break;
                case MATERIALTYPE.DRINK:
                AddDrink(tag);
                break;
                case MATERIALTYPE.FLAVORING:
                AddFlavoring(tag);
                break;
            }
        }
    }
}
