
//BOUTON MODIFIER PROFIL
btnModifProfil();

function btnModifProfil() {
    var x = document.getElementById("modifier-profil-content");
    var y = document.getElementById("btn-modif-profil");
   

    if (x.style.display === "none") {
        x.style.display = "block";
        y.style.display = "none";
    } else {
        x.style.display = "none";
    }
}

//BOUTON ANNULER MODIFICATION PROFIL
function btnAnnulerProfil() {
    var x = document.getElementById("modifier-profil-content");
    x.style.display = "none";

    var y = document.getElementById("btn-modif-profil");
    y.style.display = "block";

}

//ONGLET CONTATCS
document.getElementById("contacts-tab").onclick = function () {
    document.getElementById("contacts-tab").className = "nav-tabs active";
    document.getElementById("iuserplus").className = "fas fa-user-plus";
    document.getElementById("iusers").className = "fas fa-users iblue";
    document.getElementById("invitations-tab").className = "nav-tabs";
    document.getElementById("contacts-content").className = "d-in";
    document.getElementById("invitations-content").className = "d-no";
}

//ONGLET INVITATIONS
document.getElementById("invitations-tab").onclick = function () {
    document.getElementById("invitations-tab").className = "nav-tabs active";
    document.getElementById("iusers").className = "fas fa-users";
    document.getElementById("iuserplus").className = "fas fa-user-plus iblue";
    document.getElementById("contacts-tab").className = "nav-tabs";
    document.getElementById("invitations-content").className = "d-in";
    document.getElementById("contacts-content").className = "d-no";
}