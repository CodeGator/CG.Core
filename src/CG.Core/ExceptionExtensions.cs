using CG.Validations;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CG
{
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

            // Should we add the stack trace?
            if (includeStackTrace)
            {
                AddStackTrace(ex, sb);
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
        /// This method adds a section to a string builder for a stack trace.
        /// </summary>
        /// <param name="ex">The exception to use for the operation.</param>
        /// <param name="sb">The string builder to use for the operation.</param>
        private static void AddStackTrace(
            Exception ex,
            StringBuilder sb
            )
        {
            // Always start on a new line.
            sb.AppendLine();

            // Add the section header.
            sb.AppendLine("----- Stack Trace -----");

            // Add the section body.
            sb.AppendLine(ex.StackTrace);

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
}
