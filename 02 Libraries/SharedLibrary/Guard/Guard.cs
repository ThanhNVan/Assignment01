using System.Runtime.CompilerServices;

namespace SharedLibraries;

public static class Guard
{
    public static void IsNull<T>(T value, string message = "", [CallerMemberName] string memberName = "NA") {
        if (value == null) {
            if (string.IsNullOrEmpty(message)) {
                message = "Guard IsNull. " + typeof(T).Name + " is null.";
            }

            message = message + " Error in " + memberName + ".";
            throw new NullReferenceException(message);
        }
    }

    public static void IsNullOrEmpty(string value, string message = "", [CallerMemberName] string memberName = "NA") {
        bool flag = false;
        string value2 = null;
        if (value == null) {
            flag = true;
        } else {
            value2 = value.ToString();
        }

        if (string.IsNullOrEmpty(value2) || flag) {
            if (string.IsNullOrEmpty(message)) {
                message = "Guard IsNullOrEmpty. " + typeof(string).Name + " is null or emprty.";
            }

            message = message + " Error in " + memberName + ".";
            throw new NullReferenceException(message);
        }
    }

    public static void IsNotNull<T>(T value, string message = "", [CallerMemberName] string memberName = "NA") {
        if (value != null) {
            if (string.IsNullOrEmpty(message)) {
                message = "Guard IsNotNull. " + typeof(T).Name + " is not null.";
            }

            message = message + " Error in " + memberName + ".";
            throw new Exception(message);
        }
    }

    public static void ParamIsNull<T>(T value, string parameterName, string message = "", [CallerMemberName] string memberName = "NA") {
        if (value == null) {
            if (string.IsNullOrEmpty(message)) {
                message = "Guard IsNull. Parameter " + parameterName + " is null.";
            }

            message = message + " Error in " + memberName + ".";
            throw new ArgumentNullException(parameterName, message);
        }
    }

    public static void GuidIdIsEmpty(string param, string parameterName, string message = "", [CallerMemberName] string memberName = "NA") {
        if (param == Guid.Empty.ToString()) {
            if (string.IsNullOrEmpty(message)) {
                message = "Guid Is Empty. Parameter " + parameterName + " is null.";
            }

            message = message + " Error in " + memberName + ".";
            throw new ArgumentNullException(parameterName, message);
        }
    }

    public static void ParamIsNullOrEmpty(string value, string parameterName, string message = "", [CallerMemberName] string memberName = "NA") {
        bool flag = false;
        string value2 = null;
        if (value == null) {
            flag = true;
        } else {
            value2 = value.ToString();
        }

        if (string.IsNullOrEmpty(value2) || flag) {
            if (string.IsNullOrEmpty(message)) {
                message = "Guard IsNullOrEmpty.Parameter " + parameterName + " is null.";
            }

            message = message + " Error in " + memberName + ".";
            throw new ArgumentNullException(parameterName, message);
        }
    }

    public static void ParamIsNotNull<T>(T value, string parameterName, string message = "", [CallerMemberName] string memberName = "NA") {
        if (value != null) {
            if (string.IsNullOrEmpty(message)) {
                message = "Guard IsNotNull. Parameter " + parameterName + " is not null";
            }

            throw new ArgumentException(parameterName, message);
        }
    }

    public static void PropertyIsNull<T>(T value, string propertyName, string message = "", [CallerMemberName] string memberName = "NA") {
        if (value == null) {
            if (string.IsNullOrEmpty(message)) {
                message = "Guard IsNull. Property " + propertyName + " is null.";
            }

            message = message + " Error in " + memberName + ".";
            throw new ArgumentNullException(propertyName, message);
        }
    }

    public static void PropertyIsNullOrEmpty(string value, string propertyName, string message = "", [CallerMemberName] string memberName = "NA") {
        bool flag = false;
        string value2 = null;
        if (value == null) {
            flag = true;
        } else {
            value2 = value.ToString();
        }

        if (string.IsNullOrEmpty(value2) || flag) {
            if (string.IsNullOrEmpty(message)) {
                message = "Guard IsNullOrEmpty. Property " + propertyName + " is null.";
            }

            message = message + " Error in " + memberName + ".";
            throw new ArgumentNullException(propertyName, message);
        }
    }

    public static void PropertyIsNotNull<T>(T value, string propertyName, string message = "", [CallerMemberName] string memberName = "NA") {
        if (value != null) {
            if (string.IsNullOrEmpty(message)) {
                message = "Guard IsNotNull. Property " + propertyName + " is not null";
            }

            throw new ArgumentException(propertyName, message);
        }
    }

    public static void FileExists(string filePath) {
        if (!File.Exists(filePath)) {
            throw new FileNotFoundException("File: " + filePath + " doesn't exists.");
        }
    }
}
