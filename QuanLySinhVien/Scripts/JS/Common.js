function validateEmail(email) {
    var emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailRegex.test(email);
}
function validateKhoaHoc(khoaHoc) {
    var khoaHocRegex = /^K[1-9][0-9]*$/;
    return khoaHocRegex.test(khoaHoc);
}
function validatePassword(password) {
    var passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}$/;
    return passwordRegex.test(password);
}
function ReplaceDateTime() {
    var dateInput = $('input[type="date"]');
    if (dateInput.length > 0) {
        var date = new Date(document.getElementById("NgaySinh").defaultValue);
        var formattedDate = date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2);
        dateInput.val(formattedDate);
    }
}
function generateRandomPassword() {
    var passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}$/;
    var password = "";

    while (!passwordRegex.test(password)) {
        password = "";

        var possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        for (var i = 0; i < 16; i++) {
            password += possibleChars.charAt(Math.floor(Math.random() * possibleChars.length));
        }
    }

    return password;
}



$(document).ready(function () {
    ReplaceDateTime();
    var password = document.getElementById("Password");
    var khoaHoc = document.getElementById("KhoaHoc");
    var resetButton = document.getElementById("ResetButton");
    var searchInput = document.getElementById("searchInput");
    //Xử lý submit
    $("#Submit").click(function () {
        var email = $("#Email").val();
        if (!validateEmail(email)) {
            alert("Email không hợp lệ. Vui lòng nhập lại!");
            return false;
        }
        if (!validateKhoaHoc(khoaHoc.value)) {
            alert("Khóa học không hợp lệ. Vui lòng nhập lại!");
            return false;
        }
        if (!validatePassword(password.value)) {
            alert("Mật khẩu không hợp lệ. Vui lòng nhập lại!");
            return false;
        }
    });

    //Xử lý reset password
    resetButton.addEventListener("click", function () {
        password.value = generateRandomPassword();
    });

});
function ShowAlert(message, alertType) {
    var alertBox = document.getElementById("notifyId");
    alertBox.innerHTML = message;
    alertBox.classList.add("alert-" + alertType);
    alertBox.style.display = "block";

    setTimeout(function () {
        alertBox.style.display = "none";
    }, 3000);
}