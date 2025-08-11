import { test, expect } from '@playwright/test';

const BASE_URL = (process.env.BASE_URL || 'http://localhost:5000').replace(/\/?$/, '/');

test('User can calculate blood pressure category', async ({ page }) => {
    test.setTimeout(60_000);

    await page.goto(BASE_URL, { waitUntil: 'domcontentloaded' });

    await page.waitForSelector('input[name="BP.Systolic"]', { timeout: 20_000 });
    await page.waitForSelector('input[name="BP.Diastolic"]', { timeout: 20_000 });

    await page.fill('input[name="BP.Systolic"]', '135');
    await page.fill('input[name="BP.Diastolic"]', '85');

    // Prefer role-based selector; falls back to text if needed
    const submit = page.getByRole('button', { name: /submit/i });
    if (await submit.count()) {
        await submit.click();
    } else {
        await page.click('text=Submit');
    }

    await expect(page.locator('text=Pre-High Blood Pressure')).toBeVisible({ timeout: 10_000 });
});