namespace bmpreader
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbxInput = new System.Windows.Forms.TextBox();
            this.btnBrowseInput = new System.Windows.Forms.Button();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.tbxOutputFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ofdInfile = new System.Windows.Forms.OpenFileDialog();
            this.fbdOutfolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnProcess = new System.Windows.Forms.Button();
            this.pbxImagePreview = new System.Windows.Forms.PictureBox();
            this.btnProcessBMP = new System.Windows.Forms.Button();
            this.btnBrowseOutBMP = new System.Windows.Forms.Button();
            this.tbxBMPOut = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOpenBMP = new System.Windows.Forms.Button();
            this.tbxBMPIn = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input File";
            // 
            // tbxInput
            // 
            this.tbxInput.Location = new System.Drawing.Point(12, 25);
            this.tbxInput.Name = "tbxInput";
            this.tbxInput.Size = new System.Drawing.Size(418, 20);
            this.tbxInput.TabIndex = 1;
            // 
            // btnBrowseInput
            // 
            this.btnBrowseInput.Location = new System.Drawing.Point(436, 23);
            this.btnBrowseInput.Name = "btnBrowseInput";
            this.btnBrowseInput.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseInput.TabIndex = 2;
            this.btnBrowseInput.Text = "Browse";
            this.btnBrowseInput.UseVisualStyleBackColor = true;
            this.btnBrowseInput.Click += new System.EventHandler(this.btnBrowseInput_Click);
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Location = new System.Drawing.Point(436, 62);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseOutput.TabIndex = 5;
            this.btnBrowseOutput.Text = "Browse";
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // tbxOutputFile
            // 
            this.tbxOutputFile.Location = new System.Drawing.Point(12, 64);
            this.tbxOutputFile.Name = "tbxOutputFile";
            this.tbxOutputFile.Size = new System.Drawing.Size(418, 20);
            this.tbxOutputFile.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output File";
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(12, 90);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 6;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // pbxImagePreview
            // 
            this.pbxImagePreview.Location = new System.Drawing.Point(12, 119);
            this.pbxImagePreview.Name = "pbxImagePreview";
            this.pbxImagePreview.Size = new System.Drawing.Size(1024, 1024);
            this.pbxImagePreview.TabIndex = 7;
            this.pbxImagePreview.TabStop = false;
            // 
            // btnProcessBMP
            // 
            this.btnProcessBMP.Location = new System.Drawing.Point(531, 90);
            this.btnProcessBMP.Name = "btnProcessBMP";
            this.btnProcessBMP.Size = new System.Drawing.Size(75, 23);
            this.btnProcessBMP.TabIndex = 14;
            this.btnProcessBMP.Text = "Process";
            this.btnProcessBMP.UseVisualStyleBackColor = true;
            this.btnProcessBMP.Click += new System.EventHandler(this.btnProcessBMP_Click);
            // 
            // btnBrowseOutBMP
            // 
            this.btnBrowseOutBMP.Location = new System.Drawing.Point(955, 62);
            this.btnBrowseOutBMP.Name = "btnBrowseOutBMP";
            this.btnBrowseOutBMP.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseOutBMP.TabIndex = 13;
            this.btnBrowseOutBMP.Text = "Browse";
            this.btnBrowseOutBMP.UseVisualStyleBackColor = true;
            this.btnBrowseOutBMP.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // tbxBMPOut
            // 
            this.tbxBMPOut.Location = new System.Drawing.Point(531, 64);
            this.tbxBMPOut.Name = "tbxBMPOut";
            this.tbxBMPOut.Size = new System.Drawing.Size(418, 20);
            this.tbxBMPOut.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(531, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Output File";
            // 
            // btnOpenBMP
            // 
            this.btnOpenBMP.Location = new System.Drawing.Point(955, 23);
            this.btnOpenBMP.Name = "btnOpenBMP";
            this.btnOpenBMP.Size = new System.Drawing.Size(75, 23);
            this.btnOpenBMP.TabIndex = 10;
            this.btnOpenBMP.Text = "Browse";
            this.btnOpenBMP.UseVisualStyleBackColor = true;
            this.btnOpenBMP.Click += new System.EventHandler(this.btnBrowseInput_Click);
            // 
            // tbxBMPIn
            // 
            this.tbxBMPIn.Location = new System.Drawing.Point(531, 25);
            this.tbxBMPIn.Name = "tbxBMPIn";
            this.tbxBMPIn.Size = new System.Drawing.Size(418, 20);
            this.tbxBMPIn.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(531, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Input File";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 1053);
            this.Controls.Add(this.btnProcessBMP);
            this.Controls.Add(this.btnBrowseOutBMP);
            this.Controls.Add(this.tbxBMPOut);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOpenBMP);
            this.Controls.Add(this.tbxBMPIn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pbxImagePreview);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.btnBrowseOutput);
            this.Controls.Add(this.tbxOutputFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBrowseInput);
            this.Controls.Add(this.tbxInput);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxInput;
        private System.Windows.Forms.Button btnBrowseInput;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.TextBox tbxOutputFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog ofdInfile;
        private System.Windows.Forms.FolderBrowserDialog fbdOutfolder;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.PictureBox pbxImagePreview;
        private System.Windows.Forms.Button btnProcessBMP;
        private System.Windows.Forms.Button btnBrowseOutBMP;
        private System.Windows.Forms.TextBox tbxBMPOut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOpenBMP;
        private System.Windows.Forms.TextBox tbxBMPIn;
        private System.Windows.Forms.Label label4;
    }
}

