using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    // Script format
    // PERS (4 digit code): text
    // i.e
    // PLAY(ER): blah blah blah
    // when editing script, the game works out who is talking by the first 4 characters.
    // then remvos the first 6 characters the start at the start of the script

    // IDEAS TO CONTINUE THIS IN A FUTURE PROJECT
    // Could potentially write an effects system by having like {EFFECT:screen_shake} placed within
    // if the text rendered gets to a {, it could stop writing to figure out the effect, play the effect, then continue past the closing }
    // ability to change speed of text drawing from script {TEXT_SPEED:0.01}
    // ability to have 2 characters talking at once, i.e one at top, one at bottom
        // in the case of the final script the player could be whispering to themself while the master is yelling about how much better they feel
    // ability to have text automatically go to next line of script... useful for sarcastic situations where the text really isn't useful
        // also, similarly to the above point, the master's text could be automatically going through multiple lines, while the player controls the bottom text
    // ability to play sfx from script i.e {SFX:bang}
    // ability to pause the rendering of text i.e {PAUSE}
        //requiring player input to continue
    //The effects should keep whoever was currently talking on screen
    public static string[] getfinalScript()
    {
        string[] script =
        {
        "MAST: Oh, you're back",
        //Lights flick on
        "MAST: GAHH!  Turn off the lights!",
        "PLAY: Master! Master! I've returned! How are you feeling?",
        "MAST: WHAT DOES IT LOOK LIKE YOU SILLY BUFFOON!?!?",
        "PLAY: ...............",
        "MAST: ..sorry, shouldn't have yelled at you.  Things have been.... stressful",
        "PLAY: What do you mean?",
        "MAST: Well, I've never been this ill before,",
        "MAST: To be honest, I'm not sure how long I have left..",
        "PLAY: maste-",
        "MAST: -I've haven't finished speaking yet, don't interupt your elders",
        "MAST: Now... where was I?",
        "MAST: I'm not sure how long I have left.. and I need to say this before I go.",
        "MAST: You've always been my star pupil, I'm so proud of the work you've done for me today.",
        "MAST: This quest was of utmost importance!",
        "PLAY: Was it?  These items seem so random, what could you possibly need them for?",
        //MIX ALL INTO BOWL, THEN PLAY DRINKING ANIMATION AND SFX
        "MAST: ...",
        "PLAY: ...",
        "MAST: ...",
        "PLAY: ...",
        "MAST: FINALLY, I FEEL SO MUCH BETTER!!",
        "PLAY: ...what?",
        "MAST: PRESENTING: THE SORCERER'S HANGOVER CURE!  IT'S BEEN PASSED DOWN FOR GENERATIONS AND NOW, YOU KNOW OF IT!",
        "PLAY: ... I thought you were dying",
        "MAST: Nonesense you foolish rapscallion, the great {NAME} NEVER DIES!!!",
        "PLAY: *quietly to himself* oh my god i nearly died getting those ingredients......................",
        "MAST: Thank you so much! Now, I feel just about ready to give old mate dragon a visit",
        "MAST: Would be fun to fight and defeat them once and for all!",
        "PLAY: ..................",
        "{EFX:FADE_TO_BLACK}"
        };
        return script;
    }
}
