console.log("hello");

let number;
let number2;
//1 Запросите у пользователя число, возведите это число во 2-ю степень и выведите на экран.
number = prompt("Enter number:");
alert( "Result = " + number**2);

//2 Запросите у пользователя 2 числа и выведите среднее арифметическое этих чисел
number = prompt("Enter first number:");
number2 = prompt("Enter second number:");
alert("Result = " + ((parseInt(number) + parseInt(number2))/2));

//3 Запросите у пользователя длину стороны квадрата и выведите площадь такого квадрата.
number = prompt("Enter size side of the square:");
alert( "Result = " + number**2);

//4. Реализуйте конвертор из километров в мили (пользователь вводит километры, программа выводит мили). 1 км = 0,621371 миль. Это значение укажите в коде как константу.
const mi = 0.621371;
number = prompt("Enter km:");
alert( "Result = " + number * mi + " mi");

//5. Реализуйте калькулятор. Пользователь вводит два числа, а программа выводит результаты действий + - * / между этими числами.
number = parseInt(prompt("Enter first number:"));
number2 = parseInt(prompt("Enter second number:"));
alert( "Result\n" +
number + "+" + number2 + "= " + (number + number2) + "\n"+
number + "-" + number2 + "= " + (number - number2) + "\n"+
number + "/" + number2 + "= " + number / number2 + "\n"+
number + "*" + number2 + "= " + number * number2
);

//6. Пользователь вводит значения a и b для формулы a * x + b = 0, а программа считает и выводит значение x.
number = prompt("Enter a:");
number2 = prompt("Enter b:");
let x = -number2 / number;

if(number == 0)
{ 
   alert("No solution");
}
else
{
   alert(number + " * x +"+ number2 + "= 0\n" + "x= " + x);
}

//7. Запросите у пользователя текущее время (часы и минуты) и выведите, сколько часов и минут осталось до следующего дня.
number = parseInt(prompt("Enter hours:"));
number2 = parseInt(prompt("Enter minutes:"));

alert("Next day in: " + (23 - number) + "hours " + (60 - number2) + "minutes");

//8. Запросите у пользователя трехзначное число и выведите вторую цифру этого числа. Для решения задачи используйте оператор % (остаток от деления).

while (true) {
    number = parseInt(prompt("Enter three-digit number:"));
    if (number > 99)
        if (number < 1000)
            break;

    alert("Wrong digit!");
}

number2 = number % 100;
alert(Math.trunc(number2 / 10));

//9. Запросите у пользователя пятизначное число и переместите последнюю цифру в начало (из числа 12345 должно получиться число 51234).
while (true) {
   number = parseInt(prompt("Enter five-digit number:"));
   if (number > 9999)
       if (number < 100000)
            break;

   alert("Wrong digit!")
}

number2 = Math.trunc(number % 10) * 10000;
number2 = number2 + Math.trunc(number / 10);
alert(number2);

//10. Зарплата работника составляет $250 + 10% от продаж. Запросите общую сумму продаж за месяц и посчитайте зарплату

number = parseInt(prompt("Enter sum sales:"));
number2 = number / 100 *10;
alert("Salary: " + (number2 + 250) + "$");