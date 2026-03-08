## Context

The current Admin UI relies entirely on raw Tailwind CSS utility classes and ad-hoc custom classes written directly on HTML elements. This approach, while initially fast, has led to duplicated classes, inconsistent styling for fundamental UI components (buttons, text inputs, pagination, selects), and lack of accessible, full-featured components like date pickers. The goal is to migrate to DaisyUI to enforce standard component patterns and use the `cally` web component for date selection.

## Goals / Non-Goals

**Goals:**

- Replace raw Tailwind CSS buttons, inputs, selects, and pagination elements with DaisyUI component classes (`btn`, `input`, `select`, `join`).
- Integrate the `cally` web component inside HTML forms, effectively replacing standard `<input type="date">` elements with a highly interactive, customizable calendar picker mapped via DaisyUI.
- Ensure the UI remains fully responsive, maintaining the currently established layout structure (headers, grid forms, etc.).
- Remove redundant, hardcoded utility styling specific to basic components from `wwwroot/css/admin-input.css`.

**Non-Goals:**

- Restructuring the overarching layout structure (Sidebar, Navbar, main content wrappers).
- Changing backend C# logic around Pagination, Search, or general Filtering algorithms.
- Updating Client-facing Front-end pages (only Admin Views are targeted).

## Decisions

- **DaisyUI Integration**: Chosen because it provides semantic component classes (`.btn`, `.input`) that compile natively via Tailwind, preserving output CSS efficiency while significantly reducing HTML boilerplate.
- **`cally` for Calendars**: Standard HTML `type="date"` lacks cross-browser visual consistency and styling capabilities. `cally` provides a lightweight, framework-agnostic Web Component calendar that seamlessly aligns with DaisyUI's design language by applying custom CSS variables and utility classes.
- **Component Replacements**:
  - `<a>`/`<button>` tags $\to$ `btn`, `btn-primary`, `btn-outline`
  - Text `<input>` tags $\to$ `input input-bordered w-full`
  - `<select>` tags $\to$ `select select-bordered w-full`
  - Pagination Container $\to$ `join`, Pagination Links $\to$ `join-item btn`
- **Tailwind Config**: DaisyUI plugin is injected into `tailwind.config.js`. Cally is installed via npm but will be imported via script CDN or direct module loading on the Razor views (or Layout) to keep setup straightforward.

## Risks / Trade-offs

- **[Risk] DaisyUI conflicts with existing custom Tailwind utilities**
  - _Mitigation_: Existing custom buttons may double-apply styles. We will systematically remove long strings of custom Tailwind utility classes (`px-4`, `py-2`, `bg-blue-500`, etc.) when replacing them with semantic `btn` classes.
- **[Risk] `cally` component requires JavaScript to render the calendar dropdown, potentially causing hydration/Flash of Unstyled Content (FOUC) issues.**
  - _Mitigation_: Ensure the `cally` script is loaded efficiently in the `<head>` or at the end of the layout `_AdminLayout.cshtml` using a `<script type="module">` tag.
- **[Risk] CSS specificity issues with remaining custom CSS in `admin-input.css`.**
  - _Mitigation_: Only keep custom CSS for complex structural layouts not handled by DaisyUI. Remove any overriding rules for basic inputs/buttons in `admin-input.css`.
