﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace DayGame
{
    public partial class ToDoTaskLabel : Form
    {
        private readonly ToDo toDo;
        private readonly Character character;
        private readonly Action onUpdate;
        private readonly SaveFile saveFile;

        public ToDoTaskLabel(ToDo toDo, Character character, Action onUpdate, SaveFile saveFile)
        {
            this.toDo = toDo;
            this.character = character;
            this.onUpdate = onUpdate;
            this.saveFile = saveFile;
            InitializeComponent();

            if (toDo.DueDate < DateTime.Now)
            {
                taskFailed();
            }
            else
            {
                timeLabel.Text = toDo.DueDate.Date.ToString();
                nameLabel.Text = toDo.Name;
                var descr = toDo.Description;
                descriptionLabel.Text = descr.Length > 50 ? $@"{descr.Substring(0, 50)}..." : descr;
                nameLabel.Text = toDo.Name;
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            toDo.UpdateTask(true, character);
            onUpdate();
            button1_Click(sender, e);
        }

        private void taskFailed()
        {
            toDo.UpdateTask(false, character);
            onUpdate();
            saveFile.Tasks.Remove(toDo);
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFile.Tasks.Remove(toDo);
            Close();
        }


        private void descriptionLabel_Click(object sender, EventArgs e)
        {
            MessageBox.Show(toDo.Description, toDo.Name);
        }
    }
}