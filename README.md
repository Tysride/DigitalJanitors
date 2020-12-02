# DigitalJanitors
Digital Janitors is an action-packed desktop defense game where hackers have taken your employer's network hostage, which means the player must go to each computer in the company, excise the virus, and beat the hacker threat.

# What do I do?
    I work with the lead Programmer Shane Tupler to write all the code for the game
    I generaly make the scripts that apply to UI and Managers in the game
    Code I make varries depending on what the lead programmer may need
 
# Currency Folder
    Acts as the tracker and visual respresentaton of the players money
    Also applies to the shop

# PauseMenu 
    Functions as the manager for the pause menu in game
    Allows the player to navagate between the menus
    Allows player to change the settings of the game
# OptionsMenu
    Works with PauseMenu to show visual change in sliders
    Takes imput from sliders and adjusts systems like volume
    
# DangerBar
    Player Health Bar
    Increased Urgency when close to loosing
            Raised pitch of music 
            Screen gets glitchy
            Causes fail state when filled
# RandomShop 
    Currently functions *not* as a random shop but will be in future updates of the game
    Alows the player to pick from three hand picked items
    handpicked items are determined in engine based on what we want to show off for a demo or playtest
    
# Clock
    Let's the player have a little clock in the bottom right
    Changes time through the level
# TaskBarOpen
    If the player hovers over icons at the bottom of the screen and clicks it will open it
    simulates a task bar (go ahead and try it for yourself it acts just like it...sorta)
# Scanner
    Opens a tab on the simulated desktop that will scan unknown files 
    Reports if they are good or bad
    
# Boss 1 
    Joint effort with me and the lead programmer
    Acts as the manager Boss 1
    The laser script creates random set of lasers to spawn in a phase
# Boss 2 
I had Primary lead programming and concepting for this boss
# Boss 2 Spawn
    Acts as the manager for the Boss 2 fight
    Spawns folder at the start 
    connects with kill folder to restart when failstate is met
    Easy to edit in the Unity scene for tweaking the difficult later
# Kill folder
    This script will detect when the folder colides with the obstacals
    Will wipe the screen when the phase is complete or when the player colides with obstiacles
    

