# Entwickermodus FP4

```
einstellungen --> über das telefon --> viermal tippen auf Build Nummer
```
### Check USB Debugging on the Device:

        Ensure that USB debugging is enabled on your Android device. You can do this by going to Settings > Developer options > USB debugging.

### Check USB Connection Mode:

        Make sure your Android device is set to "File Transfer" (MTP) mode when connected via USB. Some devices may not allow ADB access in other modes like "Charge only."

    Create a udev Rule:

        On Linux, you can create a udev rule to allow your user to access the USB device without requiring root privileges.

        First, identify the vendor ID and product ID of your device:

```
        lsusb
```
        Look for your device in the output. It will look something like this:
        Copy

        Bus 001 Device 012: ID 18d1:4ee7 Google Inc. Nexus/Pixel (debug)

        Here, 18d1 is the vendor ID, and 4ee7 is the product ID.

        Create a new udev rule file:
     
```
        sudo nano /etc/udev/rules.d/51-android.rules
```
        Add the following line to the file, replacing idVendor and idProduct with the values you found:
      

        SUBSYSTEM=="usb", ATTR{idVendor}=="18d1", ATTR{idProduct}=="4ee7", MODE="0666", GROUP="plugdev"

        Save and close the file, then reload the udev rules:
```
        sudo udevadm control --reload-rules
        sudo udevadm trigger
```
    Restart ADB Server:

        After setting up the udev rule, restart the ADB server:
  ```
        adb kill-server
        adb start-server
```
    Check User Group Membership:

        Ensure that your user is part of the plugdev group:
```        

        sudo usermod -aG plugdev $USER
```
        Log out and log back in for the group change to take effect.

    Reconnect the Device:

        Disconnect and reconnect your Android device to the computer. ADB should now be able to access the device without permission issues.

    Verify Connection:

        Run the following command to verify that the device is connected:
       
```
        adb devices
```
        If everything is set up correctly, you should see your device listed.

Additional Notes:

    If you are using a custom ROM or a device with a non-standard USB configuration, you may need to adjust the udev rule accordingly.

    If the issue persists, consider running ADB as root (not recommended for regular use) or checking for any additional security settings on your Linux system that might be restricting access.

By following these steps, you should be able to resolve the "Access denied (insufficient permissions)" error and successfully connect your Android device via ADB.
New chat
