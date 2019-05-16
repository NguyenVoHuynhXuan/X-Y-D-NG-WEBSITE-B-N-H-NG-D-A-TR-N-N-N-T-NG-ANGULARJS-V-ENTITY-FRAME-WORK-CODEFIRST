
function beginPaging(args) {
    // Animate
    $('#grid-list').fadeOut('normal');
}

function successPaging() {
    // Animate
    $('#grid-list').fadeIn('normal');
    $('a').tooltip();
}

function failurePaging() {
    alert("Không có dữ liệu.");
}
