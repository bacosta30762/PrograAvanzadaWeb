var connection = new signalR.HubConnectionBuilder().withUrl("/AlertasHub").build();

var shownNotifications = new Set();

const maxNotifications = 5;  
let notificationQueue = []; 

connection.on("ReceiveMessage", function (message) {
    if (shownNotifications.has(message)) {
        return;
    }

    shownNotifications.add(message);

    var notification = document.createElement("div");
    notification.className = "dropdown-item notification";
    notification.style.cursor = "pointer";
    notification.style.borderRadius = "8px";
    notification.style.marginBottom = "10px";
    notification.style.transition = "background-color 0.3s ease, transform 0.2s ease";
    notification.style.backgroundColor = "#ffffff";
    notification.style.color = "#2C3E50";
    notification.style.padding = "20px 30px";  
    notification.style.boxShadow = "0 2px 8px rgba(0,0,0,0.1)";
    notification.style.maxWidth = "800px";  
    notification.style.wordWrap = "break-word";
    notification.style.fontFamily = "Arial, sans-serif";
    notification.style.fontSize = "14px";
    notification.style.width = "auto";  
    notification.style.minWidth = "400px";  

    
    var media = document.createElement("div");
    media.className = "media";
    media.style.display = "flex";
    media.style.alignItems = "center";
    media.style.padding = "0";

    var i = document.createElement("i");
    i.className = "fa-solid fa-circle-exclamation";
    i.alt = "Imagen de usuario";
    i.style.width = "32px";  
    i.style.height = "32px";
    i.style.fontSize = "32px";  
    i.style.color = "#E02454";

    var iconContainer = document.createElement("div");
    iconContainer.style.display = "flex";
    iconContainer.style.justifyContent = "center";
    iconContainer.style.alignItems = "center";
    iconContainer.style.marginRight = "12px";
    iconContainer.appendChild(i);

    var mediaBody = document.createElement("div");
    mediaBody.className = "media-body";

    var strongText = document.createElement("p");
    strongText.className = "mb-0";
    strongText.style.whiteSpace = "normal";
    strongText.style.fontSize = "14px";
    strongText.style.lineHeight = "1.4";
    strongText.innerHTML = `<strong>${message}</strong>`;

    mediaBody.appendChild(strongText);
    media.appendChild(iconContainer);
    media.appendChild(mediaBody);
    notification.appendChild(media);

    var messagesList = document.getElementById("notificationDropdown");

    
    if (notificationQueue.length >= maxNotifications) {
        var oldestNotification = notificationQueue.shift();  
        messagesList.removeChild(oldestNotification);  
    }

    
    notificationQueue.push(notification);  
    messagesList.appendChild(notification);

    
    var badge = document.getElementById("notificationBadge");
    var count = parseInt(badge.textContent) + 1;

    if (count < 10) {
        badge.textContent = count;
        badge.style.display = "inline";
    } else {
        badge.style.color = 'green';
        badge.textContent = '9+';
        badge.style.display = "inline";
    }

    
    notification.addEventListener("click", function () {
        notification.style.backgroundColor = "#f1f1f1";
        window.location.href = "/Vuelos/index"; 
    });

    
    notification.addEventListener("mouseover", function () {
        notification.style.backgroundColor = "#F5F5F5";
        notification.style.transform = "scale(1.05)";
        notification.style.color = "#E02454";
    });

    notification.addEventListener("mouseout", function () {
        notification.style.backgroundColor = "#ffffff";
        notification.style.transform = "scale(1)";
        notification.style.color = "black";
    });
});



connection.start().then(function () {
    
        var fechaActual = new Date();
        var fechaEn10Horas = new Date();
        fechaEn10Horas.setHours(fechaActual.getHours() + 10); 

        $.get("https://localhost:7030/Alertas/AlertasJson", function (data) {
            if (data && connection) {
                console.log("Consultando alertas de vuelos...");
                $.each(data, function (index, vuelo) {
                    if (vuelo) {
                        let message = '';

                        let horaSalidaVuelo = new Date(vuelo.departure.scheduled);

                        if (horaSalidaVuelo >= fechaActual && horaSalidaVuelo <= fechaEn10Horas) {
                            message = `El vuelo ${vuelo.flight.number} desde ${vuelo.departure.airport} hacia ${vuelo.arrival.airport} está programado para salir a las ${horaSalidaVuelo.toLocaleString()}, no olvides apartar tu vuelo!`;
                        }

                        if (message && !shownNotifications.has(message)) {
                            connection.invoke("AFlightAlert", message).catch(function (err) {
                                return console.error(err.toString());
                            });
                        }
                    }
                });
            }
        });
    
}).catch(function (err) {
    return console.error(err.toString());
});



/*
*************************************************************
 NO BORRAR, ESTO SE ACTIVA CUANDO VAYAMOS A PRESENTAR, ESTA PARTE HACE QUE LAS NOTIFICACIONES SE VAYAN ACTUALIZANDO CADA MINUTO
 
/*
connection.start().then(function () {
    // Intervalo para obtener alertas de vuelos de la API
    setInterval(function () {
        var fechaActual = new Date();
        var fechaEn10Horas = new Date();
        fechaEn10Horas.setHours(fechaActual.getHours() + 10); // Fecha y hora dentro de 10 horas

        // Realizar la solicitud a la API para obtener los vuelos
        $.get("https://localhost:7030/Alertas/AlertasJson", function (data) {
            if (data && connection) {
                console.log("Consultando alertas de vuelos...");
                $.each(data, function (index, vuelo) {
                    if (vuelo) {
                        let message = '';

                        // Convertir la hora de salida del vuelo (scheduled) a un objeto Date
                        let horaSalidaVuelo = new Date(vuelo.departure.scheduled);

                        // Verificar si el vuelo está dentro de las siguientes 10 horas
                        if (horaSalidaVuelo >= fechaActual && horaSalidaVuelo <= fechaEn10Horas) {
                            message = `El vuelo ${vuelo.flight.number} desde ${vuelo.departure.airport} hacia ${vuelo.arrival.airport} está programado para salir a las ${horaSalidaVuelo.toLocaleString()}, no olvides apartar tu vuelo!`;
                        }

                        // Si hay un mensaje, enviarlo a SignalR (solo si es una alerta nueva)
                        if (message && !shownNotifications.has(message)) {
                            connection.invoke("AFlightAlert", message).catch(function (err) {
                                return console.error(err.toString());
                            });
                        }
                    }
                });
            }
        });
    }, 60000);
}).catch(function (err) {
    return console.error(err.toString());
});
*/