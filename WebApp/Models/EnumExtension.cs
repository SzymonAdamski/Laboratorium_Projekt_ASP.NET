using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

public static class EnumExtension
{
    public static string GetDisplayName(this Enum enumValue)
    {
        // Get the type of the enum
        var type = enumValue.GetType();

        // Get the member info for the specific enum value
        var memberInfo = type.GetMember(enumValue.ToString());

        if (memberInfo != null && memberInfo.Length > 0)
        {
            // Try to get the Display attribute on the member
            var displayAttribute = memberInfo[0].GetCustomAttribute<DisplayAttribute>();

            if (displayAttribute != null)
            {
                return displayAttribute.Name;
            }
        }

        // If no Display attribute is found, return the default string representation
        return enumValue.ToString();
    }
}