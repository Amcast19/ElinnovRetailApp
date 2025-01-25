using System.Globalization;

namespace ElinnovRetail.Models.Common
{
    public static class Constants
    {
        //Success Messages
        public const string RecordSavedMessage = "Record successfully saved!";
        public const string RecordUdpatedMessage = "Record successfully updated!";
        public const string RecordDeletedMessage = "Record successfully deleted!";

        //Error Messages
        public const string GenericErrorMessage = "Something went wrong! Please try again.";
        public const string RecordNotSavedMessage = "Record not saved! Please try again.";
        public const string RecordNotFoundMessage = "Record not found! Please try again.";
        public const string RecordNotUpdatedMessage = "Record not found! Please try again.";
        public const string RecordNotDeletedMessage = "Record not found! Please try again.";
        public const string EmptyRecords = "No records found.";
        public const string InvalidInput = "Invalid input! Please try again.";

        //Others
        public const string AppName = "Inventory Management System";

        //Option Border Width
        public const int HorizontalBorderWidth = 60;
        public const int LeftPadding = 3;
    }
}
