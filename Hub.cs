using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace KwicFrontend
{
    public partial class hub : Form
    {
        public string newProject;
        public string selectedProject;
        public Process codeProcess;
        public Process chrome;
        public DirectoryInfo bin;
        public editor Editor;
        public eCon EditorContainer;
        private bool pNameContain = false;
        public hub()
        {
            InitializeComponent();
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            path = path.Substring(6);
            bin = new DirectoryInfo(path);
            bin = new DirectoryInfo(bin.Parent.FullName);
            DirectoryInfo projectData = new DirectoryInfo(bin.FullName + @"\projects\" + "projectData.json");
            string json = File.ReadAllText(projectData.FullName);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            JObject jObj = JObject.FromObject(jsonObj);
            IEnumerable<JToken> pNames = jObj["projects"].Values();
            foreach (JToken pt in pNames)
            {
                Debug.WriteLine(pt);
                projectList.Items.Add(new ListViewItem(pt.ToString()));
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private void createBTN_Click(object sender, EventArgs e)
        {
            openEditor(true);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            newProject = projectTitle.Text;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (projectList.SelectedItems.Count > 0)
            {
                selectedProject = projectList.SelectedItems[0].Text;
            }
            else { return; }
        }
        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        private void loadBTN_click(object sender, EventArgs e)
        {
            openEditor(false);
        }

        private void hubLoad(object sender, EventArgs e)
        {

        }

        private void openEditor(bool save)
        {
            DirectoryInfo code = new DirectoryInfo(bin.FullName + @"\apps\vscode\Code.exe . ");
            DirectoryInfo chromeDI = new DirectoryInfo(bin.FullName + @"\apps\chrome-win\chrome.exe");
            DirectoryInfo templateDI = new DirectoryInfo(bin.FullName + @"\projects\" + "templateproject");
            DirectoryInfo newDI = new DirectoryInfo(bin.FullName + @"\projects\" + projectTitle.Text);
            DirectoryInfo settingsDI = new DirectoryInfo(bin.FullName + @"\apps\vscode\data\user-data\storage.json");
            DirectoryInfo backupDI = new DirectoryInfo(bin.FullName + @"\apps\vscode\data\user-data\_storage.json");
            DirectoryInfo projectData = new DirectoryInfo(bin.FullName + @"\projects\" + "projectData.json");
            DirectoryInfo liveServer = new DirectoryInfo(bin.FullName + @"\apps\vscode\data\user-data\User\settings.json");

            //[DllImport("user32.dll")]
            //saving the project name to a list
            if (save)
            {
                CopyFilesRecursively(templateDI.FullName, newDI.FullName);
                //changing visual studio's window
                string json = File.ReadAllText(backupDI.FullName);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);
                Uri projURI = new Uri(newDI.FullName);
                jsonObj["windowsState"]["lastActiveWindow"]["folder"] = projURI.AbsoluteUri;
                Debug.WriteLine(projURI);
                jsonObj["windowsState"]["lastActiveWindow"]["backupPath"] = "";
                string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText(settingsDI.FullName, output);
                string json2 = File.ReadAllText(projectData.FullName);
                dynamic jsonObj2 = JsonConvert.DeserializeObject(json2);
                JObject jObj = JObject.FromObject(jsonObj2);
                IEnumerable<JToken> pNames = jObj["projects"].Values();
                pNameContain = false;
                foreach (JToken jt in pNames)
                {
                    if (jt.ToString() == newProject)
                    {
                        pNameContain = true;
                    }
                }
                if (!pNameContain) { jsonObj2["projects"].Add(newProject); projectList.Items.Add(new ListViewItem(newProject)); }
                if (pNameContain) { pNameContain = false; }
                string output2 = JsonConvert.SerializeObject(jsonObj2, Formatting.Indented);
                File.WriteAllText(projectData.FullName, output2);
                //point liverserver to chrome.exe directory>>>
                string lsJson = File.ReadAllText(liveServer.FullName);
                dynamic lsObj = JsonConvert.DeserializeObject(lsJson);
                if (lsObj["liveServer.settings.AdvanceCustomBrowserCmdLine"] != null) { lsObj["liveServer.settings.AdvanceCustomBrowserCmdLine"] = @chromeDI.FullName; }
                else { lsObj.Add(new JProperty("liveServer.settings.AdvanceCustomBrowserCmdLine", @chromeDI.FullName)); }
                string output3 = JsonConvert.SerializeObject(lsObj, Formatting.Indented);
                File.WriteAllText(liveServer.FullName, output3);
            }
            else
            {   //changing visual studio's window
                string json = File.ReadAllText(backupDI.FullName);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);
                Uri projURI = new Uri(@bin.FullName + @"\projects\" + selectedProject);
                jsonObj["windowsState"]["lastActiveWindow"]["folder"] = projURI.AbsoluteUri;
                Debug.WriteLine(projURI);
                jsonObj["windowsState"]["lastActiveWindow"]["backupPath"] = "";
                //save window size
                string ojson = File.ReadAllText(settingsDI.FullName);
                dynamic ojsonObj = JsonConvert.DeserializeObject(ojson);
                if (ojsonObj["windowsState"]["lastActiveWindow"] != null)
                { jsonObj["windowsState"]["lastActiveWindow"]["uiState"] = ojsonObj["windowsState"]["lastActiveWindow"]["uiState"]; }
                string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText(settingsDI.FullName, output);
                //point liverserver to chrome.exe directory>>>
                string lsJson = File.ReadAllText(liveServer.FullName);
                dynamic lsObj = JsonConvert.DeserializeObject(lsJson);
                if (lsObj["liveServer.settings.AdvanceCustomBrowserCmdLine"] != null) { lsObj["liveServer.settings.AdvanceCustomBrowserCmdLine"] = @chromeDI.FullName; }
                else { lsObj.Add(new JProperty("liveServer.settings.AdvanceCustomBrowserCmdLine", @chromeDI.FullName)); }
                string output3 = JsonConvert.SerializeObject(lsObj, Formatting.Indented);
                File.WriteAllText(liveServer.FullName, output3);
            }
            // now we need to navigate to the directory with in the shell of the template project and then start the application itself using the -n argument.
            codeProcess = Process.Start(code.FullName);
            codeProcess.WaitForInputIdle();
            while (codeProcess.MainWindowHandle == IntPtr.Zero)
            {
                Thread.Sleep(100);
                codeProcess.Refresh();
            }
            chrome = Process.Start(chromeDI.FullName, "--test-type");
            chrome.StartInfo.UseShellExecute = true;
            //chrome.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            chrome.EnableRaisingEvents = true;
            chrome.Exited += new EventHandler(chrome_Exited);
            chrome.WaitForInputIdle();

            while (chrome.MainWindowHandle == IntPtr.Zero)
            {
                Thread.Sleep(100);
                chrome.Refresh();
            }
            Editor = new editor();
            SetParent(codeProcess.MainWindowHandle, Editor.Handle);
            SetParent(chrome.MainWindowHandle, Editor.Handle);
            Editor.Show();
            codeProcess.EnableRaisingEvents = true;
            codeProcess.Exited += new EventHandler(code_Exited);
            Editor.Hub = this;
            //Editor.Browser = chrome;
            this.Hide();
        }
        private void chrome_Exited(object sender, System.EventArgs e)
        {
            if (Editor.InvokeRequired)
                Editor.Invoke(new Action(resetChrome));
        }
        private void code_Exited(object sender, System.EventArgs e)
        {
            if (Editor.InvokeRequired)
                Editor.Invoke(new Action(closeEditor));
        }
        private void closeEditor()
        {
            Editor.Close();
        }
        private void resetChrome()
        {
            DirectoryInfo chromeDI = new DirectoryInfo(bin.FullName + @"\apps\chrome-win\chrome.exe");
            chrome = Process.Start(chromeDI.FullName);
            chrome.EnableRaisingEvents = true;
            chrome.Exited += new EventHandler(chrome_Exited);
            chrome.WaitForInputIdle();

            while (chrome.MainWindowHandle == IntPtr.Zero)
            {
                Thread.Sleep(100);
                chrome.Refresh();
            }
            SetParent(chrome.MainWindowHandle, Editor.Handle);
        }

        private void deleteBTN_Click(object sender, EventArgs e)
        {
            DirectoryInfo newDI = new DirectoryInfo(bin.FullName + @"\projects\" + selectedProject);
            DirectoryInfo settingsDI = new DirectoryInfo(bin.FullName + @"\apps\vscode\data\user-data\storage.json");
            DirectoryInfo backupDI = new DirectoryInfo(bin.FullName + @"\apps\vscode\data\user-data\_storage.json");
            DirectoryInfo projectData = new DirectoryInfo(bin.FullName + @"\projects\" + "projectData.json");
           
            //changing visual studio's window
            string json = File.ReadAllText(backupDI.FullName);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            jsonObj["windowsState"]["lastActiveWindow"]["folder"] = bin.FullName +@"\";
            jsonObj["windowsState"]["lastActiveWindow"]["backupPath"] = "";
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(settingsDI.FullName, output);
            string json2 = File.ReadAllText(projectData.FullName);
            dynamic jsonObj2 = JsonConvert.DeserializeObject(json2);
            JObject jObj = JObject.FromObject(jsonObj2);
            IEnumerable<JToken> pNames = jObj["projects"].Values();
            pNameContain = false;
            foreach (JToken jt in pNames)
            {
                if (jt.ToString() == selectedProject)
                {
                    pNameContain = true;
                }
            }
            if (pNameContain && selectedProject != "templateproject") {
                newDI.Delete(true);
                jObj["projects"][projectList.Items.IndexOf(projectList.SelectedItems[0])].Remove();
                json2 = jObj.ToString();
                jsonObj2 = JsonConvert.DeserializeObject(json2);
                ListViewItem sP = projectList.SelectedItems[0];
                projectList.Items.Remove(sP);
            }
            if (!pNameContain) { pNameContain = false; }
            string output2 = JsonConvert.SerializeObject(jsonObj2, Formatting.Indented);
            File.WriteAllText(projectData.FullName, output2);
        }

    }
}