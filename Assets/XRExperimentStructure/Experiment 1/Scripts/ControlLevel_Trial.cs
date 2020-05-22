/*
This software is part of the Unified Suite for Experiments (USE).
Information on USE is available at
http://accl.psy.vanderbilt.edu/resources/analysis-tools/unifiedsuiteforexperiments/

Copyright (c) <2018> <Marcus Watson>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

1) The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
2) If this software is used as a component of a project that leads to publication
(e.g. a paper in a scientific journal or a student thesis), the published work
will give appropriate attribution (e.g. citation) to the following paper:
Watson, M.R., Voloh, B., Thomas, C., Hasan, A., Womelsdorf, T. (2018). USE: An
integrative suite for temporally-precise psychophysical experiments in virtual
environments for human, nonhuman, and artificially intelligent agents. BioRxiv:
http://dx.doi.org/10.1101/434944

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using USE_States;

public class ControlLevel_Trial : ControlLevel
{
    public TrialDifficulty trialDifficulty;

    //trial variables
    [System.NonSerialized]
    public int trialCount = 1;

    public override void DefineControlLevel()
    {

        //define States within this Control Level
        State stimOn = new State("StimOn");
        State negativeFeedback = new State("NegativeFeedback");
        AddActiveStates(new List<State> { stimOn, negativeFeedback });

        // stimOn -> miss 3 -> negativeFeedback (disables all floor input) -> resetTrial -> stimOn -> terminate -<

        stimOn.AddInitializationMethod(() =>
        {
            Debug.Log("Starting trial " + trialCount);
        });
        //stimOn.AddTimer() /Limit with duration based on average walking speed 

        negativeFeedback.AddInitializationMethod(() =>
        {
            // "close" floor
        });
        // give timer to reset to correct side, accept keyboard input to pause timer? only keyboard input?
        //negativeFeedback.AddDefaultTerminationMethod(() => "return to stimOn");

        this.AddTerminationSpecification(() => trialCount >= 1);
    }
}
*/