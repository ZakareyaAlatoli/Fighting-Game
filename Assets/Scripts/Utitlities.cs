using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    public class ReadOnlyAttribute : PropertyAttribute
    {

    }

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


    public struct ValueData<T>
    {
        public T underflowAmount;
        public T overflowAmount;
    }
    [System.Serializable]
    public struct ClampedInt
    {
        private int min;
        private int max;
        public int Min
        {
            get => min;
            set
            {
                if (value <= max)
                    min = value;
            }
        }
        public int Max
        {
            get => max;
            set
            {
                if (value >= min)
                    max = value;
            }
        }
        public int value;

        public void Add(int amount)
        {
            value += amount;
            value = Mathf.Clamp(value, min, max);
        }

        public void Add(int amount, out ValueData<int> data)
        {
            data = GetValueData(value + amount, this);
        }

        public static ClampedInt operator +(ClampedInt lval, int rval)
        {
            return new ClampedInt()
            {
                value = Mathf.Clamp(lval.value + rval, lval.min, lval.max),
                min = lval.min,
                max = lval.max
            };
        }

        public static ClampedInt operator -(ClampedInt lval, int rval)
        {
            return new ClampedInt()
            {
                value = Mathf.Clamp(lval.value - rval, lval.min, lval.max),
                min = lval.min,
                max = lval.max
            };
        }

        public void Set(int amount, out ValueData<int> data)
        {
            data = GetValueData(amount, this);
        }

        public static implicit operator int(ClampedInt value) => value.value;
        public static implicit operator float(ClampedInt value) => (float)value.value;
        public static implicit operator double(ClampedInt value) => (double)value.value;

        private ValueData<int> GetValueData(int amount, ClampedInt clampedInt)
        {
            ValueData<int> data = new ValueData<int> { overflowAmount = 0, underflowAmount = 0 };

            if (amount > clampedInt.max)
                data.overflowAmount = amount - max;
            else if (amount < min)
                data.underflowAmount = amount - min;

            clampedInt.value = Mathf.Clamp(amount, min, max);

            return data;
        }

        public override string ToString()
        {
            return $"{value}";
        }
    }

    public struct ClampedFloat
    {
        private float min;
        private float max;
        public float Min
        {
            get => min;
            set
            {
                if (value <= max)
                    min = value;
            }
        }
        public float Max
        {
            get => max;
            set
            {
                if (value >= min)
                    max = value;
            }
        }
        public float value;

        public void Add(float amount)
        {
            value += amount;
            value = Mathf.Clamp(value, min, max);
        }

        public void Add(float amount, out ValueData<float> data)
        {
            data = GetValueData(value + amount, this);
        }

        public static ClampedFloat operator +(ClampedFloat lval, int rval)
        {
            return new ClampedFloat()
            {
                value = Mathf.Clamp(lval.value + rval, lval.min, lval.max),
                min = lval.min,
                max = lval.max
            };
        }

        public static ClampedFloat operator -(ClampedFloat lval, int rval)
        {
            return new ClampedFloat()
            {
                value = Mathf.Clamp(lval.value - rval, lval.min, lval.max),
                min = lval.min,
                max = lval.max
            };
        }

        public void Set(int amount, out ValueData<float> data)
        {
            data = GetValueData(amount, this);
        }

        public static implicit operator int(ClampedFloat value) => (int)value.value;
        public static implicit operator float(ClampedFloat value) => (float)value.value;
        public static implicit operator double(ClampedFloat value) => (double)value.value;

        private ValueData<float> GetValueData(float amount, ClampedFloat clampedFloat)
        {
            ValueData<float> data = new ValueData<float> { overflowAmount = 0, underflowAmount = 0 };

            if (amount > clampedFloat.max)
                data.overflowAmount = amount - clampedFloat.max;
            else if (amount < min)
                data.underflowAmount = amount - clampedFloat.min;

            clampedFloat.value = Mathf.Clamp(amount, clampedFloat.min, clampedFloat.max);

            return data;
        }

        public override string ToString()
        {
            return $"{value}";
        }
    }

}
