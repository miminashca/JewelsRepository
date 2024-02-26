// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using System.IO.Ports; //the problem starts here
// using GXPEngine;
//
// class ArduinoControls : GameObject
// {
//     // Assigning starting variables
//     float yaw = 0.0f;
//     float roll = 0.0f;
//     float pitch = 0.0f;
//     float lastUpdateTime = 0.0f;
//     float updateInterval = 16.0f;
//     float previousYaw = 0.0f;
//     float startYaw = 0.0f;
//     SerialPort port;
//     public ArduinoControls()
//     {
//         port = new SerialPort();
//         port.PortName = "COM3";
//         port.BaudRate = 9600;
//         port.RtsEnable = true;
//         port.DtrEnable = true;
//     }
//
//     public void UseFile(Controller platform)
//     {
//         // Delay the file usage a little to prevent an exception
//         if (Time.time - lastUpdateTime > updateInterval)
//         {
//             port.Open();
//             port.DiscardInBuffer();
//             port.DiscardOutBuffer();
//             string incomingData = port.ReadLine();
//             port.Close();
//             incomingData = incomingData.Trim();
//             string[] incomingString = incomingData.Split(new char[] { ',' });
//             // Incase data is not read properly it preserves the previous value instead
//             if (incomingString.Length == 3)
//             {
//                 // I've adjusted the names of the variables to match the arduino when it is in a horizontal orientation
//                 // Checking if values are empty to prevent exceptions
//                 if (incomingString[0] != "" && incomingString[0] != null) { yaw = Convert.ToSingle(incomingString[0]); }
//                 if (incomingString[1] != "" && incomingString[1] != null) { roll = Convert.ToSingle(incomingString[1]); }
//                 if (incomingString[2] != "" && incomingString[2] != null) { pitch = Convert.ToSingle(incomingString[2]); }
//                 // Setting starting yaw once. The assumption is that the arduino will be in a horizontal position
//                 if (startYaw == 0.0f) 
//                 {
//                     startYaw = yaw;
//                 }
//
//                 //Checking if the movement is natural by checking for big jumps in values.
//                 //Console.WriteLine("Previous yaw: " + previousYaw);
//                 if (previousYaw - yaw < 45 || yaw - previousYaw > 315)
//                 {
//                     // Rotating the object based on the given value and inverting the value depending on the orientation of the arduino
//                     if (startYaw - yaw > 45) { platform.rotation = roll * -1; }
//                     else { platform.rotation = roll; }
//                     previousYaw = yaw;
//                 }
//             }
//             lastUpdateTime = Time.time;
//         }
//     }
// }
