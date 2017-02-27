module GildedRose

open System.Collections.Generic

type Item = { Name: string; SellIn: int; Quality: int }

type EvoluableItem =
    | BasicItem of Item
    | AgedBrie  of Item
    | Backstage of Item
    | Sulfuras  of Item
    | UpdateError of string 

let substractQuality quality subs = (quality - subs) |> max 0

let addQuality quality add = (quality + add) |> min 50

let updateSellInWithRule = function
    | Sulfuras intenalItem  -> Sulfuras { intenalItem with SellIn = intenalItem.SellIn }
    | BasicItem intenalItem -> BasicItem { intenalItem with SellIn = intenalItem.SellIn - 1 }
    | AgedBrie intenalItem  -> AgedBrie { intenalItem with SellIn = intenalItem.SellIn - 1 }
    | Backstage intenalItem -> Backstage { intenalItem with SellIn = intenalItem.SellIn - 1 }
    | UpdateError msg       -> UpdateError msg

let basicItemQualityUpdater item =
    let locUpdater = substractQuality item.Quality
    if item.SellIn < 0 then
        locUpdater 2
    else
        locUpdater 1

let agedBrieQualityUpdater item =
    let locUpdater = addQuality item.Quality
    if item.SellIn < 0 then
        locUpdater 2
    else
        locUpdater 1

let backstageItemQualityUpdater item =
    let locUpdater = addQuality item.Quality
    if item.SellIn < 0 then
        0
    else if item.SellIn < 5 then
        locUpdater 3
    else if item.SellIn < 10 then
        locUpdater 2
    else 
        locUpdater 1

let sulfurasItemQualityUpdater item = item.Quality 

let getQualityUpdater = function
    | Sulfuras  internalItem -> (internalItem, fun () -> sulfurasItemQualityUpdater internalItem)
    | BasicItem internalItem -> (internalItem, fun () -> basicItemQualityUpdater internalItem)
    | AgedBrie  internalItem -> (internalItem, fun () -> agedBrieQualityUpdater internalItem)
    | Backstage internalItem -> (internalItem, fun () -> backstageItemQualityUpdater internalItem)
    | UpdateError msg        -> failwith msg
         
let getNewItem item =
    let createNewItem item qualityUpdater =  
        { Name = item.Name ; SellIn = item.SellIn ; Quality = qualityUpdater() }
    item |> updateSellInWithRule |> getQualityUpdater ||> createNewItem 

let createEvoluableItem item =
    match item.Name with
        | "Sulfuras, Hand of Ragnaros" -> Sulfuras item
        | "Aged Brie" -> 
            AgedBrie item
        | "Backstage passes to a TAFKAL80ETC concert" -> 
            Backstage item
        | _ -> BasicItem item 

type GildedRose(items:IList<Item>) =
    let Items = items

    member this.UpdateQuality() =
        for i = 0 to Items.Count - 1 do
            Items.[i] |> createEvoluableItem 
                      |> getNewItem
                      |> fun (item:Item) -> Items.[i] <- item
        ()

[<EntryPoint>]
let main argv = 
    printfn "OMGHAI!"
    let Items = new List<Item>()
    Items.Add({Name = "+5 Dexterity Vest"; SellIn = 10; Quality = 20})
    Items.Add({Name = "Aged Brie"; SellIn = 2; Quality = 0})
    Items.Add({Name = "Elixir of the Mongoose"; SellIn = 5; Quality = 7})
    Items.Add({Name = "Sulfuras, Hand of Ragnaros"; SellIn = 0; Quality = 80})
    Items.Add({Name = "Sulfuras, Hand of Ragnaros"; SellIn = -1; Quality = 80})
    Items.Add({Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 15; Quality = 20})
    Items.Add({Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 10; Quality = 49})
    Items.Add({Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 5; Quality = 49})
    Items.Add({Name = "Conjured Mana Cake"; SellIn = 3; Quality = 6})

    let app = new GildedRose(Items)
    for i = 0 to 30 do
        printfn "-------- day %d --------" i
        printfn "name, sellIn, quality"
        for j = 0 to Items.Count - 1 do
             printfn "%s, %d, %d" Items.[j].Name Items.[j].SellIn Items.[j].Quality
        printfn ""
        app.UpdateQuality()
    0 