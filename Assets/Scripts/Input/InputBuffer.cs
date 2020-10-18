using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pratfall.MathLogic;

namespace Pratfall.Input
{
    public struct InputState
    {
        public Vector2 moveValue;
        public Vector2 altMoveValue;
        public bool jumpValue;
        public bool blockValue;
        public bool attackValue;
        public bool specialValue;
    }

    public enum InputDirection
    {
        CENTER, UP, UP_RIGHT, RIGHT, DOWN_RIGHT, DOWN, DOWN_LEFT, LEFT, UP_LEFT
    }

    public class InputBuffer
    {
        public static float stickDeadzone = 0.3f;
        readonly static string[] directionalString =
        {
            "*", "↑", "↗", "→", "↘", "↓", "↙", "←", "↖"
        };

        readonly static InputDirection[] opposite =
        {
            InputDirection.CENTER,
            InputDirection.UP,
            InputDirection.UP_LEFT,
            InputDirection.LEFT,
            InputDirection.DOWN_LEFT,
            InputDirection.DOWN,
            InputDirection.DOWN_RIGHT,
            InputDirection.RIGHT,
            InputDirection.UP_RIGHT
        };

        //-------------------------------
        readonly int size;
        InputState[] buffer;
        public InputBuffer(int size)
        {
            this.size = size;
            buffer = new InputState[size];

            for(int i=0; i<size; i++)
            {
                buffer[i] = new InputState();
            }
        }
        public InputState this[int index] { get => buffer[index]; }

        public void Next(InputState item)
        {
            for(int i=0; i<size-1; i++)
            {
                buffer[i] = buffer[i + 1];
            }
            buffer[size - 1] = item;
        }
        /// <summary>
        /// Mirrors the left/right facing directions
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static InputDirection[] InvertHorizontal(params InputDirection[] pattern)
        {
            InputDirection[] result = pattern;
            for(int i=0; i<result.Length; i++)
            {
                result[i] = opposite[(int)result[i]];
            }
            return result;
        }
        /// <summary>
        /// Returns an array of inputs in this buffer in order of execution.
        /// Consecutive identical inputs are condensed into one
        /// </summary>
        /// <returns></returns>
        public InputDirection[] MovePattern()
        {
            List<InputDirection> result = new List<InputDirection>();
            if (size < 1) return new InputDirection[0];

            result.Add(GetDirection(buffer[0].moveValue));
            for (int i=1; i < size; i++)
            {
                InputDirection current = GetDirection(buffer[i].moveValue);
                InputDirection previous = GetDirection(buffer[i-1].moveValue);
                if (current != previous)
                {
                    result.Add(current);
                }
            }
            return result.ToArray();
        }

        public InputDirection[] MovePatternFull()
        {
            List<InputDirection> result = new List<InputDirection>();

            for (int i = 0; i < size; i++)
            {
                InputDirection current = GetDirection(buffer[i].moveValue);
                result.Add(current);
            }
            return result.ToArray();
        }
        /// <summary>
        /// Same as MovePattern() but gives the raw angles of the inputs instead of an InputDirection enum
        /// </summary>
        /// <returns></returns>
        public float[] MovePatternAngle()
        {
            List<float> result = new List<float>();
            if (size < 1) return new float[0];

            Vector2 tilt = buffer[0].moveValue.normalized;
            float first = Mathf.Atan2(tilt.y, tilt.x) * 180f / Mathf.PI;
            result.Add(first);
            for (int i = 1; i < size; i++)
            {
                tilt = buffer[i].moveValue.normalized;
                float current = Mathf.Atan2(tilt.y, tilt.x) * 180f / Mathf.PI;
                tilt = buffer[i - 1].moveValue.normalized;
                float previous = Mathf.Atan2(tilt.y, tilt.x) * 180f / Mathf.PI;
                if (current != previous)
                {
                    result.Add(previous);
                }
            }
            return result.ToArray();
        }

        public float[] MovePatternAngleFull()
        {
            List<float> result = new List<float>();

            for (int i = 0; i < size; i++)
            {
                Vector2 tilt = buffer[i].moveValue.normalized;
                float current = Mathf.Atan2(tilt.y, tilt.x) * 180f / Mathf.PI;
                result.Add(current);
            }
            return result.ToArray();
        }



        public static InputDirection GetDirection(Vector2 tilt)
        {
            if (tilt.magnitude < stickDeadzone)
                return InputDirection.CENTER;
            else
            {
                Vector2 normalized = tilt.normalized;
                float angle = Mathf.Atan2(normalized.y, normalized.x) * 180f / Mathf.PI;
                if (InRange(angle, 0f, true, 22.5f, false) || InRange(angle, -22.5f, false, 0f, false))
                    return InputDirection.RIGHT;
                else if (InRange(angle, 22.5f, true, 67.5f, false))
                    return InputDirection.UP_RIGHT;
                else if (InRange(angle, 67.5f, true, 112.5f, false))
                    return InputDirection.UP;
                else if (InRange(angle, 112.5f, true, 157.5f, false))
                    return InputDirection.UP_LEFT;
                else if (InRange(angle, 157.5f, true, 180f, true) || InRange(angle, -180f, false, -157.5f, false))
                    return InputDirection.LEFT;
                else if (InRange(angle, -157.5f, true, -112.5f, false))
                    return InputDirection.DOWN_LEFT;
                else if (InRange(angle, -112f, true, -67.5f, false))
                    return InputDirection.DOWN;
                else
                    return InputDirection.DOWN_RIGHT;
            }
        }
        public static string DirectionString(params InputDirection[] dir)
        {
            string result = "";
            foreach(InputDirection dirs in dir)
            {
                result += directionalString[(int)dirs];
            }
            return result;
        }
    }
}