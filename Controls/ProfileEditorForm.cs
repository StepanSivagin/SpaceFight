﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SF.Controls
{
    public partial class ProfileEditorForm : Form
    {
        public ProfileEditorForm()
        {
            InitializeComponent();
        }

        private void curveEditorControl_Resize(object sender, EventArgs e)
        {
            curveEditorControl.Invalidate();
        }
    }
}
