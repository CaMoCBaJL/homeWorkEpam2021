const punctuationMarks = [",", "!", ".", "?", ":", ";"]


function main(str) {
    let letters = searchForAllRepeatingLetters(str);

    return removeFoundLetters(str, letters);
}

function removeFoundLetters(str, letters) {

    let newStr = "";

    for (char of str) {
        if (!letters.includes(char))
            newStr = newStr.concat(char);
    }

    return newStr;
}

function searchForAllRepeatingLetters(str) {
    let letters = [];
    let words = str.split(" ");

    for (var word of words) {
        for (var char of word) {
            if (countChars(word, char) > 1) {
                if (~!letters.indexOf(char) &&
                    !punctuationMarks.includes(char))
                    letters.push(char);
            }
        }
    }

    return letters;
}

function countChars(str, charToCount) {
    let counter = 0;
    for (var char of str) {
        if (char == charToCount)
            counter++;
    }

    return counter;
}

console.log(main("У попа была собака!!!"));
