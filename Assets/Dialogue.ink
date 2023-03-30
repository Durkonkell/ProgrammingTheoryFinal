// Dialogue for the programming theory final submission
// Alexander Gowler-Spiers March 2023

VAR chickens = 0
VAR barriers = 0
VAR herbs = 0

-> Menu

==Menu==
Main Menu
+ Farmer 
-> FARMER
+ Druid
-> DRUID
+ Escape Example
-> escape
+ Increment chickens
 ~chickens++ 
 * Increment herbs
 ~herbs++

->Menu

===FARMER===
// Put some conditional here to divert to Greet 2
    { 
    - Chickens_Complete:
    -> Idle
    - FARMER > 1:
    -> Greet_2 
    }

F: Ayup.
* Hello, Farmer.
* How do?

-  -> Chicken_Brief

= Greet_2
F: Ayup.
+ Hello again.
-> Chicken_Progress

= Chicken_Brief
F: Have been up wi' cows. Dozed off for a spell, dare say. Now've got nowt for chickens.
* Oh dear!
F: Aye.
* [How unfortunate!] What an unfortunate turn of events!
F: Aye.
* I'll help[.], of course! 

-F: Keep an eye out, aye?

-> Menu

= Chicken_Progress
{ chickens == 1:  F: Oh, aye. It's a chicken, that is. }
{ chickens == 2:  F: Good, good, Got some seed here for you. }
{ chickens == 2:  F: I mean the chickens. You'll not be wanting seed. }
{ chickens == 3:  F: Let's see. Oh, aye. That's them chickens, that is. }

{ chickens < 3:  F: Ought to be another { 3 - chickens } about. }

    + { chickens < 3 } I'll keep looking! -> Menu
    * { chickens == 3 } Glad I could help!
    F: Ta. Will be off with this lot t'coop presently. 
        ** Farewell, Farmer!
        F: Aye.
        -> Chickens_Complete
    
= Chickens_Complete
->Menu

= Idle
F: Ho chick. Chick chick chick. Good chick.
->END

=== DRUID ===
    {
    - DRUID > 1:
    -> In_Progress
    }
N: The moose watches you approach. It doesn't seem spooked or agressive, but merely turns to face you, head tilted slightly to expose a single black eye.
    * Hello, Moose?
    * Are you a real moose[?], or one of those Druids who shapeshift?

-M: MOOSE NOISES

N: Unfortunately, you do not speak moose.
    * [Do you need help?] Is there something I can help you with?
    * I suppose you've lost something?

- M: DETERMINED MOOSE NOISES
N: The moose locks eyes with you and appears to focus. At once, the world fades and you see before you a distinctive plant with bright yellow flowers.
    * You... want me to find this plant?
    N: The moose nods its head at you enthusiastically. You move out of the way of an antler.
    * Did you do that[?], or am I hallucinating?
    N: The moose bobs its head.
        ** What about this plant?
        N: The moose points a hoof in one direction and then another.
        M: PLAINTIVE MOOSE NOISES
            *** You want me to find it!
            N: The moose nods aggressively. You quickly evade the point of an antler.
            M: ENTHUSIASTIC MOOSE NOISES
            
    - * Alright then.[] I'll have a look and see what I can find.
    -> Menu

= In_Progress
M: INQUISITIVE MOOSE NOISES
+ { herbs == 0 } I haven't found it[.] yet. I'll keep looking!
-> Menu
* { herbs == 1 } Is this it?
N: You show the moose the yellow-flowered herb, the only one you could find.
M: JOYFUL MOOSE NOISES
N: The moose grabs the plant from your hands, and lowers its head. Once again you dodge the antler-points. You hear crunching.
-> END

= Human
This is where human-form dialogue will go.

=== escape ===
I ran through the forest, the dogs snapping at my heels.

	* 	I checked the jewels[] were still in my pocket, and the feel of them brought a spring to my step. <>

	*  I did not pause for breath[] but kept on running. <>

	*	I cheered with joy. <>

- 	The road could not be much further! Mackie would have the engine running, and then I'd be safe.

	*	I reached the road and looked about[]. And would you believe it?
	* 	I should interrupt to say Mackie is normally very reliable[]. He's never once let me down. Or rather, never once, previously to that night.

-	The road was empty. Mackie was nowhere to be seen.

-> END