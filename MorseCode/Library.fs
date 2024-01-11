namespace MorseCode

open System.IO
// // 1) Smoosed Morse Code
// For the purpose of this challenge, Morse code represents every letter as a sequence of 1-4 characters, each of which is either . (dot) or - (dash). The code for the letter a is .-, for b is -..., etc. The codes for each letter a through z are:
//
// .- -... -.-. -.. . ..-. --. .... .. .--- -.- .-.. -- -. --- .--. --.- .-. ... - ..- ...- .-- -..- -.-- --..
// Normally, you would indicate where one letter ends and the next begins, for instance with a space between the letters' codes, but for this challenge, just smoosh all the coded letters together into a single string consisting of only dashes and dots.
//
// Examples
// smorse("sos") => "...---..."
// smorse("daily") => "-...-...-..-.--"
// smorse("programmer") => ".--..-.-----..-..-----..-."
// smorse("bits") => "-.....-..."
// smorse("three") => "-.....-..."
// An obvious problem with this system is that decoding is ambiguous. For instance, both bits and three encode to the same string, so you can't tell which one you would decode to without more information.
//                                       
// Parsing every word in https://raw.githubusercontent.com/dolph/dictionary/master/enable1.txt should yield 2,499,157 dots and 1,565,081 dashes.
//
// 1) The sequence -...-....-.--. is the code for four different words (needing, nervate, niding, tiling). Find the only sequence that's the code for 13 different words in the txt file.
module PrintMorse =
    let morseCodeUnsorted = [
    ('a', ".-"); ('b', "-..."); ('c', "-.-."); ('d', "-.."); ('e', "."); ('f', "..-."); 
    ('g', "--."); ('h', "...."); ('i', ".."); ('j', ".---"); ('k', "-.-"); ('l', ".-.."); 
    ('m', "--"); ('n', "-."); ('o', "---"); ('p', ".--."); ('q', "--.-"); ('r', ".-."); 
    ('s', "..."); ('t', "-"); ('u', "..-"); ('v', "...-"); ('w', ".--"); ('x', "-..-"); 
    ('y', "-.--"); ('z', "--..")
    ] 

    let morseCode = morseCodeUnsorted |> List.sortBy (fun (_, code) -> code.Length)

    // Now morseCode is sorted by the length of the Morse code

    let morseCodeMap = Map.ofList morseCode
    
    
    //Smorse makes a string of . and - for a word 
    let smorse (word:string) =
        word.ToLower().ToCharArray()
        |> Array.map (fun c -> morseCodeMap.[c]) // makes an array of strings of morse code
        |> Array.reduce (+) // makes a single string of morse code for the word
    
    let swores (word: string) =
        let oldWord = word
        
        word.ToLower().ToCharArray()
        |> Array.map (fun c -> morseCodeMap.[c])
        |> Array.reduce (+)
        |> (fun morseCode -> (morseCode, oldWord))
        //maps the sting om morse code to a tuple of the morse code and the word
        
        
    //smorseWords makes an array of strings of morse code for an array of words and then groups them by the morse code
    let smorseWords (words:string[]) =
        words
        |> Array.map smorse // makes an array of strings of morse code
        |> Array.groupBy id // groups the array by the morse code string
    
        
    // makes an array of tuples of the morse code and the number of words with that morse code
    let groupeWords (arrayGrouped:(string * string[])[]) =
        arrayGrouped |>
        Array.map (fun (key, value) -> (key, value.Length)) // makes an array of tuples of the morse code and the number of words with that morse code
    let smorseWordsFromFile (filePath:string) =
        File.ReadAllLines(filePath)
        |> smorseWords
    
    let sworesFromFile (filePath:string) =
        File.ReadAllLines(filePath)
        |> Array.map swores
   
    let smorseWordsFromFileGrouped (filePath:string) =
        File.ReadAllLines(filePath)
        |> smorseWords
        |> groupeWords
        
    let smorseWordsFromFileGroupedSorted (filePath:string) =
        File.ReadAllLines(filePath)
        |> smorseWords
        |> groupeWords
        |> Array.sortBy (fun (_, count) -> count)
    
    let printSmorseWordsFromFileGroupedSorted (filePath:string) =
        smorseWordsFromFileGroupedSorted filePath
        |> Array.iter (fun (key, count) -> printfn "%s %d" key count)
    
    let returnMorseStringByCount (filePath:string) (count:int) =
        smorseWordsFromFileGroupedSorted filePath
        |> Array.filter (fun (_, c) -> c = count)
        |> Array.map (fun (key, _) -> key)
        |> Array.reduce (+)
    
    let wordsByMorseString (filePath:string) (morseString:string) =
        
        sworesFromFile filePath
        |> Array.filter (fun (key, _) -> key = morseString)
        |> Array.map (fun (_, value) -> value)
        |> Array.map (fun value -> value + " ") 
        |> Array.reduce (+)
        