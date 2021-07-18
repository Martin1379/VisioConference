//ONGLET CONTATCS
document.getElementById("contacts-tab").onclick = function () {
    document.getElementById("contacts-tab").className = "nav-tabs active";
    document.getElementById("invitations-tab").className = "nav-tabs";
    document.getElementById("contacts-content").className = "d-in";
    document.getElementById("invitations-content").className = "d-no";
}

//ONGLET INVITATIONS
document.getElementById("invitations-tab").onclick = function () {
    document.getElementById("invitations-tab").className = "nav-tabs active";
    document.getElementById("contacts-tab").className = "nav-tabs";
    document.getElementById("invitations-content").className = "d-in";
    document.getElementById("contacts-content").className = "d-no";
}