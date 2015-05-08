# SnakeBasic

It's a very simple Snake console program written in C#.

I look up to perfect OOP design. The class structure is a perfect base to achieve this.

# Vectordefinition

    ----------> x
    |
    |
    |
    |
    V
    y

Y points down, so it's easier to draw line by line

Maximum characters of default console window:

Column: 35
Line: 25
Total: 875

# Redraw

Snake: Changes position as level goes by => frequent refresh
Candy: as soon as collected => rare refresh
Wall: always same position => no refresh, only initial draw

# Keyboard

Only the latest pressed event will be processed.

First, we'll check which arrow key was pressed. After that, the direction change will be validated.
The direction change is not allowed, if the rotation would be 180 degrees (e.g.: left to right).

# Q&A

Q: Why don't you just subscribe to the KeyDown Event to process the arrow keys?
A: Console applications don't supply a KeyDown Event.
