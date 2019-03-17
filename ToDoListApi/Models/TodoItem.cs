
namespace TareaApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;


    public class TodoItem
    {   
        [Key]
        public string ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Notes { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndData { get; set; }
        [Required]
        public bool Done { get; set; }
    }
}
