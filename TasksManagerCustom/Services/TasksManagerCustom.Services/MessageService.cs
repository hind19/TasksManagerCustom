using TasksManagerCustom.Services.Interfaces;

namespace TasksManagerCustom.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
