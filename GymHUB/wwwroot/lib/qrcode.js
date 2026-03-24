(function () {
    function QRCode(el, options) {
        const text = options.text;
        const size = options.width || 200;

        const img = document.createElement("img");
        img.src = "https://api.qrserver.com/v1/create-qr-code/?size=" + size + "x" + size + "&data=" + encodeURIComponent(text);

        el.innerHTML = "";
        el.appendChild(img);
    }

    window.QRCode = QRCode;
})();