# Filter Buttons

Filter Buttons is a Blazor WebAssembly demo for an accessible, reusable active-filter experience in an approval queue.

## Features

- Professional blue-themed interface with light and dark mode.
- Accessible active filter chips with descriptive remove buttons.
- Add-filter dropdown that only shows fields not currently active.
- Apply button that refreshes the sample results after a field is selected.
- Clear all, reset, loading state, focus restoration, and live filter-count announcements.
- Responsive layout for desktop and mobile screens.

## Requirements

- .NET SDK 10.0.101 or a compatible .NET 10 SDK.

The repository includes a `global.json` file that selects the .NET 10 SDK with feature-band roll-forward enabled.

## Run locally

From the project directory:

```powershell
dotnet run
```

Open the URL printed by the application, typically `http://localhost:5249`.

## Build

```powershell
dotnet build
```

## Test

The test project uses xUnit for test execution and bUnit for rendering and interacting with Blazor components:

```powershell
dotnet test .\FilterButtons.Tests\FilterButtons.Tests.csproj
```

The suite covers active-filter rendering and accessibility, remove and clear actions, busy-state behavior, available filter fields, Apply behavior, and refreshed result counts.

## Project structure

- `Pages/Home.razor` - approval queue demo and sample filtering behavior.
- `Components/ActiveFilterBar.razor` - reusable active-filter component.
- `Layout/MainLayout.razor` - application shell and light/dark mode switch.
- `FilterButtons.Tests` - xUnit and bUnit component tests.
- `wwwroot/css/app.css` - global typography, theme variables, and base styles.
