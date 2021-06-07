function main(str){
    let letters = searchForAllRepeatingLetters(str);

    return removeFoundLetters(str, letters);
}

function removeFoundLetters(str, letters){
    for (var letter of letters){
        while (str.indexOf(letter) > -1){
            str = str.replace(letter, "");
        }
    }

    return str;
}

function searchForAllRepeatingLetters(str){
    let letters = [];
    let words = str.split(" ");

    for (var word of words){
        for (var char of word){
            if (countChars(word, char) > 1){
                if (letters.indexOf(char) < 0)
                letters.push(char);
            }
        }
    }
    
    return letters;
}

function countChars(str, charToCount){
    let counter = 0;
    for (var char of str){
        if (char == charToCount)
            counter++;
    }

    return counter;
}

console.log(main("У попа была собака"));
