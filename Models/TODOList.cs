using System.ComponentModel.DataAnnotations;

namespace Proiect_MythicTask.Models
{
    public class TODOList
    {
        [Key]
        public int NrCrit { get; set; }
        [Required]
        public string Obiectiv { get; set;}

        public string Descriere { get; set;}

        public string Locație { get; set;}

        public DateTime Deadline { get; set; } = DateTime.Now;

        public TODOList()
        {
                
        }
    }
}
