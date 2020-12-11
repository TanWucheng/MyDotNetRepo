(function () {
    /**
     * 注册Service Worker
     * @param {*} config 配置项目
     */
    const register = (config) => {
        //浏览器是否支持
        if ("serviceWorker" in navigator) {
            const swUrl = `${config.path}/${config.name}?v=${config.ver}`;
            // 通过ver的变化来强制进行更新操作，每次更新sw.js时进行ver+1操作
            navigator.serviceWorker
                .register(swUrl)
                .then((swReg) => {
                    console.log("Service Worker注册成功");
                    if (config && config.onRegister) {
                        config.onRegister(swReg);
                    }

                    swReg.onupdatefound = () => {
                        const installingWorker = swReg.installing;
                        if (!installingWorker) {
                            return;
                        }
                        installingWorker.onstatechange = () => {
                            if (installingWorker.state === "installed") {
                                if (navigator.serviceWorker.controller) {
                                    config.log &&
                                        console.log("Service Worker已安装更新");
                                    if (config && config.onUpdate) {
                                        config.onUpdate(swReg);
                                    }
                                } else {
                                    config.log &&
                                        console.log("Service Worker已安装");
                                    if (config && config.onSuccess) {
                                        config.onSuccess(swReg);
                                    }
                                }
                            }
                        };
                    };
                })
                .catch((err) => {
                    if (config && config.onError) {
                        config.onError(err);
                    }
                    console.error("Service Worker注册期间发生错误:", err);
                });
        }
    }

    //注册sw.js
    register({
        ver: 1,
        path: "",
        name: "service-worker.js",
        log: true,
        onRegister: () => { },
        onSuccess: () => { },
        onUpdate: () => { },
        onError: (err) => { console.error(err); }
    });
})()