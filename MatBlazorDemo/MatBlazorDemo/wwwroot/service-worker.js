const CACHE_NAME = "AspNetMatBlazorCache";

const IMMUTABLE_URLS = [
    "_content/MatBlazor/dist/matBlazor.js",
    "_content/MatBlazor/dist/matBlazor.css",
    "css/open-iconic/font/css/open-iconic-bootstrap.min.css",
    "css/QPlayer.min.css",
    "js/QPlayer.min.js",
    "img/icon_192.png",
    "img/icon_512.png",
    "https://cdn.bootcdn.net/ajax/libs/jQuery.Marquee/1.5.0/jquery.marquee.min.js",
    "https://cdn.bootcdn.net/ajax/libs/jquery/3.5.1/jquery.min.js",
    "https://cdn.bootcdn.net/ajax/libs/popper.js/2.5.4/umd/popper.min.js",
    "https://cdn.bootcdn.net/ajax/libs/twitter-bootstrap/4.5.3/css/bootstrap.min.css",
    "https://cdn.bootcdn.net/ajax/libs/twitter-bootstrap/4.5.3/js/bootstrap.min.js",
    "https://cdn.jsdelivr.net/npm/font-awesome/css/font-awesome.min.css",
    "https://eqcn.ajz.miesnfu.com/wp-content/plugins/wp-3d-pony/live2dw/lib/L2Dwidget.min.js",
    "https://unpkg.com/live2d-widget-model-shizuku@1.0.5/assets/shizuku.model.json"
];

self.addEventListener("install", (e) => {
    // 安装之后跳过等待
    self.skipWaiting();
    e.waitUntil(
        caches.open(CACHE_NAME).then((cache) => {
            cache.addAll(IMMUTABLE_URLS);
        })
    );
});

self.addEventListener("activate", (e) => {
    // 立即受控
    e.waitUntil(clients.claim());
});

// 网络层拦截请求
self.addEventListener("fetch", (event) => {
    if (event.request.mode === "navigate") {
        return event.respondWith(
            // fetch(event.request)
            //     .then((response) => {
            //         if (response.status == 404) {
            //             return fetch("404.html");
            //         }
            //         caches.open(CACHE_NAME).then((cache) => {
            //             cache.put(event.request, response.clone());
            //             console.log("request:", event.request);
            //         });
            //         return response;
            //     })
            //     .catch(async () => {
            //         // 离线状态下的处理
            //         const cache = await caches.open(CACHE_NAME);
            //         // 从Cache里取资源
            //         const response = await cache.match(event.request);
            //         if (response) {
            //             return response;
            //         }
            //         return cache.match("404.html");
            //     })

            caches.match(event.request).then((cacheData) => {
                const networkRes = fetch(event.request)
                    .then((response) => {
                        if (response.status == 404) {
                            return fetch("404.html");
                        }
                        caches
                            .open(CACHE_NAME)
                            .then((cache) => {
                                cache.put(event.request, response.clone());
                                return response;
                            })
                            .catch((err) => {
                                return fetch("404.html");
                            });
                    })
                    .catch(async () => {
                        // 离线状态下的处理
                        const cache = await caches.open(CACHE_NAME);
                        // 从Cache里取资源
                        const response = await cache.match(event.request);
                        if (response) {
                            return response;
                        }
                        return cache.match("404.html");
                    });

                return cacheData || networkRes;
            })
        );
    }
});

self.addEventListener("error", (err) => {
    fetch("/log-error", {
        body: JSON.stringify(err),
        method: "POST"
    });
});

self.addEventListener("unhandledrejection", (err) => {
    fetch("/log-error", {
        body: JSON.stringify(err),
        method: "POST"
    });
});

self.addEventListener("message", (e) => {
    console.log(
        `${new Date().toLocaleString()}, Service Worker收到的消息:${e.data}`
    );

    // 从e.ports里获取MessagePort
    e.ports[0] &&
        e.ports[0].postMessage(
            "这是Service Worker通过port1.postMessage()发送的消息"
        );

    e.source.postMessage("Service Worker发送的消息(e.source.postMessage)");

    self.clients.get(e.source.id).then((client) => {
        client.postMessage("Service Worker发送的消息(client.postMessage)");
    });

    self.clients.matchAll().then((clients) => {
        clients.map((client) => {
            client.postMessage("Service Worker向所有window postMessage()");
        });
    });
});
