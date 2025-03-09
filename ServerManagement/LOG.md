## Types of Components
1. Routable components
1. non-routable components

Create a route component
- Razor component..
- Servers.razor

What makes a component - **routable** component?
- Page directive @page "/aboutpage"

Uses App.razor `<Routes />` then Routes.razor to find that component (Servers) and applies Main Layout


#### Non-routable (reusable) component
Folder - Controls.
ServerComponent
Use it inside of other component.
Inside Server
`<ServerManagement.Components.Controls.ServerComponent></ServerManagement.Components.Controls.ServerComponent>`
Inclused the whole path.
Place inside imports - `@using ServerManagement.Components.Controls`. 
_Imports.razor - central place for imports.

#### Implicit Expression
Replace implicit with `@` variables.
- Create server application
    - folder - Models
    - ServerModel
    - call model in ServerComponent