# Endpoints

![image](https://github.com/user-attachments/assets/8a8a3a0a-2408-43bc-9193-29f9c795e900)

# UserDTO

## For adding a user

```csharp
public class UserAddDTO
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public string Name { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    public string Role { get; set; }
}
```

## For editing a user

```csharp
public class UserEditDTO
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public string Name { get; set; }
}
```

# LoginDTO

```csharp
public class LoginDTO
{
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}
```

# ArticleDTO

```csharp
public class ArticleDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
}
```

# CommentDTO

```csharp
public class CommentDTO
{
    public string Content { get; set; }
    public UserEditDTO User { get; set; }
}
```

# Docker

Runs on port 8080
