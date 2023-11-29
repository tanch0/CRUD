using System;
namespace CrudDemo.Models
{
	public class UpdateViewModel
	{
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public int PhoneNum { get; set; }
        public long Salary { get; set; }
    }
}

