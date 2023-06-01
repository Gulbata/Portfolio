using System;
using TextFile;

namespace Weather
{
	public class AreaEnumerator
	{
        List<Area> areas = new();
        private int  count = 0;
        private int elements;
        bool b;
        




        public AreaEnumerator()
        {
            string filename;
            Console.WriteLine("Enter file name: ");
            filename = Console.ReadLine();
            TextFileReader reader = new(filename);
            //TextFileReader reader = new(@"./file.txt");

            reader.ReadLine(out string line);
            elements = int.Parse(line);

            for (int i = 0; i < elements; ++i)
            {
                char[] separators = new char[] { ' ', '\t' };
                Area area = null;

                if (reader.ReadLine(out line))
                {
                    string[] tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    string name = tokens[0] + " " + tokens[1];

                    char ch = char.Parse(tokens[2]);
                    int p = int.Parse(tokens[3]);

                    switch (ch)
                    {
                        case 'P': area = new Plain(name, p); break;
                        case 'G': area = new GrassLand(name, p); break;
                        case 'L': area = new Lake(name, p); break;
                    }
                }
                areas.Add(area);
            }

            reader.ReadLine(out string lastLine);
            int humidity = int.Parse(lastLine);


            foreach (var area in areas)
            {
                area.SetHumidity(humidity);
                Console.WriteLine(area.GetWeather());
            }
        }

        public void First()
        {
            count = 0;
            Console.WriteLine("Line from First Before: {0}{1} th element", areas[count], count);
            areas[count] = areas[count].Traverse(areas[count].GetWeather());
            Console.WriteLine("Line from First  After: {0}{1} th element", areas[count], count);
            count++;
            Next();
        }

        public bool End()
        {

            return count == elements + 1;
        }
        public void Next()
        {


            if (count == elements)
            {
                count++;
                return;
            }


            Console.WriteLine("Line from Next Before: {0}{1} th element", areas[count], count);
            areas[count] = areas[count].Traverse(areas[count].GetWeather());
            Console.WriteLine("Line from Next  After: {0}{1} th element", areas[count], count);

            count++;


        }

        public bool Current()
        {
            b = areas[count - 2].GetType().Equals(areas[count - 1].GetType());
            return b;
        }




    }
}

