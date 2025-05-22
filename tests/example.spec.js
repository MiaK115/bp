// tests/homepage.spec.js
const { test, expect } = require('@playwright/test');

test('homepage loads and shows welcome message', async ({ page }) => {
  await page.goto('http://localhost:5000'); // adjust if your port is different
  await expect(page).toHaveTitle(/BPCalculator/i); // change depending on your app's title
});