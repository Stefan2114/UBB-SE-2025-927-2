# Team Babes - Something Something Social
Welcome to the GitHub repository for **Team Babes**! This section of the Workout App is designed to help users share their fitness journeys, connect with friends, and stay motivated through posts, workouts, meals, and group interactions.

---

## Project Overview  
Our section is a social platform where users can:  
- Share **posts** about their fitness journey.  
- Log **workouts** and **meals**.  
- Join **groups** to connect with like-minded individuals.  
- Follow other users and interact with their content.  

This section is built with a focus on usability, responsiveness, and a seamless user experience.

---

## Features  
- **Home Page**:  
  - Feed of posts with infinite scrolling.  
  - App Bar with navigation buttons and visual highlighting for the active tab.  
- **Create Post**:  
  - Modal for creating posts with optional text, images, tags, and visibility settings.  
- **Groups**:  
  - Join groups, view group-specific posts, and create new groups.  
- **User Profile**:  
  - View posts, workouts, meals, and followers.  
  - Follow/unfollow other users.  
- **Login/Register**:  
  - Secure authentication with email and password.  
  - Optional profile image upload during registration.  

## Coding Style and Conventions

This document outlines the coding style and conventions to be followed in this project to ensure consistency, readability, and maintainability.

### 1. Class, Struct, and Enum Naming

- Use PascalCase for all class, struct, and enum names.
  - ✅ Example: `CommentService`, `OrderViewModel`

### 2. Interface Naming

- Prefix all interfaces with the letter "I" and use PascalCase.
  - ✅ Example: `ICommentRepository`, `IUserService`

### 3. Method Naming

- Use PascalCase for method names.
- Method names should be descriptive and action-oriented.
  - ✅ Example: `GetUserDetails()`, `CalculateTotalCalories()`

### 4. Property and Field Naming

- Use PascalCase for properties and fields.
  - ✅ Example:
    ```csharp
    // Property
    public string FirstName { get; set; }

    // Field
    private readonly ICommentRepository commentRepository;
    ```

### 5. Variable Naming

- Use camelCase for local variables and method parameters.
  - ✅ Example:
    ```csharp
    var totalPrice = 0;
    ```

### 6. Asynchronous Methods

- Name asynchronous methods with an "Async" suffix.
  - ✅ Example: `SaveChangesAsync()`, `LoadDataAsync()`

### 7. Interface Usage and Implementation

- All classes must have a corresponding interface, except for pure Entity classes (e.g., `User`, `Product`, `Order`).
- Teams should always depend on interfaces, not on concrete classes, to improve testability, maintainability, and flexibility.
- The interface name must match the class name, prefixed with `I`.
  - ✅ Example: `UserService` → `IUserService`

### 8. Namespace Organization

- Follow a logical structure that mirrors the folder structure.
- Namespaces should reflect the pattern:
ProjectName.MVVM.Views
ProjectName.MVC.Controllers
ProjectName.Services

### 9. File Naming

- Each class, interface, or enum must be in its own file, and the file name must match the class/interface/enum name exactly.
- ✅ Example: `OrderService.cs`, `IOrderService.cs`

### 10. Braces and Indentation

- Always use braces `{}`, even for single-line blocks.
- Use 4 spaces for indentation (no tabs).

### 11. Class Naming Should Reflect Responsibility

- Class names must clearly represent the entity or concept they are responsible for, using appropriate suffixes:
- Controllers must have a `Controller` suffix.
- ViewModels must have a `ViewModel` suffix.
- Services must have a `Service` suffix.
- Repositories must have a `Repository` suffix.
- Proxies must have a `Proxy` suffix.
- DTOs (Data Transfer Objects) must have a `Dto` suffix.
- ✅ Examples:
  ```
  UserController
  UserViewModel
  UserService
  UserRepository
  UserProxy
  UserDto
  ```

### 12. Dependency Injection

- Always inject dependencies via the constructor.
- Avoid service locator or manual instantiations inside classes.

### 13. Comments and Documentation

- Use XML documentation comments (`///`) for public methods, properties, and classes.
- Keep comments meaningful and avoid redundant comments that state the obvious.

### 14. Consistent Use of `this` Keyword

- Use the `this` keyword when referencing instance members within a class to improve code clarity.
- ✅ Example:
  ```csharp
  this.userName = userName;
  ```

### 15. Constant Naming (Capital Letters)

- Constants must be named using PascalCase or `ALL_CAPS_WITH_UNDERSCORES` (team must choose one and be consistent).
- For important constants (especially global or shared ones), prefer `ALL_CAPS_WITH_UNDERSCORES` style.
- Constants should be declared as `const` or `static readonly` depending on the situation.
- ✅ Good Example:
  ```csharp
  private const string DEFAULT_USER_ROLE = "User";
  ```

### 16. Magic Numbers

- Avoid magic numbers directly in the code.
- All hard-coded numeric values and important string literals must be moved into a named constant, an enum, or a configuration file.
- Code should be self-explanatory by reading constant names.
- ✅ Good Example:
  ```csharp
  private const int MAX_RETRY_COUNT = 3;
  ```

### 17. Test Class Naming

- Use the same name as the class being tested, suffixed with Tests.

- ✅ Example:
```csharp
UserService → UserServiceTests
```

### 18. Test Method Naming

- Use the pattern:
- MethodName_Scenario_ExpectedOutcome

- ✅ Examples:
```csharp
    GetUserById_UserExists_ReturnsUser()

    CreateOrder_InvalidInput_ThrowsException()
```
- This improves readability and test discoverability.

### 19. AAA Pattern (Arrange-Act-Assert)

- Structure test methods using the AAA pattern for clarity.

- ✅ Example:
```csharp
// Arrange
var userService = new UserService(mockRepo.Object);

// Act
var result = userService.GetUserById(1);

// Assert
Assert.NotNull(result);
```

## Conditii folosire Git

- Trebuie respectate deadline-ul (sambata seara)
- Inainte sa dea push sa incerce mereu un "git pull --rebase origin main"
- Fiecare trebuie sa rezolve confictele daca este cazul
- Nu ai voie sa dai push pe git daca ai erori mereu testezi sa vezi ca aplicatia ruleaza sau macar da build
- ucrati doar pe branch-ul vostru
- folosit git add . pentru a specifica fisierele pe care vreti sa le adaugati cand creati un commit ("." reprezinta current directory)
- pentru fiecare task/ lucru nou creati un commit nou (git commit -m "scurta descriere")
- commit-ul trebuie sa contina o descriere foarte pe scurt a ce ati facut
- Inainte sa dea push sa incerce mereu un "git pull --rebase origin main"
- "git push" pentru a se trimite pe github
- dupa fiecare commit creati un pull request
- nu dati merge la codul voastru pana nu a fost verificat de cineva
- nu dati force push la cod. Dati mesaj pe grup poate se rezolva intre timp
