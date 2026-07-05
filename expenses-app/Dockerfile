# Stage 1: Build and publish the Blazor WASM app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder

WORKDIR /src

# Copy project file
COPY expenses-app/expenses-app.csproj ./expenses-app/
RUN cd expenses-app && dotnet restore

# Copy source files
COPY expenses-app/. ./expenses-app/

# Publish the Blazor WebAssembly app
RUN cd expenses-app && dotnet publish -c Release -o /app/publish

# Stage 2: Serve with nginx
FROM nginx:1.31.2-alpine

# Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*

# Copy published Blazor files from builder stage
COPY --from=builder /app/publish/wwwroot /usr/share/nginx/html

# Copy custom nginx config
COPY expenses-app/nginx.conf /etc/nginx/conf.d/default.conf

# Expose port
EXPOSE 8080

# Start nginx
CMD ["nginx", "-g", "daemon off;"]
