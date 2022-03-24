window.addEventListener("load", Init);
const URL_PB = "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5";
const select_in = document.querySelector('#select-in');
const select_out = document.querySelector('#select-out');
let currancyList = [];

select_in.addEventListener('change', (event) => {
    moneyConverter();
});
select_out.addEventListener('change', (event) => {
    moneyConverter();
});
document.querySelector('#input-in').addEventListener('input', (event)=>{
    moneyConverter();
});

function Init() {
    Request(URL_PB, Privat);
}

const Request = (URL, Callback) => {
    fetch(URL)
        .then(responce => {
            return responce.json();
        }).then(data => {
            console.log(data);
            Callback(data);
        })
        .catch(err => console.log(err));
}

const Privat = (currancy => {
    currancyList = currancy;
    CreateList(select_in, currancy);    
    CreateList(select_out, currancy);
});

function CreateList (parent, currancy) {
    console.log(parent);
    let uah1 = document.createElement("option");
    uah1.innerText = 'UAH';
    parent.appendChild(uah1);

    currancy.forEach(element => {
        if (element.ccy != "BTC") {
            let t = document.createElement("option");
            t.innerText = element.ccy;
            parent.appendChild(t);
        }
    });
};

const moneyConverter = () => { 
    let in_sum = document.querySelector('#input-in');
    let out_sum = document.querySelector('#input-out');
  
    let sel = select_in.value;
    let bye = select_out.value;
    if (sel == bye){
        out_sum.value =0; 
        return;
    }

    let selPrice = 1;
    let byePrice = 1;
    currancyList.forEach(element => {
        if (element.ccy == sel) selPrice = element.buy;
        if (element.ccy == bye) byePrice = element.sale;
    });
    //підтримка подвійної конвертації
   out_sum.value = in_sum.value * selPrice / byePrice ;
}



