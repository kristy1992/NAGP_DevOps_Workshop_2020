FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
EXPOSE 8080
COPY DemoWebApplication/DemoWebApplication/bin/Debug/netcoreapp3.0/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "DemoWebApplication.dll"]