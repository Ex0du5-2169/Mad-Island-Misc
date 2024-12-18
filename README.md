"# Mad-Island-Misc" 
Mod planning:

This started out as a basic mod to allow Reika to be impregnated, thus some leftover mod and variable names are still there. Naturally the early versions were janky and used for testing various functions.
Ultimately will become a full pregnancy handler for the game.

Short term plans:

1. Add ability for Yona and others to become pregnant by several NPCs/monsters - basic code in place - needs further testing.
2. Add "body_preg" object for Yona (and others who need one) - in-progress.
3. Add birthing animation/placeholder for those who don't have one - Yona, Reika and Nami done - needs testing.
4. Track impregnating monster and make offspring of a similar type by overriding default birth coroutine.

Long term plans:
1. Handle all default NPC pregnancies.
2. Handle monster related pregnancies as a new feature.
3. Create quasi-menstrual system.
4. Add appropriate body parts for those that lack them.


Change log:

14/10/2024 00:06 - Added a list of animations used for birthing and some potential replacements for Yona, need to find others for Nami, Reika et al.
                 - Annotated some of my code. It's a mess but at least you can see my intentions with the commented lines.
                 
17/10/2024 12:31 - Changed mod name and guid.
                 - Added some test code for animation substitution.

29/10/2024 13:28 - Started work on the basis of the new pregnancy system.
                 - Removed some useless scripts and changed some variable names.

06/11/2024 09:35 - Basic pregnancy system in-place, needs testing.
                 - Atlas modifying code provided by NM088, needs altering for the purposes of this mod.

18/12/2024 17:08 - Added Merry (Santa) character compatibility.
                 - Removed a fair amount of old or commented-out code that is no longer necessary.
                 - Removed useless conditionals from a number of areas.
                 - Added code to attempt to catch R events and trigger a pregnancy roll when they are detected (needs testing).
                 - Simplified the animation swapping code and added debug logging to help figure out why it's failing.
                 - Added code to attempt to address sex automation between tame NPCs, story NPCs and MCs (needs testing).
