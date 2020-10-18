using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pratfall.Input;

namespace Pratfall
{
    [System.Serializable]
    public struct Move
    {
        public int priority;
        public AttackMove attack;
    }

    public class Moveset : MonoBehaviour
    {
        public Move[] movelist;
        /// <summary>
        /// Returns an AttackMove corresponding to the given input or null
        /// if there is no match
        /// </summary>
        /// <param name="inputs">This is usually passed from InputBuffer.MovePattern</param>
        /// <returns></returns>
        public AttackMove ParseInput(params InputDirection[] inputs)
        {
            int highestPriority = 0;
            AttackMove performable = null;
            foreach(Move move in movelist)
            {
                if (move.attack.MatchInput(inputs))
                {
                    if (performable == null)
                    {
                        performable = move.attack;
                        highestPriority = move.priority;
                    } 
                    if(move.priority > highestPriority)
                    {
                        highestPriority = move.priority;
                        performable = move.attack;
                    }
                }
            }
            return performable;
        }
    }
}