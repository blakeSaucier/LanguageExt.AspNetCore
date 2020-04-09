using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace LanguageExt.AspNetCore.Test
{
    public class OptionExtensionTest
    {
        [Test]
        public void OptionToResult_OptionSome_ShouldProduceOkResponse()
        {
            // Arrange
            var option = Optional(new PersonDto("James", "Test", 30));

            // Act
            var result = option.ToActionResult();

            // Assert
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            var okObjectResult = result as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }

        [Test]
        public void OptionToResult_OptionNone_ShouldProduceNotFound()
        {
            // Arrange
            Option<PersonDto> option = None;

            // Act
            var result = option.ToActionResult();

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
            var notFoundResult = result as NotFoundResult;
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public async Task OptionTaskToResult_OptionSome_ShouldProduceOkResult()
        {
            // Arrange
            var option = Optional(new PersonDto("James", "Test", 30))
                .AsTask();

            // Act
            var result = await option.ToActionResult();

            // Assert
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            var okObjectResult = result as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }

        [Test]
        public async Task OptionTaskToResult_OptionNone_ShouldProduceNotFound()
        {
            // Arrange
            Option<PersonDto> option = None;

            // Act
            var result = await option.AsTask().ToActionResult();

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
            var notFoundResult = result as NotFoundResult;
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public async Task OptionAsyncToResult_OptionSome_ShouldProduceOkResponse()
        {
            // Arrange
            var personDto = new PersonDto("James", "Test", 30);
            var option = OptionalAsync(Task.FromResult(personDto));

            // Act
            var result = await option.ToActionResult();

            // Assert
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            var okObjectResult = result as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }

        [Test]
        public void OptionToJsonResult_OptionSome_ShouldBeOk()
        {
            // Arrange
            var personDto = new PersonDto("James", "Test", 30);
            var option = Optional(personDto);

            // Act
            var result = option.ToJsonResult();

            // Assert
            result.Should().BeOfType<JsonResult>();
            (result as JsonResult).StatusCode.Should().Be(200);
        }

        [Test]
        public async Task OptionTaskToJsonResult_OptionSome_ShouldBeOk()
        {
            // Arrange
            var personDto = new PersonDto("James", "Test", 30);
            var option = Optional(personDto).AsTask();

            // Act
            var result = await option.ToJsonResult();

            // Assert
            result.Should().BeOfType<JsonResult>();
            (result as JsonResult).StatusCode.Should().Be(200);
        }
    }

    public class PersonDto
    {
        public PersonDto(string first, string last, int age)
        {
            First = first;
            Last = last;
            Age = age;
        }

        public string First { get; }
        public string Last { get; }
        public int Age { get; }
    }
}
