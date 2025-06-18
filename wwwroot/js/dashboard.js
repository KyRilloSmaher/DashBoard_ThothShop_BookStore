// Dashboard JavaScript
document.addEventListener('DOMContentLoaded', function() {
    // Sidebar toggle functionality
    const sidebar = document.getElementById('sidebar');
    const mainContent = document.getElementById('mainContent');
    const sidebarToggle = document.getElementById('sidebarToggle');
    const sidebarToggleMobile = document.getElementById('sidebarToggleMobile');
    
    // Desktop sidebar toggle
    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', function() {
            sidebar.classList.toggle('collapsed');
            mainContent.classList.toggle('expanded');
        });
    }
    
    // Mobile sidebar toggle
    if (sidebarToggleMobile) {
        sidebarToggleMobile.addEventListener('click', function() {
            sidebar.classList.toggle('show');
        });
    }
    
    // Close sidebar when clicking outside on mobile
    document.addEventListener('click', function(event) {
        if (window.innerWidth <= 768) {
            if (!sidebar.contains(event.target) && !sidebarToggleMobile.contains(event.target)) {
                sidebar.classList.remove('show');
            }
        }
    });
    
    // Search functionality
    const searchInput = document.querySelector('.search-input');
    if (searchInput) {
        searchInput.addEventListener('input', function(e) {
            const searchTerm = e.target.value.toLowerCase();
            // Add search logic here
            console.log('Searching for:', searchTerm);
        });
        
        // Add search suggestions
        searchInput.addEventListener('focus', function() {
            this.parentElement.classList.add('focused');
        });
        
        searchInput.addEventListener('blur', function() {
            this.parentElement.classList.remove('focused');
        });
    }
    
    // Notification badge animation
    const notificationBtn = document.querySelector('.nav-action-btn[title="Notifications"]');
    if (notificationBtn) {
        notificationBtn.addEventListener('click', function() {
            const badge = this.querySelector('.badge');
            if (badge) {
                badge.style.transform = 'scale(0.8)';
                setTimeout(() => {
                    badge.style.transform = 'scale(1)';
                }, 200);
            }
        });
    }
    
    // User menu dropdown
    const userMenuBtn = document.querySelector('.user-menu-btn');
    if (userMenuBtn) {
        userMenuBtn.addEventListener('click', function() {
            // Add dropdown menu functionality
            console.log('User menu clicked');
        });
    }
    
    // Add loading states to buttons
    const buttons = document.querySelectorAll('.btn');
    buttons.forEach(button => {
        button.addEventListener('click', function() {
            if (!this.classList.contains('btn-loading')) {
                this.classList.add('btn-loading');
                const originalText = this.innerHTML;
                this.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Loading...';
                
                // Reset after 2 seconds (replace with actual async operation)
                setTimeout(() => {
                    this.classList.remove('btn-loading');
                    this.innerHTML = originalText;
                }, 2000);
            }
        });
    });
    
    // Table row hover effects
    const tableRows = document.querySelectorAll('.table tbody tr');
    tableRows.forEach(row => {
        row.addEventListener('mouseenter', function() {
            this.style.transform = 'translateX(4px)';
        });
        
        row.addEventListener('mouseleave', function() {
            this.style.transform = 'translateX(0)';
        });
    });
    
    // Stat cards animation on scroll
    const statCards = document.querySelectorAll('.stat-card');
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };
    
    const observer = new IntersectionObserver(function(entries) {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
            }
        });
    }, observerOptions);
    
    statCards.forEach(card => {
        card.style.opacity = '0';
        card.style.transform = 'translateY(20px)';
        card.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
        observer.observe(card);
    });
    
    // Responsive table handling
    const tables = document.querySelectorAll('.table');
    tables.forEach(table => {
        const wrapper = document.createElement('div');
        wrapper.className = 'table-responsive';
        wrapper.style.overflowX = 'auto';
        table.parentNode.insertBefore(wrapper, table);
        wrapper.appendChild(table);
    });
    
    // Add smooth scrolling for anchor links
    const anchorLinks = document.querySelectorAll('a[href^="#"]');
    anchorLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });
    
    // Add keyboard navigation for sidebar
    document.addEventListener('keydown', function(e) {
        if (e.key === 'Escape' && window.innerWidth <= 768) {
            sidebar.classList.remove('show');
        }
    });
    
    // Add tooltip functionality
    const tooltipElements = document.querySelectorAll('[title]');
    tooltipElements.forEach(element => {
        element.addEventListener('mouseenter', function() {
            const tooltip = document.createElement('div');
            tooltip.className = 'custom-tooltip';
            tooltip.textContent = this.getAttribute('title');
            tooltip.style.cssText = `
                position: absolute;
                background: #1e293b;
                color: white;
                padding: 0.5rem 0.75rem;
                border-radius: 0.375rem;
                font-size: 0.75rem;
                z-index: 1000;
                pointer-events: none;
                white-space: nowrap;
                box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1);
            `;
            
            document.body.appendChild(tooltip);
            
            const rect = this.getBoundingClientRect();
            tooltip.style.left = rect.left + (rect.width / 2) - (tooltip.offsetWidth / 2) + 'px';
            tooltip.style.top = rect.top - tooltip.offsetHeight - 8 + 'px';
            
            this.addEventListener('mouseleave', function() {
                if (tooltip.parentNode) {
                    tooltip.parentNode.removeChild(tooltip);
                }
            }, { once: true });
        });
    });
    
    // Add form validation styles
    const forms = document.querySelectorAll('form');
    forms.forEach(form => {
        const inputs = form.querySelectorAll('input, select, textarea');
        inputs.forEach(input => {
            input.addEventListener('blur', function() {
                if (this.hasAttribute('required') && !this.value.trim()) {
                    this.classList.add('is-invalid');
                } else {
                    this.classList.remove('is-invalid');
                    this.classList.add('is-valid');
                }
            });
            
            input.addEventListener('input', function() {
                if (this.classList.contains('is-invalid')) {
                    this.classList.remove('is-invalid');
                }
            });
        });
    });
    
    // Add confirmation dialogs
    const deleteButtons = document.querySelectorAll('.btn-danger');
    deleteButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            if (!confirm('Are you sure you want to delete this item?')) {
                e.preventDefault();
            }
        });
    });
    
    // Add auto-hide alerts
    const alerts = document.querySelectorAll('.alert');
    alerts.forEach(alert => {
        setTimeout(() => {
            alert.style.opacity = '0';
            alert.style.transform = 'translateY(-10px)';
            setTimeout(() => {
                if (alert.parentNode) {
                    alert.parentNode.removeChild(alert);
                }
            }, 300);
        }, 5000);
    });
    
    // Add pagination enhancement
    const paginationLinks = document.querySelectorAll('.pagination .page-link');
    paginationLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            // Add loading state to pagination
            const pagination = this.closest('.pagination');
            pagination.style.opacity = '0.5';
            pagination.style.pointerEvents = 'none';
            
            setTimeout(() => {
                pagination.style.opacity = '1';
                pagination.style.pointerEvents = 'auto';
            }, 500);
        });
    });
    
    // Add data table sorting (basic implementation)
    const sortableHeaders = document.querySelectorAll('.table th[data-sort]');
    sortableHeaders.forEach(header => {
        header.style.cursor = 'pointer';
        header.addEventListener('click', function() {
            const table = this.closest('table');
            const tbody = table.querySelector('tbody');
            const rows = Array.from(tbody.querySelectorAll('tr'));
            const columnIndex = Array.from(this.parentNode.children).indexOf(this);
            const isAscending = this.classList.contains('sort-asc');
            
            // Remove existing sort classes
            this.parentNode.querySelectorAll('th').forEach(th => {
                th.classList.remove('sort-asc', 'sort-desc');
            });
            
            // Add sort class
            this.classList.add(isAscending ? 'sort-desc' : 'sort-asc');
            
            // Sort rows
            rows.sort((a, b) => {
                const aValue = a.children[columnIndex].textContent.trim();
                const bValue = b.children[columnIndex].textContent.trim();
                
                if (isAscending) {
                    return bValue.localeCompare(aValue);
                } else {
                    return aValue.localeCompare(bValue);
                }
            });
            
            // Reorder rows
            rows.forEach(row => tbody.appendChild(row));
        });
    });
    
    // Add responsive image handling
    const images = document.querySelectorAll('img');
    images.forEach(img => {
        img.addEventListener('error', function() {
            this.src = 'https://via.placeholder.com/150x150?text=No+Image';
        });
    });
    
    // Add print styles
    const printBtn = document.querySelector('.btn-print');
    if (printBtn) {
        printBtn.addEventListener('click', function() {
            window.print();
        });
    }
    
    // Add export functionality (basic)
    const exportBtn = document.querySelector('.btn-export');
    if (exportBtn) {
        exportBtn.addEventListener('click', function() {
            const table = document.querySelector('.table');
            if (table) {
                const csv = tableToCSV(table);
                downloadCSV(csv, 'export.csv');
            }
        });
    }
    
    // Utility functions
    function tableToCSV(table) {
        const rows = table.querySelectorAll('tr');
        const csv = [];
        
        rows.forEach(row => {
            const cols = row.querySelectorAll('td, th');
            const rowData = [];
            cols.forEach(col => {
                rowData.push('"' + col.textContent.replace(/"/g, '""') + '"');
            });
            csv.push(rowData.join(','));
        });
        
        return csv.join('\n');
    }
    
    function downloadCSV(csv, filename) {
        const blob = new Blob([csv], { type: 'text/csv' });
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = filename;
        a.click();
        window.URL.revokeObjectURL(url);
    }
    
    // Initialize any additional plugins or components
    console.log('Dashboard initialized successfully');
}); 