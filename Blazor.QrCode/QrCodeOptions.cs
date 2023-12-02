using System.Drawing;

namespace Blazor.QrCodeGen;

public class QrCodeOptions : IEquatable<QrCodeOptions>
{
    private Color _colorDark = DefaultColorDark;

    private Color _colorLight = DefaultColorLight;

    private EErrorCorrectionLevel _errorCorrectionLevel = DefaultErrorCorrectionLevel;

    private int _size = DefaultSize;

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

    //public void SetChangedMode(bool changed = true)
    //{
    //    IsModified = changed;
    //}

    public Color ColorDark
    {
        get
        {
            return _colorDark;
        }
        set
        {
            if (_colorDark != value)
            {
                _colorDark = value;
                IsModified = true;
            }
        }
    }

    public Color ColorLight
    {
        get
        {
            return _colorLight;
        }
        set
        {
            if (_colorLight != value)
            {
                _colorLight = value;
                IsModified = true;
            }
        }
    }

    public static Color DefaultColorDark { get; } = Color.Black;

    public static Color DefaultColorLight { get; } = Color.White;

    public static EErrorCorrectionLevel DefaultErrorCorrectionLevel { get; } = EErrorCorrectionLevel.High;

    public static int DefaultSize { get; } = 256;

    public EErrorCorrectionLevel ErrorCorrectionLevel
    {
        get
        {
            return _errorCorrectionLevel;
        }
        set
        {
            if (_errorCorrectionLevel != value)
            {
                _errorCorrectionLevel = value;
                IsModified = true;
            }
        }
    }

    public int Size
    {
        get
        {
            return _size;
        }
        set
        {
            if (_size != value)
            {
                _size = value;
                IsModified = true;
            }
        }
    }

    internal bool IsModified { get; private set; }
}
