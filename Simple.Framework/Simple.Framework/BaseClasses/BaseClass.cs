/*------------------------------------------------------------------------------
 * Created     : 10.05.2011 
 * Programmer  : Md. Nazmul Saqib
 * E-mail      : edurazee@yahoo.com
 * 
 * Usage	   : Every Value-Object classes in the VO layer must be
 *               derived from this class.
 *------------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework
{
    /// <summary>
    /// Every Value-Object classes in the VO layer must be derived from this class.
    /// </summary>
    /// <typeparam name="T">Type of the value object.</typeparam>
    public class BaseClass<T> : IBaseClassInterface where T : IBaseClassInterface
    {
        private int? requestedHashCode;

        public int? ID { get; set; }

        protected bool IsTransient()
        {
            return Equals(ID, default(T));
        }

        /// <summary>
        /// Compare equality trough Id
        /// </summary>
        /// <param name="other">Entity to compare.</param>
        /// <returns>true is are equals</returns>
        /// <remarks>
        /// Two entities are equals if they are of the same hierarcy tree/sub-tree
        /// and has same id.
        /// </remarks>
        public virtual bool Equals(IBaseClassInterface other)
        {
            if (null == other || !GetType().IsInstanceOfType(other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            bool otherIsTransient = Equals(other.ID, default(T));
            bool thisIsTransient = IsTransient();
            if (otherIsTransient && thisIsTransient)
            {
                return ReferenceEquals(other, this);
            }

            return other.ID.Equals(ID);
        }

        public override bool Equals(object obj)
        {
            var that = obj as IBaseClassInterface;
            return Equals(that);
        }

        public override int GetHashCode()
        {
            if (!requestedHashCode.HasValue)
            {
                requestedHashCode = IsTransient() ? base.GetHashCode() : ID.GetHashCode();
            }
            return requestedHashCode.Value;
        }
    }
}
