using System.Collections.Generic;
using System.Text;

namespace Coding.Exercise
{
    public class CodeBuilder
    {
        private string _className;
        private List<Field> fields = new List<Field>();

        internal class Field
        {
            public string Name { get; set; }
            public string Type { get; set; }

            public override string ToString()
            {
                return $"  public {Type} {Name};";
            }
        }

        public CodeBuilder(string className)
        {
            _className = className;
        }

        public CodeBuilder AddField(string name, string type)
        {
            fields.Add(new Field() { Name = name, Type = type});
            return this;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"public class {_className}");
            sb.AppendLine("{");
            foreach (var field in fields)
            {
                sb.AppendLine(field.ToString());
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}