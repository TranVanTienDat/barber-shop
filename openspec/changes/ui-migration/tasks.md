## 1. Environment & Setup

- [x] 1.1 Add `cally` web component script (`<script type="module" src="https://unpkg.com/cally"></script>`) to `Views/Shared/_AdminLayout.cshtml` so that it is available across all admin pages.
- [x] 1.2 Verify `daisyui` and `cally` build successfully with the Tailwind CSS watcher (`npm run watch:css`).

## 2. Refactor Admin Customers View

- [x] 2.1 Refactor all Buttons and Action Links in `Views/Admin/Customers.cshtml` to use semantic DaisyUI classes (e.g., `btn`, `btn-primary`, `btn-outline`, `btn-sm`).
- [x] 2.2 Update the Search and Page Size Filter Inputs to use DaisyUI classes (`input input-bordered`, `select select-bordered`).
- [x] 2.3 Refactor the Pagination controls at the bottom of `Customers.cshtml` to use the DaisyUI `join` group (`join`, `join-item btn`).

## 3. Refactor Admin Services View

- [x] 3.1 Refactor all Buttons and Action Links in `Views/Admin/Services.cshtml` to use semantic DaisyUI classes.
- [x] 3.2 Update the Search, Min/Max Price Inputs, and Page Size Select to use DaisyUI input classes.
- [x] 3.3 Refactor the Pagination controls in `Services.cshtml` to use the DaisyUI `join` group.

## 4. Refactor Admin Bookings View

- [x] 4.1 Refactor all Buttons and Action Links in `Views/Admin/Bookings.cshtml` to use semantic DaisyUI classes.
- [x] 4.2 Update the Search, Status Select, and Page Size Select to use DaisyUI classes.
- [x] 4.3 Replace the native Start Date and End Date `<input type="date">` elements with the `cally` Web Component (`<calendar-date>`) styled gracefully with DaisyUI CSS variables.
- [x] 4.4 Refactor the Pagination controls in `Bookings.cshtml` to use the DaisyUI `join` group.

## 5. CSS Cleanup & Verification

- [x] 5.1 Remove any duplicate or redundant custom utility classes specific to inputs/buttons from `wwwroot/css/admin-input.css` as they are now handled by DaisyUI.
- [x] 5.2 Test across all 3 pages (`/Admin/Customers`, `/Admin/Services`, `/Admin/Bookings`) to ensure responsive layout holds correctly and date/filters work flawlessly.
