//function showEpisodes(id) {
//    console.log("کلیک شد: " + id);

//    // اینجا می‌تونی با fetch یا $.ajax اطلاعات قسمت‌ها رو بگیری
//    fetch(`/api/MultimediaHeader/Episodes/${id}`)
//        .then(res => res.json())
//        .then(data => {
//            // نمایش قسمت‌ها در مودال یا div
//            console.log(data);
//        });
//}
function showEpisodes(element) {
    const id = element.getAttribute("data-id");
    console.log("کلیک شد: " + id);

    DotNet.invokeMethodAsync("BlazorApp1", "ShowEpisodesFromJs", id);
}


