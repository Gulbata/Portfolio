using System;
using BlockMatrix;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TextFile;
using System.ComponentModel;

namespace BlockMatrixbackup
{
	public class BM
	{


        public class InvalidIndexException : Exception { };
        public class OutOfBlocksException : Exception { };
        public class DimensionMismatchException : Exception { };


        public List<int> vec;
        private int size;
        private int size_b1;
        private int size_b2;
        private int length;


        public int this[int i, int j]
        {
            get
            {
                if (i >= 0 && i < Size && j >= 0 && j < Size)
                {
                    if (this.isValidIndex(i, j))
                    {
                        return vec[ind(i, j)];
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    throw new InvalidIndexException();
                }
                
            }

            set
            {
                if (i >= 0 && i < Size && j >= 0 && j < Size)
                {
                    if (this.isValidIndex(i, j))
                    {
                       vec[ind(i, j)]= value;
                    }
                    else
                    {
                        throw new OutOfBlocksException();
                    }
                }
                else
                {
                    throw new InvalidIndexException();
                }
            }
        }

        public int Size    { get { return size;   } set { size    = value; } }
        public int Size_b1 { get { return size_b1;} set { size_b1 = value; } }
        public int Size_b2 { get { return size_b2;} set { size_b2 = value; } }
        public int Length  { get { return length; } set { length  = value; } }

       
        //main constructor, if sizes of block 1 and 2 are not provided, default to 3,5

        
        public BM(int size_b1 = 3, int size_b2 = 5)
        {
            this.size = size_b1 + size_b2;
            this.size_b1 = size_b1;
            this.size_b2 = size_b2;
            this.length = (size_b1 * size_b1) + (size_b2 * size_b2);

            vec = new List<int>();
            for (int i = 1; i <= length; i++)
            {
                vec.Add(1);
            }
        }
        


       
        // copying  constructor
        /*
        public BM(in BM m) : this(m.Size_b1, m.Size_b2)
        {

            for (int i = 0; i < m.Length; i++)
            {
                this.vec[i] = m.vec[i];
            }
        }
        */
        
        public BM(in BM m) : this(m.Size_b1, m.Size_b2)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (isValidIndex(i, j))
                    {
                        this[i, j] = m[i,j];
                    }
                }
            }
        }



        
        /*
        public static BM operator +(BM a, BM b)
        {   //check if block sizes are the same
            BM c = new BM(a.Size_b1, a.Size_b2);

            for (int i = 0; i < a.Length; i++)
            {
                c.vec[i]= a.vec[i] + b.vec[i];
            }

            return c;
        }
        */


        public static BM operator +(BM a, BM b)
        {   //check if block sizes are the same
            BM c = new BM(a.Size_b1, a.Size_b2);

            for (int i = 0; i < a.Size; i++)
            {
                for (int j = 0; j < a.Size; j++)
                {
                    if (a.isValidIndex(i, j))
                    {
                        c[i, j] = a[i, j] + b[i, j]; 
                    }
                }
            }

            return c;
        }


        public static BM operator *(BM a, BM b)
        {
            BM c = new BM(a.Size_b1, a.Size_b2);

            if (hasSameSizeBlocks(a, b))
            {
                for (int i = 0; i < c.Size; i++)
                {
                    for (int j = 0; j < c.Size; j++)
                    {

                        if (c.isValidIndex(i, j))
                        {
                            c.vec[c.ind(i, j)] = 0;

                            for (int k = 0; k < c.size; k++)
                            {
                                if (c.isValidIndex(i, k) && c.isValidIndex(k, j))
                                {
                                    c.vec[c.ind(i, j)] += a.vec[a.ind(i, k)] * b.vec[b.ind(k, j)];
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                throw new Exception();
            }

            return c;
        }

        //Working/Errorchecking
        public int ind(int i, int j)
        {

            int fb1 = 0;          //index Of first  Raw & Column Of block 1
            int lb1 = Size_b1 - 1;//index Of Last   Raw & Column Of block 1
            int fb2 = Size_b1;    //index Of first  Raw & Column Of block 2


            //indexing from zero 
            if (i <= lb1 )
            {
                
                int a =   ((i - fb1) * Size_b1 + j);
             

                return a;
            }
            else
            {
                int a =  ind ((lb1), (lb1)) + (i - fb2)  * (Size_b2) + (j - (lb1));
     
                return a;
            }
        }

       


        //printing elements from vector in matrix form
        public void print()
        {

            String str = "";
            str += Size + "x" + Size + "(" + Size_b1 + "x" + Size_b1 + ")" + "(" + Size_b2 + "x" + Size_b2 + ")" + "\n";
            Console.Write(str);
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (isValidIndex(i, j))
                    {
                        Console.Write("{0,4} ",vec[ind(i, j)]);
                    }
                    else
                    {
                        Console.Write("{0,4} ", 0);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }



        //Done
        public override string ToString()
        {
            String str = "";
            str += Size + "x" + Size + "(" + Size_b1 + "x" + Size_b1 + ")" + "(" + Size_b2 + "x" + Size_b2 + ")" +  "\n";

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (isValidIndex(i, j))
                    {
                        str += String.Format("{0,4} ", vec[ind(i, j)]);
                    }
                    else
                    {
                        str += String.Format("{0,4} ", 0);
                    }
                }
                str += "\n";
            }
            str += "\n";

            return str;
        }



        //Working/Done
        private bool isValidIndex(int i, int j)
        {
            return (i < Size_b1 & j < Size_b1) || (i >= Size_b1 & j >= Size_b1);
        }


        //Working/Done    
        public static bool hasSameSizeBlocks(BM a, BM b)
        {
            if (a.Size_b1 == b.Size_b1 && a.Size_b2 ==b.Size_b2)
            {
                return true;
            }
            return false;
        }

        //not static version of same hasSameSizeBlocks
        public bool hasSameSizeBlockss(BM b)
        {
            if (this.Size_b1 == b.Size_b1 && this.Size_b2 == b.Size_b2)
            {
                return true;
            }
            return false;
        }
	}
}

