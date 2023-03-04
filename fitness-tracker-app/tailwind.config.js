/** @type {import('tailwindcss').Config} */
module.exports = {
	content: [
		"./pages/**/*.{js,ts,jsx,tsx}",
		"./components/**/*.{js,ts,jsx,tsx}",
	],
	theme: {
		extend: {
			colors: {
				'custom-light': '#FFFFFF',
				'custom-white': '#EFEFEF',
				'custom-grey': '#FBFBFB',
				'custom-blue': '#2D7CA8',
				
			}
		},
	},
	plugins: [],
}
