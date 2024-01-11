open MorseCode
[<EntryPoint>]
let main argv =
    let filepath = "C:/Users/Mathias/source/repos/AFP/EksamensExercise/MorseCode/example1.txt"
    // MorseCode.PrintMorse.printSmorseWordsFromFileGroupedSorted filepath
    let thriteen = PrintMorse.returnMorseStringByCount filepath 13
    printfn "%A" thriteen
    printfn "%A" (PrintMorse.wordsByMorseString filepath thriteen)
    0 // return an integer exit code
