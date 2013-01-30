namespace NSCalendar
{
    partial class frmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabconMain = new System.Windows.Forms.TabControl();
            this.tabScheduleList = new System.Windows.Forms.TabPage();
            this.tabMember = new System.Windows.Forms.TabPage();
            this.tabScheduleState = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.dtpSelectinMonth = new System.Windows.Forms.DateTimePicker();
            this.listView1 = new System.Windows.Forms.ListView();
            this.tabconMain.SuspendLayout();
            this.tabScheduleList.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabconMain
            // 
            this.tabconMain.Controls.Add(this.tabScheduleList);
            this.tabconMain.Controls.Add(this.tabMember);
            this.tabconMain.Controls.Add(this.tabScheduleState);
            this.tabconMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabconMain.Location = new System.Drawing.Point(0, 0);
            this.tabconMain.Name = "tabconMain";
            this.tabconMain.SelectedIndex = 0;
            this.tabconMain.Size = new System.Drawing.Size(981, 631);
            this.tabconMain.TabIndex = 0;
            // 
            // tabScheduleList
            // 
            this.tabScheduleList.Controls.Add(this.flowLayoutPanel1);
            this.tabScheduleList.Location = new System.Drawing.Point(4, 22);
            this.tabScheduleList.Name = "tabScheduleList";
            this.tabScheduleList.Padding = new System.Windows.Forms.Padding(3);
            this.tabScheduleList.Size = new System.Drawing.Size(973, 605);
            this.tabScheduleList.TabIndex = 0;
            this.tabScheduleList.Text = "일정";
            this.tabScheduleList.UseVisualStyleBackColor = true;
            // 
            // tabMember
            // 
            this.tabMember.Location = new System.Drawing.Point(4, 22);
            this.tabMember.Name = "tabMember";
            this.tabMember.Padding = new System.Windows.Forms.Padding(3);
            this.tabMember.Size = new System.Drawing.Size(973, 605);
            this.tabMember.TabIndex = 1;
            this.tabMember.Text = "인원";
            this.tabMember.UseVisualStyleBackColor = true;
            // 
            // tabScheduleState
            // 
            this.tabScheduleState.Location = new System.Drawing.Point(4, 22);
            this.tabScheduleState.Name = "tabScheduleState";
            this.tabScheduleState.Size = new System.Drawing.Size(973, 605);
            this.tabScheduleState.TabIndex = 2;
            this.tabScheduleState.Text = "현황";
            this.tabScheduleState.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.dtpSelectinMonth);
            this.flowLayoutPanel1.Controls.Add(this.listView1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(967, 599);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // dtpSelectinMonth
            // 
            this.dtpSelectinMonth.CustomFormat = "yyyy년 MM월";
            this.dtpSelectinMonth.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtpSelectinMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSelectinMonth.Location = new System.Drawing.Point(3, 3);
            this.dtpSelectinMonth.Name = "dtpSelectinMonth";
            this.dtpSelectinMonth.Size = new System.Drawing.Size(103, 21);
            this.dtpSelectinMonth.TabIndex = 3;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(3, 30);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(967, 576);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 631);
            this.Controls.Add(this.tabconMain);
            this.Name = "frmMain";
            this.Text = "NSCalendar";
            this.tabconMain.ResumeLayout(false);
            this.tabScheduleList.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabconMain;
        private System.Windows.Forms.TabPage tabScheduleList;
        private System.Windows.Forms.TabPage tabMember;
        private System.Windows.Forms.TabPage tabScheduleState;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.DateTimePicker dtpSelectinMonth;
    }
}

