using System;

namespace FreeLoader.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ScriptExecutionOrderAttribute : Attribute
    {
        private int order = 0;

        public ScriptExecutionOrderAttribute(int order)
        {
            this.order = order;
        }

        public int GetOrder()
        {
            return order;
        }
    }
}
