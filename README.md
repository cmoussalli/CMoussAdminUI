# CMoussAdminUI - Blazor Admin Template Library

A custom Bootstrap-based admin UI template library for .NET 10 Blazor projects.

## Project Structure

- **CMoussAdminUI** - Razor Class Library containing the admin template components, styles, and JavaScript
- **CMoussAdminUI.WebDemo** - Demo Blazor application showing how to use the library

## Features

- Responsive admin layout with collapsible sidebar
- Bootstrap 5.3.2 based styling
- Bootstrap Icons integration
- Purple-themed color scheme
- Mobile-friendly navigation
- Pre-built components:
  - Top navigation bar with notifications
  - Collapsible sidebar navigation
  - User profile dropdown
  - Admin layout component

## Installation

1. Add a reference to the CMoussAdminUI project in your Blazor application:

```xml
<ItemGroup>
  <ProjectReference Include="..\CMoussAdminUI\CMoussAdminUI.csproj" />
</ItemGroup>
```

2. Add the required CSS and JavaScript references in your `App.razor` or `_Host.cshtml`:

```html
<!-- Bootstrap 5 CSS -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
<!-- Bootstrap Icons -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" rel="stylesheet" />
<!-- Admin UI Template CSS -->
<link href="_content/CMoussAdminUI/css/adminui.css" rel="stylesheet" />

<!-- Bootstrap 5 JS Bundle -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
<!-- Admin UI JavaScript -->
<script src="_content/CMoussAdminUI/js/adminui.js"></script>
```

3. Add the namespace to your `_Imports.razor`:

```csharp
@using CMoussAdminUI.Components.Layout
```

4. Use the `MainLayout.razor` from your Blazor app and include the admin template markup, or create a custom layout inheriting from `LayoutComponentBase`.

## Usage

### Using the Layout in Your Pages

Your pages should use the MainLayout that includes the admin template structure. The MainLayout in the WebDemo project demonstrates the complete implementation.

### Customizing Navigation

Edit the navigation links in your `MainLayout.razor`:

```razor
<ul class="nav nav-pills flex-column mb-auto">
    <li class="nav-item mb-1">
        <NavLink href="/" class="nav-link d-flex align-items-center px-3 py-2" Match="NavLinkMatch.All">
            <i class="bi bi-house-door me-2"></i>
            Dashboard
        </NavLink>
    </li>
    <!-- Add more navigation items -->
</ul>
```

### Customizing Styles

The main stylesheet is located at `CMoussAdminUI/wwwroot/css/adminui.css`. You can:

- Override CSS variables in your app's stylesheet
- Modify the source CSS in the library
- Add custom styles in your application

### Key CSS Variables

```css
:root {
    --bs-primary: #6d28d9;        /* Primary purple color */
    --bg-gradient-start: #667eea;
    --bg-gradient-end: #764ba2;
    /* ... and more */
}
```

## Components

### AdminLayout.razor
The main layout component that can be used as an alternative to creating custom layouts. It includes:
- Top navigation bar
- Sidebar with navigation
- Main content area
- All necessary scripts and styles

### AdminNavigation.razor
A reusable navigation component for the sidebar.

## Building the Library

To build the library:

```bash
cd CMoussAdminUI
dotnet build
```

## Running the Demo

To run the demo application:

```bash
cd CMoussAdminUI.WebDemo
dotnet run
```

Then navigate to the URL shown in the console (typically https://localhost:5001).

## Template Features

- **Responsive Design**: Works on desktop, tablet, and mobile devices
- **Collapsible Sidebar**: Can be toggled on desktop and slides in on mobile
- **Dark Sidebar**: Professional dark-themed sidebar navigation
- **Gradient Top Bar**: Eye-catching purple gradient top navigation
- **Notification System**: Pre-built notification dropdown
- **User Profile**: User profile dropdown with common actions
- **Bootstrap Icons**: Full icon library included

## Browser Support

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)

## License

[Your License Here]

## Credits

Based on a custom HTML/CSS template converted to Blazor components.
