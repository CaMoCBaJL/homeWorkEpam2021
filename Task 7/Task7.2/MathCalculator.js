const globalOperators = ["+", "-", "/", "*", "="];

function main(expression) {

    let exprCopy = expression;

    expression = removeSpaces(expression).split("=")[0];

    if (!expression.match(/\D{2,}|\.\d{1,}\./g)) {

        let operators = expression.match(/[\/+*-]/g).reverse();

        let operands = expression.match(/\d+\.\d+|\d+/g);

        let calculationResult = calculateData(operators, operands, expression[0]);

        return exprCopy + " " + calculationResult.toPrecision(calculationResult.toString()
            .split(/\./)[0].length + 2);
    }
    else {
        console.log("Ошибка при вычислении!");
    }
}

function containsOperator(expression) {

    for (var char of expression) {
        if (globalOperators.includes(char))
            return true;
    }

    return false;
}

function removeSpaces(expression) {
    while (expression.indexOf(" ") > -1) {
        expression = expression.replace(" ", "");
    }

    return expression;
}

function calculateData(operators, operands, firstChar) {
    let calculationResult;

    if (firstChar == "-")
        calculationResult = -parseFloat(operands[0]);
    else
        calculationResult = parseFloat(operands[0]);

    operands.shift();

    for (var operand of operands) {
        switch (operators.pop()) {
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

console.log(main("3.5+4*10-5.3/5 ="));