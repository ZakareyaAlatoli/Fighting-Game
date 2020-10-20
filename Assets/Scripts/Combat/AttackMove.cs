using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pratfall.Input;

namespace Pratfall
{
    [System.Serializable]
    public class AttackMove
    {
        [System.Serializable]
        public struct CommandInput
        {
            public InputDirection[] pattern;
        }
        public CommandInput[] commandInputs;
        public BaseAttack moveToPerform;
        
        //Checks to see if the correct directional inputs were made to perform this move
        public bool MatchInput(InputDirection[] inputFeed)
        {
            foreach(CommandInput c in commandInputs)
            {
                //The input buffer is checked from last to first because we only care
                //if the last subset of inputs match this move's command input string
                //i.e an input buffer of > > ^ < > ^ will pass a command input of < > ^
                int commandInputIndex = c.pattern.Length - 1;
                int inputFeedIndex = inputFeed.Length - 1;
                bool matchingExpression = true;
                while (commandInputIndex >= 0 && inputFeedIndex >= 0)
                {
                    //If a single token in the input buffer is out of place the move doesn't go through
                    if (inputFeed[inputFeedIndex] != c.pattern[commandInputIndex])
                    {
                        matchingExpression = false;
                        break;
                    }
                    commandInputIndex--;
                    inputFeedIndex--;
                }
                if (matchingExpression)
                    return true;
            }
            return false;
        }

        public void PerformMove()
        {
            moveToPerform.Begin();
        }
    }
}