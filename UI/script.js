function loadHeader() {
    fetch('header.html')
      .then(response => response.text())
      .then(data => {
        document.getElementById('header-placeholder').innerHTML = data;
        // Highlight the current page in navigation
        const currentPage = window.location.pathname.split('/').pop();
        const navLinks = document.querySelectorAll('nav a');
        navLinks.forEach(link => {
          const href = link.getAttribute('href');
          if (href === currentPage || (href === '#' && currentPage === 'index.html')) {
            link.classList.add('selected');
          } else {
            link.classList.remove('selected');
          }
        });
      })
      .catch(error => console.error('Error loading header:', error));
  }
  
  function loadFooter() {
    fetch('footer.html')
      .then(response => response.text())
      .then(data => {
        document.getElementById('footer-placeholder').innerHTML = data;
      })
      .catch(error => console.error('Error loading footer:', error));
  }
  
  window.onload = function() {
    loadHeader();
    loadFooter();
  };