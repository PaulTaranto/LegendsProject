using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    // Script format
    // PLAY (4 digit code): text
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
    //requiring player input to continue mid line
    //The effects should keep whoever was currently talking on screen
    //The ability to change level/scene {LVL:CREDITS} or {LVL:GAME}

    public static string[] getShortTestScript()
    {
        string[] script =
        {
            "PLAY: Short test script"
        };

        return script;
    }


    public static string[] getOpeningScript()
    {
        string[] script =
        {
            "MAST: KEITH!!!!!!!",
            "MAST: Come hither!",
            //TODO if time, anim of main character walking in
            "PLAY: Yes master?",
            "MAST: The world is in danger, Keith!",
            "MAST: I have been incapacitated...{EFX:PAUSE} subdued...{EFX:PAUSE} poisoned!",
            "MAST: I... {EFX:PAUSE} fear I don't have much time left!",
            "PLAY: What!?!  Who?",
            "PLAY: Who could have done such a thing?",
            "MAST: {EFX:PAUSE}.{EFX:PAUSE}.{EFX:PAUSE}.",
            "MAST: I uhh, have no clue..",
            "MAST: Annyyywaayy, I need you to venture into the dungeons and fetch ingredients to make a cure.",
            "MAST: Bring me the following: One apple, {EFX:PAUSE} One goblin’s toe, big toe of course, from the right foot!",
            "PLAY: wha-",
            "MAST: -A white spotted red mushroom, NOT a red spotted white mushroom! I do so much despise those.",
            "PLAY: I don't{EFX:PAUSE} see how these items make a 'cure'",
            "MAST: -and some fresh blue essence crystal, finely crushed, for that most delectable texture",
            "PLAY: Master, you never cease to amaze me with your wealth of knowledge. I'll go fetch those from the caves at once.",
            "MAST: Time is of the essence, Keith! Do hurry!",
            "{LVL:GAME}"
        };

        return script;
    }

    //Only use if time, would certainly help open the fight a little though
    public static string[] getOpeningDragonFightScript()
    {
        string[] script =
        {
            "PLAY: There's nothing here? {EFX:PAUSE} Has this whole journey been a waste?!",
            "PLAY: Where's the blue essence crystal? I thought for sure this cave would have some!",
            "????: Who's THERE?!",
            "PLAY: It is I! The great Keith! Don't suppose you can give me blue essence crystal for a laugh?",
            "????: {EFX:PAUSE}.{EFX:PAUSE}.{EFX:PAUSE}.",
            "PLAY: {EFX:PAUSE}.{EFX:PAUSE}.{EFX:PAUSE}.",
            "????: {SFX:DRAGON_BUILDUP} YOU HORRIBLE CHILD.  HOW DARE YOU COME INTO OUR LARE AND MAKE DEMANDS!",
            "????: IT'LL BE THE LAST THING YOU EVER DO!",
            "PLAY: wh-who goes there?",
            "DRAG: boo"
        };

        return script;
    }

    public static string[] getfinalScript()
    {
        string[] script =
        {
        "MAST: Oh, you're back",
        //TODO Lights flick on
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
        //TODO MIX ALL INTO BOWL, THEN PLAY DRINKING ANIMATION AND SFX
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
        "{LVL:CREDITS}"
       //"{EFX:FADE_TO_BLACK_CREDITS}"
        };
        return script;
    }
}
