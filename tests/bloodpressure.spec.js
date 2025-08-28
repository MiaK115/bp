// tests/bloodpressure.spec.js
import { test, expect } from '@playwright/test';

const BASE_URL = process.env.BASE_URL || 'http://localhost:5000';

async function waitForAppReady(page) {
    await page.goto(BASE_URL, { waitUntil: 'domcontentloaded' });

    // Wait until Render splash is gone
    await page.waitForFunction(
        () => !/Render - Application loading/i.test(document.title || ''),
        { timeout: 60_000 }
    );

    // Let client-side render settle
    await page.waitForLoadState('networkidle', { timeout: 30_000 });

    // ✅ Wait for a specific thing that we know exists on the real page:
    // your labels or inputs (no regex array / no multi-match text).
    const systolicLabel = page.getByLabel(/systolic/i);
    const diastolicLabel = page.getByLabel(/diastolic/i);

    // If labels aren’t wired up, fall back to inputs by name/id/placeholder
    const systolicInput = systolicLabel.or(
        page.locator('input#BP_Systolic, input[name="BP.Systolic"], input[name="BP_Systolic"]')
    ).or(page.getByPlaceholder(/systolic/i));

    const diastolicInput = diastolicLabel.or(
        page.locator('input#BP_Diastolic, input[name="BP.Diastolic"], input[name="BP_Diastolic"]')
    ).or(page.getByPlaceholder(/diastolic/i));

    await expect(systolicInput).toBeVisible({ timeout: 60_000 });
    await expect(diastolicInput).toBeVisible({ timeout: 60_000 });

    return { systolicInput, diastolicInput };
}

async function attachDebug(page, tag) {
    try {
        await test.info().attach('page-title.txt', { body: await page.title(), contentType: 'text/plain' });
        await test.info().attach('page-url.txt', { body: page.url(), contentType: 'text/plain' });
        await test.info().attach('page.html', { body: await page.content(), contentType: 'text/html' });
        const p = `test-results/${tag}-screenshot.png`;
        await page.screenshot({ path: p, fullPage: true });
        await test.info().attach(`${tag}-screenshot.png`, { path: p, contentType: 'image/png' });
    } catch { /* best effort */ }
}

test.describe('Blood Pressure Calculator', () => {
    test.setTimeout(120_000);

    test('User can calculate blood pressure category', async ({ page }) => {
        try {
            const { systolicInput, diastolicInput } = await waitForAppReady(page);

            await systolicInput.fill('135');
            await diastolicInput.fill('85');

            const submitBtn = page
                .getByRole('button', { name: /submit|calculate|compute|check/i })
                .or(page.locator('button[type="submit"], input[type="submit"]'));

            await expect(submitBtn).toBeVisible({ timeout: 20_000 });
            await submitBtn.click();

            await expect(
                page.getByText(/pre[-\s]?high|elevated|stage\s*1|normal|hypertension/i)
            ).toBeVisible({ timeout: 20_000 });
        } catch (e) {
            await attachDebug(page, 'calc-failure');
            throw e;
        }
    });

    test('Form renders with both fields', async ({ page }) => {
        try {
            const { systolicInput, diastolicInput } = await waitForAppReady(page);
            await expect(systolicInput).toBeVisible({ timeout: 45_000 });
            await expect(diastolicInput).toBeVisible({ timeout: 45_000 });
        } catch (e) {
            await attachDebug(page, 'render-failure');
            throw e;
        }
    });
});