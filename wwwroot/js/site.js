// Dark / light theme toggle using Bootstrap 5.3 data attribute
(function () {
    const html = document.documentElement;
    const stored = localStorage.getItem('theme');
    if (stored === 'dark' || stored === 'light') {
        html.setAttribute('data-bs-theme', stored);
    }

    document.addEventListener('DOMContentLoaded', function () {
        const btn = document.getElementById('themeToggle');
        if (!btn) return;

        btn.addEventListener('click', function () {
            const current = html.getAttribute('data-bs-theme') === 'dark' ? 'dark' : 'light';
            const next = current === 'dark' ? 'light' : 'dark';
            html.setAttribute('data-bs-theme', next);
            localStorage.setItem('theme', next);
        });
    });
})();
