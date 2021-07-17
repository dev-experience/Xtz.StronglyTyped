// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "RCS1194:Implement exception constructors.", Justification = "Vlad DX: Reviewed", Scope = "type", Target = "~T:Xtz.StronglyTyped.SourceGenerator.IntegrationTests.GeneratorTestsException")]
[assembly: SuppressMessage("Design", "RCS1194:Implement exception constructors.", Justification = "Vlad DX: Reviewed", Scope = "type", Target = "~T:Xtz.StronglyTyped.SourceGenerator.IntegrationTests.TestsExecutionException")]
[assembly: SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors", Justification = "Vlad DX: Reviewed", Scope = "type", Target = "~T:Xtz.StronglyTyped.SourceGenerator.IntegrationTests.GeneratorTestsBase")]
[assembly: SuppressMessage("Performance", "RCS1197:Optimize StringBuilder.Append/AppendLine call.", Justification = "Vlad DX: Reviewed. More readable", Scope = "member", Target = "~M:Xtz.StronglyTyped.SourceGenerator.IntegrationTests.GeneratorTestsBase.CompileBytes(Microsoft.CodeAnalysis.Compilation)~System.Byte[]")]
[assembly: SuppressMessage("Major Code Smell", "S4144:Methods should not have identical implementations", Justification = "Vlad DX: Reviewed. It's intentional", Scope = "member", Target = "~M:Xtz.StronglyTyped.SourceGenerator.IntegrationTests.InnerTypesGeneratorTests.ShouldGenerateClass_ForKnownTypes(System.Type)")]
[assembly: SuppressMessage("Major Code Smell", "S4144:Methods should not have identical implementations", Justification = "Vlad DX: Reviewed. It's intentional", Scope = "member", Target = "~M:Xtz.StronglyTyped.SourceGenerator.IntegrationTests.InnerTypesGeneratorTests.ShouldGenerateStruct_ForKnownTypes(System.Type)")]
