using FluentAssertions;
using Microsoft.AspNetCore.Http;
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
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }

        [Test]
        public void OptionToResult_MapSome_ShouldBeOK()
        {
            // Arrange
            var option = Optional(new PersonDto("James", "Test", 30));

            // Act
            var result = option.ToActionResult(
                some: p => new CreatedResult("/people/", p),
                none: () => new StatusCodeResult(StatusCodes.Status400BadRequest));

            // Assert
            result.Should().BeOfType<CreatedResult>();
            (result as CreatedResult).StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Test]
        public void OptionToResult_OptionNone_ShouldProduceNotFound()
        {
            // Arrange
            Option<PersonDto> option = None;

            // Act
            var result = option.ToActionResult();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            (result as NotFoundResult).StatusCode.Should().Be(404);
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
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }

        [Test]
        public async Task OptionTaskToResult_OptionNone_ShouldProduceNotFound()
        {
            // Arrange
            Option<PersonDto> option = None;

            // Act
            var result = await option.AsTask().ToActionResult();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            (result as NotFoundResult).StatusCode.Should().Be(404);
        }

        [Test]
        public async Task OptionTaskToResult_MapFail_ShouldProduceNotFound()
        {
            // Arrange
            Option<PersonDto> option = None;

            // Act
            var result = await option.AsTask().ToActionResult(
                none: () => new StatusCodeResult(StatusCodes.Status500InternalServerError));

            // Assert
            result.Should().BeOfType<StatusCodeResult>();
            (result as StatusCodeResult).StatusCode.Should().Be(500);
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
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }

        [Test]
        public async Task OptionAsyncToResult_MapSome_ShouldProduceOkResponse()
        {
            // Arrange
            var personDto = new PersonDto("James", "Test", 30);
            var option = OptionalAsync(Task.FromResult(personDto));

            // Act
            var result = await option.ToActionResult(
                some: p => new CreatedResult("/people/30", p),
                none: () => new StatusCodeResult(StatusCodes.Status500InternalServerError));

            // Assert
            result.Should().BeOfType<CreatedResult>();
            (result as CreatedResult).StatusCode.Should().Be(StatusCodes.Status201Created);
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
