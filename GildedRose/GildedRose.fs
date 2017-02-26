module GildedRose

open System.Collections.Generic

type ItemQuality = int 

type Item = { Name: string; SellIn: int; Quality: int }

type ModifierRule = { CanModify : Item -> bool; Modify : Item->Item }

type ModifierRules = ModifierRule list

let updateSellIn item = 
    if item.Name <> "Sulfuras, Hand of Ragnaros" then                 
        { Name = item.Name; SellIn  = (item.SellIn - 1); Quality = item.Quality } 
    else 
        item

let downgradeQuality item = 
    if item.Quality > 0 && item.Name <> "Sulfuras, Hand of Ragnaros" then
        { Name = item.Name; SellIn = item.SellIn; Quality = item.Quality - 1 } 
    else
        item

let addOneQualityPoint item = { Name = item.Name; SellIn = item.SellIn; Quality = item.Quality+1}

let modifyQuality modifer canModify item = 
    if (canModify item) then
        modifer item
    else 
        item

let rec applyModifiersRulesQuality modifierRules item = 
    match modifierRules with 
        | head::tail -> 
            if head.CanModify item then
                head.Modify item |> applyModifiersRulesQuality tail
            else
                item
        | [] -> item

let updateIfOutdated item = 
    if item.SellIn < 0 then
        match item.Name with
            | "Aged Brie" -> 
                if item.Quality < 50 then
                    { item with Quality   = (item.Quality + 1) }
                else
                    item
            | "Backstage passes to a TAFKAL80ETC concert" -> { item with Quality = 0 } 
            | _ -> downgradeQuality item
    else
        item

let filterOverFiftyQuality item = item.Quality < 50

let filterBackStage item = item.Name = "Backstage passes to a TAFKAL80ETC concert"

let filterSellIn value item = item.SellIn < value

let filter2State item = filterBackStage item && filterSellIn 11 item && filterOverFiftyQuality item

let filter3State item = filterSellIn 6 item && filterOverFiftyQuality item

type GildedRose(items:IList<Item>) =
    let Items = items

    let specialItemRule = { CanModify = filterOverFiftyQuality; Modify = addOneQualityPoint }::
                          { CanModify = filter2State; Modify = addOneQualityPoint }::
                          { CanModify = filter3State; Modify = addOneQualityPoint }::[]

    member this.UpdateQuality() =
        for i = 0 to Items.Count - 1 do
            // Standard item quality update
            if Items.[i].Name <> "Aged Brie" && Items.[i].Name <> "Backstage passes to a TAFKAL80ETC concert" then
                Items.[i] <- downgradeQuality Items.[i]
            // Special item quality update
            else
                Items.[i] <- applyModifiersRulesQuality specialItemRule Items.[i]
            // Update SellIn
            Items.[i] <- (updateSellIn Items.[i])
            // update quality for special item once again
            Items.[i] <- updateIfOutdated Items.[i]
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