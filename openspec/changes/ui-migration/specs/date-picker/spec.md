## ADDED Requirements

### Requirement: Calendar Web Component Integration

The system SHALL use the `cally` Web Component (`<calendar-date>`) to handle all date-picking functionalities within the admin interface, replacing the native HTML `<input type="date">`.

#### Scenario: Displaying a date filter

- **WHEN** an admin needs to filter records by date (e.g., in Bookings)
- **THEN** the interface provides a `<calendar-date>` component instead of a standard date input.

### Requirement: Styled Calendar UI

The `<calendar-date>` component SHALL be styled via CSS variables to match the DaisyUI base theme elements (using `bg-base-100`, `border-base-300`, `rounded-box`, etc.).

#### Scenario: Interacting with the calendar dropdown

- **WHEN** the calendar is rendered
- **THEN** its background, border, and active selection state visually blend with other DaisyUI components on the page.
