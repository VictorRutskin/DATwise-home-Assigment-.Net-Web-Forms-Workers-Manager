using System;

namespace CustomExceptions
{
    public class CustomException : Exception
    {
        public string Name { get; }
        public string Description { get; }
        public string Explanation { get; }
        public DateTime Time { get; }

        public CustomException(string name, string description, string explanation)
            : base($"{name}: {description}")
        {
            Name = name;
            Description = description;
            Explanation = explanation;
            Time = DateTime.Now;
        }
    }

    public class ImageAddingException : CustomException
    {
        public ImageAddingException(string explanation = "")
            : base("ImageAddingException", "Could not add image.", explanation)
        {
        }
    }

    public class NotFoundInDbException : CustomException
    {
        public NotFoundInDbException(string explanation = "")
            : base("NotFoundInDbException", "Failed to get item from database.", explanation)
        {
        }
    }

    public class ModelStateException : CustomException
    {
        public ModelStateException(string explanation = "")
            : base("ModelStateException", "Model state is invalid.", explanation)
        {
        }
    }

    public class UnauthorizedUserException : CustomException
    {
        public UnauthorizedUserException(string explanation = "")
            : base("UnauthorizedUserException", "User is not authorized to perform this action.", explanation)
        {
        }
    }
}