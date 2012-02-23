using System;

namespace InsideOutsideUpsideDown
{
    public class Module
    {
        public virtual Guid Id { get; set; }
        public virtual string ModuleCode { get; set; }

        private string moduleXml;
        public virtual string ModuleXml
        {
            get
            {
                if (moduleXml == null)
                {
                    moduleXml = Parse(ModuleCode);
                }
                return moduleXml;
            }
        }

        private string Parse(string moduleCode)
        {
            // Faking out some XML
            return "<root>" + moduleCode + "</root>";
        }
    }
}