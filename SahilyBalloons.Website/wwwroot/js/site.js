// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// ============================================================
// ADA / WCAG 2.2 Level AA Accessibility Helpers
// ============================================================

(function () {
    'use strict';

    // ----------------------------------------------------------
    // 1. ARIA Live Region for screen-reader announcements
    // ----------------------------------------------------------
    var liveRegion = document.createElement('div');
    liveRegion.setAttribute('role', 'status');
    liveRegion.setAttribute('aria-live', 'polite');
    liveRegion.setAttribute('aria-atomic', 'true');
    liveRegion.classList.add('sr-only');
    document.body.appendChild(liveRegion);

    window.announceToScreenReader = function (message) {
        liveRegion.textContent = '';
        setTimeout(function () { liveRegion.textContent = message; }, 100);
    };

    // ----------------------------------------------------------
    // 2. Cookie Banner – Focus Trap, Inert & Keyboard (2.4.11)
    //    Uses aria-modal="true" + inert to prevent focus from
    //    reaching content obscured behind the dialog.
    // ----------------------------------------------------------
    function initCookieBanner() {
        var banner = document.getElementById('sb-cookie-banner');
        var btn = document.getElementById('sb-cookie-banner-btn');
        if (!banner || !btn) return;

        // Elements outside the banner that should become inert
        var mainContent = document.querySelector('main');
        var header = document.querySelector('header');
        var footer = document.querySelector('footer');

        function setPageInert(inert) {
            [mainContent, header, footer].forEach(function (el) {
                if (el) {
                    if (inert) {
                        el.setAttribute('inert', '');
                        el.setAttribute('aria-hidden', 'true');
                    } else {
                        el.removeAttribute('inert');
                        el.removeAttribute('aria-hidden');
                    }
                }
            });
        }

        // When banner is shown, move focus to it and make page inert
        var observer = new MutationObserver(function (mutations) {
            mutations.forEach(function (m) {
                if (m.attributeName === 'style') {
                    if (banner.style.display === 'block') {
                        setPageInert(true);
                        btn.focus();
                    } else {
                        setPageInert(false);
                    }
                }
            });
        });
        observer.observe(banner, { attributes: true, attributeFilter: ['style'] });

        // Trap Tab inside the banner while it is visible
        banner.addEventListener('keydown', function (e) {
            if (banner.style.display !== 'block') return;

            // Escape key dismisses the banner
            if (e.key === 'Escape') {
                e.preventDefault();
                btn.click();
                return;
            }

            // Trap focus: get all focusable elements in banner
            var focusable = banner.querySelectorAll('a[href], button, [tabindex]:not([tabindex="-1"])');
            if (focusable.length === 0) return;

            var first = focusable[0];
            var last = focusable[focusable.length - 1];

            if (e.key === 'Tab') {
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
            }
        });

        // Announce dismissal to screen readers & restore page
        btn.addEventListener('click', function () {
            setPageInert(false);
            window.announceToScreenReader('Cookie consent accepted. Banner dismissed.');
        });
    }

    // ----------------------------------------------------------
    // 3. Mark current page's nav link with aria-current
    // ----------------------------------------------------------
    function markCurrentNavLink() {
        var path = window.location.pathname.replace(/\/$/, '').toLowerCase();
        var links = document.querySelectorAll('.navbar-nav .nav-link');
        links.forEach(function (link) {
            var href = (link.getAttribute('href') || '').replace(/\/$/, '').toLowerCase();
            if (href === path || (path === '' && href === '/')) {
                link.setAttribute('aria-current', 'page');
            }
        });
    }

    // ----------------------------------------------------------
    // 4. Add aria-label to external links missing one
    // ----------------------------------------------------------
    function labelExternalLinks() {
        document.querySelectorAll('a[target="_blank"]').forEach(function (link) {
            if (!link.getAttribute('aria-label')) {
                var text = (link.textContent || '').trim();
                if (text) {
                    link.setAttribute('aria-label', text + ' (opens in new tab)');
                }
            }
        });
    }

    // ----------------------------------------------------------
    // 5. Ensure embedded iframes have accessible titles
    // ----------------------------------------------------------
    function ensureIframeTitles() {
        function applyIframeAttributes(frame) {
            if (!frame || !frame.tagName || frame.tagName.toLowerCase() !== 'iframe') return;

            var src = (frame.getAttribute('src') || '').toLowerCase();
            if (!frame.getAttribute('title')) {
                if (src.indexOf('instagram.com') !== -1) {
                    frame.setAttribute('title', 'Instagram post');
                } else if (src.indexOf('youtube.com') !== -1 || src.indexOf('youtu.be') !== -1) {
                    frame.setAttribute('title', 'Embedded video');
                } else if (src.indexOf('maps.google') !== -1) {
                    frame.setAttribute('title', 'Map');
                } else {
                    frame.setAttribute('title', 'Embedded content');
                }
            }

            if (!frame.getAttribute('loading')) {
                frame.setAttribute('loading', 'lazy');
            }
        }

        document.querySelectorAll('iframe').forEach(applyIframeAttributes);

        if (window.MutationObserver) {
            var iframeObserver = new MutationObserver(function (mutations) {
                mutations.forEach(function (mutation) {
                    if (!mutation.addedNodes) return;
                    mutation.addedNodes.forEach(function (node) {
                        if (!node || node.nodeType !== 1) return;
                        if (node.tagName && node.tagName.toLowerCase() === 'iframe') {
                            applyIframeAttributes(node);
                        }
                        if (node.querySelectorAll) {
                            node.querySelectorAll('iframe').forEach(applyIframeAttributes);
                        }
                    });
                });
            });
            iframeObserver.observe(document.documentElement || document.body, { childList: true, subtree: true });
        }
    }

    // ----------------------------------------------------------
    // 5. Focus Not Obscured (2.4.11) — DISABLED
    //    Auto-scroll on focus was causing unwanted page scrolling.
    //    Re-enable by uncommenting initFocusNotObscured() below.
    // ----------------------------------------------------------
    // function initFocusNotObscured() {
    //     document.addEventListener('focusin', function (e) {
    //         var el = e.target;
    //         if (!el || !el.getBoundingClientRect) return;
    //
    //         var rect = el.getBoundingClientRect();
    //         var viewH = window.innerHeight || document.documentElement.clientHeight;
    //
    //         // Check if element is obscured by top navbar (~70px)
    //         // or bottom cookie banner (~60px when visible)
    //         var banner = document.getElementById('sb-cookie-banner');
    //         var bannerHeight = (banner && banner.style.display === 'block') ? banner.offsetHeight : 0;
    //         var navbarHeight = 70;
    //
    //         if (rect.top < navbarHeight || rect.bottom > viewH - bannerHeight) {
    //             el.scrollIntoView({ block: 'center', behavior: 'smooth' });
    //         }
    //     }, true);
    // }

    // ----------------------------------------------------------
    // 6. Initialize on DOM ready
    // ----------------------------------------------------------
    function init() {
        initCookieBanner();
        markCurrentNavLink();
        labelExternalLinks();
        ensureIframeTitles();
        // initFocusNotObscured(); // Disabled
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }

})();