namespace HumanCapitalManagment.Data
{
    public static class DataConstants
    {
        public static class Employee
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 2;
            public const int NationalityMaxLength = 30;
            public const int NationalityMinLength = 4;
            public const int PhoneNumberMaxLength = 20;
            public const int PhoneNumberMinLength = 6;
            public const int SalaryStatusMaxLength = 50;
        }

        public static class HRSpecialist
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 2;
            public const int PhoneNumberMaxLength = 20;
            public const int PhoneNumberMinLength = 6;
        }

        public static class Department
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 2;
        }

        public static class Candidate
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 2;
            public const int NationalityMaxLength = 30;
            public const int NationalityMinLength = 4;
            public const int PhoneNumberMaxLength = 20;
            public const int PhoneNumberMinLength = 6;
        }

        public static class User
        {
            public const int UserNameMaxLength = 30;
            public const int UserNameMinLength = 5;
            public const int FullNameMaxLength = 80;
            public const int FullNameMinLength = 5;
        }
    }
}
