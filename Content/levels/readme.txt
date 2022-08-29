char to game object map:
# -> filler
0 -> wall
. -> ice
= -> thick_ice
* -> player_end
p -> player
k -> key on ice
T -> key on thick_ice
? -> key on block_end
x -> key_hole
t -> teleporter
+ -> block
@ -> block_end
& -> block on thick_ice
s -> treasure
$ -> treasure on thick_ice
S -> treasure on block_end
X -> text area

constraints:
map must be 19 by 17 (width x height)
first and last row must be ~ characters only
0 or 2 teleporters
only 1 block
only 1 player
each key hole needs a key

if constraints are not followed, game will crash

example level
XXXXXXXXXXXXXXXXXXX
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:000000000000000:::
:0*...........p0:::
:000000000000000:::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
XXXXXXXXXXXXXXXXXXX


blank level
XXXXXXXXXXXXXXXXXXX
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
:::::::::::::::::::
XXXXXXXXXXXXXXXXXXX