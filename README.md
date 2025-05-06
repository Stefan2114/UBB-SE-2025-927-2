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


# Git Contribution Report
Generated on: 2025-05-06 07:29:40
Repository: UBB-SE-2025-927-2
Date Range: From: 2025-04-23, To: 2025-05-05
## Summary
| Contributor | Commits | Lines Changed | Files Changed | Branches | Diagrams | Non-Negligible Work |
| --- | --- | --- | --- | --- | --- | --- |
| Mihai Țălu | 40 | 10561 | 127 | 7 | 0 | ✅ |
| Toma Stefan | 12 | 0 | 0 | 0 | 0 | ✅ |
| Stefan2114 | 6 | 1677 | 55 | 11 | 0 | ✅ |
| Andrei Ungureanu | 5 | 1188 | 25 | 4 | 0 | ✅ |
| MihaiTopi | 5 | 369 | 17 | 6 | 0 | ✅ |
| PaulSchiop | 4 | 1190 | 15 | 6 | 0 | ✅ |
| Mihnea | 3 | 1129 | 16 | 6 | 0 | ✅ |
| MihneaVoda | 2 | 0 | 0 | 0 | 0 | ❌ |
| Alexandru Țigănașu | 1 | 296 | 15 | 6 | 0 | ✅ |
| MariaDariaTompea | 1 | 340 | 9 | 5 | 0 | ✅ |
| Topor Mihai | 1 | 0 | 0 | 0 | 0 | ❌ |
| Valasa-Andrei | 1 | 454 | 8 | 4 | 0 | ✅ |

## Visualizations
### Commits per Contributor
![Commits per Contributor](contribution_visualizations/commits_per_contributor.png)
### Lines Changed
![Lines Changed](contribution_visualizations/lines_changed.png)
### Contribution Timeline
![Contribution Timeline](contribution_visualizations/contribution_timeline.png)
### Non-Negligible Work Assessment
![Non-Negligible Work Assessment](contribution_visualizations/contribution_assessment.png)

## Detailed Analysis
### Mihai Țălu
### talumihai@gmail.com
#### Commit Statistics
- Total Commits: 40
- First Commit: 2025-04-29
- Last Commit: 2025-05-02
- Active Days: 3
- Contribution Timespan: 4 days
- Average Commits Per Active Day: 10.00

#### Code Changes
- Lines Added: 10181
- Lines Deleted: 380
- Total Lines Changed: 10561

#### Branches Used
- Bogdi
- Maria
- Valasa
- main
- stef3
- stef4
- stef5

#### Files Changed
- Total Files: 127
- File Types:
  - .cs: 105
  - .xaml: 16
  - .sql: 6

#### Contribution Assessment
- **Non-Negligible Work**: Yes
- Reasoning:
  - Made 40 commits (threshold: 5)
  - Changed 10561 lines of code (threshold: 100)
  - Active on 3 days (threshold: 3)

---
### Toma Stefan
### 147135917+stefan2114@users.noreply.github.com
#### Commit Statistics
- Total Commits: 12
- Active Days: 1
- Average Commits Per Active Day: 12.00

#### Code Changes
- Lines Added: 0
- Lines Deleted: 0
- Total Lines Changed: 0

#### Files Changed
- Total Files: 0

#### Contribution Assessment
- **Non-Negligible Work**: Yes
- Reasoning:
  - Made 12 commits (threshold: 5)

---
### Stefan2114
### stefan.toma021@gmail.com
#### Commit Statistics
- Total Commits: 6
- First Commit: 2025-04-25
- Last Commit: 2025-04-29
- Active Days: 4
- Contribution Timespan: 5 days
- Average Commits Per Active Day: 1.20

#### Code Changes
- Lines Added: 939
- Lines Deleted: 738
- Total Lines Changed: 1677

#### Branches Used
- Bogdi
- Maria
- Valasa
- creating-PostViewModel
- entity-framework-post
- main
- seminar4
- server-repo-post
- stef3
- stef4
- stef5

#### Files Changed
- Total Files: 55
- File Types:
  - .cs: 66
  - .csproj: 4
  - .sql: 4
  - .json: 2

#### Contribution Assessment
- **Non-Negligible Work**: Yes
- Reasoning:
  - Made 6 commits (threshold: 5)
  - Changed 1677 lines of code (threshold: 100)
  - Active on 4 days (threshold: 3)

---
### Andrei Ungureanu
### andreiungureanu2014@yahoo.ro
#### Commit Statistics
- Total Commits: 5
- First Commit: 2025-05-03
- Last Commit: 2025-05-03
- Active Days: 3
- Contribution Timespan: 1 days
- Average Commits Per Active Day: 5.00

#### Code Changes
- Lines Added: 972
- Lines Deleted: 216
- Total Lines Changed: 1188

#### Branches Used
- Valasa
- main
- stef4
- stef5

#### Files Changed
- Total Files: 25
- File Types:
  - .cs: 19
  - .cshtml: 6
  - .sql: 3
  - .json: 2
  - .csproj: 1

#### Contribution Assessment
- **Non-Negligible Work**: Yes
- Reasoning:
  - Made 5 commits (threshold: 5)
  - Changed 1188 lines of code (threshold: 100)
  - Active on 3 days (threshold: 3)

---
### MihaiTopi
### mihai.topor@outlook.com
#### Commit Statistics
- Total Commits: 5
- First Commit: 2025-05-03
- Last Commit: 2025-05-04
- Active Days: 3
- Contribution Timespan: 2 days
- Average Commits Per Active Day: 2.50

#### Code Changes
- Lines Added: 272
- Lines Deleted: 97
- Total Lines Changed: 369

#### Branches Used
- Bogdi
- Valasa
- main
- stef3
- stef4
- stef5

#### Files Changed
- Total Files: 17
- File Types:
  - .cs: 28
  - .json: 4

#### Contribution Assessment
- **Non-Negligible Work**: Yes
- Reasoning:
  - Made 5 commits (threshold: 5)
  - Changed 369 lines of code (threshold: 100)
  - Active on 3 days (threshold: 3)

---
### PaulSchiop
### schioppaul@gmail.com
#### Commit Statistics
- Total Commits: 4
- First Commit: 2025-05-04
- Last Commit: 2025-05-04
- Active Days: 3
- Contribution Timespan: 1 days
- Average Commits Per Active Day: 4.00

#### Code Changes
- Lines Added: 1180
- Lines Deleted: 10
- Total Lines Changed: 1190

#### Branches Used
- Bogdi
- Valasa
- main
- stef3
- stef4
- stef5

#### Files Changed
- Total Files: 15
- File Types:
  - .cs: 12
  - .json: 2
  - .sql: 1
  - .csproj: 1

#### Contribution Assessment
- **Non-Negligible Work**: Yes
- Reasoning:
  - Changed 1190 lines of code (threshold: 100)
  - Active on 3 days (threshold: 3)

---
### Mihnea
### mihnea.voda@stud.ubbcluj.ro
#### Commit Statistics
- Total Commits: 3
- First Commit: 2025-05-04
- Last Commit: 2025-05-05
- Active Days: 3
- Contribution Timespan: 2 days
- Average Commits Per Active Day: 1.50

#### Code Changes
- Lines Added: 686
- Lines Deleted: 443
- Total Lines Changed: 1129

#### Branches Used
- Bogdi
- Valasa
- main
- stef3
- stef4
- stef5

#### Files Changed
- Total Files: 16
- File Types:
  - .cs: 32
  - .json: 6

#### Contribution Assessment
- **Non-Negligible Work**: Yes
- Reasoning:
  - Changed 1129 lines of code (threshold: 100)
  - Active on 3 days (threshold: 3)

---
### MihneaVoda
### 147316786+desixtt@users.noreply.github.com
#### Commit Statistics
- Total Commits: 2
- Active Days: 1
- Average Commits Per Active Day: 2.00

#### Code Changes
- Lines Added: 0
- Lines Deleted: 0
- Total Lines Changed: 0

#### Files Changed
- Total Files: 0

#### Contribution Assessment
- **Non-Negligible Work**: No
- Reasoning:
  - Did not meet minimum thresholds for non-negligible work

---
### Alexandru Țigănașu
### alexandru.tiganasu@stud.ubbcluj.ro
#### Commit Statistics
- Total Commits: 1
- First Commit: 2025-05-02
- Last Commit: 2025-05-02
- Active Days: 1
- Contribution Timespan: 1 days
- Average Commits Per Active Day: 1.00

#### Code Changes
- Lines Added: 243
- Lines Deleted: 53
- Total Lines Changed: 296

#### Branches Used
- Bogdi
- Valasa
- main
- stef3
- stef4
- stef5

#### Files Changed
- Total Files: 15
- File Types:
  - .cs: 15

#### Contribution Assessment
- **Non-Negligible Work**: Yes
- Reasoning:
  - Changed 296 lines of code (threshold: 100)

---
### MariaDariaTompea
### mariatompea@gmail.com
#### Commit Statistics
- Total Commits: 1
- First Commit: 2025-05-03
- Last Commit: 2025-05-03
- Active Days: 1
- Contribution Timespan: 1 days
- Average Commits Per Active Day: 1.00

#### Code Changes
- Lines Added: 337
- Lines Deleted: 3
- Total Lines Changed: 340

#### Branches Used
- Maria
- Valasa
- main
- stef4
- stef5

#### Files Changed
- Total Files: 9
- File Types:
  - .cs: 7
  - .json: 2

#### Contribution Assessment
- **Non-Negligible Work**: Yes
- Reasoning:
  - Changed 340 lines of code (threshold: 100)

---
### Topor Mihai
### 147518933+mihaitopi@users.noreply.github.com
#### Commit Statistics
- Total Commits: 1
- Active Days: 1
- Average Commits Per Active Day: 1.00

#### Code Changes
- Lines Added: 0
- Lines Deleted: 0
- Total Lines Changed: 0

#### Files Changed
- Total Files: 0

#### Contribution Assessment
- **Non-Negligible Work**: No
- Reasoning:
  - Did not meet minimum thresholds for non-negligible work

---
### Valasa-Andrei
### andreivalasa7@gmail.com
#### Commit Statistics
- Total Commits: 1
- First Commit: 2025-05-03
- Last Commit: 2025-05-03
- Active Days: 1
- Contribution Timespan: 1 days
- Average Commits Per Active Day: 1.00

#### Code Changes
- Lines Added: 310
- Lines Deleted: 144
- Total Lines Changed: 454

#### Branches Used
- Valasa
- main
- stef4
- stef5

#### Files Changed
- Total Files: 8
- File Types:
  - .cs: 8

#### Contribution Assessment
- **Non-Negligible Work**: Yes
- Reasoning:
  - Changed 454 lines of code (threshold: 100)

---

