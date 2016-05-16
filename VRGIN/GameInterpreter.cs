﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VRGIN.Core
{
    public abstract class GameInterpreter : ProtectedBehaviour
    {
        /// <summary>
        /// Gets a list of actors in the game. Used frequently.
        /// </summary>
        public abstract IEnumerable<IActor> Actors { get; }

        public virtual bool IsEveryoneHeaded
        {
            get
            {
                return Actors.All(a => a.HasHead);
            }
        }

        /// <summary>
        /// Finds the camera object.
        /// </summary>
        /// <returns></returns>
        public virtual Camera FindCamera()
        {
            return Camera.main;
        }
    }
}