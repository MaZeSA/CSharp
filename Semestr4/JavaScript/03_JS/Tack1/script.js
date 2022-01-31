let fulImage = document.querySelector("#image1"); 
let listImage = document.querySelector(".pictures");
let selectIndex = 0;
let imgCount = 12;

setImage(0);

function imgSelect(img){
    setImage(img.alt-1);
}
function imgChange(side){
    let index = selectIndex + side
    if(index == imgCount) index = 0;
    else if (index < 0) index = imgCount - 1;
    setImage(index);
}

function setImage(index){
    let liList = listImage.children;
    removeSelector(liList[selectIndex]);

    let li = liList[index];
    fulImage.style.backgroundImage  = "url('"+li.children[0].src + "')";

    selectIndex = index;
    setSelector(liList[selectIndex]);
}

function setSelector(li){
    li.classList.add("li-select");
}
function removeSelector(li){
    li.classList.remove("li-select");
}