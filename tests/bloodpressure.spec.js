import { test, expect } from '@playwright/test';

test('User can calculate blood pressure category', async ({ page }) => {
    await page.goto('http://localhost:5000');

    await page.fill('input[name="BP.Systolic"]', '135');
    await page.fill('input[name="BP.Diastolic"]', '85');
    await page.click('text=Submit');

    // Assert expected result is shown
    await expect(page.locator('text=Pre-High Blood Pressure')).toBeVisible();
});