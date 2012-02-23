using System;

namespace InsideOutsideUpsideDown
{
    public class Module
    {
        public virtual Guid Id { get; set; }
        public virtual ModuleCode ModuleCode { get; set; }

        private string moduleXml;
        public virtual string ModuleXml
        {
            get
            {
                // CW: ModuleProxy.ModuleCode -> initialized? -> DB -> Module.ModuleCode
                // Module: this.ModuleCode -> null
                if (moduleXml == null)
                {
                    moduleXml = Parse(ModuleCode.Value);
                }
                return moduleXml;
            }
        }

        private string Parse(string moduleCode)
        {
            if (moduleCode == null) return null;
            // Faking out some XML
            return "<root>" + moduleCode + "</root>";
        }
    }

    public class ModuleCode
    {
        public string Value { get; set; }
    }
}