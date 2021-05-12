# ADP2-Flight-Inspection-App
Our .NET application is designed for people who want to study sampled flight data.
The user uploads a text file containing data that was sampled during a flight, and the application visually displays the file data from start to end.
The FlightGear simulator displays the flying plane, while another window displays the main control surface(in a joystick like view), other flight instruments, and correlating data types in various graphs.
This window also enables the user to jump to specific time locations using an interactive player, and check for anomalies, using a selection of C++ DLLs which are dynamically loaded as needed.

## Explanations of the folders and the main files of the project
In the main folder, FIApp, there are the source files of the project, and 3 folders - Properties, Resources and plugins.<br/>
"Properties" contains the settings files of the project.<br/>
"Resources" contains the images for the controllers, the "playback_small.xml" file and the LearnNormal.dll that inside there is the algorithm of the function that explore the flight details.<br/>
"plugins" contains the DLL of the anomalies detectors.<br/>
The source code consists of several controllers. Each controller is implemented by a view and a view model, whereas the model is shared
for all of them.<br/> 
The main window is responsible for displaying the application using three tabs.<br/>
The first is the home screen where the user uploads a csv file of sampled flight data and starts interfacing with Flight-Gear.<br/>
The second displays the plain's flight instruments and the joystick.<br/>
The third displays the anomaly detectors and graphs (the chosen feature graph during the flight, the correlative feature graph and their regression line).<br/>
The playback rate can be controlled by the player in the second and the third tab.<br/>

## Instructions
1. Download FlightGear flight simulator version 2020.3.8 to your computer [link for download](<https://www.flightgear.org/>) 
2. Make sure that flightgear is installed in your "C:\Program Files" folder
3. Put the anomalies DLL that you want to use in "C:\Users\<UserName>\plugins"
4. Copy the file "playback_small.xml" located in the "Resources" folder to "C:\Program Files\FlightGear 2020.3\data\Protocol" 
In order to run the application, follow the instructions in the home screen. After the three levels are completed, move to the second tab and press start.
Now, the application will display the data and the flight gear simulator will display the plane simultaneously.

## Links
here it will be a link for UML and a short video.

## Contributors
Noam Burgansky, Sapir Hender, Tal Cohen and Ella Shalom.
