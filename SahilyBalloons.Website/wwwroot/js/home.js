// ============================================================
// Sahily Balloons – Homepage Interactions
// Scroll animations + Floating help button
// ============================================================

(function () {
    'use strict';

    // ----------------------------------------------------------
    // 1. Scroll fade-in using IntersectionObserver
    // ----------------------------------------------------------
    function initScrollAnimations() {
        var elements = document.querySelectorAll('.fade-in-on-scroll');
        if (!elements.length) return;

        // Respect prefers-reduced-motion
        var prefersReducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;
        if (prefersReducedMotion) {
            elements.forEach(function (el) {
                el.classList.add('visible');
            });
            return;
        }

        if (!('IntersectionObserver' in window)) {
            elements.forEach(function (el) {
                el.classList.add('visible');
            });
            return;
        }

        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add('visible');
                    observer.unobserve(entry.target);
                }
            });
        }, {
            threshold: 0.1,
            rootMargin: '0px 0px -60px 0px'
        });

        elements.forEach(function (el) {
            observer.observe(el);
        });
    }

    // ----------------------------------------------------------
    // 2. Floating Help Button + Modal
    // ----------------------------------------------------------
    function initHelpFab() {
        var fab = document.getElementById('help-fab');
        var modal = document.getElementById('help-modal');
        var closeBtn = document.getElementById('help-modal-close');

        if (!fab || !modal || !closeBtn) return;

        function openModal() {
            modal.classList.add('open');
            closeBtn.focus();
            document.body.style.overflow = 'hidden';
            if (window.announceToScreenReader) {
                window.announceToScreenReader('Help options opened.');
            }
        }

        function closeModal() {
            modal.classList.remove('open');
            document.body.style.overflow = '';
            fab.focus();
            if (window.announceToScreenReader) {
                window.announceToScreenReader('Help options closed.');
            }
        }

        fab.addEventListener('click', openModal);
        closeBtn.addEventListener('click', closeModal);

        // Close on overlay click
        modal.addEventListener('click', function (e) {
            if (e.target === modal) {
                closeModal();
            }
        });

        // Close on Escape
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape' && modal.classList.contains('open')) {
                closeModal();
            }
        });

        // Simple focus trap within modal
        modal.addEventListener('keydown', function (e) {
            if (e.key !== 'Tab') return;
            var focusable = modal.querySelectorAll('a[href], button, [tabindex]:not([tabindex="-1"])');
            if (!focusable.length) return;
            var first = focusable[0];
            var last = focusable[focusable.length - 1];

            if (e.shiftKey) {
                if (document.activeElement === first) {
                    e.preventDefault();
                    last.focus();
                }
            } else {
                if (document.activeElement === last) {
                    e.preventDefault();
                    first.focus();
                }
            }
        });
    }

    // ----------------------------------------------------------
    // 3. Initialize
    // ----------------------------------------------------------
    function init() {
        initScrollAnimations();
        initHelpFab();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
