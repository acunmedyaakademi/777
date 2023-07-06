$('#sumbitBtn').on('click', function (e) {    
    if (($("#text").val().length > 0)) {
        var textvalue = $("#text").val();

        $.post('/user/AddText/', { Text: textvalue }).done(function () {
            var legendElement = $("#sayı");
            legendElement.text(legendElement.text() + " Metin Kaydedildi");
        });
    } else {
        e.preventDefault();
        var legendElement = $("#sayı");
        legendElement.text(legendElement.text() + " olmadı");
    }
});


function countWords() {
    // Metin alanındaki içeriği al
    var text = document.getElementById("text").value;

    // Boşluk karakterlerine göre metni bölelim
    var words = text.trim().split(/\s+/);

    // Boşlukları saymamak için filtreleyelim
    words = words.filter(function (word) {
        return word.length > 0;
    });

    // Kelime sayısını güncelle
    //var wordCount =;
    //docuement.getElementById("wordCount").textContent = "Kelime Sayısı: " + wordCount;
    console.log(words.length);
    var legendElement = document.getElementById("sayı");

    legendElement.textContent = words.length;
}