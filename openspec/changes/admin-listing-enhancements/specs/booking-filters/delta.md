# Spec Delta: Booking Filters

## MODIFIED Requirements

- [x] Admin Booking list MUST include dropdowns/date-pickers for Date Range and Booking Status.
  #### Scenario: Filtering by status
  Selecting "Confirmed" filters out "Pending" and "Cancelled" bookings.
  #### Scenario: Filtering by date range
  Selecting a `StartDate` and `EndDate` limits the bookings whose `StartTime` falls in the range.
