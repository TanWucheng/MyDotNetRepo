// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function () {
    const navbar = document.getElementById("nav-mobile");
    const navItems = navbar.getElementsByTagName("li");
    if (window.location.href === window.location.protocol + "//" + window.location.host + "/") {
        navItems[0].className = "nav-item active";
    } else {
        navItems[0].className = "nav-item";
        for (let i = 1; i < navItems.length; i++) {
            if (window.location.href.indexOf(navItems[i].getElementsByTagName("a")[0].getAttribute("href")) >= 0) {
                navItems[i].className = "nav-item active";
            } else {
                navItems[i].className = "nav-item";
            }
        }
    }
})();

/**
 * 获取指定名称的cookie值
 * @param  {string} cookieKey cookie名称
 * @return {string} cookie值
 */
function getCookie(cookieKey) {
    const arrStr = document.cookie.split("; ");
    for (let i = 0; i < arrStr.length; i++) {
        const temp = arrStr[i].split("=");
        if (temp[0] === cookieKey) return unescape(temp[1]);
    }
    return null;
}

/**
 * 添加cookie
 * @param {String} cookieKey cookie名称
 * @param {String} cookieValue cookie值
 * @param {Number} expiresHours cookie有效期(小时)
 * @param {string} path 访问域名
 */
function addCookie(cookieKey, cookieValue, expiresHours, path) {
    var str = cookieKey + "=" + escape(cookieValue);

    if (expiresHours > 0) { // 如果不设定过期时间, 浏览器关闭时cookie会自动消失
        const date = new Date();
        const ms = expiresHours * 3600 * 1000;
        date.setTime(date.getTime() + ms);
        str += `; expires=${date.toGMTString()}; path=${path}; samesite=lax`;
    }

    document.cookie = str;
}

/**
 *  格式化时间为'YYYY年MM月DD日'格式
 * @param {string} dateStr 时间字符串
 * @returns {string} 返回'YYYY年MM月DD日'格式的时间字符串
 */
function formatDate(dateStr) {
    var year;
    var month;
    var day;
    try {
        dateStr = dateStr.replace(/-/g, "/");
        const date = new Date(dateStr);
        year = date.getFullYear() + "年";
        month = (date.getMonth() + 1 < 10 ? `0${date.getMonth() + 1}` : date.getMonth() + 1) + "月";
        day = date.getDate() + "日";
        return year + month + day;
    } catch (e) {
        console.error(e.message);
        return dateStr;
    }
}

document.addEventListener("DOMContentLoaded", function () {
    const sideNavElems = document.querySelectorAll(".sidenav");
    M.Sidenav.init(sideNavElems, {});

    const modalElems = document.querySelectorAll(".modal");
    M.Modal.init(modalElems, {});

    const selectElems = document.querySelectorAll('select');
    M.FormSelect.init(selectElems, {});

    const loginModel = document.getElementById("modalSignResponseShow");
    M.Modal.init(loginModel, {
        onCloseEnd: function () {
            const token = getCookie("PiaochongAccessToke");
            if (token) {
                window.location.href = "/home/index";
            }
        }
    });

    $(".dropdown-trigger").dropdown({
        coverTrigger: false
    });
});