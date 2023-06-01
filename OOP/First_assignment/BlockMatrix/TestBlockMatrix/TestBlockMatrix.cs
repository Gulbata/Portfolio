using BlockMatrix;
using System;
using System.Drawing;
using System.Security.Principal;


namespace TestBlockMatrix;

[TestClass]
public class TestBlockMatrix
{
    //Creating, reading, and writing matrices of different size.a) 0, 1, 2, 5-size matrix

    [TestMethod]
    public void TestConstructor()
    {
        Assert.ThrowsException<BM.InvalidBlockDimentionException>(() => new BM (0, 2));
        Assert.ThrowsException<BM.InvalidBlockDimentionException>(() => new BM (1, 111));
        Assert.ThrowsException<BM.InvalidBlockDimentionException>(() => new BM (-1, 2));
        Assert.ThrowsException<BM.InvalidBlockDimentionException>(() => new BM (1, 111));
        Assert.ThrowsException<BM.InvalidBlockDimentionException>(() => new BM (-1, 111));

        BM a = new();

       

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if ((i < 2 & j < 2) || (i >= 2 & j >= 2))
                {
                    Assert.AreEqual(0, a[i, j]);
                }
            }
        }

        BM f = new(6);



        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j <11; j++)
            {
                if ((i < 2 & j < 2) || (i >= 2 & j >= 2))
                {
                    Assert.AreEqual(0, f[i, j]);
                }
            }
        }

        Assert.ThrowsException<BM.OutOfBlocksException>(() => f[2, 7] = 4);

        Assert.ThrowsException<BM.InvalidIndexException>(() => f[11, 11] = 4);




        BM b = new(2, 4);

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if ((i < 2 & j < 2) || (i >= 2 & j >= 2))
                {
                    Assert.AreEqual(0, a[i, j]);
                }
            }
        }


        Assert.ThrowsException<BM.OutOfBlocksException>(() => b[1, 4] = 4);

        Assert.ThrowsException<BM.InvalidIndexException>(() => b[-1, 11] = 4);




        BM c = new BM(2, 4, 6);

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if ((i < 2 & j < 2) || (i >= 2 & j >= 2))
                {
                    Assert.AreEqual(6, c[i, j]);
                }
            }
        }
        //Assert.AreEqual(6, a[1, 1]);

        Assert.ThrowsException<BM.OutOfBlocksException>(() => c[1, 3] = 4);

        Assert.ThrowsException<BM.InvalidIndexException>(() => c[11, 11] = 4);
        Assert.ThrowsException<BM.InvalidIndexException>(() =>  c[11, 11]);





    }

    //2)Test Copy constructor

    //a)Creating matrix b based on matrix a, comparing the entries of the two matrices.
    //  Then, changing one of the matrices and comparing the entries of the two matrices.
    [TestMethod]
    public void TestCopyConstructor()
    {
        BM a = new(5, 5, 5);
        BM b = new(a);


        Assert.IsTrue(a.Equals(b));

        a[2, 2] = 4;

        Assert.IsFalse(a.Equals(b));
    }



    //3)Getting and Settting the entry
    [TestMethod]
    public void GetSet()
    {
        BM a = new(5, 5, 5);
        BM b = new(5, 5, 5);
        BM c = new(5, 4, 5);


        Assert.AreEqual(5, a[2, 2]);
        Assert.AreEqual(5, a[4, 4]);
        Assert.AreEqual(5, a[0, 0]);
        Assert.AreEqual(5, a[0, 4]);
        Assert.AreEqual(5, a[5, 5]);
        Assert.AreEqual(5, a[9, 5]);
        Assert.AreEqual(5, a[5, 9]);


        a[2, 2] = 4;



        Assert.AreEqual(4, a[2, 2]);

        Assert.AreEqual(0, a[2, 7]);


        int r;

        Assert.ThrowsException<BM.OutOfBlocksException>(() => a[2, 7] = 4);

        Assert.ThrowsException<BM.InvalidIndexException>(() => a[11, 11] = 4);
        Assert.ThrowsException<BM.InvalidIndexException>(() => r = a[11, 11]);



    }

    //4)Comparision of martices

    //a)Executing command a=a for matrix a.
    //b)Executing command b = a for matrices a and b (with and without same size),
    //  comparing the entries of the two matrices.Then, changing one of the matrices
    //  and comparing the entries of the two matrices.

    [TestMethod]
    public void TestIsEqual()
    {
        BM a = new(5, 5, 5);
        BM b = new(5, 5, 5);
        BM c = new(5, 4, 5);

        Assert.IsTrue(a.Equals(a));
        Assert.IsTrue(a.Equals(b));
        Assert.IsFalse(a.Equals(c));

        a[2, 2] = 4;

        Assert.IsTrue(a.Equals(a));
        Assert.IsFalse(a.Equals(b));
        Assert.IsFalse(a.Equals(c));


    }
    

    //5) Sum of two matrices, command c:=a+b. //

    //a)With matrices of different block sizes 
    [TestMethod]
    
    [DataRow(5, 4, 5)]
    [DataRow(1, 5, 5)]
    [DataRow(5, 1, 5)]

    public void TestAddException(int b1, int b2, int value1)
    {
        BM a = new(b1, b1, value1);
        BM b = new(b2, b2, value1);
        Assert.ThrowsException<BM.DimensionMismatchException>(() => a + b);
    }
    //b)Checking the commutativity (a + b == b + a) 
    //c)Checking the associativity(a + b + c == (a + b) + c == a + (b + c)) 
    //d)Checking the neutral element(a + 0 == a, where 0 is the null matrix)

    [TestMethod]
    [DataRow(5, 5, 5, 5, 10)]
    [DataRow(5, 5, 2, 7, 9)]

    [DataRow(5, 5, 0, 0, 0)]
    [DataRow(5, 5, 100, 0, 100)]
    [DataRow(5, 5, 0, 100, 100)]
    

    [DataRow(1, 0, 0, 0, 0)]
    [DataRow(1, 0, 100, 0, 100)]
    [DataRow(1, 0, 0, 100, 100)]

    public void TestAddT(int b1, int b2, int value1, int value2, int value3)
    {

       
        BM a = new(b1, b2, value1);
        BM b = new(b1, b2, value2);
        BM c = new(b1, b2, value3);


        Assert.IsTrue((a+b).Equals(b + a));

        Assert.IsTrue((a + b+ c).Equals((b + a) + c ));
        Assert.IsTrue((a + b + c).Equals(b + (a + c)));

        Assert.IsTrue((a + b).Equals(c));

    }

    //Multiplication of two matrices, command c:=a*b.


    //a)With matrices of different block sizes

    [TestMethod]
    [DataRow(5, 4, 5)]
    [DataRow(1, 5, 5)]
    [DataRow(5, 1, 5)]

    public void TestprodException(int b1, int b2, int value1)
    {
        BM a = new(b1, b1, value1);
        BM b = new(b2, b2, value1);
        Assert.ThrowsException<BM.DimensionMismatchException>(() => a * b);
    }


    //b)Checking the commutativity(a* b == b* a)
    //c)Checking the associativity(a* b * c == (a* b)* c == a* (b* c))
    //d)Checking the neutral element(a* 0 == 0, where 0 is the null matrix)
    //e)Checking the identity element(a* 1 == a, where 1 is the identity matrix)


    [TestMethod]
    [DataRow(5, 5, 5, 5, 125)]
    [DataRow(5, 5, 2, 7, 70)]

    [DataRow(5, 5, 0, 0, 0)]
    [DataRow(5, 5, 100, 0, 0)]
    [DataRow(5, 5, 0, 100, 0)]

    
    [DataRow(5, 5, 100, 1, 500)]
    [DataRow(5, 5, 1, 100, 500)]


    public void Testmult(int b1, int b2, int value1, int value2, int value3)
    {
        BM a = new(b1, b2, value1);
        BM b = new(b1, b2, value2);
        BM c = new(b1, b2, value3);

        BM identity = new(5, 5, 0);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j <10; j++)
            {
                if (i==j)
                {
                    identity[i, j] = 1;
                }
            }
        }

        Assert.IsTrue((a * b).Equals(a * b));
        Assert.IsTrue((a * b * c).Equals((a * b)* c));
        Assert.IsTrue((a * b * c).Equals(a * (b * c)));

        Console.WriteLine(identity);


        Assert.IsTrue(c.Equals(a * b));

        Assert.IsTrue(a.Equals(a * identity));
    }


 
}
