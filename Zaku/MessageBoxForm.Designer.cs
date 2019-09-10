namespace Zaku
{
    partial class MessageBoxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBoxForm));
            this.materialLabel_Content = new MaterialSkin.Controls.MaterialLabel();
            this.materialFlatButton_OK = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialRaisedButton_Cancel = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // materialLabel_Content
            // 
            this.materialLabel_Content.AutoSize = true;
            this.materialLabel_Content.Depth = 0;
            this.materialLabel_Content.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel_Content.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel_Content.Location = new System.Drawing.Point(28, 97);
            this.materialLabel_Content.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel_Content.Name = "materialLabel_Content";
            this.materialLabel_Content.Size = new System.Drawing.Size(0, 19);
            this.materialLabel_Content.TabIndex = 0;
            this.materialLabel_Content.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // materialFlatButton_OK
            // 
            this.materialFlatButton_OK.AutoSize = true;
            this.materialFlatButton_OK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton_OK.Depth = 0;
            this.materialFlatButton_OK.Icon = null;
            this.materialFlatButton_OK.Location = new System.Drawing.Point(236, 149);
            this.materialFlatButton_OK.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButton_OK.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButton_OK.Name = "materialFlatButton_OK";
            this.materialFlatButton_OK.Primary = false;
            this.materialFlatButton_OK.Size = new System.Drawing.Size(39, 36);
            this.materialFlatButton_OK.TabIndex = 1;
            this.materialFlatButton_OK.Text = "OK";
            this.materialFlatButton_OK.UseVisualStyleBackColor = true;
            this.materialFlatButton_OK.Click += new System.EventHandler(this.materialFlatButton_OK_Click);
            // 
            // materialRaisedButton_Cancel
            // 
            this.materialRaisedButton_Cancel.AutoSize = true;
            this.materialRaisedButton_Cancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialRaisedButton_Cancel.Depth = 0;
            this.materialRaisedButton_Cancel.Icon = null;
            this.materialRaisedButton_Cancel.Location = new System.Drawing.Point(161, 149);
            this.materialRaisedButton_Cancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton_Cancel.Name = "materialRaisedButton_Cancel";
            this.materialRaisedButton_Cancel.Primary = true;
            this.materialRaisedButton_Cancel.Size = new System.Drawing.Size(51, 36);
            this.materialRaisedButton_Cancel.TabIndex = 2;
            this.materialRaisedButton_Cancel.Text = "取消";
            this.materialRaisedButton_Cancel.UseVisualStyleBackColor = true;
            this.materialRaisedButton_Cancel.Click += new System.EventHandler(this.materialRaisedButton_Cancel_Click);
            // 
            // MessageBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.materialRaisedButton_Cancel);
            this.Controls.Add(this.materialFlatButton_OK);
            this.Controls.Add(this.materialLabel_Content);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBoxForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "提示";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MessageBoxForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel materialLabel_Content;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton_OK;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton_Cancel;
    }
}