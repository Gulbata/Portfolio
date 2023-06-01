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

namespace BlockMatrix
{
    public class BM
    {


        public class InvalidIndexException : Exception { };
        public class OutOfBlocksException : Exception { };
        public class DimensionMismatchException : Exception { };
        public class InvalidVectorException : Exception { };
        public class InvalidBlockDimentionException : Exception { };

        public List<int> vec;
        private int size;
        private int size_b1;
        private int size_b2;
        private int length;



        //Main constructor, if sizes of block 1 and 2 are not provided, default to 3,5
        public BM(int size_b1 = 3, int size_b2 = 5, int value = 0)
        {
            if (size_b1 <1|| size_b1 > 100 || size_b2 <0 || size_b2 >100)
            {
                throw new InvalidBlockDimentionException();
            }

            this.size = size_b1 + size_b2;
            this.size_b1 = size_b1;
            this.size_b2 = size_b2;
            this.length = (size_b1 * size_b1) + (size_b2 * size_b2);



            vec = new List<int>();
            for (int i = 1; i <= length; i++)
            {
                vec.Add(value);
            }
        }

        //Copy constructor
        public BM(in BM m) : this(m.Size_b1, m.Size_b2)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (isValidIndex(i, j))
                    {
                        this[i, j] = m[i, j];
                    }
                }
            }
        }

        //Index calculator (Matrix parameters to vector parameter converter)
        private int ind(int i, int j)
        {

            int fb1 = 0;          //index Of first  Raw & Column Of block 1
            int lb1 = Size_b1 - 1;//index Of Last   Raw & Column Of block 1
            int fb2 = Size_b1;    //index Of first  Raw & Column Of block 2


            //indexing from zero 
            if (i <= lb1)
            {

                int a = ((i - fb1) * Size_b1 + j);


                return a;
            }
            else
            {
                int a = ind((lb1), (lb1)) + (i - fb2) * (Size_b2) + (j - (lb1));

                return a;
            }
        }


        //Getter and setter of vec (matrix) elements
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
                        vec[ind(i, j)] = value;
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
        //Getters and setters of primitive fields
        public int Size { get { return size; } set { size = value; } }
        public int Size_b1 { get { return size_b1; } set { size_b1 = value; } }
        public int Size_b2 { get { return size_b2; } set { size_b2 = value; } }
        public int Length { get { return length; } set { length = value; } }



        public void Set(in List<int> list)
        {
            if (list.Count <= Length)
            {
                for (int i = 0; i < Length; i++)
                {
                    vec[i] = list[i];
                }
            }
            else

            {
                throw new InvalidVectorException();
            }
        }
    
            
        

        //Plus Operator overloading 
        public static BM operator +(BM a, BM b)
        {   //check if block sizes are the same
            BM c = new BM(a.Size_b1, a.Size_b2);

            if (a.hasSameSizeBlocks(b))
            {
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
            }
            else
            {
                throw new DimensionMismatchException();
            }
                

            return c;
        }

        //Multiplication operator overluading
        public static BM operator *(BM a, BM b)
        {
            BM c = new BM(a.Size_b1, a.Size_b2);

            if (a.hasSameSizeBlocks(b))
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
                                    c[i, j] += a[i, k] * b[k, j];
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                throw new DimensionMismatchException();
            }

            return c;
        }

        

       


        //Printing elements from vector in matrix form
        public void print()
        {

            String MatrixDimentions = String.Format($"{Size} x {Size}      ");
            String B1Dimentions = String.Format($"{Size_b1} x {Size_b1}    ");
            String B2Dimentions = String.Format($"{Size_b2} x {Size_b2}    ");
            String str = "****************************************************\n";
            str += "* Matrix Dimention * B_1 dimention * B_2 dimention * \n";
            str += String.Format("* {0,16} * {1,13} * {2,13} * \n", MatrixDimentions, B1Dimentions, B2Dimentions);
            str += "****************************************************\n";
            for (int i = 0; i < Size; i++)
            {
                
                for (int j = 0; j < Size; j++)
                {
                    if (isValidIndex(i, j))
                    {
                        Console.Write("{0,4} ",this[i, j]);
                    }
                    else
                    {
                        Console.Write("{0,4} ", 0);
                    }
                }
                
                Console.WriteLine();
            }
            
        }



        //ToString overloaded(basically same as Print method)
        public override string ToString()
        {
            String MatrixDimentions = String.Format($"{Size} x {Size}      ");
            String B1Dimentions = String.Format($"{Size_b1} x {Size_b1}    ");
            String B2Dimentions = String.Format($"{Size_b2} x {Size_b2}    ");
            String str = "****************************************************\n";
            str += "* Matrix Dimention * B_1 dimention * B_2 dimention * \n";
            str += String.Format("* {0,16} * {1,13} * {2,13} * \n", MatrixDimentions, B1Dimentions, B2Dimentions);
            str += "****************************************************\n";

            for (int i = 0; i < Size; i++)
            {
                
                for (int j = 0; j < Size; j++)
                {
                    
                    if (isValidIndex(i, j))
                    {
                        str += String.Format("{0,4} ", this[i, j]);
                    }
                    else
                    {
                        str += String.Format("{0,4} ", 0);
                    }
                }
                
                str += "\n";
            }
            

            return str;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                BM p = (BM) obj;
                return this.Size_b1 == p.Size_b1 &&
                       this.Size_b2 == p.Size_b2 &&
                       Enumerable.SequenceEqual(this.vec, p.vec);
            }


        }

        public override int GetHashCode()
        {
            return vec.Sum();
        }


        //checks if indexes provided are inside the boxes
        private bool isValidIndex(int i, int j)
        {
            return (i < Size_b1 & j < Size_b1) || (i >= Size_b1 & j >= Size_b1);
        }



        //checks if two BM has same size blocks
        private bool hasSameSizeBlocks(BM b)
        {
            if (this.Size_b1 == b.Size_b1 && this.Size_b2 == b.Size_b2)
            {
                return true;
            }
            return false;
        }
	}
}

