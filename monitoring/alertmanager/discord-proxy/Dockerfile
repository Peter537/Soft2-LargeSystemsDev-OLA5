# Use small Node image
FROM node:18-alpine
WORKDIR /app
COPY package.json package-lock.json* ./
RUN npm ci --production || npm install --production
COPY index.js ./
EXPOSE 3000
CMD ["node", "index.js"]