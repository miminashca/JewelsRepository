using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using GXPEngine;

class ArduinoControls : GameObject
{
    // Assigning starting variables
    float yaw = 0.0f;
    float roll = 0.0f;
    float pitch = 0.0f;
    bool leftButtonPressed;
    bool rightButtonPressed;
    float lastUpdateTime = 0.0f;
    float updateInterval = 16.0f;
    float previousYaw = 0.0f;
    float startYaw = 0.0f;
    SerialPort port;
    public ArduinoControls()
    {
        port = new SerialPort();
        port.PortName = "COM6";
        port.BaudRate = 9600;
        port.RtsEnable = true;
        port.DtrEnable = true;
    }

    public void UseFile(Controller platform, Player player)
    {
        // Delay the file usage a little to prevent an exception and check if the file is being used
        if (Time.time - lastUpdateTime > updateInterval && SerialPort.GetPortNames().Length != 0)
        {
            port.Open();
            port.DiscardInBuffer();
            port.DiscardOutBuffer();
            string incomingData = port.ReadLine();
            port.Close();
            incomingData = incomingData.Trim();
            string[] incomingString = incomingData.Split(new char[] { ',' });
            //foreach (string str in incomingString)
            //{
            //    Console.WriteLine(str);
            //}
            //Console.WriteLine(incomingString.Length);
            //Console.WriteLine();
            // Incase data is not read properly it preserves the previous value instead
            if (incomingString.Length == 5)
            {
                // I've adjusted the names of the variables to match the arduino when it is in a horizontal orientation
                // Checking if values are empty to prevent exceptions
                if (incomingString[0] != "" && incomingString[0] != null) { yaw = Convert.ToSingle(incomingString[0]); }
                if (incomingString[1] != "" && incomingString[1] != null) { roll = Convert.ToSingle(incomingString[1]); }
                if (incomingString[2] != "" && incomingString[2] != null) { pitch = Convert.ToSingle(incomingString[2]); }
                if (incomingString[3] != "" && incomingString[3] != null)
                {
                    if (Convert.ToSingle(incomingString[3]) < 0.5f) { leftButtonPressed = true; }
                    else { leftButtonPressed = false; }
                }
                if (incomingString[4] != "" && incomingString[4] != null)
                {
                    if (Convert.ToSingle(incomingString[4]) < 0.5f) { rightButtonPressed = true; }
                    else { rightButtonPressed = false; }
                }
                // Setting starting yaw once. The assumption is that the arduino will be in a horizontal position
                if (startYaw == 0.0f)
                {
                    startYaw = yaw;
                }
                //Checking if the movement is natural by checking for big jumps in values.
                if (previousYaw - yaw < 45 || yaw - previousYaw > 315)
                {
                    // Rotating the object based on the given value and inverting the value depending on the orientation of the arduino
                    if (startYaw - yaw > 45) { platform.rotation = roll; }
                    else { platform.rotation = roll * -1; }
                    previousYaw = yaw;
                }
                //Console.WriteLine("Left button pressed: {0} Right button pressed: {1}", leftButtonPressed, rightButtonPressed);
                if (leftButtonPressed)
                {
                    player.MoveUntilCollision(-player.speed, 0);
                    player.SetCycle(0, 3);
                    player.Mirror(true, false);

                }
                else if (rightButtonPressed)
                {
                    player.MoveUntilCollision(player.speed, 0);
                    player.SetCycle(0, 3);
                    player.Mirror(false, false);
                }
                else
                {
                    player.SetCycle(4, 3);
                }
            }
            lastUpdateTime = Time.time;
        }
    }
}
