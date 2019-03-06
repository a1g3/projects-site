var hub = new Hub();

hub.onCertificateCountUpdate = (data) => {
    console.log(data);
    document.getElementById("certificateCount").textContent = data;
};

hub.start();