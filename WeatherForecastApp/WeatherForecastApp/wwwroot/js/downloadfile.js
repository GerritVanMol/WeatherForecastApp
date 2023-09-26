window.downloadFile = (fileUrl) => {
    const a = document.createElement('a');
    a.href = fileUrl;
    a.download = fileUrl.split('/').slice(-1)[0]; // Extract the filename from the URL
    a.target = '_blank';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
};
