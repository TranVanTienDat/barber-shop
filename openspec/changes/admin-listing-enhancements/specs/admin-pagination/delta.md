# Spec Delta: Admin Pagination

## MODIFIED Requirements

- [x] Admin Booking, Service, and Customer pages MUST display data paginated.
  #### Scenario: Changing page size
  When an admin changes the items per page (10, 20, 50), the list MUST reload to show the requested number of items.
  #### Scenario: Navigating pages
  Admin clicks Next Page => show the next slice of data and update the URL `?page=X`.
