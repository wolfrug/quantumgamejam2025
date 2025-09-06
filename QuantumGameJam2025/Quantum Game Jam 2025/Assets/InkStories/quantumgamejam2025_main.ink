INCLUDE quantumgamejam2025_functions.ink

VAR text_test_array= "_difficulty^0.3__attempts^0__completed^0__triggerWords^<denounce><dislike><demoralized><desire><duty>_"

VAR text_test2_array= "_difficulty^0.6__attempts^0__completed^0__triggerWords^<shrinking><beguiled><blame><weakness><men>_"

VAR testDictionary = ""

->start
==start
{ALICE} This is a pink text box.
{HATTER} And this is a blue one.

* [Let's make a choice shall we.]
{HATTER} This was a choice.
* [A choice with options.]
{HATTER} This is definitely a choice.

 - {ALICE} Well okay then.

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