    function toggleLogType(logType) {
        document.getElementById("btnLogin").classList.remove("active");
    document.getElementById("btnReg").classList.remove("active");

    document.getElementById("btn" + logType).classList.add("active");
    document.getElementById("LogType").value = logType;

    var loginFields = document.getElementById("loginFields");
    var regFields = document.getElementById("registrationFields");
    if (logType === "Login") {
        loginFields.style.display = "block";
    regFields.style.display = "none";
    document.getElementById("LoginEmail").required = true;
    document.getElementById("LoginPassword").required = true;
    document.getElementById("Email").required = false;
    document.getElementById("RegPassword").required = false;
    document.getElementById("Username").required = false;
        } else {
        loginFields.style.display = "none";
    regFields.style.display = "block";
    document.getElementById("LoginEmail").required = false;
    document.getElementById("LoginPassword").required = false;
    document.getElementById("Email").required = true;
    document.getElementById("RegPassword").required = true;
    document.getElementById("Username").required = true;
        }
    }

    function closePopup() {
        var popup = document.getElementById("error-popup");
    if (popup) {
        popup.style.display = "none";
        }
    }

    window.onload = function () {
        toggleLogType('Login');
    }