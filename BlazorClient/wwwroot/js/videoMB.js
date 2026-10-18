//window.addEventListener("DOMContentLoaded", function () {
//    const video = document.getElementById("myVideo");
//    console.warn(video);
//    if (!video) {
//        console.warn("ویدئو پیدا نشد!");
//        return;
//    }

//    video.addEventListener("play", function () {
//        const url = new URL(video.currentSrc);
//        const encoded = url.searchParams.get("address");

//        fetch(`/PlayVideo?address=${encodeURIComponent(encoded)}`)
//            .then(response => {
//                const mbSent = response.headers.get('X-MB-Sent');
//                DotNet.invokeMethodAsync('BlazorClient', 'ReportUsage', mbSent);
//            });
//    });
//});
//window.attachVideoUsageTracker = function () {
//    const video = document.getElementById("myVideo");

//    if (!video) {
//        console.warn("ویدئو پیدا نشد!");
//        return;
//    }

//    video.addEventListener("play", function () {
//        const url = new URL(video.currentSrc);
//        const encoded = url.searchParams.get("address");

//        fetch(`/PlayVideo?address=${encodeURIComponent(encoded)}`)
//            .then(response => {
//                const mbSent = response.headers.get('X-MB-Sent');
//                DotNet.invokeMethodAsync('BlazorClient', 'ReportUsage', mbSent);
//            });
//    }, { once: true });
//};
window.attachVideoUsageTracker = function () {
    const video = document.getElementById("myVideo");
    if (!video) return;

    video.addEventListener("play", function () {
        fetch(video.currentSrc)
            .then(response => {
                const mbSent = response.headers.get('X-MB-Sent');
                DotNet.invokeMethodAsync('BlazorClient', 'ReportUsage', mbSent);
            });
    });
};
