using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace LanguageExt.AspNetCore.Test
{
    public class TryExtensionsTest
    {
        [Test]
        public void TrySuccess_ToActionResult_ShouldBeOk()
        {
            // Arrange
            var sut = Try(() => AverageAge(Writers));

            // Act
            var result = sut.ToActionResult();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }

        [Test]
        public void TryFail_ToActionResult_ShouldBeServerError()
        {
            // Arrange
            var sut = Try(() => FailingCalculation(Writers));

            // Act
            var result = sut.ToActionResult();

            // Assert
            result.Should().BeOfType<ObjectResult>();
            (result as ObjectResult).StatusCode.Should().Be(500);
        }

        [Test]
        public async Task TaskTrySuccess_ToActionResult_ShouldBeOk()
        {
            // Arrange
            var sut = Try(() => AverageAge(Writers)).AsTask();

            // Act
            var result = await sut.ToActionResult();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }

        [Test]
        public async Task TaskTryFail_ToActionResult_ShouldBeServerError()
        {
            // Arrange
            var sut = Try(() => FailingCalculation(Writers)).AsTask();

            // Act
            var result = await sut.ToActionResult();

            // Assert
            result.Should().BeOfType<ObjectResult>();
            (result as ObjectResult).StatusCode.Should().Be(500);
        }

        [Test]
        public async Task TryAsyncSuccess_ToActionResult_ShouldBeOk()
        {
            // Arrange
            var sut = TryAsync(() => AverageAge(Writers).AsTask());

            // Act
            var result = await sut.ToActionResult();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }

        [Test]
        public async Task TryAsyncFail_ToActionResult_ShouldBeServerError()
        {
            // Arrange
            var sut = TryAsync(() => FailingCalculation(Writers).AsTask());

            // Act
            var result = await sut.ToActionResult();

            // Assert
            result.Should().BeOfType<ObjectResult>();
            (result as ObjectResult).StatusCode.Should().Be(500);
        }

        private class Person
        {
            public Person(string first, string last, int age)
            {
                First = first;
                Last = last;
                Age = age;
            }

            public string First { get; }
            public string Last { get; }
            public int Age { get; }
        }

        static Seq<Person> Writers = Seq(
            new Person("Kurt", "Vonnegut", 30),
            new Person("T.S.", "Eliot", 40));

        static double AverageAge(Seq<Person> people) =>
            people.Map(p => p.Age).Average();

        static double FailingCalculation(Seq<Person> people) => 
            people.Map(p => p.Age).Sum() / 0;
    }
}
