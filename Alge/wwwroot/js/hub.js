class Hub {
    constructor() {
        this.onCertificateCountUpdate = null;
        this._hub = null;
    }

    start() {
        let url = "/certstream";
        this._hub = new signalR.HubConnectionBuilder()
            .withUrl(url)
            .build();

        this._hub.onclose(() => {
            // Hub connection was closed for a unknown reason
            let interval = setInterval(() => {
                // Try to reconnect hub every 5 seconds
                this.start().then(() => {
                    // Successfully reconnected 
                    clearInterval(interval);
                });
            }, 5000);
        });

        this._hub.on('OnCertificateCountUpdate', certificateCount => {
            if (this.onCertificateCountUpdate) this.onCertificateCountUpdate(certificateCount);
        });

        return this._hub.start();

    }
}