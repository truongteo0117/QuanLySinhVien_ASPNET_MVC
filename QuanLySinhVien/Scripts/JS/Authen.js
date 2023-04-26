$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);
    const name = urlParams.get('ReturnUrl');
    if (name.match("QuanLySinhVien"))
    {
        window.location.href = window.location.href.split('?')[0];
        alert('Vui lòng đăng nhập để vào trang quản lý');
    }
})