using System.ComponentModel.DataAnnotations.Schema;

namespace Production.Models
{
    [Table("CRTemplates")]
    public class Template
    {
        public int Id { get; set; }
        public int Department { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public List<TemplateOperation>? TemplateOperations { get; set; }
    }
}
