
namespace System;

/// <summary>
/// This class contains extension methods related to the <see cref="Exception"/>
/// type.
/// </summary>
public static class ExceptionExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method adds a date/time stamp to the exception.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <returns>The value of the <paramref name="ex"/> parameters, for 
    /// chaining calls together.</returns>
    public static Exception SetDateTime(
        this Exception ex
        )
    {
        // Validate the parameter before attempting to use it.
        Guard.Instance().ThrowIfNull(ex, nameof(ex));

        // The dictionary key.
        var key = "datetime-stamp";

        // Add to the dictionary.
        ex.Data[key] = DateTime.UtcNow;

        // Return the exception.
        return ex;
    }

    // *******************************************************************

    /// <summary>
    /// This method returns the date/time the exception was thrown.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="dateTime">The date/time when the exception was thrown.</param>
    /// <returns>True if the data was found; false otherwise.</returns>
    public static bool GetDateTime(
        this Exception ex,
        out DateTime dateTime
        )
    {
        dateTime = DateTime.MinValue;

        // Validate the parameter before attempting to use it.
        Guard.Instance().ThrowIfNull(ex, nameof(ex));

        // The dictionary key.
        var key = "datetime-stamp";

        // Does the exception contain method arguments?
        if (ex.Data.Contains(key))
        {
            // Get the method args.
            dateTime = (DateTime)ex.Data[key];

            // We found the data.
            return true;
        }

        // We didn't find the data.
        return false;
    }

    // *******************************************************************

    /// <summary>
    /// This method adds arguments from the calling method to the exception.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="args">The arguments to use for the operation.</param>
    /// <returns>The value of the <paramref name="ex"/> parameters, for 
    /// chaining calls together.</returns>
    public static Exception SetMethodArguments(
        this Exception ex,
        params (string, object)[] args
        )
    {
        // Validate the parameter before attempting to use it.
        Guard.Instance().ThrowIfNull(ex, nameof(ex));

        // The dictionary key.
        var key = "method-args";

        // Should we remove arguments?
        if (null == args || args.Length == 0)
        {
            // Remove anything from the dictionary.
            ex.Data.Remove(key);
        }
        else
        {
            // Add to the dictionary.
            ex.Data[key] = args;
        }

        // Return the exception.
        return ex;
    }

    // *******************************************************************

    /// <summary>
    /// This method returns arguments from the calling method. 
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="methodArgs">The collection of method arguments for the
    /// method that created the exception.</param>
    /// <returns>True if the data was found; false otherwise.</returns>
    public static bool GetMethodArguments(
        this Exception ex,
        out (string, object)[] methodArgs
        )
    {
        methodArgs = new (string, object)[0];

        // Validate the parameter before attempting to use it.
        Guard.Instance().ThrowIfNull(ex, nameof(ex));

        // The dictionary key.
        var key = "method-args";

        // Does the exception contain method arguments?
        if (ex.Data.Contains(key))
        {
            // Get the method args.
            methodArgs = ex.Data[key] as (string, object)[];

            // We found the data.
            return true;
        }

        // We didn't find the data.
        return false;
    }

    // *******************************************************************

    /// <summary>
    /// This method adds information about the calling method to the exception.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The value of the <paramref name="ex"/> parameters, for 
    /// chaining calls together.</returns>
    public static Exception SetCallerInfo(
        this Exception ex,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        // Validate the parameter before attempting to use it.
        Guard.Instance().ThrowIfNull(ex, nameof(ex));

        // The dictionary key.
        var key = "caller-info";

        // Add to the dictionary.
        ex.Data[key] = (memberName, sourceFilePath, sourceLineNumber);

        // Return the exception.
        return ex;
    }

    // *******************************************************************

    /// <summary>
    /// This method returns arguments from the calling method. 
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="memberName">The method generated the exception.</param>
    /// <param name="sourceFilePath">The source file that generated the exception.</param>
    /// <param name="sourceLineNumber">The source line that generated the exception.</param>
    /// <returns>True if the data was found; false otherwise.</returns>
    public static bool GetCallerInfo(
        this Exception ex,
        out string memberName,
        out string sourceFilePath,
        out int sourceLineNumber
        )
    {
        memberName = string.Empty;
        sourceFilePath = string.Empty;
        sourceLineNumber = 0;

        // Validate the parameter before attempting to use it.
        Guard.Instance().ThrowIfNull(ex, nameof(ex));

        // The dictionary key.
        var key = "caller-info";

        // Does the exception contain method arguments?
        if (ex.Data.Contains(key))
        {
            // Get the json.
            var tuple = (Tuple<string, string, int>)ex.Data[key];

            // Set the output parameters.
            memberName = tuple.Item1;
            sourceFilePath = tuple.Item2;
            sourceLineNumber = tuple.Item3;

            // We found the data.
            return true;
        }

        // We didn't find the data.
        return false;
    }

    // *******************************************************************

    /// <summary>
    /// This method adds the name of the originator to the exception.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="originator">The name of the component, or class, or
    /// object, that originally threw the exception.</param>
    /// <returns>The value of the <paramref name="ex"/> parameters, for 
    /// chaining calls together.</returns>
    public static Exception SetOriginator(
        this Exception ex,
        string originator
        )
    {
        // Validate the parameter before attempting to use it.
        Guard.Instance().ThrowIfNull(ex, nameof(ex));

        // The dictionary key.
        var key = "originator-info";

        // Add to the dictionary.
        ex.Data[key] = originator;

        // Return the exception.
        return ex;
    }

    // *******************************************************************

    /// <summary>
    /// This method returns the name of the originator to the exception.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="originator">The name of the component, or class, or
    /// object, that originally threw the exception.</param>
    /// <returns>True if the data was found; false otherwise.</returns>
    public static bool GetOriginator(
        this Exception ex,
        out string originator
        )
    {
        originator = string.Empty;

        // Validate the parameter before attempting to use it.
        Guard.Instance().ThrowIfNull(ex, nameof(ex));

        // The dictionary key.
        var key = "originator-info";

        // Does the exception contain method arguments?
        if (ex.Data.Contains(key))
        {
            // Get the data.
            originator = ex.Data[key] as string;

            // We found the data.
            return true;
        }

        // We didn't find the data.
        return false;
    }

    // *******************************************************************

    /// <summary>
    /// This method adds the name of the machine that threw the exception.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <returns>The value of the <paramref name="ex"/> parameters, for 
    /// chaining calls together.</returns>
    public static Exception SetMachineName(
        this Exception ex
        )
    {
        // Validate the parameter before attempting to use it.
        Guard.Instance().ThrowIfNull(ex, nameof(ex));

        // The dictionary key.
        var key = "machine-name";

        // Add to the dictionary.
        ex.Data[key] = Environment.MachineName;

        // Return the exception.
        return ex;
    }

    // *******************************************************************

    /// <summary>
    /// This method returns the name of the machiune that threw the exception.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="machineName">The name of the machine that threw the exception.</param>
    /// <returns>True if the data was found; false otherwise.</returns>
    public static bool GetMachineName(
        this Exception ex,
        out string machineName
        )
    {
        machineName = string.Empty;

        // Validate the parameter before attempting to use it.
        Guard.Instance().ThrowIfNull(ex, nameof(ex));

        // The dictionary key.
        var key = "machine-name";

        // Does the exception contain method arguments?
        if (ex.Data.Contains(key))
        {
            // Get the data.
            machineName = ex.Data[key] as string;

            // We found the data.
            return true;
        }

        // We didn't find the data.
        return false;
    }

    // *******************************************************************

    /// <summary>
    /// This method formats and returns an extended message for an exception 
    /// by adding various optional sections to the standard <see cref="Exception.Message"/>
    /// property.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="includeType">True to include the type of exception; 
    /// false otherwise.</param>
    /// <param name="includeInnerExceptions">True to include messages from
    /// any inner exceptions; false otherwise.</param>
    /// <param name="includeStackTrace">True to include a stack trace from
    /// the exception; false otherwise.</param>
    /// <param name="includeSource">True to include the source from the
    /// exception; false otherwise.</param>
    /// <param name="includeHelpLink">True to include th help link from
    /// the exception; false otherwise.</param>
    /// <param name="includeHResult">True to include the HRESULT from the
    /// exception; false otherwise.</param>
    /// <param name="includeData">True to include the data from the exception;
    /// false otherwise.</param>
    /// <param name="includeMachineName">True to include the machine name along
    /// with the exception; false otherwise.</param>
    /// <param name="includeUserName">True to include the user name along with
    /// the exception; false otherwise.</param>
    /// <param name="includeExtras">True to include common properties that 
    /// usually contain extra information related to the exception; false 
    /// otherwise.</param>
    /// <returns>A formatted message string.</returns>
    public static string MessageEx(
        this Exception ex,
        bool includeType = false,
        bool includeInnerExceptions = false,
        bool includeStackTrace = false,
        bool includeSource = false,
        bool includeHelpLink = false,
        bool includeHResult = false,
        bool includeData = false,
        bool includeMachineName = false,
        bool includeUserName = false,
        bool includeExtras = false
        )
    {
        // Validate the parameter before attempting to use it.
        Guard.Instance().ThrowIfNull(ex, nameof(ex));

        // Start with the message.
        var sb = new StringBuilder(ex.Message);

        // Should we add an additional line?
        if (includeType ||
            includeInnerExceptions ||
            includeStackTrace ||
            includeSource ||
            includeHelpLink ||
            includeHResult ||
            includeData ||
            includeMachineName ||
            includeUserName ||
            includeExtras)
        {
            sb.AppendLine();
        }

        // Should we add the exception type?
        if (includeType)
        {
            AddType(ex, sb);
        }

        // Should we add inner exception(s)?
        if (includeInnerExceptions)
        {
            AddInnerExceptions(ex, sb);
        }

        // Should we add the source?
        if (includeSource)
        {
            AddSource(ex, sb);
        }

        // Should we add the help link?
        if (includeHelpLink)
        {
            AddHelpLink(ex, sb);
        }

        // Should we add the HRESULT?
        if (includeHResult)
        {
            AddHResult(ex, sb);
        }

        // Should we add any data?
        if (includeData)
        {
            AddData(ex, sb);
        }

        // Should we add the machine name?
        if (includeMachineName)
        {
            AddMachineName(ex, sb);
        }

        // Should we add the user name?
        if (includeUserName)
        {
            AddUserName(ex, sb);
        }

        // Should we add extras?
        if (includeExtras)
        {
            AddExtras(ex, sb);
        }

        // Convert to a string.
        var message = sb.ToString();

        // Return the result.
        return message;
    }

    #endregion

    // *******************************************************************
    // Private methods.
    // *******************************************************************

    #region Private methods

    /// <summary>
    /// This method adds a section to a string builder for the exception type.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="sb">The string builder to use for the operation.</param>
    private static void AddType(
        Exception ex,
        StringBuilder sb
        )
    {
        // Always start on a new line.
        sb.AppendLine();

        // Add the section header.
        sb.AppendLine("----- Exception Type -----");

        // Add the section body.
        sb.AppendLine($"{ex.GetType().Name}");

        // Always end on a new line.
        sb.AppendLine();
    }

    // *******************************************************************

    /// <summary>
    /// This method adds a section to a string builder for inner exceptions.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="sb">The string builder to use for the operation.</param>
    private static void AddInnerExceptions(
        Exception ex,
        StringBuilder sb
        )
    {
        // Always start on a new line.
        sb.AppendLine();

        // Add the section header.
        sb.AppendLine("----- Inner Exception(s) -----");

        // Add the section body.
        if (ex.InnerException == null)
        {
            // Say we have no inner exceptions.
            sb.AppendLine("None");
        }
        else
        {
            // Loop and add each inner exception.
            var exTemp = ex;
            while (exTemp.InnerException != null)
            {
                sb.AppendLine($"{exTemp.GetType().Name} -> {exTemp.Message}");
                exTemp = exTemp.InnerException;
            }
        }

        // Always end on a new line.
        sb.AppendLine();
    }
            
    // *******************************************************************

    /// <summary>
    /// This method adds a section to a string builder for a source method.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="sb">The string builder to use for the operation.</param>
    private static void AddSource(
        Exception ex,
        StringBuilder sb
        )
    {
        // Always start on a new line.
        sb.AppendLine();

        // Add the section header.
        sb.AppendLine("----- Source -----");

        // Add the section body.
        sb.AppendLine(ex.Source);

        // Always end on a new line.
        sb.AppendLine();
    }

    // *******************************************************************

    /// <summary>
    /// This method adds a section to a string builder for a data array.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="sb">The string builder to use for the operation.</param>
    private static void AddData(
        Exception ex,
        StringBuilder sb
        )
    {
        // Always start on a new line.
        sb.AppendLine();

        // Add the section header.
        sb.AppendLine("----- Data -----");

        // Add the section body.
        for (int x = 0; x < ex.Data.Count; x++)
        {
            sb.AppendLine($"Data {x}: {ex.Data[x]}");
        }

        // Always end on a new line.
        sb.AppendLine();
    }

    // *******************************************************************

    /// <summary>
    /// This method adds a section to a string builder for a HRESULT.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="sb">The string builder to use for the operation.</param>
    private static void AddHResult(
        Exception ex,
        StringBuilder sb
        )
    {
        // Always start on a new line.
        sb.AppendLine();

        // Add the section header.
        sb.AppendLine("----- HResult -----");

        // Add the section body.
        sb.AppendLine($"{ex.HResult}");

        // Always end on a new line.
        sb.AppendLine();
    }

    // *******************************************************************

    /// <summary>
    /// This method adds a section to a string builder for a help link.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="sb">The string builder to use for the operation.</param>
    private static void AddHelpLink(
        Exception ex,
        StringBuilder sb
        )
    {
        // Always start on a new line.
        sb.AppendLine();

        // Add the section header.
        sb.AppendLine("----- Help Link -----");

        // Add the section body.
        sb.AppendLine($"{ex.HelpLink}");

        // Always end on a new line.
        sb.AppendLine();
    }

    // *******************************************************************

    /// <summary>
    /// This method adds a section to a string builder for a machine name.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="sb">The string builder to use for the operation.</param>
    private static void AddMachineName(
        Exception ex,
        StringBuilder sb
        )
    {
        // Always start on a new line.
        sb.AppendLine();

        // Add the section header.
        sb.AppendLine("----- Machine Name -----");

        // Add the section body.
        sb.AppendLine($"{Environment.MachineName}");

        // Always end on a new line.
        sb.AppendLine();
    }

    // *******************************************************************

    /// <summary>
    /// This method adds a section to a string builder for a user name.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="sb">The string builder to use for the operation.</param>
    private static void AddUserName(
        Exception ex,
        StringBuilder sb
        )
    {
        // Always start on a new line.
        sb.AppendLine();

        // Add the section header.
        sb.AppendLine("----- User Name -----");

        // Add the section body.
        sb.AppendLine($"{Environment.UserName}");

        // Always end on a new line.
        sb.AppendLine();
    }

    // *******************************************************************

    /// <summary>
    /// This method adds a section to a string builder for a user name.
    /// </summary>
    /// <param name="ex">The exception to use for the operation.</param>
    /// <param name="sb">The string builder to use for the operation.</param>
    private static void AddExtras(
        Exception ex,
        StringBuilder sb
        )
    {
        // Always start on a new line.
        sb.AppendLine();

        // Add the section header.
        sb.AppendLine("----- Extras -----");

        // Find any public properties that aren't already part of the exception type.
        var properties = ex.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => !new string[]
            {
                "Message",
                "Source",
                "InnerException",
                "Data",
                "HelpLink",
                "HResult",
                "StackTrace",
                "TargetSite"
            }.Contains(x.Name));

        // Add the section body.
        if (!properties.Any())
        {
            // Say that we have no properties.
            sb.AppendLine($"None");
        }
        else
        {
            // Loop through and try to add these to the section body.
            foreach (var p in properties)
            {
                var obj = p.GetMethod?.Invoke(ex, new object[0]);
                if (obj != null)
                {
                    sb.AppendLine($"{p.Name} -> {obj}");
                }
                else
                {
                    sb.AppendLine($"{p.Name} -> [No public getter]");
                }
                sb.AppendLine();
            }
        }

        // Always end on a new line.
        sb.AppendLine();
    }

    #endregion
}
