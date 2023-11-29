using FluentAssertions;

namespace Blazor.QrCode.Tests
{
    using System;
    using System.Drawing;
    using Blazor.QrCode;
    using Xunit;

    public static class ColorConverterTests
    {
        [Theory]
        [InlineData(KnownColor.White, "#ffffff")]
        [InlineData(KnownColor.Black, "#000000")]
        [InlineData(KnownColor.Red, "#ff0000")]
        [InlineData(KnownColor.Lime, "#00ff00")]
        [InlineData(KnownColor.Blue, "#0000ff")]
        [InlineData(KnownColor.Cyan, "#00ffff")]
        [InlineData(KnownColor.Yellow, "#ffff00")]
        [InlineData(KnownColor.Magenta, "#ff00ff")]
        public static void TestConversion(KnownColor colorId, string expected)
        {
            // Arrange
            Color color = Color.FromKnownColor(colorId);

            // Act

            var result = Blazor.QrCode.ColorConverter.Convert(color);

            // Assert
            result.Should().Be(expected);
        }
    }
}