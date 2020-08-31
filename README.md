## NOTE: The active development on this application has stopped. It's impossible for me to keep up with the newest GPD hardware without being sponsored. You can fork this project and adjust it to your needs, but I won't be able to give any support. 

# GPD Win 2 XTU Manager

GPD Win 2 XTU Manager is an application developed by [@BlackDragonBE](https://twitter.com/BlackDragonBE) to make applying XTU (Intel Extreme Tuning Utility) profiles on the GPD Win 2 as easy and fast as possible. This allows you to change the TDP and CPU and GPU undervolts with just a single click!

## How To Use

Download the [newest release ZIP](https://github.com/BlackDragonBE/GPDWin2XTUManager/releases) and unpack it somewhere safe. Now start **GPDWin2XTUManager.exe** with administrator privileges. (it will request this by default)  
  
You'll see the following screen:

![](Images/Main105.png)

By default, there's only the STOCK profile. You can click the button any time to revert back to stock settings.  
To start creating your own profile, either click any of the **Create profile...** buttons or the **Settings** button. This will open up the **Settings** window:

![](Images/Options105.png)

The defined profiles are shown on the left side. Selecting any of these updates the fields on the right. The STOCK profile can't be adjusted.  
To create a new profile, click the **plus** button below the profile list. A new profile will be added to the list. Click on the newly made profile to select it.

The default values will be the same as the STOCK profile, you can alter these to your liking. Every change you make here is automatically saved to the profiles list.

To delete the selected profile, press the **trashcan** button. The application will ask if you're sure to prevent accidental removals.

You can automatically apply any of the profiles after logging in by checking the **Apply profile at log on** checkbox and choosing a profile in the dropdown next to it. Unchecking the checkbox will remove the log on entry from the registry.

When you're done making changes, either close the window using the Windows **close button** or press the **OK button**. The profile buttons will reflect the changes you've made.

Click any of the defined profile buttons to apply the XTU profile. This will pass the settings to XTU and close the service. 
Once you're satisfied, you can either minimize the application (the app only uses about 20MB of RAM and doesn't use the CPU in the background) or close it. Closing the application doesn't revert the XTU settings.

## Command Line (Advanced)

The applications has a "hidden" feature, it allows you to apply settings without doing any manual work, either with a task or a CMD script.  

The following format can be used to apply a setting:

    [PATH-TO-EXE]/GPDWin2XTUManager.exe minimumWatt maximumWatt cpuUndervolt gpuUndervolt

For example:

    C:\XTUManager\GPDWin2XTUManager.exe 7 15 50 50

Since version 1.01 you can also just pass the profile name like this for example:

    C:\XTUManager\GPDWin2XTUManager.exe PERFORMANCE

These will apply the settings and immediately close the application afterwards. This is ideal for applying the same settings each time at the system startup. Just make sure the settings are stable first or you'll have to boot into safe mode to undo the task. You can also set up a task like this automatically in the Options screen.

## FAQ

Q1: I love this! Can I buy you a beer/coffee?  
A: Sure! You can donate [here](https://www.paypal.me/blackdragonbe). Thanks! <3

Q2: The app freezes whenever I try to apply a profile, why?  
A: See Q3 and Q5. The XTU was probably not installed correctly. The app also won't work on anything besides a GPD Win 2.

Q3: Do I still need to download and install the Intel Extreme Tuning Utility?  
A: Yes! This application "talks" with XTU in order to apply the settings. Download it here: https://downloadcenter.intel.com/download/24075/Intel-Extreme-Tuning-Utility-Intel-XTU-
The app will also prompt you to download XTU if it's not installed yet.  

Q4: Does XTU need to be constantly running in the background while using this?  
A: Nope, the application starts and stops the XTU service by itself. The XTU window doesn't even need to be opened as only the CLI (command line interface) is used.

Q5: I installed the latest version of Intel Extreme Tuning Utility, but the app still says XTU couldn't be found. What gives?  
A: Somewhere since April 2019, Intel decided not to include the CLI executable anymore with its setup. A workaround for this is to [download the older version included in the repo](https://github.com/BlackDragonBE/GPDWin2XTUManager/blob/master/XTU_Installer/XTU-Setup-6.4.1.25.exe), removing the current XTU you have installed and installing the one I have provided.

Q6: How can I update the application?  
A: Simply download the [newest release](https://github.com/BlackDragonBE/GPDWin2XTUManager/releases), unpack it and replace the contents of the folder with that of the older version. When coming from 1.02 or older, make sure none of your profile names contain spaces before attempting to load them at logon.  
  
Q7: My antivirus detected a trojan in the zip file, should I be concerned?  
A: I assure you those are false positives. If you know some programming, you can look at the source code to verify it's all safe and you can even compile the application yourself as the repo includes every single file. [The EXE itself does come out 100% clean in virus total](https://www.virustotal.com/#/file/ba977731854d83cd75122f2419c6be94df46483ebbaa097727d8f212430e4125/detection).
  
Q8: Why is it so ugly?  
A: Because I'm more of a programmer and a tinkerer than a designer. I've also made the UI extra chunky so it's easier to use with the touchscreen. If there are any designers out there that are familiar with WinForms and know how to make the application more attractive, feel free to reach out to me! 

Q9: I'd like to see [FEATURE HERE] implemented.  
A: First, that's not a question. Second, feel free to contact me on [Reddit](https://www.reddit.com/user/BlackDragonBE/) or [Twitter](https://twitter.com/BlackDragonBE) with any suggestions.

Q10: Can I share my list of profiles with others?  
A: Sure, just share the **Settings.json** file in the application folder, that holds all the profiles. You can also edit it manually, but I wouldn't recommend it.

Q11: I've used your application and I got a BSOD! What the frick?!  
A: Be sure to set up and test any values, especially the undervolts. Every device is different and has different stable settings. Don't be afraid to experiment and ask advice on [Reddit](https://www.reddit.com/r/gpdwin/)!
If you've set up an unstable profile to be applied at log on, [boot into safe mode](https://www.digitalcitizen.life/4-ways-boot-safe-mode-windows-10) and use the application to disable the automatic applying in the settings. Temporarily moving or renaming the executable by using WinPE or a linux live usb also works.

## Issues

Please create a new issue if there's a a problem with the application. Describe what you were doing and add a screenshot if applicable.  
Keep in mind that this is a free hobby project so I might not be able to resolve issues straight away, please be patient. Thanks!

## Feature Wish List

- Optionally change active power plan as well?
- You tell me! :)
