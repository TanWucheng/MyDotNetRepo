/**
 * 创建ajax对象
 * @return {XMLHttpRequest} XMLHttpRequest
 */
function ajaxObject() {
    var xmlHttp;
    try {
        // Firefox, Opera 8.0+, Safari
        xmlHttp = new XMLHttpRequest();
    }
    catch (e) {
        // Internet Explorer
        try {
            xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
        } catch (e) {
            try {
                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (e) {
                alert("您的浏览器不支持AJAX！");
                return false;
            }
        }
    }
    return xmlHttp;
}

/**
 * ajax post请求：
 * @param {String} url 请求
 * @param {boolean} isAsync 是否允许异步请求
 * @param {Object} data 表单数据
 * @param {String} contentType Content-Type
 * @param {null} authorization token
 * @param {Function} fnSucceed 请求成功回调
 * @param {Function} fnFail 请求失败回调
 * @param {Function} fnLoading 请求中的操作
 */
function ajaxPost(url, isAsync, data, contentType, authorization, fnSucceed, fnFail, fnLoading) {
    var ajax = ajaxObject();
    var asyncAccess = true;
    if (isAsync && typeof (isAsync) === typeof (true)) {
        asyncAccess = isAsync;
    }
    ajax.open("post", url, asyncAccess);
    if (contentType !== undefined && contentType !== null) {
        ajax.setRequestHeader("Content-Type", contentType);
    }
    if (authorization !== undefined && authorization !== null) {
        ajax.setRequestHeader("Authorization", authorization);
    }
    ajax.onreadystatechange = function () {
        if (ajax.readyState === 4) {
            if (ajax.status === 200) {
                fnSucceed(ajax.responseText);
            } else {
                fnFail(ajax.status, ajax.responseText);
            }
        } else {
            if (fnLoading) {
                fnLoading();
            }
        }
    };
    if (data) {
        ajax.send(data);
    }
}

/**
 * ajax get请求：
 * @param {String} url 请求
 * @param {boolean} isAsync 是否允许异步请求
 * @param {Object} data 表单数据
 * @param {String} contentType Content-Type
 * @param {string} authorization token
 * @param {Function} fnSucceed 请求成功回调
 * @param {Function} fnFail 请求失败回调
 * @param {Function} fnLoading 请求中的操作
 */
function ajaxGet(url, isAsync, data, contentType, authorization, fnSucceed, fnFail, fnLoading) {
    var ajax = ajaxObject();
    var asyncAccess = true;
    if (isAsync && typeof (isAsync) === typeof (true)) {
        asyncAccess = isAsync;
    }
    ajax.open("get", url, asyncAccess);
    if (contentType !== undefined && contentType !== null) {
        ajax.setRequestHeader("Content-Type", contentType);
    }
    if (authorization) {
        ajax.setRequestHeader("Authorization", authorization);
    }
    ajax.onreadystatechange = function () {
        if (ajax.readyState === 4) {
            if (ajax.status === 200) {
                if (fnSucceed) {
                    fnSucceed(ajax.responseText);
                }
            } else {
                if (fnFail) {
                    fnFail(ajax.status, ajax.responseText);
                }
            }
        } else {
            if (fnLoading) {
                fnLoading();
            }
        }
    };
    ajax.send(data);
}