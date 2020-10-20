using CG.Validations;
using System.Collections.Generic;
using System.Dynamic;

namespace CG
{
    /// <summary>
    /// This class represents a dynamic base object.
    /// </summary>
    public abstract class ExpandoBase : DynamicObject, IDynamicMetaObjectProvider
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties
        
        /// <summary>
        /// This property contains the internal properties for the object.
        /// </summary>
        protected IDictionary<string, object> Properties { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="ExpandoBase"/>
        /// class.
        /// </summary>
        protected ExpandoBase()
        {
            Properties = new Dictionary<string, object>();
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method gets the value of the specified property.
        /// </summary>
        /// <param name="binder">The binder to use for the operation.</param>
        /// <param name="result">The results of the operation.</param>
        /// <returns>True if the property was found; False otherwise.</returns>
        public override bool TryGetMember(
            GetMemberBinder binder, 
            out object result
            )
        {
            // Validate the parameter before attempting to use it.
            Guard.Instance().ThrowIfNull(binder, nameof(binder));

            // Make the compiler happy.
            result = null;

            // Do we know this property?
            if (Properties.ContainsKey(binder.Name))
            {
                // Copy the value for the property.
                result = Properties[binder.Name];

                // Return the results.
                return true;
            }

            // Give the base class a chance.
            return base.TryGetMember(binder, out result);
        }

        // *******************************************************************

        /// <summary>
        /// This method sets the value of the specified property.
        /// </summary>
        /// <param name="binder">The binder to use for the operation.</param>
        /// <param name="value">The value to set in the property.</param>
        /// <returns>True if the property was set; False otherwise.</returns>
        public override bool TrySetMember(
            SetMemberBinder binder, 
            object value
            )
        {
            // Validate the parameter before attempting to use it.
            Guard.Instance().ThrowIfNull(binder, nameof(binder));

            // Set the value of the property.
            Properties[binder.Name] = value;

            // Return the results.
            return true;
        }

        // *******************************************************************

        /// <summary>
        /// This method gets the names of all the dynamic properties.
        /// </summary>
        /// <returns>An enumerable sequence of property names.</returns>
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            // Return the keys.
            return Properties.Keys;
        }

        #endregion
    }
}
