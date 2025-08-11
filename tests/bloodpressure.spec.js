// import { test, expect } from '@playwright/test';

// test('User can calculate blood pressure category', async ({ page }) => {
//     await page.goto('http://localhost:5000');

//     await page.fill('input[name="BP.Systolic"]', '135');
//     await page.fill('input[name="BP.Diastolic"]', '85');
//     await page.click('text=Submit');

//     // Assert expected result is shown
//     await expect(page.locator('text=Pre-High Blood Pressure')).toBeVisible();
// });


// import { test, expect } from '@playwright/test';

// // Use environment variable if available, fallback to local
// const BASE_URL = process.env.BASE_URL || 'http://localhost:5000';

// test('User can calculate blood pressure category', async ({ page }) => {
//     await page.goto(BASE_URL);

//     await page.fill('input[name="BP.Systolic"]', '135');
//     await page.fill('input[name="BP.Diastolic"]', '85');
//     await page.click('text=Submit');

//     // Assert expected result is shown
//     await expect(page.locator('text=Pre-High Blood Pressure')).toBeVisible();
// });


// import { test, expect } from '@playwright/test';

// const baseUrl = process.env.BASE_URL || 'http://localhost:5000';
// await page.goto(baseUrl);

// test('User can calculate blood pressure category', async ({ page }) => {
//     await page.goto(baseURL);

//     await page.fill('input[name="BP.Systolic"]', '135');
//     await page.fill('input[name="BP.Diastolic"]', '85');
//     await page.click('text=Submit');

//     await expect(page.locator('text=Pre-High Blood Pressure')).toBeVisible();
// });


const { test, expect } = require('@playwright/test');
const baseUrl = process.env.BASE_URL || 'http://localhost:5000';

test('User can calculate blood pressure category', async ({ page }) => {
    await page.goto(baseUrl);

    await page.fill('input[name="BP.Systolic"]', '135');
    await page.fill('input[name="BP.Diastolic"]', '85');
    await page.click('text=Submit');

    await expect(page.locator('text=Pre-High Blood Pressure')).toBeVisible();
});