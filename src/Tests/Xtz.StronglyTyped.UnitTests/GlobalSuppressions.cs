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
