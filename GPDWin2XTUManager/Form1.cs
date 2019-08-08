using GPDWin2XTUManager.Properties;
using GPDWin2XTUManager.UpdateChecks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.ServiceProcess;
using System.Windows.Forms;

namespace GPDWin2XTUManager
{
    public partial class MainForm : Form
    {
        private readonly List<Button> _profileButtons = new List<Button>();
        private List<XTUProfile> _xtuProfiles = new List<XTUProfile>();

        private GithubRelease _newRelease;

        public MainForm(string[] args = null)
        {
            InitializeComponent();
            CheckForXTU();
            StartXTUService();
            LoadProfilesIntoList();

            if (args == null)
            {
                return;
            }

            if (args.Length > 0)
            {
                if (args.Length == 4) // Temp profile application. Parameters: minW maxW cpuUV gpuUV
                {
                    XTUProfile tempProfile = new XTUProfile("TEMP", Convert.ToDouble(args[0]), Convert.ToDouble(args[1]), Convert.ToInt32(args[2]), Convert.ToInt32(args[3]), ProfileImage.Gaming);
                    ApplyXTUProfile(tempProfile);
                    Environment.Exit(0);
                }
                else if (args.Length == 1) // Apply profile by name. Parameter: profile name.
                {
                    XTUProfile profileToApply = _xtuProfiles.Find(p => p.Name == args[0]);

                    if (profileToApply != null)
                    {
                        ApplyXTUProfile(profileToApply);
                        Environment.Exit(0);
                    }
                    else
                    {
                        MessageBox.Show("Attempted to start a profile named " + args[0] + " that isn't defined. Closing application.", "GPD Win 2 XTU Manager: Profile not defined!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(404);
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect number of arguments. Expected 4, but was given " + args.Length);
                }
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            Text += " v" + Shared.VERSION.ToString(CultureInfo.InvariantCulture);
            ReadCurrentValues();

            _newRelease = await UpdateChecker.CheckForUpdates().ConfigureAwait(true);

            if (_newRelease != null)
            {
                btnUpdateAvailable.Visible = true;
                btnUpdateAvailable.Text = "v" + _newRelease.tag_name + " is available!\r\nClick for changelog.";
            }
        }

        private void CheckForXTU()
        {
            if (!File.Exists(Shared.XTU_PATH))
            {
                if (MessageBox.Show("The Intel Extreme Tuning Utility couldn't be found. If you did install XTU, read the FAQ on github about the latest version of XTU. Open download page?",
                        "Unable to find XTU", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    Process.Start("https://github.com/BlackDragonBE/GPDWin2XTUManager/raw/master/XTU_Installer/XTU-Setup-6.4.1.25.exe");
                }

                Environment.Exit(0);
            }
            else
            {
                InitializeUI();
            }
        }

        private void InitializeUI()
        {
            Shared.PrepareImages();
            FillButtonList();
            CheckIntelDriver();
        }

        /// <summary>
        /// Checks the currently installed Intel driver version and warns if using a bad performing one.
        /// </summary>
        private void CheckIntelDriver()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");
            foreach (ManagementObject mo in mos.Get())
            {
                foreach (PropertyData property in mo.Properties)
                {
                    //Console.WriteLine(property.Name + "=" + property.Value); // DEBUG
                    if (property.Name == "DriverVersion")
                    {
                        string driverVersion = property.Value.ToString();
                        //Console.WriteLine(driverVersion);
                        txtInfo.Text += "Intel driver version: " + driverVersion + "\r\n";
                    }
                }
            }
        }

        private void PrintDriverWarningAndDownloadlink(string warning)
        {
            txtInfo.Text += warning + "\r\n";
            txtInfo.Text += "Recommended version: 6286\r\n";
            txtInfo.Text += "Download link: https://downloadcenter.intel.com/download/27988/Intel-Graphics-Driver-for-Windows-10\r\n(You can disable this check in the settings)\r\n\r\n";
        }

        private void ReadCurrentValues()
        {
            try
            {
                string minW = ExecuteInXTUAndGetOutput("-t -id 48").Trim();
                string maxW = ExecuteInXTUAndGetOutput("-t -id 47").Trim();
                string cpuUV = ExecuteInXTUAndGetOutput("-t -id 34").Trim();
                string gpuUV = ExecuteInXTUAndGetOutput("-t -id 100").Trim();

                txtInfo.Text += "Current values: \r\nMin W: \r\n" + minW + "\r\nMax W: \r\n" + maxW + "\r\nCPU UV: \r\n" + cpuUV + "\r\nGPU UV: \r\n" + gpuUV;
            }
            catch
            {
                txtInfo.Text += "Couldn't read current values.";
            }
        }

        private string ExecuteInXTUAndGetOutput(string command)
        {
            string output = string.Empty;

            ProcessStartInfo processStartInfo = new ProcessStartInfo(Shared.XTU_PATH, command)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Normal,
                UseShellExecute = false
            };

            Process process = Process.Start(processStartInfo);
            using (StreamReader streamReader = process.StandardOutput)
            {
                output = streamReader.ReadToEnd();
            }

            return output;
        }

        private void FillButtonList()
        {
            _profileButtons.Add(btnProfile1);
            _profileButtons.Add(btnProfile2);
            _profileButtons.Add(btnProfile3);
            _profileButtons.Add(btnProfile4);
            _profileButtons.Add(btnProfile5);
            _profileButtons.Add(btnProfile6);
            _profileButtons.Add(btnProfile7);
            _profileButtons.Add(btnProfile8);
        }

        private void LoadProfilesIntoList()
        {
            _xtuProfiles.Clear();

            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\" + Shared.SETTINGS_PATH))
            {
                Console.WriteLine("No settings found. Creating new one...");
                CreateNewSettings();
            }

            _xtuProfiles = JsonConvert.DeserializeObject<List<XTUProfile>>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\" + Shared.SETTINGS_PATH));
            _xtuProfiles[0].ProfileImage = ProfileImage.GPD;
            RefreshButtonInfo();
            GC.Collect(); // Fixes potential memory leak creaty by JSON deserializing
        }

        private void RefreshButtonInfo()
        {
            for (int i = 0; i < 8; i++)
            {
                if (i < _xtuProfiles.Count)
                {
                    XTUProfile profile = _xtuProfiles[i];
                    _profileButtons[i].Text = profile.Name + "\n\n" + "Min W: " + profile.MinimumWatt + "\nMax W: " +
                                              profile.MaximumWatt + "\nCPU: -" + profile.CPUUndervolt +
                                              " mV" + "\nGPU: -" + profile.GPUUndervolt + " mV";
                    _profileButtons[i].Image = Shared.IMAGE_RESOURCES_DICTIONARY[profile.ProfileImage];
                }
                else
                {
                    _profileButtons[i].Text = "Create profile...";
                    _profileButtons[i].Image = Resources.Plus;
                }
            }
        }

        private void CreateNewSettings()
        {
            AddDefaultProfiles();
            Shared.SaveProfilesToDisk(_xtuProfiles);
        }

        private void AddDefaultProfiles()
        {
            _xtuProfiles.Add(new XTUProfile("STOCK", 7, 15, 0, 0, ProfileImage.GPD));
        }

        public void ApplyProfileByButton(int number)
        {
            if (number < _xtuProfiles.Count)
            {
                txtInfo.Text = "Applying profile, please wait...";
                txtInfo.Refresh();
                ApplyXTUProfile(_xtuProfiles[number]);
            }
            else
            {
                OpenSettings();
            }
        }

        private void ApplyXTUProfile(XTUProfile xtuProfile)
        {
            string minWResult = "";
            string maxWResult = "";
            string cpuUvResult = "";
            string gpuUvResult = "";

            StartXTUService();
            // XTU doesn't apply the underclocks reliably, so we need multiple tries
            int maxTries = 5;
            int currentTry = 0;

            while (currentTry < maxTries && !cpuUvResult.Contains(xtuProfile.CPUUndervolt + "mV") || !gpuUvResult.Contains(xtuProfile.GPUUndervolt + "mV"))
            {
                currentTry++;

                minWResult = ExecuteInXTUAndGetOutput("-t -id 48 -v " + xtuProfile.MinimumWatt);
                Console.WriteLine(minWResult);
                maxWResult = ExecuteInXTUAndGetOutput("-t -id 47 -v " + xtuProfile.MaximumWatt);
                Console.WriteLine(maxWResult);
                cpuUvResult = ExecuteInXTUAndGetOutput("-t -id 34 -v -" + xtuProfile.CPUUndervolt);
                Console.WriteLine(cpuUvResult);
                gpuUvResult = ExecuteInXTUAndGetOutput("-t -id 100 -v -" + xtuProfile.GPUUndervolt);
                Console.WriteLine(gpuUvResult);

                // turbo boost power max
                ExecuteInXTUAndGetOutput("-t -id 66 -v " + 96);
            }

            if (minWResult.Contains("Successful") && maxWResult.Contains("Successful") && cpuUvResult.Contains("Successful") && gpuUvResult.Contains("Successful"))
            {
                txtInfo.Text = "Applied " + xtuProfile.Name + " profile succesfully!\r\n";
                Console.WriteLine("Applied " + xtuProfile.Name + " profile succesfully!");
                ReadCurrentValues();
            }
            else
            {
                txtInfo.Text = "Failed to fully apply " + xtuProfile.Name + " profile. Results:\n";
                txtInfo.Text += minWResult + "\n" + maxWResult + "\n" + cpuUvResult + "\n" + gpuUvResult + "\n";

                Console.WriteLine("Failed to fully apply " + xtuProfile.Name + " profile. Results:\n");
                Console.WriteLine(minWResult + "\n" + maxWResult + "\n" + cpuUvResult + "\n" + gpuUvResult + "\n");
            }

            StopXTUService();
        }

        private void StartXTUService()
        {
            ServiceController service = new ServiceController("XTU3SERVICE");
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(1000 * 15);

                if (service.Status != ServiceControllerStatus.Running)
                {
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: XTU Service not found! " + e);
            }
        }

        private void StopXTUService()
        {
            ServiceController service = new ServiceController("XTU3SERVICE");
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(1000 * 15);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch
            {
                Console.WriteLine("Error: XTU Service not found!");
            }
        }

        private void btnProfile1_Click(object sender, EventArgs e)
        {
            ApplyProfileByButton(0);
        }

        private void btnProfile2_Click(object sender, EventArgs e)
        {
            ApplyProfileByButton(1);
        }

        private void btnProfile3_Click(object sender, EventArgs e)
        {
            ApplyProfileByButton(2);
        }

        private void btnProfile4_Click(object sender, EventArgs e)
        {
            ApplyProfileByButton(3);
        }

        private void btnProfile5_Click(object sender, EventArgs e)
        {
            ApplyProfileByButton(4);
        }

        private void btnProfile6_Click(object sender, EventArgs e)
        {
            ApplyProfileByButton(5);
        }

        private void btnProfile7_Click(object sender, EventArgs e)
        {
            ApplyProfileByButton(6);
        }

        private void btnProfile8_Click(object sender, EventArgs e)
        {
            ApplyProfileByButton(7);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void OpenSettings()
        {
            Options frmOptions = new Options
            {
                Profiles = _xtuProfiles
            };
            frmOptions.FormClosed += FrmOptionsOnFormClosed;
            frmOptions.ShowDialog();
        }

        private void FrmOptionsOnFormClosed(object sender, FormClosedEventArgs formClosedEventArgs)
        {
            LoadProfilesIntoList();
        }

        private void btnUpdateAvailable_Click(object sender, EventArgs e)
        {
            // Show changelog, ask if user wants to open release page. If yes, open page in browser.
            if (MessageBox.Show(_newRelease.name + "\n\nChangelog:\n" + _newRelease.body + "\n\nDo you want to open the release page?", "Update available!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Process.Start(_newRelease.html_url);
            }
        }
    }
}