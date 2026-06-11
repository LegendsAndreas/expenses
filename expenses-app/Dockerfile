# Use lightweight nginx image
FROM nginx:alpine

# Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*

# Copy Blazor WebAssembly files
COPY bin/Release/net10.0/publish/wwwroot /usr/share/nginx/html

# Copy custom nginx config (optional but recommended)
#COPY nginx.conf /etc/nginx/conf.d/default.conf

# Expose port
EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]