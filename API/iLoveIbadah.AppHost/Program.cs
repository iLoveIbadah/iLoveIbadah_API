var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.iLoveIbadah_Website>("iloveibadah-website");

builder.Build().Run();
