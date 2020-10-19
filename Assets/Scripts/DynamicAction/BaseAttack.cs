using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pratfall
{
    /// <summary>
    /// Fighters' attacks should inherit from this. Behavior is called from Begin().
    /// OnInterrupted() is called when the move is stopped before it would naturally
    /// finish. OnFinished is called when the move runs till completion. If the move
    /// creates any entities, they should be cleaned up when it ends or when appropriate
    /// </summary>
    public abstract class BaseAttack : DynamicAction
    {
        /// <summary>
        /// The character using this move. You can get information about
        /// their state to change how a move works
        /// </summary>
        public Characters.BaseCharacter user;
        protected override abstract IEnumerator Behavior();
        protected override abstract void OnFinished();
        protected abstract override void OnInterrupted();
    }
}