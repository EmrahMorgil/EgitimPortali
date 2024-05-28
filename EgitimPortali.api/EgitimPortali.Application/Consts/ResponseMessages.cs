using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimPortali.Application.Consts
{
    public static class ResponseMessages
    {
        public static readonly string Success = "Success";
        public static readonly string Fail = "Fail";
        public static readonly string UserAlreadyExist = "User already exist";
        public static readonly string UserNotFound = "User not found";
        public static readonly string InvalidCredentials = "Invalid credentials";
        public static readonly string NotFound = "Not found";
        public static readonly string IncorrectOldPasswordEntry = "Incorrect old password entry";
        public static readonly string NewPasswordsDoNotMatch = "New passwords do not match";
        public static readonly string NewPasswordCannotBeTheSameAsOldPassword = "New password cannot be the same as old password";
        public static readonly string AnErrorOccurredWhileLoadingTheImage = "An error occurred while loading the image";
        public static readonly string ModelValidationFail = "An error occured while request model validation";
        public static readonly string UnauthorizedEntry = "Unauthorized entry";
        public static readonly string InvalidImageType = "Invalid image type";
        public static readonly string ThisEmailIsBeingUsed = "This email is being used";
        public static readonly string PleaseEnterValidEmailAddress = "Please enter a valid email address.";
        public static readonly string EmailFieldCannotBeEmpty = "Email field cannot be empty.";
        public static readonly string EmailFieldMustBeAtMostOneHundredCharactersLong = "Email field must be at most 100 characters long.";
        public static readonly string NameFieldCannotBeEmpty = "Name field cannot be empty.";
        public static readonly string NameFieldMustBeAtMostFiftyCharactersLong = "Name field must be at most 50 characters long.";
        public static readonly string PasswordFieldCannotBeEmpty = "Password field cannot be empty.";
        public static readonly string PasswordFieldMustBeAtLeastSixCharactersLong = "Password field must be at least 6 characters long.";
        public static readonly string PasswordFieldMustBeAtMostThirtyCharactersLong = "Password field must be at most 30 characters long.";
        public static readonly string ImageFieldCannotBeEmpty = "Image field cannot be empty.";
        public static readonly string AnErrorOccurredWhileUploadingTheFile = "An error occurred while uploading the file";
        public static readonly string UnexpectedError = "Unexpected error";
        public static readonly string CourseNotFound = "Course not found";
    }
}
