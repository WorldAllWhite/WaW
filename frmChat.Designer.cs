namespace WaW
{
    partial class frmChat
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
            this.rtfRcv = new WaW.ExRichTextBox();
            this.rtfSend = new WaW.ExRichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtfRcv
            // 
            this.rtfRcv.HiglightColor = WaW.RtfColor.White;
            this.rtfRcv.Location = new System.Drawing.Point(5, 2);
            this.rtfRcv.Name = "rtfRcv";
            this.rtfRcv.ReadOnly = true;
            this.rtfRcv.Size = new System.Drawing.Size(464, 229);
            this.rtfRcv.TabIndex = 0;
            this.rtfRcv.Text = "";
            this.rtfRcv.TextColor = WaW.RtfColor.Black;
            // 
            // rtfSend
            // 
            this.rtfSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfSend.HiglightColor = WaW.RtfColor.White;
            this.rtfSend.Location = new System.Drawing.Point(5, 258);
            this.rtfSend.Name = "rtfSend";
            this.rtfSend.Size = new System.Drawing.Size(464, 142);
            this.rtfSend.TabIndex = 1;
            this.rtfSend.Text = "";
            this.rtfSend.TextColor = WaW.RtfColor.Black;
            this.rtfSend.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rtfSend_MouseClick);
            this.rtfSend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtfSend_KeyPress);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(357, 408);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "发送(Enter)";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // frmChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 441);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.rtfSend);
            this.Controls.Add(this.rtfRcv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmChat";
            this.Text = "frmChat";
            this.Load += new System.EventHandler(this.frmChat_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ExRichTextBox rtfRcv;
        private ExRichTextBox rtfSend;
        private System.Windows.Forms.Button btnSend;
    }
}