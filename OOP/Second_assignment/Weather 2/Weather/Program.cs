using System;
using TextFile;
using System.Collections.Generic;

namespace Weather;

class Program
{
    static void Main()
    {

        CycleEnumerator b = new();

        int g = 1;

        for (b.First(); !b.End(); b.Next())
        {
            Console.WriteLine("{0}th iteration",g);
            g++;
        }


    }
}

