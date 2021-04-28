var x = 0;
var s = "";
/*console.log --> tarayıcıda konsola yazar */
console.log("Hello Pluralsight");

var theForm = document.getElementById("theForm");
theForm.hidden = true;

var button = document.getElementById("byButton");
button.addEventListener("click", function () {
    console.log("Satın alma eşyası");
});

var productInfo = document.getElementsByClassName("product-props");
var listItems = productInfo.item[0].children;