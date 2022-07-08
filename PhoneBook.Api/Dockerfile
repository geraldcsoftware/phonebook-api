FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PhoneBook.Api/PhoneBook.Api.csproj", "PhoneBook.Api/"]
RUN dotnet restore "PhoneBook.Api/PhoneBook.Api.csproj"
COPY . .
WORKDIR "/src/PhoneBook.Api"
RUN dotnet build "PhoneBook.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhoneBook.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhoneBook.Api.dll"]
