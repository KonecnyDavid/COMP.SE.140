FROM node:20
RUN curl -fsSL https://get.docker.com | sh

WORKDIR /app
COPY package.json /app
RUN npm install
COPY . /app
CMD ["node","app.js"]