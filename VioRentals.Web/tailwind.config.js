/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Views/**/*.{cshtml,js}',
        'node_modules/preline/dist/*.js',
    ],
    theme: {
        extend: {},
    },
    plugins: [require("preline/plugin")],
}