using System.Drawing;

namespace Blazor.QrCode;

public class QrCodeOptions : IEquatable<QrCodeOptions>
{
    /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
    public bool Equals(QrCodeOptions? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Size == other.Size && ColorDark.Equals(other.ColorDark) && ColorLight.Equals(other.ColorLight)
               && ErrorCorrectionLevel == other.ErrorCorrectionLevel;
    }

    /// <summary>Determines whether the specified object is equal to the current object.</summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((QrCodeOptions)obj);
    }

    /// <summary>Serves as the default hash function.</summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Size, ColorDark, ColorLight, (int)ErrorCorrectionLevel);
    }

    public void SetChangedMode(bool changed = true)
    {
        IsChanged = changed;
    }

    public Color ColorDark { get; set; } = DefaultColorDark;

    public Color ColorLight { get; set; } = DefaultColorLight;

    public static Color DefaultColorDark { get; } = Color.Black;

    public static Color DefaultColorLight { get; } = Color.White;

    public static EErrorCorrectionLevel DefaultErrorCorrectionLevel { get; } = EErrorCorrectionLevel.High;

    public static int DefaultSize { get; } = 256;

    public EErrorCorrectionLevel ErrorCorrectionLevel { get; set; } = DefaultErrorCorrectionLevel;

    public int Size { get; set; } = DefaultSize;

    internal bool IsChanged { get; private set; }
}
