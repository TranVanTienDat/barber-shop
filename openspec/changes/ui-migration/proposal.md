## Why

The current admin UI relies heavily on raw Tailwind CSS utility classes and custom component styles, which can lead to inconsistencies, requires high maintenance effort, and lacks built-in interactive components (like robust date pickers). Migrating to the DaisyUI framework (and `cally` for calendars) will provide a standardized, aesthetically pleasing, and highly reusable component library, significantly improving development velocity and UX consistency.

## What Changes

- Integrate DaisyUI as the primary component library for the application.
- Refactor all existing Buttons to use DaisyUI (`btn`, `btn-outline`, `btn-primary`, etc.).
- Refactor all text Inputs and Select dropdowns to use DaisyUI (`input input-bordered`, `select select-bordered`).
- Refactor the Pagination system to use DaisyUI's `join` grouping.
- Introduce the `cally` component (`<calendar-date>`) to handle date inputs natively, styled with DaisyUI.
- **BREAKING**: Replaces standard HTML date inputs and custom Tailwind pagination/buttons inside the Admin Views.

## Capabilities

### New Capabilities

- `ui-components`: Standardized core UI building blocks (Buttons, Inputs, Selects, Pagination).
- `date-picker`: A new calendar-based date selection component powered by `cally`.

### Modified Capabilities

## Impact

- **Configuration**: `tailwind.config.js` is updated to include the DaisyUI plugin. `package.json` includes `daisyui` and `cally`.
- **Views**: All Admin dashboard files (`Views/Admin/Bookings.cshtml`, `Views/Admin/Services.cshtml`, `Views/Admin/Customers.cshtml`, etc.) require structure and class rewrites.
- **Styles**: Custom classes in `wwwroot/css/admin-input.css` may be removed or reduced to favor DaisyUI.
