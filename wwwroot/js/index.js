////belge bir jQuery nesnesi etrafında sarıldı
//// ready --> tarayıcı hazır olur olmaz bu kod bloğunda ne varsa çalıştırılacağını söyler.
//2. avantajı tüm bu şeyler kendi işlevinde gizlenmesi ve böylece içindeki tüm kodun birbirini tanımasıdır.
$(document).ready(function () {
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

    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");

    $loginToggle.on("click", function () {
        $popupForm.slideToggle(1000);
    });



});