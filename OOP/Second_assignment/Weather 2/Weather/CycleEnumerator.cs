using System;
namespace Weather
{
	public class CycleEnumerator
	{

		AreaEnumerator cycle;
		bool allsame = false; 
		bool end = false;
        bool same;

        public CycleEnumerator()
		{
			cycle = new();
		}


		public void First() {
            same = true;
            for (cycle.First(); !cycle.End(); cycle.Next())
            {
                same = same && cycle.Current();
            }

            if (same)
            {
                allsame = true;
                end = true;
            }
        }



		public bool Current() { return allsame; }

		public void Next()
		{
            same = true;
            for (cycle.First(); !cycle.End(); cycle.Next())
            {
                same = same && cycle.Current();
            }
            if (same)
            {
                allsame = true;
                end = true;
            }
        }

		public bool End()
		{
			return end;
		}
	}
}

