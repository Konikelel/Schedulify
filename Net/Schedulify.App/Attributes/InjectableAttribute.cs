using Schedulify.App.Enums;

namespace Schedulify.App.Attributes;

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
public class InjectableAttribute : Attribute
{
    public readonly Type Implementation;
    public readonly InjectableTypeEnum InjectableType;
    
    public InjectableAttribute(InjectableTypeEnum injectableType, Type implementation)
    {
        this.InjectableType = injectableType;
        this.Implementation = implementation;
    }
}