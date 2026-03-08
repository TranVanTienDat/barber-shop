# Design: Admin Listing Enhancements

## 1. Context

The admin modules currently fetch and display all rows from the SQLite database. With 30+ items per module, we need proper pagination, searching, and filtering to make the interface usable.

- Tables to update: `Bookings`, `BarberServices`, `AdminAccounts` (Role = Customer).

## 2. Goals

- Add `Page`, `PageSize` (10, 20, 50), `SearchString` properties to index controllers.
- Add `StartDate`, `EndDate`, `Status` (nullable) to Booking Controller.
- Add `MinPrice`, `MaxPrice` to Service Controller.
- Ensure the UI handles these states in the URL query string (`?page=1&pageSize=10&search=xxx`).

## 3. Decisions

### Search & Filtering Logic (Server-side)

We will implement the filtering and pagination in the EF Core LINQ queries for performance (instead of fetching everything and filtering in memory).

- Text search will use `.Contains()` on relevant fields (Name, Phone, Email, Description).
- Pagination will use `.Skip((page - 1) * pageSize).Take(pageSize)`.
- We will return a `PagedResult<T>` or utilize `ViewBag` to pass pagination metadata to the views.

### UI Toolkit (Tailwind CSS)

- We will build a reusable pagination component in HTML/CSS.
- The filter bar will be placed above each data table. The form will use `method="get"` so filters are bookmarked in the URL.

## 4. Risks and mitigations

- **URL state management:** With many filters, the URL might get long. HTML Forms with `GET` handle this perfectly.
- **Empty results:** Show a friendly "No results found" UI when filters yield zero rows.
- **SQLite performance:** `.Contains()` in SQLite translates to `LIKE '%text%'` which is fine for small datasets but not indexed. Mitigated by the fact that the scope is limited for this app.
