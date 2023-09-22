function cambiarModo() {
    document.body.classList.toggle("oscuro");

    var icono = document.getElementById("logo-btn");
    if (icono.className == "fas fa-sun") {
        icono.className = "fas fa-moon";
    }
    else {
        icono.className = "fas fa-sun";
    }
}