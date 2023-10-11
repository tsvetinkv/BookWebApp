document.addEventListener("DOMContentLoaded", function () {
    const profileButton = document.getElementById("profile-button");
    const dropdownMenu = document.getElementById("dropdown-menu");
    let isOpen = false;

    profileButton.addEventListener("click", function (e) {
        e.stopPropagation();
        isOpen = !isOpen;
        dropdownMenu.style.display = isOpen ? "block" : "none";
    });

    document.addEventListener("click", function () {
        if (isOpen) {
            isOpen = false;
            dropdownMenu.style.display = "none";
        }
    });

    dropdownMenu.addEventListener("click", function (e) {
        e.stopPropagation();
    });
});
