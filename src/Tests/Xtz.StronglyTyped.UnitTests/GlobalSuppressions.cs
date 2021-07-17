// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1806:Do not ignore method results", Justification = "Vlad DX: Reviewed (unit tests)", Scope = "member", Target = "~M:Xtz.StronglyTyped.UnitTests.Basic.CountryIdTests.CountryId_ShouldThrow_GivenInvalidValue(System.Int32)")]
[assembly: SuppressMessage("Performance", "CA1806:Do not ignore method results", Justification = "Vlad DX: Reviewed (unit tests)", Scope = "member", Target = "~M:Xtz.StronglyTyped.UnitTests.Values.CreationTests.ShouldThrow_GivenNullOrEmpty(System.String)")]
[assembly: SuppressMessage("Performance", "CA1806:Do not ignore method results", Justification = "Vlad DX: Reviewed (unit tests)", Scope = "member", Target = "~M:Xtz.StronglyTyped.UnitTests.Basic.UserIdTests.UserId_ShouldThrow_GivenInvalidValue")]
[assembly: SuppressMessage("Performance", "CA1806:Do not ignore method results", Justification = "Vlad DX: Reviewed (unit tests)", Scope = "member", Target = "~M:Xtz.StronglyTyped.UnitTests.Values.CreationTests.ShouldThrow_GivenNull_ForAllowEmpty")]
[assembly: SuppressMessage("Assertion", "NUnit2021:Incompatible types for EqualTo constraint", Justification = "Vlad DX: Reviewed (unit tests)", Scope = "member", Target = "~M:Xtz.StronglyTyped.UnitTests.Basic.UserIdTests.UserId_ShouldInstantiate_GivenGuid")]
[assembly: SuppressMessage("Minor Code Smell", "S3241:Methods should not return values that are never used", Justification = "Vlad DX: Reviewed (unit tests)", Scope = "member", Target = "~M:Xtz.StronglyTyped.UnitTests.Basic.CountryIdTests.CountryIdImplicitOperator_ShouldThrow_GivenDifferentType(System.Object)")]
[assembly: SuppressMessage("Assertion", "NUnit2010:Use EqualConstraint for better assertion messages in case of failure", Justification = "Vlad DX: Reviewed (unit tests)", Scope = "member", Target = "~M:Xtz.StronglyTyped.UnitTests.Basic.StringEqualityOperatorsTests.EqualsOperator_ShouldBeTrue_ForSameStronglyTypedValues")]
[assembly: SuppressMessage("Major Bug", "S1764:Identical expressions should not be used on both sides of a binary operator", Justification = "Vlad DX: Reviewed (unit tests)", Scope = "member", Target = "~M:Xtz.StronglyTyped.UnitTests.Basic.StringEqualityOperatorsTests.EqualsOperator_ShouldBeTrue_ForSameStronglyTypedValues")]
[assembly: SuppressMessage("Assertion", "NUnit2010:Use EqualConstraint for better assertion messages in case of failure", Justification = "Vlad DX: Reviewed (unit tests)", Scope = "type", Target = "~T:Xtz.StronglyTyped.UnitTests.Values.EqualityOperatorsTests")]
[assembly: SuppressMessage("Assertion", "NUnit2010:Use EqualConstraint for better assertion messages in case of failure", Justification = "Vlad DX: Reviewed (unit tests)", Scope = "type", Target = "~T:Xtz.StronglyTyped.UnitTests.Basic.StringEqualityOperatorsTests")]
