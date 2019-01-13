using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GPDWin2XTUManager.Properties;

namespace GPDWin2XTUManager
{
    public partial class Options : Form
    {
        public List<XTUProfile> Profiles = new List<XTUProfile>();

        public Options()
        {
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            UpdateProfileList();
            RemoveRegistryKey();
            CheckForLogonTask();
            FillIconList();
            lstOptionsProfiles.SelectedIndex = 0;
    }

        /// <summary>
        /// Removes existing reg key left by previous versions
        /// </summary>
        private void RemoveRegistryKey()
        {
            RegistryManager.ClearLogonProfileKey();
        }

        private void FillIconList()
        {
            cmbProfileImage.Items.AddRange(Enum.GetNames(typeof(ProfileImage)));
        }

        private void CheckForLogonTask()
        {
            if (StartupTaskManager.TaskExists())
            {
                // A logon task is defined

                if (Profiles.Exists(pr => pr.Name == StartupTaskManager.GetTaskParameter()))
                {
                    // The logon profile still exists
                    chkProfileLogOn.Checked = true;
                    cmbProfileLogOn.SelectedItem = Profiles.Find(pr => pr.Name == StartupTaskManager.GetTaskParameter());
                }
                else
                {
                    // The logon profile doesn't exist anymore, delete the task
                    StartupTaskManager.DeleteTask();
                    chkProfileLogOn.Checked = false;
                    cmbProfileLogOn.Enabled = false;
                    cmbProfileLogOn.SelectedIndex = 0;
                }
            }
            else
            {
                // No logon task defined
                chkProfileLogOn.Checked = false;
                cmbProfileLogOn.Enabled = false;
                cmbProfileLogOn.SelectedIndex = 0;
            }
        }

        private void UpdateProfileList()
        {
            int selectedProfileIndex = lstOptionsProfiles.SelectedIndex;
            int selectedLogonProfile = cmbProfileLogOn.SelectedIndex;

            lstOptionsProfiles.Items.Clear();
            lstOptionsProfiles.Items.AddRange(Profiles.ToArray());

            cmbProfileLogOn.Items.Clear();
            cmbProfileLogOn.Items.AddRange(Profiles.ToArray());

            if (Profiles.Count > 7)
            {
                btnAddProfile.Enabled = false;
            }
            else
            {
                btnAddProfile.Enabled = true;
            }
        }

        private void btnSettingsOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lstOptionsProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Profiles[lstOptionsProfiles.SelectedIndex] == null)
            {
                return;
            }

            XTUProfile selectedProfile = Profiles[lstOptionsProfiles.SelectedIndex];
            txtName.Text = selectedProfile.Name;
            numMinW.Value = (decimal)selectedProfile.MinimumWatt;
            numMaxW.Value = (decimal)selectedProfile.MaximumWatt;
            numCPUuv.Value = selectedProfile.CPUUndervolt;
            numGPUuv.Value = selectedProfile.GPUUndervolt;
            cmbProfileImage.SelectedItem = selectedProfile.ProfileImage.ToString();

            if (lstOptionsProfiles.SelectedIndex == 0)
            {
                DisableEditControls();
            }
            else
            {
                EnableEditControls();
            }
        }

        private void DisableEditControls()
        {
            btnDeleteProfile.Enabled = false;
            txtName.Enabled = false;
            numMinW.Enabled = false;
            numMaxW.Enabled = false;
            numCPUuv.Enabled = false;
            numGPUuv.Enabled = false;
            cmbProfileImage.Enabled = false;
        }

        private void EnableEditControls()
        {
            btnDeleteProfile.Enabled = true;
            txtName.Enabled = true;
            numMinW.Enabled = true;
            numMaxW.Enabled = true;
            numCPUuv.Enabled = true;
            numGPUuv.Enabled = true;
            cmbProfileImage.Enabled = true;
        }

        private void btnAddProfile_Click(object sender, EventArgs e)
        {
            CreateAndAddNewProfile();
        }

        private void CreateAndAddNewProfile()
        {
            if (Profiles.Count < 8)
            {
                XTUProfile newProfile = new XTUProfile();
                int newProfileCount = Profiles.Count(p => p.Name.StartsWith(newProfile.Name));

                if (newProfileCount > 0) // Already NEW_PROFILE profiles
                {
                    newProfile.Name += (newProfileCount + 1);
                }

                Profiles.Add(newProfile);
                UpdateProfileList();
                CheckForLogonTask();
                lstOptionsProfiles.SelectedIndex = lstOptionsProfiles.Items.Count - 1;
            }
        }

        private void btnDeleteProfile_Click(object sender, EventArgs e)
        {
            DeleteSelectedProfile();
        }

        private void DeleteSelectedProfile()
        {
            if (lstOptionsProfiles.SelectedIndex > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete the " + Profiles[lstOptionsProfiles.SelectedIndex].Name + " profile?", "Delete profile?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Profiles.RemoveAt(lstOptionsProfiles.SelectedIndex);
                    UpdateProfileList();
                    lstOptionsProfiles.SelectedIndex = 0;
                    CheckForLogonTask();
                }
            }
            else
            {
                MessageBox.Show("Can't delete stock profile!");
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                return;
            }

            int selectedProfileIndex = lstOptionsProfiles.SelectedIndex;
            int selectedLogonIndex = cmbProfileLogOn.SelectedIndex;
            int selectedTextIndex = txtName.SelectionStart;

            // Check for logon key if enabled
            if (chkProfileLogOn.Checked)
            {
                CheckForLogonTask();
            }

            if (selectedProfileIndex > 0)
            {
                Profiles[selectedProfileIndex].Name = txtName.Text.Replace(" ", "_"); // Don't allow spaces
                UpdateProfileList();
                lstOptionsProfiles.SelectedIndex = selectedProfileIndex;
                cmbProfileLogOn.SelectedIndex = selectedLogonIndex;
                txtName.SelectionStart = selectedTextIndex;
            }

        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            Shared.SaveProfilesToDisk(Profiles);
        }

        private void numMinW_ValueChanged(object sender, EventArgs e)
        {
            int selected = lstOptionsProfiles.SelectedIndex;
            Profiles[selected].MinimumWatt = (double)numMinW.Value;
        }

        private void numMaxW_ValueChanged(object sender, EventArgs e)
        {
            int selected = lstOptionsProfiles.SelectedIndex;
            Profiles[selected].MaximumWatt = (double)numMaxW.Value;
        }

        private void numCPUuv_ValueChanged(object sender, EventArgs e)
        {
            int selected = lstOptionsProfiles.SelectedIndex;
            Profiles[selected].CPUUndervolt = (int)numCPUuv.Value;
        }

        private void numGPUuv_ValueChanged(object sender, EventArgs e)
        {
            int selected = lstOptionsProfiles.SelectedIndex;
            Profiles[selected].GPUUndervolt = (int)numGPUuv.Value;
        }

        private void chkProfileLogOn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProfileLogOn.Checked)
            {
                cmbProfileLogOn.Enabled = true;
                AddSelectedProfileInComboboxToLogon();
            }
            else
            {
                cmbProfileLogOn.Enabled = false;
                StartupTaskManager.DeleteTask();
            }
        }

        private void cmbProfileLogOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkProfileLogOn.Checked && cmbProfileLogOn.Enabled)
            {
                AddSelectedProfileInComboboxToLogon();
            }
        }

        private void AddSelectedProfileInComboboxToLogon()
        {
            if (cmbProfileLogOn.SelectedIndex < 0)
            {
                return;
            }

            XTUProfile selectedProfile = Profiles[cmbProfileLogOn.SelectedIndex];
            StartupTaskManager.CreateTask(selectedProfile.Name);
        }

        private void cmbProfileImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(cmbProfileImage.SelectedItem.ToString(), out ProfileImage image);
            picIcon.Image = Shared.IMAGE_RESOURCES_DICTIONARY[image];

            int selected = lstOptionsProfiles.SelectedIndex;
            Profiles[selected].ProfileImage = image;
        }
    }
}
