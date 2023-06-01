using System;
using System.Xml.Linq;
//grounds unda hqondes punqcia traversi (weather)//weatheri rom miigebs shecvlis groudis shesabamisad
namespace Weather
{
    public abstract class Area
    {
        private string name;
        private int water;
        private int humidity;

        public class lowLevelOfWaterException : Exception { };
        public class HumidityException : Exception { };

        


        //Getters and setters/properties
        public string Name
        {
            get { return name; }  
        }
        public int Water
        {
            get { return water; }           
        }
        public int Humidity
        {
            get { return humidity; }  
        }

        public IWeather GetWeather()
        {
            if (humidity > 70)
            {
                return Rainy.Instance();

            }
            else if (humidity >= 40)
            {
                double chance = (humidity - 30) * 0.033;
                Random random = new Random(47);
                double rand = random.NextDouble();

                if (rand <= chance)
                {
                    return Rainy.Instance();
                }
                else
                {
                    return Cloudy.Instance();
                }
            }
            else if (humidity >= 0)
            {
                return Sunny.Instance();
            }
            else
            {
                throw new HumidityException { };
            }

        }

        //constructor
        public Area(String name, int water)
        {
            this.name = name;
            this.water = water;
            this.humidity = 0;
        }

        public Area(Area area)
        {
            this.name = area.Name;
            this.water = area.Water;
            this.humidity = area.Humidity;
        }


        public void SetHumidity   (int h) { humidity = h;}
        public void ModifyWater   (int e) { if ((water + e) < 0) { throw new HumidityException { }; } else { water = water + e; } }
        public void ModifyHumidity(int h) { humidity = humidity + h;}
        public abstract Area Transform();
        public abstract Area Traverse(IWeather p);




        static public void TraverseCicles(ref List<Area> areas)
        {


            


            bool a = false;
            while (!a)
            {

                for (int i = 0; i < areas.Count; i++)
                {

                    Console.WriteLine("Before: {0}", areas[i]);
                    areas[i] = areas[i].Traverse(areas[i].GetWeather());
                    Console.WriteLine("After : {0}\n", areas[i]);


                }


                a = true;

                for (int i = 1; i < areas.Count; i++)
                {
                    bool b = areas[i - 1].GetType().Equals(areas[i].GetType());

                    a = a & b;
                    Console.WriteLine(a);
                }

            }
        }

    }

    
    public class Plain : Area
    {
        public Plain(String name, int water) : base(name, water) { }
        public Plain(Area area) : base(area) { }
        public override Area Transform()

        {
            if (this.Water > 15)
            {
                return new GrassLand(this);
            }
            else { return this; }
        }

        public override Area Traverse(IWeather p)
        {

            return p.Change(this);

        }

       

        public override string ToString()
        {
            return string.Format("| Owner: {0, -12} | Type: {1,-10} | Water: {2,-3} | Humidity: {3,-4} | Weather: {4,-6} |",
                Name, "Plain", Water, Humidity, GetWeather());
        }


    }


    public class GrassLand : Area
    {
        public GrassLand(String name, int water) : base(name, water) { }
        public GrassLand(Area area) : base(area) { }


        public override Area Transform()

        {
            if (this.Water > 50)
            {
                return new Lake(this);
            }
            else if (this.Water < 16)
            {
                return new Plain(this);
            }
            else { return this; }
        }

        public override Area Traverse(IWeather p)
        {

            return p.Change(this);

        }

        public override string ToString()
        {
            return string.Format("| Owner: {0, -12} | Type: {1,-10} | Water: {2,-3} | Humidity: {3,-4} | Weather: {4,-6} |",
                Name, "GrassLand", Water, Humidity, GetWeather());
        }

    }


    public class Lake : Area
    {
        public Lake(String name, int water) : base(name, water) { }
        public Lake(Area area) : base(area) { }


        public override Area Transform()

        {
            if (this.Water < 50)
            {
                return new GrassLand(this);
            }
            else { return this; }
        }
        
        
        public override Area Traverse(IWeather p)
        {

            return p.Change(this);

        }

        public override string ToString()
        {
            return string.Format("| Owner: {0, -12} | Type: {1,-10} | Water: {2,-3} | Humidity: {3,-4} | Weather: {4,-6} |",
                Name, "Lake", Water, Humidity, GetWeather());
        }


    }
}

