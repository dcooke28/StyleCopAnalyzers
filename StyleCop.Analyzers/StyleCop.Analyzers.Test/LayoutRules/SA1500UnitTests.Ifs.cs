﻿namespace StyleCop.Analyzers.Test.LayoutRules
{
    using System.Threading;
    using System.Threading.Tasks;

    using StyleCop.Analyzers.LayoutRules;
    using TestHelper;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="SA1500CurlyBracketsForMultiLineStatementsMustNotShareLine"/>.
    /// </summary>
    public partial class SA1500UnitTests : DiagnosticVerifier
    {
        /// <summary>
        /// Verifies that no diagnostics are reported for the valid if statements defined in this test.
        /// </summary>
        /// <remarks>
        /// These are valid for SA1500 only, some will report other diagnostics from the layout (SA15xx) series.
        /// </remarks>
        [Fact]
        public async Task TestIfValid()
        {
            var testCode = @"public class Foo
{
    private void Bar()
    {
        var x = 0;

        // Valid if #1
        if (x == 0)
        {
        }

        // Valid if #2
        if (x == 0)
        {
            x = 1;
        }

        // Valid if #3 (Valid only for SA1500)
        if (x == 0) { }

        // Valid if #4 (Valid only for SA1500)
        if (x == 0) { x = 1; }
    }
}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that no diagnostics are reported for the valid if ... else statements defined in this test.
        /// </summary>
        /// <remarks>
        /// These are valid for SA1500 only, some will report other diagnostics from the layout (SA15xx) series.
        /// </remarks>
        [Fact]
        public async Task TestIfElseValid()
        {
            var testCode = @"public class Foo
{
    private void Bar()
    {
        var x = 0;

        // Valid if ... else #1
        if (x == 0)
        {
        }
        else
        {
        }

        // Valid if ... else #2
        if (x == 0)
        {
        }
        else
        {
            x = 1;
        }

        // Valid if ... else #3 (Valid only for SA1500)
        if (x == 0)
        {
        }
        else { }

        // Valid if ... else #4 (Valid only for SA1500)
        if (x == 0) 
        {
        }
        else { x = 1; }
    }
}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that diagnostics will be reported for all invalid if statement definitions.
        /// </summary>
        [Fact(Skip = "Disabled until the SA1500 implementation is available")]
        public async Task TestIfInvalid()
        {
            var testCode = @"public class Foo
{
    private void Bar()
    {
        var x = 0;

        // Invalid if #1
        if (x == 0) {
        }

        // Invalid if #2
        if (x == 0) {
            x = 1;
        }

        // Invalid if #3
        if (x == 0) {
            x = 1; }

        // Invalid if #4
        if (x == 0) { x = 1;
        }

        // Invalid if #5
        if (x == 0)
        {
            x = 1; }

        // Invalid if #6
        if (x == 0)
        { x = 1;
        }

        // Invalid if #7
        if (x == 0)
        { x = 1; }
    }
}";

            var expectedDiagnostics = new[]
            {
                // Invalid if #1
                this.CSharpDiagnostic().WithLocation(8, 21),
                // Invalid if #2
                this.CSharpDiagnostic().WithLocation(12, 21),
                // Invalid if #3
                this.CSharpDiagnostic().WithLocation(17, 21),
                this.CSharpDiagnostic().WithLocation(18, 20),
                // Invalid if #4
                this.CSharpDiagnostic().WithLocation(21, 21),
                // Invalid if #5
                this.CSharpDiagnostic().WithLocation(27, 20),
                // Invalid if #6
                this.CSharpDiagnostic().WithLocation(31, 9),
                // Invalid if #7
                this.CSharpDiagnostic().WithLocation(36, 9),
                this.CSharpDiagnostic().WithLocation(36, 18)
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expectedDiagnostics, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that diagnostics will be reported for all invalid if ... else statement definitions.
        /// </summary>
        [Fact(Skip = "Disabled until the SA1500 implementation is available")]
        public async Task TestIfElseInvalid()
        {
            var testCode = @"public class Foo
{
    private void Bar()
    {
        var x = 0;

        // Invalid if ... else #1
        if (x == 0)
        {
        }
        else {
        }

        // Invalid if ... else #2
        if (x == 0)
        {
        }
        else {
            x = 1;
        }

        // Invalid if ... else #3
        if (x == 0)
        {
        }
        else {
            x = 1; }

        // Invalid if ... else #4
        if (x == 0)
        {
        }
        else { x = 1;
        }

        // Invalid if ... else #5
        if (x == 0)
        {
        }
        else
        {
            x = 1; }

        // Invalid if ... else #6
        if (x == 0)
        {
        }
        else
        { x = 1;
        }

        // Invalid if ... else #7
        if (x == 0)
        {
        }
        else
        { x = 1; }
    }
}";

            var expectedDiagnostics = new[]
            {
                // Invalid if ... else #1
                this.CSharpDiagnostic().WithLocation(11, 14),
                // Invalid if ... else #2
                this.CSharpDiagnostic().WithLocation(18, 14),
                // Invalid if ... else #3
                this.CSharpDiagnostic().WithLocation(26, 14),
                this.CSharpDiagnostic().WithLocation(27, 20),
                // Invalid if ... else #4
                this.CSharpDiagnostic().WithLocation(33, 14),
                // Invalid if ... else #5
                this.CSharpDiagnostic().WithLocation(42, 20),
                // Invalid if ... else #6
                this.CSharpDiagnostic().WithLocation(49, 9),
                // Invalid if ... else #7
                this.CSharpDiagnostic().WithLocation(57, 9),
                this.CSharpDiagnostic().WithLocation(57, 18)
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expectedDiagnostics, CancellationToken.None).ConfigureAwait(false);
        }
    }
}