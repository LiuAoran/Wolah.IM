using System;

namespace Wolah.IM.Helper;

public static class EnumHelper
{
    /// <summary>
    /// Returns the value of the enum as an int
    /// </summary>
    public static int ToInt(this Enum value)
    {
        return (int)(object)value;
    }
}