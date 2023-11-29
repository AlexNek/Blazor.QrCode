using System.Drawing;

using FluentAssertions;

namespace BlazorQrCode.AppWasm.Test
{
    public class RadzenColorConverterTests
    {
        [Theory]
        [InlineData("rgb(0, 0, 0)", KnownColor.Black)]
        [InlineData("rgb(255, 255, 255)", KnownColor.White)]
        [InlineData("rgb(255, 0, 0)", KnownColor.Red)]
        [InlineData("rgb(0, 255, 0)", KnownColor.Lime)]
        [InlineData("rgb(0, 128, 0)", KnownColor.Green)]
        [InlineData("rgb(0, 0, 255)", KnownColor.Blue)]
        [InlineData("rgb(165, 42, 42)", KnownColor.Brown)]
        public void TestValidValueFroParse(string input, KnownColor expectedColorId)
        {

            // Act
            var result = RadzenColorConverter.Parse(input);
            Color expectedColor = Color.FromKnownColor(expectedColorId);
            // Assert
            //result.R.Should().Be(expectedColor.R);
            //result.G.Should().Be(expectedColor.G);
            //result.B.Should().Be(expectedColor.B);
            result.Should().Match<Color>(
                color =>
                    color.R == expectedColor.R &&
                    color.G == expectedColor.G &&
                    color.B == expectedColor.B);

        }

        [Theory]
        [InlineData(KnownColor.Black, "rgb(0, 0, 0)")]
        [InlineData(KnownColor.White, "rgb(255, 255, 255)")]
        [InlineData(KnownColor.Red, "rgb(255, 0, 0)")]
        [InlineData(KnownColor.Lime, "rgb(0, 255, 0)")]
        [InlineData(KnownColor.Green, "rgb(0, 128, 0)")]
        [InlineData(KnownColor.Blue, "rgb(0, 0, 255)")]
        [InlineData(KnownColor.Brown, "rgb(165, 42, 42)")]
        public void TestValidValuesForCallToString(KnownColor colorId, string expected)
        {
            // Arrange
            Color color = Color.FromKnownColor(colorId);

            // Act
            var result = RadzenColorConverter.ToString(color);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallParseWithInvalidInput(string value)
        {
            FluentActions.Invoking(() => RadzenColorConverter.Parse(value)).Should().Throw<ArgumentException>().WithParameterName("input");
        }
    }
}


