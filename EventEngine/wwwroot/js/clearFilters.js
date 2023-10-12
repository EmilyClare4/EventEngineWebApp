function clearFilters() {
    document.querySelector('input[name="Search"]').value = ''; // Clear the search input
    document.querySelector('input[name="IsIndoor"]').checked = false; // Uncheck the Indoor Events checkbox
    document.querySelector('select[name="categoryFilter"]').value = ''; // Clear the Category filter
    document.querySelector('select[name="sortCriteria"]').value = 'Title'; // Reset sorting to default
    document.querySelector('form').submit(); // Submit the form to reload the page
}