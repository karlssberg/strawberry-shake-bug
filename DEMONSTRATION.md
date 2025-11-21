# Bug Demonstration Summary

## What This Repository Demonstrates

This is a **minimal reproducible example** showing how StrawberryShake's code generation creates problematic C# code when a GraphQL field is named `equals`.

## The Core Issue

### GraphQL Schema
```graphql
type StringMatch {
  equals: String
}
```

### Generated C# Code
```csharp
public class StringMatch
{
    public string? Equals { get; set; }  // ⚠️ Name clash!
}
```

### The Problem
The property `Equals` hides the inherited `object.Equals(object? obj)` method, causing:

1. **Compiler Warning CS0108**
   ```
   warning CS0108: 'StringMatch.Equals' hides inherited member 'object.Equals(object?)'
   ```

2. **Ambiguous Code**
   ```csharp
   var match = new StringMatch();
   var result = match.Equals;  // Property or method?
   ```

3. **Broken Functionality**
   ```csharp
   // Cannot call object.Equals() normally
   match.Equals(other);  // ❌ Accesses property, not method!
   
   // Must cast to object to call the method
   ((object)match).Equals(other);  // ✓ Works but ugly
   ```

## How to Run

### Build Everything
```bash
dotnet build
```

**Expected Output**: Both projects build successfully with CS0108 warnings

### Run the Client Demo
```bash
cd src/Client
dotnet run
```

**Expected Output**: Console output showing the ambiguity and problems

### Run the Server (Optional)
```bash
cd src/Server
dotnet run
```

The server exposes the GraphQL endpoint at `http://localhost:5000/graphql`

## Key Files

- `src/Client/GraphQL/schema.graphql` - GraphQL schema with the problematic `equals` field
- `src/Client/Generated/StringMatch.cs` - Simulated generated code showing the issue
- `src/Client/Program.cs` - Demonstration code showing the problems
- `src/Server/Program.cs` - Hot Chocolate GraphQL server with the same type

## Proposed Solutions

1. **Auto-rename conflicting properties** (e.g., `equals` → `EqualsValue`)
2. **Use the `new` keyword** to explicitly hide inherited members
3. **Provide configuration** to customize naming for conflicts
4. **Warn users** when field names clash with System.Object methods

## Additional Notes

This issue could affect other `System.Object` methods:
- `GetHashCode()` → field named `getHashCode`
- `ToString()` → field named `toString`
- `GetType()` → field named `getType`

However, `equals` is the most likely to occur in real-world GraphQL schemas.
