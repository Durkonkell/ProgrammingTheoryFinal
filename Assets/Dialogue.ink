// Dialogue for the programming theory final submission
// Alexander Gowler-Spiers March 2023

VAR chickens = 0
VAR barriers = 0
VAR herbs = 0

VAR mooseTransform = false

-> FARMER

==Menu==
-> END

/*
Main Menu
+ Farmer 
-> FARMER
+ Druid
-> DRUID
+ Engineer
-> ENGINEER
+ Escape Example
-> escape
+ Increment chickens
 ~chickens++ 
 * Increment herbs
 ~herbs++
 + Increment barriers
 ~barriers++

- ->Menu 
*/

===FARMER===
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
* I'll help[!], of course! 

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
F: Hey chick. Chick chick chick. Good chick.
->END

=== DRUID ===
    {
    - Herb_Complete:
    -> Human
    - DRUID > 1:
    -> In_Progress
    }
N: The moose watches you approach. It doesn't seem spooked or aggressive, but merely turns to face you, head tilted slightly to expose a single black eye.
    * Hello, Moose?
    * Are you a real moose[?], or one of those Druids who likes to shapeshift?

-M: MOOSE NOISES

N: Unfortunately, you do not speak moose.
    * [Do you need help?] Is there something I can help you with?
    * Are you looking for something?[] What... sort of thing could a moose possibly want?

- M: DETERMINED MOOSE NOISES
N: The moose locks eyes with you and appears to focus. At once, the world fades and you see before you a distinctive plant with bright yellow flowers.
    * You... want me to find this plant?
    N: The moose nods its head at you enthusiastically. You move out of the way of an antler.
    * Did you do that[?], with the plant?
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
    M: MOOSE URGING
-> Menu
* { herbs == 1 } Is this it?
N: You show the moose the yellow-flowered herb, the only one you could find.
M: JOYFUL MOOSE NOISES
N: The moose grabs the plant from your hands, and lowers its head. Once again you dodge the antler-points. You hear crunching.
-> Herb_Complete

= Herb_Complete
~mooseTransform = true
-> Menu

= Human
D: Oh my, what a terrible fright! Thank you for getting me out of moosemode.

- (Questions) N: {The Druid breathes deeply, steadying himself.|The Druid regards you attentively.}
    * [Glad to help.] I'm glad I was able to help.
        D: It's fortunate that you were able to find that herb! It's very rare in these parts.
        D: Without it I would've had to trot two hundred miles back to the circle, and it wouldn't have done a bit of good for my reputation, you see.
        -> Questions
    * How did you get stuck[?] like that, anyway?
        D: Cursed by a naughty wizard, I fear! Rather embarrassing actually.
        D: It's one thing to be able to shapeshift, quite another to not be able to turn back. Quite a fright, I tell you.
        D: I suppose I'll have to find the wizard and do something druidy to them. Wrap them in vines or something. It's really rather bothersome.
        -> Questions
    + Farewell, Druid.
        D: Goodbye! May the spirits of nature look fondly upon you and so forth.
        -> Menu

=== ENGINEER ===
    {   
    -Barriers_Complete:
    -> Idle  
    -ENGINEER > 1: 
    -> In_Progress
    }

E: What a mess, what a mess...
    * Uh, hello?
    E: Hm? Oh, hello.
    * What happened here?!
- E: Right, this business behind me?
N: He gestures vaguely towards what appears to be a crash site of some kind.
E: ...Someone ran their official engineering truck up a log ramp and flipped it upside down.
    * Seems like a problem!
    E: Oh, it is! <>
    * And you're here to fix it?
    E: Exactly! <>

-I need to clean this site up, and that means barriers!
E: Fortunately my-- this truck should have a set on board. Unfortunately they appear to have been scattered by the crash.
E: Could you see if you could find them for me? There ought to be 3.
E: I'll stay here and make sure nobody gets hurt in the absence of clear warning signage.
-> Menu

= In_Progress
E: {Wh-what truck? Oh, it's you! How're we doing with the crash barriers? | Any luck?}
    + { barriers == 0 } None so far[.], but I'm looking hard!
    + { barriers == 1 } I found one[.], so far.
    + { barriers == 2 } I've found two[.] of them!
    * { barriers == 3 } [Found them all] I found them all! It should be safer now, I guess?
        -> Barriers_Complete
- { barriers < 3: Okay, good good, good, good. Ought to be another {3 - barriers} about! }
-> Menu

= Barriers_Complete
E: Great, good, excellent! Now I can concentrate on sorting this mess out without worrying about passersby.
E: Thank you. You have been a great help to the cause of civil engineering.
    * You're welcome[?], I guess?
    * Good luck[.], Engineer. Hope you get it sorted soon!
    
-E: Hmm. Maybe a crowbar? Or chisel the rock into a ramp?
N: The Engineer appears to have lost track of your presence somewhat. He continues to mutter, apparently considering his options.
-> Menu

= Idle
 E: {I... don't suppose you happen to have a crane handy, do you? | Have you spotted any dynamite around and would you tell me if you had? | If only I had another truck... }
    + Sorry.
    E: Oh, it's okay. I'm sure my engineering prowess will pay off soon.
    -> Menu
    + You'll get there![] You're an Engineer, after all.
    E: Thank you! You're right,  I shall redouble my efforts.
    -> Menu

=== DOG ===
 Dog: {~ Ruff! | Bork! | Arf ruff! | Ruff. Arf? | Barkbark! Barkbarkbark! | BOOF. | Gruff? | Meow? | Arf! }
    + Good boy![] Good doggo! Yes!
        Dog: Borf!!
        N: The dog wags his tail enthusiastically. His expression and general demeanor seem to say "What?! ME?!? AMAZING!!"
        -> Menu
    + [Pet the dog.]
        Dog: Rrf.
        N: The Dog does not purr, but he does seem to appreciate the attention nonetheless.
        -> Menu

