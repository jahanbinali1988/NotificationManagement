using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationManagement.Domain.Tests.Unit.Utility
{
    public static class UserTestData
    {
        public static Guid Id = Guid.Parse("2b7510db-a2b6-4590-859c-cfeb74f8874e");
        public static string Name = "AliAkbar";
        public static string Family = "Jahanbin";
        public static bool Sex  = true;
        public static bool IsMarrid  = true;
        public static bool IsActive  = true;
        public static string Mobile = "09224957626";
        public static string Email = "jahanbin.ali1988@gmail.com";
        public static DateTime BirthDate = new DateTime(1988, 9, 8);
    }
}
