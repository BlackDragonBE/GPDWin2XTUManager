using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

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
            UpdateLogonUI();
            lstOptionsProfiles.SelectedIndex = 0;
        }

        private void UpdateLogonUI()
        {
            if (RegistryManager.LogonRegistryKeyExists())
            {
                // A logon key is defined

                if (Profiles.Exists(pr => pr.Name == RegistryManager.GetLogonProfileKeyValue()))
                {
                    // The logon profile still exists
                    chkProfileLogOn.Checked = true;
                    cmbProfileLogOn.SelectedItem = Profiles.Find(pr => pr.Name == RegistryManager.GetLogonProfileKeyValue());
                }
                else
                {
                    // The logon profile doesn't exist, clear the key
                    RegistryManager.ClearLogonProfileKey();
                    chkProfileLogOn.Checked = false;
                    cmbProfileLogOn.Enabled = false;
                }
            }
        }

        private void UpdateProfileList()
        {
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
            numMinW.Value = (decimal) selectedProfile.MinimumWatt;
            numMaxW.Value = (decimal) selectedProfile.MaximumWatt;
            numCPUuv.Value = selectedProfile.CPUUndervolt;
            numGPUuv.Value = selectedProfile.GPUUndervolt;

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

        }

        private void EnableEditControls()
        {
            btnDeleteProfile.Enabled = true;
            txtName.Enabled = true;
            numMinW.Enabled = true;
            numMaxW.Enabled = true;
            numCPUuv.Enabled = true;
            numGPUuv.Enabled = true;
        }

        private void btnAddProfile_Click(object sender, EventArgs e)
        {
            CreateAndAddNewProfile();
        }

        private void CreateAndAddNewProfile()
        {
            if (Profiles.Count < 8)
            {
                Profiles.Add(new XTUProfile("NEW_PROFILE", 7, 15, 0, 0));
                UpdateProfileList();
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
            
            int selected = lstOptionsProfiles.SelectedIndex;

            if (selected > 0)
            {
                Profiles[selected].Name = txtName.Text.Replace(" ", "_"); // Don't allow spaces
                UpdateProfileList();
                lstOptionsProfiles.SelectedIndex = selected;
            }

        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            Shared.SaveProfilesToDisk(Profiles);
        }

        private void numMinW_ValueChanged(object sender, EventArgs e)
        {
            int selected = lstOptionsProfiles.SelectedIndex;

            if (selected > 0)
            {
                Profiles[selected].MinimumWatt = (double) numMinW.Value;
                UpdateProfileList();
                lstOptionsProfiles.SelectedIndex = selected;
            }
        }

        private void numMaxW_ValueChanged(object sender, EventArgs e)
        {
            int selected = lstOptionsProfiles.SelectedIndex;

            if (selected > 0)
            {
                Profiles[selected].MaximumWatt = (double)numMaxW.Value;
                UpdateProfileList();
                lstOptionsProfiles.SelectedIndex = selected;
            }
        }

        private void numCPUuv_ValueChanged(object sender, EventArgs e)
        {
            int selected = lstOptionsProfiles.SelectedIndex;

            if (selected > 0)
            {
                Profiles[selected].CPUUndervolt = (int) numCPUuv.Value;
                UpdateProfileList();
                lstOptionsProfiles.SelectedIndex = selected;
            }
        }

        private void numGPUuv_ValueChanged(object sender, EventArgs e)
        {
            int selected = lstOptionsProfiles.SelectedIndex;

            if (selected > 0)
            {
                Profiles[selected].GPUUndervolt = (int)numGPUuv.Value;
                UpdateProfileList();
                lstOptionsProfiles.SelectedIndex = selected;
            }
        }

        private void chkProfileLogOn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProfileLogOn.Checked)
            {
                cmbProfileLogOn.Enabled = true;
            }
            else
            {
                cmbProfileLogOn.Enabled = false;
                RegistryManager.ClearLogonProfileKey();
            }
        }
    }
}
