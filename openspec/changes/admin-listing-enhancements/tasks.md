# Implementation Tasks

## 1. Pagination Infrastructure

- [x] 1.1 Create a `PagedResult<T>` class (or use a tuple) to pass Items, TotalCount, TotalPages, CurrentPage, and PageSize.
- [x] 1.2 Create a reusable pagination UI partial view (or inline it) for Tailwind CSS.

## 2. Customer Management Enhancements

- [x] 2.1 Update `CustomerController.Index` to accept `search`, `page`, `pageSize` params.
- [x] 2.2 Update EF Core query in `CustomerController` to implement filtering and pagination.
- [x] 2.3 Update `Views/Customer/Index.cshtml` to include Search Bar, PageSize Dropdown, and Pagination Controls.

## 3. Service Management Enhancements

- [x] 3.1 Update `ServiceController.Index` to accept `search`, `minPrice`, `maxPrice`, `page`, `pageSize`.
- [x] 3.2 Update EF query to filter by Price Range and Search text.
- [x] 3.3 Update `Views/Service/Index.cshtml` to add Search, Price Range inputs, and Pagination controls.

## 4. Booking Management Enhancements

- [x] 4.1 Update `BookingController` (Admin Area) to accept `search`, `startDate`, `endDate`, `status`, `page`, `pageSize`.
- [x] 4.2 Update EF query to filter by date range and status.
- [x] 4.3 Update `Views/AdminBooking/Index.cshtml` (or equivalent) to add complex filter forms and Pagination.

## 5. UI/UX Polish

- [x] 5.1 Ensure all forms use `method="get"` and preserve state in URL.
- [ ] 5.2 Test across all 3 modules with the 30+ mock records.
