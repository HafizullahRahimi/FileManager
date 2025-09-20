getUrlFromInput = (inputId) => {
    const input = document.getElementById(inputId);
    const file = input.files[0];
    if (!file) {
        console.warn("⚠️ No file selected");
        return null;
    }
    return URL.createObjectURL(file);
};