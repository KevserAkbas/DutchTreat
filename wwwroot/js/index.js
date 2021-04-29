var x = 0;
var s = "";
/*console.log --> tarayıcıda konsola yazar */
console.log("Hello Pluralsight");

var theForm = $("#theForm");
theForm.hide();

var button = $("#byButton");
button.on("click", function () {
    console.log("Satın alma eşyası");
});

var productInfo = $(".product-props li");
productInfo.on("click", function () {
    console.log("You clicked on" + $(this).text());
});