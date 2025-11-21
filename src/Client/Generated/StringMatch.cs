// This file simulates what StrawberryShake would generate
// for a GraphQL type with an 'equals' field

namespace Client
{
    /// <summary>
    /// Generated type for StringMatch from GraphQL schema
    /// </summary>
    public class StringMatch
    {
        /// <summary>
        /// This property name causes a clash with System.Object.Equals(object? obj)
        /// StrawberryShake generates a property named 'Equals' from the GraphQL field 'equals'
        /// </summary>
        public string? Equals { get; set; }
    }
}
