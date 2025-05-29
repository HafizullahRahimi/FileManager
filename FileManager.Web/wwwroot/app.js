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