# Blasor SSR
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

#### Explicit 
- Display isOnline
- `@(server.IsOnline?"online":"offline")`
- any c# could be used as explicit

#### Static data repository
- Model > ServersRepositoryModel

#### Looping
- dynamically outpust servercomponent
- That is an unordered list (`<ul>`) with a single empty list item (`<li></li>`). It creates a bullet point with no content inside.

#### Buttons
- Create city repositories
- `<button>`

#### Static resources (images etc.)
- `app.UseStaticFiles()` - maps the requests that look for static resources to static resources
- static resources should be placed in `wwwroot` folder

#### Routable component
- new page Page > EditServer.razor
- `@page "/servers/edit"`
- link from each link to this page
- `&nbsp;` - space

#### Route parameters & Route constraints
- to send info - use route parameter
- Edit page need to accept route parameter
    - `@page "/servers/{id}"`
    - `[Parameter]`
    - it should accept only int.
- Root constraints > limits the type
    - `@page "/servers/{id:int}"`

#### OnParametersSet to receive value
- pass Id as route parameters
- retrieve info using Id
- handle null issues if statement

#### Forms & Input
- `<EdirForm>` - form
- input: `<InputText>`, `<InputNumber>`, `<InputCheckbox>`
- Bootstrap classes > for field names

#### Form submission
- each form needs to have a name
    - `FormName="formServer"`
- EventHandler `OnSubmit="Submit"`
    - Submit - function in code
    - `[SupplyParameterFromForm(FormName = "formServer")]`
	- `private Server? server { get; set; }`
    - Submit server ID back to the server
    - Logic inside `Submit()`

#### Forms validation
- use DataAnnotation on Server Class
- `[Required]` attribute
- To make the edit form validate the model object - `<DataannotationsValidator>`
- This tells theedit form to actually try to validate the model object based on the data annotations of the model class
- then we need to specify how we want to spit out the error message.
- Easy method: ValidationSummary
- Instead of `OnSubmit` use `OnValidSubmit`

#### NavigationManager & Dependency Injection
- After submit -> go back
- `@inject NavigationManager NavigationManager`
- 