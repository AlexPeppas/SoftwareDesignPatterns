using Expressions;
using SoftwareDesignPatterns;

public class Program
{
    public static void Main()
    {
        CustomSerializerTest();
        CustomExpressionTest();
        
    }

    public static void CustomExpressionTest()
    {
        CustomExpressions_IEnumerable_IQueryable.CustomExpression_SimpleFilter();
        CustomExpressions_IEnumerable_IQueryable.DynamicFilter();
        CustomExpressions_IEnumerable_IQueryable.DynamicFilterRefactored();
    }

    public static void CustomSerializerTest()
    {
        var entity = new ParentEntity
        {
            Id = 1,
            Description = "Test",
            Name = "Test",
            ParentId = 1,
            ParentName = "Test",
        };

        var serialized = CustomSerializerUsingAutoMapper.Serialize(entity);
        var deserialized = CustomSerializerUsingAutoMapper.Deserialize(serialized);
    }
}