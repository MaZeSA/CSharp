// Завдання 1
// Написати функцію, яка приймає словосполучення і перетворює його на абревіатуру.
// Наприклад: cascading style sheets в CSS, об'єктно-орієнтоване програмування в ООП.

let text = prompt("Введіть текст для абривіатури");
let result = "";

text.split(" ").forEach((element) => {
 result += element[0].toUpperCase();
});

alert(result);

// Завдання 2
// Створити масив, який описує чек у магазині. Кожен елемент масиву складається з назви товару, кількості та ціни за одиницю товару. Написати такі функції.
// 1. Друк чека на екран.
// 2. Підрахунок загальної суми покупки.
// 3. Отримання найдорожчої покупки у чеку.
// 4. Підрахунок середньої вартості одного товару у чеку.

let products = ["Картопля", "Морква", "Цибуля", "Часник", "Петрушка", "Кріп", "Яблука", "Лимон", "Олія", "Кефір", "Молоко", "Сметана", "Сир", "Сир"];

let check = [];
for (let i = 0; i < products.length; i++) {

    let obj = new Object();
    obj.product = products[i];
    obj.price = Math.floor(Math.random() * 200);
    check[i] = obj;
};
console.log(check);

function printСheck(check) {
    let parent = document.querySelector('#check-print');
    check.forEach((check) => {
        let p = document.createElement('p');
        p.innerHTML = check.product + " Ціна: " + check.price;
        parent.appendChild(p);
    });
};
printСheck(check);

function sumCheck(check) {
    let sum = 0;
    check.forEach((check) => {
        sum = sum + check.price;
    });
    return sum;
};
alert("Загальна сума чеку: " + sumCheck(check));

function getMaxPrice(check) {
    let product = check[0];

    check.forEach((check) => {
        if (product.price < check.price)
            product = check;
    });
    return product;
}

let prod = getMaxPrice(check);
alert("Найдорожча покупка: " + prod.product + " Ціна: " + prod.price);

function avePrice(check) {
    let sum = sumCheck(check);
    return sum / check.length;
}
alert("Cередня ціна покупки: " + avePrice(check));

console.log(check);

// Завдання 3
// Створити об’єкт, який описує простий маркер. Повинні бути такі компоненти:
// • поле, яке зберігає колір маркера;
// • поле, яке зберігає розмір (товщину) маркера
// • метод для друку (метод приймає рядок та виводить текст відповідним кольором).
// • функція, приймає маркер та текст, який потрібно «написати»
// Для демонстрації показати роботу методу та функції на прикладі одного маркера та масиву маркерів.

class Marker {
    constructor(color, size) {
        this.color = color;
        this.size = size;
    };

    printText(text) {
        let parent = document.querySelector('#test-markers');
        let p = document.createElement('p');
        p.innerHTML = text;
        parent.appendChild(p);
        p.style.color = this.color;
        p.style.fontSize = this.size;

    };
}
let m = new Marker("red", "15px");
m.printText("test text");

let markers = [new Marker("blue", "8px"), new Marker("green", "10px"), new Marker("gold", "14px"), new Marker("yellow", "18px"), new Marker("aqua", "24px"), new Marker("darkgray", "28px")];

function Print(marker, text) {
    let parent = document.querySelector('#test-markers');
    let p = document.createElement('p');
    p.innerHTML = text;
    parent.appendChild(p);
    p.style.color = marker.color;
    p.style.fontSize = marker.size;
}

markers.forEach((marker) => {
    Print(marker, "test text")
        });