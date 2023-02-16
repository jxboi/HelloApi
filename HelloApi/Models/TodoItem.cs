using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace HelloApi.Models
{
	public class TodoItem
	{
        public long Id { get; set; } //unique key
        public string? Name { get; set; }
        public bool IsComplete { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Please enter a value between 1 - 10")]
        public int Prority { get; set; }
    }

    public class TodoItemValidator : AbstractValidator<TodoItem>
    {
        public TodoItemValidator()
        {
            RuleFor(x => x.Name).Length(0, 10);
        }

    }
}

