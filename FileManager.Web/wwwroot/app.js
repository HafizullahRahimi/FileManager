window.downloadFileFromStream = async (fileBytes, fileName) => {
    const blob = new Blob([new Uint8Array(fileBytes)]);
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    window.URL.revokeObjectURL(url);
    document.body.removeChild(a);
};

getUrlFromInput = (inputId) => {
    const input = document.getElementById(inputId);
    const file = input.files[0];
    if (!file) {
        console.warn("⚠️ No file selected");
        return null;
    }
    return URL.createObjectURL(file);
};