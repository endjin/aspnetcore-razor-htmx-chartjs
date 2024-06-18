# ASP.NET Core + Razor + HTMX 2.x + Chart.js Demo

A sample demonstrating how to combine ASP.NET Core, Razor, and HTMX 2.x to power interactive Chart.js visualisations.

![Showing a bar chart rendered by Chart.js using HTMX to offer client side interactivity](/Artefacts/images/aspnet-razor-htmx-chartjs.png "ASP.NET Core + Razor + HTMX + Chart.js Demo")

[HTMX](https://htmx.org/) is a simple & elegant library that enables a SPA-like experience without the complexity (and dependency hell) of a full SPA framework. It's a great fit for server-side frameworks like ASP.NET Core, and at [endjin](https://endjin.com) we've been migrating all of our web apps to use it.

While HTMX has [established patterns](https://htmx.org/examples/) for many common UX interactions, integrating with client-side libraries like Chart.js is less well documented. HTMX's primary use case is to interact with the server, which renders (shifting the complexity from the client to the server) and returns HTML fragments, which HTMX then injects into the DOM, and deals with transitions, animations etc, to provide a seamless modern SPA-like experience. HTMX deals with hypermedia (HTML), not data, whereas Chart.js is designed to interact with more traditional Restful JSON APIs. 

This example demonstrates how to bridge the two worlds by using HTMX's [extensibility mechanisms](https://htmx.org/extensions/) to intercept the server response, parse the JSON payload, and use this to update the DOM and Chart.js visualisation. Client-side state and entity ids are round-tripped, using some HAL and HATEOAS inspired techniques. Client-side element state (previous / next buttons) is calculated by comparing the `self`, `previous` and `next` links in the JSON payload.

Initial page / Chart.js state is set by Razor, and previous / next buttons are adorned with the `hx-get` attribute to trigger a HTTP GET request to the server. `hx-ext="update_chartjs"` is used to specify the [HTMX extension to use](/Solution/aspnetcore-razor-htmx-chartjs/wwwroot/js/htmx-ex-update_chartjs.js) to process the response. We use the `transformResponse` [extension point](https://htmx.org/extensions/#defining) to intercept the response, parse the JSON payload, update Chart.js visualisation, and the DOM. The `hx-swap="none"` prevents HTMX's update behaviour from running, as we specify our behaviour in the extension.

If you have any questions, defect reports, or suggestions for improvements, please [raise an issue](https://github.com/endjin/aspnetcore-razor-htmx-chartjs/issues).

## Licenses

This project is available under the Apache 2.0 open source license.

[![GitHub license](https://img.shields.io/badge/License-Apache%202-blue.svg)](https://raw.githubusercontent.com/endjin/aspnetcore-razor-htmx-chartjs/main/LICENSE)

## Project Sponsor

This project is sponsored by [endjin](https://endjin.com), a UK based Technology Consultancy which specializes in Data, AI, DevOps & Cloud, and is a [.NET Foundation Corporate Sponsor](https://dotnetfoundation.org/membership/corporate-sponsorship).

> We help small teams achieve big things.

We produce two free weekly newsletters: 

 - [Azure Weekly](https://azureweekly.info) for all things about the Microsoft Azure Platform
 - [Power BI Weekly](https://powerbiweekly.info) for all things Power BI, Microsoft Fabric, and Azure Synapse Analytics

Keep up with everything that's going on at endjin via our [blog](https://endjin.com/blog), follow us on [Twitter](https://twitter.com/endjin), [YouTube](https://www.youtube.com/c/endjin) or [LinkedIn](https://www.linkedin.com/company/endjin).

We have become the maintainers of a number of popular .NET Open Source Projects:

- [Reactive Extensions for .NET](https://github.com/dotnet/reactive)
- [Reaqtor](https://github.com/reaqtive)
- [Argotic Syndication Framework](https://github.com/argotic-syndication-framework/)

And we have over 50 Open Source projects of our own, spread across the following GitHub Orgs:

- [endjin](https://github.com/endjin/)
- [Corvus](https://github.com/corvus-dotnet)
- [Menes](https://github.com/menes-dotnet)
- [Marain](https://github.com/marain-dotnet)
- [AIS.NET](https://github.com/ais-dotnet)

And the DevOps tooling we have created for managing all these projects is available on the [PowerShell Gallery](https://www.powershellgallery.com/profiles/endjin).

For more information about our products and services, or for commercial support of this project, please [contact us](https://endjin.com/contact-us). 