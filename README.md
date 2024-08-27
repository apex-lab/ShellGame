To properly run this project with LSL: 

1. Download and add to Unity then open project
2. Open terminal, navigate to project folder Assets/ShellGame/Scripts
3. Run command "py -m ShellGameLSL" (Windows, may vary for other systems)
4. Play in Unity

LSL Data will be logged to a csv file in the Assets/ShellGame/Scripts folder with the following columns.
- Time - LSL Timestamp of event
- Ray Origin - Where the user is looking from
- Direction - Direction of eyes
- Object Tag - Tag of the object being looked at (Correct/Incorrect indicate one of the crates, rest are named accordingly)
- Event - Space for event tags such as distractors and sound effects, otherwise empty
