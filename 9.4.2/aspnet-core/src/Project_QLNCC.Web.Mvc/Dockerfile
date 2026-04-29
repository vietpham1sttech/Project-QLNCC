FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/Project_QLNCC.Web.Mvc/Project_QLNCC.Web.Mvc.csproj", "src/Project_QLNCC.Web.Mvc/"]
COPY ["src/Project_QLNCC.Web.Core/Project_QLNCC.Web.Core.csproj", "src/Project_QLNCC.Web.Core/"]
COPY ["src/Project_QLNCC.Application/Project_QLNCC.Application.csproj", "src/Project_QLNCC.Application/"]
COPY ["src/Project_QLNCC.Core/Project_QLNCC.Core.csproj", "src/Project_QLNCC.Core/"]
COPY ["src/Project_QLNCC.EntityFrameworkCore/Project_QLNCC.EntityFrameworkCore.csproj", "src/Project_QLNCC.EntityFrameworkCore/"]
WORKDIR "/src/src/Project_QLNCC.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Project_QLNCC.Web.Mvc", "src/Project_QLNCC.Web.Mvc"]
COPY ["src/Project_QLNCC.Web.Core", "src/Project_QLNCC.Web.Core"]
COPY ["src/Project_QLNCC.Application", "src/Project_QLNCC.Application"]
COPY ["src/Project_QLNCC.Core", "src/Project_QLNCC.Core"]
COPY ["src/Project_QLNCC.EntityFrameworkCore", "src/Project_QLNCC.EntityFrameworkCore"]
WORKDIR "/src/src/Project_QLNCC.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Project_QLNCC.Web.Mvc.dll"]
