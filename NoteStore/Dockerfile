FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app



# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

# Expose 5000. This is where nginx will route requests to
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "NoteStore.dll"]

#docker build -t atulmirajkar/notestoredotnetcore2.2:v1 .
#docker push atulmirajkar/notestoredotnetcore2.2:v1