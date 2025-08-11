// import { test, expect } from '@playwright/test';

// test('User can calculate blood pressure category', async ({ page }) => {
//     await page.goto('http://localhost:5000');

//     await page.fill('input[name="BP.Systolic"]', '135');
//     await page.fill('input[name="BP.Diastolic"]', '85');
//     await page.click('text=Submit');

//     // Assert expected result is shown
//     await expect(page.locator('text=Pre-High Blood Pressure')).toBeVisible();
// });


import { test, expect } from '@playwright/test';

const BASE_URL = process.env.BASE_URL || 'http://localhost:5000';

test('User can calculate blood pressure category', async ({ page }) => {
    test.setTimeout(60000); // 60 seconds timeout for this test

    // Go to the live or local site
    await page.goto(BASE_URL, { waitUntil: 'domcontentloaded' });

    // Wait for input fields to be visible before interacting
    await page.waitForSelector('input[name="BP.Systolic"]', { timeout: 20000 });
    await page.waitForSelector('input[name="BP.Diastolic"]', { timeout: 20000 });

    await page.fill('input[name="BP.Systolic"]', '135');
    await page.fill('input[name="BP.Diastolic"]', '85');
    await page.click('text=Submit');

    // Assert expected result is shown
    await expect(page.locator('text=Pre-High Blood Pressure')).toBeVisible({ timeout: 10000 });
});