// ============================================================
// Sahily Balloons – Apple-style Navbar Interactions
// Dropdown menus, scroll show/hide, homepage opacity
// ============================================================

(function () {
    'use strict';

    var nav = document.getElementById('apple-nav');
    var dropdown = document.getElementById('apple-nav-dropdown');
    var scrim = document.getElementById('apple-nav-scrim');
    if (!nav) return;

    var isHome = document.body.classList.contains('page-home');
    var hoverTimer = null;
    var leaveTimer = null;
    var HOVER_DELAY = 80;
    var LEAVE_DELAY = 200;

    // ----------------------------------------------------------
    // 1. Dropdown menus
    // ----------------------------------------------------------
    function showPanel(panelId) {
        if (!dropdown) return;
        clearTimeout(leaveTimer);

        // Deactivate all panels, activate the target
        var panels = dropdown.querySelectorAll('.apple-dropdown-panel');
        for (var i = 0; i < panels.length; i++) {
            panels[i].classList.toggle('active', panels[i].getAttribute('data-panel') === panelId);
        }

        dropdown.classList.add('open');
        dropdown.setAttribute('aria-hidden', 'false');
        nav.classList.add('apple-nav--dropdown-open');
        if (scrim) scrim.classList.add('visible');
    }

    function hideDropdown(immediate) {
        clearTimeout(hoverTimer);
        var delay = immediate ? 0 : LEAVE_DELAY;
        leaveTimer = setTimeout(function () {
            if (!dropdown) return;
            dropdown.classList.remove('open');
            dropdown.setAttribute('aria-hidden', 'true');
            nav.classList.remove('apple-nav--dropdown-open');
            if (scrim) scrim.classList.remove('visible');

            // After transition remove active class
            setTimeout(function () {
                var panels = dropdown.querySelectorAll('.apple-dropdown-panel');
                for (var i = 0; i < panels.length; i++) {
                    panels[i].classList.remove('active');
                }
            }, 350);
        }, delay);
    }

    // Attach hover to trigger links
    var triggers = nav.querySelectorAll('[data-nav-dropdown]');
    for (var t = 0; t < triggers.length; t++) {
        (function (trigger) {
            trigger.addEventListener('mouseenter', function () {
                clearTimeout(leaveTimer);
                var id = this.getAttribute('data-nav-dropdown');
                clearTimeout(hoverTimer);
                hoverTimer = setTimeout(function () { showPanel(id); }, HOVER_DELAY);
            });
        })(triggers[t]);
    }

    // Keep dropdown open when hovering over it
    if (dropdown) {
        dropdown.addEventListener('mouseenter', function () {
            clearTimeout(leaveTimer);
            clearTimeout(hoverTimer);
        });
        dropdown.addEventListener('mouseleave', function () { hideDropdown(false); });
    }

    // Hide when mouse leaves nav bar area
    var navBar = nav.querySelector('.apple-nav-inner');
    if (navBar) {
        navBar.addEventListener('mouseleave', function () { hideDropdown(false); });
    }

    // Scrim click closes dropdown
    if (scrim) {
        scrim.addEventListener('click', function () { hideDropdown(true); });
    }

    // Escape closes dropdown
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape' && dropdown && dropdown.classList.contains('open')) {
            hideDropdown(true);
        }
    });

    // ----------------------------------------------------------
    // 2. Scroll behaviour
    // ----------------------------------------------------------
    var lastScrollY = window.scrollY || window.pageYOffset || 0;
    var scrollTicking = false;

    // Homepage: white cover wipes upward over REVEAL_RANGE px
    // At scroll 0   → navbar fully covered in white (video invisible)
    // At scroll 120 → cover fully gone, full frosted glass
    // The frosted glass ramps up IN PARALLEL with the wipe so
    // there's no abrupt change at the end.
    var REVEAL_RANGE = 120;

    function applyHomeNavReveal(scrollY) {
        // t: 0 (top, fully covered) → 1 (fully revealed)
        var t = Math.min(Math.max(scrollY / REVEAL_RANGE, 0), 1);

        // Set the CSS custom property that controls ::before height
        nav.style.setProperty('--nav-cover', ((1 - t) * 100) + '%');

        // Frosted glass at full strength the entire time —
        // the white cover is the only thing that changes.
        nav.style.background = 'rgba(255, 255, 255, 0.72)';
        nav.style.backdropFilter = 'saturate(180%) blur(20px)';
        nav.style.webkitBackdropFilter = 'saturate(180%) blur(20px)';
    }

    function onScroll() {
        var scrollY = window.scrollY || window.pageYOffset;

        if (isHome) {
            applyHomeNavReveal(scrollY);
        }
        // Non-home pages: navbar scrolls naturally with content (no auto-hide)

        lastScrollY = scrollY;
        scrollTicking = false;
    }

    window.addEventListener('scroll', function () {
        if (!scrollTicking) {
            window.requestAnimationFrame(onScroll);
            scrollTicking = true;
        }
    }, { passive: true });

    // Set initial state
    if (isHome) {
        applyHomeNavReveal(0);
    }
})();
