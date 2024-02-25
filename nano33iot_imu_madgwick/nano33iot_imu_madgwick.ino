/*  Example for Input-Output Lab 5
 * 
 *  Install the LSM6DS3 and the Madgwick libraries
 *  based on: https://itp.nyu.edu/physcomp/lessons/accelerometers-gyros-and-imus-the-basics/
*/

#include "Arduino_LSM6DS3.h"
#include "MadgwickAHRS.h"

// initialize a Madgwick filter:
Madgwick filter;

// values for orientation:
float yaw = 0.0;
float pitch = 0.0;
float roll = 0.0;
int pinNumber = 8;

void setup() {
  Serial.begin(57600);
  pinMode(pinNumber, INPUT);
  // attempt to start the IMU:
  if (!IMU.begin()) {
    Serial.println("Failed to initialize IMU");
    // stop here if you can't access the IMU:
    while (true);
  }
  
  // start the filter to run at the sample rate of the IMU
  filter.begin(IMU.accelerationSampleRate());
}

void loop() {
  // values for acceleration and rotation:
  float xAcc, yAcc, zAcc;
  float xGyro, yGyro, zGyro;
  // int buttonState = digitalRead(pinNumber);

  // check if the IMU is ready to read:
  if (IMU.accelerationAvailable() && IMU.gyroscopeAvailable()) {
    // read accelerometer &and gyrometer:
    IMU.readAcceleration(xAcc, yAcc, zAcc);
    IMU.readGyroscope(xGyro, yGyro, zGyro);

    // update the filter, which computes orientation:
    filter.updateIMU(xGyro, yGyro, zGyro, xAcc, yAcc, zAcc);

    // print the yaw (heading), pitch and roll
    yaw   = filter.getYaw();
    pitch = filter.getPitch();
    roll  = filter.getRoll();
    
    //Serial.println("Orientation: ");
    Serial.print(yaw);
    Serial.print(",");
    Serial.print(pitch);
    Serial.print(",");
    Serial.println(roll);
    // Serial.print(",");
    // Serial.println(buttonState);
  }
}
