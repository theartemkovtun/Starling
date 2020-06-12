FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

COPY *.sln .
COPY Starling.WebApi/*.csproj ./Starling.WebApi/
COPY Starling.Domain/*.csproj ./Starling.Domain/
COPY Starling.Shared/*.csproj ./Starling.Shared/ 
COPY Starling.Application/*.csproj ./Starling.Application/ 
COPY Starling.Aes/*.csproj ./Starling.Aes/
COPY Starling.Ecdsa/*.csproj ./Starling.Ecdsa/


RUN dotnet restore 


COPY Starling.Aes/. ./Starling.Aes/
COPY Starling.Ecdsa/. ./Starling.Ecdsa/
COPY Starling.Domain/. ./Starling.Domain/
COPY Starling.Shared/. ./Starling.Shared/ 
COPY Starling.Application/. ./Starling.Application/ 
COPY Starling.WebApi/. ./Starling.WebApi/

WORKDIR /app/Starling.WebApi
RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 

COPY --from=build /app/Starling.WebApi/out ./
ENTRYPOINT ["dotnet", "Starling.WebApi.dll"]