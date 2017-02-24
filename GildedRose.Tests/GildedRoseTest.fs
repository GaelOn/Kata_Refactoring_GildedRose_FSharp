module GildedRoseTest

open GildedRose
open System
open System.IO
open System.Text
open NUnit.Framework
open System.Collections.Generic
open ApprovalTests
open ApprovalTests.Reporters
#nowarn "0058"

[<TestFixture>]
[<UseReporter(typeof<ApprovalTests.Reporters.NUnitReporter>)>]
type ApprovalTest () =
    [<Test>] member this.ThirtyDays () =
        let fakeoutput = new StringBuilder()
        Console.SetOut(new StringWriter(fakeoutput))
        Console.SetIn(new StringReader("a\n"))

        main Array.empty<string> |> ignore
        let output = fakeoutput.ToString()
        Approvals.Verify(output)
        ()

[<TestFixture>]
type GildedRoseTest_foo () =
    [<Test>] member this.Foo ()=
        let Items = new List<Item>()  
        Items.Add({Name = "foo"; SellIn = 0; Quality = 0})
        let app = new GildedRose(Items)
        app.UpdateQuality()
        Assert.AreEqual("foo", Items.[0].Name)
        Assert.AreEqual(-1,  Items.[0].SellIn)
        Assert.AreEqual(0, Items.[0].Quality)

    [<Test>] member this.FooOutDated ()=
        let Items = new List<Item>()  
        Items.Add({Name = "foo"; SellIn = 1; Quality = 6})
        let app = new GildedRose(Items)
        app.UpdateQuality()
        Assert.AreEqual("foo", Items.[0].Name)
        Assert.AreEqual(0,  Items.[0].SellIn)
        Assert.AreEqual(5, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("foo", Items.[0].Name)
        Assert.AreEqual(-1,  Items.[0].SellIn)
        Assert.AreEqual(3, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("foo", Items.[0].Name)
        Assert.AreEqual(-2,  Items.[0].SellIn)
        Assert.AreEqual(1, Items.[0].Quality)

[<TestFixture>]
type GildedRoseTest_Sulfuras () =
    [<Test>] member this.Sulfuras ()=
        let Items = new List<Item>()  
        Items.Add({Name = "Sulfuras, Hand of Ragnaros"; SellIn = 1; Quality = 80})
        let app = new GildedRose(Items)
        app.UpdateQuality()
        Assert.AreEqual("Sulfuras, Hand of Ragnaros", Items.[0].Name)
        Assert.AreEqual(1,  Items.[0].SellIn)
        Assert.AreEqual(80, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Sulfuras, Hand of Ragnaros", Items.[0].Name)
        Assert.AreEqual(1,  Items.[0].SellIn)
        Assert.AreEqual(80, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Sulfuras, Hand of Ragnaros", Items.[0].Name)
        Assert.AreEqual(1,  Items.[0].SellIn)
        Assert.AreEqual(80, Items.[0].Quality)

[<TestFixture>]
type GildedRoseTest_AgedBrie () =
    [<Test>] member this.AgedBrie ()=
        let Items = new List<Item>()  
        Items.Add({Name = "Aged Brie"; SellIn = 1; Quality = 48})
        let app = new GildedRose(Items)
        app.UpdateQuality()
        Assert.AreEqual("Aged Brie", Items.[0].Name)
        Assert.AreEqual(0,  Items.[0].SellIn)
        Assert.AreEqual(49, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Aged Brie", Items.[0].Name)
        Assert.AreEqual(-1,  Items.[0].SellIn)
        Assert.AreEqual(50, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Aged Brie", Items.[0].Name)
        Assert.AreEqual(-2,  Items.[0].SellIn)
        Assert.AreEqual(50, Items.[0].Quality)

[<TestFixture>]
type GildedRoseTest_Backstage () =
    [<Test>] member this.Backstage ()=
        let Items = new List<Item>()  
        Items.Add({Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 12; Quality = 10})
        let app = new GildedRose(Items)
        app.UpdateQuality()
        Assert.AreEqual("Backstage passes to a TAFKAL80ETC concert", Items.[0].Name)
        Assert.AreEqual(11,  Items.[0].SellIn)
        Assert.AreEqual(11, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Backstage passes to a TAFKAL80ETC concert", Items.[0].Name)
        Assert.AreEqual(10,  Items.[0].SellIn)
        Assert.AreEqual(12, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Backstage passes to a TAFKAL80ETC concert", Items.[0].Name)
        Assert.AreEqual(9,  Items.[0].SellIn)
        Assert.AreEqual(14, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Backstage passes to a TAFKAL80ETC concert", Items.[0].Name)
        Assert.AreEqual(8,  Items.[0].SellIn)
        Assert.AreEqual(16, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Backstage passes to a TAFKAL80ETC concert", Items.[0].Name)
        Assert.AreEqual(7,  Items.[0].SellIn)
        Assert.AreEqual(18, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Backstage passes to a TAFKAL80ETC concert", Items.[0].Name)
        Assert.AreEqual(6,  Items.[0].SellIn)
        Assert.AreEqual(20, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Backstage passes to a TAFKAL80ETC concert", Items.[0].Name)
        Assert.AreEqual(5,  Items.[0].SellIn)
        Assert.AreEqual(22, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Backstage passes to a TAFKAL80ETC concert", Items.[0].Name)
        Assert.AreEqual(4,  Items.[0].SellIn)
        Assert.AreEqual(25, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Backstage passes to a TAFKAL80ETC concert", Items.[0].Name)
        Assert.AreEqual(3,  Items.[0].SellIn)
        Assert.AreEqual(28, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Backstage passes to a TAFKAL80ETC concert", Items.[0].Name)
        Assert.AreEqual(2,  Items.[0].SellIn)
        Assert.AreEqual(31, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Backstage passes to a TAFKAL80ETC concert", Items.[0].Name)
        Assert.AreEqual(1,  Items.[0].SellIn)
        Assert.AreEqual(34, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Backstage passes to a TAFKAL80ETC concert", Items.[0].Name)
        Assert.AreEqual(0,  Items.[0].SellIn)
        Assert.AreEqual(37, Items.[0].Quality)
        app.UpdateQuality()
        Assert.AreEqual("Backstage passes to a TAFKAL80ETC concert", Items.[0].Name)
        Assert.AreEqual(-1,  Items.[0].SellIn)
        Assert.AreEqual(0, Items.[0].Quality)