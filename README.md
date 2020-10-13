# CloudEvents
A .NET Standard 2.1 library that extends [CloudEvents](https://github.com/cloudevents/sdk-csharp)

# Usage

[Nuget Package](https://www.nuget.org/packages/Neuroglia.CloudEvents/)

```
  dotnet add package Neuroglia.CloudEvents
```

## Sample Code

### Create a new CloudEvent

```C#
var builder = new CloudEventBuilder();
var e = builder.WithType("MyEventType")
  .WithSubject("MySubject")
  .WithSource(new Uri("/MySource", UriKind.RelativeOrAbsolute))
  .WithData(JsonConvert.SerializeObject(new { Message = "Hello, World!" }), new ContentType(MediaTypeNames.Application.Json))
  .Build();
```

# Contributing

Please see [CONTRIBUTING.md](https://github.com/neuroglia-io/CloudEvents/blob/master/CONTRIBUTING.md) for instructions on how to contribute.
