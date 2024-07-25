namespace Template.Api.Attributes;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IgnoreGlobalTransactionsAttribute : Attribute
{
}