using System;
namespace Weather
{

    public interface IWeather
    {

        Area Change(Plain a);
        Area Change(GrassLand a);
        Area Change(Lake a);
    }


    class Sunny : IWeather
    {


        public Area Change(Plain a) {
            a.ModifyWater(-3);
            a.ModifyHumidity(5);
            return a.Transform();
        }


        public Area Change(GrassLand a)
        {
            a.ModifyWater(-6);
            a.ModifyHumidity(10);


            return a.Transform();
        }
        public Area Change(Lake a) {
            a.ModifyWater(-10);
            a.ModifyHumidity(15);

            return a.Transform();

        }

        private Sunny() { }

        private static Sunny? instance = null;

        public static Sunny Instance()
        {
            if (instance == null)
            {
                instance = new Sunny();
            }
            return instance;
        }

        public override string ToString()
        {
            return "Sunny";
        }
    }


    class Cloudy : IWeather
    {

        public Area Change(Plain a)
        {
            a.ModifyWater(-1);
            a.ModifyHumidity(5);

            return a.Transform();

        }
        public Area Change(GrassLand a)
        {
            a.ModifyWater(-2);
            a.ModifyHumidity(10);

            return a.Transform();
        }
        public Area Change(Lake a)
        {
            a.ModifyWater(-3);
            a.ModifyHumidity(15);

            return a.Transform();
        } 

       
        private static Cloudy? instance = null;

        public static Cloudy Instance()
        {
            if (instance == null)
            {
                instance = new Cloudy();
            }
            return instance;
        }


        public override string ToString()
        {
            return "Cloudy";
        }
    }

    class Rainy : IWeather
    {
        public Area Change(Plain a)
        {
            a.ModifyWater(20);
            a.ModifyHumidity(5);

            a.SetHumidity(30);
            return a.Transform();
        }
        public Area Change(GrassLand a)
        {
            a.ModifyWater(15);
            a.ModifyHumidity(10);

            a.SetHumidity(30);
            return a.Transform();
        }
        public Area Change(Lake a)
        {
            a.ModifyWater(20);
            a.ModifyHumidity(15);

            a.SetHumidity(30);
            return a.Transform();
        }

        


        private static Rainy? instance = null;

        public static Rainy Instance()
        {
            if (instance == null)
            {
                instance = new Rainy();
            }
            return instance;
        }

        

        public override string ToString()
        {
            return "Rainy";
        }
    }
}

