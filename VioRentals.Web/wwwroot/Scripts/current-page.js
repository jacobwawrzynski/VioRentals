// Get all the <a> elements on the page
const links = document.querySelectorAll('a');

// Add a click event listener to each <a> element
links.forEach(link => {
    link.addEventListener('click', function (event) {
        // Remove the 'text-blue-600' class from all links
        links.forEach(link => {
            link.classList.remove('text-blue-600');
            link.classList.add('text-gray-500');
        });

        // Add the 'text-blue-600' class to the clicked link
        this.classList.add('text-blue-600');
        this.classList.remove('text-gray-500');
    });
});

// Check the current URL and apply the appropriate styling
const currentURL = window.location.href;
links.forEach(link => {
    if (link.href === currentURL) {
        link.classList.add('text-blue-600');
        link.classList.remove('text-gray-500');
    }
});