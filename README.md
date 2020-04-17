# LanguageExt.AspNetCore

Extension methods for converting from Language Ext Monads (Option, Either, Try) to AspNetCore ActionResult. I found myself re-implementing these basic extensions several times in my own projects.

All extensions methods provide reasonable defaults for the returned ActionResults. Additional mapping functions can be provided for each type.

Available as [LanguageExt.AspNetCore](https://www.nuget.org/packages/LanguageExt.AspNetCore/) Nuget package.

## Option

* `Some<T>` -> `OkObjectResult<T>`
* `None` -> `NotFoundResult`

```C#
// Somewhere in the application:
Option<Person> person = personRepository.Fetch(999);

// In the Controller:
return person.ToActionResult();

// Example of a custom mapper
return person.ToActionResult(none: ServerError);
```

## Either

* `RIGHT` -> `OkObjectResult<RIGHT>`
* `LEFT` -> `ServerError<LEFT>`

`LEFT` might not always represent an error in which case your own mapper function can be provided.

```C#
var result = Right<Error, string>("A successful operation");

// OK "A successful operation"
return result.ToActionResult();
```

## Validation

* `SUCCESS` -> `OkObjectResult<T>`
* `FAIL` -> `BadRequestObjectResult<FAIL>`

```C#
var result = Fail<Error, int>(Error.New("Invalid Request"));

// 400 error: { 'message': 'Invalid Request' }
return result.ToActionResult();

```

## Try

The most basic usage:

* `Success<T>` -> `OkObjectResult<T>`
* `Fail<Exception>` -> `ServerError<Exception>`

```C#
var result = Try(() => AComputationThatCouldFail());

// 200 Ok { ... }
return result.ToActionResult();

```

You may not want to return the exception body and instead log it:

* `Fail<Exception>` -> `ServerError`

```C#
var failed = Try(() => FailingComputation());

// act() is from LanguageExt.Prelude
// logger is Action<Exception>
var logger = act((Exception e) => _logger.LogError(e, $"Failed to execute something id: {id}"));

// 500  
failed.ToActionResult(fail: logger);
```

Additional extensions exist for Async variants of the listed types.
