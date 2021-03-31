using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationManagement.Domain.Tests.Unit.Utility
{
    public class MessageTestData
    {
        public static Guid Id = Guid.Parse("4ffd12a3-321c-4504-9f0a-45467acde09b");
        public static string Content = "It's a sample content";
        public static string TooLongContent = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
        public static string Title = "Lorem Ipsum";
        public static bool IsSent = true;
        public static Guid UserId = Guid.Parse("2b7510db-a2b6-4590-859c-cfeb74f8874e");
    }
}
