using System;
using System.Drawing;

namespace BlockMatrix
{
	public class Menu
	{
        public List<BM> vec = new();


        //runs menu and program respectively.
        public void Run()
        {
            int v = 0;


            

            do
            {
                MenuWrite();

                try
                {
                    v = int.Parse(Console.ReadLine()!);
                }
                catch (System.FormatException) { v = -1; }

                switch (v)
                {
                    case 1:
                        GetElement();
                        break;
                    case 2:
                        SetElement();
                        break;
                    case 3:
                        PrintMatrix();
                        break;
                    case 4:
                        AddMatrix();
                        break;
                    case 5:
                        Sum();
                        break;
                    case 6:
                        Mul();
                        break;
                    case 7:
                        AddMatrices(4,4);
                        AddMatrices(4,4);
                        AddMatrices(4,6);
                        AddMatrices(6,2);
                        break;
                    case 8:
                        PrintAll();
                        break;
                }
            }
            while (v != 0);


        }
        //Menu to be printed 
        public void MenuWrite()
        {
            Console.WriteLine("\n\n 0. - Quit");
            Console.WriteLine(" 1. - Get an element");
            Console.WriteLine(" 2. - Overwrite an element");
            Console.WriteLine(" 3. - Print a matrix");
            Console.WriteLine(" 4. - Set a matrix");
            Console.WriteLine(" 5. - Add matrices");
            Console.WriteLine(" 6. - Multiply matrices");
            Console.WriteLine(" 7. - add 4 matreces     (for demonstration purposes)");
            Console.WriteLine(" 8. - Print all matrices (for demonstration purposes)");
            Console.Write(" Choose: ");
        }


        //Gets index of a matrix (0 indexing)
        private int GetIndex()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return -1;
            }

            int n = 0;
            bool ok;
            do
            {
                Console.Write("Give a matrix index: ");
                ok = false;
                try
                {
                    n = int.Parse(Console.ReadLine()!);
                    ok = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Integer is expected!");
                }
                if (n <= 0 || n > vec.Count)
                {
                    ok = false;
                    Console.WriteLine("No such matrix!");
                }
            } while (!ok);
            return n - 1;
        }




        //Gets dimention of a matrix (1 indexing)
        private int GetDimention()
        {
            
            int n = 0;
            bool ok;
            do
            {
                Console.Write("Give a matrix index between 1 and 10: ");
                ok = false;
                try
                {
                    n = int.Parse(Console.ReadLine()!);
                    ok = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Integer is expected!");
                }
                if (n < 0 || n > 100)
                {
                    ok = false;
                    Console.WriteLine("Give a matrix index between 1 and 10!");
                }
            } while (!ok);
            return n;
        }



        //Gets the element of a particular matrix
        private void GetElement()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }

            int ind = GetIndex();

            do
            {
                try
                {
                    Console.WriteLine("Give the index of the row: ");
                    int i = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Give the index of the column: ");
                    int j = int.Parse(Console.ReadLine()!);
                    Console.WriteLine($"a[{i},{j}]={vec[ind][i-1, j-1 ]}");
                    break;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine($"Raw and column index must be between 1 and {vec[ind].Size}");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine($"Raw and column index must be between 1 and {vec[ind].Size}");
                }
            } while (true);
        }


        //Sets the element of a particular matrix
        private void SetElement()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            int ind = GetIndex();
            do
            {
                try
                {
                    Console.WriteLine("Give the index of the row: ");
                    int i = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Give the index of the column: ");
                    int j = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Give the value: ");
                    int e = int.Parse(Console.ReadLine()!);
                    vec[ind][i - 1, j - 1] = e;
                    break;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine($"Index must be integer between 1 and {vec[ind].Size}");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine($"Index must be between 1 and {vec[ind].Size}");
                }
                catch (BM.OutOfBlocksException)
                {
                    Console.WriteLine("Only the elements in the block1 and block2  may be rewritten");
                }
            } while (true);

            Console.WriteLine(vec[ind]);
        }


        //Prints all the matrices of a vector
        public void PrintAll()
        {

            String a = "There are    ";
            String b = String.Format($"{vec.Count} matrices ");
            String c = "in vector  ";
            
            String str  = String.Format("* {0,16}   {1,13}   {2,13} *", a, b, c);

            String stars = "****************************************************";
            Console.WriteLine(stars);
            Console.WriteLine(str);
            

            for (int i = 0; i < vec.Count; i++)
            {
                String d = "Matrix at    ";
                String e = "position  ";
                String f = String.Format($"{i+1}      ");

                String insideStr = String.Format("* {0,16}   {1,13}   {2,13} * \n", d, e, f);

                Console.WriteLine(stars);
                Console.Write(insideStr);
               

                Console.WriteLine(vec[i]);
                Console.WriteLine(stars + "\n");
            }

            
        }


        //Prints one particular matrix
        public void PrintMatrix()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }

            int ind = GetIndex();
            Console.Write(vec[ind]);
        }



        ///Adds one particular matrix.
        private void AddMatrix()
        {
            

            int b1 = GetDimention();
            int b2 = GetDimention();


            BM d = new BM(b1,b2);

            

            bool ok = false;
            List<int> elements = new List<int>();
            for (int i = 0; i < d.Length; i++)
            {
                do
                {
                    Console.Write("Element: ");
                    try
                    {
                        int elem = int.Parse(Console.ReadLine()!);
                        elements.Add(elem);
                        ok = true;
                        break;
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("Number is expected!");
                    
                    }
                } while (true);
                    
            }

            if (ok)
            {   
                d.Set(elements);
                vec.Add(d);
            }

            PrintAll();
        }



        //adds matrix with random integers inside the blocks
        private void AddMatrices(int bb1 = 4, int bb2 = 4)
        {

            int b1 = bb1;
            int b2 = bb2;

            BM d = new BM(b1, b2);

            for (int i = 0; i < d.Length; i++)
            {
                Random a = new Random();
                d.vec[i] = a.Next(20);
            }

            vec.Add(d);
         
        }



        //Sums two matrices with same b1 and b2 dimentions and prints the result on the screen
        public void Sum()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }

            Console.Write("1st matrix: ");
            int ind1 = GetIndex();
            Console.Write("2nd matrix: ");
            int ind2 = GetIndex();
            try
            {
                Console.Write((vec[ind1] + vec[ind2]));
            }
            catch (BM.DimensionMismatchException)
            {
                Console.WriteLine("Dimension mismatch!");
            }
        }


        //Sums two matrices with same b1 and b2 dimentions and prints the result on the screen
        public void Mul()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            Console.Write("1st matrix: ");
            int ind1 = GetIndex();
            Console.Write("2nd matrix: ");
            int ind2 = GetIndex();
            try
            {
                Console.Write((vec[ind1] * vec[ind2]));
            }
            catch (BM.DimensionMismatchException)
            {
                Console.WriteLine("Dimension mismatch!");
            }
        }



    }
}

