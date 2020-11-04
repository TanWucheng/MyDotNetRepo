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

document.addEventListener('DOMContentLoaded', function () {
    const sideNavElems = document.querySelectorAll('.sidenav');
    M.Sidenav.init(sideNavElems, {});
});