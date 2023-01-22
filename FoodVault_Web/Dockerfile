FROM node:lts-alpine as build-stage
WORKDIR /app
COPY FoodVault_Web/. ./
RUN npm install
COPY . .
CMD ["npm", "run", "dev"]