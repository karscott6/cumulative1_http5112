window.onload = formCheck;

function formCheck() {
    var form = document.getElementById("editform");

    if (form) {
        form.addEventListener("submit", formValidation);
    } else {
        console.error("No edit form found. Script will not run.");
    }
};

function formValidation() {
    // Get FORM ELEMENTS
    var tfn = document.getElementById("TeacherFName");
    var tln = document.getElementById("TeacherLName");
    var ts = document.getElementById("Salary");
    var ten = document.getElementById("EmployeeNumber");

    //Get Values

    var fNameValue = tfn.value;
    var lNameValue = tln.value;
    var salaryValue = ts.value;
    var empNumValue = ten.value;

    //CHECK IF EMPTY
    if (fNameValue.trim() === "") {
        alert("First Name field is required.");
        tfn.style.border = "1px solid red";
        return false;
    } else if (lNameValue.trim() === "") {
        alert("Last Name field is required..");
        tln.style.border = "1px solid red";
        return false;
     } else if (salaryValue.trim() === "") {
        alert("Salary field is required.");
    ts.style.border = "1px solid red";
    return false;
} else if  (empNumValue.trim() === "") {
        alert("Employee Number field is required.");
    ten.style.border = "1px solid red";
    return false;
}   
    // if validation passes, submit true
    return true;
}