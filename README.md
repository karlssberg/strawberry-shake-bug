# Strawberry Shake Bug Demonstration

This repository demonstrates a code generation issue in StrawberryShake when a GraphQL field name clashes with methods from `System.Object`.

## The Problem

When a GraphQL schema contains a field named `equals`, StrawberryShake generates a C# property named `Equals`. This creates a name clash with the inherited `object.Equals(object? obj)` method, leading to:

1. **Compiler Warning CS0108**: `'StringMatch.Equals' hides inherited member 'object.Equals(object?)'`
2. **Ambiguous Code**: It becomes unclear whether code is accessing the property or calling the method
3. **Broken Functionality**: The inherited `Equals()` method is hidden and cannot be called normally

## GraphQL Schema

```graphql
type StringMatch {
  equals: String
}
```

## Reproducing the Issue

### Build the solution:
```bash
dotnet build
```

You'll see the compiler warning:
```
warning CS0108: 'StringMatch.Equals' hides inherited member 'object.Equals(object?)'
```

### Run the demonstration:
```bash
cd src/Client
dotnet run
```

This will show how the property `Equals` hides the `object.Equals()` method.

## Project Structure

- **src/Server**: A minimal GraphQL server with Hot Chocolate exposing the `StringMatch` type
- **src/Client**: A console application with simulated StrawberryShake-generated code demonstrating the bug
  - `GraphQL/schema.graphql`: The problematic schema
  - `GraphQL/GetStringMatch.graphql`: A sample query
  - `Generated/StringMatch.cs`: Simulated generated code showing the name clash

## Expected Behavior

StrawberryShake should either:
1. Rename fields that clash with `System.Object` methods (e.g., `Equals` → `EqualsValue`)
2. Use the `new` keyword to explicitly hide the inherited member
3. Provide a configuration option to handle such naming conflicts

## Other Affected Methods

This issue could potentially affect other `System.Object` methods:
- `GetHashCode()`
- `ToString()`
- `GetType()`

## Environment

- .NET 10.0
- StrawberryShake.Transport.Http 15.1.11
- HotChocolate.AspNetCore 15.1.11
