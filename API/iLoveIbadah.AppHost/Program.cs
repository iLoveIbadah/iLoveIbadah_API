var builder = DistributedApplication.CreateBuilder(args);

var website = builder.AddProject<Projects.iLoveIbadah_Website>("iloveibadah-website")
    .WithHttpEndpoint(5001, 8080, "iloveibadah-website-http")
    .WithHttpEndpoint(5002, 8081, "iloveibadah-website-https");

var api = builder.AddProject<Projects.iLoveIbadah_API>("iloveibadah-api")
    .WithHttpEndpoint(5003, 80, "iloveibadah-api-http")
    .WithHttpEndpoint(5004, 443, "iloveibadah-api-https");

//var blazorServer = builder.AddProject<Projects.iLoveIbadah_Blazor_Server_UI>("ilobeibadah-blazor-ui")
//    .WithHttpEndpoint(5005, 8082, "iloveibadah-blazor-ui-http")
//    .WithHttpEndpoint(5006, 8083, "iloveibadah-blazor-ui-https");

var webapp = builder.AddProject<Projects.iLoveIbadah_Blazor_WebApp>("iloveibadah-blazor-webapp")
    .WithHttpEndpoint(5005, 8082, "iloveibadah-blazor-webapp-http")
    .WithHttpEndpoint(5006, 8083, "iloveibadah-blazor-webapp-https");

//var blazorServer = builder.AddProject<Projects.iLoveIbadah_Blazor_Server_UI>("ilobeibadah-blazor-ui")
//    .WithHttpEndpoint(5005, 8082, "iloveibadah-blazor-ui-http")
//    .WithHttpEndpoint(5006, 8083, "iloveibadah-blazor-ui-https");

builder.Build().Run();
