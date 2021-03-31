using System;

namespace NotificationManagement.Presentation.Api.Messages.Message.Request
{
    public class CreateUserRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public bool Sex { get; set; }
        public bool IsMarrid { get; set; }
        public bool IsActive { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
