﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using LanguageExt;
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
    }

    public class PersonDto
    {
        public PersonDto(string first, string last, int age)
        {
            First = first;
            Last = last;
            Age = age;
        }

        public string First { get; set; }
        public string Last { get; set; }
        public int Age { get; set; }
    }
}
