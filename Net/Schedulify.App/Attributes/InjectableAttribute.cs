using Schedulify.App.Enums;

namespace Schedulify.App.Attributes;

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
public class InjectableAttribute: Attribute
{
    public readonly Type InterfaceDefinition;
    public readonly InjectableTypeEnum InjectableType;
    
    public InjectableAttribute(InjectableTypeEnum injectableType, Type instanceDefinition)
    {
        this.InjectableType = injectableType;
        this.InterfaceDefinition = instanceDefinition;
    }
}