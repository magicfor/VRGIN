﻿using SpeechTransport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRGIN.Core;

namespace VRGIN.Controls.Speech
{
    public class VoiceShortcut : IShortcut
    {
        SpeechResult? _LastResult;
        int _MinID = 0;

        Action _Action;
        VoiceCommands _Command;

        public VoiceShortcut(VoiceCommands command, Action action)
        {
            _Action = action;
            _Command = command;

            if (VR.Speech)
            {
                VR.Speech.SpeechRecognized += OnRecognized;
            }
        }

        private void OnRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.ID >= _MinID)
            {
                _LastResult = e.Result;
            }
        }

        public void Dispose()
        {
            // Unregister handler
            if (VR.Speech)
            {
                VR.Speech.SpeechRecognized -= OnRecognized;
            }
        }

        public void Evaluate()
        { 
            if(_LastResult.HasValue)
            {
                if(_Command.IsMatch(_LastResult.Value.Text))
                {
                    if (_LastResult.Value.Confidence > 0.6f || _LastResult.Value.Final)
                    {
                        _Action();
                        _MinID = _LastResult.Value.ID + 1;
                    }
                }
            }

            _LastResult = null;
        }
    }
}