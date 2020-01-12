using System;
namespace UserOperationsPOC.DataTransferObjects
{
    public class UserDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public string Job { get; set; }
    }
}
