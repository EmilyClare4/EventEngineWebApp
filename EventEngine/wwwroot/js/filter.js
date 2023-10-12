document.addEventListener("DOMContentLoaded", function () {
    var filterToggle = document.getElementById("filter-toggle");
    var filterSection = document.querySelector(".filter-section");

    filterToggle.addEventListener("click", function () {
        filterSection.classList.toggle("active");
    });
});

function clearFilters() {
    document.querySelector('input[name="Search"]').value = ''; // Clear the search input
    document.querySelector('input[name="IsIndoor"]').checked = false; // Uncheck the Indoor Events checkbox
    document.querySelector('select[name="categoryFilter"]').value = ''; // Clear the Category filter
    document.querySelector('select[name="sortCriteria"]').value = 'Title'; // Reset sorting to default
    document.querySelector('form').submit(); // Submit the form to reload the page
}
