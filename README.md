[![NuGet](https://img.shields.io/nuget/v/PMart.DeveloperTools.CoreMask.svg)](https://www.nuget.org/packages/PMart.DeveloperTools.CoreMask)
[![NuGet](https://img.shields.io/nuget/dt/PMart.DeveloperTools.CoreMask.svg)](https://www.nuget.org/packages/PMart.DeveloperTools.CoreMask)
[![Build status](https://github.com/p-martinho/DeveloperTools/actions/workflows/publish.yml)](https://github.com/p-martinho/DeveloperTools/actions/workflows/publish.yml/badge.svg)

# PMart.DeveloperTools

This set of libraries and applications provides different simple tools that may be useful for developers. The tools can also be used in .NET applications, they are available as NuGet packages.

## NuGet Packages

[__PMart.DeveloperTools.CoreMask__](./src/CoreMask/README.md): Tools related with core masking.
[![NuGet](https://img.shields.io/nuget/v/PMart.DeveloperTools.CoreMask.svg)](https://www.nuget.org/packages/PMart.DeveloperTools.CoreMask)

# Installation

Install one or more of the available NuGet packages in your project.

Use your IDE or the command:
```bash
dotnet add package <PACKAGE_NAME>
```

# Usage

For specific usage details, read the documentation of the tools you want to use:

- [CoreMask](./src/CoreMask/README.md)



# TODO

- Remove the Microsoft.NET.Test.Sdk package and check if the tests are not marked as unused and if code coverage works (wait for Rider fix)
- Update to .NET 10 (when stable)
    - Update the tests for MTP mode:
        - https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test#microsofttestingplatform-mtp-mode-of-dotnet-test (removing, for instance, <TestingPlatformDotnetTestSupport>true</TestingPlatformDotnetTestSupport> may not be possible with Rider)
        - https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test?tabs=dotnet-test-with-mtp#vstest-and-microsofttestingplatform-mtp
        - Update Github flow (dotnet test command)
    - "allowPrerelease": false in global.json, when .NET 10 is no longer preview
- Update dependencies
- Pipeline to pack and publish