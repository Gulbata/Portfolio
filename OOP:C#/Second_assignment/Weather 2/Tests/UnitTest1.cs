
using Weather;
namespace Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Testconstructors()
    {
        Plain a = new("Ben",45);
        a.SetHumidity(98);

        Assert.AreEqual("Ben",a.Name);
        Assert.AreEqual(45, a.Water);


        GrassLand b = new(a);

        Assert.AreEqual(a.Name, b.Name);

        Assert.AreEqual(a.Water, a.Water);

        Assert.AreEqual(a.Humidity, a.Humidity);

    }


    [TestMethod]
    public void traverseCicle()
    {
        Lake a = new("Ben", 90);
        a.SetHumidity(98);

        Assert.AreEqual("Ben", a.Name);
        Assert.AreEqual(90, a.Water);




        GrassLand b = new(a);

        List<Area> c = new();

        c.Add(b);
        c.Add(a);



        Area.TraverseCicles(ref c);


        Assert.AreEqual(110, a.Water);
        Assert.AreEqual(30, a.Humidity);

        Assert.AreEqual(105, b.Water);
        Assert.AreEqual(30, b.Humidity);



        Plain aa = new("rt", 30);
        aa.SetHumidity(30);
        

        Plain ab = new("rt", 11);
        ab.SetHumidity(80);
        


        Plain ac = new("sdf", 57);
        ac.SetHumidity(50);



        GrassLand ca = new("rt", 30);
        ca.SetHumidity(30);
        

        GrassLand cb = new("rt", 11);
        cb.SetHumidity(70);
        


        GrassLand cc = new("sdf", 57);
        cc.SetHumidity(50);

        c.Add(aa);
        c.Add(ab);
        c.Add(ac);
        c.Add(ca);
        c.Add(ab);
        c.Add(cc);


        Area.TraverseCicles(ref c);


        Assert.AreEqual(30, cc.Humidity);
        Assert.AreEqual(28, ab.Water);


        Assert.AreEqual(72, cc.Water);
        Assert.AreEqual(35, ab.Humidity);



    }


    [TestMethod]
    public void traversePlain()
    {
        Plain aa = new("rt",30);
        aa.SetHumidity(30);
        aa.Traverse(aa.GetWeather());

        Plain ab = new("rt",11);
        ab.SetHumidity(80);
        aa.Traverse(ab.GetWeather());


        Plain ac = new("sdf", 57);
        ac.SetHumidity(50);
        ac.Traverse(ac.GetWeather());


        Assert.AreEqual(47, aa.Water);
        Assert.AreEqual(30, aa.Humidity);

        Assert.AreEqual(11, ab.Water);
        Assert.AreEqual(80, ab.Humidity);

        Assert.AreEqual(77, ac.Water);
        Assert.AreEqual(30, ac.Humidity);


        
    }


    [TestMethod]
    public void traverseGrass()
    {
        

        GrassLand ca = new("rt", 30);
        ca.SetHumidity(30);
        ca.Traverse(ca.GetWeather());

        GrassLand cb = new("rt", 11);
        cb.SetHumidity(70);
        ca.Traverse(cb.GetWeather());


        GrassLand cc = new("sdf", 57);
        cc.SetHumidity(50);
        cc.Traverse(cc.GetWeather());




        Assert.AreEqual(39, ca.Water);
        Assert.AreEqual(30, ca.Humidity);

        Assert.AreEqual(11, cb.Water);
        Assert.AreEqual(70, cb.Humidity);

        Assert.AreEqual(72, cc.Water);
        Assert.AreEqual(30, cc.Humidity);



    }

    [TestMethod]
    public void traverseLake()
    {
        Lake da = new("rt", 30);
        da.SetHumidity(11);
        da.Traverse(da.GetWeather());

        Lake db = new("rt", 11);
        db.SetHumidity(68);
        da.Traverse(db.GetWeather());


        Lake dc = new("sdf", 57);
        dc.SetHumidity(43);
        dc.Traverse(dc.GetWeather());




        Assert.AreEqual(40, da.Water);
        Assert.AreEqual(30, da.Humidity);

        Assert.AreEqual(11, db.Water);
        Assert.AreEqual(68, db.Humidity);

        Assert.AreEqual(77, dc.Water);
        Assert.AreEqual(30, dc.Humidity);

    }

    
}
