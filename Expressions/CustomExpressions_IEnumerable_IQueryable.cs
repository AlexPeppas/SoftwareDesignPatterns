using Shared;
using System.Linq.Expressions;

namespace Expressions
{
    public class CustomExpressions_IEnumerable_IQueryable
    {
        public static void IEnumerableVsIQueryable()
        {
            var cars = new List<string> { "jeep", "sport", "SUV", "daily" };

            var jeeps = cars.Where(car => car == "jeep").ToList();
            var jeepsEnumerable = Enumerable.Where(cars, car => car == "jeep").ToList();
            
            var query = cars.Where(car => car == "jeep").AsQueryable();
            
        }

        public static void CustomExpression_SimpleFilter()
        {
            // x > 12

            var xExpression = Expression.Parameter(typeof(int), "x");

            var constantExpression = Expression.Constant(12);

            var greaterThan = Expression.GreaterThan(xExpression, constantExpression);

            var expr = Expression.Lambda<Func<int,bool>>(greaterThan, false, xExpression);

            var expression = expr.Compile();

            Console.WriteLine(expression(1));
        }

        public static void CustomExpression_ComplexFilter()
        {
            // x<4 OR x>12

            var xOperand = Expression.Parameter(typeof(int), "x");

            var fourConstant = Expression.Constant(4);
            var twelveConstance = Expression.Constant(12);

            var greaterThan = Expression.GreaterThan(xOperand, twelveConstance);
            var lessThan = Expression.LessThan(xOperand, fourConstant);

            var finalExpressionTree = Expression.Or(lessThan, greaterThan);

            var funcOutput = Expression.Lambda<Func<int, bool>>(finalExpressionTree, false, xOperand);

            var expressionCompiled = funcOutput.Compile();

            Console.WriteLine(expressionCompiled(13)); // True
            Console.WriteLine(expressionCompiled(3)); // True
            Console.WriteLine(expressionCompiled(12)); // False
            Console.WriteLine(expressionCompiled(7)); // False
        }

        public static void DynamicFilter()
        {
            // ch.Gender = male and ch.Surname = "CS"

            var characters = PrepareCharacters();

            var maleConstant = Expression.Constant("male");
            var csConstant = Expression.Constant("CS");

            var characterParameter = Expression.Parameter(typeof(Character));

            var genderProperty = Expression.Property(characterParameter, Character.FieldNames.GenderField);
            var surnameProperty = Expression.Property(characterParameter, Character.FieldNames.SurnameField);

            var gendEquals = Expression.Equal(genderProperty, maleConstant);
            var surnEquals = Expression.Equal(surnameProperty, csConstant);

            var expression = Expression.And(gendEquals, surnEquals);

            var expLambda = Expression.Lambda<Func<Character, bool>>(expression, false, characterParameter);

            var predicate = expLambda.Compile();

            var charactersFiltered = characters.Where(predicate);

        }

        public static void DynamicFilterRefactored()
        {
            // ch.Gender = male and ch.Surname = "CS"

            var characters = PrepareCharacters();

            var characterParameter = Expression.Parameter(typeof(Character));

            Expression currentExpression = null;

            currentExpression = CreateExpression<string>(
                characterParameter,
                Character.FieldNames.GenderField,
                "male");

            currentExpression = CreateExpression<string>(
                characterParameter,
                Character.FieldNames.SurnameField,
                "CS",
                currentExpression);

            var expLambda = Expression.Lambda<Func<Character, bool>>(currentExpression, false, characterParameter);

            var predicate = expLambda.Compile();

            var charactersFiltered = characters.Where(predicate);

        }

        private static Expression CreateExpression<T>(
            ParameterExpression parameter,
            string propertyName,
            T valueToCheck,
            Expression currentExpression = null,
            string operatorValue = "=")
        {
            var property = Expression.Property(parameter, propertyName);

            var constant = Expression.Constant(valueToCheck, typeof(T));

            var condition = operatorValue switch
            {
                "!=" => Expression.NotEqual(property, constant),
                ">" => Expression.GreaterThan(property, constant),
                "<" => Expression.LessThan(property, constant),
                _ => Expression.Equal(property, constant)
            };

            if (currentExpression is null)
            {
                return condition;
            }

            var previousExpression = currentExpression;
            return Expression.And(previousExpression, condition);
        }

        private static IEnumerable<Character> PrepareCharacters()
        {
            return new List<Character>
            {
                new Character
                {
                    Gender = "male",
                    CharacterName = "A",
                    Surname = "AS",
                },
                new Character
                {
                    Gender = "male",
                    CharacterName = "A",
                    Surname = "CS",
                },
                new Character
                {
                    Gender = "female",
                    CharacterName = "B",
                    Surname = "BS",
                },
            };
        }
    }
}