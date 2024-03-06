const reservation = document.querySelector('.reservation');
const reservationBtn = document.querySelector('.reservation-btn');

const settings = document.querySelector('.settings');
const settingsBtn = document.querySelector('.settings-btn');

const room = document.querySelector('.room');
const roomBtn = document.querySelector('.room-btn');

const updateBtn = document.querySelectorAll('.update-btn');
const updateReservation = document.querySelector('.update-reservation');


//AVAILABLE
const available = document.querySelector('.available');
const availableBtn = document.querySelectorAll('.available-btn');
/////////////

const closeBtnRoom = document.querySelector('.room .icon-close');
const closeBtnReservation = document.querySelector('.reservation .icon-close');
const closeBtnSettings = document.querySelector('.settings .icon-close');
const closeBtnUpdate = document.querySelector('.update-reservation .icon-close');






const guide = document.querySelector('.guide-wrapper');

//AVAULABLE
const closeBtnAvailable = document.querySelector('.available .icon-close');
/////////


//RESERVARION
reservationBtn.addEventListener('click', () => {
    reservation.classList.add('active');
    settings.classList.remove('active');
    room.classList.remove('active');
    updateReservation.classList.remove('active');
    available.classList.remove('active');


    guide.classList.add('hidden');
});

closeBtnReservation.addEventListener('click', () => {
    reservation.classList.remove('active');


    guide.classList.remove('hidden');
});


//SETTINGS
settingsBtn.addEventListener('click', () => {
    settings.classList.add('active');
    reservation.classList.remove('active');
    room.classList.remove('active');
    updateReservation.classList.remove('active');
    available.classList.remove('active');


    guide.classList.add('hidden');
});

closeBtnSettings.addEventListener('click', () => {
    settings.classList.remove('active');


    guide.classList.remove('hidden');
});


//ROOM
roomBtn.addEventListener('click', () => {
    room.classList.add('active');
    reservation.classList.remove('active');
    settings.classList.remove('active');
    updateReservation.classList.remove('active');
    available.classList.remove('active');



    guide.classList.add('hidden');
});

closeBtnRoom.addEventListener('click', () => {
    room.classList.remove('active');



    guide.classList.remove('hidden');

});

//UPDATE RESERVATION

updateBtn.forEach(button => {

    button.addEventListener('click', () => {
        // Dohvati Reservation_Id iz data atributa
        const reservationId = button.getAttribute('data-reservation-id');
        const firstName = button.getAttribute('data-first-name');
        const lastName = button.getAttribute('data-last-name');
        const phone = button.getAttribute('data-phone');
        const checkIn = button.getAttribute('data-checkIn');
        const checkOut = button.getAttribute('data-checkOut');
        const room = button.getAttribute('data-room');

        const formattedCheckIn = moment(checkIn, 'DD.MM.YYYY').format('YYYY-MM-DD');
        const formattedCheckOut = moment(checkOut, 'DD.MM.YYYY').format('YYYY-MM-DD');

        document.getElementById('reservationIdShow').value = reservationId;
        document.getElementById('reservationId').value = reservationId;
        document.getElementById('reservationFirstName').value = firstName;
        document.getElementById('reservationLastName').value = lastName;
        document.getElementById('reservationPhone').value = phone;
        document.getElementById('reservationCheckIn').value = formattedCheckIn;
        document.getElementById('reservationCheckOut').value = formattedCheckOut;
        document.getElementById('reservationRoom').value = room;

       
        updateReservation.classList.add('active');
        settings.classList.remove('active');
       
    });
    
});



closeBtnUpdate.addEventListener('click', () => {
    updateReservation.classList.remove('active');
    settings.classList.add('active');
});


//AVAILABLE


availableBtn.forEach(button => {
    button.addEventListener('click', () => {
        const roomId = button.getAttribute('data-room-id');

      

        
        available.classList.add('active');
        room.classList.remove('active');
    });
});
    





closeBtnAvailable.addEventListener('click', () => {
    available.classList.remove('active');
    room.classList.add('active');
});


//////////////////////////////


//Check-In & Check-Out

var checkInInput = document.getElementById('check-in');
var checkOutInput = document.getElementById('check-out');


var checkInUpdateInput = document.getElementById('reservationCheckIn');
var checkOutUpdateInput = document.getElementById('reservationCheckOut');

var today = new Date();

var dd = today.getDate();
var mm = today.getMonth() + 1;
var yyyy = today.getFullYear();


if (dd < 10) {
    dd = '0' + dd;
}

if (mm < 10) {
    mm = '0' + mm;
}


today = yyyy + '-' + mm + '-' + dd;



checkInInput.setAttribute('min', today);
checkInInput.value = today;


checkOutInput.addEventListener('input', function () {
    checkInInput.setAttribute('max', checkOutInput.value);


});

checkInInput.addEventListener('input', function () {

    checkOutInput.setAttribute('min', checkInInput.value);
});

checkOutInput.setAttribute('min', today);
checkOutInput.value = today;




checkInUpdateInput.setAttribute('min', today);



checkOutUpdateInput.addEventListener('input', function () {
    checkInUpdateInput.setAttribute('max', checkOutUpdateInput.value);


});

checkInUpdateInput.addEventListener('input', function () {

    checkOutUpdateInput.setAttribute('min', checkInUpdateInput.value);
});

checkOutUpdateInput.setAttribute('min', today);




function hideSuccessMessage() {
    var successMessage = document.getElementById("reservationSuccessMessage");
    successMessage.classList.add('hide-success-message');
}


setTimeout(hideSuccessMessage, 3000);




function hideErrorMessage() {
    var errorMessage = document.getElementById("reservationErrorMessage");
    errorMessage.classList.add('hide-error-message');
}


setTimeout(hideErrorMessage, 3000);



function hideUpdateSuccessMessage() {
    var successMessage = document.getElementById("reservationUpdateSuccessMessage");
    successMessage.classList.add('hide-update-success-message');
}


setTimeout(hideUpdateSuccessMessage, 3000);




function hideUpdateErrorMessage() {
    var errorMessage = document.getElementById("reservationUpdateErrorMessage");
    errorMessage.classList.add('hide-update-error-message');
}


setTimeout(hideUpdateErrorMessage, 3000);






