window.addEventListener("load", function () {
    const qrCodeData = document.getElementById("qrCodeData");
    const qrCodeElement = document.getElementById("qrCode");

    if (!qrCodeData || !qrCodeElement) return;

    const qrCodeUri = qrCodeData.getAttribute("data-url");
    if (!qrCodeUri) return;

    new QRCode(qrCodeElement, {
        text: qrCodeUri,
        width: 200,
        height: 200
    });
});