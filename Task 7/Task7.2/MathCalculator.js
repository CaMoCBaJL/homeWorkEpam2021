const globalOperands = ["+", "-", "/", "*", "="];

function main(expression){

    let exprCopy = expression;

    if (checkForErrors(expression)){

        expression = removeSpaces(expression).split("=")[0];

        let operators = removeEmptyStrings(expression.split(/[\d\.]/)).reverse();

        let operands = removeEmptyStrings(expression.split(/[+|*|/|-]/));

        let calculationResult = calculateData(operators, operands, expression[0]);

        return exprCopy + " " +calculationResult.toPrecision(calculationResult.toString()
        .split(/\./)[0].length + 2);
    }
    else{
        console.log("Ошибка при вычислении!");
}
}

function checkForErrors(expression){
    let prevElem = "";

    for (var elem of expression){
        if (globalOperands.includes(elem) && globalOperands.includes(prevElem) ||
        globalOperands.includes(elem) && prevElem == "")
            return false;
        
        prevElem = elem;
    }

    let arr = expression.split(/\./);

    for (var elem of expression.split(/\./)){
        if (!containsOperator(elem)) {
            if (expression.indexOf(elem) > 0)
                return false;
        }
    }

    return true;

}

function containsOperator(expression){

    for (var char of expression){
        if (globalOperands.includes(char))
            return true;
    }

    return false;
}

function removeSpaces(expression){
    while (expression.indexOf(" ") > -1){
        expression = expression.replace(" ", "");
    }

    return expression;
}

function calculateData(operators, operands, firstChar){
    let calculationResult;
    
    if (firstChar == "-")
        calculationResult = -parseFloat(operands[0]);
    else
        calculationResult = parseFloat(operands[0]);
     
    operands.splice(0, 1);

    for (var operand of operands){
        switch(operators.pop()){
            case "+":
                calculationResult = calculationResult + parseFloat(operand);
            break;
            case "-":
                calculationResult = calculationResult - parseFloat(operand);
            break;
            case "/":
                calculationResult = calculationResult / parseFloat(operand);
            break;
            case "*":
                calculationResult = calculationResult * parseFloat(operand);
            break;
        }
    }

    return calculationResult;
}

function removeEmptyStrings(arr){
    let indx = 0;

    do{
        indx = arr.indexOf("");

        if (arr[indx] == ""){
            arr.splice(indx, 1);
        }
    }while (indx > -1);

    return arr;
}

console.log(main("3.5+4*10-5.3/5 ="));