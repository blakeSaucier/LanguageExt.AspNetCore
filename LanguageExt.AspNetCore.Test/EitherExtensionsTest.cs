using FluentAssertions;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace LanguageExt.AspNetCore.Test
{
    public class EitherExtensionsTest
    {
        [Test]
        public void EitherRight_ShouldProduceOk()
        {
            // Arrange
            var right = Right<Error, string>("alt");

            // Act
            var result = right.ToActionResult();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }

        [Test]
        public void EitherLeft_ShouldProduceError()
        {
            // Arrange
            var left = Left<Error, string>(Error.New("Alt"));

            // Act
            var result = left.ToActionResult();

            // Assert
            result.Should().BeOfType<ObjectResult>();
            (result as ObjectResult).StatusCode.Should().Be(500);
        }

        [Test]
        public void EitherLeft_MapLeft()
        {
            // Arrange
            var left = Left<Error, string>(Error.New("Alt"));

            // Act
            var result = left.ToActionResult(e => new StatusCodeResult(500));

            result.Should().BeOfType<StatusCodeResult>();
            (result as StatusCodeResult).StatusCode.Should().Be(500);
        }

        [Test]
        public void EitherLeft_MapBoth()
        {
            // Arrange
            var left = Left<Error, string>(Error.New("Alt"));

            // Act
            var result = left.ToActionResult(
                e => new StatusCodeResult(400),
                res => new StatusCodeResult(StatusCodes.Status201Created));

            result.Should().BeOfType<StatusCodeResult>();
            (result as StatusCodeResult).StatusCode.Should().Be(400);
        }
        [Test]
        public async Task TaskEitherRight_ShouldProduceOk()
        {
            // Arrange
            var right = Right<Error, string>("alt").AsTask();

            // Act
            var result = await right.ToActionResult();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }

        [Test]
        public async Task TaskEitherLeft_ShouldProduceError()
        {
            // Arrange
            var left = Left<Error, string>(Error.New("Alt")).AsTask();

            // Act
            var result = await left.ToActionResult();

            // Assert
            result.Should().BeOfType<ObjectResult>();
            (result as ObjectResult).StatusCode.Should().Be(500);
        }

        [Test]
        public async Task TaskEitherLeft_MapLeft()
        {
            // Arrange
            var left = Left<Error, string>(Error.New("Alt")).AsTask();

            // Act
            var result = await left.ToActionResult(e => new StatusCodeResult(500));

            result.Should().BeOfType<StatusCodeResult>();
            (result as StatusCodeResult).StatusCode.Should().Be(500);
        }

        [Test]
        public async Task TaskEitherLeft_MapBoth()
        {
            // Arrange
            var left = Left<Error, string>(Error.New("Alt")).AsTask();

            // Act
            var result = await left.ToActionResult(
                e => new StatusCodeResult(400),
                res => new StatusCodeResult(StatusCodes.Status201Created));

            // Assert
            result.Should().BeOfType<StatusCodeResult>();
            (result as StatusCodeResult).StatusCode.Should().Be(400);
        }

        [Test]
        public async Task EitherAsyncLeft_DefaultActionResult_ServerError()
        {
            // Arrange
            var left = LeftAsync<Error, string>(Error.New("Alt").AsTask());

            // Act
            var result = await left.ToActionResult();

            // Assert
            result.Should().BeOfType<ObjectResult>();
            (result as ObjectResult).StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Test]
        public async Task EitherAsyncRight_MapRight_Ok()
        {
            // Arrange
            var right = RightAsync<Error, string>("So it goes".AsTask());

            // Act
            var result = await right.ToActionResult(
                right: r => new CreatedResult("/slaughterhouse", r),
                left: l => new BadRequestObjectResult("Invalid quote"));

            // Assert
            result.Should().BeOfType<CreatedResult>();
            (result as CreatedResult).StatusCode.Should().Be(StatusCodes.Status201Created);
        }
    }
}
