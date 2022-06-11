// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function updateWarning(text, show){
    const warningE = document.getElementById("warning");

    if (show) {
        warningE.style.display = "block";
        warningE.textContent = text;
        document.getElementById("createBtn").disabled = true;
        document.getElementById("createBtn").style.background = "#1b4d3e";
    }
    else {
        warningE.style.display = "none";
        document.getElementById("createBtn").disabled = false;
    }
}

function computePrice(event) {


    const checkInDate = new Date(checkInInput.value);
    const checkOutDate = new Date(checkOutInput.value);
    const diffTime = Math.abs(checkOutDate - checkInDate);
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    console.log(diffDays);
    const inputPriceElement = document.getElementById("Price");
    const pricePerNight = document.getElementById("PricePerNight").value;
    const total = parseInt(diffDays) * parseInt(pricePerNight);
    inputPriceElement.value = total.toString();
    console.log(total);

    const startDatesString = document.getElementById("DatesStart").value;
    const endDatesString = document.getElementById("DatesEnd").value;

    const startDates = startDatesString.split(',').map(function (item) {
        return parseInt(item);
    });
    const endDates = endDatesString.split(',').map(function (item) {
        return parseInt(item);
    });

    var checkInDateMillis = checkInDate.getTime();
    console.log("miliin", checkInDateMillis);
    var checkOutDateMillis = checkOutDate.getTime();
    var valid = true;
    if (checkOutDateMillis < checkInDateMillis) {
        updateWarning("Date is  invalid", true);
        return;
    }
    for (var i = 0; i < startDates.length; i++) {
        var bookBeforeAll = checkInDateMillis < startDates[i] && checkOutDateMillis < startDates[i];
        console.log('before:',bookBeforeAll);
        var bookAfterAll = checkInDateMillis > endDates[i] && checkOutDateMillis > endDates[i];
        console.log('after:',bookAfterAll);
        if (!bookBeforeAll && !bookAfterAll) {
            valid = false;
            break;
        }
    }
    updateWarning("Date is not available", !valid);
    console.log(valid);
    console.log(startDates);
    console.log(endDates);

}
const checkOutInput = document.getElementById("CheckOutDate");
const checkInInput = document.getElementById("CheckInDate");
checkOutInput.addEventListener("change", computePrice);
checkInInput.addEventListener("change", computePrice);


