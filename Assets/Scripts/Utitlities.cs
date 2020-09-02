using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    [System.Serializable]
    public class Filter<T>
    {
        public enum ComparisonType { AND, OR }
        /// <summary>
        /// If set to true, all targets must pass for the result to be true
        /// </summary>
        public ComparisonType comparisonType = ComparisonType.OR;

        [Tooltip("Negates the result of the check")]
        /// <summary>
        /// If set to true, inverts the result
        /// </summary>
        public bool negate = true;
        public T[] targets;

        public bool Check(params T[] others)
        {
            //AND
            if (comparisonType == ComparisonType.AND)
            {
                foreach (T target in others)
                {
                    foreach (T item in targets)
                    {
                        if (!target.Equals(item))
                        {
                            if (negate)
                                return true;
                            else
                                return false;
                        }
                    }
                }
            }
            //OR
            else
            {
                foreach (T target in others)
                {
                    foreach (T item in targets)
                    {
                        if (target.Equals(item))
                        {
                            if (negate)
                                return false;
                            else
                                return true;
                        }
                    }
                }
            }
            return true;
        }
    }
}
