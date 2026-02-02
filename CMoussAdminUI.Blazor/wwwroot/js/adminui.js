// Wait for Blazor to be fully loaded
window.addEventListener('load', function() {
    setTimeout(initializeAdminUI, 100);
});

// Initialize admin UI functionality
function initializeAdminUI() {
    console.log('Initializing Admin UI...');
    
    const sidebar = document.getElementById('sidebar');
    const mainContent = document.getElementById('mainContent');
    const overlay = document.getElementById('overlay');
    const sidebarToggle = document.getElementById('sidebarToggle');

    if (!sidebar || !mainContent || !overlay || !sidebarToggle) {
        console.log('Elements not ready, retrying...');
        setTimeout(initializeAdminUI, 100);
        return;
    }

    console.log('Elements found, attaching events...');

    // Function to toggle sidebar
    function toggleSidebar(e) {
        e.preventDefault();
        e.stopPropagation();
        console.log('Toggle clicked');
        
        if (window.innerWidth >= 768) {
            // Desktop behavior
            sidebar.classList.toggle('collapsed');
            mainContent.classList.toggle('collapsed');
        } else {
            // Mobile behavior
            sidebar.classList.toggle('show');
            overlay.classList.toggle('show');
        }
    }

    // Function to close sidebar
    function closeSidebar() {
        if (window.innerWidth >= 768) {
            // Desktop behavior
            sidebar.classList.remove('collapsed');
            mainContent.classList.remove('collapsed');
        } else {
            // Mobile behavior
            sidebar.classList.remove('show');
            overlay.classList.remove('show');
        }
    }

    // Remove existing event listeners
    const newToggle = sidebarToggle.cloneNode(true);
    sidebarToggle.parentNode.replaceChild(newToggle, sidebarToggle);

    // Add event listener
    newToggle.addEventListener('click', toggleSidebar);
    console.log('Click event attached to hamburger button');

    // Close sidebar when clicking overlay
    overlay.addEventListener('click', closeSidebar);

    // Close sidebar when pressing Escape key
    document.addEventListener('keydown', function(e) {
        if (e.key === 'Escape') {
            closeSidebar();
        }
    });

    // Handle window resize
    window.addEventListener('resize', function() {
        if (window.innerWidth >= 768) {
            // Desktop view - remove mobile classes
            sidebar.classList.remove('show');
            overlay.classList.remove('show');
        } else {
            // Mobile view - remove desktop classes
            sidebar.classList.remove('collapsed');
            mainContent.classList.remove('collapsed');
        }
    });

    // Close sidebar when clicking on navigation links (mobile only)
    const navLinks = document.querySelectorAll('.sidebar .nav-link');
    navLinks.forEach(link => {
        link.addEventListener('click', function() {
            if (window.innerWidth < 768) {
                closeSidebar();
            }
        });
    });
}
