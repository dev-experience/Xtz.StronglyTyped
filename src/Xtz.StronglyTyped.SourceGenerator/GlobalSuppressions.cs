// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "RCS1194:Implement exception constructors.", Justification = "Vlad DX: Reviewed. It's intended", Scope = "type", Target = "~T:Xtz.StronglyTyped.SourceGenerator.CodeWriterException")]
[assembly: SuppressMessage("Design", "RCS1194:Implement exception constructors.", Justification = "Vlad DX: Reviewed. It's intended", Scope = "type", Target = "~T:Xtz.StronglyTyped.SourceGenerator.SyntaxReceiverException")]
[assembly: SuppressMessage("Major Code Smell", "S108:Nested blocks of code should not be left empty", Justification = "Vlad DX: Reviewed. Source generator", Scope = "member", Target = "~M:Xtz.StronglyTyped.SourceGenerator.StronglyTypedGenerator.WriteClass(Xtz.StronglyTyped.SourceGenerator.CodeWriter,Xtz.StronglyTyped.SourceGenerator.StronglyTypedWorkItem)")]
[assembly: SuppressMessage("Major Code Smell", "S108:Nested blocks of code should not be left empty", Justification = "Vlad DX: Reviewed. Source generator", Scope = "member", Target = "~M:Xtz.StronglyTyped.SourceGenerator.StronglyTypedGenerator.WriteGuidStringConstructor(Xtz.StronglyTyped.SourceGenerator.CodeWriter,Xtz.StronglyTyped.SourceGenerator.StronglyTypedWorkItem)")]
[assembly: SuppressMessage("Major Code Smell", "S108:Nested blocks of code should not be left empty", Justification = "Vlad DX: Reviewed. Source generator", Scope = "member", Target = "~M:Xtz.StronglyTyped.SourceGenerator.StronglyTypedGenerator.WriteStringConstructor(Xtz.StronglyTyped.SourceGenerator.CodeWriter,Xtz.StronglyTyped.SourceGenerator.StronglyTypedWorkItem)")]
[assembly: SuppressMessage("Major Code Smell", "S108:Nested blocks of code should not be left empty", Justification = "Vlad DX: Reviewed. Source generator", Scope = "member", Target = "~M:Xtz.StronglyTyped.SourceGenerator.StronglyTypedGenerator.TryWriteParsingStringConstructor(Xtz.StronglyTyped.SourceGenerator.CodeWriter,Xtz.StronglyTyped.SourceGenerator.StronglyTypedWorkItem)")]
[assembly: SuppressMessage("Minor Code Smell", "S3626:Jump statements should not be redundant", Justification = "Vlad DX: Reviewed", Scope = "member", Target = "~M:Xtz.StronglyTyped.SourceGenerator.StronglyTypedGenerator.TryWriteToString(Xtz.StronglyTyped.SourceGenerator.CodeWriter,Xtz.StronglyTyped.SourceGenerator.StronglyTypedWorkItem)")]
