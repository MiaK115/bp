// // tests/homepage.spec.js
// const { test, expect } = require('@playwright/test');

// test('homepage loads and shows welcome message', async ({ page }) => {
//   await page.goto('http://localhost:5000');
//   await expect(page).toHaveTitle(/BPCalculator/i);
// });


import { test, expect } from '@playwright/test';

const BASE_URL = process.env.BASE_URL || 'http://localhost:5000';

test('homepage loads and shows welcome message', async ({ page }) => {
  await page.goto(BASE_URL);
  await expect(page).toHaveTitle(/BPCalculator/i);
});