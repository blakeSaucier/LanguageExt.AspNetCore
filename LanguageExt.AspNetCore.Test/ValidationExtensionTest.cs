using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace LanguageExt.AspNetCore.Test
{
    public class ValidationExtensionTest
    {
        [Test]
        public void ValidationToResult_Success_ShouldProductOkResponse()
        {
            // Arrange
            var success = fun((int i) => Success<string, int>(i));

            // Act
            var result = success(42).ToActionResult();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.Should().Be(200);
        }

        [Test]
        public void ValidationToResult_Fail_ShouldProductOkResponse()
        {
            // Arrange
            var fail = fun((string e) => Fail<string, int>(e));

            // Act
            var result = fail("Invalid request").ToActionResult();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var okObjectResult = result as BadRequestObjectResult;
            okObjectResult.StatusCode.Should().Be(400);
        }

        [Test]
        public async Task TaskValidationToResult_Success_ShouldProductOkResponse()
        {
            // Arrange
            var success = fun((int i) => Success<string, int>(i).AsTask());

            // Act
            var result = await success(42).ToActionResult();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task ValidationInnerTaskToResult_Success_ShouldProductOkResponse()
        {
            // Arrange
            var success = fun((int i) => Success<string, Task<int>>(i.AsTask()));

            // Act
            var result = await success(42).ToActionResult();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.Should().Be(200);
        }
    }
}
