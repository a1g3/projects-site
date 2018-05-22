$(document).ready(function () {
    $('#text').on('input', function () {
        updateOutput($('#text').val()); 
    });
    $('#file').change(function () {
        calculateFileHash();
    });

    $('#algorithm').change(function () {
        if ($('input[name=input]:checked').val() === 'text') {
            updateOutput($('#text').val()); 
        }
        else if ($('input[name=input]:checked').val() === 'file') {
            calculateFileHash();
        }
    });
    $('input[type=radio][name=input]').change(function () {
        $('#output').val("");
        if (this.value === 'text') {
            $('#file').val("");
            $('#file').addClass('hidden');
            $('#text').removeClass('hidden');
        }
        else if (this.value === 'file') {
            $('#text').val("");
            $('#file').removeClass('hidden');
            $('#text').addClass('hidden');
        }
    });
});

function calculateFileHash() {
    if (document.getElementById("file").files[0]) {
        var reader = new FileReader();
        reader.onload = function () {

            var arrayBuffer = this.result,
                array = new Uint8Array(arrayBuffer),
                binaryString = String.fromCharCode.apply(null, array);

            updateOutput(binaryString);
        };
        reader.readAsArrayBuffer(document.getElementById("file").files[0]);
    }
}

function updateOutput(str) {
    $("#output").val("");
    var algo = $("#algorithm option:selected").val();
    hashSHA(algo, str).then(function (x) {
        $("#output").val(x);
    });
}

function hashSHA(algorithm, str) {
    var buffer = textEncode(str);
    return crypto.subtle.digest(algorithm, buffer).then(function (hash) {
        return hex(hash);
    });
}

function textDecode(arr) {
    if (window.TextDecoder) {
        return new TextDecoder("utf-8").decode(arr);
    }
}

function textEncode(str) {
    if (window.TextEncoder) {
        return new TextEncoder('utf-8').encode(str);
    }
    var utf8 = unescape(encodeURIComponent(str));
    var result = new Uint8Array(utf8.length);
    for (var i = 0; i < utf8.length; i++) {
        result[i] = utf8.charCodeAt(i);
    }
    return result;
}

function hex(buffer) {
    var hexCodes = [];
    var view = new DataView(buffer);
    for (var i = 0; i < view.byteLength; i += 4) {
        var value = view.getUint32(i);
        var stringValue = value.toString(16);
        var padding = '00000000';
        var paddedValue = (padding + stringValue).slice(-padding.length);
        hexCodes.push(paddedValue);
    }

    return hexCodes.join("");
}