using System;
using System.ComponentModel.DataAnnotations;

namespace HelloApi.Models
{
	public class TodoItem
	{
        public long Id { get; set; } //unique key
        public string? Name { get; set; }
        public bool IsComplete { get; set; }

        [Required]
        public int Prority { get; set; }
    }
}

