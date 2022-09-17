FROM node:16

WORKDIR /APP

COPY . ./

RUN npm install -g npm@^8.19.2 && npm install -g @angular/cli @angular-devkit/build-angular && npm install

EXPOSE 4200

CMD ["npm", "start"]