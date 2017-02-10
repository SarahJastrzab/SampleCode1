
function validateForm() {
    var firstName = document.getElementById('ClientFirstName').value;
    var lastName = document.getElementById('ClientLastName').value;
    var phone = document.getElementById('ClientPhone').value;
    var zip = document.getE3lementById('Zip').value;
    var streetNum = document.getElementById('StreetNumber').value;
    var street = document.getElementById('StreetName').value;
    var building = document.getElementById('Building').value;
    var aptNum = document.getElementById('AptNumber').value;

    if (firstName == ""  || phone == "" || zip == "") {
        alert("A required field is missing");
        return false;
    } else if (/[^a-zA-Z]/.test(firstName) || /[^a-zA-Z]/.test(lastName) || /[^0-9]/.test(phone) || /[^0-9]/.test(zip) || /[^0-9]/.test(streetNum) || /[^a-zA-Z0-9]/.test(street) || /[^a-zA-Z0-9]/.test(building) || /[^a-zA-Z0-9]/.test(aptNum)) {
        alert('Please check your inputs');
        return false;
    } else {
        return true;
    }

    return true;


}

function validatePizzaForm() {
    var num = document.getElementById('NumOrdered').value;

    if (num == "") {
        alert("A required field is missing");
        return false;
    } else if ( /[^0-9]/.test(num)) {
        alert('Please check your inputs');
        return false;
    } else {
        return true;
    }

}