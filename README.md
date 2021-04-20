<h1>Pokemon Dojo</h1>

<h2>Description:</h2>
Text based Pokemon battle simulator, wherein pokemon are compared via their respective base stats. 
<h2>Requirements and Features:</h2>

<strong>(Implement a “master loop” console application where the user can repeatedly enter commands/perform actions, including choosing to exit the program):</strong> 
The main method in Program.cs finds the user selecting three pokemon or commanding the program to randomly select three pokemon for them, subsequently choosing their first pokemon to send into battle against the opposing team, selecting any replacement pokemon in the event of the current battling pokemon's defeat (or choosing to concede, exiting the program). Upon the battle's resolution, the user is prompted to decide whether they would like to battle again or exit the program. If they choose to continue, the main method resets, and the user can continue play ad infinitum.



<strong>(Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program):</strong>
At line #__ in Program.cs, I've created a function to construct a complete list of pokemon found in Pokemon.csv, generating several different properties, including names, stats, legendary-status, and more. Various properties of a selected pokemon are used and displayed at many points throughout the application (e.g. when two pokemon are contending in the 'Battle()' method, each of their stats are compared, and the name of the winner of each stat comparison is displayed, along with the respective stat.

<strong>(Read data from an external file, such as text, JSON, CSV, etc and use that data in your application):</strong>
All data handled in the paragraph preceding this one is read from Pokemon.csv.

