# ADP2-Flight-Inspection-App

Our .NET Application in C# that analyzes recorded (simulated) flight data using a selection of C++ DLLs which are dynamically loaded as needed. 
The application enables the user to view correlating data types in various graphs, jump to specific time locations using an interactive player, and view the planes orientation and location in a joystick like view.
## Explanation on the project and his folders
First, in "Resources" folder there is the images for the player, anomalies, joystick and LearnNormal.dll that inside there is the algorithm of the function that explore the flight details.<br/>
Second, in "Properties" folder there is Visual studio files.<br/>
Third, in "plugins" directory there is the DLL of the anomalies detectors.<br/>
And all the rest of the files is the source code of the project. For each controller there is a view-model and a view, whereas the model is shared
for all of the controllers.<br/>
In the source code, there is the "StartFlightGearViewModel.cs" that responsible for displaying the entire application. Inside this file there is
three tabs when the first is the open view (upload csv and interfacing with Flight-Gear), the second is the the planes orientation and location in a joystick and player. The third is the anomaly detector, graphs (the chosen feature graph during the flight, the correlative feature graph and their regression line) and a player.<br/>

## Installation
Download the package "Oxyplot.Core" and "Oxyplot.Wpf".

```bash
Install-Package Oxyplot.Core
Install-Package Oxyplot.Wpf
```
## Instructions
1. Make sure that FlightGear simulator version 2020.3.6 is installed on your computer [link for download](<https://www.flightgear.org/download/>)
2. Put the anomalies DLL that you want to use in "C:\Users\<UserName>\plugins"

## Links
here it will be a link for UML and a short video.

## Contributing
Noam Burganski, Sapir Hender, Tal Choen and Ella Shalom.
