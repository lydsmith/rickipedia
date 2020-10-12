# *Rickipedia* - **Xamarin Android project**

An Android app which allows you to delve into the Rick and Morty universe via its range of quirky characters. Search for characters by name or browse by status and gender. Click on any of the character cards to view their full profile as well as see which episodes they feature in.

### **Requirements to run the app**

To run this app you will either need access to an Android mobile device, or use an emulator to run the application.

### **Run APK directly on Android Device**

This repo contains an APK file which can be sideloaded directly onto an Android device. Install this file and open the application.

### **Run with Visual Studio and Android Emulator**

1. Clone the repo via HTTPS using the command:
```
 git clone https://github.com/lydsmith/rickipedia.git
```
2. Ensure you have Visual Studio 2019 with Mobile Development installed. If you haven’t got this installed, go to Visual Studio Installer, find your version of Visual Studio and Select ‘Modify’. This will take you to a screen where you can add Mobile Development to your setup.
Once you have VS2019 set up you can load the Xamarin Project into Visual Studio by selecting the solution file (.sln). You may need to download, install or update your Android SDK to get the Android project working. 

3. If this is the first time running an Android emulator you will then need to set up a new emulator. This can be done from within Visual Studio under Tools>Android>Android Device Monitor. Once this is set up you can simply select the Android project from the Startup Projects dropdown and run (f5).

### **Run with Visual Studio and Android Device**

- To run on a real Android device, follow steps 1 & 2 above, then enable USB debugging from within the Settings on your device.
This will make your Android device visible in the available devices dropdown from within Visual Studio - select the Android project in Startup Projects, then click the arrow to the right of this dropdown. You will see which android device is currently selected and you will see your phone or device there.
Select your device and hit Play, and the app will deploy to your device.
