// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "RCS1194:Implement exception constructors.", Justification = "Vlad DX: Reviewed", Scope = "type", Target = "~T:Xtz.StronglyTyped.InvalidValueException")]
[assembly: SuppressMessage("Design", "RCS1194:Implement exception constructors.", Justification = "Vlad DX: Reviewed", Scope = "type", Target = "~T:Xtz.StronglyTyped.StronglyTypedException")]
[assembly: SuppressMessage("Design", "RCS1194:Implement exception constructors.", Justification = "Vlad DX: Reviewed", Scope = "type", Target = "~T:Xtz.StronglyTyped.TypeConverters.JsonConverterException")]
[assembly: SuppressMessage("Design", "RCS1194:Implement exception constructors.", Justification = "Vlad DX: Reviewed", Scope = "type", Target = "~T:Xtz.StronglyTyped.TypeConverters.StringTypeConverterException")]
[assembly: SuppressMessage("Blocker Code Smell", "S3875:\"operator==\" should not be overloaded on reference types", Justification = "Vlad DX: Reviewed", Scope = "type", Target = "~T:Xtz.StronglyTyped.StronglyTyped`1")]
[assembly: SuppressMessage("Style", "IDE0002:Name can be simplified", Justification = "Vlad DX: Reviewed", Scope = "type", Target = "~T:Xtz.StronglyTyped.StronglyTyped`1")]
[assembly: SuppressMessage("Style", "IDE0034:Simplify 'default' expression", Justification = "Vlad DX: Reviewed. More readable", Scope = "type", Target = "~T:Xtz.StronglyTyped.TypeConverters.StronglyTypedJsonConverter`1")]
[assembly: SuppressMessage("Major Code Smell", "S1066:Collapsible \"if\" statements should be merged", Justification = "Vlad DX: Reviewed. More readable", Scope = "member", Target = "~M:Xtz.StronglyTyped.StronglyTyped`1.ThrowIfInvalid(`0)")]
