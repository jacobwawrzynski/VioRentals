/** @type {import('tailwindcss').Config} */
module.exports = {
	content: ['./VioRentals.Web/**/*.cshtml', 'node_modules/preline/dist/*.js'],
	theme: {
		extend: {},
	},
	plugins: [require('preline/plugin')],
}
