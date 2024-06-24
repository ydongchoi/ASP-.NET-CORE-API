FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-image
WORKDIR /home/app
COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
RUN dotnet restore

COPY . .
RUN dotnet test ./Tests/Tests.csproj
RUN dotnet publish ./CompanyEmployees/CompanyEmployees.csproj -o /publish/ 

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /publish
COPY --from=build-image /publish .
COPY ./CompanyEmployees.Presentation/*.xml .
ENV ASPNETCORE_URLS=https://+:5002;http://+:5001
ENV ASPNETCORE_HTTPS_PORT=8082

ENTRYPOINT ["dotnet", "CompanyEmployees.dll"]