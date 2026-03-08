## ADDED Requirements

### Requirement: Standardized Buttons

The system SHALL use DaisyUI button classes for all primary, secondary, and utility buttons across the admin interface.

- Primary buttons MUST use `.btn.btn-primary`.
- Outline buttons MUST use `.btn.btn-outline`.
- Action buttons MUST include appropriate DaisyUI modifiers (e.g., `.btn-sm`, `.btn-error`).

#### Scenario: Rendering an admin primary button

- **WHEN** an admin views a page with a primary action (e.g., "Save", "Create")
- **THEN** the button is rendered with the `.btn.btn-primary` classes without redundant utility classes.

### Requirement: Standardized Form Inputs

The system SHALL use DaisyUI input classes for all text fields and select dropdowns in admin forms and filters.

- Text inputs MUST use `.input.input-bordered`.
- Select dropdowns MUST use `.select.select-bordered`.

#### Scenario: Rendering a search filter input

- **WHEN** an admin views a listing page with a search bar
- **THEN** the text input uses `.input.input-bordered` and fully spans its container width if applicable.

### Requirement: Standardized Pagination

The system SHALL use DaisyUI's join component group to format pagination controls.

- The pagination container MUST use the `.join` class.
- Individual page links MUST use `.join-item.btn` classes.
- The active page MUST include a modifier (e.g., `.btn-active` or equivalent styling mapping) to indicate current selection.

#### Scenario: Navigating multiple pages

- **WHEN** a data list exceeds the page size limit
- **THEN** the pagination controls appear grouped as a seamless horizontal list using `.join` and `.join-item`.
