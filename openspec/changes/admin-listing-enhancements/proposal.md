# Proposal: Admin Listing Enhancements

## 1. Why are we doing this?

Currently, the Admin modules (Booking, Service, Customer) lack pagination, searching, and filtering. This makes it difficult to manage a large amount of data (like the new 30 mock records per module). We need to enhance the listing UI and backend logic to improve usability and performance.

## 2. What changes are we making?

- Add pagination (10, 20, 50 items per page) across all admin modules (Bookings, Services, Customers).
- Implement search functionality (by name, phone, email, etc.) for all admin modules.
- Add specific filters to the Booking Management module (Filter by Time, Status).
- Add specific filters to the Service Management module (Filter by Price Range).
- Update the admin UI to integrate these controls seamlessly using Tailwind CSS.

## 3. Capabilities

- `admin-pagination`: Support jumping between pages and changing page size.
- `admin-search`: Global search text input per module.
- `booking-filters`: Select inputs for Status and Start/End Date ranges.
- `service-filters`: Inputs for Min/Max Price.

## 4. Expected Impact

- Immensely improved data management experience for the Admin.
- Faster loading times (since we won't load all DB rows at once).
- Easily find specific Bookings or Customers.
