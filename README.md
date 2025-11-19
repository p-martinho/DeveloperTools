[![NuGet](https://img.shields.io/nuget/v/PMart.DeveloperTools.CoreMask.svg)](https://www.nuget.org/packages/PMart.DeveloperTools.CoreMask)
[![NuGet](https://img.shields.io/nuget/dt/PMart.DeveloperTools.CoreMask.svg)](https://www.nuget.org/packages/PMart.DeveloperTools.CoreMask)
[![Build status](https://github.com/p-martinho/DeveloperTools/actions/workflows/publish.yaml/badge.svg)](https://github.com/p-martinho/DeveloperTools/actions/workflows/publish.yaml)

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

# Unit Tests and Code Coverage

For unit testing, the solution uses the XUnit v3 framework, with the Microsoft Testing Platform (MTP) v2 enabled.

To run the unit tests and assess the code coverage, and if your IDE does not have a tool for it, follow these instructions:

1. Install (if not already) the **ReportGenerator** tool:

    ``` bash
    dotnet tool install dotnet-reportgenerator-globaltool --global
    ```

2. Run the tests with code coverage enabled. Run this command in the **root folder** of the solution:

    ``` bash
    dotnet test --solution DeveloperTools.slnx --coverage --coverage-output-format cobertura --coverage-output coverage.cobertura.xml --coverage-settings ./tests/CodeCoverage-settings.xml
    ```

3. Use the **ReportGenerator** tool to create HTML from the XML coverage files. Run this command in the **root folder** of the solution:

    ``` bash
    ReportGenerator -reports:**/coverage.cobertura.xml -targetdir:CoverageReport
    ```

4. Open the HTML file `CoverageReport\index.html` to see the results.

✅ The solution has **100%** code coverage.
