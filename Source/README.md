<p align="center">
  <a href="" rel="noopener">
  <img width="200px" height="200px" src="../Docs/img/logo.jpg" alt="Project logo"></a>
</p>

<h3 align="center">Cogworks Umbraco Essentials</h3>

<div align="center">

[![Project Code](https://img.shields.io/static/v1?label=cog%20umbraco%20essentials&message=cog-umbraco-essentials&color=lightgray&style=flat-square)]() [![Version](https://img.shields.io/static/v1?label=&message=version&color=informational&style=flat-square)](https://github.com/thecogworks/cog-umbraco-essentials/releases) [![License](https://img.shields.io/badge/license-MIT-4c9182.svg)](LICENSE.md)

</div>

---

##### Links for DevOps environments

- Build status -

---

### Code Style

We are using combination of fallow tools:

- [Ryslyn Analyser (FxCopAnalyzers)](https://github.com/dotnet/roslyn-analyzers)
- [StyleCopAnalyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers)
- [EditorConfig](https://github.com/editorconfig/editorconfig/wiki/EditorConfig-Properties)

You need to manual install those packages:

* `StyleCop.Analyzers` version `1.1.118`
* `Microsoft.CodeAnalysis.FxCopAnalyzers` version `3.3.1`
* `Microsoft.CodeQuality.Analyzers` version `3.3.1`
* `Microsoft.NetCore.Analyzers` version `3.3.1`
* `Microsoft.NetFramework.Analyzers` version `3.3.1`
* `Microsoft.CodeAnalysis.VersionCheckAnalyzer` version `3.3.1`
* `SmartanAlyzers.ExceptionAnalyzer` version `1.0.10`

##### How to modify for custom project (override)

For overriding style for dedicated project we need to do the fallowing:

**EditorConfig**:

1. Overriding for all directories

You need to create .editorconfig in root to apply for all directories (if you want to do that for all pages add .editorconfig file in same place where sln file is located).

2. Overriding in dedicated directory f.e. generated models directory

You need to create .editorconfig in dedicated directory with all overridden rules.

3. Overriding rules for dedicated file extensions

You need to create .editorconfig and inside it using templating:

```yml
[*.someExtension.cs]
# here the rules for this file extension
```

More configuration details in files:

- [editorconfig](linting/.editorconfig)
- [codeanalysis](linting/codeanalysis.ruleset)
- [stylecop](linting/stylecop.json)

### Tests

#### Unit Tests

##### Naming Convention

**Class names**:

```csharp
public class (TestedClassName)Tests
```

**Methods names**:

```csharp
public void/Task/ValueTask Should_ExpectedBehaviour_When_StateUnderTest()
```

**Example**:

```csharp
public class RateTests
{
    [Fact]
    public void Should_ThrowRateTypeException_When_DisablingRateOnEmptyProject() => Assert.Throws<RateTypeException>(()
        => new Project(new ProjectId(Guid.NewGuid()), new DateTime(2017, 1, 1))
                .DisableRate("r1", DateTime.Now));
}
```

#### Integration Tests

##### Naming Convention

```csharp
 public class (ClassName)Specs
 {
     //general configuration goes here


     public class Given_SomeArrangements : (ClassName)Specs
     {
         public Task/void Should_SomeAction_SomeOutcome
     }
 }
```

**Example**:

```csharp
public class RatesSpecs : GivenServices
{
    /* Here goes some configuration */

     public class Given_2RateTypes1Overriden : RatesSpecs
        {
            public Given_2RateTypes1Overriden() => Project
                .WithRate(3, Start, name: Day)
                .WithRate(4, Start, WorkerId)
                .Build();

            [Fact]
            public async Task Should_Return_1Default() => (await GetRatesAsync(WorkerId, FirstRateStart, FirstRateEnd))
                .Should().Contain(rate => rate.ClientRate == 3);

            [Fact]
            public async Task Should_NotReturn_OverridenDefault() => (await GetRatesAsync(WorkerId, FirstRateStart, FirstRateEnd))
                .Should().NotContain(rate => rate.ClientRate == 2);
        }

        public class Given_2OverridenRateTypes : RatesSpecs
        {
            public Given_2OverridenRateTypes() => Project
                .WithRate(2, Start, name: Day)
                .WithRate(3, Start, WorkerId, Day)
                .WithRate(3, Start, WorkerId)
                .Build();

            [Fact]
            public async Task Should_NotReturn_Defaults() => (await GetRatesAsync(WorkerId, FirstRateStart, FirstRateEnd))
                .Should().NotContain(rate => rate.ClientRate == 2);
        }

        public class Given_RateStartedBeforeProjectStart : RatesSpecs
        {
            [Fact]
            public void Should_Throw_RateTypeException()
                => Assert.Throws<RateTypeException>(() => Project.WithRate(3, Start.AddDays(-1), name: Day));
        }
}
```
