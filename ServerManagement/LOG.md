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

#### Routable component
- Add new page > AddServer
- Need to use Form for **delete**
- Add unique form id
- as blazor SSR static server side rendering is only server side
- there is no interactivity at all
- so the button does not work.
- we need a form to submit data and handle event
- We need event handler (state management)
    - for now just use navigation to update the page (it's http request)
    - better to use anchor points

#### Stream rendering
- `@attribute [StreamRendering]` - handles waiting time let's say
- `<script src="_framework/blazor.web.js"></script>`

## Server Interactivity
- What is not interactive? request - answer then page reloads
- Interactivity in blazor -> should work like windows application
    - in traditional web app. the whole page gets refreshed
    - to solve the problem, we update only the part that needs to be updated
    - no need for javascript
    - WebAssembly could be used instead

### Enhanced Navigation
- when creating Blazor web app template - enhanced navigation is enabled
- `<script src="_framework/blazor.web.js"></script>` - through this line in `App.razor`

### **Enhanced form handling**
- Dynamic forms
- Add `ServerComponent` in `Server.razor`
- Add a button in `ServerComponent` to dynamically change the status
- in blazor only way to do it - button type - Submit
- that means we need to use `<EditForm>`
- add `OnSubmit`, `[SupplyParameterFromForm]`, `OnParametersSet()`, `ChangeServerStatus()`
- add hidden values to send back
- now, the whole page gets reloaded. The logic works but need to fix the reload issue
- Just need to add attribute in `<EditForm>` > `Enhance="true"`

### Server Interactivity
- Instead of using the blazer app.js to send fetch API requests to the web server
- server interactivity is using blazer JS to set up a **WebSocket** channel
- that is called **Signal** Channel
- how it works, the first http request goes to the server and then it should come back
- that makes js file available inside the browser - and then immediately WebSocket channel (Signal) is established. 
- backend holds a memory of the browser
- in traditional - backend does not hold any memory of backend. 
- thus with websocket > the backend is completely different.

#### Enable Server Interactivity
- Make ServerComponent interactive
- It's already interactive, no?
- Yes, remove Enhanced Handling
- We will use **Server Interactivity**
- Instead of `<EditForm>` and `submit` button
- Use `"button"` and `@onclick="changeStatus"`
- How to enable **Server Interactivity**?
    - `Program.cs` > `app.MapRazorComponents<App>().AddInteractiveServerRenderMode();`
    - Dependency injection part: `builder.Services.AddRazorComponents().AddInteractiveServerComponents();`
    - `Servers.razor` > specify render mode in `<ServerComponent @rendermode="InteractiveServer"></ServerComponent>`

#### Interactivity Location
- Inside the ServerComponent we added rendermode.
- we set it to be interactive
- therefore this location is called interactive location
- Two different categories of interactivity:
    1. location is set on the page or set on the component. (component itself or parent component)
    1. at Global level - App.razor > `<Routes />` > `<Routes @rendermode="InteractiveServer"/>` entire application will use server interactivity
- ***Recommendation: Specify interectivity at component level.***
    - not inside of the file itself, but where you use that component.

#### Main aspects of interactive components
- Stateful application: Once you use interactive component in Blazor the app becomes statefull app (**at least for that component**)
- 3 different main aspects: 
    - **View**: UI or Page, what users see
    - **Event**: When user interacts with view
    - **State**: whatever we do, it changes the internal state variables. Handling of events

    - When *State* changes it modifies *View*.

### Event Handling: Passing information
- Handle `onClick` for city names in Servers.razor
- create a function to handle click and receive the city name
- `@onclick="@(() => {SelectCity(city);}`
- now change state variable
- how? first declare it!
- Dont forget `@rendermode InteractiveServer`
- `this.servers = ServersRepositoryModels.GetServersByCity(this.selectedCity);`
- Highlight currect city (card border)

### Update state variables with OnChange event
- Add search bar
- `ChangeEventArgs`
- Add methods to handle search

#### Data binding
- remove onChange for filter
- add `@bind-value="searchFilter" `
- blazor behind the scenes uses OnChange
- get and set inside the value to immidiatly react to changes

### Interactive EditForm
- `@rendermode InteractiveServer`
- no need to have FormName and `[SupplyParameterFromForm(FormName = "formServer")]`
- when rootable component is Interactive then `EditForm` is also interactive
- so when submit button is clicked, the form no longer submitted back to the server with http post
- it will sen message through the Signal channel and then event handler will be triggered
- also we now have **data binding**

#### Virtualization
- if list is long
- whenever you use `foreach`, you can use virtualization
- and if the height of the items is the same
- `<Virtualize>` instead of `foreach`
- no wait time as in foreach
- it knows height of the items, and viewpoint
- it calculates how many items to show
- as you scroll it shows only rendered items

# Thinking in components
- Separation of concern
- Single responsibility principle
    - One component should only change due to one reason

## Extract server components
- ServerListComponent `<ServerListComponent></ServerListComponent>`
- Add render mode

## Communicate between components
- use **Component Parameters**
- Parent > Child component
- define property eg. CityName
    - `[Parameter]`
    - use HTML attribute to specify
    - how can I know if parameter has received the value?
    - override - `OnParametersSet()` > call `GetServers` here
- use a variable which is change based on called method - `@this.selectedCity`

### Break down into smaller components
- ServerListItemComponent
- Extract city components (city selection)

### EventCallback
- EventCallback is essentially a parameter
- call it as `SelectCityCallback.InvokeAsync(cityName);`
- Get's called in child component > then in parent

### Reference a child component
- call child's method
- reference it `@ref="cityListComponent"`
- Clean search when city selected
- Clean city selection when searched
