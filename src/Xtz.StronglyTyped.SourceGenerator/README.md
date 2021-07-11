## IMPORTANT

**Despite having a reference to `Xtz.StronglyTyped` project do not use any type from it**.

In such a case generator will start to fail with an exception:

> "Could not load file or assembly 'Xtz.StronglyTyped, Version=0.X.Y.Z, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified."


**Note**:

> July 9, 2021
> 
> Source Generators references are not being copied to target directories when analyzer is being used by Visual Studio.
>
> You can find all analyzers loaded to Visual Studio there:
>
> %Temp%\VS\AnalyzerAssemblyLoader

A reference in the `csproj` file is there to have an auto-generated dependency in `Xtz.StronglyTyped.SourceGenerator` NuGet package (when `Xtz.StronglyTyped` NuGet package is generated).
