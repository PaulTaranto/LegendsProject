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
            "MAST: Keith...",
            "MAST: Keith, can you hear me?",
            //TODO if time, anim of main character walking in
            "PLAY: Master... yes... but how?",
            "MAST: I'm a wizard Keith, it's magic!",
            "PLAY: Oh... of course",
            "MAST: The world is in danger, Keith!",
            "MAST: I have been incapacitated...{EFX:PAUSE} subdued...{EFX:PAUSE} poisoned!",
            "MAST: I... {EFX:PAUSE} fear I don't have much time left!",
            "PLAY: What!?! Who?",
            "PLAY: Who could have done such a thing?",
            "MAST: {EFX:PAUSE}.{EFX:PAUSE}.{EFX:PAUSE}.",
            "MAST: ...Evil forces, Keith",
            "MAST: It is up to you to save me Keith!",
            "MAST: Venture forth, into the depths of the dungeons",
            "MAST: Bring me the ingredients to make a cure",
            "PLAY: Yes, master! What do you need?",
            "MAST: Bring me the following: One apple, {EFX:PAUSE} One Large egg",
            "MAST: One goblin's toe, the big toe of course, from the right foot!",
            "PLAY: One goblin's wha-",
            //possibly remove mushroom item
            //"MAST: -A white spotted red mushroom, NOT a red spotted white mushroom! I do so much despise those.",
            "PLAY: I don't{EFX:PAUSE} see how these items make a 'cure'",
            "MAST: Of course, the slime bladder of an ooze",
            "PLAY: That's disgus-",
            "MAST: -and some fresh blue essence crystal, finely crushed, I do like a bit of crunch!",
            "PLAY: Are you sure these will help?",
            "PLAY: Master, you never cease to amaze me with your wealth of knowledge. I'll go fetch those from the caves at once.",
            "MAST: Time is of the essence, Keith! Hurry!",
            "{LVL:GAME}"
        };

        return script;
    }

    //Only use if time, would certainly help open the fight a little though
    public static string[] getOpeningDragonFightScript()
    {
        string[] script =
        {
            //"PLAY: There's nothing here? {EFX:PAUSE} Has this whole journey been a waste?!",
            //"PLAY: Where's the egg? I thought for sure this cave would have it!",
            "PLAY: Hmm... it looks like I have everything. Now I just need an egg...",
            "????: Who's THERE?!",
            //"PLAY: It is I! The great Keith! Gonna need you to give me an egg for a laugh?",
            "PLAY: Umm... {EFX:PAUSE} Kei... {EFX.PAUSE} No one...",
            "????: {EFX:PAUSE}.{EFX:PAUSE}.{EFX:PAUSE}.",
            "PLAY: {EFX:PAUSE}.{EFX:PAUSE}.{EFX:PAUSE}.",
            //"????: {SFX:DRAGON_BUILDUP} YOU HORRIBLE CHILD.  HOW DARE YOU COME INTO OUR LARE AND MAKE DEMANDS!",
            //"????: IT'LL BE THE LAST THING YOU EVER DO!",
            "????: {SFX:DRAGON_BUILDUP} I SUPPOSE YOU THINK YOU WILL SEE THE MASTER!",
            "????: NO ONE WILL SEE THE MASTER!!!",
            "PLAY: Y... You must be stopped!",
            "DRAG: Do you think a limp piece of lettuce like yourself will stop a great dragon like me!?",
            //"DRAG: You are nothing more than a limp piece of lettuce in yesterdays salad!",
            "PLAY: I... {EFX:PAUSE} I'm not a lettuce...",
            "DRAG: HA {EFX:PAUSE} HA {EFX:PAUSE} HA {EFX:PAUSE}.",
            "DRAG: LEAVE HERE! OR I SHALL DISPOSE OF YOU!",
            "PLAY: N... No! I will defeat you!",
            //"DRAG: boo",
            //"DRAG: It is I!  The great dragon of this dungeon!  There are very few people I enjoy in this realm, and you don't appear to be one of them",
            //"PLAY: Bruv, Imma need to take your egg, sorry bruv.  It's just gotta happen!",
            //"DRAG: Fine. {EFX:PAUSE} Then you shall die!"
        };

        return script;
    }

    public static string[] getfinalScript()
    {
        string[] script =
        {
        //"MAST: Oh, you're back",
        "MAST: Keith?{EFX:PAUSE} {EFX:PAUSE} Is that you?",
        "PLAY: Yes, Master. I have the ingredients!",
        "MAST: Quickly...{EFX:PAUSE} Keith...{EFX:PAUSE} Make the cure...",
        "PLAY: Yes, Master! Just let me turn on the lights...",
        //TODO Lights flick on
        "MAST: {EFX:PAUSE} GAH!{EFX:PAUSE} Turn them off!",
        "MAST: My head{EFX:PAUSE}...{EFX:PAUSE} It's going to explode!",
        "PLAY: MASTER! MASTER! Are you okay!?",
        "MAST: KEITH! You imbecile! What does it look like!",
        "MAST: I AM DYING, KEITH!!!",
        "PLAY: S...{EFX:PAUSE} Sorry, master!",
        //Master throws up?
        "MAST: *Master throws up*",
        "MAST: Blargh! It's getting worse, Keith!",
        "PLAY: No! Master! I will save you!!",
        //TODO MIX ALL INTO BOWL
        "PLAY: *Keith mixes the ingredients into a bowl*",
        "PLAY: Here, Master! Drink!",
        "MAST: *Master drinks the mixture*",
        //TODO PLAY DRINKING ANIMATION
        "MAST: Ahhh! Yes! That hits the spot!",
        "MAST: I feel so much better, Keith!",
        "MAST: I swear, I will never drink that much again!",
        "PLAY: Drink that mu-",
        "MAST: Nothing works better than my famous 'Sorcerer's Secret hangover cure!",
        "MAST: Brilliantly done, Keith!",
        "PLAY: Hang... {EFX:PAUSE} over... {EFX:PAUSE} cure...",
        "PLAY: I THOUGHT YOU WERE DYING!!!",
        "MAST: Oh, Keith! Don't be so dramatic!",
        "MAST: You make it sound like the world was in danger!",
        "PLAY: But...{EFX:PAUSE} you said{EFX:PAUSE}...",
        "MAST: Now, where is my faithful pet dragon? I can tell her to stop guarding my door now",
        "PLAY: Umm...{EFX:PAUSE} Well...",
        "MAST: Actually, how did you get in? I told her not to let anyone past?",
        "PLAY: ... {EFX:PAUSE} ... {EFX:PAUSE} ...",

        /*
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
        "MAST: Nonesense you foolish rapscallion, the great Keith NEVER DIES!!!",
        "PLAY: *quietly to himself* oh my god i nearly died getting those ingredients......................",
        "MAST: Thank you so much! Now, I feel just about ready to give old mate dragon a visit",
        "MAST: Would be fun to go and play with my dear beloved pet.",
        "PLAY: ..................",*/
        "{LVL:CREDITS}"
       //"{EFX:FADE_TO_BLACK_CREDITS}"
        };
        return script;
    }
}
