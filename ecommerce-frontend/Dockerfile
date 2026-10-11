FROM node:20-alpine AS build
WORKDIR /app

# Копируем зависимости из папки frontend
COPY package*.json ./
RUN npm ci

# Копируем весь код фронтенда
COPY . .
RUN npm run build

# Раздача через Nginx
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]