# ProductService
The Product service is implemented for demo purposes.
Implemented REST API with CRUD repository. The solution includes the following projects:
  \Api - it's REST API service;
  \Common - it contains shared models and interfaces;
  \Data.MongoDb - it is data provider for using MongoDB database;
  \Data.Sql - it is data provider for using Postgres database;
  \Tests - it contains unit tests;

Using data provider is switched by [true/false] on parameter "UseMongoDB" in "Api\appsettings.json"
By default this parameter is setted to "true" - using MongoDB
