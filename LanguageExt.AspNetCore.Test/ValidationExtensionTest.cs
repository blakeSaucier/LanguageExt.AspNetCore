using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using static LanguageExt.Prelude;
using LanguageExt.Common;

namespace LanguageExt.AspNetCore.Test
{
    public class ValidationExtensionTest
    {
        [Test]
        public void ValidationToResult_Success_ShouldProduceOkResponse()
        {
            // Arrange
            var success = Success<string, int>(42);

            // Act
            var result = success.ToActionResult();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.Should().Be(200);
        }

        [Test]
        public void ValidationToResult_Fail_ShouldProduceBadRequest()
        {
            // Arrange
            var failed = Fail<Error, int>(Error.New("InvalidRequest"));

            // Act
            var result = failed.ToActionResult();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var okObjectResult = result as BadRequestObjectResult;
            okObjectResult.StatusCode.Should().Be(400);
        }

        [Test]
        public async Task TaskValidationToResult_Success_ShouldProduceOkResponse()
        {
            // Arrange
            var success = Success<Error, int>(42).AsTask();

            // Act
            var result = await success.ToActionResult();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task ValidationInnerTaskToResult_Success_ShouldProduceOkResponse()
        {
            // Arrange
            var success = Success<string, Task<int>>(42.AsTask());

            // Act
            var result = await success.ToActionResult();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task ValidationInnerTask_Fail_ShouldProductBadRequest()
        {
            // Arrange
            var fail = Fail<Task<Error>, int>(Error.New("Error").AsTask());

            // Act
            var result = await fail.ToActionResult();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var okObjectResult = result as BadRequestObjectResult;
            okObjectResult.StatusCode.Should().Be(400);
        }
    }
}
