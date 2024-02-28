# RapidPay REST Api

**Intructions:**

There are 2 ways to generate the database:
1. Changing AppSetting or EnvironmentVariable "CreateDbOnRuntimeExecution" : true so the Database gets deleted and created when executed the first time.
2. Running EF Core command for executing migrations:
 
	 `dotnet ef database update`

**Modules**
 - Card Management
 - Payment Fees 
 
 **Endpoints**
 - Create card:  `POST /api/CardManagement/card/create`
 - Pay: `PUT /api/CardManagement/card/payment`
 - Get card balance: `GET /api/CardManagement/card/{cardNumber}/balance`
 - Get Access Token: `GET /api/Security/access-token`
 
 
**Stack**
 - NET 8
 - Sql Server
 - EntityFrameworkCore 8 Code First Approach
 - EF Core Migrations
 - JWT Authentication
 - FluentValidation
 - Swagger
   
**Design Patterns**
 - N-Layered Architecture
 - Async/Await
 - Repository
 - Dependency Injection
 - Singleton


**Authentication**

 There are a couple of users already seeded at the moment of Database initialization that can be used for executing endpoints on this RES Api: 
 
| Username  | Password  |
|--|--|
| demo@payrapid.io | R@pidPay! |

 - A New user can be created using the endpoint for creating a new Cards, when account property is not null:

```
   POST /api/CardManagement/card/create 
	{
	  "number": "379901349705741",
	  "expirationMonth": 9,
	  "expirationtYear": 2024,
	  "cvc": "368",
	  "balance": 5000,
	  "account": {
	    "firstName": "NewUser",
	    "lastName": "Demo1",
	    "email": "demouser@ducks.com",
	    "password": "123Abc",
	    "phoneNumber": null,
	    "address": null
	  }
	}
```

 - When creating a new card without account information, the new card is automatically added to the existing demo account (to "demo@payrapid.io" account):
```
POST /api/CardManagement/card/create 
{
  "number": "379901349705741",
  "expirationMonth": 9,
  "expirationtYear": 2024,
  "cvc": "368",
  "balance": 5000,
  "account": null
}
```
