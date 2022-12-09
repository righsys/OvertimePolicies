namespace OvertimePolicies.Services.Common.Exceptions
{
    public static class ExceptionMessages
    {
        public const string GeneralError = "خطا در پردازش اطلاعات";
        public const string EntityNotFoundError = "اطلاعات مورد نظر یافت نشد";
        public const string UpdateEntityError = "بروزرسانی اطلاعات با خطا مواجه شد";
        public const string DeleteEntityError = "حذف اطلاعات با خطا مواجه شد";
        public const string SearchResultsEmpty = "جستجو نتیجهه ای در بر نداشت";
        public const string EmployeeNotExist = "اطلاعات کارمند یافت نشد";
        public const string DuplicateSalaryInsert = "اطلاعات حقوق تکراری است";
        public const string SalaryNotExistToUpdate = "اطلاعات حقوق یافت نشد";
    }
}