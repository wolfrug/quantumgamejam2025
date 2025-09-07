INCLUDE quantumgamejam2025_functions.ink

VAR text_test_array= "_difficulty^0.3__attempts^0__completed^0__noiseImage^3__triggerWords^<denounce><dislike><demoralized><desire><duty>_"

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

{HATTER} And you have no idea who that is?

{ALICE} Not yet I don't. Hmmm.

{HATTER} What?

{ALICE} There's a folder here called \#QPAC, but it has a password on it.

{HATTER} Future you is determined to make things hard for you, I see...
->DONE
=lose
{ALICE} Dammit..
->DONE

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