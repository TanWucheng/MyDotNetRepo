/**
 * 创建验证码
 * @param {number} codeLength 随机字符串长度
 */
function createCode(codeLength) {
    var code = "";
    const checkCode = document.getElementById("spanIdentifyCode");
    const codeChars = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"];
    for (let i = 0; i < codeLength; i++) {
        const charNum = Math.floor(Math.random() * 52);
        code += codeChars[charNum];
    }
    if (checkCode) {
        checkCode.innerText = code;
    }
}

/**
 * 生成二维码
 */
function createQRCode() {
    // 设置参数方式
    const qrCode = new QRCode("qrcode", {
        text: "your content",
        width: 256,
        height: 256,
        colorDark: "#000000",
        colorLight: "#ffffff",
        correctLevel: QRCode.CorrectLevel.H
    });

    // 使用 API
    qrCode.clear();
    qrCode.makeCode("https://nuget.org/");
}

/**
 * 切换到二维码登录点击
 */
function qrCodeIndicateImgClick() {
    const img = document.getElementById("qrcode_indicate_img");
    img.addEventListener("click", function () {
        const loginForm = document.getElementById("loginform");
        const qrCodeContainer = document.getElementById("qrcode_container");
        loginForm.style.display = "none";
        qrCodeContainer.style.display = "inline-block";
    }, false);
}

/**
 * 切换到用户信息输入登录
 */
function inputIndicateImgClick() {
    const img = document.getElementById("input_indicate_img");
    img.addEventListener("click", function () {
        const loginForm = document.getElementById("loginform");
        const qrCodeContainer = document.getElementById("qrcode_container");
        loginForm.style.display = "inline-block";
        qrCodeContainer.style.display = "none";
    }, false);
}

window.onload = function () {
    createCode(6);
    document.getElementById("spanIdentifyCode").addEventListener("click", function () {
        createCode(6);
    }, false);

    document.getElementById("buttonLogin").addEventListener("click", function () {
        const jsEncrypt = new JSEncrypt();
        jsEncrypt.setPublicKey(document.getElementById("public_key").value);
        var password = jsEncrypt.encrypt(document.getElementById("inputPassword").value);
        var id = jsEncrypt.encrypt(document.getElementById("inputId").value);
        id = encodeURIComponent(id);
        password = encodeURIComponent(password);
        const formData = "grant_type=password&userIdentity=" + id + "&password=" + password + "&platform=browser";
        ajaxPost("/token", true, formData, "application/x-www-form-urlencoded", null, requestSuccess, requestFail, function () {
        });
    }, false);
};

function requestSuccess(responseText) {
    const json = JSON.parse(responseText);
    const token = json.access_token;
    addCookie("PiaochongAccessToke", token, 168, "/");
    const model = M.Modal.getInstance(document.getElementById("modalSignResponseShow"));
    document.getElementById("panelLoginResult").innerText = "登陆成功";
    model.open();
}

function requestFail(statusCode, responseText) {
    const model = M.Modal.getInstance(document.getElementById("modalSignResponseShow"));
    document.getElementById("panelLoginResult").innerText = "登录失败，状态码:" + statusCode;
    model.open();
}