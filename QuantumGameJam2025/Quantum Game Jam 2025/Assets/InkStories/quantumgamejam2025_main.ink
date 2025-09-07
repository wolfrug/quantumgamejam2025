INCLUDE quantumgamejam2025_functions.ink



VAR text_test2_array= "_difficulty^0.6__attempts^0__completed^0__noiseImage^7__triggerWords^<shrinking><beguiled><blame><weakness><men>_"

VAR testDictionary = ""

->start
==start
{ALICE} Joe, you don't have a dog, do you?

{HATTER} Alice?! What the hell?! Where have you been??

{ALICE} You know. On the run. I died.

{HATTER} Again? What is it this time? Another apocalypse?

{ALICE} That's the thing - I can't remember. But I think future me is trying to remind me. I've got these...files.

{HATTER} What are you talking about? Last time you dreamt, there was music...

{ALICE} And the end of the world. No, this is something else. The dog?

{HATTER} ...it's always so freaky when you do this. No, I don't have a dog, but I am going to take care of my mum's dog, Jackie, next week.

{ALICE} Jackie! That'll help.

* [Wait, tell me what's going on \[Tutorial\]]
{ALICE} Uhhh. Soo...my desktop is behaving really strangely. It's full of files that I'm pretty sure are dated in the future.

{HATTER} What?

{ALICE} Yup. But they're not...whole. It's like my patchy memory when I kept waking up before the apocalypse, remember?

{HATTER} I don't remember any files.

{ALICE} Nope, this is new. But I found that if I can...recall certain trigger words associated with the files, they become clearer.
* [I still can't believe this is happening...]
{HATTER} This is weird, even by your standards, Alice.

- {ALICE} Yeahh....I feel like I might have fallen into an even deeper rabbit hole this time.

{ALICE} But with your help, I'm sure I'll figure it out. Before I die again...

->DONE

VAR tutorial_array= "_difficulty^0.1__attempts^0__completed^0__noiseImage^2__triggerWords^<alice><mystery>_"

==tutorial
Any word in the file helps! Even a little!

Look for names, places, concepts for trigger words!

Words not in the text CAN be trigger words too! (for example: in images)

Careful with guessing: wrong guesses lead to decoherence!

Trigger words for this: alice (that's me!) and 'mystery'.

Good luck!

OBJECTIVE(tutorial, TEXT_IMAGE, notes_to_self.txt, selfie)
->DONE
=win
{ALICE} Good to know I can still follow my own instructions...

{HATTER} Huh?

{ALICE} ...nevermind. Just testing something out.
->DONE
=lose
{ALICE} Well...that didn't work out. Dammit. At least I know there wasn't anything important hidden in there...

{HATTER} What are you on about?

{ALICE} Just testing something...
->DONE


VAR joes_dog_array= "_difficulty^0.3__attempts^0__completed^0__noiseImage^1__triggerWords^<jackie><forrest>_"

==joes_dog
MadHatter: Her name's Jackie! She's a...King Charles Cavalier I think?

Alice: Dog tax. I demand a picture.

MadHatter: Hold your horses. First, an Alice-tax. What are you up to?

Alice: I'm just about getting my feet under me. I've met some interesting people who might be able to help me.

MadHatter: You sound a lot more hopeful than the last time we spoke.

Alice: I am! You have no idea how hard it is to convince people I am not crazy, but Professor Forrest seems to actually believe me!

MadHatter: Are you sure they're not with...you know?

OBJECTIVE(joes_dog, TEXT_ONLY, joes_doggy.txt)
->DONE
=win
{ALICE} I got it!

{HATTER} You did? So what did you find out?

{ALICE} ...it seems I'm going to meet some new people. Someone named Professor Forrest?
{not opened_qpac:
{HATTER} And you have no idea who that is?

{ALICE} Not yet I don't. Hmmm.

{HATTER} What?

{ALICE} There's a folder here called \#QPAC, but it has a password on it.

{HATTER} Future you is determined to make things hard for you, I see...
- else:
{HATTER} Who's that?

{ALICE} I suppose I'll find out..!

}
->DONE
=lose
{ALICE} Dammit...it's gone.

{HATTER} Did you try Jackie?

{ALICE} Doesn't matter...it seems that if I just spam whatever, the memory fades away into noise.

{HATTER} ...let's hope there wasn't anything important there then...
->DONE

==opened_qpac
{ALICE} Okay! I got the QPAC folder opened!

{joes_dog.win:
{HATTER} Did you remember the code, like you did with the safe?
- else:
{HATTER} The what now?

{ALICE} There's a folder here labelled QPAC, except it was locked with a password.

{HATTER} And how did you get it open this time? Magic memory shenanigans?
}

{ALICE} Actually future-me had written it into a picture of myself. So yeah: gotta pay attention!

{HATTER} Well, better have at it...
->DONE

==opened_final
{ALICE} Holy shiiit. It worked.

{HATTER} What did?

{ALICE} The code. You know. 4714.

{HATTER} That's a deep dive. So I guess you've figured it all out, huh.

{ALICE} For now, maybe. What a brainfu

{HATTER} Alice?

{ALICE} ...crap. I gotta go. Later, gator!

->DONE

VAR forrest_intro_array= "_difficulty^0.3__attempts^0__completed^0__noiseImage^2__triggerWords^<forrest><claremont><qpac><dawn>_"

==forrest_intro
Dear Miss Alice,

I am looking forward to finally meeting you in person at our Claremont campus (picture attached). Everyone on the QPAC team has been briefed and are eagerly awaiting your arrival!

You've stressed your need for discretion, so I have embedded the code to the campus side entrance into the picture - that way you do not have to go through the lobby at all!

Best,
Prof. Dawn Forrest PhD
QPAC, Claremont Campus,
324 Arroyo Blvd, CA, USA

OBJECTIVE(forrest_intro, TEXT_ONLY, invitation.txt)
->DONE
=win
{ALICE} Looks like I was invited to some fancy university campus by Professor Forrest?

{HATTER} Hmm. That doesn't sound like you. You're usually a lot more careful around people.

{ALICE} Yeah...
->DONE
=lose
{ALICE} That was a dead end. Damn.

{HATTER} Can you recall anything at all about it?

{ALICE} It's all just noise...
->DONE

VAR qpac_campus_array= "_difficulty^0.5__attempts^0__completed^0__noiseImage^6__triggerWords^<claremont><arroyo><qpac>_"

==qpac_campus
claremont, arroyo, qpac

OBJECTIVE(qpac_campus, IMAGE_ONLY, attachment.png, campus)
->DONE
=win
{ALICE} Hah! More codes on pictures.

{HATTER} ...you know what this suggests, right?

{ALICE} What?

{HATTER} That this future you would know about the corrupted files, and that she used it to hide things from people who are not-you.

{ALICE} Like a kind of weird temporal encryption? Neat. Well done future-me.

{HATTER} ...well, until you screw up I guess.
->DONE
=lose
{ALICE} Just noise. Damn it.

{HATTER} You probably really needed that to continue, you know.

{ALICE} I knowwww. Makes you want to go back in time and try again.

{HATTER} Alas, the arrow of time...

{ALICE} ...does bend for some of us...
->DONE

VAR interview1_array= "_difficulty^0.4__attempts^0__completed^0__noiseImage^4__triggerWords^<AC><quantum><echo><dream>_"

==interview1
Interview with Subject AC

I: Describe how it feels to remember the future.

AC: Like a dream. I wake up, and I dreamt the future.

I: In the dream, do you ever remember having had the dream of the future before?

AC: No, which is funny now that you mention it. But sometimes I change what I do in the dream without really knowing why - except when I wake up I <i>do</i> know why I did it.

I: Are you familiar with the concept of a Quantum Echo?

AC: Nope.

OBJECTIVE(interview1, TEXT_ONLY, interview_transcript_1.txt)
->DONE
=win
{ALICE} Quantum Echoes, eh...

{HATTER} Sounds like a video game.

{ALICE} Hehe. Or an experimental musician.
->DONE
=lose
{ALICE} Nope. This is all just noise now...

{HATTER} Let's hope it wasn't important.
->DONE

VAR interview2_array= "_difficulty^0.1__attempts^0__completed^0__noiseImage^4__triggerWords^<white><rabbit><jefferson><airplane>_"

==interview2
Interview Part 2

I: Imagine shouting into a room, closing the door, and then coming back later and hearing the echo of your own voice.

I: And this is how you'd remember and record information.

AC: For making notes, I prefer pen and paper.

I: Except we can't record quantum information on pen and paper. But we may be able to do it using other methods, like sound.

AC: There is a song that plays sometimes...

I: White Rabbit by Jefferson Airplane?

AC: Exactly.

OBJECTIVE(interview2, TEXT_ONLY, interview_transcript_2.txt)
->DONE

=win
{ALICE} Okay, so they're suggesting the song is somehow connected to...something.

{HATTER} The song? Oh, right. The song. 'When logic and proportion have fallen sloppy dead'...

{ALICE} What are you up to, future Alice...
->DONE
=lose
{ALICE} I'm not getting anything more out of this one...

{HATTER} Damn.
->DONE

VAR interview3_array= "_difficulty^0.24__attempts^0__completed^0__noiseImage^8__triggerWords^<veteran><predestinator><experiment>_"

==interview3
Interview Part 3

I: But the song hasn't been working as well any longer, right?

AC: Mm. I think it was connected to the Veteran, not me.

I: Right, yes, the previous Predestinator.

AC: But now that that's me, I might need my own music?

I: Or other sounds. That's the experiment we'll be doing here at QPAC.

AC: Exciting!

OBJECTIVE(interview3, TEXT_ONLY, interview_transcript_3.txt)
->DONE

=win
{ALICE} Experiments! We're doing experiments! On music, it seems.

{HATTER} Curiouser and curiouser.

{ALICE} I feel like I can almost remember...
->DONE
=lose
{ALICE} Ach, just noise. Dammit.

{HATTER} Hopefully nothing of value was lost!
->DONE

VAR experiment_array= "_difficulty^0.24__attempts^0__completed^0__noiseImage^8__triggerWords^<qpac><experiment><predestination><rbthole>_"

==experiment
Quantum Predestination Echo Experiment \#1

Performed at the Quantum Predestination Analysis Centre (QPAC)

Presiding staff: Dawn Forrest, PhD, Quantum Physics at the Claremont Campus office.

\//Message:

Hey Alice! It's me! Or you! Don't worry, you're not. At least I don't think you're dead. This is a message, from the future. If you're getting this, it succeeded, yay!

To confirm though, I need you to open up one last folder. It's called rbthole, and the code to it is one only you would know ;) Well, you and Joe I guess.

Woo! Go science!
\// End of Message

OBJECTIVE(experiment, TEXT_ONLY, the_experiment.txt)
->DONE

=win
{ALICE} Well, well, well. So it seems I can now communicate with my past self. Somehow.

{HATTER} Seems to go against the spirit of the thing a little.

{ALICE} ...I suspect there's a lot more to this actually. Gotta check out that last folder I guess.

{HATTER} And the code to it?

{ALICE} Well, you know. 4174 of course.

{HATTER} Of course.
->DONE
=lose
{ALICE} Well, that didn't work out. Hmmm.

{HATTER} What even was this text? It wasn't there when you started.

{ALICE} A message? An experiment? I guess? Weird. There's a folder here too though - with a password.

{HATTER} Rbthole. I wonder...do you think it could be...

{ALICE} The numbers? Haha, maybe. Worth a try...

{HATTER} 4714?

{ALICE} You remembered!
->DONE


VAR text_test_array= "_difficulty^0.3__attempts^0__completed^0__noiseImage^3__triggerWords^<denounce><dislike><demoralized><desire><duty>_"

==text_test
On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from. 

OBJECTIVE(text_test, TEXT_ONLY, Test Text.txt)
->DONE
=win
{ALICE} This text happens when you win the text test.

{HATTER} Ok, neat I guess.
->DONE
=lose
{ALICE} This is what happens when you lose :( :(

{HATTER} Ohnoes.
->DONE

==text_test2
This is the second test text and it has stuff like shrinking, beguiled, blame weakness and men as the trigger words. Also an image. 

OBJECTIVE(text_test2, TEXT_IMAGE, Test Text 2.txt, testImage)
->DONE
=win
{ALICE} This text happens when you win the text test.

{HATTER} Ok, neat I guess.
->DONE
=lose
{ALICE} This is what happens when you lose :( :(

{HATTER} Ohnoes.
->DONE