document.addEventListener("DOMContentLoaded", function () {
    let arrIndex = 0;
    document.body.addEventListener(
        "click",
        function (e) {
            let r = Math.random() * 255;
            let g = Math.random() * 255;
            let b = Math.random() * 255;
            var color = "rgb(" + r + "," + g + "," + b + ")";
            var arr = [
                "HTML",
                "JavaScript",
                "CSS",
                "C#",
                ".NET",
                "ASP.NET",
                ".NET Core",
                "ASP.NET Core",
                "SqlServer",
                "MySQL",
                "MongoDB",
                "SQLite",
                "Oracle",
                "Electron",
                "Progressive Web App",
                "Python",
                "ASP.NET Core Blazor",
                "ASP.NET Web API",
                "ASP.NET MVC",
                "Vue",
                "React",
                "Xamarin",
                "UWP",
                "WPF",
                "gRPC Service",
            ];
            var promptSpan = document.createElement("span");
            promptSpan.innerText = arr[arrIndex];
            arrIndex = (arrIndex + 1) % arr.length;
            var x = e.pageX,
                y = e.pageY;
            var size = Math.random() * 10 + 8 + "px";
            promptSpan.style.zIndex = 99999;
            promptSpan.style.top = `${y - 20}px`;
            promptSpan.style.left = `${x}px`;
            promptSpan.style.position = "absolute";
            promptSpan.style.fontWeight = "800";
            promptSpan.style.fontSize = size;
            promptSpan.style.color = color;
            document.body.append(promptSpan);
            promptSpan.animate(
                [
                    // keyframes
                    { transform: "translateY(-200px)" },
                ],
                {
                    // timing options
                    duration: 1500,
                    iterations: 1,
                }
            ).onfinish = () => {
                promptSpan.remove();
            };
        },
        false
    );

    window.QPlayer.list = [
        {
            name: "且听风吟",
            artist: "V.A.",
            audio:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/且听风吟.mp3",
        },
        {
            name: "白色",
            artist: "灰澈",
            audio: "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/白色.mp3",
            cover: "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/白色.jpg",
        },
        {
            name: "渚 Warm Piano Arrange",
            artist: "Key Sounds Label",
            audio:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/渚 Warm Piano Arrange.mp3",
            cover:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/渚 Warm Piano Arrange.jpg",
        },
        {
            name: "小さなてのひら",
            artist: "水月陵",
            audio:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/小さなてのひら.mp3",
            cover:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/小さなてのひら.jpg",
        },
        {
            name: "Nightglow",
            artist: "蔡健雅",
            audio:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/Nightglow.mp3",
            cover:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/Nightglow.jpg",
            lrc:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/Nightglow.lrc",
        },
        {
            name: "やわらかな光",
            artist: "やまだ豊",
            audio:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/やわらかな光.mp3",
            cover:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/やわらかな光.jpg",
        },
        {
            name: "Saya's Song",
            artist: "Lia",
            audio:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/Saya's Song.mp3",
            cover:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/Saya's Song.jpg",
            lrc:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/Saya's Song.lrc",
        },
        {
            name: "一番の宝物 (Original Version)",
            artist: "karuta",
            audio:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/一番の宝物 (Original Version).mp3",
            cover:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/一番の宝物 (Original Version).jpg",
            lrc:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/一番の宝物 (Original Version).lrc",
        },
        {
            name: "僕が死のうと思ったのは",
            artist: "中島美嘉",
            audio:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/僕が死のうと思ったのは.mp3",
            cover:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/僕が死のうと思ったのは.jpg",
            lrc:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/僕が死のうと思ったのは.lrc",
        },
        {
            name: "跟太阳系说再见",
            artist: "祝乾亮",
            audio:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/跟太阳系说再见.mp3",
            cover:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/跟太阳系说再见.jpg",
        },
        {
            name: "I Love Study 我爱学习（Off Vocal）",
            artist: "秋风MusiX",
            audio:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/I Love Study 我爱学习（Off Vocal）.mp3",
            cover:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/I Love Study 我爱学习（Off Vocal）.jpg",
        },
        {
            name: "Blessing 世界版",
            artist: ["茶理理", "V.A."],
            audio:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/Blessing 世界版.mp3",
            cover:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/Blessing 世界版.jpg",
            lrc:
                "https://cdn.jsdelivr.net/gh/moeshin/QPlayer-res/Blessing 世界版.lrc",
        },
        {
            name: "烟花易冷",
            artist: "林志炫",
            audio: "StaticFiles/music/林志炫 - 烟花易冷.wav",
            cover: "",
        },
        // 尼尔:自动人形
        {
            name: "意味/無",
            artist: "岡部啓一 / J'Nique Nicole",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490563&auth=14e550102b1fc2a2da07b666b7f8db92393b5363",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490563&auth=4899bf3f644d88e64fad8589db9197b44e5a7f85",
        },
        {
            name: "遺サレタ場所/斜光",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490564&auth=024e17b6b1513b24aa1c291503a40af597a1c83c",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490564&auth=0ebdec74168347f02eff3f43c59a4c7651849368",
        },
        {
            name: "穏ヤカナ眠リ",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490565&auth=d7b8e1b3cba93e7563ad47f289b974f69cbb44ee",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490565&auth=d49aefc3ddda1a0ddebdddfbe810f5497863952c",
        },
        {
            name: "砂塵ノ記憶",
            artist: "高橋邦幸",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490566&auth=d12c507a00b76a4f2c297491265d4c52cfc4a544",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490566&auth=90088e41b06a46825cffb3300a4269c264a84b5e",
        },
        {
            name: "生マレ出ヅル意思",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490567&auth=cc4f959fa5dc4e3f81f26ac7fdeac06ec903acd2",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490567&auth=a950a3bbc3f543e5a265aaa5b6231d4800b19ea5",
        },
        {
            name: "沈痛ノ色",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490568&auth=a76687c62933f1a0c1efb1dffbb0112ce44bb217",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490568&auth=0c5b125a40581f5ba3d82834d6277f6b513cce79",
        },
        {
            name: "遊園施設",
            artist: "帆足圭吾",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490569&auth=f74272c6641c98b49991fcb436736709d8f5825c",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490569&auth=59640bbe3657c89f701b746435d129d4db85f9e2",
        },
        {
            name: "美シキ歌",
            artist: "帆足圭吾",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490570&auth=78ef31a7011d38c626e90dc5297a5edd91f28f56",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490570&auth=dbe72e64646c4ff430eb957971d6f5f91894e05c",
        },
        {
            name: "還ラナイ声/ギター",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490571&auth=cbf59cd9ed09fff65b2a888a0bb42ec8dfc6f663",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490571&auth=3cb1cb7c38794100b8cf27f677655977efbed5ed",
        },
        {
            name: "オバアチャン/破壊",
            artist: "岡部啓一 / 高橋邦幸",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490572&auth=d63a608695cba08faa475b71c3ef5d48995ced17",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490572&auth=dead047a1c2477764bc89797e54c36ec8b20b788",
        },
        {
            name: "澱ンダ祈リ/暁風",
            artist: "帆足圭吾 / 岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490573&auth=35a52329e6e8893de59c50d8d77ea67386def3b5",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490573&auth=95f20a2ea4d7f826af56a82496db425383fcbc2d",
        },
        {
            name: "エミール/ショップ",
            artist: "高橋邦幸 / 石濱翔 / 門脇舞以",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490574&auth=11475285a1f393e65fcf91fd019d2ffc9ef7d80f",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490574&auth=2f753bd5872e8422f99d66ef9ff244c3f3255472",
        },
        {
            name: "大切ナ時間",
            artist: "帆足圭吾 / 石井咲希",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490575&auth=1f36c8bb44946c0a971d9f077a929b35df46c541",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490575&auth=0bddcaf3cb9a86003614c9f11c20746af6762255",
        },
        {
            name: "曖昧ナ希望/氷雨",
            artist: "帆足圭吾",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490576&auth=997651562d5fc9080134dc7db73e7869db9b08a7",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490576&auth=3c6cd6d1188c0bcb8aac88528e0a7a28953223d1",
        },
        {
            name: "Weight of the World (English Version)",
            artist: "J'Nique Nicole",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490577&auth=260fdae6ff1e73844c7de2acf158d9997f5b41ad",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490577&auth=5abe8959cda8833553cc65cada90d59a5a1613ac",
        },
        {
            name: "意味",
            artist: "Emi Evans / 河野マリナ / 岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490578&auth=b875dbbf94c7467e7379768324b7aae09493ed52",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490578&auth=fd94409249aa747c3a5b391752ecde529cd3ff59",
        },
        {
            name: "遺サレタ場所/遮光",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490579&auth=954f08331c20ce240943c615d49dcb0eee8acff3",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490579&auth=cfd35a58bd24fc9f30f055bd9d33c2598b7af6e4",
        },
        {
            name: "異形ノ末路",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490580&auth=218e8e0ea20f457945ebf504e180b0924ba95459",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490580&auth=ccb154ee6c7cf4f35486b9de0137adcb5945dd29",
        },
        {
            name: "還ラナイ声/通常",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490581&auth=93931e3e6f9f1a5b3a66d862d6ae773b4ef9fe78",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490581&auth=58ef640dc3806a01f12d0f8b76d1858554a93116",
        },
        {
            name: "パスカル",
            artist: "岡部啓一 / 石井咲希",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490582&auth=29d95d02f1558a2d02d7cd56dfc7c9b18b39b053",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490582&auth=4bdb02f4e1b8f77ec41edb5c4428313f3b93a400",
        },
        {
            name: "森ノ王国",
            artist: "帆足圭吾",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490583&auth=9ba838ff74a7bf1112685fe09c6427977ceb74ac",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490583&auth=6b98cd8f1355d5ed883bf6bf9a296e11e3f77c9e",
        },
        {
            name: "全テヲ破壊スル黒キ巨人/怪獣",
            artist: "帆足圭吾 / 岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490584&auth=e54a42ed73fbb9b33e90cfd47d7962c30440137c",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490584&auth=37ed17a3bc5769d27728908884552737080207a7",
        },
        {
            name: "複製サレタ街",
            artist: "帆足圭吾",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490585&auth=22fc942af9d5fa190a5bc6e940c787a582edd55f",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490585&auth=18a61d73158fc7e322f94891b7ffb67cd7d6ffc8",
        },
        {
            name: "愚カシイ兵器:乙:甲",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490586&auth=02a2dcd78648ee7983a1e474b3baa7f8db122acb",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490586&auth=07f209334f7c434f3001addfba435e4d0a47a10c",
        },
        {
            name: "取リ憑イタ業病",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490587&auth=694dddf7e26f930f330722b975940101555b6cd8",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490587&auth=af4a503eaa9c60b11fb0d646c4c623bd446c5c3b",
        },
        {
            name: "割レタ心",
            artist: "帆足圭吾",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490588&auth=c9668dea20321dd0c173bea90bd66d846d7cdb30",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490588&auth=98b16982eefccfc2d0212fee2ba0ba49a29615c6",
        },
        {
            name: "愚カシイ兵器:丙",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490589&auth=a0f76192d31e200a8ffff6b06bf9c53d0421ed08",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490589&auth=94fb721e97dde68b5dfc24755f597d96392a10f2",
        },
        {
            name: "追悼",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490590&auth=544bc6e5cbd8b018938a9a136c39bacc7e2274b8",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490590&auth=b61b3ba2f0fef30567784c547042a540edd40bab",
        },
        {
            name: "依存スル弱者",
            artist: "高橋邦幸",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490591&auth=f43ddf069309c3b947c2c778404a7c32a66eb7ce",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490591&auth=8b380a2d18f8ca547db4932ce2724da416a3e8ec",
        },
        {
            name: "Weight of the World/壊レタ世界ノ歌",
            artist: "河野マリナ",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490592&auth=8df80dd43bc3b701d0f1391f7efbd23afde7acab",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490592&auth=69ee3f435f02b54d2dc1a6435e3fc7d842e6bcf8",
        },
        {
            name: "再生ト希望",
            artist: "帆足圭吾",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490593&auth=d380310f499084deb0052117483fd749dd4e5b10",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490593&auth=eca6ea440db7c428ccc0516fb00842a93be57ef7",
        },
        {
            name: "戦争ト戦争",
            artist: "帆足圭吾 / 岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490594&auth=4bb24133e86f8fa758685dccb81732615dfea08d",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490594&auth=aac194fed0f6fa25e1e96aebb0d8ede286a69338",
        },
        {
            name: "崩壊ノ虚妄",
            artist: "帆足圭吾",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490595&auth=9cb1966ba9c0b920af09ecad08e8a739be2aeeab",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490595&auth=931b5b73c8abad8882c9a91872960eec1e014a7e",
        },
        {
            name: "茫洋タル病",
            artist: "帆足圭吾",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490596&auth=2d787dd328a3ff262c0ee8ba8db238932151cf33",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490596&auth=ee03a0e7fb327f7cba14ce2f84842e2121014995",
        },
        {
            name: "偽リノ城塞",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490597&auth=f292a0ee1f68cff4e7287caef2eaa5709d15d6ac",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490597&auth=2f2283b043818d314073bc3f5687022a851c3b8c",
        },
        {
            name: "曖昧ナ希望/翠雨",
            artist: "帆足圭吾",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490598&auth=efa94c33dc6460c0a58165c9d5df39ff6fb4d379",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490598&auth=20bfd7c89be385c8715afd8db3c8948b14a7872d",
        },
        {
            name: "イニシエノウタ/贖罪",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490599&auth=bc5879adf0987559701cde9a1046f5fa156b73af",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490599&auth=78dcd61892eaf319f4cd0e583cfc2ce004b70bca",
        },
        {
            name: "幸セナ死",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490600&auth=b58854a6a51ebc0ea5c9da17aac3bcb47eb67756",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490600&auth=000edeea694e815f9fabc201295d0aa143d4c24b",
        },
        {
            name: "エミール/絶望",
            artist: "帆足圭吾 / 石濱翔",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490601&auth=89c53de3c292a75c1cb6148b6919e673265e068d",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490601&auth=aa6853de65f8eb12cbfb8d959ad44a46e19975a9",
        },
        {
            name: "澱ンダ祈リ/星空",
            artist: "帆足圭吾 / 岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490602&auth=4b451a27a711aaa4b4dd3575f2375973e07dba15",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490602&auth=32b0594fcdfc024a0cdf8d9f92aba5b8def795cd",
        },
        {
            name: "顕現シタ異物",
            artist: "岡部啓一 / 帆足圭吾",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490603&auth=34c3e05c7ef246e1e9358c56a804dca092549363",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490603&auth=a2ee13c93affa15605c676b8a6ee594c806af1e4",
        },
        {
            name: "「塔」",
            artist: "岡部啓一",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490604&auth=85cb9f8deac3604adac93f8f4da350fe7bdc98d7",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490604&auth=bdca4fcf47ce963ba3582819afa54fed4322994d",
        },
        {
            name: "双極ノ悪夢",
            artist: "帆足圭吾",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490605&auth=a4fedeca39fbf9907fcbfab8e3aa13c49251bc7a",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490605&auth=d4f419d45157fb7662360b2748e92719168cec4f",
        },
        {
            name: "終ワリノ音",
            artist: "帆足圭吾",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490606&auth=9828c7b8f1014f73d243ad078799746205dc8e75",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490606&auth=d6d2f58c7cbbf142525343207f27a48cc40ccdce",
        },
        {
            name: "Weight of the World (Nouveau-FR Version)",
            artist: "Emi Evans",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490607&auth=ba509b9a09a2032a186035b33c6689b8fdcc49f5",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490607&auth=e450009ff5518749774744bd4ef3d0569a2c3f74",
        },
        {
            name: "Weight of the World/the End of YoRHa",
            artist: "YoRHa",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=468490608&auth=a4ec488e5a247c7cf90b01411ea92fbb0ae17fe0",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18710389371705314&auth=08e2e2834543f80f3347a9ea345245b60ef20fb8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=468490608&auth=93dd8ce597c092e81cefd82308f2b084565f83d8",
        },
        // 史诗音乐
        {
            name: "He's a Pirate",
            artist: "Klaus Badelt",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=1620506&auth=a8433e456a1f7d81be4c9e6b7840e796071ff66e",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=837827860415832&auth=25778403d8f2b42dec34d9b6950e1be09d2d9fbd",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=1620506&auth=192d0e2e436dcb4c5e0bc9500bbfc6efb723e737",
        },
        {
            name: "Go Time",
            artist: "Mark Petrie",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=29717271&auth=a7bdbdc3373a297f6ef0c2038b2f0027ac950ce1",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=3233663697760186&auth=4fb7ba29e90437e6111e28e2dc1b710216f52823",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=29717271&auth=00fcbb412ca3e1f08311e609793fb5fa042e2292",
        },
        {
            name: "Breath and Life",
            artist: "Audiomachine",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=3935139&auth=6131bfdee7fe986221df2147dc20ec338899f4f1",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951163818565831&auth=e46e2907e25f75f9c8abb5f15582ac9fa69def9f",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=3935139&auth=ec9e86037737449b0cbf342e307ea50eda126d60",
        },
        {
            name: "Star Sky - Instrumental",
            artist: "Two Steps From Hell",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=31654479&auth=0ad1351f537bfdfed06713b3b54702e520d8e60a",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951163892182787&auth=c368e8ec172dfc9d1a47b7b2ec84d97ba1917773",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=31654479&auth=04feb7de9333d5a85e4df71aeadaa8b43e230f85",
        },
        {
            name: "Strength Of A Thousand Men",
            artist: "Two Steps From Hell",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=4377220&auth=bafd0582925ad761dd67348e776d2fa16fced6fd",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=1792203953282154&auth=78b0e2c2f2ea7a41c5ba3bdba0fd5eb9e99dc81d",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=4377220&auth=1d0bf07cd9e2f8b060c1621e7a26bdf30f332d04",
        },
        {
            name: "Will and Elizabeth",
            artist: "Klaus Badelt",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=1620471&auth=82746b22f54731827a6b4999b19f20fca920ba95",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=837827860415832&auth=25778403d8f2b42dec34d9b6950e1be09d2d9fbd",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=1620471&auth=ad188d21fa4781bc538d29eff7627ea25750df06",
        },
        {
            name: "Rags To Rings",
            artist: "Danny McCarthy",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=28949412&auth=59a797324c0e657bddfbf60ca5fa98bc1d2c20b1",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951163927001234&auth=b4c38d58f168c6ff83e300adfa7263609b21c447",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=28949412&auth=9460f56c95e3afff9556d8c8e0f1db16b8e8b468",
        },
        {
            name: "Man At Arms",
            artist: "Position Music",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=5137277&auth=3a987c59f72a59b96bda31cfa4ba6e2057ebbf9d",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=1692148395160175&auth=aa7098b292e07fbd89c596d1e2e930f51fc52cd2",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=5137277&auth=f26a8fa65db7bdfb01125c33a26d03497beccdfd",
        },
        {
            name: "Victory",
            artist: "Two Steps From Hell",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=31654455&auth=e4729b7a27a99f62d1f588793ff7bdfc9d3f95e4",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951163892182787&auth=c368e8ec172dfc9d1a47b7b2ec84d97ba1917773",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=31654455&auth=0cd00982498547813f5e0863a6d72df4b7da734a",
        },
        {
            name: "Tales of the Electric Romeo",
            artist: "Immediate Music",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=27129154&auth=de8adb7511e2b89de4a62b5291ef93d871536992",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951165134752033&auth=a207c515b0e03006af0ba92cf981cac30a531c2f",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=27129154&auth=7b87dc647d2241caf67a9dac92e411ea35a1e09c",
        },
        {
            name: "Brotherhood",
            artist: "John Dreamer",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=28283145&auth=416d9373bd81abb72c8c1f78d60a32477291a203",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=6056110045882960&auth=426984141196e81668c9cd7fc8ddac8179ffe576",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=28283145&auth=834e26b2cf71e2b70b93f349da25b8fd009208f3",
        },
        {
            name: "Here Comes the King",
            artist: "X-Ray Dog",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=4436059&auth=c2025389100b9105f5a3a6b0012fb64d42fa4faf",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=734473767399807&auth=8922cc1d5a93cece1e46807f276de29e4ab5d2bb",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=4436059&auth=709c04037ce58999477935850d3b230e40292ada",
        },
        {
            name: "For the Win",
            artist: "Two Steps From Hell",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=27032793&auth=94712ebe937a560376d63bc33478e735d7b557f6",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951164985327121&auth=3a1a49fdae8ea52f27b870186f02ff7b88e2fb0d",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=27032793&auth=2f1933fef3016dfa0385d74e6942aaa170519dc8",
        },
        {
            name: "El Dorado (Dubstep Remix)",
            artist: "Two Steps From Hell / Thomas Bergersen",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=27406244&auth=26ae7faa748fcf6fae5bc096f51173780231cfc7",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951165052064665&auth=f2f28b1d02b368911d60dcbe10aac8982661271f",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=27406244&auth=64493e93aae1699cefe2882558d8969b27242c98",
        },
        {
            name: "League of Legends Epic Dubstep(LOL Remix)",
            artist: "ThimLife",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=32341324&auth=e5dfa895a5285df25ad90afe50cea80e958d81de",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951163927000273&auth=97bf6b82402d1735e455a221c72464255c492e6e",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=32341324&auth=d2179dd5ebeb3e36bfff46a7c89d9dd85d343db4",
        },
        {
            name: "Archangel",
            artist: "Two Steps From Hell",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=4377218&auth=14de93b6e786e04417d3b9506388426547985538",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=1792203953282154&auth=78b0e2c2f2ea7a41c5ba3bdba0fd5eb9e99dc81d",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=4377218&auth=9180283e4bd88d47816fcd07eb8a1c9a5d03b4c1",
        },
        {
            name: "String Tek",
            artist: "X-Ray Dog",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=4441159&auth=7be69564c7af94bcad32a6ba5d4d8160056140c3",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=1654764999813134&auth=80e152c9ea6f27be3922c753c63d3fde6049ac54",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=4441159&auth=523d0d492b1d7c1cd1e25956287e5f37f6ec3282",
        },
        {
            name: "Seafights",
            artist: "Groove Addicts",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=34167790&auth=ea0c9b98a95418eb280e30911f8a5b7d89b10ce6",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=3290838302387401&auth=c28dcddc0bebcd2a978654d41f4d21e6d9986907",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=34167790&auth=8d1bba70c77fd33ea34b2b44e573c647f5179cea",
        },
        {
            name: "The Mass",
            artist: "Era",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=17538867&auth=99abc26912b1a2cb8b5b1dae45e0d825a05c25c4",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=732274744107448&auth=28ee5b399b1d72f51238e981984b70da6014d082",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=17538867&auth=e0c71c25810d1408675c17221baa1d90890c239d",
        },
        {
            name: "Danuvius",
            artist: "Audiomachine",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=3934578&auth=2b52751d144707bdab2e8b07a3917197fa61f922",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=648711860431937&auth=9ae69aed3ff36fb5777306ef1b3452486aecd2ff",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=3934578&auth=867e666e6c21936c3239a4126d38faa592176531",
        },
        {
            name: "Star Sky",
            artist: "Two Steps From Hell",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=31654478&auth=218e895d07bf235f0d526f4736e2dfaba20f9665",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951163892182787&auth=c368e8ec172dfc9d1a47b7b2ec84d97ba1917773",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=31654478&auth=ba75dcfc16ed2901be51b5ea4dec33293087372f",
        },
        {
            name: "Requiem For A Tower",
            artist: "Escala",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=17625646&auth=0d9f295a8faa25abc7a528db3fba4359bda8981e",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=18853325881506897&auth=bed00a45aacc253dde75ad0171fa4bc59b38a1ee",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=17625646&auth=dd4f4a01b25c847bffa0ce14377f3ebaf2d2640a",
        },
        {
            name: "Serenata Immortale",
            artist: "Immediate Music",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=1461836&auth=a84cca7784bccba5cada857c095cb378715bf440",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=1753721046309793&auth=428392a4b8f509735d0fc44ec10ad5473a447700",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=1461836&auth=3ad226d16d91563dc1649b936299ed38632951c3",
        },
        {
            name: "Heart of Courage",
            artist: "Two Steps From Hell",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=4377969&auth=d50271e9c4d48de21478f60a289b47db8c17d249",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=887305883660802&auth=e7f85547b29bb2ce2d0836538a64a12731ceca26",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=4377969&auth=b46606f0a46b5353e6b4d6d897d998722fd1d34b",
        },
        {
            name: "Qiu Mansion",
            artist: "Position Music",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=5137274&auth=68a05c950867f6070285002276b207f7279b08af",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=1692148395160175&auth=aa7098b292e07fbd89c596d1e2e930f51fc52cd2",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=5137274&auth=97f7067acf4d33a78ee08ce3cb4add8f96fa0e9b",
        },
        {
            name: "Croatian Rhapsody",
            artist: "Maksim Mrvica",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=1696373&auth=00570436752b71551a69051f3a7cb0f59dfdbb7c",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=705886465073362&auth=7150f1701028af838b7f569c308874238032f60e",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=1696373&auth=b4598da2a52f9a95bf1f8d3af00fd94729945854",
        },
        {
            name: "Legions of Doom",
            artist: "Audiomachine",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=3935183&auth=bf6e49ef6a17d742d2961be96f384f89fe0260e4",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951163818565831&auth=e46e2907e25f75f9c8abb5f15582ac9fa69def9f",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=3935183&auth=ac870f7577f68b26a07e29b39b9c5954c40eae91",
        },
        {
            name: "The New York Nightfall",
            artist: "Position Music",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=5137275&auth=9b7a3f4ad1e9ff1d8055f51244cd96235e68bee3",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=1692148395160175&auth=aa7098b292e07fbd89c596d1e2e930f51fc52cd2",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=5137275&auth=c28933f4d8607d30064327aa54905f40b006e0ca",
        },
        {
            name: "Icarus",
            artist: "Ivan Torrent",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=28465302&auth=ef6233385bf3c93ea9fbc3909f0950022809e30f",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=6057209557731235&auth=db92b0dbfc5d65fbe50b2d70d0ef9d72a719e6ba",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=28465302&auth=14d4a79c25b8499c45311f1537a3eceadb0e5f44",
        },
        {
            name: "Shadowfall",
            artist: "Audiomachine",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=27969824&auth=6d7c8b757dd32aee6249c2232454dcf569aeb7f8",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=5681176580838005&auth=9ad63b318eec00cc9e2e430722226fdbe0f77f51",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=27969824&auth=6ae6beb1ab622d926f3cad87658c0e399fef5d26",
        },
        {
            name: "Winterspell",
            artist: "Two Steps From Hell",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=27032787&auth=a67f1bf918cec2b00b737f2fcbb2c8f6207a50fb",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951164985327121&auth=3a1a49fdae8ea52f27b870186f02ff7b88e2fb0d",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=27032787&auth=fb5740eddab31cfda036678806002e382edf6f3c",
        },
        {
            name: "Sad Run",
            artist: "V.A.",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=5397654&auth=776389c6dc87883f00bf171c0e354521b07eea06",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=614626999932663&auth=c746750b874e5b06a504976d240f9b7080c777e1",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=5397654&auth=27757d4ccbdd1da22ead2331bc369e70dfe0a668",
        },
        {
            name: "Face The World",
            artist: "Sub Pub Music",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=30284103&auth=50e3dc3efc838786fbb37efd3a37251ad9cfac45",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=7740561859650191&auth=48d97142b0d4ec0cadfddb87fde5f0f6184670d4",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=30284103&auth=ce8eed2bf2fda9b4da2361e6518c5905d69d9eea",
        },
        {
            name: "Immortal",
            artist: "Thomas Bergersen",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=2061739&auth=d7b80f2ca768f3a95ced41c933f3d8b39722ddbd",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951164985328086&auth=fbd141020db7ade1014852c4e7a89b119e348c95",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=2061739&auth=51b3b5424288bfe766bd9e0c0bc36f105e3878b1",
        },
        {
            name: "Empire of Angels",
            artist: "Thomas Bergersen",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=29460377&auth=432dfe844c2889a9fb618f3a219d3e80a8a203f8",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951164985317990&auth=69bf391a4b27b619a00aeed9e3bf1be20aecbf84",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=29460377&auth=5ce70e713c379e7619fa123d750610303750e820",
        },
        {
            name: "Cyberworld",
            artist: "X-Ray Dog",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=4441146&auth=3101d49a5af0f286e062bcb6a4e0288e43914521",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=1654764999813134&auth=80e152c9ea6f27be3922c753c63d3fde6049ac54",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=4441146&auth=7088032229597c25014c631d8f95595c98089ea5",
        },
        {
            name: "Rise - Epic Music",
            artist: "John Dreamer",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=28283137&auth=401989ed4b93ef6ffd06331486eb9bf24837ce5f",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951165091749637&auth=481be56f7b876ea7ceaef870ff0d2d6d1eb8bd0b",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=28283137&auth=0f3d0b9add067692611fcaece07e8f8850ca70f2",
        },
        {
            name: "El Dorado",
            artist: "Two Steps From Hell",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=27032783&auth=4ff1a3374c571d77eb57aea0061eff0f0d914e5e",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951164985327121&auth=3a1a49fdae8ea52f27b870186f02ff7b88e2fb0d",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=27032783&auth=2c0c4ed3bb9951b527b4bb02fa1f65364284b517",
        },
        {
            name: "Becoming a Legend",
            artist: "John Dreamer",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=28283167&auth=784243384fdbaf522af0d0e2baa856773fba2c63",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=5909874999459103&auth=7dfa52c5817707a36f4d0ca76398b6d7192d3ab9",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=28283167&auth=43173c13bb66f5483ba1ba11cade71d99e87184d",
        },
        {
            name: "Mind Heist",
            artist: "Zack Hemsey",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=2112355&auth=cbbde99d90a23785720bace0705c8e69e50556ec",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=798245441818902&auth=8febd91774338173670ad9886e15fb6f741f21f8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=2112355&auth=a666c721ed6c3d19b691dbcef2df4a1d306e950a",
        },
        {
            name: "Hope Always",
            artist: "X-Ray Dog",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=4437849&auth=0735f5e970dfeb53d4e4a900e9dc2bc583f8a348",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=700388906936458&auth=6e743c6f762e497fe2bc81ea6bfac37f7e951301",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=4437849&auth=f32d7ee4cf85ac30a8dec9014d2a630d5655485a",
        },
        {
            name: "Dark Side Of Power",
            artist: "Immediate Music",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=1461587&auth=e9c653a8d65990a635e255b61a3d077b26249a1b",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=1725133743987657&auth=d3b264ad1e588c835b291d6f0a7d8fb17a74bcbb",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=1461587&auth=34daea87e3b3becdeba9b4b15f0b96a32345c9cd",
        },
        {
            name: "Opening Credits",
            artist: "Hans Zimmer / Lorne Balfe",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=1427087&auth=58204d41134fe27cec1fb9ee0d0e1fcbb0ff76b7",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951165341307864&auth=1e4af4148ec3fef370fef4e5676e6865bb74a445",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=1427087&auth=f1eeedbfe3f8392a6711a4876b8e6f4144e9cb66",
        },
        {
            name: "Hero's Theme",
            artist: "Steven Burke",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=1984625&auth=3bcd0763582dc07c69f2dcf1a126ae1585490771",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=831230790651610&auth=31ad981386999479a6f1ab357c12f4feea7d64af",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=1984625&auth=b16f4747be235a7e713a9036936c380fbdc2c548",
        },
        {
            name: "300 Violin Orchestra",
            artist: "Jorge Quintero",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=28391612&auth=dc126c92b9559cfd1abd994d594b532891482dc2",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=3264450024433090&auth=b22e915f2daf8eb8fd68e244f66324174248d127",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=28391612&auth=baebe75ecda506db993e3b0735122dd3cab02555",
        },
        {
            name: "Dogs of War",
            artist: "Brand X Music",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=3977931&auth=ea5103f3fe2cbded19c15feeda02c8b388c5b3b0",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=1819691743976214&auth=5fc43b029785b2c79da800979cde055fbc8ba06a",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=3977931&auth=3424f3b30d94930c47070aaad0fd86756ffe0651",
        },
        {
            name: "Falling Into a Dream",
            artist: "Brian Tyler",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=28188764&auth=29161ad089fbc731ee3ea8415a0027ec70bf9763",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951164856034586&auth=3327003b4b8f42a1817d812d5ca6ba0379423be5",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=28188764&auth=cec63f8c759573e23b593fdad454a5e7c0ec570c",
        },
        {
            name: "Human Legacy",
            artist: "Ivan Torrent",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=27570917&auth=8cc7ca0c08a5f8a92116542a38e7d09990b4da9c",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=5757042883102605&auth=d2eb61d933d2be7430a1308b14ac705a3082f60e",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=27570917&auth=6214262b680ed96dba5cd1302086f02ca65fbb6d",
        },
        {
            name: "False King",
            artist: "Two Steps From Hell",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=19558020&auth=49f499f614fdeb8f95cff555ac57a7f62d8dff82",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951164985320902&auth=0656a9933a25d543fb48bee1e9fd2a861542fb03",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=19558020&auth=8b100f0478275438309ca9a344e742c3d47fbcf2",
        },
        {
            name: "Gangsta Symphony",
            artist: "Philip Guyler",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=29812868&auth=2251cd2dcb6e6384643711cd2731fea9bf157488",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=6647647302861412&auth=48cde98cabebfc1b713300e0de3501afc3b57d71",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=29812868&auth=57ea35c46bf0abaeaad2ad800b78406de5804ecd",
        },
        {
            name: "The New Earth",
            artist: "Audiomachine",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=3935239&auth=bfa3770fb0e5e92f6652db38948d4932981a9abf",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951163818605986&auth=8092c74052f9253231715dbc88ab6bfc99eb18fa",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=3935239&auth=908f18fc7729132bf078702acbdd2533861d2589",
        },
        {
            name: "Dragon Rider",
            artist: "Two Steps From Hell",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=19554998&auth=f7213d8ef3339135bf3c7959850f77bc76ed4321",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=2536573325495334&auth=887098ea06f47785656b3c7d1f0ef0ca9ddb5564",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=19554998&auth=44148678fbae693ed8b2340af4455621b31d6e3f",
        },
        {
            name: "Liberators",
            artist: "Epic Score",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=41630480&auth=d9ba5a5d0bc94ac1a1755d6e975aab3873e92a82",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=2535473818452611&auth=fd50c7217223a30a1a0a4760be1ea5f87159732f",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=41630480&auth=3428c9dc7d3840340a327a9e5ff2b8a4efa84501",
        },
        {
            name: "Never Back Down",
            artist: "Two Steps From Hell",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=31654453&auth=05fcb7f609f40a3435b1a6e8ed4a4b9753192f61",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951163892182787&auth=c368e8ec172dfc9d1a47b7b2ec84d97ba1917773",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=31654453&auth=6d31865bb2a7fc51264d477703bb920e9eddc39e",
        },
        {
            name: "True Strength - Epic Music",
            artist: "John Dreamer",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=28283155&auth=f5bed6beaecf1f27dca1bf8daf23dfee1b367c5b",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=6024224208678156&auth=32bdaa2460166630b01710f99a896b73756a5082",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=28283155&auth=309e0e2a10c1064da9ba320d4815bd0b5ab68658",
        },
        {
            name: "Helmet to Helmet",
            artist: "Brand X Music",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=34749433&auth=41b861d2084cbb9ffd0d0fdbef91714f10a143ea",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=3331520232839703&auth=648997ed376e9c55808957628b0f4f569c241134",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=34749433&auth=8fae0f3043d8b8b14ecdea9acfeecf8e1a3a93d9",
        },
        {
            name: "Guardians at the Gate",
            artist: "Audiomachine",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=3935176&auth=f464599b6b643821ff1b5cc6da79cbfe55e7e867",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951163818565831&auth=e46e2907e25f75f9c8abb5f15582ac9fa69def9f",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=3935176&auth=7acd914f9d5e024b55426e51ac05d2b5c492bc7f",
        },
        {
            name: "SkyWorld",
            artist: "Two Steps From Hell",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=27032782&auth=94f6e0e2feb6097061dc9efdf6200053b255d6c5",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=109951164985327121&auth=3a1a49fdae8ea52f27b870186f02ff7b88e2fb0d",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=27032782&auth=9ff14e4221551a00e7c68ef0fdbe0aa26855ddd4",
        },
        {
            name: "Pacific Rim",
            artist: "Ramin Djawadi",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=26625301&auth=e81e36b17bd8a1a537d39e35dccef11af7c544cd",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=4449723557623509&auth=432653f1c8242b67c90ee2e6ee32348f0af8e9fe",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=26625301&auth=baddd41f9f86c30382d1c8fe18338f66c5ce8c32",
        },
        {
            name: "Main Titles",
            artist: "Ramin Djawadi",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=32526653&auth=8579bb1c2108f9ee9dcfdbab0e06ce593ae7916c",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=7987951976023943&auth=311385104ebee35590846a73347790d4d2cf2ef9",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=32526653&auth=4154c0f8693e4ff4aa3fbd332cdc20d9d5449733",
        },
        {
            name: "Vishnu",
            artist: "E.S. Posthumus",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=17539358&auth=2ddd5f058397b5b947be7cdd5e2fc52771fbc671",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=2528876745229043&auth=5275ba106e8ca534cbcc3bcaea9780c53b0e77ed",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=17539358&auth=bb391dbc2671ef5018db6d6755a67156a798648f",
        },
        {
            name: "New Light",
            artist: "Mark Petrie",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=29045492&auth=f8ce04c031c3cf0b84ff23f7e34e18fb068d37e0",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=6646547791017831&auth=8d7acd16fb547d7cd47fa8e354e95cac729d35a8",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=29045492&auth=d60c53baf87c94545dc0cc32d36424ba3594a2b0",
        },
        {
            name: "Equitatus",
            artist: "PostHaste Music Library",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=36871361&auth=0d4dd9b44aafaee4a39577e85ce463294dd056a9",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=3254554421840924&auth=1bcbc849b580004331b9b9a5003ca811dc70328e",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=36871361&auth=123e9d2158aabc8e79cee097abf7848057ef6cec",
        },
        {
            name: "Glory Seeker",
            artist: "Immediate Music",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=1461513&auth=985c13e7eace8c21327ac7f249e30a1057ec18a6",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=1725133743987647&auth=466e0978bc4d2f716f32fc3c6c93e125689437aa",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=1461513&auth=02992751219ff39f6eee33bfc8d84dbb41c9cdd0",
        },
        {
            name: "Invictus",
            artist: "Immediate Music",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=1461521&auth=1e448eb15a92a4231f709652f47021b1bc9d172e",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=1725133743987647&auth=466e0978bc4d2f716f32fc3c6c93e125689437aa",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=1461521&auth=4471d89ad3e19531e1e36a4897cca282fd1df3e0",
        },
        {
            name: "Intro",
            artist: "The xx",
            audio:
                "https://api.i-meto.com/meting/api?server=netease&type=url&id=4341314&auth=7c7b19ff1802c87d8ef05592a0e44510700c20c0",
            cover:
                "https://api.i-meto.com/meting/api?server=netease&type=pic&id=825733232504415&auth=e257b539a3902c673f5468c2f9584b75e20ac4bd",
            lrc:
                "https://api.i-meto.com/meting/api?server=netease&type=lrc&id=4341314&auth=3b321589908321685b22b97a85b9e780311543c7",
        },
    ];

    $(".toast").toast({ delay: 1500 });

    window.copyToClipboard = (text) => {
        try {
            // let transfer = document.createElement('input');
            // document.body.appendChild(transfer);
            // transfer.value = text;  // 这里表示想要复制的内容
            // transfer.focus();
            // transfer.select();
            // if (document.execCommand('copy')) {
            //     document.execCommand('copy');
            // }
            // transfer.blur();
            // console.log('复制成功');
            // document.body.removeChild(transfer);
            navigator.clipboard
                .writeText(text)
                .then(() => {
                    console.log("已复制到剪贴板");
                })
                .catch((err) => {
                    console.error(err);
                    return false;
                });
            $("#copySuccessToast").toast("show");
            return true;
        } catch (error) {
            console.error(error);
            return false;
        }
    };
});
