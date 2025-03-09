# UserLoginFunctionality

## Description

This project is a **User Login System** developed using **ASP.NET Core 9**. It provides authentication functionality, allowing users to log in, register, and revoke authentication tokens.

## Features

- User Registration
- User Login with JWT Authentication
- Token Revocation (Single & All Tokens)
- Secure API endpoints with JWT authentication
- Input validation with FluentValidation
- Rate Limiting to prevent brute-force attacks
- Exception handling for error management
- Implemented using **N-Tier Architecture**
- Uses **MediatR** for CQRS pattern
- **Unit of Work** pattern for better transaction management
- HTTPS enforced in production

## Technologies Used

- **ASP.NET Core 9**
- **Entity Framework Core**
- **JWT Authentication**
- **FluentValidation**
- **MediatR**
- **Rate Limiting Middleware**
- **Unit of Work Pattern**
- **Swagger (OpenAPI)**

## Installation

1. Clone the repository:

```bash
git clone https://github.com/ZiyaMammadli/InternIntelligence_UserLoginFunctionality.git
cd UserLoginFunctionality
```

2. Configure the **appsettings.json** with your database connection string.
3. Apply Migrations:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4. Run the project:

```bash
dotnet run
```

## API Endpoints

### Authentication
| Endpoint            | Method | Description                   | Authentication |
| ------------------ | ------ | --------------------------- | -------------- |
| /api/Auth/Register | POST   | User Registration           | ❌              |
| /api/Auth/Login    | POST   | User Login                  | ❌              |
| /api/Auth/Revoke   | POST   | Revoke current user's token | ✅              |
| /api/Auth/RefreshToken| POST   | Refresh Token           | ✅              |

✅ - Requires JWT Token ❌ - Public Access

## Security

- **JWT Token**: Secure API endpoints
- **Input Validation**: All incoming requests are validated using FluentValidation.
- **Rate Limiting**: Prevents brute-force attacks by limiting requests.
- **Exception Handling**: All errors are properly handled and returned as structured responses.
- **HTTPS**: Enforced in production mode.

## Usage

To authenticate and access protected endpoints:

1. Obtain JWT token from `/api/auth/login`.
2. Include the token in the **Authorization** header as:

```bash
Authorization: Bearer {your_token}
```

## Contact

For questions or issues, please reach out to:

- Email: [ziyam040@gmail.com](mailto:ziyam040@gmail.com)
- GitHub: [Profile](https://github.com/ZiyaMammadli)

## License

This project is licensed under the MIT License.

