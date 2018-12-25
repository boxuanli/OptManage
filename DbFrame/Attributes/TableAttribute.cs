using System;
namespace DbFrame.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {

        public string TableName = string.Empty;

        public TableAttribute(string _TableName)
        {
            this.TableName = _TableName;
        }

    }
}
