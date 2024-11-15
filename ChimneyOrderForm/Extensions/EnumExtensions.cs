using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ChimneyOrderForm.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        DisplayAttribute? attribute = enumValue.GetType()
            .GetMember(enumValue.ToString())[0]
            .GetCustomAttribute<DisplayAttribute>();

        return attribute?.GetName() ?? enumValue.ToString();
    }
}